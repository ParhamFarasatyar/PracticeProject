using PracticeProject.Domain.BaseInfo.People;
using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.CreateUser
{
    public interface ICreateUser
    {
        Response Execute(Request request);

        class Request
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public Gender Gender { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        class Response
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
        }
    }
}
