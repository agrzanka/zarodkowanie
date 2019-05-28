using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace zarodkowanie
{

  /*  enum Neighborhood
    {
        Moore=0,
        vonNeumann=1,
        Pentagonal,
        Hexagonal
    }

    enum Pentagonal
    {
        Left=4,
        Right =5,
        Upper=6,
        Bottom=7
    }

    enum Hexagonal
    {
        Left=10,
        Right=11
    }*/

    public partial class Form1 : Form
    {
        SolidBrush[] brush = new SolidBrush[17];
        int maxSize;
        bool manualMode = true;
        Board board;
        Zarodkowanie zarodkowanie;
        int zarodki;
        int z;
        int cellSize;


        public Form1()
        {
            InitializeComponent();
            brush[0] = new SolidBrush(Color.Bisque);
            brush[1] = new SolidBrush(Color.DarkTurquoise);
            brush[2] = new SolidBrush(Color.Black);
            brush[3] = new SolidBrush(Color.Tomato);
            brush[4] = new SolidBrush(Color.RoyalBlue);
            brush[5] = new SolidBrush(Color.Blue);
            brush[6] = new SolidBrush(Color.Chartreuse);
            brush[7] = new SolidBrush(Color.CornflowerBlue);
            brush[8] = new SolidBrush(Color.DarkGoldenrod);
            brush[9] = new SolidBrush(Color.DodgerBlue);
            brush[10] = new SolidBrush(Color.Moccasin);
            brush[11] = new SolidBrush(Color.Ivory);
            brush[12] = new SolidBrush(Color.HotPink);
            brush[13] = new SolidBrush(Color.Gainsboro);
            brush[14] = new SolidBrush(Color.Fuchsia);
            brush[15] = new SolidBrush(Color.ForestGreen);
            brush[16] = new SolidBrush(Color.Firebrick);

            maxSize = (panel1.Width < panel1.Height) ? panel1.Width : panel1.Height;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int size = (int)numericUpDown1.Value;
            int bH = (int)numericUpDown2.Value;
            int nh=domainUpDown2.SelectedIndex;

            zarodki = (int)numericUpDown3.Value;

            cellSize = (size > bH) ? maxSize / size : maxSize / bH;

            int width = cellSize * size;
            int height = cellSize * bH;
            panel1.Width = width;
            panel1.Height = height;

            panel1.Refresh();
            board = new Board(size, bH, nh);

            int numOfNeigh = board.numOfNeigh;

            zarodkowanie = new Zarodkowanie(board, bH, cellSize);

            Pen pen = new Pen(Color.MediumVioletRed, 1f);
            //SolidBrush brush = new SolidBrush(Color.MediumVioletRed);
            Graphics graphics = panel1.CreateGraphics();

            zarodkowanie.startBoard.setup_randomly(zarodki);
            zarodkowanie.drawResult(width, height, graphics, pen, brush, zarodki);

            //for (int i = 1; i < 5; i++)

            int zeroes = 0;
            foreach (var c in board.cells)
            {
                if (c.Life == 0)
                    zeroes++;
            }

            while (zeroes > 0)
            {

                Thread.Sleep(300);
                //panel1.Refresh();
                zarodkowanie.startBoard.update(zarodki, numOfNeigh);
                zarodkowanie.drawResult(width, height, graphics, pen, brush, zarodki);

                zeroes = 0;
                foreach (var c in board.cells)
                {
                    if (c.Life == 0)
                        zeroes++;
                }
            }
        }

       /* private void panel1_Click(object sender, EventArgs e)
        {
            if (manualMode == true)
            {


                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Left)
                {
                    if (me.X >= panel1.Width || me.Y >= panel1.Height)
                        return;
                    if (me.X < 0 || me.Y < 0)
                        return;
                   // if (zarodkowanie.startBoard.cells[me.X / cellSize, me.Y / cellSize].Life > 0)
                   //     return;
                    zarodkowanie.startBoard.setup_manually(me.X / cellSize, me.Y / cellSize, z);
                    z--;
                    if (z <= 0)
                        manualMode = false;
                }
            }
        }*/

       /* private void button4_Click(object sender, EventArgs e)
        {
            int size = (int)numericUpDown1.Value;
            int bH = (int)numericUpDown2.Value;
            zarodki = (int)numericUpDown3.Value;
            z = zarodki;
            cellSize = (size > bH) ? maxSize / size : maxSize / bH;

            int width = cellSize * size;
            int height = cellSize * bH;
            panel1.Width = width;
            panel1.Height = height;

            panel1.Refresh();
            board = new Board(size, bH);
            zarodkowanie = new Zarodkowanie(board, bH, cellSize);

            Pen pen = new Pen(Color.MediumVioletRed, 1f);
            //SolidBrush brush = new SolidBrush(Color.MediumVioletRed);
            Graphics graphics = panel1.CreateGraphics();

            while (z > 0)
                manualMode = true;
           

            zarodkowanie.drawResult(width, height, graphics, pen, brush, zarodki);

            int zeroes = 0;
            foreach (var c in board.cells)
            {
                if (c.Life == 0)
                    zeroes++;
            }

            while (zeroes > 0)
            {

                Thread.Sleep(300);
                //panel1.Refresh();
                zarodkowanie.startBoard.update(zarodki);
                zarodkowanie.drawResult(width, height, graphics, pen, brush, zarodki);

                zeroes = 0;
                foreach (var c in board.cells)
                {
                    if (c.Life == 0)
                        zeroes++;
                }
            }

        }*/
    }
}