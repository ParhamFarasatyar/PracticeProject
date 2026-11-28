using System;
using System.Drawing;
using PracticeProject.Application.BaseInfos.UserManagement.CreateUser;
using PracticeProject.Domain.BaseInfo.Exceptions;
using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;

namespace EndPoint.WebForm;

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

        var request = new ICreateUser.Request
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
            CreateUser.Execute(request);

            Response.Redirect("./Administrator.aspx");
        }
        catch (AppException ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }
}