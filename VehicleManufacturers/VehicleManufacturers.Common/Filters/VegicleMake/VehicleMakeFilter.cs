namespace VehicleManufacturers.Common.Filters.VegicleMake
{
    public class VehicleMakeFilter : BaseFilter, IVehicleMakeFilter
    {
        public Guid? Id { get; set; }
        public string? Abrv { get; set; }
    }
}
