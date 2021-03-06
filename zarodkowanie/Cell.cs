﻿using System;
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
        public int idd;
        public List<int[]> neighbors = new List<int[]>();
        public int numOfNeigh;
        public int[][] baseNeighbors;

        public int cellSize;

        //global barycentre
        public int bx;
        public int by;

        public int energy;

        public double ro;

        public bool rekryst;

        public Cell()
        {
            this.Life = 0;
            this.id[0] = 0;
            this.id[1] = 0;
        }

        public Cell(int[] id, int life, int boardSize, int idd, int cellSize)
        {
            this.id = id;
            this.Life = life;
            this.idd = idd;
            this.cellSize = cellSize;

            Random random = new Random();
            double localBX = random.NextDouble();
            double localBY = random.NextDouble();

            this.bx = ((int)(localBX * cellSize)) + cellSize * id[0];
            this.by = ((int)(localBY * cellSize)) + cellSize * id[1];

            this.ro = 0;
            this.energy = 0;
            this.rekryst = false;
        }

        public void setNeighbors(int boardSize, int boardH, int nType, int bcType, Cell[,] cells, int radius)
        {
            List<int> periodical = new List<int>();

            //setting coordinates for every possible neighbor
            int x1, x2, y1, y2;

            if (this.id[0] - 1 < 0)
            {
                x1 = boardSize - 1;
                periodical.Add(0);
                periodical.Add(1);
                periodical.Add(7);
            }
            else x1 = this.id[0] - 1;

            if (this.id[0] + 1 > boardSize - 1)
            {
                x2 = 0;
                periodical.Add(3);
                periodical.Add(4);
                periodical.Add(5);
            }
            else x2 = this.id[0] + 1;

            if (this.id[1] - 1 < 0)
            {
                y1 = boardH - 1;
                periodical.Add(1);
                periodical.Add(2);
                periodical.Add(3);
            }
            else y1 = this.id[1] - 1;

            if (this.id[1] + 1 > boardH - 1)
            {
                y2 = 0;
                periodical.Add(5);
                periodical.Add(6);
                periodical.Add(7);
            }
            else y2 = this.id[1] + 1;

            baseNeighbors = new int[8][] { new int[2] { x1, id[1] }, new int[2] { x1, y1 },new int[2] { id[0], y1 },
            new int[2]{x2,y1 },new int[2]{x2,id[1] },new int[2]{x2,y2 },new int[2]{id[0],y2 },new int[2]{x1,y2 } };

            switch (nType)
            {
                //==========vonNeumann==================
                case 1:
                    neighbors.Add(baseNeighbors[0]);
                    neighbors.Add(baseNeighbors[2]);
                    neighbors.Add(baseNeighbors[4]);
                    neighbors.Add(baseNeighbors[6]);
                    break;

                //========Pentagonal Left==================
                case 2:
                    neighbors.Add(baseNeighbors[0]);
                    neighbors.Add(baseNeighbors[1]);
                    neighbors.Add(baseNeighbors[2]);
                    neighbors.Add(baseNeighbors[6]);
                    neighbors.Add(baseNeighbors[7]);
                    break;


                //======Pentagonal Right===========
                case 3:
                    neighbors.Add(baseNeighbors[2]);
                    neighbors.Add(baseNeighbors[3]);
                    neighbors.Add(baseNeighbors[4]);
                    neighbors.Add(baseNeighbors[5]);
                    neighbors.Add(baseNeighbors[6]);
                    break;


                //======Pentagonal Upper==========
                case 4:
                    neighbors.Add(baseNeighbors[0]);
                    neighbors.Add(baseNeighbors[1]);
                    neighbors.Add(baseNeighbors[2]);
                    neighbors.Add(baseNeighbors[3]);
                    neighbors.Add(baseNeighbors[4]);
                    break;


                //========Pentagonal Bottom=====
                case 5:
                    neighbors.Add(baseNeighbors[4]);
                    neighbors.Add(baseNeighbors[5]);
                    neighbors.Add(baseNeighbors[6]);
                    neighbors.Add(baseNeighbors[7]);
                    neighbors.Add(baseNeighbors[0]);

                    break;


                //=====Hexagonal Left ==============
                case 6:
                    neighbors.Add(baseNeighbors[0]);
                    neighbors.Add(baseNeighbors[2]);
                    neighbors.Add(baseNeighbors[3]);
                    neighbors.Add(baseNeighbors[4]);
                    neighbors.Add(baseNeighbors[6]);
                    neighbors.Add(baseNeighbors[7]);
                    break;


                //====Hexagonal Right ===========
                case 7:
                    neighbors.Add(baseNeighbors[0]);
                    neighbors.Add(baseNeighbors[1]);
                    neighbors.Add(baseNeighbors[2]);
                    neighbors.Add(baseNeighbors[4]);
                    neighbors.Add(baseNeighbors[5]);
                    neighbors.Add(baseNeighbors[6]);
                    break;

                case 9:
                    foreach(var c in cells)
                    {
                        if(Math.Pow((c.bx-bx),2)+ Math.Pow((c.by - by), 2)<=Math.Pow(radius,2))
                        {
                            int[] n = new int[2] { c.id[0], c.id[1] };
                            neighbors.Add(n);
                        }
                    }
                    break;


                //case for promieniowe
                //random Pentagonal NIE MOŻE BYĆ w kejsach ani w difolcie!
                //może sprawę randoma w zakresie od 2 do 5 rozwiązać wyżej, poza kejsem? (linijki 50,51)
                //i wtedy nie będzie trzeba się martwić o deafault 

                //================Moore=================
                default:
                    neighbors.Add(baseNeighbors[0]);
                    neighbors.Add(baseNeighbors[1]);
                    neighbors.Add(baseNeighbors[2]);
                    neighbors.Add(baseNeighbors[3]);
                    neighbors.Add(baseNeighbors[4]);
                    neighbors.Add(baseNeighbors[5]);
                    neighbors.Add(baseNeighbors[6]);
                    neighbors.Add(baseNeighbors[7]);
                    break;
            }

            //=====for non-periodical boundary conditions======
            if(bcType==1)
            {
                foreach (var p in periodical)
                    neighbors.Remove(baseNeighbors[p]);
            }

            numOfNeigh = neighbors.Count();
        }

        public int checkEnergy(int newLife,Cell[,]cells)
        {
            int e = 0;

            for (int n = 0; n < 8; n++)
            {
                if (cells[baseNeighbors[n][0], baseNeighbors[n][1]].Life != newLife)
                {
                    e++;
                }
            }

            return e;
        }
    }
}
