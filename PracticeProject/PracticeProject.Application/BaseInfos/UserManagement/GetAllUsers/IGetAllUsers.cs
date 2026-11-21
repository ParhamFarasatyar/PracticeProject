using PracticeProject.Domain.BaseInfos.Users;
using System.Linq;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetAllUsers
{
    public interface IGetAllUsers
    {
        Response Execute();

        public class Response
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public IQueryable<User> Users { get; set; }
        }
    }
}
