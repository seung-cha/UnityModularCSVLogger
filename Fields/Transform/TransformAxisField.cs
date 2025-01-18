using UnityEngine;

namespace CSVLogger.Fields.Transforms
{
    public sealed class TransformAxisField : TransformField
    {
        public TransformAxisField() : base()
        {
            header = "Axis Vector";
        }

        protected override string Header
        {
            get => $"\"{header}.x\",\"{header}.y\",\"{header}.z\"";
        }

        protected override string Data
        {
            get
            {
                Vector3 axis = Vector3.zero;

                switch (axisOption)
                {
                    case AxisOption.Up:
                        axis = transform.up;
                        break;
                    case AxisOption.Down:
                        axis = -transform.up;
                        break;
                    case AxisOption.Left:
                        axis = -transform.right;
                        break;
                    case AxisOption.Right:
                        axis = transform.right;
                        break;
                    case AxisOption.Forward:
                        axis = transform.forward;
                        break;
                    case AxisOption.Back:
                        axis = -transform.forward;
                        break;
                }

                return $"{axis.x},{axis.y},{axis.z}";
            }
        }

        [SerializeField]
        private AxisOption axisOption;
    }
}
