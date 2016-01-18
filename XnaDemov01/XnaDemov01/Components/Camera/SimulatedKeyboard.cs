using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene
{
    public static class SimulatedKeyboard
    {
        public static List<Keys> PressedKeys { get; private set; } = new List<Keys>();

        public static void Simulate(params Keys[] pressedKeys)
        {
            PressedKeys.AddRange(pressedKeys);
        }

        public static void Reset()
        {
            PressedKeys.Clear();
        }
    }
}