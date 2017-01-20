using System;
using System.IO;
using Knowlead.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Knowlead.DomainModel.LookupModels.Geo;
using Knowlead.DomainModel.LookupModels.Core;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using static Knowlead.Common.Constants;

namespace Knowlead.Tools
{
    public static class SeedScript
    {
        public static ISeeder SeederFactory(Type type, ApplicationDbContext context, ImportConfig config, bool verbose)
        {
            if (type == typeof(State))
            {
                return new Seeder<State>(context, config, verbose);
            }
            else if (type == typeof(Country))
            {
                return new Seeder<Country>(context, config, verbose);
            }
            else if (type == typeof(OpenIddict.Models.OpenIddictApplication<Guid>))
            {
                return new Seeder<OpenIddict.Models.OpenIddictApplication<Guid>>(context, config, verbose);
            }
            else if (type == typeof(Language))
            {
                return new Seeder<Language>(context, config, verbose);
            }
            else if (type == typeof(FOS))
            {
                return new Seeder<FOS>(context, config, verbose);
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
        {"OpenIddictApplication", typeof(OpenIddict.Models.OpenIddictApplication<Guid>)},
        {"Language", typeof(Language)},
        {"FOS", typeof(FOS)},
        {"Country", typeof(Country)}
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

        private static ApplicationDbContext context;
        public static void Seed(string[] args)
        {
            Console.WriteLine("Seeding: " + args[1]);
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Missing file argument. Usage: seed filename.json");
                return;
            }
            bool verbose = args.Contains("--verbose");
            if (Directory.Exists(args[1]))
            {
                IEnumerable<string> targets = Directory.GetFiles(args[1]).Concat(Directory.GetDirectories(args[1]));
                foreach (var target in targets)
                {
                    Seed(new String[] { args[0], target });
                }
                return;
            }
            string text = "";
            try
            {
                text = File.ReadAllText(args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file '" + args[1] + "': " + ex.Message);
                return;
            }
            SeedClass data;
            try
            {
                data = JsonConvert.DeserializeObject<SeedClass>(text);
            }
            catch (Newtonsoft.Json.JsonSerializationException ex)
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
            }
            else
            {
                data.ImportConfig = new ImportConfig();
            }
            if (verbose)
                Console.WriteLine("Debug: Using class: '" + entityType.Name + "'");
            if (context == null)
                context = ScriptUtils.InitializeContext();
            
            /** Seeding roles and roleclaims **/
            Console.WriteLine("Seeding roles...");
            var roles = GetConstants(typeof(UserRoles));
            foreach (var role in roles)
            {
                var exists = context.Roles.Where(x => x.Name.Equals(role)).Count() > 0;
                if(!exists)
                {
                    var r = new IdentityRole<Guid>(role);
                    r.NormalizedName = role.Normalize();
                    context.Roles.Add(r);
                }
            }
            context.SaveChanges();
            Console.WriteLine("Done seeding roles");
            /** Done seeding roles **/

            ISeeder seeder = SeederFactory(entityType, context, data.ImportConfig, verbose);
            if (data.ImportConfig.ClearTable)
            {
                seeder.ClearTable();
                seeder.SaveChanges();
            }
            if (verbose)
                Console.WriteLine("Importing ... ");
            foreach (JObject row in data.Data)
            {
                seeder.ImportSingleRow(row);
            }
            if (!data.ImportConfig.SaveAfterEachRow)
            {
                seeder.SaveChanges();
            }
            if (data.ImportConfig.Key != null)
            {
                if (verbose)
                    Console.WriteLine("Validating ...");
                seeder.Validate();
            }
            Console.Write($"New: {seeder.New}\t");
            Console.Write($"Same: {seeder.Same}\t");
            Console.WriteLine($"Updated: {seeder.Updated}");
        }

        private static List<String> GetConstants(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public |
                BindingFlags.Static | BindingFlags.FlattenHierarchy);

           return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(x => x.GetRawConstantValue().ToString()).ToList().Distinct().ToList();
        }
    }
}