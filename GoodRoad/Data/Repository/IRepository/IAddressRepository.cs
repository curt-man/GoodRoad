using GoodRoad.Models;

namespace GoodRoad.Data.Repository.IRepository
{
    public interface IAddressRepository
    {
        
        ICollection<Address> GetAddresses();
        Address GetAddress(int id);

    }
}
