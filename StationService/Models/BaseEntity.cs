using System.ComponentModel.DataAnnotations;

namespace StationService.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }

    }
}
