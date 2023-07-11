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

        /// <summary>
        /// Saves the changes made in the context.
        /// </summary>
        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a new entity of type TEntity based on the provided model.
        /// </summary>
        public TModel Create(TModel model)
        {
            Context.Set<TEntity>().Add(Mapper.Map<TEntity>(model));
            return model;
        }

        /// <summary>
        /// Retrieves a collection of entities of type TEntity, with optional sorting, filtering, and pagination.
        /// </summary>
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

        /// <summary>
        /// Retrieves an entity of type TEntity by its Id.
        /// </summary>
        public async Task<TModel> GetByIdAsync(Guid id)
        {
            TEntity? entity = await Context.Set<TEntity>()
                                    .FirstOrDefaultAsync(e => e.Id == id);

            return Mapper.Map<TModel>(entity!);
        }

        /// <summary>
        /// Updates an entity of type TEntity based on the provided model.
        /// </summary>
        public TModel Update(TModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            Context.Entry(entity).State = EntityState.Modified;
            return model;
        }

        /// <summary>
        /// Deletes an entity of type TEntity by its ID.
        /// </summary>
        public void Delete(Guid id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        /// <summary>
        /// Applies filtering to the provided query based on the specified filter object.
        /// </summary>
        private IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter)
        {
            var entityType = typeof(TEntity);
            var filterType = filter.GetType();
            var filterProperties = filterType.GetProperties();

            foreach (var property in filterProperties)
            {
                // Get the value of the property from the filter object
                var filterValue = property.GetValue(filter);
                if (filterValue != null)
                {
                    // Get the corresponding property of the entity type
                    var entityProperty = entityType.GetProperty(property.Name);
                    if (entityProperty != null)
                    {
                        // Create a parameter expression for the entity type
                        var parameter = Expression.Parameter(entityType, "para");

                        // Create a property access expression for the entity property
                        var propertyAccess = Expression.Property(parameter, entityProperty);

                        // Create an equality expression to compare the property value with the filter value
                        var equality = Expression.Equal(propertyAccess, Expression.Constant(filterValue));

                        // Create a lambda expression with the equality expression and the parameter
                        var lambda = Expression.Lambda<Func<TEntity, bool>>(equality, parameter);

                        // Apply the filtering to the query using the lambda expression
                        query = query.Where(lambda);
                    }
                }
            }

            return query;
        }

        /// <summary>
        /// Applies sorting to the provided query based on the specified sort by and sort order.
        /// </summary>
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

        /// <summary>
        /// Applies pagination to the provided query based on the specified pagination settings.
        /// </summary>
        private IQueryable<TEntity> ApplyPagination(IQueryable<TEntity> query, IPagination? pagination)
        {
            if (pagination != null && pagination.PageSize.HasValue)
            {
                var pageNumber = pagination.PageNumber ?? 1;
                pageNumber = Math.Max(pageNumber, 1);

                // Calculate the number of items to skip based on the page number and page size
                var skip = (pageNumber - 1) * pagination.PageSize.Value;

                // Apply skip and take operations to the query
                query = query.Skip(skip).Take(pagination.PageSize.Value);
            }

            return query;
        }
    }
}
