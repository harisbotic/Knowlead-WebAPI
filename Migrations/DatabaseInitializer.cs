using System;
using System.Threading.Tasks;

namespace Knowlead.Migrations {
    public class DatabaseInitializer : IDatabaseInitializer
    {
        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
    public interface IDatabaseInitializer
    {
        Task Seed();
    }
}