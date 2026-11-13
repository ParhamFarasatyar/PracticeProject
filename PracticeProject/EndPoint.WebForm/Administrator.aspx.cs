using PracticeProject.Application.BaseInfo.UserManagement.DeleteUser;
using PracticeProject.Application.BaseInfo.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfo.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EndPoint.WebForm
{
    public partial class Administrator : System.Web.UI.Page
    {
        public IDeleteUser DeleteUser { get; set; }
        public ISearchUsers SearchUsers { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void BindUsers(string searchString)
        {
            ISearchUsers.SearchUsersResponse response = SearchUsers.Execute(new ISearchUsers.SearchUsersRequest { SearchString = searchString });

            if (!response.IsSuccess)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = response.Message;
                return;
            }

            var firstUser = response.SearchResult.FirstOrDefault();

            gvUsers.DataSource = response.SearchResult;
            gvUsers.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUsers(tboxSearch.Text.Trim());
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
                    IDeleteUser.DeleteUserResponse response = 
                        DeleteUser.Execute(new IDeleteUser.DeleteUserRequest { Id = userId });

                    if (response.IsSuccess)
                    {
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
    }
}