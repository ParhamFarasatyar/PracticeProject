using PracticeProject.Application;
using PracticeProject.Application.BaseInfo;
using PracticeProject.Domain.BaseInfo.People;
using PracticeProject.Domain.BaseInfo.Users;
using System.Collections.Generic;
using System.Data.Entity;
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

        public IQueryable<User> GetAll() => _context.Query<User>().Include(u => u.Person);

        public User GetById(int id) => _context.Query<User>().Include(u => u.Person).FirstOrDefault(u => u.Id == id);

        public User GetByEmail(string email, string includedPassword) => _context.Query<User>()
            .Include(u => u.Person)
            .FirstOrDefault(u => u.Email == email && u.Password == includedPassword);

        public void Update(User user)
        {
            _context.Update<User>(user);
            _context.Update<Person>(user.Person);
        }

        public bool ExistsByEmail(string email) => _context.Query<User>().Any(u => u.Email == email);

        public bool ExistsByEmail(string email, int excludedUserId) => _context.Query<User>()
            .Any(u => u.Email == email && u.Id != excludedUserId);

        public IQueryable<User> SearchUsers(string searchString)
        {
            searchString = searchString.Replace(" ", "");

            return _context.Query<User>()
                .Include(u => u.Person)
                .Where(u =>
                u.Person.FirstName.Replace(" ", "").Contains(searchString) ||
                u.Person.LastName.Replace(" ", "").Contains(searchString) ||
                u.Email.Replace(" ", "").Contains(searchString));
        }
    }
}
