using System.Collections.Generic;
using System.Linq;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Application.BaseInfos
{
    public interface IUserDataAccess
    {
        void Create(User user);

        User GetById(int id);

        User GetByEmail(string email);

        IQueryable<User> GetAll();

        void Update(User user);

        void Delete(int id);

        bool ExistsByEmail(string email);

        bool ExistsByEmail(string email, int excludedUserId);
    }
}
