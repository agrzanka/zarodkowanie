using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace zarodkowanie
{
    class DRX
    {
        public double A;
        public double B;
        public double timeStep;
        public double tMax;
        public double critical;

        public double ro;
        public double deltaRO;
        public double time;

        public List<int[]> lastRekr = new List<int[]>();

        public Zarodkowanie zarodkowanie;


        public DRX(double A, double B, double timeStep, double tMax, Zarodkowanie zarodkowanie)
        {
            this.A = A;
            this.B = B;
            this.timeStep = timeStep;
            this.tMax = tMax;
            this.zarodkowanie = zarodkowanie;
            this.critical = 4215840142323 / (this.zarodkowanie.startBoard.size * this.zarodkowanie.startBoard.boardH);
            this.time = 0;
            this.ro = 0;
        }

        public void compute(Graphics graphics)
        {
            int iterations = (int)(tMax / timeStep);

            for(int iter=0;iter<iterations;iter++)
            {
                
                singleIteration();
                time += timeStep;
                //zarodkowanie.drawRDX(zarodkowanie.startBoard.size, zarodkowanie.startBoard.boardH, graphics);
                SolidBrush bbb = new SolidBrush(Color.Black);
                for (int h = 0; h < zarodkowanie.startBoard.boardH; h++)
                    for (int s = 0; s < zarodkowanie.startBoard.size; s++)
                    {
                        // for (int colour = 1; colour < zarodki + 1; colour++)
                        //    if (startBoard.cells[s, i].Life == colour)
                        //         graphics.FillRectangle(brush[(colour - 1) % 25], s * this.cellSize, i * this.cellSize, this.cellSize, this.cellSize);

                        if (zarodkowanie.startBoard.cells[s, h].rekryst == true)
                            graphics.FillRectangle(bbb, s * zarodkowanie.cellSize, h * zarodkowanie.cellSize, zarodkowanie.cellSize, zarodkowanie.cellSize);
                    }
            }
        }

        public void singleIteration()
        {
            double AdivB = (A / B);
            double oneAdivB = 1 - AdivB;
            double expo = Math.Exp(-(B * time));
            double nextRo = AdivB + oneAdivB*expo;
            this.deltaRO = nextRo - ro;
            ro = nextRo;

            int numOfCells = zarodkowanie.startBoard.boardH * zarodkowanie.startBoard.size;
            double singleDeltaRO = deltaRO / numOfCells;

            Random c = new Random();
            Random probability = new Random();
            Random percentage = new Random();

            double percent = percentage.NextDouble();

            foreach(var cell in zarodkowanie.startBoard.cells)
            {
                cell.ro += (singleDeltaRO * 0.3);//percent);
            }

            deltaRO = deltaRO * (1.0 - percent);

            double prob;

            while(deltaRO>0)
            {
                percent = percentage.NextDouble();

                if (deltaRO < 0.01)
                    percent = 1.0;

                int idd = c.Next(0, numOfCells - 1);
                int x = idd % zarodkowanie.startBoard.size;
                int y = idd / zarodkowanie.startBoard.size;
                if (zarodkowanie.startBoard.cells[x, y].energy > 0)
                    prob = 0.8;
                else
                    prob = 0.2;

                double p = probability.NextDouble();

                if (p < prob)
                {
                    zarodkowanie.startBoard.cells[x, y].ro += deltaRO * percent;
                    deltaRO -= deltaRO * percent;
                }
            }

            //checking rules:

            List<int[]> rekr = new List<int[]>();

            foreach(var cell in zarodkowanie.startBoard.cells)
            {
                if (cell.rekryst==false)
                {
                    if (cell.ro > critical)
                    {
                        cell.rekryst = true;
                        rekr.Add(cell.id);
                    }

                    else
                    {
                        bool rule2a = false;
                        int r2a = 0;
                        bool rule2b = false;
                        int r2b = 0;

                        foreach (var n in cell.baseNeighbors)
                        {
                            if(lastRekr.Contains(n))
                            {
                                rule2a = true;
                                r2a++;
                            }
                            else
                            {
                                if (zarodkowanie.startBoard.cells[n[0], n[1]].ro > cell.ro)
                                {
                                    rule2b = true;
                                    r2b++;
                                }
                            }
                        }

                        if(rule2a==true&&rule2b==true&&(r2a+r2b)==8)
                        {
                            cell.rekryst = true;
                            rekr.Add(cell.id);
                        }
                    }
                }
            }

            lastRekr = rekr;
        }
    }
}
