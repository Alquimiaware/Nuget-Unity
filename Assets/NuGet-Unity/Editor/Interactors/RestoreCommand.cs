namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public class RestoreCommand : NuGetCommand
    {
        public RestoreCommand(Sources sources)
            : base(sources)
        {
        }

        public void Execute(PackageDependencies dependencies)
        {
            // Clean Temp folder
            string tempDest = this.TempDestDirectory;
            if (Directory.Exists(tempDest))
                Directory.Delete(tempDest, true);
            Directory.CreateDirectory(tempDest);
            // Generate config file in temp position
            string config = Path.Combine(tempDest, "packages.config");
            GenerateConfigFile(config, dependencies);
            var restoreArgs = new RestoreCommandArgs(this.Sources);
            restoreArgs.PathToConfig = config;
            restoreArgs.OutputDirectory = tempDest;
            // Call nuget restore passing the file on output position
            CallNuGet(restoreArgs.ToString());

            // Classify all of them
            // Copy to packages folder ( override if forced )
        }

        private void GenerateConfigFile(string configFullPath, PackageDependencies dependencies)
        {
            string configContent = dependencies.ToXmlString();
            using (var text = new StreamWriter(configFullPath))
            {
                text.Write(configContent);
            }
        }

        public string TempDestDirectory
        {
            get
            {
                return Path.Combine(
                    Application.dataPath,
                    "../Library/PackagesRestore/Temp");
            }
        }
    }
}