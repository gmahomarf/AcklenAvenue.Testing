using System;
using System.Linq.Expressions;

namespace AcklenAvenue.Testing.Moq
{
    public static class ExpressionsExtensions
    {
        public static bool Evaluate<T>(this Expression<Func<T, bool>> expression, T value)
        {
            return expression.Compile()(value);
        }
    }
}