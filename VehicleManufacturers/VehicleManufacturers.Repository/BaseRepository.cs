using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
            IQueryable<TEntity> query = Context.Set<TEntity>();

            // Filtering
            if (filter != null)
            {
                query = ApplyFilter(query, filter);
            }

            // Sorting
            if (sort != null && !string.IsNullOrEmpty(sort.SortBy))
            {
                query = ApplySorting(query, sort.SortBy, sort.Order);
            }

            // Pagination
            query = ApplyPagination(query, pagination);

            int total = await query.CountAsync();
            List<TEntity> data = await query.ToListAsync();
            List<TModel> mappedData = Mapper.Map<List<TModel>>(data);

            return (mappedData, total);
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
            Context.Entry(entity).State = EntityState.Modified;
            return model;
        }

        public void Delete(Guid id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        private IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter)
        {
            var entityType = typeof(TEntity);
            var filterType = filter.GetType();
            var filterProperties = filterType.GetProperties();

            foreach (var property in filterProperties)
            {
                var filterValue = property.GetValue(filter);
                if (filterValue != null)
                {
                    var entityProperty = entityType.GetProperty(property.Name);
                    if (entityProperty != null)
                    {
                        var parameter = Expression.Parameter(entityType, "para");
                        var propertyAccess = Expression.Property(parameter, entityProperty);
                        var equality = Expression.Equal(propertyAccess, Expression.Constant(filterValue));

                        var lambda = Expression.Lambda<Func<TEntity, bool>>(equality, parameter);
                        query = query.Where(lambda);
                    }
                }
            }

            return query;
        }

        private IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, string sortBy, string? sortOrder)
        {
            if (sortOrder != null)
            {
                if (sortOrder == SortOrder.Desc)
                {
                    query = query.OrderByDescending(x => EF.Property<object>(x, sortBy));
                }
                else
                {
                    query = query.OrderBy(x => EF.Property<object>(x, sortBy));
                }
            }

            return query;
        }

        private IQueryable<TEntity> ApplyPagination(IQueryable<TEntity> query, IPagination? pagination)
        {
            if (pagination != null && pagination.PageSize.HasValue)
            {
                var pageNumber = pagination.PageNumber ?? 1;
                pageNumber = Math.Max(pageNumber, 1);
                var skip = (pageNumber - 1) * pagination.PageSize.Value;
                query = query.Skip(skip).Take(pagination.PageSize.Value);
            }

            return query;
        }
    }
}
