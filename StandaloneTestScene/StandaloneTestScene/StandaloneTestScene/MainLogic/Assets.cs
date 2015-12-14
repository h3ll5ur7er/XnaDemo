using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public static class Assets
    {
        public static class Textures
        {
            public static Texture2D FloorTexture { get; private set; }
            public static Texture2D WallTexture { get; private set; }

            public static Texture2D KeyTexture { get; private set; }
            public static Texture2D RedKeyTexture { get; private set; }
            public static Texture2D GreenKeyTexture { get; private set; }
            public static Texture2D BlueKeyTexture { get; private set; }

            public static Texture2D RedDoorClosedTexture { get; private set; }
            public static Texture2D GreenDoorClosedTexture { get; private set; }
            public static Texture2D BlueDoorClosedTexture { get; private set; }

            public static Texture2D RedDoorOpenTexture { get; private set; }
            public static Texture2D GreenDoorOpenTexture { get; private set; }
            public static Texture2D BlueDoorOpenTexture { get; private set; }

            public static Texture2D Particle { get; private set; }

            internal static void Load()
            {
                WallTexture = Load("floor");
                FloorTexture = Load("wall");

                KeyTexture = Load("Key");        
                RedKeyTexture = Load("RedKey");
                GreenKeyTexture = Load("GreenKey");
                BlueKeyTexture = Load("BlueKey");

                RedDoorClosedTexture = Load("RedDoorClosed");
                GreenDoorClosedTexture = Load("GreenDoorClosed");
                BlueDoorClosedTexture = Load("BlueDoorClosed");

                RedDoorOpenTexture = Load("RedDoorOpen");
                GreenDoorOpenTexture = Load("GreenDoorOpen");
                BlueDoorOpenTexture = Load("BlueDoorOpen");

                Particle = Load("Particle");
            }

            private static Texture2D Load(string assetName)
            {
                return GameController.Content.Load<Texture2D>("Textures/" + assetName);
            }
        }


        public static class Models
        {

            public static TexturedCubeModel TexturedCube;
            public static ItemModel ItemModel;
            public static DoorModel DoorModel;
            public static ParticleModel ParticleModel;

            internal static void Load()
            {
                TexturedCube = new TexturedCubeModel();
                ItemModel = new ItemModel();
                DoorModel = new DoorModel();
                ParticleModel = new ParticleModel(0.05f);
            }
        }

        public static class Levels
        {
            public static Texture2D Level0 { get; private set; }
            public static Texture2D Level1 { get; private set; }
            public static Texture2D Level2 { get; private set; }
            public static Texture2D Level3 { get; private set; }

            internal static void Load()
            {
                Level0 = Load("Level0");
                Level1 = Load("Level1");
                Level2 = Load("Level2");
                Level3 = Load("Level3");
            }

            private static Texture2D Load(string assetName)
            {
                return GameController.Content.Load<Texture2D>("Levels/" + assetName);
            }
        }

        public static void LoadAssets()
        {
            Textures.Load();
            Models.Load();
            Levels.Load();
        }

    }
}