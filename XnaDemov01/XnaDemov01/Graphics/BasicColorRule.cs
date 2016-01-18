using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public class BasicColorRule : IColorRule
    {
        public Color GetColor(float height)
        {
            return Color.LightSlateGray;
        }
    }
}