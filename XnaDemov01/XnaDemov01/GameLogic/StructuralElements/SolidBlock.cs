using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public class SolidBlock : StructureElementBase
    {

        public SolidBlock(int x, int z)
        {
            Position = new Vector3(x * Settings.TILE_WIDTH, 0, z * Settings.TILE_HEIGHT);
            BB = new BoundingBox(Position+new Vector3(-0.5f), Position+new Vector3(0.5f));
        }

        public override void Update()
        {
            
        }
         
        public override void Render(Matrix world) => Assets.Models.TexturedCube.Draw(world * Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up) * Matrix.CreateScale(Settings.TILE_WIDTH, 1, Settings.TILE_HEIGHT), Assets.Textures.WallTexture);
    }
}