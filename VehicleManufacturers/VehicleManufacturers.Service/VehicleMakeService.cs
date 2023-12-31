﻿using VehicleManufacturers.Common.Filters.VegicleMake;
using VehicleManufacturers.Common.Pagination;
using VehicleManufacturers.Common.Sort;
using VehicleManufacturers.Model.Common;
using VehicleManufacturers.Repository.Common;
using VehicleManufacturers.Service.Common;

namespace VehicleManufacturers.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IVehicleMakeRepository _vehicleMakeRepository;

        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
        }

        public async Task<IVehicleMake> CreateAsync(IVehicleMake vehicleMake)
        {
            try
            {
                _vehicleMakeRepository.Create(vehicleMake);
                await _vehicleMakeRepository.SaveChangesAsync();
                return vehicleMake;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var vehicleMake = await GetByIdAsync(id);
            try
            {
                if (vehicleMake != null)
                {
                    _vehicleMakeRepository.Delete(id);
                    await _vehicleMakeRepository.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<(ICollection<IVehicleMake> Data, int Total)> GetAsync(ISort? sort = null, IPagination? pagination = null, IVehicleMakeFilter? filter = default)
        {
            try
            {
                return await _vehicleMakeRepository.GetAsync(sort, pagination, filter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IVehicleMake> GetByIdAsync(Guid id)
        {
            try
            {
                return await _vehicleMakeRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IVehicleMake> UpdateAsync(IVehicleMake vehicleMake)
        {
            try
            {
                var updatedVehicleMake = _vehicleMakeRepository.Update(vehicleMake);
                await _vehicleMakeRepository.SaveChangesAsync();
                return updatedVehicleMake;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

    }
}
