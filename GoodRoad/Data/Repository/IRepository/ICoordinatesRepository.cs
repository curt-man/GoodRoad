using GoodRoad.Models;

namespace GoodRoad.Data.Repository.IRepository
{
    public interface ICoordinatesRepository
    {
        
        ICollection<Coordinates> GetCoordinates();
        Coordinates GetCoordinate(int id);

    }
}
