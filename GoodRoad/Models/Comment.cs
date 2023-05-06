using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodRoad.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfLikes { get; set; }

        public string? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        [ValidateNever]
        public AppUser? Owner { get; set; }

        public int? HoleId { get; set; }
        [ForeignKey("HoleId")]
        [ValidateNever]
        public Hole? Hole { get; set; }

    }
}
