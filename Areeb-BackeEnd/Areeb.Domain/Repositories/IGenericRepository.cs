using Areeb.Domain.Entities;
using System.Linq.Expressions;

namespace Areeb.Domain.Repositories
{
	public interface IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
	{
		Task<TEntity?> GetByIdAsync(TKey id);
		Task AddAsync(TEntity entity);
		Task AddRangeAsync(IEnumerable<TEntity> entities);
		Task<IReadOnlyList<TEntity>> GetAllAsync();

		Task<IReadOnlyList<TEntity>> GetWithPrdicate(Expression<Func<TEntity, bool>> pridecate);

		void RemoveRange(IEnumerable<TEntity> entities);
		void Remove(TEntity entity);
		void Update(TEntity entity);
	}
}
