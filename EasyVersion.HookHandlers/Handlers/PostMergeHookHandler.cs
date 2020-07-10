using EasyVersion.Core;
using LibGit2Sharp;
using System;
using System.Linq;

namespace EasyVersion.HookHandlers
{
    /// <summary>
    /// Инкрементирует значение MINOR в версии перед слиянием в master
    /// </summary>
    public class MasterMerge_PostMergeHookHandler
    {
        public static void Run(string repoDir)
        {
            var repo = new Repository(repoDir);
            string branchName = repo.Head.FriendlyName;
            Console.WriteLine($"Current branch: {branchName}");

            if (repo.Head.FriendlyName != "master")
            {
                Console.WriteLine($"Current branch is not <master>. Processing stopped.");
                return;
            }

            var versionMng = Factory.AssemblyInfo.GetManager();
            string[] changedFiles = versionMng.IncrementVersions(repoDir, IncrementType.IncrementMinor);

            // Зафиксируем новые изменения
            if (changedFiles.Any())
                Commands.Stage(repo, changedFiles);

            // Закоммитим изменения
            Signature author = new Signature("git-hook", "@none", DateTime.Now);
            Signature committer = author;
            Commit commit = repo.Commit("[AUTO COMMIT] Increment MINOR version", author, committer);

            Console.WriteLine($"Processing finished succesfully");
        }
    }
}
