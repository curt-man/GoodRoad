using GoodRoad.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodRoad.Data.Repository.IRepository
{
    public interface IHoleRepository
    {
        
        ICollection<Hole> GetHoles();
        ICollection<Hole> GetHolesByCity(string city);
        ICollection<Hole> GetHolesByState(string state);
        ICollection<Hole> GetHolesByStreet(string street);
        ICollection<Hole> GetHolesByUser(string user);
        Hole GetHole(int id);
        int GetHoleLikes(int id);

        bool CreateHole(Hole hole);
        bool UpdateHole(Hole hole);
        bool DeleteHole(int id);


        bool Save();
    }
}
