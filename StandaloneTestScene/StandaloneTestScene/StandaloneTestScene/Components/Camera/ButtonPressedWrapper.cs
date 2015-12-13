using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene
{
    public class ButtonPressedWrapper
    {
        private readonly InputMethod input;

        private readonly KeyboardState kState;
        private readonly GamePadState gState;
        private readonly IEnumerable<Keys> sState;

        public bool Pad { get { return input == InputMethod.Gamepad; } }

        private ButtonPressedWrapper(KeyboardState state)
        {
            kState = state;
            input = InputMethod.KeyboardMouse;
        }

        private ButtonPressedWrapper(GamePadState state)
        {
            gState = state;
            input = InputMethod.Gamepad;
        }

        private ButtonPressedWrapper(IEnumerable<Keys> state)
        {
            sState = state;
            input = InputMethod.Gamepad;
        }

        public bool IsDown(Buttons button)
        {
            return gState.IsButtonDown(button);
        }
        public bool IsUp(Buttons button)
        {
            return gState.IsButtonUp(button);
        }
        public bool IsDown(Keys key, bool sim = false)
        {
            if (sim) return sState.Contains(key);
            return kState.IsKeyDown(key);
        }
        public bool IsUp(Keys key, bool sim = false)
        {
            if (sim) return !sState.Contains(key);
            return kState.IsKeyUp(key);
        }

        public static implicit operator ButtonPressedWrapper(KeyboardState state)
        {
            return new ButtonPressedWrapper(state);
        }
        public static implicit operator ButtonPressedWrapper(GamePadState state)
        {
            return new ButtonPressedWrapper(state);
        }
        public static implicit operator ButtonPressedWrapper(List<Keys> state)
        {
            return new ButtonPressedWrapper(state);
        }
    }
}