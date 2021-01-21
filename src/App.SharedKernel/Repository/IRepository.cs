using App.SharedKernel.Domain;
using App.SharedKernel.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.SharedKernel.Repository
{
    public interface IRepository<T, TKey> where T : Entity<TKey>
    {
        Task<T> FirstOrDefaultAsync(ISpecification<T> filter, string include = null);

        Task<bool> AnyAsync(ISpecification<T> filter);

        Task<T> GetByIdAsync(TKey id);

        void Delete(T entity);

        void Insert(T entity);

        Task<int> CountAsync(ISpecification<T> filter);

        Task<IEnumerable<T>> AllAsync();

        Task<IEnumerable<T>> GetAsync(ISpecification<T> filter, string include = null);

        Task<T> GetById(TKey id, string include = null);
        Task<IEnumerable<T>> GetAsync(ISpecification<T> filter, int pageNumber, int pageSize, System.Func<System.Linq.IQueryable<T>, System.Linq.IOrderedQueryable<T>> orderBy, string include = null);
    }
}
