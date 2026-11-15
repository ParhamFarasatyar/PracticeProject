<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EndPoint.WebForm.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PracticeProject - Login</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/css/bootstrap.min.css"
        rel="stylesheet" />

    <link href="./Assets/Styles/LoginStyle.css" rel="stylesheet" />
</head>

<body>
    <main class="login-container">
        <section class="login-card">

            <div class="login-icon">
                <svg viewBox="0 0 24 24">
                    <path d="M15 3h4a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2h-4" />
                    <path d="M10 17l5-5-5-5" />
                    <path d="M15 12H3" />
                </svg>
            </div>

            <span class="login-label">WELCOME BACK</span>

            <h1 id="headerText">Login</h1>

            <p class="login-description">
                Sign in to your account to continue.
           
            </p>

            <div class="login-divider"></div>

            <form id="loginForm" runat="server">

                <div class="form-fields">

                    <div class="form-group">
                        <asp:TextBox
                            ID="tboxEmail"
                            runat="server"
                            placeholder="Email"
                            TextMode="Email"
                            CssClass="form-control">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator
                            ID="rfvEmail"
                            runat="server"
                            ErrorMessage="Email required"
                            ForeColor="#f87171"
                            ControlToValidate="tboxEmail"
                            Display="Dynamic"
                            CssClass="validation-message">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator
                            ID="revEmail"
                            runat="server"
                            ErrorMessage="Your email should look like this: abc123@example.com"
                            ForeColor="#f87171"
                            ControlToValidate="tboxEmail"
                            ValidationExpression="^[a-zA-Z0-9._%+-]+@example\.com$"
                            Display="Dynamic"
                            CssClass="validation-message">
                        </asp:RegularExpressionValidator>
                    </div>

                    <div class="form-group">
                        <div class="password-field">
                            <asp:TextBox
                                ID="tboxPassword"
                                runat="server"
                                placeholder="Password"
                                CssClass="form-control"
                                TextMode="Password">
                            </asp:TextBox>

                            <button type="button"
                                class="password-toggle"
                                onclick="togglePassword(this)"
                                aria-label="Show password">

                                <svg class="eye-icon" viewBox="0 0 24 24">
                                    <path d="M2 12s3.6-7 10-7 10 7 10 7-3.6 7-10 7-10-7-10-7Z" />
                                    <circle cx="12" cy="12" r="3" />
                                </svg>

                                <svg class="eye-off-icon" viewBox="0 0 24 24">
                                    <path d="m3 3 18 18" />
                                    <path d="M10.6 10.6a2 2 0 0 0 2.8 2.8" />
                                    <path d="M9.9 5.2A11 11 0 0 1 12 5c6.4 0 10 7 10 7a16 16 0 0 1-3.1 3.8" />
                                    <path d="M6.2 6.2C3.5 8 2 12 2 12s3.6 7 10 7a10 10 0 0 0 4-.8" />
                                </svg>

                            </button>
                        </div>

                        <asp:RequiredFieldValidator
                            ID="rfvPassword"
                            runat="server"
                            ErrorMessage="Password required"
                            ForeColor="#f87171"
                            ControlToValidate="tboxPassword"
                            Display="Dynamic"
                            CssClass="validation-message">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator
                            ID="revPassword"
                            runat="server"
                            ErrorMessage="Password must contain lowercase, uppercase, a digit, one of @ # $, and at least 8 characters."
                            ForeColor="#f87171"
                            ControlToValidate="tboxPassword"
                            Display="Dynamic"
                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$]).{8,}$"
                            CssClass="validation-message">
                        </asp:RegularExpressionValidator>
                    </div>

                    <asp:Button
                        ID="btnSubmit"
                        runat="server"
                        Text="Sign In"
                        CssClass="login-button"
                        OnClick="btnSubmit_Click" />

                    <div class="register-link">
                        <span>Don't have an account?</span>
                        <a href="./Register.aspx">Register</a>
                    </div>

                    <asp:Label
                        ID="lblMessage"
                        runat="server"
                        CssClass="server-message">
                    </asp:Label>

                </div>
            </form>

            <p class="footer-text">PracticeProject © 2026</p>

        </section>
    </main>

    <script>
        function togglePassword(button) {
            const wrapper = button.closest(".password-field");
            const input = wrapper.querySelector("input");

            const isPassword = input.type === "password";

            input.type = isPassword ? "text" : "password";

            button.classList.toggle("show-password", isPassword);

            button.setAttribute(
                "aria-label",
                isPassword ? "Hide password" : "Show password"
            );
        }
    </script>
</body>
</html>
