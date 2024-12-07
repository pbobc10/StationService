using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class SupervisorOutputDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string FamilyName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string GasStationName { get; set; }
    }
}
