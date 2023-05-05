using GoodRoad.Models;
using Microsoft.AspNetCore.Identity;

namespace GoodRoad.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public int NumberOfHoles { get; set; }
        public int NumberOfComments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? BirthDay { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageId { get; set; }

        public ICollection<Hole>? Holes { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
