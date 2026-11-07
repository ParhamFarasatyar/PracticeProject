using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PracticeProject.Application.BaseInfo;
using PracticeProject.Application.BaseInfo.UserManagement.CreateUser;
using PracticeProject.Domain.BaseInfo.User;
using System;

namespace PracticeProject.Application.Tests.UserManagement.CreateUserTests
{
    [TestClass]
    public class CreateUserTests
    {
        private Mock<IUserDataAccess> _userDataAccessMock;
        private CreateUser _useCase;

        [TestInitialize]
        public void Setup()
        {
            _userDataAccessMock = new Mock<IUserDataAccess>();

            _useCase = new CreateUser(_userDataAccessMock.Object);
        }

        [TestMethod]
        public void Create_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            CreateUser.CreateUserRequest request = new CreateUser.CreateUserRequest
            {
                FirstName = "Parham",
                LastName = "CreateTest",
                Age = 23,
                Gender = default,
                Email = $"create_test_{Guid.NewGuid()}@example.com",
                Password = "TestPassword"
            };

            _userDataAccessMock.Setup(u => u.ExistsByEmail(It.IsAny<string>())).Returns(false);

            _userDataAccessMock.Setup(u => u.Create(It.IsAny<User>())).Callback<User>(user => user.Id = 123);

            // Act
            CreateUser.CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(123, response.UserId);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void Create_DuplicateEmail_ReturnsFailure()
        {
            // Arrange
            CreateUser.CreateUserRequest request = new CreateUser.CreateUserRequest()
            {
                FirstName = "Parham",
                LastName = "CreateDuplicateEmailTest",
                Age = 23,
                Gender = default,
                Email = $"createduplicateemail_test_{Guid.NewGuid()}@example.com",
                Password = "TestPassword"
            };

            _userDataAccessMock.Setup(u => u.ExistsByEmail(It.IsAny<string>())).Returns(true);

            // Act
            CreateUser.CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.UserId);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Never);
        }

        [TestMethod]
        public void Create_NullRequest_ReturensFailure()
        {
            // Arrange
            CreateUser.CreateUserRequest request = null;

            // Act
            CreateUser.CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.UserId);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Never);
            _userDataAccessMock.Verify(u => u.ExistsByEmail(It.IsAny<string>()), Times.Never);
        }


        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Create_InvalidFirstName_ReturnsFailure(string firstname)
        {
            // Arrange
            CreateUser.CreateUserRequest request = new CreateUser.CreateUserRequest
            {
                FirstName = firstname,
                LastName = "CreateEmptyFirstNameTest",
                Age = 23,
                Gender = default,
                Email = $"createemptyfirstname_test_{Guid.NewGuid()}@example.com",
                Password = "TestPassword"
            };

            // Act
            CreateUser.CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.UserId);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Never);
            _userDataAccessMock.Verify(u => u.ExistsByEmail(It.IsAny<string>()), Times.Never);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Create_InvalidEmail_ReturnsFailure(string email)
        {
            // Arrange
            CreateUser.CreateUserRequest request = new CreateUser.CreateUserRequest
            {
                FirstName = "Parham",
                LastName = "CreateInvalidEmailTest",
                Age = 23,
                Gender = default,
                Email = email,
                Password = "TestPassword"
            };

            // Act
            CreateUser.CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.UserId);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Never);
            _userDataAccessMock.Verify(u => u.ExistsByEmail(It.IsAny<string>()), Times.Never);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Create_InvalidPassword_ReturnsFailure(string password)
        {
            // Arrange
            CreateUser.CreateUserRequest request = new CreateUser.CreateUserRequest
            {
                FirstName = "Parham",
                LastName = "CreateInvalidPasswordTest",
                Age = 23,
                Gender = default,
                Email = $"createinvalidpassword_test_{Guid.NewGuid()}@example.com",
                Password = password
            };

            // Act
            CreateUser.CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.UserId);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Never);
            _userDataAccessMock.Verify(u => u.ExistsByEmail(It.IsAny<string>()), Times.Never);
        }

        [DataTestMethod]
        [DataRow("FirstName")]
        [DataRow("LastName")]
        [DataRow("Email")]
        [DataRow("Password")]
        public void Create_FieldExceedsMaximumLength_ReturnFailure(string fieldName)
        {
            // Arrange
            string longText = new string('A', 300);

            CreateUser.CreateUserRequest request = new CreateUser.CreateUserRequest
            {
                FirstName = "Parham",
                LastName = "CreateMaximumLengthTest",
                Age = 23,
                Gender = default,
                Email = $"exceedmaximumlength_test_{Guid.NewGuid()}@example.com",
                Password = "PasswordTest"
            };

            switch (fieldName)
            {
                case "FirstName":
                    request.FirstName = longText;
                    break;

                case "LastName":
                    request.LastName = longText;
                    break;

                case "Email":
                    request.Email = longText;
                    break;

                case "Password":
                    request.Password = longText;
                    break;
            }

            // Act
            CreateUser.CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.UserId);
            _userDataAccessMock.Verify(u => u.ExistsByEmail(It.IsAny<string>()), Times.Never);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Never);
        }
    }
}
