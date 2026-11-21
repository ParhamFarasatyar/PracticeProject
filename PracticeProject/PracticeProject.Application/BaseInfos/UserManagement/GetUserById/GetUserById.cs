using PracticeProject.Domain.BaseInfos.Users;
using System;

namespace PracticeProject.Application.BaseInfos.UserManagement.GetUserById
{
    public class GetUserById(IUserDataAccess userDataAccess, IPersonDataAccess personDataAccess) : IGetUserById
    {
        public IGetUserById.Response Execute(IGetUserById.Request request)
        {
            IGetUserById.Response response = new IGetUserById.Response();

            if (request is null)
                return CreateFailureResponse("Request cannot be null.");

            if (request.Id <= 0)
                CreateFailureResponse("ID cannot be zero or negative.");

            User user = userDataAccess.GetById(request.Id);
            user.Person = personDataAccess.GetById(user.PersonId);

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
