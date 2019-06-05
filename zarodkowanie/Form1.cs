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
    public partial class Form1 : Form
    {
        SolidBrush[] brush = new SolidBrush[25];
        int maxSize;
        bool manualMode = false;
        Graphics graphics;
        Board board;
        Zarodkowanie zarodkowanie;
        int zarodki;
        int z;
        int cellSize;

        int nh;
        int bc;

        int width;
        int height;

        int size;
        int bH;


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
            brush[10] = new SolidBrush(Color.Yellow);
            brush[11] = new SolidBrush(Color.Tan);
            brush[12] = new SolidBrush(Color.HotPink);
            brush[13] = new SolidBrush(Color.Gainsboro);
            brush[14] = new SolidBrush(Color.Fuchsia);
            brush[15] = new SolidBrush(Color.ForestGreen);
            brush[16] = new SolidBrush(Color.Firebrick);
            brush[17] = new SolidBrush(Color.Violet);
            brush[18] = new SolidBrush(Color.Thistle);
            brush[19] = new SolidBrush(Color.Teal);
            brush[20] = new SolidBrush(Color.SaddleBrown);
            brush[21] = new SolidBrush(Color.SpringGreen);
            brush[22] = new SolidBrush(Color.Silver);
            brush[23] = new SolidBrush(Color.RoyalBlue);
            brush[24] = new SolidBrush(Color.Red);

            maxSize = (pictureBox1.Width < pictureBox1.Height) ? pictureBox1.Width : pictureBox1.Height;
        }


        private void button3_Click(object sender, EventArgs e) //random
        {
            size = (int)numericUpDown1.Value;
            bH = (int)numericUpDown2.Value;
            nh=domainUpDown2.SelectedIndex;
            bc = domainUpDown1.SelectedIndex;

            if(nh==8)
            {
                Random r = new Random();
                nh = r.Next(2, 5);
            }

            zarodki = (int)numericUpDown3.Value;
            int maxZ = size * bH;
            if (zarodki > maxZ)
            {
                zarodki = maxZ;
                numericUpDown3.Value = maxZ;
            }

            cellSize = (size > bH) ? maxSize / size : maxSize / bH;

            int width = cellSize * size;
            int height = cellSize * bH;
            pictureBox1.Width = width;
            pictureBox1.Height = height;

            pictureBox1.Refresh();
            board = new Board(size, bH, nh, bc, cellSize);
            

            zarodkowanie = new Zarodkowanie(board, bH, cellSize);

            graphics = pictureBox1.CreateGraphics();

            zarodkowanie.startBoard.setup_randomly(zarodki);
            zarodkowanie.drawResult(width, height, graphics, brush, zarodki);

            int zeroes = 0;
            foreach (var c in board.cells)
            {
                if (c.Life == 0)
                    zeroes++;
            }

            while (zeroes > 0)
            {

                Thread.Sleep(300);
                
                zarodkowanie.startBoard.update(zarodki);
                zarodkowanie.drawResult(width, height, graphics, brush, zarodki);

                zeroes = 0;
                foreach (var c in board.cells)
                {
                    if (c.Life == 0)
                        zeroes++;
                }
            }
        }
    
        private void button4_Click(object sender, EventArgs e) //manual
        {
            nh = domainUpDown2.SelectedIndex;
            bc = domainUpDown1.SelectedIndex;

            size = (int)numericUpDown1.Value;
            bH = (int)numericUpDown2.Value;
            zarodki = (int)numericUpDown3.Value;
            int maxZ = size * bH;
            if (zarodki > maxZ)
            {
                zarodki = maxZ;
                numericUpDown3.Value = maxZ;
            }

            z = 1;
            cellSize = (size > bH) ? maxSize / size : maxSize / bH;

            width = cellSize * size;
            height = cellSize * bH;

            pictureBox1.Height = height;
            pictureBox1.Width = width;

            pictureBox1.Refresh();

            board = new Board(size, bH, nh,bc, cellSize);
            zarodkowanie = new Zarodkowanie(board, bH, cellSize);
            graphics = pictureBox1.CreateGraphics();

            manualMode = true;
        }

        private void button5_Click(object sender, EventArgs e) //start do manuala
        {
            manualMode = false;
            zarodkowanie.drawResult(width, height, graphics, brush, zarodki);

            int zeroes = 0;
            foreach (var c in board.cells)
            {
                if (c.Life == 0)
                    zeroes++;
            }

            while (zeroes > 0)
            {

                Thread.Sleep(300);
                zarodkowanie.startBoard.update(zarodki);
                zarodkowanie.drawResult(width, height, graphics,brush, zarodki);

                zeroes = 0;
                foreach (var c in board.cells)
                {
                    if (c.Life == 0)
                        zeroes++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //jednorodnie
        {
            size = (int)numericUpDown1.Value;
            bH = (int)numericUpDown2.Value;
            nh = domainUpDown2.SelectedIndex;
            bc = domainUpDown1.SelectedIndex;

            if (nh == 8)
            {
                Random r = new Random();
                nh = r.Next(2, 5);
            }

            zarodki = (int)numericUpDown3.Value;
            int maxZ = (int)Math.Sqrt(size * bH);
            if (zarodki > maxZ)
            {
                zarodki = maxZ;
                numericUpDown3.Value = maxZ;
            }

                cellSize = (size > bH) ? maxSize / size : maxSize / bH;

            int width = cellSize * size;
            int height = cellSize * bH;
            pictureBox1.Width = width;
            pictureBox1.Height = height;

            pictureBox1.Refresh();
            board = new Board(size, bH, nh, bc, cellSize);


            zarodkowanie = new Zarodkowanie(board, bH, cellSize);

            graphics = pictureBox1.CreateGraphics();

            zarodkowanie.startBoard.setup_homogeneus(zarodki, bH,size);
            zarodkowanie.drawResult(width, height, graphics, brush, zarodki);

            numericUpDown3.Value = zarodki;

            int zeroes = 0;
            foreach (var c in board.cells)
            {
                if (c.Life == 0)
                    zeroes++;
            }

            while (zeroes > 0)
            {

                Thread.Sleep(300);
           
                zarodkowanie.startBoard.update(zarodki);
                zarodkowanie.drawResult(width, height, graphics, brush, zarodki);

                zeroes = 0;
                foreach (var c in board.cells)
                {
                    if (c.Life == 0)
                        zeroes++;
                }
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            if (manualMode == true)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Left)
                {
                    if (me.X >= pictureBox1.Width || me.Y >= pictureBox1.Height)
                        return;
                    if (me.X < 0 || me.Y < 0)
                        return;

                    if (zarodkowanie.startBoard.cells[me.X / cellSize, me.Y / cellSize].Life < 1)
                    {
                        zarodkowanie.startBoard.setup_manually(me.X / cellSize, me.Y / cellSize, z);
                        z++;
                    }
                    if (z >zarodki)
                        manualMode = false;

                    zarodkowanie.drawResult(width, height, graphics, brush, z);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //promieniowe
        {
            size = (int)numericUpDown1.Value;
            bH = (int)numericUpDown2.Value;
            nh = domainUpDown2.SelectedIndex;
            bc = domainUpDown1.SelectedIndex;

            if (nh == 8)
            {
                Random r = new Random();
                nh = r.Next(2, 5);
            }

            zarodki = (int)numericUpDown3.Value;

            cellSize = (size > bH) ? maxSize / size : maxSize / bH;

            int width = cellSize * size;
            int height = cellSize * bH;
            pictureBox1.Width = width;
            pictureBox1.Height = height;

            pictureBox1.Refresh();
            board = new Board(size, bH, nh, bc, cellSize);


            zarodkowanie = new Zarodkowanie(board, bH, cellSize);

            graphics = pictureBox1.CreateGraphics();

            zarodkowanie.startBoard.setup_radial(zarodki);

            numericUpDown3.Value = zarodkowanie.startBoard.zarodki;

            zarodkowanie.drawResult(width, height, graphics, brush, zarodki);



            int zeroes = 0;
            foreach (var c in board.cells)
            {
                if (c.Life == 0)
                    zeroes++;
            }

            while (zeroes > 0)
            {

                Thread.Sleep(300);

                zarodkowanie.startBoard.update(zarodki);
                zarodkowanie.drawResult(width, height, graphics, brush, zarodki);

                zeroes = 0;
                foreach (var c in board.cells)
                {
                    if (c.Life == 0)
                        zeroes++;
                }
            }
        }
    }
    }