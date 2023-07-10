using VehicleManufacturers.DAL.Entities;
using VehicleManufacturers.WebApi.RestModels.VehicleModel;

namespace VehicleManufacturers.WebApi.RestModels
{
    public class RestProfile : AutoMapper.Profile
    {
        public RestProfile()
        {
            CreateMap<VehicleModel.VehicleModelRest, Model.VehicleModel>();
            CreateMap<VehicleModel.UpdateVehicleModelRest, Model.VehicleModel>();
            CreateMap<VehicleModel.CreateVehicleModelRest, Model.VehicleModel>();
            CreateMap<Model.VehicleModel, VehicleModel.VehicleModelRest>().MaxDepth(1);

            CreateMap<VehicleMake.VehicleMakeRest, Model.VehicleMake>();
            CreateMap<VehicleMake.UpdateVehicleMakeRest, Model.VehicleMake>();
            CreateMap<VehicleMake.CreateVehicleMakeRest, Model.VehicleMake>();
            CreateMap<Model.VehicleMake, VehicleMake.VehicleMakeRest>().MaxDepth(1);

        }
    }
}
