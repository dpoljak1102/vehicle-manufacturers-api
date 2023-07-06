using VehicleManufacturers.Model.Common;

namespace VehicleManufacturers.Model
{
    public class VehicleModel : IVehicleModel
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }
    }
}
