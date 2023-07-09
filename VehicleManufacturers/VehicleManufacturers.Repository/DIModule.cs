using Ninject.Modules;
using VehicleManufacturers.Repository.Common;

namespace VehicleManufacturers.Repository
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>().InSingletonScope();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>().InSingletonScope();
        }
    }
}
