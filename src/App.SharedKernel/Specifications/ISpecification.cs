using System;
using System.Linq.Expressions;

namespace App.SharedKernel.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfied();
    }
}
