using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Common
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly DbContext _context;
        private DbSet<T> _entities;

        #endregion

        #region Ctor

        public Repository(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking => Entities.AsNoTracking();

        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                _entities ??= _context.Set<T>();

                return _entities;
            }
        }

        #endregion

        #region Methods

        public IQueryable<T> GetAll(string[] include = null, bool includeAllRecords = false)
        {
            IQueryable<T> query = includeAllRecords ? TableNoTracking.IgnoreQueryFilters() : Table;

            if (include != null)
            {
                query = include.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query;
        }

        public IQueryable<T> GetAll(IEnumerable<Func<T, bool>> predicates, string[] include, bool includeAllRecords = false)
        {
            IQueryable<T> query = includeAllRecords ? TableNoTracking.IgnoreQueryFilters() : Table;

            if (include != null)
            {
                query = include.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            query = predicates.Aggregate(query, (current, predicate) => current.Where(predicate).AsQueryable());

            return query;
        }

        public Task<T> GetByIdAsync(int id, string[] include = null, bool includeAllRecords = false, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = includeAllRecords ? TableNoTracking.IgnoreQueryFilters() : Table;

            query = Queryable.Where(query, e => e.Id == id);

            if (include != null)
            {
                query = include.Aggregate(query, (current, includePath) => current.Include(includePath));
            }

            return query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var newEntity = await Entities.AddAsync(entity, cancellationToken);

            return newEntity.Entity;
        }

        public async Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await Entities.AddRangeAsync(entities, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            await Entities.AddRangeAsync(entities, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity.Id > 0) //Id already assigned, need to update
            {
                //We query local context first to see if it's there.
                var attachedEntity = await _context.Set<T>().FindAsync(entity.Id, cancellationToken);

                //We have it in the context, need to update.
                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    //If it's not found locally, we can attach it by setting state to modified.
                    //This would result in a SQL update statement for all fields
                    //when SaveChangesAsync is called.
                    var entry = _context.Entry(entity);
                    entry.State = EntityState.Modified;
                }
            }
            else
            {
                await _context.Set<T>().AddAsync(entity, cancellationToken);
            }
        }

        public async Task DeleteAsync(T entity, bool softDelete = false, CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (softDelete)
            {
                await UpdateAsync(entity, cancellationToken);
            }
            else
            {
                Entities.Remove(entity);
            }
        }

        #endregion
    }
}
