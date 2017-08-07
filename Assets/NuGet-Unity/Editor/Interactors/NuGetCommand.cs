namespace Alquimiaware.NuGetUnity
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;
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

        public Verbosity OutputVerbosity { get; set; }
        protected Sources Sources { get; private set; }

        /// <summary>
        /// Calls nuget, blocks until completion and returns output.
        /// </summary>
        /// <param name="args">nuget.exe args to be passed</param>
        /// <returns>The nuget command result.</returns>
        protected NuGetCommandResult CallNuGet(string args)
        {
            args += string.Format(" -Verbosity {0}", this.OutputVerbosity);
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
            StringBuilder stdOutBuilder = new StringBuilder();
            StringBuilder stdErrorBuilder = new StringBuilder();

            var process = new Process();
            process.StartInfo = startInfo;

            using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
            using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
            {
                process.OutputDataReceived += (s, e) =>
                {
                    if (e.Data == null)
                        outputWaitHandle.Set();
                    else
                        stdOutBuilder.AppendLine(e.Data);
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (e.Data == null)
                        errorWaitHandle.Set();
                    else
                        stdErrorBuilder.AppendLine(e.Data);
                };

                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                process.WaitForExit();
                outputWaitHandle.WaitOne();
                errorWaitHandle.WaitOne();
            }

            string stdOut = stdOutBuilder.ToString();
            string stdError = stdErrorBuilder.ToString();

            bool hasOutput = !string.IsNullOrEmpty(stdOut);
            bool hasErrors = !string.IsNullOrEmpty(stdError);

            if (hasOutput)
                UnityEngine.Debug.Log(stdOut);
            if (hasErrors)
                UnityEngine.Debug.LogError(stdError);

            return new NuGetCommandResult(!hasErrors, stdOut, stdError);
        }

        public enum Verbosity
        {
            Normal,
            Quiet,
            Detailed
        }
    }
}