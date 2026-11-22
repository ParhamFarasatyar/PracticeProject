using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Application.BaseInfos.UserManagement.DeleteUser;
using PracticeProject.Application.BaseInfos.UserManagement.GetUsersPage;
using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfos.Users;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace EndPoint.WebForm
{
    public partial class Administrator : AdminBasePage
    {
        public IDeleteUser DeleteUser { get; set; }
        public ISearchUsers SearchUsers { get; set; }
        public ISearchPersons SearchPersons { get; set; }
        public IGetUsersPage GetUsersPage { get; set; }

        private int CurrentPage
        {
            get => ViewState["CurrentPage"] is null ? 1 : (int)ViewState["CurrentPage"];
            set => ViewState["CurrentPage"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurrentPage = 1;
                BindUsers();
            }
        }

        private void BindUsers(string keyword = "")
        {
            var response = GetUsersPage.Respond(new IGetUsersPage.Request
            {
                PageNumber = CurrentPage,
                SearchRequest = new ISearchUsers.Request
                {
                    KeyWord = keyword
                }
            });

            if (!string.IsNullOrWhiteSpace(tboxSearchFirstNameOrLastName.Text))
            {
                response = GetUsersPage.Respond(new IGetUsersPage.Request
                {
                    PageNumber = CurrentPage,
                    SearchRequest = new ISearchUsers.Request
                    {
                        KeyWord = keyword,
                        PersonRequest = new ISearchPersons.Request
                        {
                            Keyword = tboxSearchFirstNameOrLastName.Text.Trim()
                        }
                    }
                });
            }

            if (response is null)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Request failed.";
                return;
            }

            var resultList = response.Users.Select(i => new
            {
                i.Id,
                i.Email,
                i.Role,
                i.Person.FirstName,
                i.Person.LastName,
                i.Person.Gender,
                i.Person.Age
            }).ToList();

            gvUsers.DataSource = resultList;
            gvUsers.DataBind();

            CurrentPage = response.CurrentPage;

            lblCurrentPage.Text = response.TotalPages == 0
                ? "Page 0 of 0"
                : $"Page {response.CurrentPage} of {response.TotalPages}";

            btnPrevious.Enabled = response.CurrentPage > 1;

            btnNext.Enabled = response.CurrentPage < response.TotalPages;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            BindUsers(tboxSearchEmail.Text.Trim());
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "EditUser":
                    Response.Redirect($"./EditUser.aspx?id={userId}");
                    break;

                case "DeleteUser":
                    try
                    {
                        DeleteUser.Execute(new IDeleteUser.Request { Id = userId });

                        if (Convert.ToInt32(Session["UserId"]) == userId)
                        {
                            Session["UserId"] = null;
                            Session["UserRole"] = null;
                            Session["IsAuthenticated"] = false;
                            Response.Redirect($"./Login.aspx");
                            return;
                        }

                        BindUsers();
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "User deleted successfully.";
                    }
                    catch (Exception ex)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = ex.Message;
                    }

                    break;
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e) => Response.Redirect("./AddUser.aspx");

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Session["UserRole"] = null;
            Session["IsAuthenticated"] = false;

            Response.Redirect("./Login.aspx");
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                BindUsers();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (btnNext.Enabled)
            {
                CurrentPage++;
                BindUsers();
            }
        }
    }
}