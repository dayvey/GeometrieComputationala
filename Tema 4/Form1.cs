using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 3);
            Random rnd = new Random();

            int n = 40;
            Point[] points = new Point[n];
            int i, j;

            for (i = 0; i < n; i++)
            {
                points[i].X = rnd.Next(100, 700);
                points[i].Y = rnd.Next(100, 400);
                g.DrawEllipse(p, points[i].X, points[i].Y, 3, 3);
            }

            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                {
                    if (points[i].X < points[j].X)
                    {
                        Point aux = new Point();
                        aux = points[i];
                        points[i] = points[j];
                        points[j] = aux;
                    }
                }

            Point[] L = new Point[n];
            Point[] lInf = new Point[2 * n];
            Point[] lSup = new Point[2 * n];

            lSup[0] = points[0];
            lSup[1] = points[1];

            j = 2;

            for(i = 2; i < n; i++)
            {
                lSup[j] = points[i];
                j++;
                while (j > 2 && (ccw(lSup[j-3], lSup[j-2], lSup[j-1]) > 0))
                {
                    lSup[j - 2] = lSup[j - 1];
                    j--;
                }
            }

            p = new Pen(Color.Green, 3);
            g.DrawEllipse(p, points[n - 1].X, points[n - 1].Y, 3, 3);

            p = new Pen(Color.Red, 3);
            for (i = 0; i < j - 1; i++)
                g.DrawLine(p, lSup[i].X, lSup[i].Y, lSup[i + 1].X, lSup[i + 1].Y);

            p = new Pen(Color.Blue, 3);

            lInf[0] = points[n - 1];
            lInf[1] = points[n - 2];

            j = 2;
            for (i = n - 3; i >= 0; i--)
            {
                lInf[j] = points[i];
                j++;
                while (j > 2 && (ccw(lInf[j - 3], lInf[j - 2], lInf[j - 1]) > 0))
                {
                    lInf[j - 2] = lInf[j - 1];
                    j--;
                }
            }

            for (i = 0; i < j - 1; i++)
                g.DrawLine(p, lInf[i].X, lInf[i].Y, lInf[i + 1].X, lInf[i + 1].Y);
        }

        public int ccw(Point p, Point q, Point r)
        {
            int sum = 0;
            sum = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
            return sum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
