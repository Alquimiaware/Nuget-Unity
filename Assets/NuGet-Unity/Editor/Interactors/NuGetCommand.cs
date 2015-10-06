namespace Alquimiaware.NuGetUnity
{
    using System.Diagnostics;
    using System.IO;
    using UnityEngine;

    public class NuGetCommand
    {
        public NuGetCommand(Sources sources)
        {
            this.Sources = sources;
        }

        protected Sources Sources { get; private set; }

        /// <summary>
        /// Calls nuget, blocks until completion and returns output.
        /// </summary>
        /// <param name="args">nuget.exe args to be passed</param>
        /// <returns>The output or error stream.</returns>
        protected static string CallNuGet(string args)
        {
            UnityEngine.Debug.Log(args);
            var nugetFullPath = Directory.GetFiles(
                Application.dataPath,
                "nuget.exe",
                SearchOption.AllDirectories)[0];

            var startInfo = new ProcessStartInfo(nugetFullPath, args);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            var process = Process.Start(startInfo);
            var stdOut = process.StandardOutput.ReadToEnd();
            var stdError = process.StandardError.ReadToEnd();

            return !string.IsNullOrEmpty(stdOut) ?
                   stdOut :
                   stdError;
        }
    }
}