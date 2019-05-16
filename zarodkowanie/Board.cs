using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife2D
{
    class Board
    {
        public int size;
        public int boardH;
        public Cell[,] cells;

        public Board(int size, int boardH)
        {
            this.boardH = boardH;
            this.size = size;


            cells = new Cell[this.size, this.boardH];

            for (int i = 0; i < size; i++)
            {
                for (int s = 0; s < boardH; s++)
                {
                    int[] idd = { i, s };
                    this.cells[i, s] = new Cell(idd, false, size);
                }
            }

            foreach (var c in cells)
            {
                c.setNeighbors(this.size, this.boardH);
            }
        }

        

        public void setup_const()
        {
            this.cells[1, 1].Life = true;
            this.cells[2, 1].Life = true;
            this.cells[1, 2].Life = true;
            this.cells[2, 2].Life = true;
        }


        public void update()
        {
            List<int[]> nextRoundAlive = new List<int[]>();

            foreach (var c in this.cells)
            {
                int alive = 0;
                for (int n = 0; n < 8; n++)
                {
                    if (this.cells[c.neighbors[n, 0], c.neighbors[n, 1]].Life == true)
                        alive++;
                }
                if (c.Life == true && (alive == 2 || alive == 3))
                    nextRoundAlive.Add(c.id);
                else if (c.Life == false && alive == 3)
                    nextRoundAlive.Add(c.id);
            }

            foreach (var c in this.cells)
            {
                if (nextRoundAlive.Contains(c.id))
                    c.Life = true;
                else
                    c.Life = false;
            }
        }
    }
}
