using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class FuelPipeInputDto
    {
        [Required]
        public FuelType Type { get; set; }

        [Required]
        public int DispensingUnitId { get; set; }

        public GasMeterInputDto Meter { get; set; }
    }
}
