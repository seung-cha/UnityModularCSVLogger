namespace CSVLogger.Fields.Primitives
{
    public sealed class IntField : PrimitiveField<IntValue>
    {
        public IntField() : base()
        {
            header = "Int Value";
        }

        protected override string Data
        {
            get => $"{reference.Value}";
        }
    }
}