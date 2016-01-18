using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class Game1 : Game
    {
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
			RenderHUDToRT(gameTime);

            GraphicsDevice.Clear(Color.Transparent);
            
			mainBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone);
			
			mainBatch.Draw(gameRT, new Rectangle(0, 0, Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            mainBatch.Draw(hudRT, new Rectangle(0, 0, Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT), null, Color.White, 0f,Vector2.Zero, SpriteEffects.None, 1f);

            mainBatch.End();

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
		private void RenderHUDToRT(GameTime gt)
        {
            var tempRt = GraphicsDevice.GetRenderTargets();

            GraphicsDevice.SetRenderTarget(hudRT);
            GraphicsDevice.Clear(Color.Transparent);
			hudBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default, RasterizerState.CullNone);

            Player.RenderHUD(hudBatch, gt);

            hudBatch.End();

            GraphicsDevice.SetRenderTargets(tempRt);
        }
    }
}
