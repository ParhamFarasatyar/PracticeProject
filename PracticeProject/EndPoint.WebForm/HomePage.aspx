<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="EndPoint.WebForm.HomePage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="fa" dir="rtl">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>PracticeProject</title>
    <link href="./Assets/Styles/HomePageStyle.css" rel="stylesheet" />
</head>

<body>
    <form id="homeForm" runat="server">
        <nav class="navbar">
            <a href="HomePage.aspx" class="logo">
                <div class="logo-icon">P</div>
                <span>PracticeProject</span>
            </a>
            <div class="nav-links">
                <a href="Login.aspx">Login</a>
                <a href="Register.aspx" class="nav-register">Register</a>
            </div>
        </nav>
        <section class="hero">
            <div class="badge">
                ✦ &nbsp; Welcome to PracticeProject
            </div>
            <h1>A Project
                <span>For CRUD Operation</span>
            </h1>
            <p>
                Welcome to Practice Project!
                An environment for managing users with admin accessibility
            </p>
        </section>

        <section class="auth-container">
            <div class="auth-card">
                <div class="card-icon login-icon">
                    ↗
                </div>
                <h2>Welcome Back!</h2>
                <p>
                    If you have already created an account, log in to access your user interface.
                </p>
                <a href="Login.aspx" class="action-button login-button">Log In
                </a>
            </div>

            <div class="auth-card">
                <div class="card-icon register-icon">
                    +
                </div>

                <h2>Join Us!</h2>

                <p>
                    Don't have an account yet? Sign up now and start your journey.
                </p>
                <a href="Register.aspx" class="action-button register-button">Register New Account
                </a>
            </div>
        </section>

    </form>
</body>
</html>
