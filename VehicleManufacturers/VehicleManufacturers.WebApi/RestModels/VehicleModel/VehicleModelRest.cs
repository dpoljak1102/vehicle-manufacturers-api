namespace VehicleManufacturers.WebApi.RestModels.VehicleModel
{
    public class VehicleModelRest
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }
    }
}
