namespace CSVLogger.Fields.Transforms
{
    public sealed class TransformPositionField : TransformField
    {
        public TransformPositionField() : base()
        {
            header = "Position";
        }

        protected override string Header
        {
            get => $"\"{header}.x\",\"{header}.y\",\"{header}.z\"";
        }

        protected override string Data
        {
            get => $"{transform.position.x},{transform.position.y},{transform.position.z}";
        }

    }
}