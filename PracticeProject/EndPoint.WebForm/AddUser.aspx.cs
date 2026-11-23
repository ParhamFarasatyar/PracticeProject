using PracticeProject.Application.BaseInfos.UserManagement.CreateUser;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System;
using System.Web.UI;

namespace EndPoint.WebForm
{
    public partial class AddUser : AdminBasePage
    {
        public ICreateUser CreateUser { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ICreateUser.Request request = new ICreateUser.Request
            {
                FirstName = tboxFirstName.Text.Trim(),
                LastName = tboxLastName.Text.Trim(),
                Age = Convert.ToInt32(tboxAge.Text),
                Gender = (Gender)Convert.ToInt32(ddlGender.SelectedValue),
                Role = Role.User,
                Email = tboxEmail.Text.Trim().ToLower(),
                Password = tboxPassword.Text.Trim()
            };

            try
            {
                User createdUser = CreateUser.Execute(request);

                Response.Redirect("./Administrator.aspx");
            }
            catch (AppException ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
        }
    }
}