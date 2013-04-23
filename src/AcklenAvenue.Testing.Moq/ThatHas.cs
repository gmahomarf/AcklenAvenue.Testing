using System;
using System.Linq.Expressions;
using Moq;

namespace AcklenAvenue.Testing.Moq
{
    public static class ThatHas
    {
        public static Expression<Func<T, bool>> SimpleExpressionLike<T>(Expression<Func<T, bool>> expr)
        {
            return
                Match.Create<Expression<Func<T, bool>>>(
                    expression => expression.ToString() == expr.ToString());
        }

        public static Expression<Func<T, bool>> AnyPredicateOf<T>()
        {
            return It.IsAny<Expression<Func<T, bool>>>();
        }

        public static Expression<Func<T, bool>> AnyPredicateThatMatches<T>(
            T objectThatShouldReturnTrueWhenPassedToExpression)
        {
            return It.Is<Expression<Func<T, bool>>>(y => y.Compile()(objectThatShouldReturnTrueWhenPassedToExpression));
        }        
    }
}