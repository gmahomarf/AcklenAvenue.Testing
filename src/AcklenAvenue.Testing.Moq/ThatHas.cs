using System;
using System.Linq.Expressions;
using Moq;

namespace AcklenAvenue.Testing.Moq
{
    public static class ThatHas
    {
        public static Expression<Func<T, bool>> SimpleExpressionLike<T>(Expression<Func<T, bool>> expectedExpression)
        {
            return
                Match.Create<Expression<Func<T, bool>>>(
                    actualExpression =>
                        {
                            if (actualExpression.ToString() != expectedExpression.ToString())
                            {
                                throw new Exception(
                                    string.Format(
                                        "The expression that was passed in from the production code did not match the expression given in the spec/test. \r\nExpected: {0}\r\nActual: {1}\r\n\r\nNOTE: Remember that ThatHas.SimpleExpression compares the .ToString() values of the expressions. This means that the expression must be very simple and must match exactly on both sides (production and spec).",
                                        actualExpression.ToString(), expectedExpression.ToString()));
                            }
                            return true;
                        });
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