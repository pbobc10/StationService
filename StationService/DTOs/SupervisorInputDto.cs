using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class SupervisorInputDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string FamilyName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public int? GasStationId { get; set; }

    }
}
