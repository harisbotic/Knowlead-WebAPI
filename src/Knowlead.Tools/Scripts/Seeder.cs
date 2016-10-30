using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Knowlead.DAL;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Knowlead.Tools {
public interface ISeeder
{
    void ImportSingleRow(JObject row);
    void ClearTable();
    void SaveChanges();
    void Validate();
    int Same { get; set; }
    int Updated { get; set; }
    int New { get; set; }
}

public class Seeder<T> : ISeeder where T : class 
{
    public int Same { get; set; }
    public int Updated { get; set; }
    public int New { get; set; }
    ApplicationDbContext _context;
    DbSet<T> _dbset;
    SeedScript.ImportConfig config;
    bool verbose;
    public Seeder(ApplicationDbContext context, SeedScript.ImportConfig importConfig, bool verbose)
    {
        this.config = importConfig;
        _context = context;
        _dbset = _context.Set<T>();
        Mapper.Initialize(config => {
            config.CreateMap<T, T>();
            config.ShouldMapProperty = p =>
                p.GetCustomAttribute(typeof(KeyAttribute)) == null &&
                Utils.IsSimple(p.PropertyType);
        });
        this.verbose = verbose;
    }
    public void ClearTable()
    {
        if (verbose)
            Console.WriteLine("Warning: Clearing table ...");
        _dbset.RemoveRange(_dbset.ToList());
        if (verbose)
            Console.WriteLine("OK");
    }

    public void ImportSingleRow(JObject row)
    {
        if (verbose)
            Console.WriteLine("Debug: Importing row: " + row.ToString(Formatting.None));
        string[] includes = new string[]{};
        if (config.References != null && config.References.Length > 0)
        {
            foreach (var reference in config.References)
            {
                if (row[reference.ToProperty] == null)
                {
                    continue;
                }
                Type referenceType = SeedScript.models[reference.SearchModel];
                if (referenceType == null)
                {
                    Console.WriteLine("Error: Invalid reference: model '" + reference.SearchModel + "' doesn't exist");
                    return;
                }
                Object db = _context.GetType().GetMethod("Set").MakeGenericMethod(referenceType).Invoke(_context, new object[] {});
                Object where = typeof(Seeder<T>)
                    .GetMethod("GetWhereClauseGeneric")
                    .MakeGenericMethod(referenceType)
                    .Invoke(this, new object[] {reference.SearchProperty, row[reference.ToProperty].ToString()});
                MethodInfo mi = typeof(System.Linq.Queryable)
                    .GetMethods()
                    .First((m) => m.Name == "Where")
                    .MakeGenericMethod(referenceType);
                Object queryable = mi
                    .Invoke(null, new object[] {db, where});
                Object[] objs = (Object[])typeof(System.Linq.Enumerable)
                    .GetMethod("ToArray")
                    .MakeGenericMethod(referenceType)
                    .Invoke(null, new object[] {queryable});
                String expression = reference.SearchModel + "." + reference.FromProperty + " == '" + row[reference.ToProperty].ToString() + "'";
                if (objs.Length == 0)
                {
                    Console.WriteLine("Error: No current data matches expression " + expression);
                    return;
                } else if (objs.Length > 1)
                {
                    Console.WriteLine("Error: More then 1 value match expression " + expression);
                    return;
                }
                PropertyInfo prop = referenceType.GetProperty(reference.FromProperty);
                if (prop == null)
                {
                    Console.WriteLine("Error: Model " + reference.SearchModel + " doesn't contain property " + reference.FromProperty);
                    return;
                }
                if (verbose)
                    Console.WriteLine(row[reference.ToProperty].ToString() + " -> " + prop.GetValue(objs[0]) + " (expression: " + expression + ")");
                row.GetValue(reference.ToProperty)
                    .Replace(new JValue(prop.GetValue(objs[0])));
            }
        }
        T obj = row.ToObject<T>();
        if (config.Key == null)
        {
            DoImportRow(obj);
        } else
        {
            T[] candidates = _dbset.Where(GetWhereClause(obj, config.Key)).ToArray();
            if (candidates.Length == 0)
            {
                DoImportRow(obj);
            } else if (candidates.Length > 1)
            {
                Console.WriteLine("Error: More then one candidate found for key '" + config.Key + "'");
                return;
            } else
            {
                DoUpdateRow(obj, ref candidates[0]);
            }
        }
    }

    public static Expression<Func<TT, bool>> GetWhereClauseGeneric<TT>(string key, object value)
    {
        var arg = Expression.Parameter(typeof(TT));
        var leftEx = Expression.Property(arg, key);
        var rightEx = Expression.Constant(value);
        Expression clause = Expression.Equal(leftEx, rightEx);
        return Expression.Lambda<Func<TT, bool>>(clause, arg);
    }

    public Expression<Func<T, bool>> GetWhereClause(T obj, string key)
    {
        return GetWhereClauseGeneric<T>(key, obj.GetType().GetProperty(key).GetValue(obj));
    }

    private void DoImportRow(T obj)
    {
        if (verbose)
            Console.WriteLine("Debug: Importing new row");
        _dbset.Add(obj);
        New++;
        if (config.SaveAfterEachRow)
        {
            SaveChanges();
        }
    }

    private void DoUpdateRow(T from, ref T to)
    {
        if (verbose)
            Console.WriteLine("Debug: Updating a row");
        if (from.PublicInstancePropertiesEqual(to))
        {
            Same++;
        } else
        {
            Mapper.Map(from, to, typeof(T), typeof(T));
            Updated++;
        }
        if (config.SaveAfterEachRow)
        {
            SaveChanges();
        }
    }
    public void SaveChanges()
    {
        if (verbose)
            Console.WriteLine("Saving changes ... ");
        _context.SaveChanges();
        if (verbose)
            Console.WriteLine("OK");
    }
    public void Validate()
    {
        HashSet<Object> keys = new HashSet<Object>();
        Object[] values = _dbset.ToArray();
        foreach (var item in values)
        {
            Object keyValue = item.GetType().GetProperty(config.Key).GetValue(item);
            if (keys.Contains(keyValue))
            {
                Console.WriteLine("Error: Model in database contains duplicate keys '" + config.Key + "' = " + keyValue);
            } else {
                keys.Add(keyValue);
            }
        }
    }
}

}