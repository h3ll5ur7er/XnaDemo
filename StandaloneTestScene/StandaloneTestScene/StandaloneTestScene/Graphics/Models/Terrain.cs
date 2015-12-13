using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public abstract class Terrain<T> : Model<T> where T : struct, IVertexType
    {
        protected readonly int xVertices, zVertices;
        protected readonly float xVertexDistance, zVertexDistance;
        
        protected Vector3[,] positions;
        protected Color[,] colors;
        protected Vector3[,] normals;

        protected Terrain(int xVertices, int zVertices, float xVertexDistance, float zVertexDistance)
        {
            this.xVertices = xVertices;
            this.zVertices = zVertices;
            this.xVertexDistance = xVertexDistance;
            this.zVertexDistance = zVertexDistance;

            positions = new Vector3[xVertices, zVertices];
            colors = new Color[xVertices, zVertices];
            normals = new Vector3[xVertices, zVertices];
        }

        protected abstract void SetUpGrid();
        protected abstract void SetUpColors();
        protected abstract void SetUpNormals();


        public void Render(BasicEffect effect, GraphicsDevice device, Matrix world, ICamera3D cam, Texture2D texture)
        {
            base.Render(effect, device, world, cam, true, texture);
        }
        public void Render(Effect effect, GraphicsDevice device, Matrix world, ICamera3D cam, Texture2D texture)
        {
            base.Render(effect, device, world, cam, true, texture);
        }
    }
}