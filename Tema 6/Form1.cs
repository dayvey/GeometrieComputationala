using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tema_6
{
    public partial class Form1 : Form
    {
        List<PointF> points = new List<PointF>();

        public Form1()
        {
            this.MouseDown += Form1_MouseDown;
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 3);

            foreach (var point in points)
                g.DrawEllipse(p, point.X, point.Y, 3, 3);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            PointF aux = this.PointToClient(new Point(Form1.MousePosition.X, Form1.MousePosition.Y));
            Pen p = new Pen(Color.Black, 3);
            
            points.Add(aux);
            Graphics g = this.CreateGraphics();

            g.DrawEllipse(p, aux.X, aux.Y, 3, 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black, 3);
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(p, points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y);
            }
            g.DrawLine(p, points[0].X, points[0].Y, points[points.Count - 1].X, points[points.Count - 1].Y);

            p = new Pen(Color.Green, 3);
            for (int i = 2; i < points.Count - 2; i++) {
                g.DrawLine(p, points[i].X, points[i].Y, points[0].X, points[0].Y);
            }
        }
    }
}
