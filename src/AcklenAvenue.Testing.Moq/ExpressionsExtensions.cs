using System;
using System.Linq.Expressions;

namespace AcklenAvenue.Testing.Moq
{
    public static class ExpressionsExtensions
    {
        public static void ShouldMatch<T>(this Expression<Func<T, bool>> actual, Expression<Func<T, bool>> expected)
        {
            if (!ExpressionComparer.AreEqual(expected, actual))
            {
                throw new Exception(
                    "The expressions didn't match but not sure why. Need to add more descriptions the reasons for failure to the ExpressionComparer.");
            }
        }
    }
}