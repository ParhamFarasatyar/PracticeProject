using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeProject.Application.BaseInfo.Interfaces;
using PracticeProject.Domain.BaseInfo.User;
using PracticeProject.Persistence.Contexts;
using System;
using System.Data.Entity;
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

        [TestMethod]
        public void Set_ShouldTrackEntities()
        {
            // Arrange
            string email = $"settracking_test_{Guid.NewGuid()}@example.com";
            User user = new User(
                0,
                default,
                "Parham",
                "SetTrackingTest",
                email,
                "TestPassword"
                );

            _context.Add<User>(user);
            _context.SaveChanges();
            int id = user.Id;

            IDb getSavedUserContext = new DataBaseContext();
            User savedUser = getSavedUserContext.Set<User>().FirstOrDefault(u => u.Id == id);
            savedUser.FirstName = "Modified";
            getSavedUserContext.SaveChanges();
            getSavedUserContext.Dispose();

            // Act
            IDb verificationContext = new DataBaseContext();
            string modifiedUserFirstName= verificationContext.Query<User>().FirstOrDefault(u => u.Id == id).FirstName;
            verificationContext.Dispose();

            // Assert
            Assert.AreEqual("Modified", modifiedUserFirstName);
        }

        [TestMethod]
        public void Add_ShouldSetEntityStateToAdded()
        {
            // Arrange
            DataBaseContext context = new DataBaseContext();
            string email = $"addsetstatetoadded_test_{Guid.NewGuid()}@example.com";
            User user = new User(
                0,
                default,
                "Parham",
                "AddSetStateToAddedTest",
                email,
                "TestPassword"
                );

            // Act
            context.Users.Add(user);
            EntityState state = context.Entry<User>(user).State;
            context.Dispose();

            // Assert
            Assert.AreEqual(EntityState.Added, state);
        }
    }
}
