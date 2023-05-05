using GoodRoad.Data.Repository.IRepository;
using GoodRoad.Models;

namespace GoodRoad.Data.Repository
{
    
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AddressRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Address GetAddress(int id)
        {
            return _dbContext.Addresses.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Address> GetAddresses()
        {
            return _dbContext.Addresses.OrderBy(p=>p.Id).ToList();
        }
    }
}
