using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class GasStationInputDto
    {
        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Location { get; set; }

        public int? SupervisorId { get; set; }

        [Required]
        public int AdministratorId { get; set; }

    }
}
