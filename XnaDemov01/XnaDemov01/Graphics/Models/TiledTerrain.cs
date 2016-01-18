using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class TiledTerrain : Terrain<VertexPositionNormalTexture>
    {
        private readonly float height;
        private readonly bool useDefaultShader;

        public TiledTerrain(int xTiles, int zTiles, float tileWidth, float tileHeight, float height = 0f, bool useDefaultShader = true)
            : base(xTiles, zTiles, tileWidth, tileHeight)
        {
            this.height = height;
            this.useDefaultShader = useDefaultShader;

            //var normal = (positions[1, 0] - positions[0, 0]).Cross(positions[0, 1] - positions[0, 0]);
            var normal = Vector3.Up;

            for (int x = 0; x < xVertices; x++)
            {
                for (int z = 0; z < zVertices; z++)
                {
                    FloorTile(x,z, normal);
                }
            }
            SetUp(GameController.Engine);
        }

        protected override void SetUpGrid()
        {
        }

        protected override void SetUpColors()
        {
        }

        protected override void SetUpNormals()
        {
            
        }

        private void FloorTile(int x, int z, Vector3 normal)
        {
            Vertices.Add(new Vector3(x       * xVertexDistance, height, z       * zVertexDistance), normal, new Vector2(0,0));
            Vertices.Add(new Vector3(x       * xVertexDistance, height, (z + 1) * zVertexDistance), normal, new Vector2(0, 1));
            Vertices.Add(new Vector3((x + 1) * xVertexDistance, height, z       * zVertexDistance), normal, new Vector2(1, 0));

            Vertices.Add(new Vector3((x + 1) * xVertexDistance, height, (z + 1) * zVertexDistance), normal, new Vector2(1, 1));
            Vertices.Add(new Vector3((x + 1) * xVertexDistance, height, z       * zVertexDistance), normal, new Vector2(1, 0));
            Vertices.Add(new Vector3(x       * xVertexDistance, height, (z + 1) * zVertexDistance), normal, new Vector2(0, 1));
        }

        public void Render(Matrix world)
        {
            if(useDefaultShader)
                base.Render(GameController.DefaultShader, GameController.GDevice, world, FirstPersonCamera.Instance, false, Assets.Textures.FloorTexture);
            else
                base.Render(GameController.CustomShader, GameController.GDevice, world, FirstPersonCamera.Instance, false, Assets.Textures.FloorTexture);
        }
    }
}