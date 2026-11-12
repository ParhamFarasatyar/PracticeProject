using PracticeProject.Application.BaseInfo.UserManagement.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PracticeProject.Domain.BaseInfo.People;
using System.Threading;

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

            var request = new ICreateUser.CreateUserRequest
            {
                FirstName = tboxFirstName.Text.Trim(),
                LastName = tboxLastName.Text.Trim(),
                Age = Convert.ToInt32(tboxAge.Text.Trim()),
                Gender = (Gender)Convert.ToInt32(ddlGender.SelectedValue),
                Email = tboxEmail.Text.Trim(),
                Password = tboxPassword.Text.Trim()
            };

            var response = CreateUserUseCase.Execute(request);

            if (response.IsSuccess)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = response.Message;

                Response.Redirect("Users.aspx");
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = response.Message;
            }
        }
    }
}