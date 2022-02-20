using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Segitiga
{
    public partial class Form1 : Form
    {
        int count = 0;
        int[] posy = new int[3];
        int[] posx = new int[3];
        List<Rectangle> pixel = new List<Rectangle>();
        List<GraphicsPath> pathh = new List<GraphicsPath>();
        List<PathGradientBrush> pgb = new List<PathGradientBrush>();
        bool yes = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (yes)
            {
                Point[] points = {
                new Point(posx[0], posy[0]),
                new Point(posx[1], posy[1]),
                new Point(posx[2], posy[2])};
                GraphicsPath path = new GraphicsPath();
                path.AddLines(points);

                PathGradientBrush pthGrBrush = new PathGradientBrush(path);

                pthGrBrush.CenterColor = Color.FromArgb(255, crossproduct("R"), crossproduct("G"), crossproduct("B"));

                Color[] colors = {
                button1.BackColor,
                button2.BackColor,
                button3.BackColor};
                pthGrBrush.SurroundColors = colors;
                pathh.Add(path);
                pgb.Add(pthGrBrush);
                for (int i = 0; i < pathh.Count; i++)
                {
                    g.FillPath(pgb[i], pathh[i]);
                }
                yes = false;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            posx[count % 3] = e.X;
            posy[count % 3] = e.Y;
            if (count % 3 == 2)
            {
                yes = true;
                pictureBox1.Refresh();
            }
            count++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDlg.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDlg.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = colorDlg.Color;
            }
        }

        public int crossproduct(String H)
        {
            if (H == "R")
            {
                int[] a = {posx[1] - posx[0], posy[1] - posy[0],  button2.BackColor.R - button1.BackColor.R};
                int[] b = {posx[2] - posx[1], posy[2] - posy[1],  button3.BackColor.R - button2.BackColor.R};
                int[] c = { Math.Abs(a[1] * b[2] - a[2] * b[1]), Math.Abs(a[0] * b[2] - a[2] * b[0]), Math.Abs(a[0] * b[1] - b[0] * a[1]) };
                int px = (posx[0] + posx[1] + posx[2]) / 3;
                int py = (posy[0] + posy[1] + posy[2]) / 3;
                int d = -1 * (c[0] * posx[0] + c[1] * posy[0] + c[2] * button1.BackColor.R);
                int z = (-1 * (c[0] * px + c[1] * py + d)) / c[2];
                return Math.Abs(z);
            }
            else if (H=="G")
            {
                int[] a = { posx[1] - posx[0], posy[1] - posy[0], button2.BackColor.G - button1.BackColor.G };
                int[] b = { posx[2] - posx[1], posy[2] - posy[1], button3.BackColor.G - button2.BackColor.G };
                int[] c = { Math.Abs(a[1] * b[2] - a[2] * b[1]), Math.Abs(a[0] * b[2] - a[2] * b[0]), Math.Abs(a[0] * b[1] - b[0] * a[1]) };
                int px = (posx[0] + posx[1] + posx[2]) / 3;
                int py = (posy[0] + posy[1] + posy[2]) / 3;
                int d = -1 * (c[0] * posx[0] + c[1] * posy[0] + c[2] * button1.BackColor.G);
                int z = (-1 * (c[0] * px + c[1] * py + d)) / c[2];
                return Math.Abs(z);
            }
            else
            {
                int[] a = { posx[1] - posx[0], posy[1] - posy[0], button2.BackColor.B - button1.BackColor.B };
                int[] b = { posx[2] - posx[1], posy[2] - posy[1], button3.BackColor.B - button2.BackColor.B };
                int[] c = { Math.Abs(a[1] * b[2] - a[2] * b[1]), Math.Abs(a[0] * b[2] - a[2] * b[0]), Math.Abs(a[0] * b[1] - b[0] * a[1]) };
                int px = (posx[0] + posx[1] + posx[2]) / 3;
                int py = (posy[0] + posy[1] + posy[2]) / 3;
                int d = -1 * (c[0] * posx[0] + c[1] * posy[0] + c[2] * button1.BackColor.B);
                int z = (-1 * (c[0] * px + c[1] * py + d)) / c[2];
                return Math.Abs(z);
            }

            return 0;
        }
    }
}
