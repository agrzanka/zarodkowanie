using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zarodkowanie
{
    class Monte_Carlo
    {
        public Zarodkowanie zarodkowanie;
        public double kt;
        List<int> idds;


        public Monte_Carlo(Zarodkowanie zarodkowanie, double kt)
        {
            this.zarodkowanie = zarodkowanie;
            this.kt = kt;

            this.idds = new List<int>();
            for(int i=0;i<(zarodkowanie.startBoard.boardH*zarodkowanie.startBoard.size);i++)
            {
                idds.Add(i);
            }
        }

        public void compute()
        {
            int deltaE;
            double prob;//random
            Random probability = new Random();
            Random r = new Random();
            Random rn = new Random();
            double p;//probability from MC formula

            while(idds.Count()>0)
            {
                int rr = r.Next(0, idds.Count());
                int x = rr % zarodkowanie.startBoard.size;
                int y = rr / zarodkowanie.startBoard.size;

                int n = rn.Next(0, 7);
                
                int newLife = zarodkowanie.startBoard.cells[zarodkowanie.startBoard.cells[x, y].baseNeighbors[n][0], zarodkowanie.startBoard.cells[x, y].baseNeighbors[n][1]].Life;
                int e1 = zarodkowanie.startBoard.cells[x, y].checkEnergy(newLife, zarodkowanie.startBoard.cells);

                deltaE = zarodkowanie.startBoard.cells[x, y].energy - e1;

                if(deltaE>=0)
                {
                    p = Math.Exp(-((double)deltaE / kt));
                    prob = probability.NextDouble();

                    if (prob < p)
                        zarodkowanie.startBoard.cells[x, y].Life = newLife;
                }

                idds.RemoveAt(rr);
            }

            zarodkowanie.startBoard.setEnergy();
        }
    }
}
