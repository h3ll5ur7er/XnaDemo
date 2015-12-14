using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public static class GameController
    {
        private static List<Texture2D> levels = new List<Texture2D>();
        private static int currentLevel = 0;

        public static Game1 Engine { get; private set; }
        public static GraphicsDevice GDevice { get; private set; }
        public static GraphicsDeviceManager GDMgr { get; private set; }
        public static ContentManager Content { get; private set; }
        public static IScreen CurrentScreen { get; private set; }
        public static BasicEffect DefaultShader { get; private set; }
        public static Effect SimpleCustomShader { get; private set; }
        public static Effect PhongCustomShader { get; private set; }
        public static Effect CustomShader { get; private set; }
        public static void SetUp(Game1 engine)
        {
            Engine = engine;
            GDevice = engine.GraphicsDevice;
            GDMgr = engine.graphics;
            Content = engine.Content;

            GDMgr.PreferredBackBufferWidth = Settings.SCREEN_WIDTH;
            GDMgr.PreferredBackBufferHeight = Settings.SCREEN_HEIGHT;
            GDMgr.ApplyChanges();

            Assets.LoadAssets();


            DefaultShader = new BasicEffect(GDevice)
            {
                PreferPerPixelLighting = true,
                SpecularPower = 10000f,
                SpecularColor = Vector3.Zero,

                FogEnabled = true,
                FogStart = 2,
                FogEnd = 20,
                FogColor = new Vector3(0x00)
            };

            DefaultShader.EnableDefaultLighting();

            SimpleCustomShader = Content.Load<Effect>("Shaders/SimpleEffect");
            PhongCustomShader = Content.Load<Effect>("Shaders/Phong");

            CustomShader = SimpleCustomShader;

            GDevice.RasterizerState = new RasterizerState
            {
                FillMode = FillMode.WireFrame,
                CullMode = CullMode.None
            };

            levels.Add(Assets.Levels.Level0);
            levels.Add(Assets.Levels.Level3);
            levels.Add(Assets.Levels.Level2);
            levels.Add(Assets.Levels.Level1);

            FirstPersonCamera.Instance.LoadContent();
            Player.SetUp();
            StartGame();
        }
        public static void SetScreen(IScreen screen, bool firstLoad = true)
        {
            if (firstLoad)
                screen.SetUp();

            CurrentScreen = screen;
        }
        public static void Update() => CurrentScreen.Update();
        public static void Render() => CurrentScreen.Render();
        public static void StartGame()
        {
            SetScreen(Screens.GameScreen);
            Screens.GameScreen.LoadLevel(levels[currentLevel]);
        }

        public static void NextLevel()
        {
            currentLevel++;
            Player.Inventory.Clear();
            if (currentLevel >= levels.Count)
            {
                Engine.Exit();
                return;
            }
            Screens.GameScreen.LoadLevel(levels[currentLevel]);

        }
    }
}