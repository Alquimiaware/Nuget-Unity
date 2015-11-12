namespace Alquimiaware.NuGetUnity
{
    public interface IFolderCommands
    {
        /// <summary>
        /// Deletes a folder and all its contents
        /// </summary>
        /// <param name="path">The folder's path</param>
        void Delete(string path);

        /// <summary>
        /// Determines whether a folder exists or not.
        /// </summary>
        /// <param name="path">The path to the checked folder</param>
        /// <returns>True whenever the folders exists, False otherwise.</returns>
        bool Exists(string path);

        /// <summary>
        /// Create a folder on the given path.
        /// </summary>
        /// <param name="path">The path where the folder will be created.</param>
        void Create(string path);

        /// <summary>
        /// Removes a folder from it's source location and places it on the given dest location.
        /// </summary>
        /// <param name="sourcePath">The source folder path</param>
        /// <param name="destPath">The destination folder path</param>
        void Move(string sourcePath, string destPath);
    }
}