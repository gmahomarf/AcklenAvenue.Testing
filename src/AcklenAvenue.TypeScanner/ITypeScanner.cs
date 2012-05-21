using System;
using System.Collections.Generic;

namespace AcklenAvenue.TypeScanner
{
    public interface ITypeScanner<T>
    {
        List<Type> GetTypes();
    }
}