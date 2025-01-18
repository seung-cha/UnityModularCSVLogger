using UnityEngine;

namespace CSVLogger
{
    [CreateAssetMenu(menuName = "CSVLogger/CharValue")]
    public sealed class CharValue : ScriptableObject
    {
        public char Value;
    }
}