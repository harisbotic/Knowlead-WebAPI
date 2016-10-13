using System;
using System.IO;
using Knowlead.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.DomainModel.LookupModels.Core;
using System.Linq;
using System.Collections.Generic;

public static class SeedScript
{
    public static ISeeder SeederFactory(Type type, ApplicationDbContext context, ImportConfig config)
    {
        if (type.Name == "State")
        {
            return new Seeder<State>(context, config);
        }
        else if (type.Name == "OpenIddictApplication")
        {
            return new Seeder<OpenIddict.OpenIddictApplication<Guid>>(context, config);
        }
        else if (type.Name == "Language")
        {
            return new Seeder<Language>(context, config);
        } 
        else
        {
            throw new ArgumentException(type.Name + " not supported in factory", "type");
        }
    }
    //private static readonly string[] AllowedClasses = {"State", "Language"};

    public static Dictionary<string, Type> models = new Dictionary<string, Type>()
    {
        {"State", typeof(State)},
        {"OpenIddictApplication", typeof(OpenIddict.OpenIddictApplication<Guid>)},
        {"Language", typeof(Language)}
    };
    private class SeedClass
    {
        public string Model { get; set; }
        public JArray Data { get; set; }
        public ImportConfig ImportConfig { get; set; }
    }
    public class ReferenceClass
    {
        public string FromProperty { get; set; }
        public string ToProperty { get; set; }
        public string SearchProperty { get; set; }
        public string SearchModel { get; set; }
    }
    public class ImportConfig
    {
        public string Key { get; set; }
        public bool ClearTable { get; set; } = false;
        public ReferenceClass[] References { get; set; }
        public bool SaveAfterEachRow { get; set; } = false;
    }
    public static void Seed(string[] args)
    {
        
        if (args.Length < 2)
        {
            Console.WriteLine("Error: Missing file argument. Usage: seed filename.json");
            return;
        }
        string text = "";
        try {
            text = File.ReadAllText(args[1]);
        } catch (Exception ex)
        {
            Console.WriteLine("Error reading file '" + args[1] + "': " + ex.Message);
            return;
        }
        SeedClass data;
        try
        {
            data = JsonConvert.DeserializeObject<SeedClass>(text);
        } catch (Newtonsoft.Json.JsonSerializationException ex)
        {
            Console.WriteLine("Error deserializing from json: " + ex.Message);
            return;
        }
        if (data.Model == null)
        {
            Console.WriteLine("Error: root object doesn't contain 'table' element.");
            return;
        }
        Type entityType;
        models.TryGetValue(data.Model, out entityType);
        if (entityType == null)
        {
            Console.WriteLine("Error: model '" + data.Model + "' not found.");
            return;
        }
        if (data.Data == null || data.Data.Count == 0)
        {
            Console.WriteLine("Warning: data for import is empty");
            return;
        }
        if (data.ImportConfig != null)
        {
            if (data.ImportConfig.Key != null)
            {
                if (Array.IndexOf(
                    entityType.GetProperties().Select(prop => prop.Name).ToArray(),
                    data.ImportConfig.Key) == -1)
                {
                    Console.WriteLine("Error: model '" + data.Model + "' doesn't contain property '" + data.ImportConfig.Key + "'");
                    return;
                }
            }
        } else
        {
            data.ImportConfig = new ImportConfig();
        }
        Console.WriteLine("Debug: Using class: '" + entityType.Name + "'");
        ApplicationDbContext context = ScriptUtils.InitializeContext();
        ISeeder seeder = SeederFactory(entityType, context, data.ImportConfig);
        if (data.ImportConfig.ClearTable)
        {
            seeder.ClearTable();
            seeder.SaveChanges();
        }
        Console.WriteLine("Importing ... ");
        foreach (JObject row in data.Data)
        {
            seeder.ImportSingleRow(row);
        }
        Console.WriteLine("OK");
        if (!data.ImportConfig.SaveAfterEachRow)
        {
            seeder.SaveChanges();
        }
    }
}