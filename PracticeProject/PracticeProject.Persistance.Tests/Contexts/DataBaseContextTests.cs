using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticeProject.Application;
using PracticeProject.Domain.BaseInfo.Person;
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

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "QueryShouldNotTrackEntitiesTest",
                Age = 23,
                Gender = default
            };

            var user = new User(
                0,
                default,
                email,
                "TestPassword"
                )
            { Person = person };

            _db.Add<User>(user);
            _db.SaveChanges();
            int id = user.Id;

            DataBaseContext getSavedUserContext = new DataBaseContext();
            IDb getSavedUserDb = getSavedUserContext;
            User savedUser = getSavedUserDb.Query<User>().FirstOrDefault(u => u.Id == id);

            savedUser.Password = "Modified";
            getSavedUserDb.SaveChanges();
            getSavedUserContext.Dispose();

            // Act
            IDb verificationContext = new DataBaseContext();
            string password = verificationContext.Query<User>().FirstOrDefault(u => u.Id == id).Password;
            verificationContext.Dispose();

            // Assert
            Assert.AreEqual("TestPassword", password);
        }

        [TestMethod]
        public void Set_ShouldTrackEntities()
        {
            // Arrange
            string email = $"settracking_test_{Guid.NewGuid()}@example.com";

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "SetShouldTrackEntitiesTest",
                Age = 23,
                Gender = default
            };

            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                )
            { Person = person };

            _db.Add<User>(user);
            _db.SaveChanges();
            int id = user.Id;

            DataBaseContext getSavedUserContext = new DataBaseContext();
            IDb getSavedUserDb = getSavedUserContext;
            User savedUser = getSavedUserDb.Set<User>().FirstOrDefault(u => u.Id == id);
            savedUser.Password = "Modified";
            getSavedUserDb.SaveChanges();
            getSavedUserContext.Dispose();

            // Act
            IDb verificationContext = new DataBaseContext();
            string modifiedPassword = verificationContext.Query<User>().FirstOrDefault(u => u.Id == id).Password;
            verificationContext.Dispose();

            // Assert
            Assert.AreEqual("Modified", modifiedPassword);
        }

        [TestMethod]
        public void Add_ShouldSetEntityStateToAdded()
        {
            // Arrange
            string email = $"addsetstatetoadded_test_{Guid.NewGuid()}@example.com";

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "AddSetStateToAddedTest",
                Age = 23,
                Gender = default
            };

            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                )
            { Person = person };

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

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "AddShouldPersistEntityTest",
                Age = 23,
                Gender = default
            };

            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                )
            { Person = person };

            _db.Add<User>(user);
            _db.SaveChanges();

            // Act
            IDb verificationContext = new DataBaseContext();
            User savedUser = verificationContext.Query<User>().FirstOrDefault(u => u.Id == user.Id);
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

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "RemoveShouldSetStateToDeletedTest",
                Age = 23,
                Gender = default
            };

            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                )
            { Person = person };

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

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "RemoveShouldDeleteEntityFromDatabaseTest",
                Age = 23,
                Gender = default
            };

            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                )
            { Person = person };

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

            var person = new Person
            {
                FirstName = "Parham",
                LastName = "DeleteByIdShouldDeleteEntityFromDatabaseTest",
                Age = 23,
                Gender = default
            };

            User user = new User(
                0,
                default,
                email,
                "TestPassword"
                )
            { Person = person };

            _db.Add<User>(user);
            _db.SaveChanges();

            int id = user.Id;

            DataBaseContext deleteContext = new DataBaseContext();
            IDb deleteDb = deleteContext;
            deleteDb.DeleteById<User>(id);
            deleteDb.SaveChanges();
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
