using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSVLogger.Fields.Transforms
{
    public abstract class TransformField : ItemField
    {
        public TransformField() : base() { }
        public override void Initialise(LoggingItem parent, Logger logger)
        {
            base.Initialise(parent, logger);
            transform = parent.Reference.GetComponent<Transform>();
        }

        protected Transform transform;
    }
}
