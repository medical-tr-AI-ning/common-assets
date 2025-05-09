using System;
using System.Collections.Generic;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    [Serializable]
    public class PathologyVariant
    {
        public PathologyData Pathology = new PathologyData();
        public AnamnesisData Anamnesis = new AnamnesisData();
        public AgentConfig Agent;

        [Serializable]
        public class AnamnesisData
        {
            public List<AnamnesisQuestion> AnamnesisQuestions = new List<AnamnesisQuestion>();
        }
    }
}