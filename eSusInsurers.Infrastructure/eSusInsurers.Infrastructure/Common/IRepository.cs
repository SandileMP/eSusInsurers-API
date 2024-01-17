using eSusInsurers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eSusInsurers.Infrastructure.Common
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }

        #endregion

        #region Methods

        /// <summary>
        /// GetAll all entities
        /// </summary>
        /// <param name="include">Include from</param>
        /// <param name="includeAllRecords"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(string[] include = null, bool includeAllRecords = false);

        /// <summary>
        /// GetAll all records from the table
        /// </summary>
        /// <param name="predicates">Predicate to filter</param>
        /// <param name="include">Includes a navigation property</param>
        /// <param name="includeAllRecords">Include all records even if they use some queryfilters</param>
        /// <returns></returns>
        IQueryable<T> GetAll(IEnumerable<Func<T, bool>> predicates, string[] include = null,
            bool includeAllRecords = false);

        /// <summary>
        /// GetAll entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="include">Include navigation properties</param>
        /// <param name="includeAllRecords">include deleted records or not</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id, string[] include = null, bool includeAllRecords = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// AddAsync entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// AddAsync entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// AddAsync entities
        /// </summary>
        /// <param name="entities">Entities</param>
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);


        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task DeleteAsync(T entity, bool softDelete = false, CancellationToken cancellationToken = default);

        #endregion
    }
}
