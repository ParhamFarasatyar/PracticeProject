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
                <asp:TextBox ID="tboxSearch" runat="server" CssClass="form-control" placeholder="Search for user"></asp:TextBox>

                <asp:LinkButton
                    ID="btnSearch"
                    runat="server"
                    OnClick="btnSearch_Click"
                    CssClass="search-button"
                    ToolTip="Search">
                    <svg xmlns="http://www.w3.org/2000/svg"
                         width="21"
                         height="21"
                         viewBox="0 0 24 24"
                         fill="none"
                         stroke="currentColor"
                         stroke-width="2"
                         stroke-linecap="round"
                         stroke-linejoin="round">
                        <circle cx="11" cy="11" r="8"></circle>
                        <line x1="21" y1="21" x2="16.65" y2="16.65"></line>
                    </svg>
                </asp:LinkButton>

                <asp:Button ID="btnAddUser" runat="server" Text="Add New User" CssClass="btn-outline theme-inverse" OnClick="btnAddUser_Click" />
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
                                CommandName="EditUser"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="action-button edit-button"
                                ToolTip="Edit User">

                                <svg xmlns="http://www.w3.org/2000/svg"
                                     width="18" height="18"
                                     viewBox="0 0 24 24"
                                     fill="none" stroke="currentColor"
                                     stroke-width="2"
                                     stroke-linecap="round"
                                     stroke-linejoin="round">
                                    <path d="M12 20h9"/>
                                    <path d="M16.5 3.5a2.12 2.12 0 0 1 3 3L8 18l-4 1 1-4Z"/>
                                </svg>

                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="btnDelete"
                                runat="server"
                                CommandName="DeleteUser"
                                CommandArgument='<%# Eval("Id") %>'
                                CssClass="action-button delete-button"
                                ToolTip="Delete User"
                                OnClientClick="return confirm('Are you sure you want to delete this user?');">

                                <svg xmlns="http://www.w3.org/2000/svg"
                                     width="18" height="18"
                                     viewBox="0 0 24 24"
                                     fill="none" stroke="currentColor"
                                     stroke-width="2"
                                     stroke-linecap="round"
                                     stroke-linejoin="round">
                                    <polyline points="3 6 5 6 21 6"/>
                                    <path d="M19 6l-1 14H6L5 6"/>
                                    <path d="M10 11v6"/>
                                    <path d="M14 11v6"/>
                                    <path d="M9 6V4h6v2"/>
                                </svg>

                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:Label ID="lblMessage" runat="server" CssClass="message-label"></asp:Label>
        </div>
    </form>
</body>
</html>
