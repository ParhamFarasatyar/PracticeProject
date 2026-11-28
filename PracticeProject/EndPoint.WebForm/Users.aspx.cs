using System;

namespace EndPoint.WebForm;

public partial class Users : UserBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["UserId"] = null;
        Session["UserRole"] = null;
        Session["IsAuthenticated"] = false;

        Response.Redirect("./Login.aspx");
    }
}