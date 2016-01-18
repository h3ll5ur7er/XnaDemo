using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene
{
    public class InputController : GameComponent
    {
        public static InputController Instance { get; private set; }

        private InputMethod autoMethod = InputMethod.Combined;

        private readonly Dictionary<InputMethod, ButtonPressedWrapper> buttons;

        private readonly Dictionary<InputMethod, StatePositionWrapper> positions;
        public InputController(Game game) : base(game)
        {
            if(Instance != null)
                throw new InvalidOperationException("Input Controller allready created");

            var ms = Mouse.GetState();
            var ks = Keyboard.GetState();
            var gs = GamePad.GetState(PlayerIndex.One);
            positions = new Dictionary<InputMethod, StatePositionWrapper>
            {
                {InputMethod.KeyboardMouse, ms},
                {InputMethod.Gamepad, gs},
                {InputMethod.Simulated, new Vector2(SimulatedMouse.X, SimulatedMouse.Y)},
                {InputMethod.Combined, ((StatePositionWrapper)ms).Position + ((StatePositionWrapper)gs).Position},
                {InputMethod.Auto, ((StatePositionWrapper)ms).Position + ((StatePositionWrapper)gs).Position}
            };
            
            buttons = new Dictionary<InputMethod, ButtonPressedWrapper>
            {
                {InputMethod.KeyboardMouse, ks},
                {InputMethod.Gamepad, gs},
                {InputMethod.Simulated, SimulatedKeyboard.PressedKeys},
                {InputMethod.Auto, ks}
            }
                ;
            Instance = this;
        }

        public override void Update(GameTime gameTime)
        {
            var ms = Mouse.GetState();
            var ks = Keyboard.GetState();
            var gs = GamePad.GetState(PlayerIndex.One);
            

            positions[InputMethod.KeyboardMouse] = ms;
            positions[InputMethod.Gamepad] = gs;
            positions[InputMethod.Simulated] = new Vector2(SimulatedMouse.X, SimulatedMouse.Y);
			positions[InputMethod.Combined] = ((StatePositionWrapper)ms).Position + ((StatePositionWrapper)gs).Position.LinuxFix()*40;
            positions[InputMethod.Auto] = positions[autoMethod];
            
            buttons[InputMethod.KeyboardMouse] = ks;
            buttons[InputMethod.Gamepad] = gs;
            buttons[InputMethod.Simulated] = SimulatedKeyboard.PressedKeys;
            buttons[InputMethod.Combined] = ks;
            buttons[InputMethod.Auto] = buttons[autoMethod];
            SimulatedKeyboard.Reset();
            base.Update(gameTime);
        }

        public Vector2 GetPosition(InputMethod input = InputMethod.Auto) => positions[input].Position;
        public bool IsKeyUp(InputMethod input, Buttons key) => buttons[input].IsUp(key);
        public bool IsKeyDown(InputMethod input, Buttons key) => buttons[input].IsDown(key);
        public bool IsKeyUp(InputMethod input, Keys key) => buttons[input].IsUp(key);
        public bool IsKeyDown(InputMethod input, Keys key) => buttons[input].IsDown(key);
        public static Vector2 Position(InputMethod input = InputMethod.Auto) => Instance.GetPosition(input);
        public static bool KeyUp(Keys key, InputMethod input = InputMethod.Auto) => Instance.IsKeyUp(input, key);
        public static bool KeyDown(Keys key, InputMethod input = InputMethod.Auto) => Instance.IsKeyDown(input, key);
        public static bool KeyUp(Buttons key, InputMethod input = InputMethod.Gamepad) => Instance.IsKeyUp(input, key);
        public static bool KeyDown(Buttons key, InputMethod input = InputMethod.Gamepad) => Instance.IsKeyDown(input, key);
    }

    public static class DebugExtentions
    {
        public static T _<T>(this T value)
        {
            Debug.WriteLine(value);
            return value;
        }
    }
}