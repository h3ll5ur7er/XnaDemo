using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
	public class GoalParticles : ParticleEffect
	{
		public GoalParticles (Vector3 position, int lifetime, int count, float speed = 0.001f) : base(position, 2* lifetime, count, speed*1.5f)
		{
			
		}
	}

    public class ParticleModel : Model<VertexPositionNormalTexture>
    {
        private readonly bool useDefaultShader;

        public ParticleModel(float size = 1f, bool useDefaultShader = true)
        {
            this.useDefaultShader = useDefaultShader;

            Vertices.Add(new Vector3(-size, -size, 0), Vector3.Backward, new Vector2(0, 0));
            Vertices.Add(new Vector3(-size, size, 0), Vector3.Backward, new Vector2(0, 1));
            Vertices.Add(new Vector3(size, -size, 0), Vector3.Backward, new Vector2(1, 0));

            Vertices.Add(new Vector3(size, size, 0), Vector3.Backward, new Vector2(1, 1));
            Vertices.Add(new Vector3(size, -size, 0), Vector3.Backward, new Vector2(1, 0));
            Vertices.Add(new Vector3(-size, size, 0), Vector3.Backward, new Vector2(0, 1));

            Vertices.Add(new Vector3(-size, -size, 0), Vector3.Forward, new Vector2(0, 0));
            Vertices.Add(new Vector3(size, -size, 0), Vector3.Forward, new Vector2(1, 0));
            Vertices.Add(new Vector3(-size, size, 0), Vector3.Forward, new Vector2(0, 1));

            Vertices.Add(new Vector3(size, size, 0), Vector3.Forward, new Vector2(1, 1));
            Vertices.Add(new Vector3(-size, size, 0), Vector3.Forward, new Vector2(0, 1));
            Vertices.Add(new Vector3(size, -size, 0), Vector3.Forward, new Vector2(1, 0));
            SetUp(GameController.Engine);
        }
    }

    public class Particle
    {
        private bool useDefaultShader;
		private float speed;
		private int baseLifeTime;
		Vector2 scale;

        public int Lifetime { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }

		public Particle(int lifetime, Vector3 position, Vector3 direction, Vector2 scale, float speed = 0.01f, bool useDefaultShader = true)
        {
            this.Lifetime = lifetime;
            this.baseLifeTime = lifetime;
            this.Position = position;
            this.Direction = direction;
            this.speed = speed;
			this.useDefaultShader = useDefaultShader;
			//this.scale = new Vector2 (1, 1);
			this.scale = scale;

        }

        public void Reset(Vector3 position)
        {
            this.Position = position;
            this.Lifetime = baseLifeTime;
			Direction = Vector3.Transform (Direction, Matrix.CreateRotationY ((float)ParticleEffect.rng.NextDouble ()));
        }

        public void Update()
        {
            if (ParticleEffect.rng.NextDouble()>0.75)
            {
				var noise = Matrix.CreateFromYawPitchRoll(Direction.X+ParticleEffect.rng.NextFloatSquared(), Direction.Y + ParticleEffect.rng.NextFloat(), Direction.Z + ParticleEffect.rng.NextFloat());

                Direction = Vector3.Transform(Direction, noise);
            }
            Position += Direction.Normalized()*speed;
            Lifetime--;

        }

		public void Render(Matrix world, Vector2 scale)
        {
            //if(useDefaultShader)
			Assets.Models.ParticleModel.Render(GameController.DefaultShader, GameController.GDevice, Matrix.CreateScale(scale.X, scale.Y, 1f)*world*Matrix.CreateWorld(Position, FirstPersonCamera.Instance.Position-Position, Vector3.Up)*0.0005f, FirstPersonCamera.Instance, false, Assets.Textures.Particle);
        }
    }
	public interface IScalableEntity : IEntity
	{
		void Render (Matrix world, Vector2 scale);

	}
	public class ParticleEffect : IScalableEntity
    {
        private readonly int lifetime;
        internal static readonly Random rng = new Random();
        
        public Vector3 Position { get; }
        public BoundingBox BB { get; }

        public IOrderedEnumerable<Particle> particles;

        public ParticleEffect(Vector3 position, int lifetime, int count, float speed = 0.01f)
        {
            this.lifetime = lifetime;
            Position = position;
            BB = new BoundingBox(Position + new Vector3(-0.2f), Position + new Vector3(0.2f));
            var particles = new List<Particle>();
            for (int i = 0; i < count; i++)
            {
				particles.Add(new Particle((int) (lifetime*(Math.Pow(rng.NextDouble(),2)))+10, Position, new Vector3((float) rng.NextDouble(), (float) rng.NextDouble(), (float) rng.NextDouble()).Normalized()*speed*(float)rng.NextDouble(), new Vector2((float)rng.NextDouble(), (float)rng.NextDouble()), speed));
                particles[i].Update();
            }
            this.particles = particles.OrderByDescending(x => Vector3.Distance(x.Position, FirstPersonCamera.Instance.Position));
        }

        public void Update()
        {
            particles.Select(UpdateParticle).Where(p => p.Lifetime < 1).Foreach(ResetParticle);
        }

		public void Render(Matrix world) => particles.Foreach(p => p.Render(world, 0.05f*new Vector2((float)ParticleEffect.rng.NextDouble(), (float)ParticleEffect.rng.NextDouble())));
		public void Render(Matrix world, Vector2 scale) => particles.Foreach(p => p.Render(world, scale));

        private static Particle UpdateParticle(Particle p)
        {
            p.Update();
            return p;
        }
        private void ResetParticle(Particle p)
        {
            p.Reset(Position);
        }
    }

    public static class IEnumerableExtentions
    {
        public static void Foreach<T>(this IEnumerable<T> collection, Action<T> a)
		{
			if (collection == null)
				return;
			if (a == null)
				return;
            foreach (var v in collection)
            {
                a(v);
            }
        }
    }
    public static class Vector3Extentions
    {
        public static Vector3 Normalized(this Vector3 v)
        {
            v.Normalize();
            return v;
        }
        public static Vector4 Normalized(this Vector4 v)
        {
            v.Normalize();
            return v;
        }
        public static Vector2 Normalized(this Vector2 v)
        {
            v.Normalize();
            return v;
        }
    }

	public static class RandomExtentions
	{
		public static float NextFloat(this Random rng)
		{
			if (rng.NextDouble () > 0.5f)
				return (float)-rng.NextDouble ();
			else
				return (float)rng.NextDouble ();
		}

		public static float NextFloatSquared(this Random rng)
		{
			return (float) Math.Pow (rng.NextFloat(), 2);
		}

		public static double NextDoubleSquared(this Random rng)
		{
			return Math.Pow (rng.NextDouble(), 2);
		}
	}
}