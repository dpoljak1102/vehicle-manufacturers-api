namespace VehicleManufacturers.Common.Filters.VehicleModel
{
    public interface IVehicleModelFilter : IBaseFilter
    {
        Guid? MakeId { get; set; }
        string? Abrv { get; set; }
    }
}
