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
                        var passing = true;
                        _matching
                            .ForEach(m =>
                                         {
                                             if (actualFunc(m))
                                                 return;

                                             string message =
                                                 "The function passed in from the production code did not match the required object: ";
                                             ReportWarningToConsole(m, message);
                                             passing = false;
                                         });

                        if (passing)
                        {
                            _notMatching
                                .ForEach(m =>
                                             {
                                                 if (!actualFunc(m))
                                                     return;

                                                 string message =
                                                     "The function passed in from the production code matched an object that it shouldn't have: ";
                                                 ReportWarningToConsole(m, message);
                                                 passing = false;
                                             });
                        }

                        return passing;
                    });
        }

        static void ReportWarningToConsole(T m, string message)
        {
            var serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(m);
            Console.WriteLine(message + json);
        }
    }
}