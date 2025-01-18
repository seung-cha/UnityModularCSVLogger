namespace CSVLogger.Fields.Primitives
{
    public sealed class StringField : PrimitiveField<StringValue>
    {
        public StringField() : base()
        {
            header = "String Value";
        }

        protected override string Data
        {
            get => $"{reference.Value}";
        }
    }
}
