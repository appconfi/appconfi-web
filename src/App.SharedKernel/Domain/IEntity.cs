using System;

namespace App.SharedKernel.Domain
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }

    public class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public class Entity : Entity<Guid>
    {
    }

    public interface ICreatedAt
    {
        DateTime CreatedAt { get; set; }
    }
}