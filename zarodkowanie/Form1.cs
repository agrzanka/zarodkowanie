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
        SolidBrush[] brush = new SolidBrush[5];
        int maxSize;

        public Form1()
        {
            InitializeComponent();
            brush[0] = new SolidBrush(Color.Bisque);
            brush[1] = new SolidBrush(Color.DarkTurquoise);
            brush[2] = new SolidBrush(Color.Black);
            brush[3] = new SolidBrush(Color.Tomato);
            brush[4] = new SolidBrush(Color.RoyalBlue);

            maxSize = (panel1.Width < panel1.Height) ? panel1.Width : panel1.Height;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int size = (int)numericUpDown1.Value;
            int bH = (int)numericUpDown2.Value;
            int zarodki = (int)numericUpDown3.Value;

            int cellSize = (size > bH) ? maxSize / size : maxSize / bH;

            int width = cellSize * size;
            int height = cellSize * bH;
            panel1.Width = width;
            panel1.Height = height;

            panel1.Refresh();
            Board board = new Board(size, bH);
            Zarodkowanie zarodkowanie =new Zarodkowanie(board, bH, cellSize);

            Pen pen = new Pen(Color.MediumVioletRed, 1f);
            //SolidBrush brush = new SolidBrush(Color.MediumVioletRed);
            Graphics graphics = panel1.CreateGraphics();

            zarodkowanie.startBoard.setup_randomly(zarodki);
            zarodkowanie.drawResult(width, height, graphics, pen, brush,zarodki);

            //for (int i = 1; i < 5; i++)

            int zeroes = 0;
            foreach(var c in board.cells)
            {
                if (c.Life == 0)
                    zeroes++;
            }

            while (zeroes > 0)
            {

                Thread.Sleep(300);
                panel1.Refresh();
                zarodkowanie.startBoard.update(zarodki);
                zarodkowanie.drawResult(width, height, graphics, pen, brush, zarodki);

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
