using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AcklenAvenue.Testing.Moq.Specs
{
    public interface ITestRepository
    {
        IEnumerable<T> Query<T>(Expression<Func<T, bool>> func);
        T FindFirst<T>(Expression<Func<T, bool>> func);

        IEnumerable<T> QueryFunc<T>(Func<T, bool> func);
        T FindFirstFunc<T>(Func<T, bool> func);
    }
}