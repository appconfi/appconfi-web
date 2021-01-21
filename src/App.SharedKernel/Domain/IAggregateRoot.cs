using System;

namespace App.SharedKernel.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; set; }
    }
}
