using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodRoad.Data.Repository
{

    public class HoleRepository : IHoleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public HoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateHole(Hole hole)
        {
            _dbContext.Add(hole);
            return Save();
        }

        public bool DeleteHole(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateHole(Hole hole)
        {
            throw new NotImplementedException();
        }

        public Hole GetHole(int id)
        {
            return _dbContext.Holes.Include("Address").Include("Coordinates").FirstOrDefault(x => x.Id == id);
        }

        public int GetHoleLikes(int id)
        {
            return _dbContext.Holes.FirstOrDefault(x => x.Id == id).NumberOfLikes;
        }

        public ICollection<Hole> GetHoles()
        {
            return _dbContext.Holes.Include("Address").Include("Coordinates").OrderBy(p => p.Id).ToList();
        }

        public ICollection<Hole> GetHolesByCity(string city)
        {
            return _dbContext.Holes.Include("Address").Where(x => x.Address.City == city).Include("Coordinates").OrderBy(p => p.Id).ToList();
        }

        public ICollection<Hole> GetHolesByState(string state)
        {
            return _dbContext.Holes.Include("Address").Where(x => x.Address.State == state).Include("Coordinates").OrderBy(p => p.Id).ToList();
        }

        public ICollection<Hole> GetHolesByStreet(string street)
        {
            return _dbContext.Holes.Include("Address").Where(x => x.Address.Street == street).Include("Coordinates").OrderBy(p => p.Id).ToList();
        }

        


        public bool Save()
        {
            return _dbContext.SaveChanges()!=0;
        }

    }
}
