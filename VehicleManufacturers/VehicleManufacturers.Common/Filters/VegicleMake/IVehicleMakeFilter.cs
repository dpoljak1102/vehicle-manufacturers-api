using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManufacturers.Common.Filters.VegicleMake
{
    public interface IVehicleMakeFilter : IBaseFilter
    {
        Guid? Id { get; set; }
        string? Abrv { get; set; }
    }
}
