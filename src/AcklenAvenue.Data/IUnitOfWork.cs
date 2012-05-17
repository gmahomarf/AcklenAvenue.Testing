using System;

namespace AcklenAvenue.Data
{
    public interface IUnitOfWork<out TContext>
    {
        void Commit(Action<TContext> action);
        T Commit<T>(Func<TContext, T> func);
    }
}