using System;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    [Serializable]
    public class AgentConfig
    {
        public string AgentID;
        public string Name;
        public string Age;
        public string Occupation;
        public string Weight;
        public string Height;
        public bool ShowDetails;
    }
}