using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public class BasicColorRule2 : IColorRule
    {
        public Color GetColor(float height)
        {
            if(height<0.01f) return Color.Blue;
            if(height<1.0f) return Color.Green;
            if(height<2.0f) return Color.DarkOliveGreen;
            if(height<3.0f) return Color.LightGreen;
            if(height<4.0f) return Color.Gray;
            if(height<5.0f) return Color.LightGray;
            return Color.White;
        }
    }
}