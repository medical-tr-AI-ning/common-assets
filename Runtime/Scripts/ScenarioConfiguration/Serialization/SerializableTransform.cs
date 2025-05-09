using System;
using UnityEngine;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    /// <summary>
    /// Serializable data format for transforms (position and rotation)
    /// </summary>
    [Serializable]
    public class SerializableTransform
    {
        public float PositionX;
        public float PositionY;
        public float PositionZ;
        public float RotationX;
        public float RotationY;
        public float RotationZ;
        public float RotationW;

        public void ApplyPropertiesToTransform(Transform transform)
        {
            transform.SetPositionAndRotation(new Vector3(PositionX, PositionY, PositionZ),
                new Quaternion(RotationX, RotationY, RotationZ, RotationW));
        }

        public SerializableTransform() {}

        public SerializableTransform(Transform transform)
        {
            var position = transform.position;
            PositionX = position.x;
            PositionY = position.y;
            PositionZ = position.z;
            var rotation = transform.rotation;
            RotationX = rotation.x;
            RotationY = rotation.y;
            RotationZ = rotation.z;
            RotationW = rotation.w;
        }
    }
}