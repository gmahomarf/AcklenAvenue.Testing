using System;
using System.Linq.Expressions;

namespace AcklenAvenue.Testing.Moq
{
    public static class ShouldExtensions
    {
        public static void ShouldMatch<T>(this Expression<Func<T, bool>> expression, T objectThatShouldMatch)
        {
            var match = expression.Compile()(objectThatShouldMatch);
            if(!match)
            {
                throw new Exception("The expression should have matched the given item, but it failed miserably.");
            }
        }

        public static void ShouldNotMatch<T>(this Expression<Func<T, bool>> expression, T objectThatShouldNotMatch)
        {
            var match = expression.Compile()(objectThatShouldNotMatch);
            if (match)
            {
                throw new Exception("The expression should NOT have matched the given item, but it DID! FAIL!");
            }
        }
    }
}