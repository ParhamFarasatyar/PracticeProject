using PracticeProject.Domain.BaseInfos.Users;
using System.Collections.Generic;
using System.Linq;
using static PracticeProject.Application.BaseInfos.IUserDataAccess;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetAllUsers
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly IUserDataAccess _userDataAccess;

        public GetAllUsers(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        public IGetAllUsers.Response Execute()
        {
            IGetAllUsers.Response response = new IGetAllUsers.Response();

            IQueryable<SearchResult> result = _userDataAccess.GetAll();

            if (!result.Any())
            {
                response.IsSuccess = true;
                response.Message = "No users found.";
                return response;
            }

            response.IsSuccess = true;
            response.Result = result;
            return response;
        }
    }
}
