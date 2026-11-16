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
                FirstName = string.IsNullOrWhiteSpace(tboxFirstName.Text) ? null : tboxFirstName.Text.Trim(),
                LastName = string.IsNullOrWhiteSpace(tboxLastName.Text) ? null : tboxLastName.Text.Trim(),
                Age = string.IsNullOrWhiteSpace(tboxAge.Text) ? null : Convert.ToInt32(tboxAge.Text),
                Gender = ddlGender.SelectedValue == "" ? null : (Gender)Convert.ToInt32(ddlGender.SelectedValue),
                Role = ddlRole.SelectedValue == "" ? null : (Role)Convert.ToInt32(ddlRole.SelectedValue),
                Email = string.IsNullOrWhiteSpace(tboxEmail.Text) ? null : tboxEmail.Text.Trim().ToLower(),
                Password = string.IsNullOrWhiteSpace(tboxPassword.Text) ? null : tboxPassword.Text.Trim()
            };

            if (request is
                {
                    FirstName: null,
                    LastName: null,
                    Age: null,
                    Gender: null,
                    Role: null,
                    Email: null,
                    Password: null
                })
            {
                Response.Redirect("./Administrator.aspx");
                return;
            }

            IUpdateUser.Response response = UpdateUser.Execute(request);

            if (response.IsSuccess)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = response.Message;
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