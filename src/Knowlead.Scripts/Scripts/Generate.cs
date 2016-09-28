using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Knowlead.Common;

public static class GenerateScript
{
    public static void Generate(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Error: Missing output folder.");
            Console.WriteLine("Usage: generate output_folder");
            return;
        }

        if (!Directory.Exists(args[1]))
        {
            Console.WriteLine("Path doesn't exist");
            return;
        }
        
        List<Type> types = GetTypesInNamespace(Assembly.Load(new AssemblyName("Knowlead.DTO")), "Knowlead.DTO");
        types.Add(typeof(Constants));
        var generator = new TypeScriptBuilder.TypeScriptGenerator();
        Console.Write("Writing classes: ");
        foreach (Type type in types)
        {
            Console.Write(type.Name + " ");
            generator.AddCSType(type);
        }
        Console.WriteLine();
        Console.WriteLine("-----------------------------------");
        Console.WriteLine(generator.ToString());
    }
    private static List<Type> GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
        Console.WriteLine(assembly.ToString());
        return assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.StartsWith(nameSpace)).ToList();
    }

}