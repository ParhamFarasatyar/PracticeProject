using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.GetUserById
{
    public interface IGetUserById
    {
        Response Execute(Request request);

        public class Request
        {
            public int Id { get; set; }
        }

        public class Response
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
        }
    }
}
