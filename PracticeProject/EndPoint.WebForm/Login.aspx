<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EndPoint.WebForm.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PracticeProject - Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-B/GM4XqrwHnWXNOWMbloTmrYXZg10cakYGmpfsR/bbzQ6JAJI4ihuyADKLnBgrCe" crossorigin="anonymous"/>
    <script type="module" src="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-1a/pXj49ZQ1aHEmrJ+gMw1otqoVsYwlEnlD8mIfY2TV03r20Y0CN7uqx1tQogjPL" crossorigin="anonymous"></script>
    <link href="./Assets/Styles/LoginStyle.css" rel="stylesheet" />
</head>
<body>
    <header>
        <h1 id="headerText">Login</h1>
    </header>
    <form id="loginForm" runat="server">
        <div class="d-flex flex-column mb-3 row-gap-8">
            <asp:TextBox ID="tboxEmail" runat="server" placeholder="Email" TextMode="Email" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email required" ForeColor="Red" ControlToValidate="tboxEmail" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Your email should like this: abc123@example.com" ForeColor="Red" ControlToValidate="tboxEmail" ValidationExpression="^[a-zA-Z0-9._%+-]+@example\.com$" Display="Dynamic" TextMode="Email"></asp:RegularExpressionValidator>

            <asp:TextBox ID="tboxPassword" runat="server" placeholder="Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Password required" ForeColor="Red" ControlToValidate="tboxPassword" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator 
                ID="revPassword" 
                runat="server" 
                ErrorMessage="Your password should like this: <br/> * Should contain lower case letters <br/> * Should contain upper case letters <br/> * Should contain digits <br/> * Should contain at least one of these @, #, $ <br/> * Should be at least 8 characters" 
                ForeColor="Red" 
                ControlToValidate="tboxPassword" 
                Display="Dynamic" 
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$]).{8,}$">
            </asp:RegularExpressionValidator>

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn-subtle theme-primary" OnClick="btnSubmit_Click"/>

            <div>
                <span>
                    Don't have account?
                </span>
                <a href="./Register.aspx">Register</a>
            </div>

            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
