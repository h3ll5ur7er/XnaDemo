using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public static class Player
    {
		private static long GameTicks = 0;
        public static Inventory Inventory { get; private set; }

        public static void SetUp()
        {
            Inventory = new Inventory();
        }

        private static void RenderInventory(SpriteBatch batch)
        {
            Inventory.Render(batch);
        }

        public static void RenderHUD(SpriteBatch hudBatch, GameTime gt)
        {
			GameTicks += (long)gt.ElapsedGameTime.TotalMilliseconds;
			var ms = GameTicks % 1000;
			long _s = GameTicks / 1000;
			var s = _s % 60;
			long _m  = _s / 60;
			var m = _m % 60;
			long h = _m / 60;


            RenderInventory(hudBatch);
			hudBatch.DrawString (Assets.Fonts.DebugFont, $"{h:D2}:{m:D2}\'{s:D2}\'\'{ms:D3}\'\'\'", 20*Vector2.One, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }
    }
}