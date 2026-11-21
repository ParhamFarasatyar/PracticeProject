using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Application.BaseInfos.UserManagement.DeleteUser;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindUsers();
        }

        private void BindUsers(string keyword = "")
        {
            IQueryable<User> result = SearchUsers.Respond(new ISearchUsers.Request
            {
                KeyWord = keyword
            });

            if (result is null)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Request failed.";
                return;
            }

            var resultList = result.Select(i => new
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
        }

        protected void btnSearch_Click(object sender, EventArgs e) => BindUsers(tboxSearch.Text.Trim());

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "EditUser":
                    Response.Redirect($"./EditUser.aspx?id={userId}");
                    break;

                case "DeleteUser":
                    IDeleteUser.Response response =
                        DeleteUser.Execute(new IDeleteUser.Request { Id = userId });

                    if (response.IsSuccess)
                    {
                        if (Convert.ToInt32(Session["UserId"]) == userId)
                        {
                            Session["UserId"] = null;
                            Session["UserRole"] = null;
                            Session["IsAuthenticated"] = false;
                            Response.Redirect($"./Login.aspx");
                            return;
                        }

                        BindUsers(tboxSearch.Text.Trim());
                        lblMessage.Text = "User deleted successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = response.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
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
    }
}