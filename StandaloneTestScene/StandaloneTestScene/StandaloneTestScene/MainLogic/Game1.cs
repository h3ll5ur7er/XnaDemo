//using IntegrationTestGame;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class Game1 : Game
    {
        //private Texture2D WallTexture, FloorTexture, KeyTemplate, DoorTemplate; 

        //private Model<VertexPositionNormalTexture> cube;

        //private HeightMapTerrain europe;

        public GraphicsDeviceManager graphics;
        private SpriteBatch mainBatch, hudBatch;
        private RenderTarget2D gameRT, hudRT;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Components.Add(new InputController(this));
            Components.Add(new FirstPersonCamera(this));
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {

            GameController.SetUp(this);

            gameRT = new RenderTarget2D(GraphicsDevice, Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT, false, GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
            hudRT = new RenderTarget2D(GraphicsDevice, Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);

            mainBatch = new SpriteBatch(GraphicsDevice);
            hudBatch = new SpriteBatch(GraphicsDevice);
            //WallTexture = Content.Load<Texture2D>("Textures/wall");
            //FloorTexture = Content.Load<Texture2D>("Textures/floor");
            //KeyTemplate = Content.Load<Texture2D>("key");
            //DoorTemplate = Content.Load<Texture2D>("door");

            //cube = new TexturedCubeModel(this, true);

            //europe = new HeightMapTerrain(this, 128, 128, 1f, 1f, 0, 0.025f, new BasicColorRule2());


            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            GameController.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RenderSceneToRT();
            RenderHUDToRT();

            GraphicsDevice.Clear(Color.Transparent);
            
            mainBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone);

            mainBatch.Draw(hudRT, new Rectangle(0, 0, Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT), null, Color.White, 0f,Vector2.Zero, SpriteEffects.None, 0f);

            mainBatch.Draw(gameRT, new Rectangle(0, 0, Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            
            mainBatch.End();

            //cube.Draw(effect, GraphicsDevice, Matrix.Identity, FirstPersonCamera.Instance);
            //europe.Render(basiceffect, GraphicsDevice, Matrix.Identity, FirstPersonCamera.Instance, null);

            base.Draw(gameTime);
        }

        private void RenderSceneToRT()
        {
            var tempRt = GraphicsDevice.GetRenderTargets();

            GraphicsDevice.SetRenderTarget(gameRT);
            GraphicsDevice.Clear(Color.Transparent);
            

            GraphicsDevice.RasterizerState = new RasterizerState
            {
                FillMode = FillMode.Solid,
                CullMode = CullMode.None
            };
            GameController.Render();

            GraphicsDevice.SetRenderTargets(tempRt);
        }
        private void RenderHUDToRT()
        {
            var tempRt = GraphicsDevice.GetRenderTargets();

            GraphicsDevice.SetRenderTarget(hudRT);
            GraphicsDevice.Clear(Color.Transparent);
            hudBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone);


            
            Player.RenderHUD(hudBatch);

            hudBatch.End();

            GraphicsDevice.SetRenderTargets(tempRt);
        }
    }
}
