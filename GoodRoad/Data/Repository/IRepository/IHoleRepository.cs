using GoodRoad.Models;

namespace GoodRoad.Data.Repository.IRepository
{
    public interface IHoleRepository
    {
        
        ICollection<Hole> GetHoles();
        Hole GetHole(int id);
        int GetHoleLikes(int id);
    }
}
