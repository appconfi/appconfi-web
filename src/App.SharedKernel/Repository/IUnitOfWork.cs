using App.SharedKernel.Domain;
using System.Threading.Tasks;

namespace App.SharedKernel.Repository
{
    public interface IUnitOfWork
    {
        IRepository<T, TKey> Repository<T, TKey>() where T : Entity<TKey>;

        Task SaveAsync();

        void Save();
    }
}
