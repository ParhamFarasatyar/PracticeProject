<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="EndPoint.WebForm.AccessDenied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="./Assets/Styles/AccessDeniedStyle.css" rel="stylesheet" />
    <title>PracticeProject - Access Denied</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-B/GM4XqrwHnWXNOWMbloTmrYXZg10cakYGmpfsR/bbzQ6JAJI4ihuyADKLnBgrCe" crossorigin="anonymous" />
    <script type="module" src="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-1a/pXj49ZQ1aHEmrJ+gMw1otqoVsYwlEnlD8mIfY2TV03r20Y0CN7uqx1tQogjPL" crossorigin="anonymous"></script>
</head>

<body>
    <form id="form1" runat="server">
        <main class="error-container">
            <section class="error-card">

                <div class="error-icon">
                    <span>403</span>
                </div>

                <span class="error-label">ACCESS RESTRICTED</span>

                <h1>Access Denied!</h1>

                <p class="error-description">
                    Sorry, you don't have permission to access this page.
                    Please contact your administrator if you believe
                    this is a mistake.
               
                </p>

                <div class="error-divider"></div>

                <p class="error-code">
                    ERROR CODE: <strong>403 FORBIDDEN</strong>
                </p>

                <a onclick="history.back(); return false;" href="#" class="home-button">
                    <span>Return</span>
                    <span class="arrow">→</span>
                </a>

                <p class="footer-text">
                    PracticeProject &copy; 2026
               
                </p>

            </section>
        </main>
    </form>
</body>
</html>
