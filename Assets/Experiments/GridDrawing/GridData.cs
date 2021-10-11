using UnityEngine;

namespace Experiments
{
    public sealed class GridData
    {
        public const int Width = 100;
        public const int Height = 100;

        public readonly bool[] Cells = new bool[Width * Height];

        public GridData()
        {
            for (int i = 0; i < Width * Height; i++)
                Cells[i] = true;
        }

        public bool HasCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return false;
            return Cells[y * Width + x];
        }

        public int CellMask(int x, int y)
        {
            const int L = 1;
            const int R = 2;
            const int T = 4;
            const int B = 8;

            int mask = 0;
            if (HasCell(x - 1, y))
                mask |= L;
            if (HasCell(x + 1, y))
                mask |= R;
            if (HasCell(x, y - 1))
                mask |= T;
            if (HasCell(x, y + 1))
                mask |= B;

            return mask;
        }
    }
}
