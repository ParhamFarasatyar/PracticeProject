using System;
using System.Web.UI;
using PracticeProject.Domain.BaseInfos.Users;

namespace EndPoint.WebForm;

public class UserBasePage : Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (Session["UserId"] is null || Session["UserRole"] is null)
        {
            Response.Redirect("./Login.aspx");
            return;
        }

        if ((Role)Session["UserRole"] != Role.User) Response.Redirect("./AccessDenied.aspx");
    }
}