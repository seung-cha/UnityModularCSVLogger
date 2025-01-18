namespace CSVLogger.Fields.Primitives
{
    public sealed class DoubleField : PrimitiveField<DoubleValue>
    {
        public DoubleField() : base()
        {
            header = "Double Value";
        }

        protected override string Data
        {
            get => $"{reference.Value}";
        }
    }
}
