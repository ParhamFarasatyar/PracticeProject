using PracticeProject.Application.Interfaces;
using PracticeProject.Domain.Entities;
using PracticeProject.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Persistence.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly DataBaseContext _context;

        public UserDataAccess(DataBaseContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = GetById(id);

            if (user == null)
                return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
