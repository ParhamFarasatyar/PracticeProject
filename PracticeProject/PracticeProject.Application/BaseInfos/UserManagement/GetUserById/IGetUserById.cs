using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetUserById
{
    public interface IGetUserById
    {
        User Execute(Request request);

        public class Request
        {
            public int Id { get; set; }
        }
    }
}
