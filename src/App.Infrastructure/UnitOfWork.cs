using App.SharedKernel.Domain;
using App.SharedKernel.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        Dictionary<Type, object> _instances;
        public UnitOfWork(DatabaseContext context)
        {
            Context = context;
            _instances = new Dictionary<Type, object>();
        }

        DatabaseContext Context { get; }
        bool _disposed;

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IRepository<T, TKey> Repository<T, TKey>()
            where T : Entity<TKey>
        {
            if (!_instances.ContainsKey(typeof(T)))
            {
                _instances.Add(typeof(T), new Repository<T, TKey>(Context));
            }
            return _instances[typeof(T)] as Repository<T, TKey>;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
