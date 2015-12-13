using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StandaloneTestScene
{
    public class Level
    {
        public static readonly Color SPAWN = new Color(0x42, 0x42, 0x42);
        public static readonly Color WALL = new Color(0x92, 0x92, 0x92);
        public static readonly Color RED_KEY = new Color(0x92, 0x42, 0x42);
        public static readonly Color GREEN_KEY = new Color(0x42, 0x92, 0x42);
        public static readonly Color BLUE_KEY = new Color(0x42, 0x42, 0x92);
        public static readonly Color RED_DOOR = new Color(0xff, 0x00, 0x00);
        public static readonly Color GREEN_DOOR = new Color(0x00, 0xff, 0x00);
        public static readonly Color BLUE_DOOR = new Color(0x00, 0x00, 0xff);

        public const string RED_KEY_ID = "RED_KEY";
        public const string GREEN_KEY_ID = "GREEN_KEY";
        public const string BLUE_KEY_ID = "BLUE_KEY";
        public static readonly Dictionary<string, Color> KEY_COLORS = new Dictionary<string, Color>
        {
            {RED_KEY_ID, Color.Red},
            {GREEN_KEY_ID, Color.Green},
            {BLUE_KEY_ID, Color.Blue}
        };

        private List<IRenderableGameContent> thingsToRender; 
        private IOrderedEnumerable<IRenderableGameContent> thingsToRenderInOrder;
        private Color[,] levelData;

        public TiledTerrain Terrain { get; set; }
        public TiledTerrain Ceilling { get; set; }

        public List<IStructureElement> Structure { get; set; } 
        public List<IEntity> Entities { get; set; }

        public Color this[int x, int y] => levelData[x, y];

        public Level()
        {
            Structure = new List<IStructureElement>();
            Entities = new List<IEntity>();

            FirstPersonCamera.Instance.TryMove += CollisionDetection;
            thingsToRender = new List<IRenderableGameContent>();

        }

        private bool CollisionDetection(Vector3 position, float x, float z)
        {
            var playerBB = new BoundingBox(position + new Vector3(x, 0, z) + new Vector3(-0.25f, -0.25f, -0.25f),
                position + new Vector3(x, 0, z) + new Vector3(0.25f, 0.25f, 0.25f));

            foreach (var entity in Entities.Where(entity => entity.BB.Contains(playerBB) != ContainmentType.Disjoint).ToList())
            {
                Player.Inventory.PickUp(entity);
            }

            return Structure.All(s => s.BB.Contains(playerBB) == ContainmentType.Disjoint);
        }

        public void Load(Texture2D level)
        {
            Terrain = new TiledTerrain(level.Width, level.Height, Settings.TILE_WIDTH, Settings.TILE_HEIGHT, -0.5f);
            Ceilling = new TiledTerrain(level.Width, level.Height, Settings.TILE_WIDTH, Settings.TILE_HEIGHT, 0.5f);

            var data = new Color[level.Width * level.Height];

            levelData = new Color[level.Width, level.Height];
            level.GetData(data);
            List<Vector3> doorPositions = new List<Vector3>();
            for (int x = 0; x < level.Width; x++)
            {
                for (int y = 0; y < level.Height; y++)
                {
                    var color = data[y * level.Width + x];
                    levelData[x, y] = color;
                    if (color == WALL)
                    {
                        Structure.Add(new SolidBlock(x, y));
                    }
                    else if (color == SPAWN)
                    {
                        FirstPersonCamera.Instance.Position = new Vector3(x * Settings.TILE_WIDTH, FirstPersonCamera.Instance.Position.Y, y * Settings.TILE_HEIGHT);
                    }
                    else if (color == RED_KEY)
                    {
                        Entities.Add(new Key(x, y, RED_KEY_ID));
                    }
                    else if (color == RED_DOOR)
                    {
                        doorPositions.Add(new Vector3(x, y, 0));
                    }
                    else if (color == GREEN_KEY)
                    {
                        Entities.Add(new Key(x, y, GREEN_KEY_ID));
                    }
                    else if (color == GREEN_DOOR)
                    {
                        doorPositions.Add(new Vector3(x, y, 1));
                    }
                    else if (color == BLUE_KEY)
                    {
                        Entities.Add(new Key(x, y, BLUE_KEY_ID));
                    }
                    else if (color == BLUE_DOOR)
                    {
                        doorPositions.Add(new Vector3(x, y, 2));
                    }
                }
            }
            foreach (var door in doorPositions)
            {
                var TOLERANCE = 0.0001;
                if (Math.Abs(door.Z) < TOLERANCE)
                {
                    Structure.Add(new Door((int)door.X, (int)door.Y, RED_KEY_ID));
                }
                else if (Math.Abs(door.Z - 1) < TOLERANCE)
                {

                    Structure.Add(new Door((int)door.X, (int)door.Y, GREEN_KEY_ID));
                }
                else if (Math.Abs(door.Z - 2) < TOLERANCE)
                {

                    Structure.Add(new Door((int)door.X, (int)door.Y, BLUE_KEY_ID));
                }
            }
        }

        public void Update()
        {
            foreach (var element in Structure)
            {
                element.Update();
            }
            foreach (var element in Entities)
            {
                element.Update();
            }
            thingsToRender.Clear();
            thingsToRender.AddRange(Structure);
            thingsToRender.AddRange(Entities);
            thingsToRenderInOrder = thingsToRender.OrderByDescending(x => Vector3.Distance(FirstPersonCamera.Instance.Position, x.Position));
        }

        public void Render()
        {
            Terrain.Render(Matrix.Identity);
            Ceilling.Render(Matrix.Identity);
            foreach (var element in thingsToRenderInOrder)
            {
                element.Render(Matrix.Identity);
            }
        }
    }
}