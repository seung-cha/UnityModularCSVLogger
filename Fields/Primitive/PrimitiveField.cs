using UnityEngine;

namespace CSVLogger.Fields.Primitives
{
    public abstract class PrimitiveField<T> : ItemField where T : ScriptableObject
    {
        [SerializeField]
        protected T reference;
    }
}