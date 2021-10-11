using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Experiments
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class DrawableGrid : MonoBehaviour
    {
        const float TexRepeat = GridData.Width;

        Mesh mesh;
        MeshFilter meshFilter;
        MeshCollider meshCollider;

        GridData gridData = new GridData();
        Geometry geometry = new Geometry(new Vector2(TexRepeat / GridData.Width, TexRepeat / GridData.Height));
        readonly bool[] Visited = new bool[GridData.Width * GridData.Height];

        void Awake()
        {
            mesh = new Mesh();
            mesh.name = "Grid";

            meshFilter = GetComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;

            meshCollider = GetComponent<MeshCollider>();

            UpdateMesh();
        }

        void OnDisable()
        {
            Destroy(mesh);
        }

        void Update()
        {
            if (!Mouse.current.leftButton.IsPressed())
                return;

            var v = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(new Vector3(v.x, v.y, 0));
            if (Physics.Raycast(ray, out var hit)) {
                Vector3 vv = transform.InverseTransformPoint(hit.point);
                int cellX = Mathf.FloorToInt(vv.x);
                int cellY = Mathf.FloorToInt(vv.y);

                const int Size = 3;
                bool changed = false;
                for (int y = cellY - Size; y <= cellY + Size; y++) {
                    for (int x = cellX - Size; x <= cellX + Size; x++) {
                        if (x >= 0 && y >= 0 && x < GridData.Width && y < GridData.Height) {
                            Vector2 d = new Vector2(x, y) - new Vector2(cellX, cellY);
                            if (d.sqrMagnitude > Size * Size)
                                continue;

                            if (gridData.Cells[y * GridData.Width + x]) {
                                gridData.Cells[y * GridData.Width + x] = false;
                                changed = true;
                            }
                        }
                    }
                }

                if (changed)
                    UpdateMesh();
            }
        }

        public void UpdateMesh()
        {
            geometry.Clear();

            for (int i = 0; i < GridData.Width * GridData.Height; i++)
                Visited[i] = false;

            for (int y = 0; y < GridData.Height; y++) {
                for (int x = 0; x < GridData.Width; x++) {
                    int index = y * GridData.Width + x;
                    if (Visited[index])
                        continue;
                    Visited[index] = true;

                    if (!gridData.Cells[index])
                        continue;

                    int mask = gridData.CellMask(x, y);
                    if (mask != 15) {
                        geometry.EmitCell(x, y, mask);
                        continue;
                    }

                    // Пытаемся выделить большой сплошной прямоугольник

                    int w = 1;
                    for (int curX = x + 1; curX < GridData.Width; curX++) {
                        if (gridData.CellMask(curX, y) != 15 || Visited[y * GridData.Width + curX])
                            break;
                        w++;
                    }

                    int fullW = w;

                    int h = 1;
                    for (int curY = y + 1; curY < GridData.Height; curY++) {
                        int lineW = 0;
                        for (int curX = x; curX < GridData.Width; curX++) {
                            if (gridData.CellMask(curX, curY) != 15 || Visited[curY * GridData.Width + curX])
                                break;
                            ++lineW;
                        }

                        if (lineW == 0 || lineW < fullW / 2)
                            break;
                        if (lineW < w)
                            w = lineW;

                        ++h;
                    }

                    // Помечаем ячейки прямоугольника как посещенные

                    for (int ry = 0; ry < h; ry++) {
                        for (int rx = 0; rx < w; rx++)
                            Visited[(y + ry) * GridData.Width + (x + rx)] = true;
                    }

                    // Отрисовываем

                    geometry.EmitSurface(x, y, w, h);
                }
            }

            geometry.UpdateMesh(mesh);

            mesh.RecalculateBounds();
            meshCollider.sharedMesh = mesh;
        }
    }
}
