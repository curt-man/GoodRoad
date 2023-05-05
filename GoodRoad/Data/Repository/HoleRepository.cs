using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;

namespace GoodRoad.Data.Repository
{
    
    public class HoleRepository : IHoleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public HoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Hole GetHole(int id)
        {
            return _dbContext.Holes.FirstOrDefault(x => x.Id == id);
        }

        public int GetHoleLikes(int id)
        {
            return _dbContext.Holes.FirstOrDefault(x => x.Id == id).NumberOfLikes;
        }

        public ICollection<Hole> GetHoles()
        {
            return _dbContext.Holes.OrderBy(p=>p.Id).ToList();
        }
    }
}
