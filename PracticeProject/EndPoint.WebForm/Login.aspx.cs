using PracticeProject.Application.BaseInfo.UserManagement.LoginUser;
using PracticeProject.Domain.BaseInfo.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EndPoint.WebForm
{
    public partial class Login : System.Web.UI.Page
    {
        public ILoginUser LoginUser { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            var request = new ILoginUser.LoginUserRequest
            {
                Email = tboxEmail.Text.Trim().ToLower(),
                Password = tboxPassword.Text.Trim(),
            };

            var response = LoginUser.Execute(request);

            if (response.IsSuccess)
            {
                if (response.User.Role == Role.Admin)
                {
                    Response.Redirect("./Administrator.aspx");
                }
                else
                {
                    Response.Redirect("./Users.aspx");
                }
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = response.Message;
            }
        }
    }
}