<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="EndPoint.WebForm.AddUser" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>PracticeProject - Add User</title>
    <link href="./Assets/Styles/AddUserStyle.css" rel="stylesheet" />
</head>
<body>

    <header class="page-header">
        <div class="header-icon">
            <svg viewBox="0 0 24 24">
                <path d="M16 21v-2a4 4 0 0 0-4-4H6a4 4 0 0 0-4 4v2" />
                <circle cx="9" cy="7" r="4" />
                <line x1="19" y1="8" x2="19" y2="14" />
                <line x1="22" y1="11" x2="16" y2="11" />
            </svg>
        </div>
        <h1>Add New User</h1>
        <p>Create a new account in your system</p>
    </header>

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

    <form id="addForm" runat="server">

        <div class="form-card">

            <div class="form-section">
                <div class="section-heading">
                    <span class="section-number">01</span>
                    <div>
                        <h2>Personal Information</h2>
                        <p>Enter the user's personal details</p>
                    </div>
                </div>

                <div class="form-grid">

                    <div class="form-group">
                        <label for="<%= tboxFirstName.ClientID %>">First Name</label>
                        <asp:TextBox ID="tboxFirstName" runat="server"
                            placeholder="Enter first name"
                            CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                            ErrorMessage="First name is required"
                            ForeColor="Red"
                            ControlToValidate="tboxFirstName"
                            Display="Dynamic"
                            CssClass="validation-message" />
                    </div>

                    <div class="form-group">
                        <label for="<%= tboxLastName.ClientID %>">Last Name</label>
                        <asp:TextBox ID="tboxLastName" runat="server"
                            placeholder="Enter last name"
                            CssClass="form-control" />
                    </div>

                    <div class="form-group">
                        <label for="<%= tboxAge.ClientID %>">Age</label>
                        <asp:TextBox ID="tboxAge" TextMode="Number" runat="server"
                            placeholder="Enter age"
                            CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvAge" runat="server"
                            ErrorMessage="Age is required"
                            ForeColor="Red"
                            ControlToValidate="tboxAge"
                            Display="Dynamic"
                            CssClass="validation-message" />
                        <asp:RangeValidator ID="rvAge" runat="server"
                            ErrorMessage="Age must be between 18 and 80"
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
                        <asp:DropDownList ID="ddlGender" runat="server"
                            CssClass="form-control">
                            <asp:ListItem Text="Select Gender" Value="" />
                            <asp:ListItem Text="Male" Value="0" />
                            <asp:ListItem Text="Female" Value="1" />
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="rfvGender" runat="server"
                            ControlToValidate="ddlGender"
                            InitialValue=""
                            ErrorMessage="Please select your gender"
                            ForeColor="Red"
                            Display="Dynamic"
                            CssClass="validation-message" />
                    </div>

                </div>
            </div>

            <div class="section-divider"></div>

            <div class="form-section">
                <div class="section-heading">
                    <span class="section-number">02</span>
                    <div>
                        <h2>Account Information</h2>
                        <p>Set up login credentials for this user</p>
                    </div>
                </div>

                <div class="form-group full-width">
                    <label for="<%= tboxEmail.ClientID %>">Email Address</label>
                    <asp:TextBox ID="tboxEmail" runat="server"
                        placeholder="example@example.com"
                        CssClass="form-control" />

                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ErrorMessage="Email is required"
                        ForeColor="Red"
                        ControlToValidate="tboxEmail"
                        Display="Dynamic"
                        CssClass="validation-message" />

                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ErrorMessage="Enter a valid email address"
                        ForeColor="Red"
                        ControlToValidate="tboxEmail"
                        ValidationExpression="^[a-zA-Z0-9._%+-]+@example\.com$"
                        Display="Dynamic"
                        CssClass="validation-message" />
                </div>

                <div class="form-grid">

                    <div class="form-group">
                        <label for="<%= tboxPassword.ClientID %>">Password</label>

                        <div class="password-field">
                            <asp:TextBox ID="tboxPassword" runat="server"
                                placeholder="Create password"
                                CssClass="form-control"
                                TextMode="Password" />

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

                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                            ErrorMessage="Password is required"
                            ForeColor="Red"
                            ControlToValidate="tboxPassword"
                            Display="Dynamic"
                            CssClass="validation-message" />

                        <asp:RegularExpressionValidator ID="revPassword" runat="server"
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
                                placeholder="Repeat password"
                                CssClass="form-control"
                                TextMode="Password" />

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

                        <asp:CompareValidator ID="cvConfirmPassword" runat="server"
                            ErrorMessage="Passwords do not match"
                            ControlToCompare="tboxPassword"
                            ControlToValidate="tboxConfirmPassword"
                            Display="Dynamic"
                            ForeColor="Red"
                            CssClass="validation-message" />

                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server"
                            ErrorMessage="Confirm password is required"
                            ForeColor="Red"
                            ControlToValidate="tboxConfirmPassword"
                            Display="Dynamic"
                            CssClass="validation-message" />
                    </div>

                </div>
            </div>

            <div class="form-footer">
                <asp:Label ID="lblMessage" runat="server" CssClass="form-message" />

                <asp:Button ID="btnSubmit" runat="server"
                    Text="Create User"
                    CssClass="submit-button"
                    OnClick="btnSubmit_Click" />
            </div>

        </div>

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
