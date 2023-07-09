using VehicleManufacturers.Model;

namespace VehicleManufacturers.WebApi.RestModels
{
    public class RestProfile : AutoMapper.Profile
    {
        public RestProfile() 
        {
            CreateMap<VehicleModelRest, VehicleModel>();
            CreateMap<VehicleModel, VehicleModelRest>().MaxDepth(1);

            CreateMap<VehicleMakeRest, VehicleMake>();
            CreateMap<VehicleMake, VehicleMakeRest>().MaxDepth(1);

        }
    }
}
