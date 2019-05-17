using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zarodkowanie
{
    class Cell
    {
        public int Life { get; set; }
        public int[] id = new int[2];
        public int[,] neighbors = new int[8, 2];


        public Cell()
        {
            this.Life = 0;
            this.id[0] = 0;
            this.id[1] = 0;
        }

        public Cell(int[] id, int life, int boardSize)
        {
            this.id = id;
            this.Life = life;

        }
        //periodical boundary conditions
        public void setNeighbors(int boardSize, int boardH)
        {
            int x1, x2, y1, y2;

            if (this.id[0] - 1 < 0)
                x1 = boardSize - 1;
            else x1 = this.id[0] - 1;

            if (this.id[0] + 1 > boardSize - 1)
                x2 = 0;
            else x2 = this.id[0] + 1;

            if (this.id[1] - 1 < 0)
                y1 = boardH - 1;
            else y1 = this.id[1] - 1;

            if (this.id[1] + 1 > boardH - 1)
                y2 = 0;
            else y2 = this.id[1] + 1;


            this.neighbors[0, 0] = x1;
            this.neighbors[0, 1] = this.id[1];

            this.neighbors[1, 0] = x1;
            this.neighbors[1, 1] = y1;

            this.neighbors[2, 0] = this.id[0];
            this.neighbors[2, 1] = y1;

            this.neighbors[3, 0] = x2;
            this.neighbors[3, 1] = y1;

            this.neighbors[4, 0] = x2;
            this.neighbors[4, 1] = this.id[1];

            this.neighbors[5, 0] = x2;
            this.neighbors[5, 1] = y2;

            this.neighbors[6, 0] = this.id[0];
            this.neighbors[6, 1] = y2;

            this.neighbors[7, 0] = x1;
            this.neighbors[7, 1] = y2;
        }

        //non-periodical boundary condiditions:


    }
}
