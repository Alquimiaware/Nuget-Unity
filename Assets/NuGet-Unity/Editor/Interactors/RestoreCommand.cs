namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public class RestoreCommand : NuGetCommand
    {
        private ClassifyPackages classifyCommand;
        private PackageMover packageMover;

        public RestoreCommand(
            ClassifyPackages classifyCommand ,
            IFolderCommands folderCommands,
            Sources sources)
            : base(sources)
        {
            this.classifyCommand = classifyCommand;
            this.packageMover = new PackageMover(folderCommands);
        }

        public string OutputDirectory { get; internal set; }

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
            var classifiedPackages = classifyCommand.Execute(tempDest);

            // Copy to packages folder ( override if forced )
            packageMover.MovePackagesToOutputFolder(classifiedPackages, OutputDirectory);
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