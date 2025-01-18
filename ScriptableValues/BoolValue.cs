using UnityEngine;

namespace CSVLogger
{
    [CreateAssetMenu(menuName = "CSVLogger/BoolValue")]
    public sealed class BoolValue : ScriptableObject
    {
        public bool Value;
    }
}