using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class GasStation : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public int? SupervisorId { get; set; }

        public Supervisor Supervisor { get; set; }

        [Required]
        public int AdministratorId { get; set; }

        public Administrator Administrator { get; set; }

        public ICollection<GasStationAttendant> GasStationAttendants { get; set; } 
        public ICollection<DispensingUnit> DispensingUnits { get; set; } 
    }
}
