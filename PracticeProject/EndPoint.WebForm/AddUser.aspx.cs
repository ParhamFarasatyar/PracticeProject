using PracticeProject.Application.BaseInfo.UserManagement.CreateUser;
using PracticeProject.Domain.BaseInfo.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EndPoint.WebForm
{
    public partial class AddUser : System.Web.UI.Page
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
                Email = tboxEmail.Text.Trim().ToLower(),
                Password = tboxPassword.Text.Trim()
            };

            ICreateUser.Response response = CreateUser.Execute(request);

            if (response.IsSuccess)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = response.Message;
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = response.Message;
            }
        }
    }
}