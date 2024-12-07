using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class DispensingUnitInputDto
    {
        [Required, StringLength(1)]
        public string UnitNumber { get; set; }

        [Required]
        public int GasStationId { get; set; }

    }
}
