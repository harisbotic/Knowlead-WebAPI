using System;
using System.IO;
using Knowlead.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Knowlead.DomainModel.LookupModels.Geo;
using System.Linq;
using System.Collections.Generic;

namespace Knowlead.Scripts
{
    public static class SeedScript
    {
        public static ISeeder SeederFactory(Type type, ApplicationDbContext context)
        {
            if (type.Name == "City")
            {
                return new Seeder<City>(context);
            } else if (type.Name == "OpenIddictApplication")
            {
                return new Seeder<OpenIddict.OpenIddictApplication<Guid>>(context);
            } else
            {
                throw new ArgumentException(type.Name + " not supported in factory", "type");
            }
        }

        public static Dictionary<string, Type> models = new Dictionary<string, Type>()
        {
            {"City", typeof(City)},
            {"OpenIddictApplication", typeof(OpenIddict.OpenIddictApplication)}
        };
        private class SeedClass
        {
            public string Model { get; set; }
            public JArray Data { get; set; }
            public ImportConfig ImportConfig { get; set; }
        }

        private class ImportConfig
        {
            public string Key { get; set; }
            public bool ClearTable { get; set; }
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
            string key = null;
            if (data.ImportConfig != null)
            {
                if (data.ImportConfig.Key != null)
                {
                    key = data.ImportConfig.Key;
                    if (Array.IndexOf(
                        entityType.GetProperties().Select(prop => prop.Name).ToArray(),
                        data.ImportConfig.Key) == -1)
                    {
                        Console.WriteLine("Error: model '" + data.Model + "' doesn't contain property '" + key + "'");
                        return;
                    }
                }
            }
            Console.WriteLine("Debug: Using class: '" + entityType.Name + "'");
            ApplicationDbContext context = ScriptUtils.InitializeContext();
            ISeeder seeder = SeederFactory(entityType, context);
            if (data.ImportConfig.ClearTable)
            {
                seeder.ClearTable();
                seeder.SaveChanges();
            }
            Console.Write("Importing ... ");
            Console.Out.Flush();
            foreach (JObject row in data.Data)
            {
                seeder.ImportSingleRow(row, key);
            }
            Console.WriteLine("OK");
            seeder.SaveChanges();
        }
    }
}