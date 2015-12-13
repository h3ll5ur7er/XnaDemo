using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace StandaloneTestScene
{
    public class Door : StructureElementBase
    {
        private readonly string keyId;

        private readonly bool vertical;
        private bool open;

        public Door(int x, int z, string keyId)
        {
            this.keyId = keyId;
            Position = new Vector3(x * Settings.TILE_WIDTH, 0, z * Settings.TILE_HEIGHT);
            BB = new BoundingBox(Position+new Vector3(-0.5f), Position+new Vector3(0.5f));
            if (Screens.GameScreen.CurrentLevel[x, z + 1] == Level.WALL)
                vertical = true;
            if (Screens.GameScreen.CurrentLevel[x + 1, z] == Level.WALL)
                vertical = false;
            //vertical =
            //    Screens.GameScreen.CurrentLevel.Structure.Count(
            //        y => Vector3.Distance(Position, y.Position) < 1.4 && Math.Abs(y.Position.X - Position.X) > 0.001)
            //    <
            //    Screens.GameScreen.CurrentLevel.Structure.Count(
            //        y => Vector3.Distance(Position, y.Position) < 1.4 && Math.Abs(y.Position.Y - Position.Y) > 0.001);
            
            
        }
        

        public override void Update()
        {
            if(!Player.Inventory[keyId]) return;
            open = true;
            BB = new BoundingBox(Vector3.Zero, Vector3.Zero);
        }
         
        public override void Render(Matrix world)
        {
            if(vertical)
                Assets.Models.DoorModel.Draw(world * Matrix.CreateRotationY(MathHelper.PiOver2)* Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up)  * Matrix.CreateScale(Settings.TILE_WIDTH, 1, Settings.TILE_HEIGHT), GetTexture());
            else
                Assets.Models.DoorModel.Draw(world * Matrix.CreateWorld(Position, Vector3.Forward, Vector3.Up) * Matrix.CreateScale(Settings.TILE_WIDTH, 1, Settings.TILE_HEIGHT), GetTexture());
        }

        private Texture2D GetTexture()
        {
            switch (keyId)
            {
                case Level.RED_KEY_ID:
                    return open? Assets.Textures.RedDoorOpenTexture:Assets.Textures.RedDoorClosedTexture;
                case Level.BLUE_KEY_ID:
                    return open ? Assets.Textures.BlueDoorOpenTexture : Assets.Textures.BlueDoorClosedTexture;
                case Level.GREEN_KEY_ID:
                    return open ? Assets.Textures.GreenDoorOpenTexture : Assets.Textures.GreenDoorClosedTexture;
                default:
                    return null;
            }
        }
    }
}