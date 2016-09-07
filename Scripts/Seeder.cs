using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Knowlead.DomainModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Knowlead.Scripts
{
    public interface ISeeder
    {
        void ImportSingleRow(JObject row, string key);
        void ClearTable();
        void SaveChanges();
    }

    public class Seeder<T> : ISeeder where T : class 
    {
        ApplicationDbContext _context;
        DbSet<T> _dbset;
        public Seeder(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
            Mapper.Initialize(config => {
                config.CreateMap<T, T>();
                config.ShouldMapProperty = p =>
                    p.GetCustomAttribute(typeof(KeyAttribute)) == null &&
                    !p.Name.EndsWith("Id") && !p.Name.EndsWith("ID");
            });
        }
        public void ClearTable()
        {
            Console.Write("Warning: Clearing table ... ");
            Console.Out.Flush();
            _dbset.RemoveRange(_dbset.ToList());
            Console.WriteLine("OK");
        }

        public void ImportSingleRow(JObject row, string key)
        {
            T obj = row.ToObject<T>();
            if (key == null)
            {
                DoImportRow(obj);
            } else
            {
                T[] candidates = _dbset.Where(GetWhereClause(obj, key)).ToArray();
                if (candidates.Length == 0)
                {
                    DoImportRow(obj);
                } else if (candidates.Length > 1)
                {
                    throw new InvalidOperationException("More then one candidate found for key '" + key + "'");
                } else
                {
                    DoUpdateRow(obj, ref candidates[0]);
                }
            }
        }

        private Expression<Func<T, bool>> GetWhereClause(T obj, string key)
        {
            var arg = Expression.Parameter(typeof(T));
            var left = Expression.Property(arg, key);
            var right = Expression.Constant(obj.GetType().GetProperty(key).GetValue(obj));
            Expression clause = Expression.Equal(left, right);
            return Expression.Lambda<Func<T, bool>>(clause, arg);
        }

        private void DoImportRow(T obj)
        {
            _dbset.Add(obj);
        }

        private void DoUpdateRow(T from, ref T to)
        {
            Mapper.Map(from, to, typeof(T), typeof(T));
            /*EntityEntry entry = _context.Entry(to);
            entry.State = EntityState.Modified;*/
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