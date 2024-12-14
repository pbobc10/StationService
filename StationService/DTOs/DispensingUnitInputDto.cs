using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class DispensingUnitInputDto
    {
        [Required, StringLength(1,ErrorMessage ="Can only have one letter")]
        public string UnitNumber { get; set; }

        [Required]
        public int GasStationId { get; set; }

    }
}
