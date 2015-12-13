using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public abstract class StructureElementBase : IStructureElement
    {
        public Vector3 Position { get; protected set; }
        public BoundingBox BB { get; protected set; }
        public abstract void Update();

        public abstract void Render(Matrix world);
    }
}