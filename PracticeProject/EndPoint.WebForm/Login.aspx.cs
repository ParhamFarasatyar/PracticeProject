using PracticeProject.Application.BaseInfos.UserManagement.LoginUser;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.Users;
using System;
using System.Web.UI;

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

            try
            {
                User user = LoginUser.Execute(request);

                Session["UserId"] = user.Id;
                Session["UserRole"] = user.Role;
                Session["IsAuthenticated"] = true;

                if (user.Role == Role.Admin)
                    Response.Redirect("./Administrator.aspx");

                else
                    Response.Redirect("./Users.aspx");
            }
            catch (AppException ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
        }
    }
}