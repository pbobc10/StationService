using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class Assignment : BaseEntity
    {
        [Required]
        public int GasStationAttendantId { get; set; }
        public GasStationAttendant GasStationAttendant { get; set; }

        [Required]
        public int DispenserUnitId { get; set; }
        public DispensingUnit DispensingUnit { get; set; }

        [Required]
        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }

        [Required]
        public ShiftType ShiftType { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "0:dd-mm-yyyy", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        public ICollection<FuelQuantity> FuelQuantities { get; set; } 


    }
}
