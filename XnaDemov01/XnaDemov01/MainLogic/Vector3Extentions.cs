using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public static class VectorExtentions
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
		public static Vector2 LinuxFix(this Vector2 v)
		{
			return new Vector2 (v.Y, v.X);
		}
    }
}