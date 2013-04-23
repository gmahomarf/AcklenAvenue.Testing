using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using Moq;

namespace AcklenAvenue.Testing.Moq
{
    public static class ThatHas
    {
        public static Expression<Func<T, bool>> AnExpressionFor<T>(Expression<Func<T, bool>> expected)
        {
            return Match.Create<Expression<Func<T, bool>>>(t => ExpressionComparer.AreEqual(expected, t));
        }

        public static ExpressionComparisonBuilder<T> AnExpressionFor<T>()
        {
            return new ExpressionComparisonBuilder<T>();
        }        
    }

    public class ExpressionComparisonBuilder<T>
    {
        public List<T> Matching = new List<T>();
        public List<T> NotMatching = new List<T>();

        public ExpressionComparisonBuilder<T> ThatMatches(params T[] matching)
        {
            Matching.AddRange(matching);
            return this;
        }

        public ExpressionComparisonBuilder<T> And()
        {
            return this;
        }

        public ExpressionComparisonBuilder<T> ThatDoesNotMatch(params T[] notMatching)
        {
            NotMatching.AddRange(notMatching);
            return this;
        }

        public Expression<Func<T, bool>> Build()
        {
            return Match.Create<Expression<Func<T, bool>>>(actualExpression =>
                                                               {
                                                                   Matching.ForEach(m=>
                                                                                        {
                                                                                            if (actualExpression.Compile()(m))
                                                                                                return;

                                                                                            var serializer = new JavaScriptSerializer();
                                                                                            var json = serializer.Serialize(m);
                                                                                            throw new Exception(
                                                                                                "The expression passed in from the production code did not match the required object: " +
                                                                                                json);
                                                                                        });

                                                                   NotMatching.ForEach(m=>
                                                                                           {
                                                                                               if (!actualExpression.Compile()(m))
                                                                                                   return;

                                                                                               var serializer = new JavaScriptSerializer();
                                                                                               var json = serializer.Serialize(m);
                                                                                               throw new Exception(
                                                                                                   "The expression passed in from the production code matched an object that it shouldn't have: " +
                                                                                                   json);
                                                                                           });

                                                                   return true;
                                                               });
        }
    }
}