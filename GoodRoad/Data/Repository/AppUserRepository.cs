using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodRoad.Data.Repository
{
    
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AppUserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AppUser GetUser(string id)
        {
            return _dbContext.AppUsers.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<AppUser> GetUsers()
        {
            return _dbContext.AppUsers.OrderBy(p=>p.Id).ToList();
        }

        public ICollection<Hole> GetHolesByUser(string user)
        {
            return _dbContext.Holes.Include("Address").Where(x => x.ContributorId == user).Include("Coordinates").OrderBy(p => p.NumberOfLikes).ToList();
        }

        //public bool CreateUser(AppUser appUser)
        //{
        //    _dbContext.AppUsers.Add(appUser);
        //    return Save();
        //}

        //public bool DeleteUser(AppUser appUser)
        //{
        //    _dbContext.AppUsers.Remove(appUser);
        //    return Save();
        //}


        //public bool UpdateUser(AppUser appUser)
        //{
        //    _dbContext.AppUsers.Update(appUser);
        //    return Save();
        //}

        public bool Save()
        {
            return _dbContext.SaveChanges() != 0;
        }
    }
}
