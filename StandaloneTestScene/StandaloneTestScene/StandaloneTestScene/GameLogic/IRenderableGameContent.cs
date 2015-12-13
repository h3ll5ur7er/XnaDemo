using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public interface IRenderableGameContent
    {
        Vector3 Position { get; }
        BoundingBox BB { get; }

        void Update();
        void Render(Matrix world);
    }
}