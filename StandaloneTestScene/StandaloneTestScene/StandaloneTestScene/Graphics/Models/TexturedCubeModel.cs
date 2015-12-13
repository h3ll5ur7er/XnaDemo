using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class TexturedCubeModel : Model<VertexPositionNormalTexture>
    {
        private readonly bool useDefaultShader;

        public TexturedCubeModel(bool useDefaultShader = true)
        {
            this.useDefaultShader = useDefaultShader;
            var p000 = new Vector3(-0.5f, -0.5f, -0.5f);
            var p001 = new Vector3(-0.5f, -0.5f,  0.5f);
            var p010 = new Vector3(-0.5f,  0.5f, -0.5f);
            var p100 = new Vector3( 0.5f, -0.5f, -0.5f);
            var p110 = new Vector3( 0.5f,  0.5f, -0.5f);
            var p011 = new Vector3(-0.5f,  0.5f,  0.5f);
            var p101 = new Vector3( 0.5f, -0.5f,  0.5f);
            var p111 = new Vector3( 0.5f,  0.5f,  0.5f);

            CubeFace(p011, p001, p111, p101);
            CubeFace(p111, p101, p110, p100);
            CubeFace(p110, p100, p010, p000);
            CubeFace(p010, p000, p011, p001);


            SetUp(GameController.Engine);
        }

        private void CubeFace(Vector3 p00, Vector3 p01, Vector3 p10, Vector3 p11)
        {
            var v1 = p01 - p00;
            var v2 = p10 - p00;
            var normal = v1.Cross(v2);


            Vertices.Add(p00, normal, new Vector2(0, 0));
            Vertices.Add(p01, normal, new Vector2(0, 1));
            Vertices.Add(p10, normal, new Vector2(1, 0));

            Vertices.Add(p11, normal, new Vector2(1, 1));
            Vertices.Add(p10, normal, new Vector2(1, 0));
            Vertices.Add(p01, normal, new Vector2(0, 1));
        }

        public void Draw(Matrix world, Texture2D texture)
        {
            if(useDefaultShader)
                base.Render(GameController.DefaultShader, GameController.GDevice, world, FirstPersonCamera.Instance, false, texture);
            else
                base.Render(GameController.CustomShader, GameController.GDevice, world, FirstPersonCamera.Instance, false, texture);
        }

    }
}