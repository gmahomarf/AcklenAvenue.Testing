using System;
using System.Linq.Expressions;
using Moq;

namespace AcklenAvenue.Testing.Moq
{
    public static class ThatHas
    {
        public static Expression<Func<T, bool>> AnExpressionLike<T>(Expression<Func<T, bool>> expected)
        {
            return Match.Create<Expression<Func<T, bool>>>(t => ExpressionComparer.AreEqual(expected, t));
        }        
    }
}