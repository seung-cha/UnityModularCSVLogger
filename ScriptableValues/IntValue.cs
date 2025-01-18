using UnityEngine;

namespace CSVLogger
{
    [CreateAssetMenu(menuName= "CSVLogger/IntValue")]
    public sealed class IntValue : ScriptableObject
    {
        public int Value;
    }
}