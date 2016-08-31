using System;
using System.IO;
using Knowlead.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Knowlead.DomainModel.LookupModels.Geo;

namespace Knowlead.Scripts
{
    public static class SeedScript
    {
        public static ISeeder SeederFactory(Type type, ApplicationDbContext context)
        {
            if (type.Name == "City")
            {
                return new Seeder<City>(context);
            } else
            {
                throw new ArgumentException(type.Name + " not supported in factory", "type");
            }
        }
        private static readonly string[] AllowedClasses = {"City"};
        private class SeedClass
        {
            public string Model { get; set; }
            public JArray Data { get; set; }
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
            Type entityType = Type.GetType(data.Model);
            if (entityType == null)
            {
                Console.WriteLine("Error: model '" + data.Model + "' not found.");
                return;
            }
            if (!entityType.Namespace.StartsWith("Knowlead.DomainModel"))
            {
                Console.WriteLine("Error: only classes in 'Knowlead.DomainModel' are allowed to be seeded.");
                return;
            }
            if (Array.IndexOf(AllowedClasses, entityType.Name) == -1)
            {
                Console.WriteLine("Error: model '" + data.Model + "' not allowed to be seeded.");
                return;
            }
            Console.WriteLine("Debug: Using class: '" + entityType.Name + "'");
            ApplicationDbContext context = ScriptUtils.InitializeContext();
            if (data.Data == null || data.Data.Count == 0)
            {
                Console.WriteLine("Warning: data for import is empty");
                return;
            }
            ISeeder seeder = SeederFactory(entityType, context);
            seeder.ClearTable();
            Console.Write("Importing ... ");
            Console.Out.Flush();
            foreach (JObject row in data.Data)
            {
                seeder.ImportSingleRow(row);
            }
            Console.WriteLine("OK");
            seeder.SaveChanges();
        }
    }
}