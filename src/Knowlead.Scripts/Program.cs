using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            if (args[0].ToLower() == "seed")
            {
                SeedScript.Seed(args);
                return;
            } else if (args[0].ToLower() == "generate")
            {
                GenerateScript.Generate(args);
                return;
            }
        }
    }
}