using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public class FuelQuantity : BaseEntity
    {
        [Required]
        public FuelType FuelType { get; set; }

        public double OpeningQuantity { get; set; }
        public double ClosingQuantity { get; set; }

        [Required]
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
