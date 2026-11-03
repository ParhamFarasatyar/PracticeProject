using PracticeProject.Domain.BaseInfo.User;
using System.Collections.Generic;

namespace PracticeProject.Application.BaseInfo.UserManagement.GetAllUsers
{
    public class GetAllUsersResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
