using Foundation;
using UnityEngine;
using System.Collections.Generic;

namespace Experiments
{
    public sealed class Geometry
    {
        struct LineIndices
        {
            public readonly byte i1;
            public readonly byte i2;

            public LineIndices(byte i1, byte i2)
            {
                this.i1 = i1;
                this.i2 = i2;
            }
        }

        sealed class Template
        {
            public readonly Vector2[] vertices;
            public readonly LineIndices[] lineIndices;
            public readonly byte[] fillIndices;

            public Template(Vector2[] vertices, LineIndices[] lineIndices, byte[] fillIndices)
            {
                this.vertices = vertices;
                this.lineIndices = lineIndices;
                this.fillIndices = fillIndices;
            }
        }

        static readonly Template[] Templates = {
                // [0000] Single
                new Template(
                    vertices: new Vector2[] {
                            /*
                            new Vector2(0.5f, 0.0f),
                            new Vector2(1.0f, 0.5f),
                            new Vector2(0.0f, 0.5f),
                            new Vector2(0.5f, 1.0f),
                            */
                        },
                    lineIndices: new LineIndices[] {
                            /*
                            new LineIndices(0, 1),
                            new LineIndices(1, 3),
                            new LineIndices(3, 2),
                            new LineIndices(2, 0),
                            */
                        },
                    fillIndices: new byte[]{/* 0, 2, 1, 1, 2, 3 */}
                ),

                // [0001] Left
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(0.5f, 0.5f),
                            new Vector2(0.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(0, 1),
                            new LineIndices(1, 2),
                        },
                    fillIndices: new byte[]{ 0, 2, 1 }
                ),

                // [0010] Right
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.5f, 0.5f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(2, 1),
                            new LineIndices(1, 0),
                        },
                    fillIndices: new byte[]{ 1, 2, 0 }
                ),

                // [0011] Left, Right
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(0, 1),
                            new LineIndices(3, 2),
                        },
                    fillIndices: new byte[]{ 0, 2, 1, 1, 2, 3 }
                ),

                // [0100] Top
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(0.5f, 0.5f),
                            new Vector2(1.0f, 0.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(2, 1),
                            new LineIndices(1, 0),
                        },
                    fillIndices: new byte[]{ 1, 2, 0 }
                ),

                // [0101] Top, Left
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(1, 2),
                        },
                    fillIndices: new byte[]{ 1, 0, 2 }
                ),

                // [0110] Top, Right
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(2, 0),
                        },
                    fillIndices: new byte[]{ 2, 1, 0 }
                ),

                // [0111] Top, Left, Right
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(3, 2),
                        },
                    fillIndices: new byte[]{ 0, 2, 1, 1, 2, 3 }
                ),

                // [1000] Bottom
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 1.0f),
                            new Vector2(0.5f, 0.5f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(0, 1),
                            new LineIndices(1, 2),
                        },
                    fillIndices: new byte[]{ 0, 2, 1 }
                ),

                // [1001] Bottom, Left
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                            new Vector2(0.0f, 0.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(2, 1),
                        },
                    fillIndices: new byte[]{ 2, 0, 1 }
                ),

                // [1010] Bottom, Right
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                            new Vector2(1.0f, 0.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(0, 2),
                        },
                    fillIndices: new byte[]{ 0, 1, 2 }
                ),

                // [1011] Bottom, Left, Right
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(0, 1),
                        },
                    fillIndices: new byte[]{ 0, 2, 1, 1, 2, 3 }
                ),

                // [1100] Top, Bottom
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(2, 0),
                            new LineIndices(1, 3),
                        },
                    fillIndices: new byte[]{ 0, 2, 1, 1, 2, 3 }
                ),

                // [1101] Top, Bottom, Left
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(1, 3),
                        },
                    fillIndices: new byte[]{ 0, 2, 1, 1, 2, 3 }
                ),

                // [1110] Top, Bottom, Right
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] {
                            new LineIndices(2, 0),
                        },
                    fillIndices: new byte[]{ 0, 2, 1, 1, 2, 3 }
                ),

                // [1111] Full
                new Template(
                    vertices: new Vector2[] {
                            new Vector2(0.0f, 0.0f),
                            new Vector2(1.0f, 0.0f),
                            new Vector2(0.0f, 1.0f),
                            new Vector2(1.0f, 1.0f),
                        },
                    lineIndices: new LineIndices[] { },
                    fillIndices: new byte[]{ 0, 2, 1, 1, 2, 3 }
                ),
            };

        Vector2 uvScale;
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<int> triangles = new List<int>();

        public Geometry(Vector2 uvScale)
        {
            this.uvScale = uvScale;
        }

        public void Clear()
        {
            vertices.Clear();
            uv.Clear();
            triangles.Clear();
        }

        public void EmitCell(int x, int y, int mask)
        {
            Template template = Templates[mask];

            var xy = new Vector2(x, y);

            // Вершины для лицевой стороны

            int faceIndex = vertices.Count;
            foreach (var v in template.vertices) {
                vertices.Add(new Vector3(v.x + xy.x, v.y + xy.y, 0));
                uv.Add((xy + v) * uvScale);
            }

            // Треугольники для лицевой стороны

            foreach (var i in template.fillIndices)
                triangles.Add(faceIndex + i);

            // Боковая сторона

            foreach (var l in template.lineIndices) {
                Vector2 p1 = template.vertices[l.i1] + xy;
                Vector2 p2 = template.vertices[l.i2] + xy;

                int sideIndex = vertices.Count;
                vertices.Add(new Vector3(p1.x, p1.y, 0));
                vertices.Add(new Vector3(p2.x, p2.y, 0));
                vertices.Add(new Vector3(p1.x, p1.y, 1));
                vertices.Add(new Vector3(p2.x, p2.y, 1));
                uv.Add((new Vector2(x, y) + p1) * uvScale);
                uv.Add((new Vector2(x, y) + p2) * uvScale);
                uv.Add((new Vector2(x, y) + p1) * uvScale);
                uv.Add((new Vector2(x, y) + p2) * uvScale);

                triangles.Add(sideIndex + 1);
                triangles.Add(sideIndex + 2);
                triangles.Add(sideIndex + 0);
                triangles.Add(sideIndex + 2);
                triangles.Add(sideIndex + 1);
                triangles.Add(sideIndex + 3);
            }
        }

        public void EmitSurface(int x, int y, int w, int h)
        {
            Template template = Templates[15];
            var xy = new Vector2(x, y);
            var wh = new Vector2(w, h);

            int faceIndex = vertices.Count;
            foreach (var v in template.vertices) {
                vertices.Add(new Vector3(v.x * wh.x + xy.x, v.y * wh.y + xy.y, 0));
                uv.Add((xy + v * wh) * uvScale);
            }

            foreach (var i in template.fillIndices)
                triangles.Add(faceIndex + i);
        }

        public void UpdateMesh(Mesh mesh)
        {
            DebugOnly.Message($"Verts: {vertices.Count}  Tris: {triangles.Count/3}");

            mesh.Clear();
            mesh.SetVertices(vertices);
            mesh.SetUVs(0, uv);
            mesh.SetTriangles(triangles, 0);
            mesh.RecalculateNormals();
        }
    }
}
