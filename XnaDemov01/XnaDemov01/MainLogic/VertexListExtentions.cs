using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public static class VertexListExtentions
    {
        public static void Add(this List<VertexPositionNormalTexture> collection, Vector3 position, Vector3 normal,
            Vector2 texcoord)
        {
            collection.Add(new VertexPositionNormalTexture(position, normal, texcoord));
        }
    }
}