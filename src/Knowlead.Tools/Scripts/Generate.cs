using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Knowlead.Common;
using Knowlead.Common.Exceptions;
using TypeScriptBuilder;
using static Knowlead.Common.Constants;

namespace Knowlead.Tools
{
public static class GenerateScript
{
    public static void Generate(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Error: Missing output folder. Default is: '../../../Knowlead-WebClient/src/app/models'");
            Console.WriteLine("Usage: generate output_folder");
            return;
        }

        if (!Directory.Exists(args[1]))
        {
            Console.WriteLine("Path doesn't exist");
            return;
        }

        if (!args[1].EndsWith("/")) {
            args[1] += "/";
        }
        
        List<Type> types = GetTypesInNamespace(Assembly.Load(new AssemblyName("Knowlead.DTO")), "Knowlead.DTO");
        TypeScriptGeneratorOptions options = new TypeScriptGeneratorOptions{
            IgnoreNamespaces = true,
            EmitIinInterface = false
        };
        var generator = new TypeScriptBuilder.TypeScriptGenerator(options);
        generator.ExcludeType(typeof(Profile));
        generator.ExcludeType(typeof(Guid));

        generator.ExcludeType(typeof(FormErrorModel));
        generator.ExcludeType(typeof(ErrorModel));

        foreach (Type type in types)
        {
            generator.AddCSType(type);
        }
        Console.WriteLine("dto.ts");
        generator.Store(args[1] + "dto.ts");
        File.AppendAllLines(args[1] + "dto.ts", new String[] {"export type Guid = string;"});

        generator = new TypeScriptBuilder.TypeScriptGenerator(options);
        generator.AddCSType(typeof(Constants.ErrorCodes));
        generator.AddCSType(typeof(Constants.NotificationTypes));
        generator.AddCSType(typeof(Constants.CallEndReasons));
        generator.AddCSType(typeof(EnumActions.ListP2PsRequest));
        Console.WriteLine("constants.ts");
        generator.Store(args[1] + "constants.ts");
    }
    private static List<Type> GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        return assembly.GetTypes().Where(t =>
            t.Namespace != null &&
            t.Namespace.StartsWith(nameSpace) &&
            !t.Name.Contains("<>") &&
            t.GetTypeInfo().BaseType != typeof(Profile)
        ).ToList();
    }
}

}