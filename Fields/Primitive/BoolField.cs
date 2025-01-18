namespace CSVLogger.Fields.Primitives
{

    public sealed class BoolField : PrimitiveField<BoolValue>
    {
        public BoolField() : base()
        {
            header = "Bool Value";
        }

        protected override string Data
        {
            get => $"{reference.Value}";
        }
    }

}