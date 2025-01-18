namespace CSVLogger.Fields.RigidBodies
{
    public sealed class RigidBodyVelocityField : RigidBodyField
    {
        public RigidBodyVelocityField()
        {
            header = "Velocity";
        }

        protected override string Header
        {
            get => $"\"{header}.x\",\"{header}.y\",\"{header}.z\"";
        }

        protected override string Data
        {
            get => $"{rigidBody.velocity.x},{rigidBody.velocity.y},{rigidBody.velocity.z}";
        }
    }
}
