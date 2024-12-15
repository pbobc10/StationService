using StationService.Models;

namespace StationService.DTOs
{
    public class GasStationAttendantOutputDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }

        public ShiftType Shift { get; set; }

        public string GasStationName { get; set; }

        public int GasStationId { get; set; }
    }
}
