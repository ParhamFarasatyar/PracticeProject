using PracticeProject.Domain.BaseInfo.User;
using System;
using System.Data.Entity.Infrastructure;

namespace PracticeProject.Application.BaseInfo.UserManagement.UpdateUser
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IUserDataAccess _userDataAccess;

        private readonly IUnitOfWork _unitOfWork;

        public UpdateUser(IUserDataAccess userDataAccess, IUnitOfWork unitOfWork)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
            _unitOfWork = unitOfWork;
        }

        public UpdateUserResponse Execute(UpdateUserRequest request)
        {
            UpdateUserResponse response = new UpdateUserResponse();

            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            if (request.Email is null || request.Password is null)
                return CreateFailureResponse("Email and password cannot be null.");

            if (request.Id <= 0)
                return CreateFailureResponse("ID cannot be zero or negative.");

            if (_userDataAccess.ExistsByEmail(request.Email))
                return CreateFailureResponse("Email already exist.");

            User user = new User(request.Id, request.Role, request.Email, request.Password);

            try
            {
                _userDataAccess.Update(user);
                _unitOfWork.SaveChanges();

                response.IsSuccess = true;
                response.Message = "User modified successfully";
                response.ModifiedUser = user;
                return response;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return CreateFailureResponse(ex.Message);
            }
        }

        public class UpdateUserRequest
        {
            public int Id { get; set; }
            public Role Role { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UpdateUserResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User ModifiedUser { get; set; }
        }

        private UpdateUserResponse CreateFailureResponse(string message)
        {
            UpdateUserResponse response = new UpdateUserResponse
            {
                IsSuccess = false,
                Message = message,
                ModifiedUser = null
            };

            return response;
        }
    }
}
