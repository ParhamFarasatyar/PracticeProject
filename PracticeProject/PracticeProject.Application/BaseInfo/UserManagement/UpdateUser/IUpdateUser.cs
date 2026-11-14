using PracticeProject.Domain.BaseInfo.People;
using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.UpdateUser
{
    public interface IUpdateUser
    {
        Response Execute(Request request);

        public class Request
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public Gender Gender { get; set; }
            public Role Role { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User ModifiedUser { get; set; }
        }
    }
}
