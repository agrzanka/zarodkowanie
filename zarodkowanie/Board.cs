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

        public int zarodki=0;

        public Board(int size, int boardH, int neighborhoodType, int bcType)
        {
            this.boardH = boardH;
            this.size = size;
            this.neighborhoodType = neighborhoodType;
            this.boundaryConditionType = bcType;

            cells = new Cell[this.size, this.boardH];

            int idd = 0;

            for (int i = 0; i < size; i++)
            {
                for (int s = 0; s < boardH; s++)
                {
                    int[] id = { i, s };
                    this.cells[i, s] = new Cell(id, 0, size, idd);
                    idd++;
                }
            }

            foreach (var c in cells)
            {
                c.setNeighbors(this.size, this.boardH, this.neighborhoodType, this.boundaryConditionType);
            }
        }

        public void setup_randomly(int z)
        {
            this.zarodki = z;
            Random r = new Random();

            List<int> idds = new List<int>();

            for (int i = 1; i < z+1; i++)
            {
                int rr = r.Next(0,(boardH*size));
                if (idds.Contains(rr))
                    i--;
                else
                {
                    idds.Add(rr);
                    int x = rr % size;
                    int y = rr / size;

                    cells[x, y].Life = i;
                }
            }
        }

        public void setup_homogeneus(int z, int h, int w)
        {
            this.zarodki = z;
            double scale = (double)w / (double)h;
            double asqrt = z * scale;

            int a = (int)Math.Sqrt(asqrt);
            if ((a * a < asqrt))
                a = a + 1;
            int spaceX = (w - a) / (a + 1);
            a = w / (spaceX + 1);
            if (((spaceX * a + a) >= w - 1))
                a = a - 1; //final a
            if (a < 1)
                a = 1;

            int b = z / a;
            if (b * a < z)
                b = b + 1; //final b
            int spaceY = (h - b) / (b + 1);

            int counter = 1;

            do
            {
                for (int j = 0; j < b; j++)
                    for (int i = 0; i < a; i++)
                    {
                        cells[spaceX * (i + 1) + i, spaceY * (j + 1) + j].Life = counter;
                        counter++;
                        if (counter > z)
                            break;
                    }
            } while (counter < z);
        }

        public void setup_manually(int x, int y, int z)
        {
            if (this.zarodki < z)
                this.zarodki = z;
            cells[x, y].Life = z;
        }


        public void setup_radial(int z)
        {
            int i = 1;
            int trying = 0;

            Random r = new Random();

            List<int> idds = new List<int>();

            for ( i = 1; i < z + 1; i++)
            {
                int rr = r.Next(0, (boardH * size));
                if (idds.Contains(rr))
                    i--;
                else
                {
                    idds.Add(rr);
                    int x = rr % size;
                    int y = rr / size;

                    bool free = true;

                    for(int n=0;n<8;n++)
                    {
                        if(cells[cells[x,y].baseNeighbors[n][0], cells[x, y].baseNeighbors[n][1]].Life>0)
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free == true)
                    {
                        cells[x, y].Life = i;
                        trying = 0;
                    }
                    else
                    {
                        i--;
                        trying++;
                        if (trying >= 10)
                        {
                            z = i + 1;
                            break;
                        }
                    }
                }

                this.zarodki = z;
            }
            this.zarodki = i-1;
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
