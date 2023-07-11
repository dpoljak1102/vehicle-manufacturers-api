using VehicleManufacturers.Common.Filters.VehicleModel;
using VehicleManufacturers.Common.Pagination;
using VehicleManufacturers.Common.Sort;
using VehicleManufacturers.Model.Common;
using VehicleManufacturers.Repository.Common;
using VehicleManufacturers.Service.Common;

namespace VehicleManufacturers.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehicleModelRepository _vehicleModelRepository;
        
        public VehicleModelService(IVehicleModelRepository vehicleModelRepository)
        {
            _vehicleModelRepository = vehicleModelRepository;

        }

        public async Task<IVehicleModel> CreateAsync(IVehicleModel model)
        {
            try
            {
                _vehicleModelRepository.Create(model);
                await _vehicleModelRepository.SaveChangesAsync();
                return model;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await GetByIdAsync(id);
            try
            {
                if (model != null)
                {
                    _vehicleModelRepository.Delete(id);
                    await _vehicleModelRepository.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<(ICollection<IVehicleModel> Data, int Total)> GetAsync(ISort? sort = null, IPagination? pagination = null, IVehicleModelFilter? filter = null)
        {
            try
            {
                return await _vehicleModelRepository.GetAsync(sort, pagination, filter);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IVehicleModel> GetByIdAsync(Guid id)
        {

            try
            {
                return await _vehicleModelRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IVehicleModel> UpdateAsync(IVehicleModel model)
        {
            try
            {
                var updatedVehicleMake = _vehicleModelRepository.Update(model);
                await _vehicleModelRepository.SaveChangesAsync();
                return updatedVehicleMake;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
