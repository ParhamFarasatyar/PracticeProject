using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetUserById
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
            public IUserDataAccess.SearchResult Result{ get; set; }
        }
    }
}
