using System.Runtime.Remoting.Messaging;

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
}
