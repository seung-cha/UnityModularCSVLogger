namespace CSVLogger.Fields.Primitives
{
    public sealed class Vec2Field : PrimitiveField<Vec2Value>
    {
        public Vec2Field()
        {
            header = "Vec2 Value";
        }

        protected override string Header
        {
            get => $"\"{header}.x\",\"{header}.y\"";
        }

        protected override string Data
        {
            get => $"{reference.Value.x},{reference.Value.y}";
        }
    }
}
