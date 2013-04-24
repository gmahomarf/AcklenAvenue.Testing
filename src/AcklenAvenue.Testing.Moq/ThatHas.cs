using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using Moq;

namespace AcklenAvenue.Testing.Moq
{
    public static class ThatHas
    {
        public static ExpressionComparisonBuilder<T> AnExpressionFor<T>()
        {
            return new ExpressionComparisonBuilder<T>();
        }
    }

    public class ExpressionComparisonBuilder<T>
    {
        readonly List<T> _matching = new List<T>();
        readonly List<T> _notMatching = new List<T>();

        public ExpressionComparisonBuilder<T> ThatMatches(params T[] matching)
        {
            _matching.AddRange(matching);
            return this;
        }

        public ExpressionComparisonBuilder<T> And()
        {
            return this;
        }

        public ExpressionComparisonBuilder<T> ThatDoesNotMatch(params T[] notMatching)
        {
            _notMatching.AddRange(notMatching);
            return this;
        }

        public Expression<Func<T, bool>> Build()
        {
            return Match.Create<Expression<Func<T, bool>>>(
                actualExpression =>
                    {
                        _matching.ForEach(m =>
                                              {
                                                  if (actualExpression.Compile()(m))
                                                      return;

                                                  var serializer = new JavaScriptSerializer();
                                                  string json = serializer.Serialize(m);
                                                  throw new Exception(
                                                      "The expression passed in from the production code did not match the required object: " +
                                                      json);
                                              });

                        _notMatching.ForEach(m =>
                                                 {
                                                     if (!actualExpression.Compile()(m))
                                                         return;

                                                     var serializer = new JavaScriptSerializer();
                                                     string json = serializer.Serialize(m);
                                                     throw new Exception(
                                                         "The expression passed in from the production code matched an object that it shouldn't have: " +
                                                         json);
                                                 });

                        return true;
                    });
        }
    }
}