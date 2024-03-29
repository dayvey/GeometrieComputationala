﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Tema_7_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> points = new List<Point>();

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

        private async void PolyTriangulate(object sender, RoutedEventArgs e)
        {
            int n = points.Count;
            List<Segment> segs = new List<Segment>();
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == i || j == i - 1 || j == i + 1)
                    {
                        continue;
                    }
                    int next = j % n;
                    if (i == 0 && j == n - 1)
                    {
                        break;
                    }
                    bool ok = true;
                    Segment toCheck = new Segment(points[i], points[next]);
                    Line drew = DrawLine(toCheck.p, toCheck.q, Colors.Red);
                    for (int k = 0; k < segs.Count; k++)
                    {
                        if (toCheck.p == segs[k].p || toCheck.p == segs[k].q || toCheck.q == segs[k].p || toCheck.q == segs[k].q)
                        {
                            continue;
                        }
                        if (toCheck.Intersects(segs[k]))
                        {
                            ok = false;
                            break;
                        }

                    }
                    for (int k = 0; k < points.Count; k++)
                    {
                        if (k == i || k == next || (k + 1) % n == next || (k + 1) % n == i)
                        {
                            continue;
                        }
                        if (toCheck.Intersects(new Segment(points[k], points[(k + 1) % n])))
                        {
                            ok = false;
                            break;
                        }
                    }
                    int prev = i - 1;
                    if (prev < 0)
                    {
                        prev = n - 1;
                    }
                    if (IsLeft(points[i + 1], points[i], points[prev]))
                    {
                        Ellipse helpppp = DrawCircle(10, points[i].X, points[i].Y);
                        helpppp.Fill = Brushes.Red;
                        drew.Fill = Brushes.Red;
                        if (!(IsLeft(points[i + 1], points[i], points[j]) && IsLeft(points[j], points[i], points[prev])))
                        {
                            ok = false;
                        }
                        
                    }
                    else
                    {
                        Ellipse helper = DrawCircle(10, points[i].X, points[i].Y);
                        helper.Fill = Brushes.Blue;
                        drew.Fill = Brushes.Blue;
                        if (!IsLeft(points[i + 1], points[i], points[next]) && !IsLeft(points[next], points[i], points[prev]))
                        {
                            ok = false;
                        }
                    }
                    await Task.Delay(50);
                    if (ok)
                    {
                        segs.Add(toCheck);
                    }
                    else
                    {
                        MainCanvas.Children.Remove(drew);
                    }

                }
            }
            for (int i = 0; i < segs.Count; i++)
            {
                DrawLine(segs[i].q, segs[i].p, Colors.Black);
            }
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
