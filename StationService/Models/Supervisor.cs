using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class Supervisor: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string FamilyName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int? GasStationId { get; set; }

        public GasStation Station { get; set; }
    }
}
