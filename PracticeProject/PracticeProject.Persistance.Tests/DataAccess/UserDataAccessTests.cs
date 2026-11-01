using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeProject.Persistence.DataAccess;
using PracticeProject.Persistence.Contexts;
using PracticeProject.Domain.Entities;
using PracticeProject.Domain.Enums;
using System;
using System.Linq;
using System.Data.Entity;

namespace PracticeProject.Persistence.Tests
{
    [TestClass]
    public class UserDataAccessTests
    {
        private DataBaseContext _context;

        private UserDataAccess _userDataAccess;

        [TestInitialize]
        public void Setup()
        {
            _context = new DataBaseContext();
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

            var user = new User(
                0,
                default,
                "Parham",
                "Test",
                email,
                "TestPassword"
                );

            // Act
            _userDataAccess.Create(user);

            // Assert
            using (var verificationContext = new DataBaseContext())
            {
                var savedUser = verificationContext.Users.SingleOrDefault(u => u.Email == email);

                Assert.IsNotNull(savedUser);
                Assert.AreEqual("Parham", savedUser.FirstName);
                Assert.AreEqual("Test", savedUser.LastName);
                Assert.AreEqual(email, savedUser.Email);
            }
        }

        [TestMethod]
        public void GetById_ExistingUser_ReturnsUser()
        {
            // Arrange
            string email = $"getbyid_test_{Guid.NewGuid():N}@example.com";

            var user = new User(
                0,
                default,
                "Parham",
                "Test",
                email,
                "TestPassword"
                );

            _userDataAccess.Create(user);

            // Act
            var result = _userDataAccess.GetById(user.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
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

            var user1 = new User(
                0,
                default,
                "User1",
                "Test",
                email1,
                "TestPassword"
                );

            var user2 = new User(
                0,
                default,
                "User2",
                "Test",
                email2,
                "TestPassword"
                );

            _userDataAccess.Create(user1);
            _userDataAccess.Create(user2);

            // Act
            var result = _userDataAccess.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count >= 2);
            Assert.IsTrue(result.Any(u => u.Email == user1.Email));
            Assert.IsTrue(result.Any(u => u.Email == user2.Email));
        }

        [TestMethod]
        public void Update_DetachedUser_SaveChanges()
        {
            // Arrange
            string email = $"update_test_{Guid.NewGuid()}@example.com";
            var user = new User(
                0,
                default,
                "Parham",
                "UpdateTest",
                email,
                "TestPassword"
                );

            _userDataAccess.Create(user);

            _context.Entry(user).State = EntityState.Detached;

            // Act
            user.FirstName = "Modified";
            _userDataAccess.Update(user);

            // Assert
            using (var verificationContext = new DataBaseContext())
            {
                var result = verificationContext.Users.SingleOrDefault(u => u.Id == user.Id);
                Assert.IsNotNull(result);
                Assert.AreEqual("Modified", result.FirstName);
                Assert.AreEqual(email, result.Email);
            }
        }

        [TestMethod]
        public void Delete_ExistingDetachedUser_RemovesUser()
        {
            // Arrange
            string email = $"delete_test_{Guid.NewGuid()}@example.com";
            var user = new User(
                0,
                default,
                "Parham",
                "DeleteTest",
                email,
                "TestPassword"
                );

            _userDataAccess.Create(user);

            _context.Entry(user).State = EntityState.Detached;

            // Act
            _userDataAccess.Delete(user.Id);

            // Assert
            using (var verificationContext = new DataBaseContext())
            {
                var result = verificationContext.Users.SingleOrDefault(u => u.Id == user.Id);
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void Delete_NonExistingUser_DoesNotThrow()
        {
            // Arrange
            int id = 99999;

            // Act
            _userDataAccess.Delete(id);

            // Assert
        }
    }
}
