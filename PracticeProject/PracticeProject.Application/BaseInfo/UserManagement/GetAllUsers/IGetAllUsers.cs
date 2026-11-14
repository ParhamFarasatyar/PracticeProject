using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Application.BaseInfo.UserManagement.GetAllUsers
{
    public interface IGetAllUsers
    {
        GetAllUsersResponse Execute();

        public class GetAllUsersResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public IQueryable<User> Users { get; set; }
        }
    }
}
