using System;
using System.Linq;
using Knowlead.DomainModel;
using Knowlead.DomainModel.LookupModels.Geo;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Knowlead.Scripts
{
    public interface ISeeder
    {
        void ImportSingleRow(JObject row);
        void ClearTable();
        void SaveChanges();
    }

    public class Seeder<T> : ISeeder where T : class 
    {
        ApplicationDbContext _context;
        public Seeder(ApplicationDbContext context)
        {
            _context = context;
        }
        public void ClearTable()
        {
            Console.Write("Clearing table ... ");
            Console.Out.Flush();
            GetDbSet().RemoveRange(GetDbSet().ToList());
            Console.WriteLine("OK");
        }

        public void ImportSingleRow(JObject row)
        {
            GetDbSet().Add(row.ToObject<T>());
        }

        private DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }

        public void SaveChanges()
        {
            Console.Write("Saving changes ... ");
            Console.Out.Flush();
            _context.SaveChanges();
            Console.WriteLine("OK");
        }
    }
}