using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.CreateUser;

public interface ICreateUser
{
    User Execute(Request request);

    class Request
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}