using JobCandidateHub.Application.Interfaces.Repositories;
using JobCandidateHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Linq;

namespace JobCandidateHub.Infrastructure.Implementation.Repositories
{
    public class GenericRepository(ApplicationDbContext dbContext) : IGenericRepository
    {
        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity);

            await dbContext.Set<TEntity>().AddAsync(entity);

            await dbContext.SaveChangesAsync();

            var ret = 0;

            var key = typeof(TEntity).GetProperties().FirstOrDefault(p =>
                p.CustomAttributes.Any(attr =>
                    attr.AttributeType == typeof(KeyAttribute)));

            if (key != null)
            {
                ret = (int)(key.GetValue(entity, null) ?? 0);
            }

            return ret;
        }

        public async Task UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entityToUpdate);

            dbContext.Update(entityToUpdate);

            await dbContext.SaveChangesAsync();
        }
    }
}
