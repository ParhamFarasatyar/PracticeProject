<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="EndPoint.WebForm.Users" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Practice Project - Admin</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-B/GM4XqrwHnWXNOWMbloTmrYXZg10cakYGmpfsR/bbzQ6JAJI4ihuyADKLnBgrCe" crossorigin="anonymous"/>
    <script type="module" src="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-1a/pXj49ZQ1aHEmrJ+gMw1otqoVsYwlEnlD8mIfY2TV03r20Y0CN7uqx1tQogjPL" crossorigin="anonymous"></script>
    <link href="./Assets/Styles/UsersStyle.css" rel="stylesheet" />
</head>
<body>
    <header>
        <h1>Users Page</h1>
    </header>
    <form id="form1" runat="server">
        <div class="video-container">
            <video class="welcome-video" autoplay loop playsinline>
                <source src="./Assets/Videos/usersPageVideo.mp4" type="video/mp4" />
            </video>

            <asp:LinkButton
                ID="btnLogout"
                runat="server"
                CssClass="logout-button"
                ToolTip="Log Out"
                OnClick="btnLogout_Click">

                <svg xmlns="http://www.w3.org/2000/svg"
                     width="18"
                     height="18"
                     viewBox="0 0 24 24"
                     fill="none"
                     stroke="currentColor"
                     stroke-width="2"
                     stroke-linecap="round"
                     stroke-linejoin="round">
                    <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/>
                    <polyline points="16 17 21 12 16 7"/>
                    <line x1="21" y1="12" x2="9" y2="12"/>
                </svg>

                <span>Log Out</span>
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
