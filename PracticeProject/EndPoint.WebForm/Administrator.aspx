<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="EndPoint.WebForm.Administrator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PracticeProgect - Admin</title>
    <link href="./Assets/Styles/AdministratorStyle.css" rel="stylesheet" />
</head>
<body>
    <header>
        <h1>Admin Page</h1>
    </header>
    <form id="adminForm" runat="server">
        <div>
            <div id="searchBox" class="d-flex flex-row mb-3">
                <asp:TextBox ID="tboxSearchEmail" runat="server" CssClass="form-control" placeholder="Search for user by email" MaxLength="100"></asp:TextBox>
                <asp:TextBox ID="tboxSearchFirstNameOrLastName" runat="server" CssClass="form-control" placeholder="Search for user by firstname or lastname" MaxLength="100"></asp:TextBox>

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

                <asp:Button
                    ID="btnAddUser"
                    runat="server"
                    Text="Add New User"
                    CssClass="add-user-button"
                    OnClick="btnAddUser_Click" />

                <asp:LinkButton
                    ID="btnLogout"
                    runat="server"
                    CssClass="logout-button"
                    ToolTip="Log Out" OnClick="btnLogout_Click">

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

            <asp:GridView OnRowCommand="gvUsers_RowCommand" CssClass="table table-striped" ID="gvUsers" runat="server" AutoGenerateColumns="false" EmptyDataText="No users found">
                <Columns>
                    <asp:TemplateField HeaderText="FirstName">
                        <ItemTemplate>
                            <%# Eval("FirstName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LastName">
                        <ItemTemplate>
                            <%# Eval("LastName") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Age">
                        <ItemTemplate>
                            <%# Eval("Age") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <%# Eval("Gender") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <%# Eval("Email") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Role">
                        <ItemTemplate>
                            <%# Eval("Role") %>
                        </ItemTemplate>
                    </asp:TemplateField>

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

            <div id="pagination">
                <asp:LinkButton 
                    ID="btnPrevious" 
                    runat="server" 
                    CssClass="pagination-button" 
                    OnClick="btnPrevious_Click"
                    Text="Previous"></asp:LinkButton>

                <asp:Label
                    ID="lblCurrentPage"
                    runat="server"
                    CssClass="page-info">
                </asp:Label>

                <asp:LinkButton
                    ID="btnNext"
                    runat="server"
                    CssClass="pagination-button"
                    OnClick="btnNext_Click"
                    Text="Next">
                </asp:LinkButton>
            </div>

            <asp:Label ID="lblMessage" runat="server" CssClass="message-label"></asp:Label>
        </div>
    </form>
</body>
</html>
