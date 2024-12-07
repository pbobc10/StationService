using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class GasStationAttendant: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string FamilyName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public ShiftType Shift { get; set; }

        [Required]
        public int GasStationId { get; set; }

        public GasStation GasStation { get; set; }
    }
}
