using AutoMapper;
using VehicleManufacturers.Common.Filters.VehicleModel;
using VehicleManufacturers.DAL;
using VehicleManufacturers.DAL.Entities;
using VehicleManufacturers.Model.Common;
using VehicleManufacturers.Repository.Common;

namespace VehicleManufacturers.Repository
{
    public class VehicleModelRepository : RepositoryBase<VehicleModelEntity,IVehicleModel, IVehicleModelFilter> , IVehicleModelRepository
    {
        public VehicleModelRepository(VehicleManufacturersContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
