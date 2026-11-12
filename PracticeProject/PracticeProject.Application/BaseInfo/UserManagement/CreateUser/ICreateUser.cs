using PracticeProject.Domain.BaseInfo.People;
using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.CreateUser
{
    public interface ICreateUser
    {
        CreateUserResponse Execute(CreateUserRequest request);

        class CreateUserRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public Gender Gender { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        class CreateUserResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
        }
    }
}
