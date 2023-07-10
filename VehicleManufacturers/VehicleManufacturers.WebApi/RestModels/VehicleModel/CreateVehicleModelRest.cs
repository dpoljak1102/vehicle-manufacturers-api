namespace VehicleManufacturers.WebApi.RestModels.VehicleModel
{
    public class CreateVehicleModelRest
    {
        public Guid MakeId { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }
    }
}
