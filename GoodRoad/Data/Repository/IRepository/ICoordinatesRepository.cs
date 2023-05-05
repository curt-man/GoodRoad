using GoodRoad.Models;

namespace GoodRoad.Data.Repository.IRepository
{
    public interface ICoordinatesRepository
    {
        
        ICollection<Coordinates> GetCoordinates();
        Coordinates GetCoordinates(int id);

    }
}
