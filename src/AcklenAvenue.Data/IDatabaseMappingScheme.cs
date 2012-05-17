using System;

namespace AcklenAvenue.Data
{
    public interface IDatabaseMappingScheme<in TTypeOfMappingConfiguration>
    {
        Action<TTypeOfMappingConfiguration> Mappings { get; }
    }
}