using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeProject.Persistence.DataAccess;
using PracticeProject.Persistence.Contexts;
using PracticeProject.Domain.Entities;
using PracticeProject.Domain.Enums;
using System;
using System.Linq;

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
            string email = $"test_{Guid.NewGuid():N}@example.com";

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
    }
}
