using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ex1(sender, e);
        }

        private void ex3(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 3);
            Random rnd = new Random();

            int n = rnd.Next(5, 15);
            Point[] points = new Point[n];
            float d = 0; float dmax = 0;

            for (int i = 0; i < n; i++)
            {
                points[i].X = rnd.Next(100, 300);
                points[i].Y = rnd.Next(100, 300);
                g.DrawEllipse(p, points[i].X, points[i].Y, 5, 5);
            }

            Point centru = new Point();
            float radius = 0;
            p = new Pen(Color.Green, 3);


            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    d = (float)Math.Sqrt(Math.Pow(points[i].X - points[j].X, 2) +
                                         Math.Pow(points[j].Y - points[i].Y, 2));

                    if (d > dmax)
                    {
                        centru.X = (points[i].X + points[j].X) / 2;
                        centru.Y = (points[i].Y + points[j].Y) / 2;
                        radius = (d + 5) / 2;
                        dmax = d;
                    }
                    
                }

            g.DrawEllipse(p, centru.X - radius, centru.Y - radius, radius + radius, radius + radius);

        }

        private void ex2(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 3);
            Random rnd = new Random();

            int n = rnd.Next(10, 20);
            Point[] points = new Point[n];
            Point c = new Point();
            float arie = 0, ariemin = int.MaxValue;

            for (int i = 0; i < n; i++)
            {
                points[i].X = rnd.Next(100, 600);
                points[i].Y = rnd.Next(100, 300);
                g.DrawEllipse(p, points[i].X, points[i].Y, 5, 5);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (points[i].X * points[j].Y < points[j].X * points[i].Y)
                    {
                        c = points[i];
                        points[i] = points[j];
                        points[j] = c;
                    }
                }
            }

            Point A = new Point();
            Point B = new Point();
            Point C = new Point();

            for (int i = 0; i < n - 2; i++)
            {
                //x1y2 + x2y3 + x3y1 - x3y2 - x1y3 - x2y1
                arie = (points[i].X * points[i + 1].Y + points[i + 1].X * points[i + 2].Y + points[i + 2].X + points[i].Y - points[i + 2].X * points[i + 1].Y - points[i].X * points[i + 2].Y - points[i + 1].X * points[i].Y) / 2;
                if (arie < ariemin)
                {
                    A = points[i];
                    B = points[i + 1];
                    C = points[i + 2];
                    ariemin = arie;
                }
            }

            p = new Pen(Color.Green, 2);
            g.DrawLine(p, A.X, A.Y, B.X, B.Y);
            g.DrawLine(p, B.X, B.Y, C.X, C.Y);
            g.DrawLine(p, C.X, C.Y, A.X, A.Y);

        }

        private void ex1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 3);
            Random rnd = new Random();

            int n = rnd.Next(5, 15);
            int xq = rnd.Next(100, 600);
            int yq = rnd.Next(100, 300);

            g.DrawEllipse(p, xq, yq, 5, 5);

            float dconst = 100;
            float d;

            for (int i = 0; i < n; i++)
            {
                int x = rnd.Next(100, 600);
                int y = rnd.Next(100, 300);

                d = (float)Math.Sqrt(Math.Pow(xq - x, 2) + Math.Pow(yq - y, 2));

                if (d <= dconst)
                {
                    p = new Pen(Color.Green, 3);
                    g.DrawEllipse(p, x, y, 5, 5);
                }
                else
                {
                    p = new Pen(Color.Red, 3);
                    g.DrawEllipse(p, x, y, 5, 5);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
