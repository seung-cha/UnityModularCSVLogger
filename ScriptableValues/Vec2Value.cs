using UnityEngine;

namespace CSVLogger
{
    [CreateAssetMenu(menuName = "CSVLogger/Vec2Value")]
    public sealed class Vec2Value : ScriptableObject
    {
        public Vector2 Value;
    }
}