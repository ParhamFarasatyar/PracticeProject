using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.UpdateUser;

public interface IUpdateUser
{
    User Execute(Request request);

    public class Request
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public Gender? Gender { get; set; }
        public Role? Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}