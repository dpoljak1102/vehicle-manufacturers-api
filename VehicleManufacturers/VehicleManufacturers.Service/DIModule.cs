using Ninject.Modules;
using VehicleManufacturers.Service.Common;

namespace VehicleManufacturers.Service
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMakeService>().To<VehicleMakeService>().InTransientScope();
            Bind<IVehicleModelService>().To<VehicleModelService>().InTransientScope();
        }
    }
}
