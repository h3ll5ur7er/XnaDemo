using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Microsoft.Xna.Framework;

namespace StandaloneTestScene
{
    public class HeightMapTerrain: Terrain<VertexPositionColorNormal>
    {
        //private readonly Texture2D heightMap;
        private readonly float heightOffset;
        private readonly float heightFactor;
        private readonly IColorRule colorRule;

        private float hMin = float.MaxValue;
        private float hMax = float.MinValue;

        public HeightMapTerrain(Game game, int xVertices, int zVertices, float xVertexDistance, float zVertexDistance,  float heightOffset, float heightFactor, IColorRule colorRule)
            : base(xVertices, zVertices, xVertexDistance, zVertexDistance)
        {
            //this.heightMap = heightMap;
            this.heightOffset = heightOffset;
            this.heightFactor = heightFactor;
            this.colorRule = colorRule;

            SetUp(game);
        }

        protected override void SetUpGrid()
        {
            var heightmap = new Bitmap("europe.png");
            for (int z = 0; z < zVertices; z++)
            {
                for (int x = 0; x < xVertices; x++)
                {
                    var h = heightOffset + heightmap.GetPixel(x, z).G*heightFactor;
                    positions[x,z] = new Vector3(x*xVertexDistance, h, z*zVertexDistance);

                    if (h < hMin) hMin = h;
                    if (h > hMax) hMax = h;

                }
            }
        }
        protected override void SetUpColors()
        {
            for (int z = 0; z < zVertices; z++)
            {
                for (int x = 0; x < xVertices; x++)
                {
                    colors[x, z] = colorRule.GetColor(positions[x, z].Y);
                }
            }
        }
        protected override void SetUpNormals()
        {
            for (int x = 1; x < xVertices-1; x++)
            {
                for (int z = 1; z < zVertices-1; z++)
                {
                    var cross1 = (positions[x - 1, z] - positions[x, z]).Cross((positions[x, z + 1] - positions[x, z]));
                    var cross2 = (positions[x, z + 1] - positions[x, z]).Cross((positions[x + 1, z] - positions[x, z]));
                    var cross3 = (positions[x + 1, z] - positions[x, z]).Cross((positions[x, z - 1] - positions[x, z]));
                    var cross4 = (positions[x, z - 1] - positions[x, z]).Cross((positions[x - 1, z] - positions[x, z]));

                    normals[x, z] = Interpolate(cross1, cross2, cross3, cross4);
                }
            }

            for (int x = 1; x < xVertices-1; x++)
            {
                var cross1 = (positions[x - 1, 0] - positions[x, 0]).Cross((positions[x, 1] - positions[x, 0]));
                var cross2 = (positions[x, 1] - positions[x, 0]).Cross((positions[x + 1, 0] - positions[x, 0]));

                normals[x, 0] = Interpolate(cross1, cross2);

                var cross3 = (positions[x + 1, zVertices - 1] - positions[x, zVertices - 1]).Cross((positions[x, zVertices - 2] - positions[x, zVertices - 1]));
                var cross4 = (positions[x, zVertices - 2] - positions[x, zVertices - 1]).Cross((positions[x - 1, zVertices - 1] - positions[x, zVertices - 1]));

                normals[x, zVertices - 1] = Interpolate(cross3, cross4);
            }

            for (int z = 1; z < zVertices-1; z++)
            {
                var cross1 = (positions[0, z + 1] - positions[0, z]).Cross((positions[1, z] - positions[0, z]));
                var cross2 = (positions[1, z] - positions[0, z]).Cross((positions[0, z - 1] - positions[0, z]));
                
                normals[0, z] = Interpolate(cross1, cross2);

                var cross3 = (positions[xVertices - 2, z] - positions[xVertices - 1, z]).Cross((positions[xVertices - 1, z + 1] - positions[xVertices - 1, z]));
                var cross4 = (positions[xVertices - 1, z - 1] - positions[xVertices - 1, z]).Cross((positions[xVertices - 2, z] - positions[xVertices - 1, z]));

                normals[xVertices - 1, z] = Interpolate(cross3, cross4);
            }

            normals[0, 0] = (positions[0, 1] - positions[0, 0])
                .Cross((positions[1, 0] - positions[0, 0]));

            normals[xVertices - 1, 0] = (positions[xVertices - 2, 0] - positions[xVertices - 1, 0])
                .Cross((positions[xVertices - 1, 1] - positions[xVertices - 1, 0]));

            normals[0, zVertices - 1] = (positions[1, zVertices - 1] - positions[0, zVertices - 1])
                .Cross((positions[0, zVertices - 2] - positions[0, zVertices - 1]));

            normals[xVertices - 1, zVertices - 1] = (positions[xVertices - 1, zVertices - 2] - positions[xVertices - 1, zVertices - 1])
                .Cross((positions[xVertices - 2, zVertices - 1] - positions[xVertices - 1, zVertices - 1]));

        }

        private void SetUpVertices()
        {
            SetUpGrid();
            Debug.WriteLine("hmin: " + hMin);
            Debug.WriteLine("hmax: " + hMax);
            SetUpColors();
            SetUpNormals();

            for (int z = 0; z < zVertices; z++)
            {
                for (int x = 0; x < xVertices; x++)
                {
                    Vertices.Add(new VertexPositionColorNormal(positions[x,z], colors[x,z], normals[x,z]));
                }
            }
        }
        private void SetUpIndices()
        {
            for (int z = 0; z < zVertices-1; z++)
            {
                for (int x = 0; x < xVertices-1; x++)
                {
                    Indices.AddRange(GetIndicesForCoordinate(x, z));
                }
            }
        }

        public new void SetUp(Game game)
        {
            SetUpVertices();
            SetUpIndices();

            SetUpIndexed(game);
        }

        private IEnumerable<short> GetIndicesForCoordinate(int x, int z)
        {

            /*
             givene point p adding all vertices needed for a palne:
             
                p2 p3
                
                p  p1
             
             created by triangles p p1 p2 and p1 p3 p2
             */

            var p  = (short) ( z      * xVertices + x);
            var p1 = (short) ( z      * xVertices + x + 1);
            var p2 = (short) ((z + 1) * xVertices + x);
            var p3 = (short) ((z + 1) * xVertices + x + 1);

            yield return p;
            yield return p1;
            yield return p2;

            yield return p1;
            yield return p3;
            yield return p2;
        }


        private Vector3 Interpolate(params Vector3[] vectors)
        {
            var x = 0f;
            var y = 0f;
            var z = 0f;
            foreach (var v in vectors)
            {
                x += v.X;
                y += v.Y;
                z += v.Z;
            }
            var vec = new Vector3(x/vectors.Length,y/vectors.Length,z/vectors.Length);
            vec.Normalize();
            return vec;
        }
    }
}