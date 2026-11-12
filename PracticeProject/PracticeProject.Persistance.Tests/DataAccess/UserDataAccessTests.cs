using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeProject.Persistence.DataAccess;
using PracticeProject.Persistence.Contexts;
using PracticeProject.Domain.BaseInfo.Users;
using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PracticeProject.Domain.BaseInfo.People;
using PracticeProject.Application;

namespace PracticeProject.Persistence.Tests
{
    [TestClass]
    public class UserDataAccessTests
    {
        private DataBaseContext _context;

        private IDb _db;

        private UserDataAccess _userDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _context = new DataBaseContext();
            _db = _context;
            _userDataAccess = new UserDataAccess(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void Create_ValidUser_UserShouldBeSaved()
        {
            // Arrange
            string email = $"create_test_{Guid.NewGuid():N}@example.com";

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "CreateUserSavedTest",
                Age = 23,
                Gender = default
            };

            var user = new User(
                default,
                email,
                "TestPassword",
                person
                );

            // Act
            _userDataAccess.Create(user);
            _db.SaveChanges();

            // Assert
            using (var verificationContext = new DataBaseContext())
            {
                var savedUser = verificationContext.Users
                    .Include(u => u.Person)
                    .SingleOrDefault(u => u.Email == email);

                Assert.IsNotNull(savedUser);
                Assert.IsNotNull(savedUser.Person);
                Assert.AreEqual(person.Id, savedUser.PersonId);
                Assert.AreEqual(person.FirstName, savedUser.Person.FirstName);
                Assert.AreEqual(person.LastName, savedUser.Person.LastName);
                Assert.AreEqual(person.Age, savedUser.Person.Age);
                Assert.AreEqual(email, savedUser.Email);
            }
        }

        [TestMethod]
        public void GetById_ExistingUser_ReturnsUser()
        {
            // Arrange
            string email = $"getbyid_test_{Guid.NewGuid():N}@example.com";

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "GetByIdExistingUserTest",
                Age = 23,
                Gender = default
            };

            var user = new User(
                default,
                email,
                "TestPassword",
                person
                );

            _userDataAccess.Create(user);
            _db.SaveChanges();

            // Act
            var result = _userDataAccess.GetById(user.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(person.Id, result.PersonId);
            Assert.AreEqual(user.Email, result.Email);
        }

        [TestMethod]
        public void GetById_NonExistingUser_ReturnsNull()
        {
            // Arrange
            int id = 1;

            // Act
            var result = _userDataAccess.GetById(id);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAll_TwoUsersExist_ReturnsBothUsers()
        {
            // Arrange
            string email1 = $"getall_test_{Guid.NewGuid():N}@example.com";
            string email2 = $"getall_test_{Guid.NewGuid():N}@example.com";

            var person1 = new Person
            {
                FirstName = "Parham",
                LastName = "GetAllExistingUser1Test",
                Age = 23,
                Gender = default
            };

            var user1 = new User(
                default,
                email1,
                "TestPassword",
                person1
                );

            var person2 = new Person
            {
                FirstName = "Parham",
                LastName = "GetAllExistingUser2Test",
                Age = 23,
                Gender = default
            };

            var user2 = new User(
                default,
                email2,
                "TestPassword",
                person2
                );

            _userDataAccess.Create(user1);
            _userDataAccess.Create(user2);
            _db.SaveChanges();

            // Act
            var result = _userDataAccess.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count(u => u.Email == user1.Email || u.Email == user2.Email));
            Assert.IsTrue(result.Any(u => u.Email == user1.Email));
            Assert.IsTrue(result.Any(u => u.Email == user2.Email));
            Assert.IsTrue(result.Any(u => u.PersonId == user1.PersonId));
            Assert.IsTrue(result.Any(u => u.PersonId == user2.PersonId));
        }

        [TestMethod]
        public void GetAll_NoUserExist_ReturnsEmptyList()
        {
            // Arrange
            _context.Database.ExecuteSqlCommand("DELETE Users");

            // Act
            var result = _userDataAccess.GetAll();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Update_DetachedUser_SaveChanges()
        {
            // Arrange
            string email = $"update_test_{Guid.NewGuid()}@example.com";

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "UpdateDetachedUserTest",
                Age = 23,
                Gender = default
            };

            var user = new User(
                default,
                email,
                "TestPassword",
                person
                );

            _userDataAccess.Create(user);
            _db.SaveChanges();

            _context.Entry(user).State = EntityState.Detached;

            // Act
            user.Password = "ModifiedPassword";
            _userDataAccess.Update(user);
            _db.SaveChanges();

            // Assert
            using (var verificationContext = new DataBaseContext())
            {
                var result = verificationContext.Users.SingleOrDefault(u => u.Id == user.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual("ModifiedPassword", result.Password);
                Assert.AreEqual(email, result.Email);
            }
        }

        [TestMethod]
        public void Update_NonExistingUser_ThrowException()
        {
            // Arrange
            string email = $"updatenonexisting_test_{Guid.NewGuid()}@example.com";

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "UpdateNonExistingUserTest",
                Age = 23,
                Gender = default
            };

            var user = new User(
                default,
                email,
                "TestPassword",
                person
                );

            // Act
            _userDataAccess.Update(user);
            Action act = () => _db.SaveChanges();

            // Assert
            Assert.ThrowsException<DbUpdateConcurrencyException>(act);
        }

        [TestMethod]
        public void Delete_ExistingDetachedUser_RemovesUser()
        {
            // Arrange
            string email = $"delete_test_{Guid.NewGuid()}@example.com";

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "DeleteExistingUserTest",
                Age = 23,
                Gender = default
            };

            var user = new User(
                default,
                email,
                "TestPassword",
                person
                );

            _userDataAccess.Create(user);
            _db.SaveChanges();

            _context.Entry(user).State = EntityState.Detached;

            // Act
            _userDataAccess.Delete(user.Id);
            _db.SaveChanges();

            // Assert
            using (var verificationContext = new DataBaseContext())
            {
                var result = verificationContext.Users.SingleOrDefault(u => u.Id == user.Id);
                Assert.IsNull(result);

                var savedPerson = verificationContext.Set<Person>()
                    .SingleOrDefault(p => p.Id == person.Id);

                Assert.IsNotNull(savedPerson);
            }
        }

        [TestMethod]
        public void Delete_NonExistingUser_DoesNotThrow()
        {
            // Arrange
            int id = 99999;

            // Act
            _userDataAccess.Delete(id);
            Action act = () => _db.SaveChanges();

            // Assert
            Assert.ThrowsException<DbUpdateConcurrencyException>(act);
        }

        [TestMethod]
        public void ExistsByEmail_ExistingDetachedUser_ReturnsTrue()
        {
            // Arrange
            string email = $"existsbyemail_test_{Guid.NewGuid()}@example.com";

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "ExistsByEmailExistingUserTest",
                Age = 23,
                Gender = default
            };

            var user = new User(
                default,
                email,
                "TestPassword",
                person
                );

            _userDataAccess.Create(user);
            _db.SaveChanges();
            _context.Entry(user).State = EntityState.Detached;

            // Act
            bool isExist = _userDataAccess.ExistsByEmail(email);

            // Assert
            Assert.IsTrue(isExist);
        }

        [TestMethod]
        public void ExistsByEmail_NonExistingEmail_ReturnsFalse()
        {
            // Arrange
            string email = $"nonexistingemail_test_{Guid.NewGuid()}@example.com";

            // Act
            bool isExist = _userDataAccess.ExistsByEmail(email);

            // Assert
            Assert.IsFalse(isExist);
        }
    }
}
