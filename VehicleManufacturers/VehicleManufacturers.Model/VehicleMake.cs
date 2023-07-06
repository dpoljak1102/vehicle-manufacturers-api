﻿using VehicleManufacturers.Model.Common;

namespace VehicleManufacturers.Model
{
    public class VehicleMake : IVehicleMake
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Abrv { get; set; }
    }
}
