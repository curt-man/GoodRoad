using GoodRoad.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GoodRoad.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public int NumberOfHoles { get; set; }
        public int NumberOfComments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? BirthDay { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageId { get; set; }

        [ValidateNever]
        public ICollection<Hole>? Holes { get; set; }
        [ValidateNever]
        public ICollection<Comment>? Comments { get; set; }
    }
}
