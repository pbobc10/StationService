using StationService.Models;

namespace StationService.DTOs
{
    public class GasStationOutputDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string SupervisorName { get; set; } // Nom complet du superviseur

        public int? SupervisorId { get; set; }

        public string AdministratorName { get; set; } // Nom complet de l'administrateur

        public int AdministratorId { get; set; } // ID de l'administrateur

        public ICollection<GasStationAttendantOutputDto> GasStationAttendants { get; set; }
        public ICollection<DispensingUnitOutputDto> DispensingUnits { get; set; }
    }
}
