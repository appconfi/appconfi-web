using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.SharedKernel.Specifications
{
    public class And<T> : ISpecification<T>
    {
        public And(ISpecification<T> left, ISpecification<T> right)
        {
            Right = right;
            Left = left;
        }

        public ISpecification<T> Left { get; }

        public ISpecification<T> Right { get; }

        public Expression<Func<T, bool>> IsSatisfied()
        {
            var leftExpression = Left.IsSatisfied();
            var rightExpression = Right.IsSatisfied();

            var parameter = leftExpression.Parameters.Single();
            var body = Expression.AndAlso(leftExpression.Body, SpecificationParameterRebinder.ReplaceParameter(rightExpression.Body, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

    public class Or<T> : ISpecification<T>
    {
        public Or(ISpecification<T> left, ISpecification<T> right)
        {
            Right = right;
            Left = left;
        }

        public ISpecification<T> Left { get; }

        public ISpecification<T> Right { get; }

        public Expression<Func<T, bool>> IsSatisfied()
        {
            var leftExpression = Left.IsSatisfied();
            var rightExpression = Right.IsSatisfied();

            var parameter = leftExpression.Parameters.Single();
            var body = Expression.OrElse(leftExpression.Body, SpecificationParameterRebinder.ReplaceParameter(rightExpression.Body, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

    public class Negation<T> : ISpecification<T>
    {
        public Negation(ISpecification<T> specification)
        {
            Specification = specification;
        }

        public ISpecification<T> Specification { get; }

        public Expression<Func<T, bool>> IsSatisfied()
        {
            var expression = Specification.IsSatisfied();
            var parameter = expression.Parameters[0];
            var body = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

    public class DirectSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; }

        public DirectSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<T, bool>> IsSatisfied()
        {
            return Expression;
        }
    }

    public static class SpecificationExtension
    {
        public static ISpecification<T> And<T>(this ISpecification<T> specification, ISpecification<T> right)
        {
            return new And<T>(specification, right);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> specification, ISpecification<T> right)
        {
            return new Or<T>(specification, right);
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> specification)
        {
            return new Negation<T>(specification);
        }
    }
}
