using VehicleManufacturers.Common.Pagination;
using VehicleManufacturers.Common.Sort;

namespace VehicleManufacturers.Repository.Common
{
    public interface IBaseRepository<TModel, TFilter>
    {
        Task<int> SaveChangesAsync();
        TModel Create(TModel model);
        Task<(ICollection<TModel> Data, int Total)> GetAsync(ISort? sort = null, IPagination? pagination = null, TFilter? filter = default);
        Task<TModel> GetByIdAsync(Guid id);
        TModel Update(TModel model);
        void Delete(Guid id);
    }
}
