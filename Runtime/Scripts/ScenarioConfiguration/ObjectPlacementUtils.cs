using System.Collections.Generic;
using medicaltraining.assetstore.ScenarioConfiguration;
using UnityEngine;
using ObjectPlacement = medicaltraining.assetstore.ScenarioConfiguration.Serialization.ObjectPlacement;

namespace SceneConfiguration
{
    public static class ObjectPlacementUtils
    {
        public static List<GameObject> InstantiateSceneObjects(List<ObjectPlacement> objectPlacements, List<PrefabMapping> prefabMappings, Transform targetObjectContainer)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (ObjectPlacement objectPlacement in objectPlacements)
            {
                PrefabMapping mapping = prefabMappings.Find(mapping => mapping.prefabID == objectPlacement.PrefabID);
                if (mapping == null)
                {
                    Debug.LogError($"No prefab was assigned for ID {objectPlacement.PrefabID}");
                    break;
                }
                GameObject go = Object.Instantiate(mapping.prefab, targetObjectContainer, true);
                objectPlacement.SerializableTransform.ApplyPropertiesToTransform(go.transform);
                gameObjects.Add(go);
            }

            return gameObjects;
        }
    }
}