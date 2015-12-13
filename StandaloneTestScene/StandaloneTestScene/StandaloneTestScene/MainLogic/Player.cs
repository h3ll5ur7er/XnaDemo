using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public static class Player
    {
        public static Inventory Inventory { get; private set; }

        public static void SetUp()
        {
            Inventory = new Inventory();
        }

        private static void RenderInventory(SpriteBatch batch)
        {
            Inventory.Render(batch);
        }

        public static void RenderHUD(SpriteBatch hudBatch)
        {
            RenderInventory(hudBatch);
        }
    }
}