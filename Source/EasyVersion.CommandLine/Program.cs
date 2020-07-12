using CommandLine;
using EasyVersion.Core;
using EasyVersion.HookHandlers;
using System;

namespace EasyVersion.CommandLine
{
    class Program
    {
        public class Options
        {
            [Option('c', "command", Required = true, HelpText = "Type of command (<IncrementVersion> or <HookHandle>)")]
            public string CommandType { get; set; }

            [Option('d', "directory", Required = true, HelpText = "Solution directory path")]
            public string Directory { get; set; }

            [Option('i', "increment", HelpText = "Incrementation style (major, minor or patch)")]
            public string IncrementType { get; set; }

            [Option('h', "hook", HelpText = "Processed git-hook name (<post-merge> or <pre-commit>)")]
            public string HookName { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
               .WithParsed<Options>(o =>
               {
                   if (o.CommandType == "IncrementVersion")
                   {
                       switch (o.IncrementType)
                       {
                           case "major": IncrementVersion(o.Directory, IncrementType.IncrementMajor); break;
                           case "minor": IncrementVersion(o.Directory, IncrementType.IncrementMinor); break;
                           case "patch": IncrementVersion(o.Directory, IncrementType.IncrementPatch); break;
                           default: Console.WriteLine("Unknown Increment Type"); break;
                       }
                   }
                   else if (o.CommandType == "HookHandle")
                   {
                       switch (o.HookName)
                       {
                           case "post-merge": MasterMerge_PostMergeHookHandler.Run(o.Directory); break;
                           case "pre-commit": DevelopCommit_PreCommitHookHandler.Run(o.Directory); break;
                           default: Console.WriteLine("Unknown Hook Name"); break;
                       }
                   }
                   else
                   {
                       Console.WriteLine("Unknown Command Type"); 
                   }
               });            
        }

        static void IncrementVersion(string dir, IncrementType incrementType)
        {
            var mng = Factory.AssemblyInfo.GetManager();
            mng.IncrementVersions(dir, incrementType);
        }
    }
}
