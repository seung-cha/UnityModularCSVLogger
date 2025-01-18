using UnityEngine;

namespace CSVLogger.Fields.Transforms
{
    public sealed class TransformLocalPositionField : TransformField
    {
        public TransformLocalPositionField() : base()
        {
            header = "Local Position";
        }

        protected override string Header
        {
            get => $"\"{header}.x\",\"{header}.y\",\"{header}.z\"";
        }

        protected override string Data
        {
            get => $"{transform.localPosition.x},{transform.localPosition.y},{transform.localPosition.z}";
        }
    }
}
