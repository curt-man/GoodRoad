using GoodRoad.Data.Enums;
using GoodRoad.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodRoad.InputModels
{
    public class HoleInputModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfLikes { get; set; }
        public Size Size { get; set; }

        public Address? Address { get; set; }

        public AppUser? Contributor { get; set; }

        public Coordinates? Coordinates { get; set; }

        public IFormFile? Image { get; set; }

    }
}
