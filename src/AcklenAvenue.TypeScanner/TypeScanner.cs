using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AcklenAvenue.TypeScanner
{
    public class TypeScanner<T> : ITypeScanner<T>
    {
        public List<Type> GetTypes()
        {
            var assemblies = GetLocalAssemblies();
            var manyTypes = assemblies
                .SelectMany(x => x.GetTypes());

            return manyTypes
                .Where(IsImplementationOfIMessage).ToList();
        }

        static bool IsImplementationOfIMessage(Type x)
        {
            return typeof(T).IsAssignableFrom(x)
                   && x.IsClass;
        }

        static IEnumerable<Assembly> GetLocalAssemblies()
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            string path = new Uri(Path.GetDirectoryName(callingAssembly.CodeBase)).AbsolutePath;

            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.IsDynamic && new Uri(x.CodeBase).AbsolutePath.Contains(path)).ToList();
        }        
    }
}