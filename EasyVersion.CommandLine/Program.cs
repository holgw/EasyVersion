using CommandLine;
using EasyVersion.Core;
using System;

namespace EasyVersion.CommandLine
{
    class Program
    {
        public class Options
        {
            [Option('d', "directory", Required = true, HelpText = "Solution directory path")]
            public string Directory { get; set; }

            [Option('i', "increment", Required = true, HelpText = "Incrementation stryle (major, minor or patch)")]
            public string IncrementType { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
               .WithParsed<Options>(o =>
               {
                   switch (o.IncrementType)
                   {
                       case "major": ExecutePost(o.Directory, IncrementType.IncrementMajor); break;
                       case "minor": ExecutePost(o.Directory, IncrementType.IncrementMinor); break;
                       case "patch": ExecutePost(o.Directory, IncrementType.IncrementPatch); break;
                       default: Console.WriteLine("Unknown Increment Type"); Console.ReadLine(); break;
                   }
               });            
        }

        static void ExecutePost(string dir, IncrementType incrementType)
        {
            var mng = Factory.AssemblyInfo.GetManager();
            mng.IncrementVersions(dir, incrementType);
        }
    }
}
