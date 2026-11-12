using PracticeProject.Application;
using PracticeProject.Application.BaseInfo;
using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Persistence.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IDb _context;

        public UserDataAccess(IDb context)
        {
            _context = context;
        }

        public void Create(User user) => _context.Add<User>(user);

        public void Delete(int id) => _context.DeleteById<User>(id);

        public List<User> GetAll() => _context.Query<User>().ToList();

        public User GetById(int id) => _context.Query<User>().FirstOrDefault(u => u.Id == id);

        public void Update(User user) => _context.Update<User>(user);

        public bool ExistsByEmail(string email) => _context.Query<User>().Any(u => u.Email == email);

        public bool ExistsByEmail(string email, int excludedUserId) => _context.Query<User>()
            .Any(u => u.Email == email && u.Id != excludedUserId);
    }
}
