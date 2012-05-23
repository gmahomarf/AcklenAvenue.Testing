using System;
using System.Collections.Generic;

namespace AcklenAvenue.TypeScanner
{
    public interface ITypeScanner
    {
        List<Type> GetTypesOf<T>();
    }
}