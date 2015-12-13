using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public static class GameController
    {
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
                //VertexColorEnabled = true
            };
            DefaultShader.SpecularPower /= 0.05f;

            DefaultShader.EnableDefaultLighting();

            DefaultShader.FogEnabled = true;
            DefaultShader.FogStart = 5;
            DefaultShader.FogEnd = 40;
            DefaultShader.FogColor = new Vector3(0x00);

            SimpleCustomShader = Content.Load<Effect>("Shaders/SimpleEffect");
            PhongCustomShader = Content.Load<Effect>("Shaders/Phong");

            CustomShader = SimpleCustomShader;

            GDevice.RasterizerState = new RasterizerState
            {
                FillMode = FillMode.WireFrame,
                CullMode = CullMode.None
            };

            FirstPersonCamera.Instance.LoadContent();
            Player.SetUp();
            SetScreen(Screens.GameScreen);
            Screens.GameScreen.LoadLevel(Assets.Levels.Level3);
        }

        public static void SetScreen(IScreen screen, bool firstLoad = true)
        {
            if (firstLoad)
                screen.SetUp();

            CurrentScreen = screen;
        }

        public static void Update()
        {
            CurrentScreen.Update();
        }

        public static void Render()
        {
            CurrentScreen.Render();
        }
    }
}