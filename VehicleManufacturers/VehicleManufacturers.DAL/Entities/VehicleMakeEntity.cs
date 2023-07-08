using System.ComponentModel.DataAnnotations;

namespace VehicleManufacturers.DAL.Entities
{
    public class VehicleMakeEntity : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? Abrv { get; set; }

    }
}
