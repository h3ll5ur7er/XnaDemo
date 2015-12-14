using Microsoft.Xna.Framework.Input;

namespace StandaloneTestScene
{
    public static class Settings
    {
        public const int SCREEN_WIDTH = 1680;
        public const int SCREEN_HEIGHT = 1050;

        public static class Control
        {

            public static bool INVERTED_LOOK = false;

            public const float MOVE_SPEED_KEYBOARD = 0.15f;
            public const float LOOK_SPEED_MOUSE = 0.002f;
            public const float TURN_SPEED_KEYBOARD = 0.02f;

            public const float MOVE_SPEED_PAD = 0.5f;
            public const float LOOK_SPEED_PAD = 0.03f;
            public const float TURN_SPEED_PAD = 0.02f;

            #region Keyboard

            public const Keys kForward = Keys.W;
            public const Keys kBackward = Keys.S;
            public const Keys kLeft = Keys.A;
            public const Keys kRight = Keys.D;

            public const Keys kUp = Keys.Space;
            public const Keys kDown = Keys.LeftShift;

            public const Keys kQuit = Keys.Escape;

            //public const Keys kFullScreen = Keys.F11;
            //public const Keys kDebug = Keys.F3;
            //public const Keys kWireframe = Keys.Q;
            //public const Keys kNextModel = Keys.E;

            //public const Keys kUndo = Keys.R;
            //public const Keys kCursor = Keys.M;

            #endregion Keyboard

            #region Gamepad

            public const Buttons gForward = Buttons.LeftThumbstickUp;
            public const Buttons gBackward = Buttons.LeftThumbstickDown;
            public const Buttons gLeft = Buttons.LeftThumbstickLeft;
            public const Buttons gRight = Buttons.LeftThumbstickRight;

            public const Buttons gUp = Buttons.A;
            public const Buttons gDown = Buttons.B;

            public const Buttons gQuit = Buttons.Back;

            //public const Buttons gFullScreen = Buttons.LeftStick;
            //public const Buttons gDebug = Buttons.Start;
            //public const Buttons gWireframe = Buttons.RightStick;
            //public const Buttons gNextModel = Buttons.X;

            //public const Buttons gUndo = Buttons.Y;
            //public const Buttons gCursor = Buttons.LeftShoulder;

            #endregion Gamepad
        }
        public const float TILE_WIDTH = 1f;
        public const float TILE_HEIGHT = 1f;
        public const float WALL_HEIGHT = 1f;

    }
}