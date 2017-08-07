namespace Alquimiaware.NuGetUnity
{
    using System.Diagnostics;
    using System.IO;
    using UnityEngine;

    public class NuGetCommand
    {
        private string dataPath;

        public NuGetCommand(Sources sources)
        {
            this.Sources = sources;
            // Can only be called on main thread
            this.dataPath = Application.dataPath;
        }

        protected Sources Sources { get; private set; }

        /// <summary>
        /// Calls nuget, blocks until completion and returns output.
        /// </summary>
        /// <param name="args">nuget.exe args to be passed</param>
        /// <returns>The nuget command result.</returns>
        protected NuGetCommandResult CallNuGet(string args)
        {
            UnityEngine.Debug.Log(args);
            var nugetFullPath = Directory.GetFiles(
                this.dataPath,
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

            bool hasOutput = !string.IsNullOrEmpty(stdOut);
            bool hasErrors = !string.IsNullOrEmpty(stdError);

            if (hasOutput)
                UnityEngine.Debug.Log(stdOut);
            if (hasErrors)
                UnityEngine.Debug.LogError(stdError);

            return new NuGetCommandResult(!hasErrors, stdOut, stdError);
        }
    }
}