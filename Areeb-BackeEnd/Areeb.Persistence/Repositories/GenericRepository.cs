using Areeb.Domain.Entities;
using Areeb.Domain.Repositories;
using Areeb.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Persistence.Repositories
{
	public class GenericRepository<TKey, TEntity> : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
	{
		private readonly AreebDbContext _context;
		private readonly DbSet<TEntity> _entities;
		public GenericRepository(AreebDbContext context)
		{
			_context = context;
			_entities = context.Set<TEntity>();
		}
		public async Task AddAsync(TEntity entity)
		=> await _entities.AddAsync(entity);

		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		=> await _entities.AddRangeAsync(entities);

		public async Task<IReadOnlyList<TEntity>> GetAllAsync()
		=> await _entities.AsNoTracking().ToListAsync();

		public async Task<TEntity?> GetByIdAsync(TKey id)
		=> await _entities.FindAsync(id)!;

		public async Task<IReadOnlyList<TEntity>> GetWithPrdicate(Expression<Func<TEntity, bool>> pridecate)
		=> await _entities.Where(pridecate).ToListAsync();

		public void Remove(TEntity entity)
		=> _entities.Remove(entity);

		public void RemoveRange(IEnumerable<TEntity> entities)
		=> _entities.RemoveRange(entities);

		public void Update(TEntity entity)
			=>_entities.Update(entity);

	}

}
