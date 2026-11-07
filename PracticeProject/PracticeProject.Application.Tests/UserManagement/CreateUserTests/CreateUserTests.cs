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
            CreateUserRequest request = new CreateUserRequest
            {
                FirstName = "Parham",
                LastName = "CreateTest",
                Email = $"create_test_{Guid.NewGuid()}@example.com",
                Password = "TestPassword"
            };

            _userDataAccessMock.Setup(u => u.ExistsByEmail(It.IsAny<string>())).Returns(false);

            _userDataAccessMock.Setup(u => u.Create(It.IsAny<User>())).Callback<User>(user => user.Id = 123);

            // Act
            CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.AreEqual(123, response.UserId);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void Create_DuplicateEmail_ReturnsFailure()
        {
            // Arrange
            CreateUserRequest request = new CreateUserRequest()
            {
                FirstName = "Parham",
                LastName = "CreateDuplicateEmailTest",
                Email = $"createduplicateemail_test_{Guid.NewGuid()}@example.com",
                Password = "TestPassword"
            };

            _userDataAccessMock.Setup(u => u.ExistsByEmail(It.IsAny<string>())).Returns(true);

            // Act
            CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.UserId);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Never);
        }

        [TestMethod]
        public void Create_NullRequest_ReturensFailure()
        {
            // Arrange
            CreateUserRequest request = null;

            // Act
            CreateUserResponse response = _useCase.Execute(request);

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
            CreateUserRequest request = new CreateUserRequest
            {
                FirstName = firstname,
                LastName = "CreateEmptyFirstNameTest",
                Email = $"createemptyfirstname_test_{Guid.NewGuid()}@example.com",
                Password = "TestPassword"
            };

            // Act
            CreateUserResponse response = _useCase.Execute(request);

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
            CreateUserRequest request = new CreateUserRequest
            {
                FirstName = "Parham",
                LastName = "CreateInvalidEmailTest",
                Email = email,
                Password = "TestPassword"
            };

            // Act
            CreateUserResponse response = _useCase.Execute(request);

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
            CreateUserRequest request = new CreateUserRequest
            {
                FirstName = "Parham",
                LastName = "CreateInvalidPasswordTest",
                Email = $"createinvalidpassword_test_{Guid.NewGuid()}@example.com",
                Password = password
            };

            // Act
            CreateUserResponse response = _useCase.Execute(request);

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

            CreateUserRequest request = new CreateUserRequest
            {
                FirstName = "Parham",
                LastName = "CreateMaximumLengthTest",
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
            CreateUserResponse response = _useCase.Execute(request);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.UserId);
            _userDataAccessMock.Verify(u => u.ExistsByEmail(It.IsAny<string>()), Times.Never);
            _userDataAccessMock.Verify(u => u.Create(It.IsAny<User>()), Times.Never);
        }
    }
}
