using StationService.Models;

namespace StationService.DTOs
{
    public class DispensingUnitOutputDto
    {
        public int Id { get; set; }
        public string UnitNumber { get; set; }
        public string GasStationName { get; set; }
        public ICollection<FuelPipeOutputDto> Pipes { get; set; }
    }
}
