<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="EndPoint.WebForm.Users" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>PracticeProject - Users</title>
    <link href="./Assets/Styles/UsersStyle.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">

        <main class="page-container">

            <header class="page-header">
                <div class="header-icon">
                    <svg viewBox="0 0 24 24">
                        <path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2" />
                        <circle cx="9" cy="7" r="4" />
                        <path d="M22 21v-2a4 4 0 0 0-3-3.87" />
                        <path d="M16 3.13a4 4 0 0 1 0 7.75" />
                    </svg>
                </div>

                <h1>Users Page</h1>
                <p>Welcome to your personal workspace</p>
            </header>

            <section class="content-card">

                <div class="video-container">

                    <div class="video-heading">
                        <div>
                            <h2>Welcome!</h2>
                            <p>We're glad to have you here.</p>
                        </div>

                        <span class="status-indicator">
                            <span class="status-dot"></span>
                            Online
                        </span>
                    </div>

                    <div class="video-frame">
                        <video class="welcome-video" autoplay loop playsinline>
                            <source src="./Assets/Videos/usersPageVideo.mp4" type="video/mp4" />
                        </video>
                    </div>

                    <div class="video-caption">
                        <span class="caption-line"></span>
                        <span>PracticeProject</span>
                        <span class="caption-line"></span>
                    </div>
                </div>

                <div class="footer-section">
                    <div class="footer-text">
                        <span class="footer-title">Your workspace</span>
                        <span class="footer-description">Have a productive day!</span>
                    </div>

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
            </section>

            <footer class="page-footer">
                <span>PracticeProject</span>
                <span>Built for learning and growth</span>
            </footer>
        </main>
    </form>
</body>
</html>
