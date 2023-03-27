using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Tema1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ex3(sender ,e);
        }

        private void ex3(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Red, 2);
            Random rnd = new Random();

            int n = rnd.Next(5, 10);
            double d = 0; double dmin = int.MaxValue;
            int xmin = 0, ymin = 0;

            float xq = rnd.Next(10, this.Size.Width - 10);
            float yq = rnd.Next(10, this.Size.Height - 10);

            g.DrawEllipse(p, xq, yq, 2, 2);

            p = new Pen(Color.Black, 2);

            for (int i = 0; i < n; i++)
            {
                int x = rnd.Next(10, this.Size.Width - 10);
                int y = rnd.Next(10, this.Size.Height - 10);

                g.DrawEllipse(p, x, y, 2, 2);

                d = Math.Sqrt(Math.Pow(xq - x, 2) + Math.Pow(yq - y, 2));

                if (d < dmin)
                {
                    xmin = x;
                    ymin = y;
                    dmin = d;
                }
            }

            p = new Pen(Color.Black, 2);

            g.DrawLine (p, xq, yq, xmin, ymin);

            float dmin2 = (float)dmin;

            g.DrawEllipse(p, xq - dmin2, yq - dmin2, dmin2 + dmin2, dmin2 + dmin2);
        }

        private void ex2(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Red, 2);
            Random rnd = new Random();

            int n = rnd.Next(5, 10);
            int[] xs = new int[n];
            int[] ys = new int[n];
            int i;
            double d = 0, dmin = int.MaxValue;
            int xmin = 0, ymin = 0;

            for (i = 0; i < n; i++)
            {
                xs[i] = rnd.Next(10, this.Size.Width - 10);
                ys[i] = rnd.Next(10, this.Size.Height - 10);
                g.DrawEllipse(p, xs[i], ys[i], 2, 2);
            }
            p = new Pen(Color.Green, 2);

            for (i = 0; i < n; i++)
            {
                int x = rnd.Next(10, this.Size.Width - 10);
                int y = rnd.Next(10, this.Size.Height - 10);
                g.DrawEllipse(p, x, y, 2, 2);

                for (int j = 0; j < n; j++)
                {
                    d = Math.Sqrt(Math.Pow(x - xs[j], 2) + Math.Pow(y - ys[j], 2));
                    if (d < dmin)
                    {
                        dmin = d;
                        xmin = xs[j];
                        ymin = ys[j];
                    }
                }

                p = new Pen(Color.Black, 2);

                g.DrawLine(p, xmin, ymin, x, y);

                p = new Pen(Color.Green, 2);

                dmin = int.MaxValue;
            }
        }

        private void ex1 (object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 2);
            Random rnd = new Random();

            int n = rnd.Next(5, 10);
            int xmin = this.Size.Width, xmax = 0, ymin = this.Size.Height, ymax = 0;
            int x, y;

            for (int i = 0; i < n; i++)
            {
                x = rnd.Next(10, this.Size.Width - 10);
                y = rnd.Next(10, this.Size.Height - 10);

                g.DrawEllipse(p, x, y, 2, 2);

                if (x < xmin)
                    xmin = x;
                else if (x > xmax)
                    xmax = x;
                if (y < ymin)
                    ymin = y;
                else if (y > ymax)
                    ymax = y;
            }

            g.DrawRectangle(p, xmin - 1, ymin - 1, xmax - xmin + 1, ymax - ymin + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
