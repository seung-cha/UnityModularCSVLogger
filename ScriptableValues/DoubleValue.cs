using UnityEngine;

namespace CSVLogger
{
    [CreateAssetMenu(menuName = "CSVLogger/DoubleValue")]
    public sealed class DoubleValue : ScriptableObject
    {
        public double Value;
    }
}