using UnityEngine;
namespace CSVLogger.Fields.Transforms
{
    public sealed class TransformRotationField : TransformField
    {

        public TransformRotationField() : base()
        {
            header = "Rotation";
        }

        protected override string Header
        {
            get
            {
                if(rotationOption == RotationOption.Euler)
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
                if(rotationOption == RotationOption.Euler)
                {
                    // Euler angle returns in degree
                    Vector3 rot = transform.eulerAngles;

                    if (eulerRepresentationOption == EulerRepresentationOption.Radian)
                        rot *= Mathf.Deg2Rad;

                    return $"{rot.x},{rot.y},{rot.z}";
                }
                else
                {
                    return $"{transform.rotation.x},{transform.rotation.y},{transform.rotation.z},{transform.rotation.w}";
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
