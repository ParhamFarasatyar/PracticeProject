namespace PracticeProject.Application.BaseInfo.UserManagement.DeleteUser
{
    public interface IDeleteUser
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
            public int? UserId { get; set; }
        }
    }
}
