using System.ComponentModel.DataAnnotations;

namespace StationService.DTOs
{
    public class AdministratorOutputDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }

        public ICollection<GasStationOutputDto> gasStations { get; set; }
    }
}
