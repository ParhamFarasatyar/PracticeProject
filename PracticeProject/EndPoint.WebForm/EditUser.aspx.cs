using PracticeProject.Application.BaseInfo;
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
    public partial class EditUser : System.Web.UI.Page
    {
        public IUserDataAccess UserDataAccess { get; set; }
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

                var user = UserDataAccess.GetById(userId);
                if (user is null)
                {
                    Response.Redirect("./Administrator.aspx");
                    return;
                }

                tboxFirstName.Text = user.Person.FirstName;
                tboxLastName.Text = user.Person.LastName;
                tboxAge.Text = user.Person.Age.ToString();
                tboxEmail.Text = user.Email;
                tboxPassword.Text = user.Password;
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

            IUpdateUser.UpdateUserRequest request = new IUpdateUser.UpdateUserRequest
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

            IUpdateUser.UpdateUserResponse response = UpdateUser.Execute(request);

            if (response.IsSuccess)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = response.Message;
            }

            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = response.Message;
        }
    }
}