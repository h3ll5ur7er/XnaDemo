using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public abstract class Model<T> where T : struct, IVertexType
    {
        internal List<T> Vertices = new List<T>();
        internal List<short> Indices = new List<short>();

        private VertexBuffer vBuffer;
        private IndexBuffer iBuffer;

        protected void SetUpIndexed(Game game)
        {
            vBuffer = new VertexBuffer(game.GraphicsDevice, typeof(T), Vertices.Count, BufferUsage.WriteOnly);
            iBuffer = new IndexBuffer(game.GraphicsDevice, typeof(short), Indices.Count, BufferUsage.WriteOnly);

            vBuffer.SetData(Vertices.ToArray());
            iBuffer.SetData(Indices.ToArray());
        }

        protected void SetUp(Game game)
        {
            vBuffer = new VertexBuffer(game.GraphicsDevice, typeof(T), Vertices.Count, BufferUsage.WriteOnly);

            vBuffer.SetData(Vertices.ToArray());
        }

        public virtual void Render(BasicEffect effect, GraphicsDevice device, Matrix world, ICamera3D cam, bool indexed, Texture2D texture)
        {
            effect.View = cam.View;
            effect.Projection = cam.Projection;
            effect.World = world;
            
            effect.TextureEnabled = true;
            effect.Texture = texture;

            effect.CurrentTechnique.Passes[0].Apply();
            device.BlendState = BlendState.AlphaBlend;
            device.SetVertexBuffer(vBuffer);
            if (indexed)
            {
                device.Indices = iBuffer;

                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vBuffer.VertexCount, 0, iBuffer.IndexCount / 3);
            }
            else
            {
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, vBuffer.VertexCount / 3);
            }
        }
        public virtual void Render(Effect effect, GraphicsDevice device, Matrix world, ICamera3D cam, bool indexed, Texture2D texture)
        {
            var foo = effect.Parameters;
            var bar = string.Join(", ", foo.Select(x => x.Name));
            // Script, gWorldXf, gWorldITXf, gWvpXf, gViewIXf, gLamp0DirPos, gLamp0Color, gAmbiColor, gKs, gSpecExpon, gColorTexture
            //effect.Parameters["gViewIXf"].SetValue(cam.View);
            //effect.Parameters["gWvpXf"].SetValue(cam.Projection);
            //effect.Parameters["gWorldXf"].SetValue(world);
            //effect.Parameters["gWorldITXf"].SetValue(world);

            effect.Parameters["World"].SetValue(world);
            effect.Parameters["Projection"].SetValue(cam.Projection);
            effect.Parameters["View"].SetValue(cam.View);

            effect.Parameters["tex"].SetValue(texture);

            effect.Techniques[0].Passes[0].Apply();

            device.SetVertexBuffer(vBuffer);
            if (indexed)
            {
                device.Indices = iBuffer;

                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vBuffer.VertexCount, 0, iBuffer.IndexCount / 3);
            }
            else
            {
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, vBuffer.VertexCount / 3);
            }
        }
    }
}