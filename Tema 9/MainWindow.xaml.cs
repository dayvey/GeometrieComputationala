﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Tema_9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> points = new List<Point>();
        List<Triangle> triangles = new List<Triangle>();

        Line last;
        Ellipse set;
        bool done = false;
        public MainWindow()
        {
            InitializeComponent();
            MouseDown += MainWindow_MouseDown;
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!done)
            {
                double y = e.GetPosition(this).Y;
                double x = e.GetPosition(this).X;

                if (points.Count == 0)
                {
                    Ellipse st = new Ellipse()
                    {
                        Width = 20,
                        Height = 20,
                        Fill = new SolidColorBrush(Colors.Black),
                        Stroke = new SolidColorBrush(Colors.Black),
                    };
                    st.MouseDown += Finish_Click;
                    st.MouseEnter += St_MouseEnter;
                    st.MouseLeave += St_MouseLeave;
                    Canvas.SetTop(st, y - st.Width / 2);
                    Canvas.SetLeft(st, x - st.Height / 2);
                    MainCanvas.Children.Add(st);
                    Canvas.SetZIndex(st, 1);
                    set = st;

                }
                if (points.Count == 0)
                {
                    DrawCircle(5, x, y).MouseDown += Finish_Click;
                }
                else DrawCircle(5, x, y);

                Point clicked = new Point(x, y);
                if (points.Count > 0)
                {
                    DrawLine(points.Last(), clicked, Colors.Gray);
                }
                points.Add(clicked);
            }
        }
        private Ellipse DrawCircle(double width, double x, double y)
        {
            Ellipse c = new Ellipse()
            {
                Width = width,
                Height = width,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.Black),
            };
            Canvas.SetTop(c, y - c.Width / 2);
            Canvas.SetLeft(c, x - c.Height / 2);
            MainCanvas.Children.Add(c);
            Canvas.SetZIndex(c, 2);
            return c;
        }
        private Ellipse DrawCircle(double width, Point p, Color cl)
        {
            Ellipse c = new Ellipse()
            {
                Width = width,
                Height = width,
                Fill = new SolidColorBrush(cl),
                Stroke = new SolidColorBrush(cl),
            };
            Canvas.SetTop(c, p.Y - c.Width / 2);
            Canvas.SetLeft(c, p.X - c.Height / 2);
            MainCanvas.Children.Add(c);
            Canvas.SetZIndex(c, 2);
            return c;
        }
        private void St_MouseLeave(object sender, MouseEventArgs e)
        {
            set.Fill = new SolidColorBrush(Colors.Black);
        }

        private void St_MouseEnter(object sender, MouseEventArgs e)
        {
            set.Fill = new SolidColorBrush(Colors.Black);
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {

            DrawLine(points.First(), points.Last(), Colors.Gray);
            done = true;
            MainCanvas.Children.Remove(last);
            MainCanvas.Children.Remove(set);
        }
        Line DrawLine(Point p, Point q, Color c)
        {
            Line l = new Line()
            {
                X1 = p.X,
                X2 = q.X,
                Y1 = p.Y,
                Y2 = q.Y,
                StrokeThickness = 2,
                Stroke = new SolidColorBrush(c),
                Fill = new SolidColorBrush(c),
            };
            MainCanvas.Children.Add(l);
            return l;
        }
        Line HoverLine(Point p, Point q, Color c)
        {
            Line l = new Line()
            {
                X1 = p.X,
                X2 = q.X,
                Y1 = p.Y,
                Y2 = q.Y,
                StrokeThickness = 2,
                Stroke = new SolidColorBrush(c),
                Fill = new SolidColorBrush(c),
            };
            l.MouseDown += MainWindow_MouseDown;
            MainCanvas.Children.Add(l);
            Canvas.SetZIndex(l, -1);
            return l;
        }
        
        bool IsLeft(Point r, Point p, Point q)
        {
            double Det = p.X * q.Y + p.Y * r.X + q.X * r.Y - r.X * q.Y - r.Y * p.X - q.X * p.Y;
            if (Det < 0)
            {
                return false;
            }
            return true;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            points = new List<Point>();
            done = false;
            MainCanvas.Children.Clear();
        }

        private void ConvPolMon(object sender, RoutedEventArgs e)
        {
            Point[] sortat = new Point[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                sortat[i] = points[i];
                int j = i;
                while (j > 1 && sortat[j].Y < sortat[j - 1].Y)
                {
                    (sortat[j], sortat[j - 1]) = (sortat[j - 1], sortat[j]);
                    j--;
                }
            }
            for (int i = 0; i < sortat.Length; i++)
            {
                if (IsReflex(sortat[i], points))
                {
                    int ptindex = points.IndexOf(sortat[i]);
                    Point ant = points[(ptindex - 1 + points.Count) % points.Count];
                    Point urm = points[(ptindex + 1) % points.Count];
                    if (ant.Y < sortat[i].Y && urm.Y < sortat[i].Y) //  --> varf de unire
                    {
                        Ellipse unire = DrawCircle(10, sortat[i].X, sortat[i].Y);
                        unire.Fill = Brushes.Green;

                        for (int j = i + 1; j < points.Count; j++)
                        {
                            Point urmafterurm = sortat[j];
                            if (IsDiagonalaDinReflex(sortat[i], urmafterurm, points))
                            {
                                DrawLine(sortat[i], urmafterurm, Colors.DarkGreen);
                                break;
                            }
                        }


                    }
                    else if (ant.Y > sortat[i].Y && urm.Y > sortat[i].Y) // --> varf de separare
                    {
                        Ellipse unire = DrawCircle(10, sortat[i].X, sortat[i].Y);
                        unire.Fill = Brushes.Red;

                        for (int j = i - 1; j >= 0; j--)
                        {
                            Point urmafterurm = sortat[j];
                            if (IsDiagonalaDinReflex(sortat[i], urmafterurm, points))
                            {
                                DrawLine(sortat[i], urmafterurm, Colors.DarkRed);
                                break;
                            }

                        }
                    }
                }
            }
        }
        bool IsDiagonalaDinReflex(Point reflex, Point q, List<Point> pts)
        {
            Point p = reflex;
            bool ok = true;
            int i = pts.IndexOf(p);
            int j = pts.IndexOf(q);
            Point urm = pts[(i + 1) % pts.Count];
            Point ant = pts[((i - 1) + pts.Count) % pts.Count];
            Segment toCheck = new Segment(p, q);
            for (int k = 0; k < pts.Count; k++)
            {
                if (k == i || k == j || (k + 1) % pts.Count == j || (k + 1) % pts.Count == i)
                {
                    continue;
                }
                if (toCheck.Intersects(new Segment(pts[k], pts[(k + 1) % pts.Count])))
                {
                    ok = false;
                    break;
                }
            }
            if (!IsLeft(urm, p, q) && !IsLeft(q, p, ant))
                ok = false;
            return ok;
        }
        bool IsReflex(Point p, List<Point> pts)
        {
            int i = pts.IndexOf(p);
            int urm = (i + 1) % pts.Count;
            int ant = ((i - 1) + pts.Count) % pts.Count;
            if (IsLeft(pts[urm], points[i], points[ant]))
            {
                return false;
            }
            return true;
        }
    }

    class Triangle
    {
        public Point[] pts { get; set; }
        public Triangle(Point p1, Point p2, Point p3)
        {
            pts = new Point[] { p1, p2, p3 };
        }
        public double Area()
        {
            return 0.5 * ((pts[0].X * (pts[1].Y - pts[2].Y)) + (pts[1].X * (pts[2].Y - pts[0].Y)) + (pts[2].X * (pts[0].Y - pts[1].Y)));
        }
    }
    class Segment
    {
        public Point p;
        public Point q;
        public double Dist;
        public Segment(Point p, Point q)
        {
            this.p = p;
            this.q = q;
            Dist = GetDist();
        }
        public Segment(Line l)
        {
            this.p = new Point(l.X1, l.Y1);
            this.q = new Point(l.X2, l.Y2);
            Dist = GetDist();
        }
        public bool Intersects(Segment other)
        {
            return doIntersect(this, other);
        }
        bool doIntersect(Segment s1, Segment s2)
        {
            Point p1 = s1.p, q1 = s1.q, p2 = s2.p, q2 = s2.q;

            // Find the four orientations needed for general and
            // special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are collinear and p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are collinear and q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are collinear and p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are collinear and q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }
        int orientation(Point p, Point q, Point r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/
            // for details of below formula.
            double val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  // collinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }
        bool onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.Y >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }
        double GetDist()
        {
            return Math.Sqrt((p.X - q.X) * (p.X - q.X) + (p.Y - q.Y) * (p.Y - q.Y));
        }
    }
}