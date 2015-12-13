using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene
{
    public class StatePositionWrapper
    {
        private readonly Vector2 value;
        private readonly InputMethod input;
        public Vector2 Position { get { return value; } }
        public float X { get { return value.X; } }
        public float Y { get { return value.Y; } }

        public bool Pad { get { return input == InputMethod.Gamepad; } }

        private StatePositionWrapper(float x, float y, InputMethod input)
        {
            if(input == InputMethod.KeyboardMouse)
                value = new Vector2(x - FirstPersonCamera.mx, y - FirstPersonCamera.my);
            this.input = input;
        }

        public static implicit operator StatePositionWrapper(MouseState state)
        {
            return new StatePositionWrapper(state.X, state.Y, InputMethod.KeyboardMouse);
        }

        public static implicit operator StatePositionWrapper(GamePadState state)
        {
            return new StatePositionWrapper(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y, InputMethod.Gamepad);
        }
        public static implicit operator StatePositionWrapper(Vector2 state)
        {
            return new StatePositionWrapper(state.X, state.Y, InputMethod.Simulated);
        }
    }
}