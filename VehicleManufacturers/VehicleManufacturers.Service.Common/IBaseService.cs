using VehicleManufacturers.Common.Pagination;
using VehicleManufacturers.Common.Sort;

namespace VehicleManufacturers.Service.Common
{
    public interface IBaseService<TModel, TFilter>
    {
        Task<TModel> CreateAsync(TModel model);
        Task<(ICollection<TModel> Data, int Total)> GetAsync(ISort? sort = null, IPagination? pagination = null, TFilter? filter = default);
        Task<TModel> GetByIdAsync(Guid id);
        Task<TModel> UpdateAsync(TModel model);
        Task DeleteAsync(Guid id);
    }
}
