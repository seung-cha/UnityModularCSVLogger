namespace CSVLogger.Fields.Primitives
{
    public sealed class Vec3Field : PrimitiveField<Vec3Value>
    {
        public Vec3Field()
        {
            header = "Vec3 Value";
        }

        protected override string Header
        {
            get => $"\"{header}.x\",\"{header}.y\",\"{header}.z\"";
        }

        protected override string Data
        {
            get => $"{reference.Value.x},{reference.Value.y},{reference.Value.z}";
        }
    }
}