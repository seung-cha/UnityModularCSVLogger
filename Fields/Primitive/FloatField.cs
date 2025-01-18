namespace CSVLogger.Fields.Primitives
{
    public sealed class FloatField : PrimitiveField<FloatValue>
    {
        public FloatField() : base()
        {
            header = "Float Value";
        }

        protected override string Data
        {
            get => $"{reference.Value}";
        }
    }
}