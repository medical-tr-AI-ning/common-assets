using System;
using System.Collections.Generic;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    [Serializable]
    public class EnvironmentConfig
    {
        public string EnvironmentID;
        public List<ObjectPlacement> ObjectPlacements;
    }
}