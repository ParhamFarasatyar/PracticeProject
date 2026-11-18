using PracticeProject.Domain.BaseInfos.People;
using PracticeProject.Domain.BaseInfos.Users;
using System.Collections.Generic;
using System.Linq;

namespace PracticeProject.Application.BaseInfos
{
    public interface IUserDataAccess
    {
        void Create(User user);

        SearchResult GetById(int id);

        SearchResult GetByEmail(string email);

        IQueryable<SearchResult> GetAll();

        void Update(SearchResult user);

        void Delete(int id);

        bool ExistsByEmail(string email);

        bool ExistsByEmail(string email, int excludedUserId);

        public class SearchResult
        {
            public User User { get; set; }
            public Person Person { get; set; }
        }
    }
}
