using UnityEngine;

namespace CSVLogger
{
    [CreateAssetMenu(menuName = "CSVLogger/StringValue")]
    public sealed class StringValue : ScriptableObject
    {
        public string Value;
    }
}
