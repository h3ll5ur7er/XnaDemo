using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class Inventory
    {
        private List<IItemEntity> entities;

        public bool this[string id]
        {
            get { return entities.Any(x => x.ID == id); }
        }
        public Inventory()
        {
            Clear();
        }

        public void Clear()
        {
            entities = new List<IItemEntity>();
        }

        public void PickUp(IItemEntity entity)
        {
            entities.Add(entity);
            Screens.GameScreen.CurrentLevel.Entities.Remove(entity);
        }

        public void Render(SpriteBatch batch)
        {
            RenderKey(batch, 1, Level.RED_KEY_ID);
            RenderKey(batch, 2, Level.GREEN_KEY_ID);
            RenderKey(batch, 3, Level.BLUE_KEY_ID);
        }

        private void RenderKey(SpriteBatch batch, int slot, string id)
        {
            if (this[id])
                batch.Draw(Assets.Textures.KeyTexture, new Rectangle(50, (slot+1)*150, 100, 100), null, Level.KEY_COLORS[id], 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}