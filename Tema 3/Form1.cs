using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_3
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ex2(sender, e);
        }

        private void ex2(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 3);
            Random rnd = new Random();

            int s = 5;
            _2DLine[] Lines = new _2DLine[s];
            Point[] points = new Point[2 * s];
            bool[] isLeft = new bool[2 * s];
            int i = 0, j = 0;

            for (i = 0; i < s; i++)
            {
                Point a = new Point();
                Point b = new Point();
                a.X = rnd.Next(100, 700);
                a.Y = rnd.Next(100, 500);
                b.X = rnd.Next(100, 700);
                b.Y = rnd.Next(100, 500);
                Lines[i] = new _2DLine(a, b);
                points[j] = a;
                points[j + 1] = b;
                if (a.X < b.Y)
                {
                    isLeft[j] = true;
                    isLeft[j + 1] = false;
                }
                else
                {
                    isLeft[j] = false;
                    isLeft[j + 1] = true;
                }
                g.DrawEllipse(p, a.X, a.Y, 3, 3);
                g.DrawEllipse(p, b.X, b.Y, 3, 3);
                g.DrawLine(p, a.X, a.Y, b.X, b.Y);
                j += 2;
            }

            int nr = 0;

            //Point auxP = new Point();
            //bool auxB;

            //for (i = 0; i < 2 * s; i++)
            //{
            //    for (j = 0; j < 2 * s; j++)
            //        if (points[i].X > points[j].X)
            //        {
            //            auxP = points[i];
            //            points[i] = points[j];
            //            points[j] = auxP;
            //            auxB = isLeft[i];
            //            isLeft[i] = isLeft[j];
            //            isLeft[j] = auxB;
            //        }
            //}

            //_2DLine[] active = new _2DLine[2 * s];
            //int k = 0;
            //for (i = 0; i < 2*s; i++)
            //{
            //    for (j = 0; j < s; j++)
            //        if (points[i] == Lines[j].x || points[i] == Lines[j].y)
            //        {
            //            if (isLeft[i])
            //            {
            //                active[k] = new _2DLine(Lines[j].x, Lines[j].y);
            //                k++;
            //            }
            //            else
            //            {
            //                k--;
            //            }
            //        }
            //    if (k != 1)
            //    {
            //        Point a = Lines[0].x;
            //        Point b = Lines[0].y;
            //        Point c = Lines[k + 1].x;
            //        Point d = Lines[k + 1].y;
            //        if (doIntersect(a, b, c, d))
            //            nr++;
            //    }
            //}



            for (i = 0; i < s - 1; i++)
            {
                for (j = i + 1; j < s; j++)
                {
                    if (doIntersect(Lines[i].x, Lines[i].y, Lines[j].x, Lines[j].y))
                        nr++;
                }
            }
            Display(nr);
            
        }

        private void Display(int nr)
        {
            Console.WriteLine(nr);
        }

        static Boolean onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }

        static int orientation(Point p, Point q, Point r)
        {
            int val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;

            return (val > 0) ? 1 : 2;
        }

        static Boolean doIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            if (o1 != o2 && o3 != o4)
                return true;

            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false;
        }

        private void ex1(object sender, PaintEventArgs e) {
            Graphics g  = e.Graphics;
            Pen p = new Pen(Color.Black, 3);
            Random rnd = new Random();

            int n = rnd.Next(5, 10);
            n *= 2;
            float dmin = float.MaxValue, d = 0;
            int i, j;

            Point[] points = new Point[n];
            for (i = 0; i < n; i++)
            {
                points[i].X = rnd.Next(150, 1000);
                points[i].Y = rnd.Next(150, 400);
                g.DrawEllipse(p, points[i].X, points[i].Y, 3, 3);
            }

            for (i = 0; i < n; i++)
            {
                dmin = float.MaxValue;
                Point pt1 = new Point();
                Point pt2 = new Point();
                int x = 0, y = 0;
                for (j = 0; j < n; j++)
                {
                    if (j!=i)
                    {
                        d = (float)Math.Sqrt(Math.Pow(points[i].X - points[j].X, 2) + Math.Pow(points[i].Y - points[j].Y, 2));
                        if (d < dmin)
                        {
                            dmin = d;
                            pt1 = points[i];
                            pt2 = points[j];
                            x = j;
                            y = i;
                        }
                    }
                }
                j = x;
                i = y;
                g.DrawLine(p, pt1.X, pt1.Y, pt2.X, pt2.Y);
                points[i].X = 0;
                points[j].X = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
