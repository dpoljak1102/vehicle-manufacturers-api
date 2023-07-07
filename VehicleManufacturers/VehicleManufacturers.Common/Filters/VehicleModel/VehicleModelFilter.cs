namespace VehicleManufacturers.Common.Filters.VehicleModel
{
    public class VehicleModelFilter : BaseFilter, IVehicleModelFilter
    {
        public Guid? MakeId { get; set; }
        public string? Abrv { get; set; }
    }
}
