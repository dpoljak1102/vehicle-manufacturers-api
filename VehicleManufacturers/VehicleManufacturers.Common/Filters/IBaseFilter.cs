﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManufacturers.Common.Filters
{
    public interface IBaseFilter
    {
        string? Search { get; set; }
    }
}
