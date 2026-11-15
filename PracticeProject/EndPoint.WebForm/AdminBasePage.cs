using PracticeProject.Domain.BaseInfo.Users;
using System;
using System.Web.UI;

namespace EndPoint.WebForm
{
    public class AdminBasePage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Session["UserId"] is null || Session["UserRole"] is null)
            {
                Response.Redirect("./Login.aspx");
                return;
            }
            
            if ((Role)Session["UserRole"] != Role.Admin)
            {
                Response.Redirect("./AccessDenied.aspx");
                return;
            }
        }
    }
}