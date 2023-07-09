using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManufacturers.Common.Filters.VegicleMake;
using VehicleManufacturers.DAL;
using VehicleManufacturers.DAL.Entities;
using VehicleManufacturers.Model.Common;
using VehicleManufacturers.Repository.Common;

namespace VehicleManufacturers.Repository
{
    public class VehicleMakeRepository : RepositoryBase<VehicleMakeEntity, IVehicleMake, IVehicleMakeFilter>, IVehicleMakeRepository
    {
        public VehicleMakeRepository(VehicleManufacturersContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
