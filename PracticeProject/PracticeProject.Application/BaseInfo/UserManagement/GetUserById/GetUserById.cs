using PracticeProject.Domain.BaseInfo.Users;
using System;

namespace PracticeProject.Application.BaseInfo.UserManagement.GetUserById
{
    public class GetUserById : IGetUserById
    {
        private readonly IUserDataAccess _userDataAccess;

        public GetUserById(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess ?? throw new ArgumentNullException(nameof(userDataAccess));
        }

        public IGetUserById.Response Execute(IGetUserById.Request request)
        {
            IGetUserById.Response response = new IGetUserById.Response();

            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            if (request.Id <= 0)
                CreateFailureResponse("ID cannot be zero or negative.");

            User user = _userDataAccess.GetById(request.Id);

            if (user is null)
                return CreateFailureResponse($"No user found with ID {request.Id}");

            response.IsSuccess = true;
            response.Message = "User found successfully.";
            response.User = user;
            return response;
        }

        private IGetUserById.Response CreateFailureResponse(string message)
        {
            IGetUserById.Response response = new IGetUserById.Response
            {
                IsSuccess = false,
                Message = message,
                User = null
            };
            return response;
        }
    }
}
