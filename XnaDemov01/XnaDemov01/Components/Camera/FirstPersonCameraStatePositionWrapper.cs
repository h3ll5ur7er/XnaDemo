using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene
{
    public static class SimulatedMouse
    {
        private static bool leftDown = false;
        private static bool rightDown = false;
        public static int X { get; set; }
        public static int Y { get; set; }

        public static void SimulateLeftClick()
        {
        }
    }

    public static class SimulatedKeyboard
    {
        private static List<Keys> keys = new List<Keys>(); 
        public static List<Keys> PressedKeys { get { return keys; } private set { keys = value; } }

        public static void Simulate(params Keys[] pressedKeys)
        {
            PressedKeys.AddRange(pressedKeys);
        }

        public static void Reset()
        {
            PressedKeys.Clear();
        }
    }

    public class InputController : GameComponent
    {
        public static InputController Instance { get; private set; }

        private InputMethod autoMethod = InputMethod.KeyboardMouse;

        private readonly Dictionary<InputMethod, ButtonPressedWrapper> buttons;

        private readonly Dictionary<InputMethod, StatePositionWrapper> positions;
        public InputController(Game game) : base(game)
        {
            if(Instance != null)
                throw new InvalidOperationException("Input Controller allready created");

            positions = new Dictionary<InputMethod, StatePositionWrapper>
            {
                {InputMethod.KeyboardMouse, Mouse.GetState()},
                {InputMethod.Gamepad, GamePad.GetState(PlayerIndex.One)},
                {InputMethod.Simulated, new Vector2(SimulatedMouse.X, SimulatedMouse.Y)},
                {InputMethod.Auto, Mouse.GetState()}
            };

            buttons = new Dictionary<InputMethod, ButtonPressedWrapper>
            {
                {InputMethod.KeyboardMouse, Keyboard.GetState()},
                {InputMethod.Gamepad, GamePad.GetState(PlayerIndex.One)},
                {InputMethod.Simulated, SimulatedKeyboard.PressedKeys},
                {InputMethod.Auto, Keyboard.GetState()}
            }
            ;
            Instance = this;
        }

        public override void Update(GameTime gameTime)
        {
            positions[InputMethod.KeyboardMouse] = Mouse.GetState();
            positions[InputMethod.Gamepad] = GamePad.GetState(PlayerIndex.One);
            positions[InputMethod.Simulated] = new Vector2(SimulatedMouse.X, SimulatedMouse.Y);
            positions[InputMethod.Auto] = positions[autoMethod];

            buttons[InputMethod.KeyboardMouse] = Keyboard.GetState();
            buttons[InputMethod.Gamepad] = GamePad.GetState(PlayerIndex.One);
            buttons[InputMethod.Simulated] = SimulatedKeyboard.PressedKeys;
            buttons[InputMethod.Auto] = buttons[autoMethod];
            SimulatedKeyboard.Reset();
            base.Update(gameTime);
        }

        public Vector2 GetPosition(InputMethod input = InputMethod.Auto)
        {
            return positions[input].Position;
        }

        public bool IsKeyUp(InputMethod input, Buttons key)
        {
            return buttons[input].IsUp(key);
        }

        public bool IsKeyDown(InputMethod input, Buttons key)
        {
            return buttons[input].IsDown(key);
        }


        public bool IsKeyUp(InputMethod input, Keys key)
        {
            return buttons[input].IsUp(key);
        }

        public bool IsKeyDown(InputMethod input, Keys key)
        {
            return buttons[input].IsDown(key);
        }

        public static Vector2 Position(InputMethod input = InputMethod.Auto)
        {
            return Instance.GetPosition(input);
        }
        public static bool KeyUp(Keys key, InputMethod input = InputMethod.Auto)
        {
            return Instance.IsKeyUp(input, key);
        }
        public static bool KeyDown(Keys key, InputMethod input = InputMethod.Auto)
        {
            return Instance.IsKeyDown(input, key);
        }
        public static bool KeyUp(Buttons key, InputMethod input = InputMethod.Gamepad)
        {
            return Instance.IsKeyUp(input, key);
        }
        public static bool KeyDown(Buttons key, InputMethod input = InputMethod.Gamepad)
        {
            return Instance.IsKeyDown(input, key);
        }
    }

    public enum InputMethod
    {
        Auto,
        KeyboardMouse,
        Gamepad,
        Simulated
    }
}
