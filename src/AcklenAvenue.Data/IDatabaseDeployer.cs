using System.Collections.Generic;

namespace AcklenAvenue.Data
{
    public interface IDatabaseDeployer
    {
        void Create();
        void Drop();
        void Seed(List<IDataSeeder> seeders);
    }
}