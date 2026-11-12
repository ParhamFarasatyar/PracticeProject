using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Application.BaseInfo.UserManagement.SearchUsers
{
    public class SearchUsers : ISearchUsers
    {
        private readonly IUserDataAccess _userDataAccess;

        public SearchUsers(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public ISearchUsers.SearchUsersResponse Execute(ISearchUsers.SearchUsersRequest request)
        {
            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            List<User> users = new List<User>();

            if (string.IsNullOrWhiteSpace(request.SearchString))
                users = _userDataAccess.GetAll();
            else 
                users = _userDataAccess.SearchUsers(request.SearchString);

            if (!users.Any())
                return new ISearchUsers.SearchUsersResponse
                {
                    IsSuccess = true,
                    Message = "No users found.",
                    SearchResult = new List<User>()
                };

            return new ISearchUsers.SearchUsersResponse
            {
                IsSuccess = true,
                Message = "Users found.",
                SearchResult = users
            };
        }

        private ISearchUsers.SearchUsersResponse CreateFailureResponse(string message)
        {
            return new ISearchUsers.SearchUsersResponse
            {
                IsSuccess = false,
                Message = message,
                SearchResult = null
            };
        }
    }
}
