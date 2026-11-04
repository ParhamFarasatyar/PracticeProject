using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeProject.Application.BaseInfo.Interfaces;
using PracticeProject.Domain.BaseInfo.User;
using PracticeProject.Persistence.Contexts;
using System;
using System.Linq;

namespace PracticeProject.Persistance.Tests.Contexts
{
    [TestClass]
    public class DataBaseContextTests
    {
        private IDb _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new DataBaseContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void Query_ShouldNotTrackEntities()
        {
            // Arrange
            string email = $"querynontracking_test_{Guid.NewGuid()}@example.com";

            var user = new User(
                0,
                default,
                "Parham",
                "QueryNonTrackingTest",
                email,
                "TestPassword"
                );

            _context.Add<User>(user);
            _context.SaveChanges();
            int id = user.Id;

            IDb getSavedUserContext = new DataBaseContext();
            User savedUser = getSavedUserContext.Query<User>().FirstOrDefault(u => u.Id == id);

            savedUser.FirstName = "Modified";
            getSavedUserContext.SaveChanges();
            getSavedUserContext.Dispose();

            // Act
            IDb verificationContext = new DataBaseContext();
            string firstname = verificationContext.Query<User>().FirstOrDefault(u => u.Id == id).FirstName;
            verificationContext.Dispose();

            // Assert
            Assert.AreEqual("Parham", firstname);
        }
    }
}
