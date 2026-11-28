using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos;

public interface IUserDataAccess
{
    void Create(User user);

    User GetById(int id);

    User GetByEmail(string email);

    void Update(User user);

    void Delete(int id);

    bool ExistsByEmail(string email);

    bool ExistsByEmail(string email, int excludedUserId);
}