using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class Key : Item
    {
        private RasterizerState rs;

        public override string ID { get; }



        public Key(float x, float z, string id)
        {
            ID = id;
            Position = new Vector3(x*Settings.TILE_WIDTH, 0, z*Settings.TILE_HEIGHT);

            BB = new BoundingBox(Position+new Vector3(-0.3f), Position+new Vector3(0.3f));
            rs = new RasterizerState
            { 
                
            };
        }

        public override void Render(Matrix world)
        {
            var rsTemp = GameController.GDevice.RasterizerState;
            Assets.Models.ItemModel.Draw(world*hovering*rotating*Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up), GetTexture(ID));
        }

        private Texture2D GetTexture(string id)
        {
            switch (id)
            {
                case Level.RED_KEY_ID:
                    return Assets.Textures.RedKeyTexture;
                case Level.GREEN_KEY_ID:
                    return Assets.Textures.GreenKeyTexture;
                case Level.BLUE_KEY_ID:
                    return Assets.Textures.BlueKeyTexture;
                default:
                    return null;
            }
        }
    }
}