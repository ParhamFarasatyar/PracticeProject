<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="EndPoint.WebForm.Administrator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PracticeProgect - Admin</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-B/GM4XqrwHnWXNOWMbloTmrYXZg10cakYGmpfsR/bbzQ6JAJI4ihuyADKLnBgrCe" crossorigin="anonymous"/>
    <script type="module" src="https://cdn.jsdelivr.net/npm/bootstrap@6.0.0-alpha.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-1a/pXj49ZQ1aHEmrJ+gMw1otqoVsYwlEnlD8mIfY2TV03r20Y0CN7uqx1tQogjPL" crossorigin="anonymous"></script>
    <link href="./Assets/Styles/AdministratorStyle.css" rel="stylesheet" />
</head>
<body>
    <header>
        <h1>Admin Page</h1>
    </header>
    <form id="adminForm" runat="server">
        <div>
            <div id="searchBox" class="d-flex flex-row mb-3">
                <asp:TextBox ID="tboxSearch" runat="server" CssClass="form-control"></asp:TextBox>

                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-subtle theme-primary" />

                <asp:Button ID="btnAddUser" runat="server" Text="Add New" CssClass="btn-outline theme-inverse" />
            </div>

            <asp:GridView OnRowCommand="gvUsers_RowCommand" CssClass="table table-striped" ID="gvUsers" runat="server" AutoGenerateColumns="false" EmptyDataText="No users found">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />

                    <asp:TemplateField HeaderText="FirstName">
                        <ItemTemplate>
                            <%# Eval("Person.FirstName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LastName">
                        <ItemTemplate>
                            <%# Eval("Person.LastName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Age">
                        <ItemTemplate>
                            <%# Eval("Person.Age") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <%# Eval("Person.Gender") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Email" HeaderText="Email" />

                    <asp:BoundField DataField="Role" HeaderText="Role" />

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="btnEdit"
                                runat="server"
                                Text="Edit"
                                CommandName="EditUser"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn-outline theme-info">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="btnDelete"
                                runat="server"
                                Text="Delete"
                                CommandName="DeleteUser"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="btn-outline theme-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this user?');">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
