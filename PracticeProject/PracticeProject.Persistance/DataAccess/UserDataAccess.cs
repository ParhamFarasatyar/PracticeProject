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

        public void Delete(int id)
        {
            var user = SearchUsers("").FirstOrDefault(u => u.Id == id);
            if (user is null)
                return;

            _context.Remove<Person>(user.Person);
            _context.Remove<User>(user);
        }

        public IQueryable<User> GetAll() => SearchUsers("");

        public User GetById(int id) => SearchUsers("").FirstOrDefault(u => u.Id == id);

        public User GetByEmail(string email) => SearchUsers("")
            .FirstOrDefault(u => u.Email == email);

        public void Update(User user)
        {
            _context.Update<User>(user);
            _context.Update<Person>(user.Person);
        }

        public bool ExistsByEmail(string email) => SearchUsers("").Any(u => u.Email == email);

        public bool ExistsByEmail(string email, int excludedUserId) => SearchUsers("")
            .Any(u => u.Email == email && u.Id != excludedUserId);

        public IQueryable<User> SearchUsers(string keyword)
        {
            keyword = keyword.Replace(" ", "");

            return _context.Query<User>()
                .Include(u => u.Person)
                .Where(u =>
                u.Person.FirstName.Replace(" ", "").Contains(keyword) ||
                u.Person.LastName.Replace(" ", "").Contains(keyword) ||
                u.Email.Replace(" ", "").Contains(keyword));
        }
    }
}
