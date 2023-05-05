using GoodRoad.Models;

namespace GoodRoad.Data.Repository.IRepository
{
    public interface IAppUserRepository
    {
        ICollection<AppUser> GetUsers();
        AppUser GetUser(string id);
       
        bool CreateUser(AppUser user);
        bool UpdateUser(AppUser user);
        bool DeleteUser(AppUser user);
        
        bool Save();
    }
}
