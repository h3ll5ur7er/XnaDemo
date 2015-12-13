using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class CubeModel : Model<VertexPositionColorNormal>
    {
        public CubeModel(Game game)
        {
            Vertices.Add(new VertexPositionColorNormal(new Vector3(-1000,-1000,1000), new Color(0,0,255), Vector3.Up));
            Vertices.Add(new VertexPositionColorNormal(new Vector3(1000,-1000,1000), new Color(255,0,255), Vector3.Up));
            Vertices.Add(new VertexPositionColorNormal(new Vector3(1000,1000,1000), new Color(255,255,255), Vector3.Up));
            Vertices.Add(new VertexPositionColorNormal(new Vector3(-1000,1000,1000), new Color(0,255,255), Vector3.Up));

            Vertices.Add(new VertexPositionColorNormal(new Vector3(-1000,-1000,-1000), new Color(0,0,0), Vector3.Up));
            Vertices.Add(new VertexPositionColorNormal(new Vector3(1000,-1000,-1000), new Color(255,0,0), Vector3.Up));
            Vertices.Add(new VertexPositionColorNormal(new Vector3(1000,1000,-1000), new Color(255,255,0), Vector3.Up));
            Vertices.Add(new VertexPositionColorNormal(new Vector3(-1000,1000,-1000), new Color(0,255,0), Vector3.Up));

            Indices.Add(0); Indices.Add(1); Indices.Add(2);
            Indices.Add(2); Indices.Add(3); Indices.Add(0);

            Indices.Add(3); Indices.Add(2); Indices.Add(6);
            Indices.Add(6); Indices.Add(7); Indices.Add(3);

            Indices.Add(7); Indices.Add(6); Indices.Add(5);
            Indices.Add(5); Indices.Add(4); Indices.Add(7);

            Indices.Add(4); Indices.Add(5); Indices.Add(1);
            Indices.Add(1); Indices.Add(0); Indices.Add(4);

            Indices.Add(4); Indices.Add(0); Indices.Add(3);
            Indices.Add(3); Indices.Add(7); Indices.Add(4);

            Indices.Add(1); Indices.Add(5); Indices.Add(6);
            Indices.Add(6); Indices.Add(2); Indices.Add(1);




            SetUpIndexed(game);
        }

        public void Draw(BasicEffect effect, GraphicsDevice graphicsDevice, Matrix world, ICamera3D cam)
        {
            base.Render(effect, graphicsDevice, world, cam, true,null );
        }
        public void Draw(Effect effect, GraphicsDevice graphicsDevice, Matrix world, ICamera3D cam)
        {
            base.Render(effect, graphicsDevice, world, cam, true,null);
        }
    }
}