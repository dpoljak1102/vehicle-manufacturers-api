using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManufacturers.DAL.Entities;
using VehicleManufacturers.Model;
using VehicleManufacturers.Model.Common;

namespace VehicleManufacturers.Repository
{
    public class RepositoryProfile : AutoMapper.Profile
    {
        public RepositoryProfile()
        {
            CreateMap<VehicleMake, VehicleMakeEntity>().MaxDepth(1);
            CreateMap<VehicleMakeEntity, VehicleMake>();
            CreateMap<VehicleMakeEntity, IVehicleMake>().As<VehicleMake>();

            CreateMap<VehicleModel, VehicleModelEntity>().MaxDepth(1);
            CreateMap<VehicleModelEntity, VehicleModel>();
            CreateMap<VehicleModelEntity, IVehicleModel>().As<VehicleModel>();
        }
    }
}
