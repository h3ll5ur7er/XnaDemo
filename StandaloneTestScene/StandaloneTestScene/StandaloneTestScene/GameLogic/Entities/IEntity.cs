using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public interface IEntity : IRenderableGameContent
    {
        string ID { get; }
    }
}