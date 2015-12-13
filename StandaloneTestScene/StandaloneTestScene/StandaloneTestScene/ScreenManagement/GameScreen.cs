using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class GameScreen : IScreen
    {
        public Level CurrentLevel { get; private set; }

        public void SetUp()
        {
        }

        public void LoadLevel(Texture2D newLevel)
        {
            CurrentLevel = new Level();
            CurrentLevel.Load(newLevel);
        }

        public void Update()
        {
            CurrentLevel.Update();
        }

        public void Render()
        {
            CurrentLevel.Render();
        }
    }
}