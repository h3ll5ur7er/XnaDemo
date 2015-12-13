using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public interface ICamera3D : IGameComponent
    {
        Matrix View { get; }
        Matrix Projection { get; }
    }
}