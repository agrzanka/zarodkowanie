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
        public int[,] neighbors;// = new int[8, 2];
      //  public int[,] nUsable;

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
        public void setNeighbors(int boardSize, int boardH, int nType)
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

           // if(nType==9)
           //     nType=Random z przedziału 2-5


            switch (nType)
            {
                //==========vonNeumann==================
                case 1: 
                    this.neighbors = new int[4, 2];

                    this.neighbors[0, 0] = x1;
                    this.neighbors[0, 1] = this.id[1];

                    this.neighbors[1, 0] = this.id[0];
                    this.neighbors[1, 1] = y1;

                    this.neighbors[2, 0] = x2;
                    this.neighbors[2, 1] = this.id[1];

                    this.neighbors[3, 0] = this.id[0];
                    this.neighbors[3, 1] = y2;
                    break;

                    //========Pentagonal Left==================
                case 2:
                    this.neighbors = new int[5, 2];

                    this.neighbors[0, 0] = x1;
                    this.neighbors[1, 0] = x1;
                    this.neighbors[2, 0] = this.id[0];
                    this.neighbors[3, 0] = this.id[0];
                    this.neighbors[4, 0] = x1;

                    this.neighbors[0, 1] = this.id[1];
                    this.neighbors[1, 1] = y1;
                    this.neighbors[2, 1] = y1;
                    this.neighbors[3, 1] = y2;
                    this.neighbors[4, 1] = y2;
                    break;


                //======Pentagonal Right===========
                case 3:
                    this.neighbors[0, 0] = neighbors[2, 0];
                    this.neighbors[1, 0] = neighbors[3, 0];
                    this.neighbors[2, 0] = neighbors[4, 0];
                    this.neighbors[3, 0] = neighbors[5, 0];
                    this.neighbors[4, 0] = neighbors[6, 0];

                    this.neighbors[0, 1] = neighbors[2, 1];
                    this.neighbors[1, 1] = neighbors[3, 1];
                    this.neighbors[2, 1] = neighbors[4, 1];
                    this.neighbors[3, 1] = neighbors[5, 1];
                    this.neighbors[4, 1] = neighbors[6, 1];
                    break;


                //======Pentagonal Upper==========
                case 4:
                    this.neighbors[0, 0] = neighbors[0, 0];
                    this.neighbors[1, 0] = neighbors[1, 0];
                    this.neighbors[2, 0] = neighbors[2, 0];
                    this.neighbors[3, 0] = neighbors[3, 0];
                    this.neighbors[4, 0] = neighbors[4, 0];

                    this.neighbors[0, 1] = neighbors[0, 1];
                    this.neighbors[1, 1] = neighbors[1, 1];
                    this.neighbors[2, 1] = neighbors[2, 1];
                    this.neighbors[3, 1] = neighbors[3, 1];
                    this.neighbors[4, 1] = neighbors[4, 1];
                    break;


                //========Pentagonal Bottom=====
                case 5:
                    this.neighbors[0, 0] = neighbors[4, 0];
                    this.neighbors[1, 0] = neighbors[5, 0];
                    this.neighbors[2, 0] = neighbors[6, 0];
                    this.neighbors[3, 0] = neighbors[7, 0];
                    this.neighbors[4, 0] = neighbors[0, 0];

                    this.neighbors[0, 1] = neighbors[4, 1];
                    this.neighbors[1, 1] = neighbors[5, 1];
                    this.neighbors[2, 1] = neighbors[6, 1];
                    this.neighbors[3, 1] = neighbors[7, 1];
                    this.neighbors[4, 1] = neighbors[0, 1];

                    break;


                //=====Hexagonal Left ==============
                case 6:
                    this.neighbors[0, 0] = neighbors[0, 0];
                    this.neighbors[1, 0] = neighbors[2, 0];
                    this.neighbors[2, 0] = neighbors[3, 0];
                    this.neighbors[3, 0] = neighbors[4, 0];
                    this.neighbors[4, 0] = neighbors[6, 0];
                    this.neighbors[5, 0] = neighbors[7, 0];

                    this.neighbors[0, 1] = neighbors[0, 1];
                    this.neighbors[1, 1] = neighbors[2, 1];
                    this.neighbors[2, 1] = neighbors[3, 1];
                    this.neighbors[3, 1] = neighbors[4, 1];
                    this.neighbors[4, 1] = neighbors[6, 1];
                    this.neighbors[5, 1] = neighbors[7, 1];
                    break;


                //====Hexagonal Right ===========
                case 7:
                    this.neighbors[0, 0] = neighbors[0, 0];
                    this.neighbors[1, 0] = neighbors[1, 0];
                    this.neighbors[2, 0] = neighbors[2, 0];
                    this.neighbors[3, 0] = neighbors[4, 0];
                    this.neighbors[4, 0] = neighbors[5, 0];
                    this.neighbors[5, 0] = neighbors[6, 0];

                    this.neighbors[0, 1] = neighbors[0, 1];
                    this.neighbors[1, 1] = neighbors[1, 1];
                    this.neighbors[2, 1] = neighbors[2, 1];
                    this.neighbors[3, 1] = neighbors[4, 1];
                    this.neighbors[4, 1] = neighbors[5, 1];
                    this.neighbors[5, 1] = neighbors[6, 1];
                    break;


                    //case for promieniowe
                    //random Pentagonal NIE MOŻE BYĆ w kejsach ani w difolcie!
                    //może sprawę randoma w zakresie od 2 do 5 rozwiązać wyżej, poza kejsem? (linijki 50,51)
                    //i wtedy nie będzie trzeba się martwić o deafault 

                    //================Moore=================
                default:
                    this.neighbors = new int[8, 2];

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
                    break;
            }


            
        }

        //non-periodical boundary condiditions:


    }
}


