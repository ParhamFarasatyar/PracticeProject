using PracticeProject.Application.BaseInfos.UserManagement.CreateUser;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System;
using System.Web.UI;

namespace EndPoint.WebForm
{
    public partial class Register : System.Web.UI.Page
    {
        public ICreateUser CreateUserUseCase { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            var request = new ICreateUser.Request
            {
                FirstName = tboxFirstName.Text.Trim(),
                LastName = tboxLastName.Text.Trim(),
                Age = Convert.ToInt32(tboxAge.Text.Trim()),
                Gender = (Gender)Convert.ToInt32(ddlGender.SelectedValue),
                Role = Role.User,
                Email = tboxEmail.Text.Trim().ToLower(),
                Password = tboxPassword.Text.Trim()
            };

            try
            {
                User createdUser = CreateUserUseCase.Execute(request);

                Session["UserId"] = createdUser.Id;
                Session["UserRole"] = createdUser.Role;
                Session["IsAuthenticated"] = true;

                if (createdUser.Role == Role.User)
                    Response.Redirect("./Users.aspx");
                else
                    Response.Redirect("./Administrator.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
        }
    }
}