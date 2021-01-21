using App.SharedKernel.Domain;
using App.SharedKernel.Repository;
using App.SharedKernel.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class Repository<T, TKey> : IRepository<T, TKey>
        where T : Entity<TKey>
    {
        DbSet<T> _dbSet;
        DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<bool> AnyAsync(ISpecification<T> filter)
        {
            return await _dbSet.AnyAsync(filter.IsSatisfied());
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> filter, string include = null)
        {
            IQueryable<T> dbSet = PrepareSet(filter, include);
            return await dbSet.FirstOrDefaultAsync();
        }

        private IQueryable<T> PrepareSet(ISpecification<T> filter, string include = null)
        {
            IQueryable<T> dbSet = _dbSet;

            if (!string.IsNullOrEmpty(include))
            {
                foreach (var inc in include.Split(','))
                {
                    dbSet = dbSet.Include(inc);
                };
            }

            return dbSet.Where(filter.IsSatisfied());
        }

        public async Task<IEnumerable<T>> GetAsync(ISpecification<T> filter, string include = null)
        {
            IQueryable<T> dbSet = PrepareSet(filter, include);

            return await dbSet
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(ISpecification<T> filter, int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string include = null)
        {
            IQueryable<T> dbSet = PrepareSet(filter, include);
            dbSet = orderBy(dbSet);
            dbSet = dbSet.Skip((pageNumber) * pageSize).Take(pageSize);
            return await dbSet
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<T> GetById(TKey id, string include = null)
        {
            var dbSet = PrepareSet(new DirectSpecification<T>(x => x.Id.Equals(id)), include);
            return await dbSet.FirstAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> filter)
        {
            var dbSet = PrepareSet(filter);
            return await dbSet.CountAsync();
        }
    }
}
