using PracticeProject.Application.BaseInfos.UserManagement.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System.Threading;
using PracticeProject.Application.BaseInfos.Security;

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

            var response = CreateUserUseCase.Execute(request);

            if (response.IsSuccess)
            {
                Session["UserId"] = response.User.Id;
                Session["UserRole"] = response.User.Role;
                Session["IsAuthenticated"] = true;

                if (response.User.Role == Role.User)
                    Response.Redirect("./Users.aspx");
                else
                    Response.Redirect("./Administrator.aspx");
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = response.Message;
            }
        }
    }
}