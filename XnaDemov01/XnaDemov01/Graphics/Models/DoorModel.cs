using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class DoorModel : Model<VertexPositionNormalTexture>
    {
        private readonly bool useDefaultShader;

        public DoorModel(bool useDefaultShader = true)
        {
            this.useDefaultShader = useDefaultShader;
            Vertices.Add(new Vector3(-0.5f, -0.5f, 0), Vector3.Backward, new Vector2(0, 1));
            Vertices.Add(new Vector3(-0.5f,  0.5f, 0), Vector3.Backward, new Vector2(0, 0));
            Vertices.Add(new Vector3( 0.5f, -0.5f, 0), Vector3.Backward, new Vector2(1, 1));

            Vertices.Add(new Vector3( 0.5f,  0.5f, 0), Vector3.Backward, new Vector2(1, 0));
            Vertices.Add(new Vector3( 0.5f, -0.5f, 0), Vector3.Backward, new Vector2(1, 1));
            Vertices.Add(new Vector3(-0.5f,  0.5f, 0), Vector3.Backward, new Vector2(0, 0));

            Vertices.Add(new Vector3(-0.5f, -0.5f, 0), Vector3.Forward, new Vector2(0, 1));
            Vertices.Add(new Vector3(0.5f, -0.5f, 0), Vector3.Forward, new Vector2(1, 1));
            Vertices.Add(new Vector3(-0.5f, 0.5f, 0), Vector3.Forward, new Vector2(0, 0)); 

            Vertices.Add(new Vector3(0.5f, 0.5f, 0), Vector3.Forward, new Vector2(1, 0));
            Vertices.Add(new Vector3(-0.5f,  0.5f, 0), Vector3.Forward, new Vector2(0, 0));
            Vertices.Add(new Vector3(0.5f, -0.5f, 0), Vector3.Forward, new Vector2(1, 1));
            SetUp(GameController.Engine);
        }
        public void Draw(Matrix world, Texture2D texture)
        {
            if (useDefaultShader)
                base.Render(GameController.DefaultShader, GameController.GDevice, world, FirstPersonCamera.Instance, false, texture);
            else
                base.Render(GameController.CustomShader, GameController.GDevice, world, FirstPersonCamera.Instance, false, texture);
        }
    }
}