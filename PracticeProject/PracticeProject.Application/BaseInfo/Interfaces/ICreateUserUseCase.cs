using PracticeProject.Application.UserUseCases.CreateUser;

namespace PracticeProject.Application.BaseInfo.Interfaces
{
    public interface ICreateUserUseCase
    {
        CreateUserResponse Execute(CreateUserRequest request);
    }
}
