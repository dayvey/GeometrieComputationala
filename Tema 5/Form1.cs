using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //JARVIS

            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 3);
            Random rnd = new Random();

            int n = 30;
            Point[] points= new Point[n];

            for (int i = 0; i < n; i++)
            {
                points[i].X = rnd.Next(100, 700);
                points[i].Y = rnd.Next(100, 400);
                g.DrawEllipse(p, points[i].X, points[i].Y, 3, 3);
            }

            List<Point> invelitoare = new List<Point>();

            int cms = 0;
            for (int i = 0; i < n; i++)
                if (points[i].X < points[cms].X)
                    cms = i;

            int j = cms, q;
            do
            {
                invelitoare.Add(points[j]);
                q = (j + 1) % n;

                for (int i = 0; i < n; i++)
                {
                    if (ccw(points[j], points[i], points[q]) < 0)
                        q = i;
                }

                j = q;
            } while (j != cms);
            invelitoare.Add(points[j]);

            p = new Pen(Color.Green, 3);
            for (int i = 0; i < invelitoare.Count - 1; i++)
                g.DrawLine(p, invelitoare[i].X, invelitoare[i].Y, invelitoare[i + 1].X, invelitoare[i + 1].Y);
        }

        public int ccw (Point p, Point q, Point r)
        {
            int sum = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
            return sum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
