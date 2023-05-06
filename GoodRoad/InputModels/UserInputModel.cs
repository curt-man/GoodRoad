using GoodRoad.Data.Enums;
using GoodRoad.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodRoad.InputModels
{
    public class UserInputModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime? BirthDay { get; set; }

        public IFormFile? Image { get; set; }

        public string? Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
