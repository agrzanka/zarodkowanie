using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zarodkowanie
{
    class Board
    {
        public int size;
        public int boardH;
        public Cell[,] cells;
        public int neighborhoodType;
        public int boundaryConditionType;
        //public int numOfNeigh;

        public Board(int size, int boardH, int neighborhoodType, int bcType)
        {
            this.boardH = boardH;
            this.size = size;
            this.neighborhoodType = neighborhoodType;
            this.boundaryConditionType = bcType;

            cells = new Cell[this.size, this.boardH];

            for (int i = 0; i < size; i++)
            {
                for (int s = 0; s < boardH; s++)
                {
                    int[] idd = { i, s };
                    this.cells[i, s] = new Cell(idd, 0, size);
                }
            }

            foreach (var c in cells)
            {
                c.setNeighbors(this.size, this.boardH, this.neighborhoodType, this.boundaryConditionType);
            }

           // if (neighborhoodType == 1)
           //     numOfNeigh = 4;
          //  else if (neighborhoodType == 2 || neighborhoodType == 3 || neighborhoodType == 4 || neighborhoodType == 5)
            //    numOfNeigh = 5;
           // else if (neighborhoodType == 6 || neighborhoodType == 7)
           //     numOfNeigh = 6;
          //  else
          //      numOfNeigh = 8;


        }



        public void setup_randomly(int z)
        {
            Random x = new Random();
            Random y = new Random();

            for (int i = 1; i < z+1; i++)
            {
                int xx = x.Next(1, size - 1);
                int yy = y.Next(1, boardH - 1);

                cells[xx, yy].Life = i;
            }

        }


        public void setup_homogeneus(int z, int h, int w)
        {
            int zh;
            int zw;

            if (w>h)
            {
                zh = (int)Math.Sqrt(z);
                zw = z / zh;
            }
            else
            {
                zw = (int)Math.Sqrt(z);
                zh = z / zw;
            }

            int sw = (w - zw) / (zw + 1);
            int sh = (h - zh) / (zh + 1);

            for(int j=1;j<zh;j++)
            {
                for(int i=1;i<zw;i++)
                {
                    cells[i * sw, j * sh].Life = (i + zw * (j - 1));
                }
            }


        }

        public void setup_manually(int x, int y, int z)
        {
            cells[x, y].Life = z;
        }


        public void update(int z)
        {
            List<int[]>[] nextRound = new List<int[]>[z+1];
            for (int nR = 0; nR < z + 1; nR++)
                nextRound[nR] = new List<int[]>();
            foreach (var c in this.cells)
            {
                int [] alive =new int[z];
                for (int i = 0; i < z; i++)
                    alive[i] = 0;

                for (int neigh = 0; neigh < c.numOfNeigh; neigh++)
                {
                    if (this.cells[c.neighbors[neigh] [0], c.neighbors[neigh][ 1]].Life >0)
                        alive[this.cells[c.neighbors[neigh][ 0], c.neighbors[neigh][1]].Life-1]++;
                }

                int colour=0;
                int n = 0;

                for(int i=0;i<z;i++)
                {
                    if (n < alive[i])
                    {
                        colour = i+1;
                        n = alive[i];
                    }
                }

                nextRound[colour].Add(c.id);
            }

            for (int i=0;i<z+1;i++)
            {
                for (int j=0;j<nextRound[i].Count;j++)
                {
                    if(cells[nextRound[i][j][0], nextRound[i][j][1]].Life==0)
                        cells[nextRound[i][j][0], nextRound[i][j][1]].Life = i;
                }
            }
        }
    }
}
