using UnityEngine;

namespace CSVLogger
{
    [CreateAssetMenu(menuName = "CSVLogger/Vec3Value")]
    public sealed class Vec3Value : ScriptableObject
    {
        public Vector3 Value;
    }
}