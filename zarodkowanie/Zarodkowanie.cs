﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace zarodkowanie
{
    class Zarodkowanie
    {
        public int cellSize;
        public int boardH;
        public Board startBoard;

        public Zarodkowanie(Board board, int bH, int cellSize)
        {
            this.startBoard = board;

            this.boardH = bH;
            this.cellSize = cellSize;
        }

        public void drawResult(int width, int height, Graphics graphics, Pen pen, SolidBrush[] brush, int zarodki)
        {
            //for (int it = 0; it < width; it += cellSize)
            //    graphics.DrawLine(pen, it, 0, it, height);

           // for (int it = 0; it < height; it += cellSize)
            //    graphics.DrawLine(pen, 0, it, width, it);

            for (int i = 0; i < boardH; i++)
                for (int s = 0; s < startBoard.size; s++)
                    for(int colour=1;colour<zarodki+1;colour++)
                        if (startBoard.cells[s, i].Life == colour)
                            graphics.FillRectangle(brush[colour-1], s * this.cellSize + 1, i * this.cellSize + 1, this.cellSize - 1, this.cellSize - 1);

        }
    }
}
