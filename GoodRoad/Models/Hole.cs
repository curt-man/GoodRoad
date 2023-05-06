using GoodRoad.Data.Enums;
using GoodRoad.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodRoad.Models
{
    public class Hole
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfLikes { get; set; }
        public Size Size { get; set; }

        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        [ValidateNever]
        public Address? Address { get; set; }

        public string? ContributorId { get; set; }
        [ForeignKey("ContributorId")]
        [ValidateNever]
        public AppUser? Contributor { get; set; }

        public int? CoordinatesId { get; set; }
        [ForeignKey("CoordinatesId")]
        [ValidateNever]
        public Coordinates? Coordinates { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime FixedAt { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageId { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
