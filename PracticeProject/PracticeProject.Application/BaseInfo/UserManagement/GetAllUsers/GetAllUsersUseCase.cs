using PracticeProject.Application.BaseInfo.Interfaces;
using PracticeProject.Domain.BaseInfo.User;
using System.Collections.Generic;

namespace PracticeProject.Application.BaseInfo.UserManagement.GetAllUsers
{
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private readonly IUserDataAccess _userDataAccess;

        public GetAllUsersUseCase(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public GetAllUsersResponse Execute()
        {
            GetAllUsersResponse response = new GetAllUsersResponse();

            List<User> users = _userDataAccess.GetAll();

            if (users.Count == 0)
            {
                response.IsSuccess = true;
                response.Message = "No users found.";
                return response;
            }

            response.IsSuccess = true;
            response.Users = users;
            return response;
        }
    }
}
