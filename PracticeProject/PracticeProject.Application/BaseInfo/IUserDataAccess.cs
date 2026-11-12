using System.Collections.Generic;
using PracticeProject.Domain.BaseInfo.Users;

namespace PracticeProject.Application.BaseInfo
{
    public interface IUserDataAccess
    {
        void Create(User user);

        User GetById(int id);

        User GetByEmail(string email, string includedPassword);

        List<User> GetAll();

        void Update(User user);

        void Delete(int id);

        bool ExistsByEmail(string email);

        bool ExistsByEmail(string email, int excludedUserId);
    }
}
