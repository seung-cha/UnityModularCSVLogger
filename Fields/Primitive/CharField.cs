namespace CSVLogger.Fields.Primitives
{
    public sealed class CharField : PrimitiveField<CharValue>
    {
        public CharField() : base()
        {
            header = "Char Value";
        }

        protected override string Data
        {
            get => $"{reference.Value}";
        }
    }

}