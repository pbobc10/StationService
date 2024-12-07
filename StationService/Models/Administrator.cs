using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class Administrator : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string FamilyName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public  ICollection<GasStation> Stations = new List<GasStation>();

    }
}
