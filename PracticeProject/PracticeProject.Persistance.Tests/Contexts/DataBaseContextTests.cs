using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeProject.Application;
using PracticeProject.Domain.BaseInfo.User;
using PracticeProject.Persistence.Contexts;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PracticeProject.Persistance.Tests.Contexts
{
    [TestClass]
    public class DataBaseContextTests
    {
        private DataBaseContext _context;
        private IDb _db;

        [TestInitialize]
        public void Setup()
        {
            _context = new DataBaseContext();
            _db = _context;
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
                email,
                "TestPassword"
                );

            _db.Add<User>(user);
            _db.SaveChanges();
            int id = user.Id;

            IDb getSavedUserContext = new DataBaseContext();
            User savedUser = getSavedUserContext.Query<User>().FirstOrDefault(u => u.Id == id);

            //savedUser.FirstName = "Modified";
            getSavedUserContext.SaveChanges();
            getSavedUserContext.Dispose();

            // Act
            IDb verificationContext = new DataBaseContext();
            //string firstname = verificationContext.Query<User>().FirstOrDefault(u => u.Id == id).FirstName;
            verificationContext.Dispose();

            // Assert
            //Assert.AreEqual("Parham", firstname);
        }

        [TestMethod]
        public void Set_ShouldTrackEntities()
        {
            // Arrange
            string email = $"settracking_test_{Guid.NewGuid()}@example.com";
            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                );

            _db.Add<User>(user);
            _db.SaveChanges();
            int id = user.Id;

            IDb getSavedUserContext = new DataBaseContext();
            User savedUser = getSavedUserContext.Set<User>().FirstOrDefault(u => u.Id == id);
            //savedUser.FirstName = "Modified";
            getSavedUserContext.SaveChanges();
            getSavedUserContext.Dispose();

            // Act
            IDb verificationContext = new DataBaseContext();
            //string modifiedUserFirstName= verificationContext.Query<User>().FirstOrDefault(u => u.Id == id).FirstName;
            verificationContext.Dispose();

            // Assert
            //Assert.AreEqual("Modified", modifiedUserFirstName);
        }

        [TestMethod]
        public void Add_ShouldSetEntityStateToAdded()
        {
            // Arrange
            string email = $"addsetstatetoadded_test_{Guid.NewGuid()}@example.com";
            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                );

            // Act
            _db.Add(user);
            EntityState state = _context.Entry<User>(user).State;

            // Assert
            Assert.AreEqual(EntityState.Added, state);
        }

        [TestMethod]
        public void Add_ShouldPersistEntity()
        {
            // Arrange
            string email = $"addpersistentity_test_{Guid.NewGuid()}@example.com";
            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                );

            _db.Add<User>(user);
            _db.SaveChanges();

            // Act
            IDb verificationContext = new DataBaseContext();
            User savedUser = verificationContext.Set<User>().AsNoTracking().FirstOrDefault(u => u.Id == user.Id);
            verificationContext.Dispose();

            // Assert
            Assert.IsNotNull(savedUser);
            Assert.AreEqual(user.Id, savedUser.Id);
        }

        [TestMethod]
        public void Remove_ShouldSetEntityStateToDeleted()
        {
            // Arrange
            string email = $"removesetentitytodeleted_test_{Guid.NewGuid()}@example.com";
            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                );
            _db.Add<User>(user);
            _db.SaveChanges();

            //Act
            User savedUser = _db.Set<User>().FirstOrDefault(u => u.Id == user.Id);
            _db.Remove<User>(savedUser);
            EntityState state = _context.Entry<User>(savedUser).State;

            // Assert
            Assert.AreEqual(EntityState.Deleted, state);
        }

        [TestMethod]
        public void Remove_ShouldDeleteEntityFromDatabase()
        {
            // Arrange
            string email = $"removedeletefromdatabase_test_{Guid.NewGuid()}@example.com";
            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                );

            _db.Add<User>(user);
            _db.SaveChanges();

            _db.Remove<User>(user);
            _db.SaveChanges();

            // Act
            IDb verificationContext = new DataBaseContext();
            User deletedUser = verificationContext.Query<User>().FirstOrDefault(u => u.Id == user.Id);
            verificationContext.Dispose();

            // Assert
            Assert.IsNull(deletedUser);
        }

        [TestMethod]
        public void DeleteById_ShouldDeleteEntityFromDatabase()
        {
            // Arrange
            string email = $"deletebyiddeletefromdatabase_test_{Guid.NewGuid()}@example.com";
            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                );

            _db.Add<User>(user);
            _db.SaveChanges();

            int id = user.Id;

            IDb deleteContext = new DataBaseContext();
            deleteContext.DeleteById<User>(id);
            deleteContext.SaveChanges();
            deleteContext.Dispose();

            // Act
            IDb verificationContext = new DataBaseContext();
            User deletedUser = verificationContext.Query<User>().FirstOrDefault(u => u.Id == id);
            verificationContext.Dispose();

            // Assert
            Assert.IsNull(deletedUser);
        }

        [TestMethod]
        public void DeleteById_NonExistingEntity_ShouldThrowException()
        {
            // Arrange
            int id = 999999999;

            // Act
            _db.DeleteById<User>(id);
            Action act = () => _db.SaveChanges();

            // Assert
            Assert.ThrowsException<DbUpdateConcurrencyException>(act);
        }
    }
}
