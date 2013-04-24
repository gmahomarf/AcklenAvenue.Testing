using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Moq;

namespace AcklenAvenue.Testing.Moq
{
    public class FuncComparisonBuilder<T>
    {
        readonly List<T> _matching = new List<T>();
        readonly List<T> _notMatching = new List<T>();

        public FuncComparisonBuilder<T> ThatMatches(params T[] matching)
        {
            _matching.AddRange(matching);
            return this;
        }

        public FuncComparisonBuilder<T> And()
        {
            return this;
        }

        public FuncComparisonBuilder<T> ThatDoesNotMatch(params T[] notMatching)
        {
            _notMatching.AddRange(notMatching);
            return this;
        }

        public Func<T, bool> Build()
        {
            return Match.Create<Func<T, bool>>(
                actualFunc =>
                    {
                        _matching.ForEach(m =>
                                              {
                                                  if (actualFunc(m))
                                                      return;

                                                  var serializer = new JavaScriptSerializer();
                                                  string json = serializer.Serialize(m);
                                                  throw new Exception(
                                                      "The function passed in from the production code did not match the required object: " +
                                                      json);
                                              });

                        _notMatching.ForEach(m =>
                                                 {
                                                     if (!actualFunc(m))
                                                         return;

                                                     var serializer = new JavaScriptSerializer();
                                                     string json = serializer.Serialize(m);
                                                     throw new Exception(
                                                         "The function passed in from the production code matched an object that it shouldn't have: " +
                                                         json);
                                                 });

                        return true;
                    });
        }
    }
}