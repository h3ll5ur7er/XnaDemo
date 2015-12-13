using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public static class Vector3Extentions
    {
        public static Vector3 Normalize(this Vector3 vec)
        {
            vec.Normalize();
            return vec;
        }
        public static Vector3 Cross(this Vector3 v1, Vector3 v2)
        {
            return Vector3.Cross(v1, v2);
        }
    }
}