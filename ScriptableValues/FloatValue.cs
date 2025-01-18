using UnityEngine;

namespace CSVLogger
{
    [CreateAssetMenu(menuName = "CSVLogger/FloatValue")]
    public sealed class FloatValue : ScriptableObject
    {
        public float Value;
    }
}