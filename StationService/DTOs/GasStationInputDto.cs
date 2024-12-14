using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class GasStationInputDto
    {
        public int Id { get; set; }

        [Required, StringLength(30,ErrorMessage = "Name cannot be longer than 30 characters.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Location cannot be longer than 50 characters.")]
        public string Location { get; set; }

        public int? SupervisorId { get; set; }

        [Required]
        public int AdministratorId { get; set; }

    }
}
