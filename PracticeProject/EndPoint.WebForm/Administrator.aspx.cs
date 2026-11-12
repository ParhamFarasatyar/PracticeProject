using PracticeProject.Application.BaseInfo.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfo.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EndPoint.WebForm
{
    public partial class Administrator : System.Web.UI.Page
    {
        public ISearchUsers SearchUsers { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void BindUsers(string searchString)
        {
            ISearchUsers.SearchUsersResponse response = SearchUsers.Execute(new ISearchUsers.SearchUsersRequest { SearchString = searchString });

            if (!response.IsSuccess)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = response.Message;
                return;
            }

            var firstUser = response.SearchResult.FirstOrDefault();

            gvUsers.DataSource = response.SearchResult;
            gvUsers.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUsers(tboxSearch.Text.Trim());
        }
    }
}