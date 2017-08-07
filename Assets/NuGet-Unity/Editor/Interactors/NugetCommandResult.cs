namespace Alquimiaware.NuGetUnity
{
    using UnityEngine;
    using System.Collections;

    public class NuGetCommandResult
    {
        public bool Succeeded { get; private set; }
        public string StdOutput { get; private set; }
        public string ErrorOutput { get; private set; }

        public NuGetCommandResult(bool succeeded, string stdOut, string errorOut)
        {
            this.Succeeded = succeeded;
            this.StdOutput = stdOut;
            this.ErrorOutput = errorOut;
        }
    }
}