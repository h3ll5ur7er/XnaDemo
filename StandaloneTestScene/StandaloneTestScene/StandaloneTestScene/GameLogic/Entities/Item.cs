using System;
using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public abstract class Item : IEntity
    {
        public abstract string ID { get; }
        protected float HoveringCounter { get; private set; }
        protected float HoveringCounterSpeed { get; set; }
        protected float HoveringCounterPeriod { get; private set; }

        protected float RotatingCounter { get; private set; }
        protected float RotatingCounterSpeed { get; set; }
        protected float RotatingCounterPeriod { get; private set; }


        protected Matrix hovering, rotating;
        public Vector3 Position { get; protected set; }
        public BoundingBox BB { get; protected set; }

        protected Item()
        {
            HoveringCounter = 0;
            HoveringCounterSpeed = 0.05f;
            HoveringCounterPeriod = MathHelper.TwoPi;

            RotatingCounterPeriod = 0;
            RotatingCounterSpeed = 0.03f;
            RotatingCounterPeriod = MathHelper.TwoPi;
        }

        public virtual void Update()
        {
            HoveringCounter += HoveringCounterSpeed;
            HoveringCounter %= HoveringCounterPeriod;

            hovering = Matrix.CreateTranslation(0, (float)Math.Sin(HoveringCounter)*0.1f, 0);

            RotatingCounter += RotatingCounterSpeed;
            RotatingCounter %= RotatingCounterPeriod;

            rotating = Matrix.CreateRotationY(RotatingCounter);
        }

        public abstract void Render(Matrix world);
    }
}