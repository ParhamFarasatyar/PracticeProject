using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo.UserManagement.GetUserById
{
    public interface IGetUserById
    {
        GetUserByIdResponse Execute(GetUserByIdRequest request);

        public class GetUserByIdRequest
        {
            public int Id { get; set; }
        }

        public class GetUserByIdResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
        }
    }
}
