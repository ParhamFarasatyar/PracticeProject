using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Application.BaseInfos.UserManagement.GetUserById;
using PracticeProject.Application.BaseInfos.UserManagement.UpdateUser;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System;
using System.Web.UI;
using EndPoint.WebForm.Extensions;

namespace EndPoint.WebForm
{
    public partial class EditUser : AdminBasePage
    {
        public IGetUserById GetUserById { get; set; }
        public IUpdateUser UpdateUser { get; set; }
        public int UserId => Request.QueryString.GetId();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            try
            {
                User user = GetUserById.Execute(UserId);

                tboxFirstName.Text = user.Person.FirstName;
                tboxLastName.Text = user.Person.LastName;
                tboxAge.Text = user.Person.Age.ToString();
                tboxEmail.Text = user.Email;
                tboxPassword.Text = user.Password;
            }
            catch (AppException ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
            catch (Exception)
            {
                Response.Redirect("./Administrator.aspx");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                IUpdateUser.Request request = new IUpdateUser.Request
                {
                    Id = UserId,
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


                UpdateUser.Execute(request);

                Response.Redirect("./Administrator.aspx");
            }
            catch (AppException ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = ex.Message;
            }
            catch (Exception)
            {
                Response.Redirect("./Administrator.aspx");
            }
        }
    }
}