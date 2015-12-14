using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene
{
    public class StatePositionWrapper
    {
        private readonly InputMethod input;
        public Vector2 Position { get; }

        public float X => Position.X;
        public float Y => Position.Y;

        public bool Pad => input == InputMethod.Gamepad || input == InputMethod.Combined;

        private StatePositionWrapper(float x, float y, InputMethod input)
        {
            Position = input == InputMethod.KeyboardMouse || input == InputMethod.Combined
                ? new Vector2(x - FirstPersonCamera.mx, y - FirstPersonCamera.my)
                : new Vector2(x, y);
            this.input = input;
        }

        public static implicit operator StatePositionWrapper(MouseState state) => new StatePositionWrapper(state.X, state.Y, InputMethod.KeyboardMouse);

        public static implicit operator StatePositionWrapper(GamePadState state) => new StatePositionWrapper(state.ThumbSticks.Right.X, -state.ThumbSticks.Right.Y, InputMethod.Gamepad);

        public static implicit operator StatePositionWrapper(Vector2 state) => new StatePositionWrapper(state.X, state.Y, InputMethod.Simulated);

        public override string ToString() => $"{{{X}, {Y}}}";
    }
}