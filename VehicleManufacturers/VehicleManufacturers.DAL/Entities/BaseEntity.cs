using System.ComponentModel.DataAnnotations;

namespace VehicleManufacturers.DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
