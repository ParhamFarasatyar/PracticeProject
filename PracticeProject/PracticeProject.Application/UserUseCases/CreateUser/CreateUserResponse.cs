namespace PracticeProject.Application.UserUseCases.CreateUser
{
    public class CreateUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
    }
}
