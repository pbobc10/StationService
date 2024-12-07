using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class GasStationAttendantInputDto
    {
        [Required, StringLength(30)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string FamilyName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public ShiftType Shift { get; set; }

        [Required]
        public int GasStationId { get; set; }

        public GasStationInputDto GasStation { get; set; }
    }
}
