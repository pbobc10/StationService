using StationService.Models;
using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class GasMeterInputDto
    {
        [Required]
        public double MeterReading { get; set; }

        public int? FuelPipeId { get; set; }
       
    }
}
