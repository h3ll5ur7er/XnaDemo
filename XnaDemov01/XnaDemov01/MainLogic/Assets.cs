using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

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

            internal static void LoadTextures()
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

			public static TexturedCubeModel TexturedCube { get; private set; }
			public static ItemModel ItemModel { get; private set; }
			public static DoorModel DoorModel { get; private set; }
			public static ParticleModel ParticleModel { get; private set; }

            internal static void LoadModels()
            {
                TexturedCube = new TexturedCubeModel();
                ItemModel = new ItemModel();
                DoorModel = new DoorModel();
                ParticleModel = new ParticleModel(0.5f);
            }
        }

        public static class Levels
        {
            public static Texture2D Level0 { get; private set; }
            public static Texture2D Level1 { get; private set; }
			public static Texture2D Level2 { get; private set; }
			public static Texture2D Level3 { get; private set; }
			public static Texture2D Nope { get; private set; }

            internal static void LevelsLoad()
            {
                Level0 = Load("Level0");
                Level1 = Load("Level1");
				Level2 = Load("Level2");
				Level3 = Load("Level3");
				Nope = Load("NopeLevel");
            }

            private static Texture2D Load(string assetName)
            {
                return GameController.Content.Load<Texture2D>("Levels/" + assetName);
            }
		}


		public static class Sounds
		{
			public static class Effects
			{
				public static SoundEffect OpenDoor { get; private set; }
				public static SoundEffect PickupKey { get; private set; }
				public static SoundEffect Levelup { get; private set; }

				internal static void LoadSoundEffects()
				{
					OpenDoor = Load("OpenDoor");
					PickupKey = Load ("PickupKey");
					Levelup = Load ("Levelup");
				}

				private static SoundEffect Load(string assetName)
				{
					return GameController.Content.Load<SoundEffect> ("Sound/Effects/" + assetName);
				}
			}


			public static class Background
			{
				public static SoundEffect BackgroundMusic { get; private set; }

				internal static void LoadBackgroundMusic ()
				{
					BackgroundMusic = Load("BackgroundMusic");
				}

				private static SoundEffect Load(string assetName)
				{
					return GameController.Content.Load<SoundEffect> ("Sound/Background/" + assetName);
				}
			}

			internal static void LoadSounds()
			{
				Effects.LoadSoundEffects ();
				Background.LoadBackgroundMusic ();
			}
		}

		public static class Fonts
		{
			public static SpriteFont DebugFont { get; private set; }

			public static void LoadFonts()
			{
				DebugFont = Load ("DebugFont");
			}
			public static SpriteFont Load(string assetName)
			{
				return GameController.Content.Load<SpriteFont> ("Fonts/"+assetName);
			}
		}

        public static void LoadAssets()
        {
			Textures.LoadTextures();
            Models.LoadModels();
            Levels.LevelsLoad();
			Sounds.LoadSounds ();
			Fonts.LoadFonts ();
        }
    }
}