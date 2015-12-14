using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene
{
    public class FirstPersonCamera : DrawableGameComponent, ICamera3D
    {
        public delegate bool TryMoveHandler(Vector3 position, float x, float z);

        public event TryMoveHandler TryMove;

        public const int mx = 100;
        public const int my = 100;
        private readonly Game game;
        private float yaw, pitch;
        private const bool FlightEnabled = false;


        private static FirstPersonCamera camInstance;
        private static object camInstancePadlock = new object();

        public static FirstPersonCamera Instance
        {
            get
            {
                if (camInstance != null) return camInstance;
                throw new ArgumentException("no instance created");
            }
        }

        public BoundingBox PlayerBB => new BoundingBox(Position+new Vector3(-0.25f, -0.25f, -0.25f), Position+new Vector3(0.25f, 0.25f, 0.25f));
        public Vector3 Position { get; set; }
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }
        public bool Active { get; set; }

        public Ray CentralRay
            => new Ray(
                Game.GraphicsDevice.Viewport.Unproject(new Vector3(Settings.SCREEN_WIDTH/2f,Settings.SCREEN_HEIGHT/2f,0), Projection, View, Matrix.Identity),
                Game.GraphicsDevice.Viewport.Unproject(new Vector3(Settings.SCREEN_WIDTH/2f,Settings.SCREEN_HEIGHT/2f,1), Projection, View, Matrix.Identity));

        public FirstPersonCamera(Game game) : base(game)
        {
            if(camInstance != null) throw new ArgumentException("instance allready created");
            this.game = game;
            camInstance = this;
        }

        public override void Initialize()
        {
            Position =  Vector3.Zero;
        }

        public new void LoadContent()
        {
            Resize(MathHelper.PiOver4,
                game.GraphicsDevice.Viewport.AspectRatio,
                0.1f,
                10000f);
            Active = true;
            base.LoadContent();
        }
        
        public void Resize(float aspectRatio, float fov = MathHelper.PiOver4, float near = 0f, float far = 1000f)
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(
                        aspectRatio,
                        fov,
                        near,
                        far);
        }

        public override void Update(GameTime gt)
        {
            if(!(Active && game.IsActive)) return;

            Look();
            Movement();

            View =
                Matrix
                    .CreateLookAt(
                        Position,
                        Position + Vector3.Transform(Vector3.Forward, Matrix.CreateRotationX(pitch) * Matrix.CreateRotationY(yaw)),
                        Vector3.Up);
            base.Update(gt);
        }
        
        private void Look()
        {
            yaw += -InputController.Position(InputMethod.Combined).X * Settings.Control.LOOK_SPEED_MOUSE;
            pitch += -InputController.Position(InputMethod.Combined).Y * (Settings.Control.INVERTED_LOOK ? -1 : 1) *Settings.Control.LOOK_SPEED_MOUSE;
            pitch = MathHelper.Clamp(pitch, -1.5f, 1.5f);

            if (Active)
                Mouse.SetPosition(mx, my);
        }
        private void Movement()
        {
            
            if (InputController.KeyDown(Settings.Control.gForward) || InputController.KeyDown(Settings.Control.kForward) ||InputController.KeyDown(Settings.Control.kForward, InputMethod.Simulated))
            {
                Move(Vector3.Forward);
            }

            if (InputController.KeyDown(Settings.Control.gLeft) || InputController.KeyDown(Settings.Control.kLeft) || InputController.KeyDown(Settings.Control.kLeft, InputMethod.Simulated))
            {
                Move(Vector3.Left);
            }

            if (InputController.KeyDown(Settings.Control.gBackward) || InputController.KeyDown(Settings.Control.kBackward) || InputController.KeyDown(Settings.Control.kBackward, InputMethod.Simulated))
            {
                Move(Vector3.Backward);
            }

            if (InputController.KeyDown(Settings.Control.gRight) || InputController.KeyDown(Settings.Control.kRight) || InputController.KeyDown(Settings.Control.kRight, InputMethod.Simulated))
            {
                Move(Vector3.Right);
            }
            #pragma warning disable 162
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // ReSharper disable HeuristicUnreachableCode
            if (FlightEnabled)
            {
                if (InputController.KeyDown(Settings.Control.gDown) || InputController.KeyDown(Settings.Control.kDown) || InputController.KeyDown(Settings.Control.kDown, InputMethod.Simulated))
                {
                    Move(Vector3.Down);
                }

                if (InputController.KeyDown(Settings.Control.gUp) || InputController.KeyDown(Settings.Control.kUp) || InputController.KeyDown(Settings.Control.kUp, InputMethod.Simulated))
                {
                    Move(Vector3.Up);
                }
            }
            // ReSharper enable HeuristicUnreachableCode
            #pragma warning restore 162
            

            if (InputController.KeyDown(Settings.Control.gQuit) || InputController.KeyDown(Settings.Control.kQuit) || InputController.KeyDown(Settings.Control.kQuit, InputMethod.Simulated))
            {
                game.Exit();
            }
        }
        private void Move(Vector3 dir)
        {
            var posOffset =  Vector3.Transform(
                dir*Settings.Control.MOVE_SPEED_KEYBOARD,
                Matrix.CreateRotationY(yaw));
            if (TryMove == null) Position += posOffset;

            if (TryMove != null && TryMove(Position, posOffset.X, 0))
                Position += new Vector3(posOffset.X, 0, 0);
            if (TryMove != null && TryMove(Position, 0, posOffset.Z))
                Position += new Vector3(0, 0, posOffset.Z);
        }
    }
}