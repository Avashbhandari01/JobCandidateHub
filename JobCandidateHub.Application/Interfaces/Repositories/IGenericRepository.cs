using System.Linq.Expressions;

namespace JobCandidateHub.Application.Interfaces.Repositories
{
    public interface IGenericRepository
    {
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "") where TEntity : class;

        Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;

        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;

        Task UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class;
    }
}
