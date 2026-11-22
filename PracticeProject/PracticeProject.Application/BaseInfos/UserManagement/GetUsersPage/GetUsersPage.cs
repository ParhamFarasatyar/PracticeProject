using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using System;
using System.Linq;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetUsersPage
{
    public class GetUsersPage : IGetUsersPage
    {
        private readonly ISearchUsers _searchUsers;

        public GetUsersPage(ISearchUsers searchUsers)
        {
            _searchUsers = searchUsers;
        }

        public IGetUsersPage.Response Respond(IGetUsersPage.Request request)
        {
            if (request is null)
                request = new IGetUsersPage.Request();

            int pageSize = 10;
            int pageNumber = Math.Max(1, request.PageNumber);

            var items = _searchUsers.Respond(request.SearchRequest);

            int totalCount = items.Count();

            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            
            pageNumber = totalPages == 0 ? 1 : Math.Min(pageNumber, totalPages);

            var users = items
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return new IGetUsersPage.Response
            {
                Users = users,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };
        }
    }
}
