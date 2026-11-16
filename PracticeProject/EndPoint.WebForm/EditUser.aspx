<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="EndPoint.WebForm.EditUser" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>PracticeProject - Edit User</title>
    <link href="./Assets/Styles/EditUserStyle.css" rel="stylesheet" />
</head>

<body>
    <form id="editForm" runat="server">

        <main class="page-container">

            <div class="back-container">
                <asp:HyperLink
                    ID="hlBack"
                    runat="server"
                    NavigateUrl="~/Administrator.aspx"
                    CssClass="back-button">
                    <span class="back-icon">&#8592;</span>
                    Back to Admin
                </asp:HyperLink>
            </div>

            <section class="form-card">

                <div class="form-header">
                    <div class="header-icon">
                        <svg viewBox="0 0 24 24">
                            <path d="M12 20h9" />
                            <path d="M16.5 3.5a2.12 2.12 0 0 1 3 3L8 18l-4 1 1-4Z" />
                        </svg>
                    </div>

                    <h1>Edit User</h1>
                    <p>Update user information and account details</p>
                </div>

                <div class="form-content">

                    <div class="section-heading">
                        <span class="section-number">01</span>
                        <h2>Personal Information</h2>
                    </div>

                    <div class="form-grid">

                        <div class="form-group">
                            <label for="<%= tboxFirstName.ClientID %>">First Name</label>
                            <asp:TextBox ID="tboxFirstName" runat="server"
                                placeholder="Enter first name" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <label for="<%= tboxLastName.ClientID %>">Last Name</label>
                            <asp:TextBox ID="tboxLastName" runat="server"
                                placeholder="Enter last name" CssClass="form-control" />
                        </div>

                        <div class="form-group">
                            <label for="<%= tboxAge.ClientID %>">Age</label>
                            <asp:TextBox ID="tboxAge" runat="server"
                                TextMode="Number" placeholder="Enter age"
                                CssClass="form-control" />
                            <asp:RangeValidator
                                ID="rvAge" runat="server"
                                ErrorMessage="Age should be between 18 to 80"
                                ForeColor="Red"
                                ControlToValidate="tboxAge"
                                Display="Dynamic"
                                Type="Integer"
                                MinimumValue="18"
                                MaximumValue="80"
                                CssClass="validation-message" />
                        </div>

                        <div class="form-group">
                            <label for="<%= ddlGender.ClientID %>">Gender</label>
                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Gender" Value="" />
                                <asp:ListItem Text="Male" Value="0" />
                                <asp:ListItem Text="Female" Value="1" />
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="section-heading account-heading">
                        <span class="section-number">02</span>
                        <h2>Account Information</h2>
                    </div>

                    <div class="form-grid">

                        <div class="form-group">
                            <label for="<%= ddlRole.ClientID %>">Role</label>
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Role" Value="" />
                                <asp:ListItem Text="Admin" Value="0" />
                                <asp:ListItem Text="User" Value="1" />
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label for="<%= tboxEmail.ClientID %>">Email Address</label>
                            <asp:TextBox ID="tboxEmail" runat="server"
                                TextMode="Email" placeholder="name@example.com"
                                CssClass="form-control" />
                            <asp:RegularExpressionValidator
                                ID="revEmail" runat="server"
                                ErrorMessage="Your email should look like this: abc123@example.com"
                                ForeColor="Red"
                                ControlToValidate="tboxEmail"
                                ValidationExpression="^[a-zA-Z0-9._%+-]+@example\.com$"
                                Display="Dynamic"
                                CssClass="validation-message" />
                        </div>

                        <div class="form-group">
                            <label for="<%= tboxPassword.ClientID %>">New Password</label>
                            <div class="password-field">
                                <asp:TextBox ID="tboxPassword" runat="server"
                                    placeholder="Enter new password"
                                    CssClass="form-control"
                                    TextMode="Password" />
                                <button type="button" class="password-toggle"
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
                            <asp:RegularExpressionValidator
                                ID="revPassword" runat="server"
                                ErrorMessage="Password must contain lowercase, uppercase, a digit, one of @ # $, and at least 8 characters."
                                ForeColor="Red"
                                ControlToValidate="tboxPassword"
                                Display="Dynamic"
                                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$]).{8,}$"
                                CssClass="validation-message" />
                        </div>

                        <div class="form-group">
                            <label for="<%= tboxConfirmPassword.ClientID %>">Confirm Password</label>
                            <div class="password-field">
                                <asp:TextBox ID="tboxConfirmPassword" runat="server"
                                    placeholder="Confirm new password"
                                    CssClass="form-control"
                                    TextMode="Password" />
                                <button type="button" class="password-toggle"
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
                            <asp:CompareValidator
                                ID="cvConfirmPassword" runat="server"
                                ErrorMessage="Password doesn't match"
                                ControlToCompare="tboxPassword"
                                ControlToValidate="tboxConfirmPassword"
                                Display="Dynamic"
                                ForeColor="Red"
                                CssClass="validation-message" />
                        </div>
                    </div>

                    <div class="form-footer">
                        <asp:Label ID="lblMessage" runat="server" CssClass="form-message" />

                        <asp:Button
                            ID="btnSubmit"
                            runat="server"
                            Text="Save Changes"
                            CssClass="btn-submit"
                            OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </section>
        </main>
    </form>
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
