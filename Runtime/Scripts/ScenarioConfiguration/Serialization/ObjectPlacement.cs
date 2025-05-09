using System;
using UnityEngine;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    /// <summary>
    /// Serializable data format for objects placed in the scene by the user.
    /// </summary>
    [Serializable]
    public class ObjectPlacement
    {
        public string PrefabID;
        public SerializableTransform SerializableTransform;
        
        /// <summary>
        /// Default Constructor for deserialization purposes
        /// </summary>
        public ObjectPlacement()
        {
        }

        public ObjectPlacement(SerializableTransform serializableTransform, string prefabID)
        {
            SerializableTransform = serializableTransform;
            PrefabID = prefabID;
        }

        //Constructor Overload to accept Transforms and convert them to Serializable Transforms
        public ObjectPlacement(Transform objectTransform, string prefabID)
            : this(new SerializableTransform(objectTransform), prefabID)
        {
        }
    }
}