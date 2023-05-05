using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;

namespace GoodRoad.Data.Repository
{
    
    public class CoordinatesRepository : ICoordinatesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CoordinatesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Coordinates GetCoordinates(int id)
        {
            return _dbContext.Coordinates.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Coordinates> GetCoordinates()
        {
            return _dbContext.Coordinates.OrderBy(p=>p.Id).ToList();
        }
    }
}
