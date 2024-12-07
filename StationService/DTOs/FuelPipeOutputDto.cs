using StationService.Models;

namespace StationService.DTOs
{
    public class FuelPipeOutputDto
    {
        public int Id { get; set; }
        public FuelType Type { get; set; }

        public string DispensingUnitName { get; set; }

        public GasMeterOutputDto Meter { get; set; }
    }
}
