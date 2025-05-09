
using System;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization.Utils
{
    public interface IFileWriter
    {
        public void WriteFile(string targetPath, Object data);
    }
}