namespace VehicleManufacturers.Common.Filters.VegicleMake
{
    public interface IVehicleMakeFilter : IBaseFilter
    {
        Guid? Id { get; set; }
        string? Abrv { get; set; }
    }
}
