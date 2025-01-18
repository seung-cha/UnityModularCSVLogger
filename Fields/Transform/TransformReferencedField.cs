using UnityEngine;
namespace CSVLogger.Fields.Transforms
{
    public sealed class TransformReferencedField : TransformField
    {
        public TransformReferencedField() : base()
        {
            header = "Referenced";
        }

        protected override string Header
        {
            get
            {
                if(transformReferenceOption == TransformReferenceOption.Difference ||
                   transformReferenceOption == TransformReferenceOption.DifferenceNormalised)
                {
                    return $"\"{header}.x\",\"{header}.y\",\"{header}.x\",";
                }
                else
                {
                    return base.Header;
                }
            }
        }

        protected override string Data
        {
            get
            {
                Vector3 vec = targetTransform.position - transform.position;
                if (transformReferenceOption == TransformReferenceOption.Difference ||
                   transformReferenceOption == TransformReferenceOption.DifferenceNormalised)
                {

                    if (transformReferenceOption == TransformReferenceOption.DifferenceNormalised)
                        vec.Normalize();

                    if (vectorRepresentationOption == VectorRepresentationOption.Local)
                        vec = transform.InverseTransformDirection(vec);

                    return $"{vec.x},{vec.y},{vec.z}";
                }
                else
                {

                    float val = transformReferenceOption == TransformReferenceOption.DistanceSquare ? vec.sqrMagnitude : vec.magnitude;
                    return $"{val}";
                }
            }
        }

        [SerializeField]
        private Transform targetTransform;

        [SerializeField]
        private TransformReferenceOption transformReferenceOption;

        [SerializeField]
        private VectorRepresentationOption vectorRepresentationOption;
    }
}