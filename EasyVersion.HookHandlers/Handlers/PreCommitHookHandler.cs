using EasyVersion.Core;
using LibGit2Sharp;
using System;
using System.Linq;

namespace EasyVersion.HookHandlers
{
    /// <summary>
    /// Инкрементирует значение PATCH в версии перед коммитом в develop
    /// </summary>
    public class DevelopCommit_PreCommitHookHandler
    {
        public static void Run(string repoDir)
        {
            var repo = new Repository(repoDir);
            string branchName = repo.Head.FriendlyName;
            Console.WriteLine($"Current branch: {branchName}");

            if (repo.Head.FriendlyName != "develop")
            {
                Console.WriteLine($"Current branch is not <develop>. Processing stopped.");
                return;
            }

            var versionMng = Factory.AssemblyInfo.GetManager();
            string[] changedFiles = versionMng.IncrementVersions(repoDir, IncrementType.IncrementPatch);

            // Зафиксируем новые изменения
            if (changedFiles.Any())
                Commands.Stage(repo, changedFiles);

            Console.WriteLine($"Processing finished succesfully");
        }
    }
}
