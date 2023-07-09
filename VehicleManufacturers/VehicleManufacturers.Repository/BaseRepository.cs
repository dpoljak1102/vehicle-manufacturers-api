using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VehicleManufacturers.Common.Filters;
using VehicleManufacturers.Common.Pagination;
using VehicleManufacturers.Common.Sort;
using VehicleManufacturers.DAL;
using VehicleManufacturers.DAL.Entities;
using VehicleManufacturers.Repository.Common;

namespace VehicleManufacturers.Repository
{
    public abstract class RepositoryBase<TEntity, TModel, TFilter> : IBaseRepository<TModel, TFilter>
        where TEntity : BaseEntity, new()
        where TFilter : IBaseFilter
    {
        protected readonly VehicleManufacturersContext Context;
        protected readonly IMapper Mapper;

        protected RepositoryBase(VehicleManufacturersContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public TModel Create(TModel model)
        {
            Context.Set<TEntity>().Add(Mapper.Map<TEntity>(model));
            return model;
        }

        public virtual async Task<(ICollection<TModel> Data, int Total)> GetAsync(ISort? sort = null, IPagination? pagination = null, TFilter? filter = default)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();
            List<TEntity> entities = await query.ToListAsync();

            return (Mapper.Map<List<TModel>>(entities), 5);
        }

        public async Task<TModel> GetByIdAsync(Guid id)
        {
            TEntity? entity = await Context.Set<TEntity>()
                                    .FirstOrDefaultAsync(e => e.Id == id);

            return Mapper.Map<TModel>(entity!);
        }

        public TModel Update(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return model;
        }

        public void Delete(Guid id)
        {
            var entity = new TEntity { Id = id };
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Deleted;
        }

    }
}
