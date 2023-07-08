using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleManufacturers.DAL.Entities
{
    public class VehicleModelEntity : BaseEntity
    {
        [Required]
        public Guid MakeId { get; set; }

        [Required]
        [StringLength(255)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? Abrv { get; set; }

        [ForeignKey("MakeId")]
        public virtual VehicleMakeEntity? Make { get; set; }
    }
}
