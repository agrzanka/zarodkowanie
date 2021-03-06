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

        public void drawResult(int width, int height, Graphics graphics, SolidBrush[] brush, int zarodki)
        {
            for (int i = 0; i < boardH; i++)
                for (int s = 0; s < startBoard.size; s++)
                    for(int colour=1;colour<zarodki+1;colour++)
                        if (startBoard.cells[s, i].Life == colour)
                            graphics.FillRectangle(brush[(colour-1)%25], s * this.cellSize, i * this.cellSize, this.cellSize, this.cellSize);
        }

        public void drawEnergy(int width, int height, Graphics graphics, SolidBrush[] energyBrush)
        {
            for (int i = 0; i < boardH; i++)
                for (int s = 0; s < startBoard.size; s++)
                    for (int colour = 0; colour < 8 ; colour++)
                        if (startBoard.cells[s, i].energy == colour)
                            graphics.FillRectangle(energyBrush[colour%8], s * this.cellSize, i * this.cellSize, this.cellSize, this.cellSize);
        }

        public void drawRDX(int width, int height, Graphics graphics)
        {
            SolidBrush bbb = new SolidBrush(Color.Black);
            for (int i = 0; i < boardH; i++)
                for (int s = 0; s < startBoard.size; s++)
                {
                   // for (int colour = 1; colour < zarodki + 1; colour++)
                    //    if (startBoard.cells[s, i].Life == colour)
                   //         graphics.FillRectangle(brush[(colour - 1) % 25], s * this.cellSize, i * this.cellSize, this.cellSize, this.cellSize);

                    if (startBoard.cells[s, i].rekryst == true)
                        graphics.FillRectangle(bbb, s * this.cellSize, i * this.cellSize, this.cellSize, this.cellSize);
                }
        }
    }
}
