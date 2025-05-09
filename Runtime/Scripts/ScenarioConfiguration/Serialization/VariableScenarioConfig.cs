using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    [Serializable]
    public class VariableScenarioConfig
    {
        public string ScenarioName;
        public string DisplayTitle;
        public string Description;
        public DateTime ModificationDate;
        public List<EnvironmentConfig> Environments = new();
        public List<AgentConfig> Agents = new();
        public List<SerializablePathologyVariant> Pathologies = new();
        public List<ScenarioVariant> Variants = new();
        public SerializedDictionary<string, string> ScenarioSpecificSettings = new();
        public bool UseUnifiedAnamnesisData = false;


        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            Formatting = Formatting.Indented
        };

        [Serializable]
        public class ScenarioVariant
        {
            public EnvironmentConfig Environment;
            public SerializablePathologyVariant Pathology;
            public AgentConfig Agent;

            //public string LLMSystemPrompt;
            public float Probability;
        }


        public void PopulateScenarioVariants()
        {
            if (Environments.Count == 0 || Agents.Count == 0)
            {
                Debug.LogWarning("Variable Scenario is incomplete! Cannot populate Variants!");
            }

            EnvironmentConfig defaultEnvironment = Environments[0];
            Variants = new List<ScenarioVariant>();

            //Add new ScenarioVariant for each PathologyVariant in the default agent
            foreach (SerializablePathologyVariant pathologyVariant in Pathologies)
            {
                ScenarioVariant scenarioVariant = new ScenarioVariant
                {
                    Agent = pathologyVariant.Agent,
                    Environment = defaultEnvironment,
                    Pathology = pathologyVariant,
                    Probability = 1.0f
                };
                Variants.Add(scenarioVariant);
            }
        }
        
        public AgentConfig GetDefaultAgent() => Agents[0];
        public EnvironmentConfig GetDefaultEnvironment() => Environments[0];
        public SerializablePathologyVariant GetDefaultPathology() => Pathologies[0];
        
        public override bool Equals(object obj)
        {
            if (obj is VariableScenarioConfig scenarioConfig)
            {
                return this.Serialize() == scenarioConfig.Serialize();
            }

            return false;
        }

        public static bool IsValid(VariableScenarioConfig variableScenarioConfig, out Exception exception)
        {
            if (variableScenarioConfig == null)
            {
                exception = new NullReferenceException("Config Reference is null");
                return false;
            }

            if (string.IsNullOrEmpty(variableScenarioConfig.ScenarioName))
            {
                exception = new ArgumentException("Configuration is missing scenario name!");
                return false;
            }

            if (string.IsNullOrEmpty(variableScenarioConfig.DisplayTitle))
            {
                exception = new ArgumentException("Configuration is missing display title!");
                return false;
            }
            
            if (variableScenarioConfig.ModificationDate == null)
            {
                exception = new ArgumentException("Configuration has no modification date!");
                return false;
            }

            if (variableScenarioConfig.Agents == null || variableScenarioConfig.Agents.Count == 0)
            {
                exception = new ArgumentException("Configuration must have at least one Agent!");
                return false;
            }

            if (variableScenarioConfig.Environments == null || variableScenarioConfig.Environments.Count == 0)
            {
                exception = new ArgumentException("Configuration must have at least one Environment!");
                return false;
            }

            if (variableScenarioConfig.Variants == null || variableScenarioConfig.Variants.Count == 0)
            {
                exception = new ArgumentException("Configuration must have at least one Variant!");
                return false;
            }

            if (variableScenarioConfig.ScenarioSpecificSettings == null)
            {
                exception = new ArgumentException("ScenarioSpecificSettings must be initialized!");
                return false;
            }

            exception = null;
            return true;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, JsonSerializerSettings);
        }

        public static VariableScenarioConfig TryDeserialize(string json)
        {
            VariableScenarioConfig deserializedConfig;
            try
            {
                deserializedConfig =
                    JsonConvert.DeserializeObject<VariableScenarioConfig>(json, JsonSerializerSettings);
            }
            catch (Exception e)
            {
                throw new FormatException(
                    $"Json could not be deserialized with error: {e.Message}. \n Raw JSON: {json}");
            }

            if (!IsValid(deserializedConfig, exception: out Exception validationException))
            {
                throw validationException;
            }

            return deserializedConfig;
        }
    }
}