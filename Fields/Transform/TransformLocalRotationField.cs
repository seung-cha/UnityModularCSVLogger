using CSVLogger.Fields.Transforms;
using UnityEngine;

namespace CSVLogger.Fields
{
    public sealed class TransformLocalRotationField : TransformField
    {
        public TransformLocalRotationField() : base()
        {
            header = "Local Rotation";
        }

        protected override string Header
        {
            get
            {
                if (rotationOption == RotationOption.Euler)
                {
                    return $"\"{header}.x\",\"{header}.y\",\"{header}.z\"";
                }
                else
                {
                    return $"\"{header}.x\",\"{header}.y\",\"{header}.z\",\"{header}.w\"";
                }
            }
        }

        protected override string Data
        {
            get
            {
                if (rotationOption == RotationOption.Euler)
                {
                    return $"{transform.localEulerAngles.x},{transform.localEulerAngles.y},{transform.localEulerAngles.z}";
                }
                else
                {
                    return $"{transform.localRotation.x},{transform.localRotation.y},{transform.localRotation.z},{transform.localRotation.w}";
                }
            }

        }

        [SerializeField]
        private RotationOption rotationOption;

        [Tooltip("This field is not used if Rotation Option is not Euler")]
        [SerializeField]
        private EulerRepresentationOption eulerRepresentationOption;
    }
}
