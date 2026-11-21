using PracticeProject.Domain.BaseInfos.Users;
using System.Linq;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetAllUsers
{
    public class GetAllUsers(IUserDataAccess userDataAccess) : IGetAllUsers
    {
        public IGetAllUsers.Response Execute()
        {
            IGetAllUsers.Response response = new IGetAllUsers.Response();

            IQueryable<User> result = userDataAccess.GetAll();

            if (!result.Any())
            {
                response.IsSuccess = true;
                response.Message = "No users found.";
                return response;
            }

            response.IsSuccess = true;
            response.Users = result;
            return response;
        }
    }
}
