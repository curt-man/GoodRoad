using GoodRoad.Models;
using Microsoft.AspNetCore.Identity;

namespace GoodRoad.Models
{
    public class AppUser : IdentityUser
    {
        public int NumberOfHoles { get; set; }
        public int NumberOfComments { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }

        public ICollection<Hole>? Holes { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
