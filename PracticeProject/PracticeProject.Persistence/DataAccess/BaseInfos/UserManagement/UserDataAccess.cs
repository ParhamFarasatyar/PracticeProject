using System.Linq;
using PracticeProject.Application;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPersons;
using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Domain.BaseInfos.Users;

namespace PracticeProject.Persistence.DataAccess.BaseInfos.UserManagement;

public class UserDataAccess(IDb db, ISearchUsers searchUsers) : IUserDataAccess
{
    public void Create(User user)
    {
        db.Add(user);
    }

    public void Delete(int id)
    {
        var user = GetById(id);

        if (user is null)
            return;

        db.Remove(user);
    }

    public User GetById(int id)
    {
        return searchUsers.Respond(new ISearchUsers.Request
        {
            Id = id,
            PersonRequest = new ISearchPersons.Request()
        }).SingleOrDefault(u => u.Id == id);
    }

    public User GetByEmail(string email)
    {
        return searchUsers.Respond(new ISearchUsers.Request
        {
            KeyWord = email,
            PersonRequest = new ISearchPersons.Request()
        }).SingleOrDefault(r => r.Email == email);
    }

    public void Update(User user)
    {
        db.Update(user);
    }

    public bool ExistsByEmail(string email)
    {
        return searchUsers.Respond(new ISearchUsers.Request
        {
            KeyWord = email
        }).Any();
    }

    public bool ExistsByEmail(string email, int excludedUserId)
    {
        return searchUsers.Respond(new ISearchUsers.Request
        {
            Id = excludedUserId,
            KeyWord = email
        }).Any(u => u.Id != excludedUserId);
    }

    public IQueryable<User> GetAll()
    {
        return searchUsers.Respond(new ISearchUsers.Request
        {
            PersonRequest = new ISearchPersons.Request()
        });
    }
}