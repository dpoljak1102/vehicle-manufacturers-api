namespace VehicleManufacturers.WebApi.RestModels
{
    public class VehicleModelRest
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public string? Abrv { get; set; }
    }
}
