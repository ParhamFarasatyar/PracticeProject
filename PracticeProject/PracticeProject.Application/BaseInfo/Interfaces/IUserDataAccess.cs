using System.Collections.Generic;
using PracticeProject.Domain.BaseInfo.User;

namespace PracticeProject.Application.BaseInfo.Interfaces
{
    public interface IUserDataAccess
    {
        void Create(User user);

        User GetById(int id);

        List<User> GetAll();

        void Update(User user);

        void Delete(int id);
        bool ExistsByEmail(string email);
    }
}
