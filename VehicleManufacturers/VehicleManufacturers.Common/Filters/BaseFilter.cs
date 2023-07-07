using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManufacturers.Common.Filters
{
    public class BaseFilter : IBaseFilter
    {
        public string? Search { get; set; }
    }
}