/*
 * neighborhood schema:
 
    ================ MOORE'A ====================================================

    usefullNeighbours=neighbours;


    ================ VON NEUMANNA ===============================================

    usefullNeighbors[0,0]=neighbors[0,0];
    usefullNeighbors[0,1]=neighbors[0,1];

    usefullNeighbors[1,0]=neighbors[2,0];
    usefullNeighbors[1,1]=neighbors[2,1];

    usefullNeighbors[2,0]=neighbors[4,0];
    usefullNeighbors[2,1]=neighbors[4,1];

    usefullNeighbors[3,0]=neighbors[6,0];
    usefullNeighbors[3,1]=neighbors[6,1];


    ================ HEXAGONALNE LEWE ============================================

    usefullNeighbors[0,0]=neighbors[0,0];
    usefullNeighbors[1,0]=neighbors[2,0];
    usefullNeighbors[2,0]=neighbors[3,0];
    usefullNeighbors[3,0]=neighbors[4,0];
    usefullNeighbors[4,0]=neighbors[6,0];
    usefullNeighbors[5,0]=neighbors[7,0];

    usefullNeighbors[0,1]=neighbors[0,1];
    usefullNeighbors[1,1]=neighbors[2,1];
    usefullNeighbors[2,1]=neighbors[3,1];
    usefullNeighbors[3,1]=neighbors[4,1];
    usefullNeighbors[4,1]=neighbors[6,1];
    usefullNeighbors[5,1]=neighbors[7,1];


    ================ HEXAGONALNE PRAWE ============================================

    usefullNeighbors[0,0]=neighbors[0,0];
    usefullNeighbors[1,0]=neighbors[1,0];
    usefullNeighbors[2,0]=neighbors[2,0];
    usefullNeighbors[3,0]=neighbors[4,0];
    usefullNeighbors[4,0]=neighbors[5,0];
    usefullNeighbors[5,0]=neighbors[6,0];

    usefullNeighbors[0,1]=neighbors[0,1];
    usefullNeighbors[1,1]=neighbors[1,1];
    usefullNeighbors[2,1]=neighbors[2,1];
    usefullNeighbors[3,1]=neighbors[4,1];
    usefullNeighbors[4,1]=neighbors[5,1];
    usefullNeighbors[5,1]=neighbors[6,1];


    ================ PENTAGONALNE LEWE =============================================

    usefullNeighbors[0,0]=neighbors[0,0];
    usefullNeighbors[1,0]=neighbors[1,0];
    usefullNeighbors[2,0]=neighbors[2,0];
    usefullNeighbors[3,0]=neighbors[6,0];
    usefullNeighbors[4,0]=neighbors[7,0];

    usefullNeighbors[0,1]=neighbors[0,1];
    usefullNeighbors[1,1]=neighbors[1,1];
    usefullNeighbors[2,1]=neighbors[2,1];
    usefullNeighbors[3,1]=neighbors[6,1];
    usefullNeighbors[4,1]=neighbors[7,1];


   ================ PENTAGONALNE PRAWE =============================================

    usefullNeighbors[0,0]=neighbors[2,0];
    usefullNeighbors[1,0]=neighbors[3,0];
    usefullNeighbors[2,0]=neighbors[4,0];
    usefullNeighbors[3,0]=neighbors[5,0];
    usefullNeighbors[4,0]=neighbors[6,0];

    usefullNeighbors[0,1]=neighbors[2,1];
    usefullNeighbors[1,1]=neighbors[3,1];
    usefullNeighbors[2,1]=neighbors[4,1];
    usefullNeighbors[3,1]=neighbors[5,1];
    usefullNeighbors[4,1]=neighbors[6,1];
    

    ================ PENTAGONALNE GÓRNE =============================================

    usefullNeighbors[0,0]=neighbors[0,0];
    usefullNeighbors[1,0]=neighbors[1,0];
    usefullNeighbors[2,0]=neighbors[2,0];
    usefullNeighbors[3,0]=neighbors[3,0];
    usefullNeighbors[4,0]=neighbors[4,0];

    usefullNeighbors[0,1]=neighbors[0,1];
    usefullNeighbors[1,1]=neighbors[1,1];
    usefullNeighbors[2,1]=neighbors[2,1];
    usefullNeighbors[3,1]=neighbors[3,1];
    usefullNeighbors[4,1]=neighbors[4,1];

    ================ PENTAGONALNE DOLNE =============================================

    usefullNeighbors[0,0]=neighbors[4,0];
    usefullNeighbors[1,0]=neighbors[5,0];
    usefullNeighbors[2,0]=neighbors[6,0];
    usefullNeighbors[3,0]=neighbors[7,0];
    usefullNeighbors[4,0]=neighbors[0,0];

    usefullNeighbors[0,1]=neighbors[4,1];
    usefullNeighbors[1,1]=neighbors[5,1];
    usefullNeighbors[2,1]=neighbors[6,1];
    usefullNeighbors[3,1]=neighbors[7,1];
    usefullNeighbors[4,1]=neighbors[0,1];


 */
