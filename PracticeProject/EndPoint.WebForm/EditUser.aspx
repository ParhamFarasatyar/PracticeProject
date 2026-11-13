<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="EndPoint.WebForm.EditUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PracticeProject - Edit User</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-B/GM4XqrwHnWXNOWMbloTmrYXZg10cakYGmpfsR/bbzQ6JAJI4ihuyADKLnBgrCe" crossorigin="anonymous"/>
    <script type="module" src="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-1a/pXj49ZQ1aHEmrJ+gMw1otqoVsYwlEnlD8mIfY2TV03r20Y0CN7uqx1tQogjPL" crossorigin="anonymous"></script>
    <link href="./Assets/Styles/EditUserStyle.css" rel="stylesheet" />
</head>
<body>
    <header>
        <h1>
            Edit User
        </h1>
    </header>
    <form id="editForm" runat="server">
        <div class="d-flex flex-column mb-3 row-gap-5">
            <asp:TextBox ID="tboxFirstName" runat="server" placeholder="FirstName" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvFirstName" 
                runat="server" 
                ErrorMessage="First name required" 
                ForeColor="Red" 
                ControlToValidate="tboxFirstName" 
                Display="Dynamic">
            </asp:RequiredFieldValidator>

            <asp:TextBox ID="tboxLastName" runat="server" placeholder="LastName" CssClass="form-control"></asp:TextBox>

            <asp:TextBox ID="tboxAge" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvAge" 
                runat="server" 
                ErrorMessage="Age required" 
                ForeColor="Red" 
                ControlToValidate="tboxAge" 
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RangeValidator 
                ID="rvAge" 
                runat="server" 
                ErrorMessage="Age should between 18 to 80" 
                ForeColor="Red" ControlToValidate="tboxAge" 
                Display="Dynamic" 
                Type="Integer" 
                MinimumValue="18" 
                MaximumValue="80">
            </asp:RangeValidator>

             <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                 <asp:ListItem Text="Select Gender" Value="" />
                 <asp:ListItem Text="Male" Value="0" />
                 <asp:ListItem Text="Female" Value="1" />
             </asp:DropDownList>
            <asp:RequiredFieldValidator
                ID="rfvGender"
                runat="server"
                ControlToValidate="ddlGender"
                InitialValue=""
                ErrorMessage="Please select a gender"
                ForeColor="Red"
                Display="Dynamic" />

             <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                 <asp:ListItem Text="Select Role" Value="" />
                 <asp:ListItem Text="Admin" Value="0" />
                 <asp:ListItem Text="User" Value="1" />
             </asp:DropDownList>
            <asp:RequiredFieldValidator
                ID="rfvRole"
                runat="server"
                ControlToValidate="ddlRole"
                InitialValue=""
                ErrorMessage="Please select a role"
                ForeColor="Red"
                Display="Dynamic" />

            <asp:TextBox ID="tboxEmail" runat="server" TextMode="Email" placeholder="Email" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvEmail" 
                runat="server" 
                ErrorMessage="Email required" 
                ForeColor="Red" 
                ControlToValidate="tboxEmail" 
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator 
                ID="revEmail" 
                runat="server" 
                ErrorMessage="Your email should like this: abc123@example.com" 
                ForeColor="Red" 
                ControlToValidate="tboxEmail" 
                ValidationExpression="^[a-zA-Z0-9._%+-]+@example\.com$" 
                Display="Dynamic" 
                TextMode="Email">
            </asp:RegularExpressionValidator>

            <asp:TextBox ID="tboxPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator 
                ID="rfvPassword" 
                runat="server" 
                ErrorMessage="Password required" 
                ForeColor="Red" ControlToValidate="tboxPassword"
                Display="Dynamic">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator 
                ID="revPassword" 
                runat="server" 
                ErrorMessage="Your password should like this: <br/> * Should contain lower case letters <br/> * Should contain upper case letters <br/> * Should contain digits <br/> * Should contain at least one of these @, #, $ <br/> * Should be at least 8 characters" 
                ForeColor="Red" 
                ControlToValidate="tboxPassword" 
                Display="Dynamic" 
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$]).{8,}$">
            </asp:RegularExpressionValidator>

            <asp:TextBox ID="tboxConfirmPassword" runat="server" placeholder="Confirm Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator 
                ID="cvConfirmPassword" 
                runat="server" 
                ErrorMessage="Password doesn't match" 
                ControlToCompare="tboxPassword" 
                ControlToValidate="tboxConfirmPassword" 
                Display="Dynamic" 
                ForeColor="Red">
            </asp:CompareValidator>

            <asp:Button 
                ID="btnSubmit" 
                runat="server" 
                Text="Submit" 
                CssClass="btn-subtle theme-primary" 
                OnClick="btnSubmit_Click"/>
        </div>
    </form>
</body>
</html>
