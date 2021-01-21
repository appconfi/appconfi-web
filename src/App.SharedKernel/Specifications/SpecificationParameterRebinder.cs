using System.Linq.Expressions;

namespace App.SharedKernel.Specifications
{
    internal class SpecificationParameterRebinder : ExpressionVisitor
    {
        readonly ParameterExpression specificationParameter;

        SpecificationParameterRebinder(ParameterExpression specificationParameter)
        {
            this.specificationParameter = specificationParameter;
        }

        public static Expression ReplaceParameter(Expression expression, ParameterExpression parameter)
        {
            return new SpecificationParameterRebinder(parameter).Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return p.Type == specificationParameter.Type ? base.VisitParameter(specificationParameter) : base.VisitParameter(p);
        }
    }
}
