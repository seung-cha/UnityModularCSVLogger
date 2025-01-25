using UnityEngine;

namespace CSVLogger.Fields.RigidBodies
{
    public sealed class RigidbodyAngularVelocityField : RigidBodyField
    {
        public RigidbodyAngularVelocityField() : base()
        {
            header = "Angular Velocity";
        }

        protected override string Header
        {
            get
            {
                return $"\"{header}.x\",\"{header}.y\",\"{header}.z\"";
            }
        }

        protected override string Data
        {
            get
            {
                // Euler angle returns in degree
                Vector3 rot = rigidBody.angularVelocity;

                if (eulerRepresentationOption == EulerRepresentationOption.Degree)
                    rot *= Mathf.Rad2Deg;

                return $"{rot.x},{rot.y},{rot.z}";
            }
        }

        [SerializeField]
        private EulerRepresentationOption eulerRepresentationOption;
    }
}
