

using UnityEngine;

namespace CSVLogger.Fields.RigidBodies
{
    public abstract class RigidBodyField : ItemField
    {
        public override void Initialise(LoggingItem parent, Logger logger)
        {
            base.Initialise(parent, logger);
            rigidBody = parent.Reference.GetComponent<Rigidbody>();
        }

        protected Rigidbody rigidBody;
    }
}
