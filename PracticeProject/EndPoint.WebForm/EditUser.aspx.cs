using PracticeProject.Application.BaseInfo;
using PracticeProject.Application.BaseInfo.UserManagement.GetUserById;
using PracticeProject.Application.BaseInfo.UserManagement.UpdateUser;
using PracticeProject.Domain.BaseInfo.People;
using PracticeProject.Domain.BaseInfo.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EndPoint.WebForm
{
    public partial class EditUser : AdminBasePage
    {
        public IGetUserById GetUserById { get; set; }
        public IUpdateUser UpdateUser { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!int.TryParse(Request.QueryString["id"], out int userId))
                {
                    Response.Redirect("./Administrator.aspx");
                    return;
                }

                var response = GetUserById.Execute(new IGetUserById.Request { Id = userId });
                if (!response.IsSuccess)
                {
                    Response.Redirect("./Administrator.aspx");
                    return;
                }

                tboxFirstName.Text = response.User.Person.FirstName;
                tboxLastName.Text = response.User.Person.LastName;
                tboxAge.Text = response.User.Person.Age.ToString();
                tboxEmail.Text = response.User.Email;
                tboxPassword.Text = response.User.Password;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            if (!int.TryParse(Request.QueryString["id"], out int userId))
            {
                Response.Redirect("./Administrator.aspx");
                return;
            }

            IUpdateUser.Request request = new IUpdateUser.Request
            {
                Id = userId,
                FirstName = tboxFirstName.Text.Trim(),
                LastName = tboxLastName.Text.Trim(),
                Age = Convert.ToInt32(tboxAge.Text),
                Gender = (Gender)Convert.ToInt32(ddlGender.SelectedValue),
                Role = (Role)Convert.ToInt32(ddlRole.SelectedValue),
                Email = tboxEmail.Text.Trim().ToLower(),
                Password = tboxPassword.Text.Trim()
            };

            IUpdateUser.Response response = UpdateUser.Execute(request);

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