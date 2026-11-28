namespace PracticeProject.Application.BaseInfos.UserManagement.DeleteUser;

public interface IDeleteUser
{
    void Execute(Request request);

    public class Request
    {
        public int UserId { get; set; }
        public int PersonId { get; set; }
    }
}