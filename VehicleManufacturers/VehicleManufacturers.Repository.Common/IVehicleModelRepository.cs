using VehicleManufacturers.Common.Filters.VehicleModel;
using VehicleManufacturers.Model.Common;

namespace VehicleManufacturers.Repository.Common
{
    public interface IVehicleModelRepository : IBaseRepository<IVehicleModel, IVehicleModelFilter>
    {
    }
}
