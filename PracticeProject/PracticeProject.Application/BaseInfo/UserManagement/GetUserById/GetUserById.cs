using PracticeProject.Domain.BaseInfo.User;
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

        public GetUserByIdResponse Execute(GetUserByIdRequest request)
        {
            GetUserByIdResponse response = new GetUserByIdResponse();

            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            if (request.Id <= 0)
                CreateFailureResponse("ID cannot be zero or negative.");

            User user = _userDataAccess.GetById(request.Id);

            if (user is null)
                return CreateFailureResponse($"No user found with ID {request.Id}");

            response.IsSuccess = true;
            response.User = user;
            return response;
        }

        public class GetUserByIdRequest
        {
            public int Id {  get; set; }
        }

        public class GetUserByIdResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public User User { get; set; }
        }

        private GetUserByIdResponse CreateFailureResponse(string message)
        {
            GetUserByIdResponse response = new GetUserByIdResponse
            {
                IsSuccess = false,
                Message = message,
                User = null
            };
            return response;
        }
    }
}
