using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace cobaGradientBrush {
    public partial class Form1 : Form {
        Point[] click = new Point[3];
        Button[] cp;
        int ctr = 0;
        public Form1() {
            InitializeComponent();
            cp = new Button[3]{c1, c2, c3};
        }

        private void button1_Click(object sender, EventArgs e) {
            if(colorDialog1.ShowDialog() == DialogResult.OK) {
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void drawTriangle(Point[] pt, Color[] col, Graphics g) {
            TrianglePoint[] tp = new TrianglePoint[3];
            tp[0] = new TrianglePoint(pt[0], col[0]);
            tp[1] = new TrianglePoint(pt[1], col[1]);
            tp[2] = new TrianglePoint(pt[2], col[2]);

            PathGradientBrush path = new PathGradientBrush(pt);

            path.SurroundColors = col;

            path.CenterColor = getCenterColor(tp);

            g.FillPolygon(path, pt);
        }

        private Color getCenterColor (TrianglePoint[] triangle){
            int[] cc = new int[3];
            for (int i = 0; i < 3; i++) {
                int[] a = MatrixOperator.getVector(triangle[0].getMatrix(i), triangle[1].getMatrix(i));
                int[] b = MatrixOperator.getVector(triangle[1].getMatrix(i), triangle[2].getMatrix(i));
                int[] res = MatrixOperator.crossProduct(a, b);
                int D = -(
                        res[0] * triangle[0].getMatrix(i)[0] +
                        res[1] * triangle[0].getMatrix(i)[1] +
                        res[2] * triangle[0].getMatrix(i)[2]
                    );
                int[] tricenter = getTriangleCenterPoint(triangle);
                cc[i] = Math.Abs((res[0] * tricenter[0] + res[1] * tricenter[1] + D) / res[2]);
            }
            for (int i = 0; i < cc.Length; i++) {
                Console.WriteLine(cc[i]);
            }
            return Color.FromArgb(255, cc[0], cc[1], cc[2]);
        }

        private int[] getTriangleCenterPoint(TrianglePoint[] triangle, int idx = 0) {
            return new int[] {
                (triangle[0].getMatrix(idx)[0] + triangle[1].getMatrix(idx)[0] + triangle[2].getMatrix(idx)[0])/3,
                (triangle[0].getMatrix(idx)[1] + triangle[1].getMatrix(idx)[1] + triangle[2].getMatrix(idx)[1])/3
                };
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            Graphics g = pictureBox1.CreateGraphics();
            click[ctr] = new Point(e.X, e.Y);
            g.FillRectangle(new SolidBrush(cp[ctr].BackColor), e.X, e.Y, 1, 1);
            ctr++;
            if(ctr == 3) {
                ctr = 0;
                drawTriangle(setTrianglePosition(click), new Color[] { cp[0].BackColor, cp[1].BackColor, cp[2].BackColor }, g);
            }
        }

        private Point[] setTrianglePosition(Point[] p) {
            int mostLeftVal = Math.Min(p[0].X, Math.Min(p[1].X , p[2].X));
            //int mostBottomVal = Math.Max(p[0].Y, Math.Max(p[1].Y, p[2].Y));
            //int mostRightVal = Math.Max(p[0].X, Math.Max(p[1].X, p[2].X));
            Point pMostLeft = (p[0].X == mostLeftVal) ? p[0] : (p[1].X == mostLeftVal) ? p[1] : p[2];
            //Point pMostBottom= (p[0].Y == mostBottomVal) ? p[0] : (p[1].Y == mostBottomVal) ? p[1] : p[2];
            //Point pMostRight = (p[0].X == mostRightVal) ? p[0] : (p[1].X == mostRightVal) ? p[1] : p[2];
            Point p1, p2, p3;
            p1 = pMostLeft;
            if (p1 == p[0]) {
                if(p[1].X > p[2].X) {
                    if(p[1].Y > p[2].Y) {
                        p2 = p[2];
                        p3 = p[1];
                    }
                    else {
                        p2 = p[1];
                        p3 = p[2];
                    }
                }
                else {
                    if (p[1].Y > p[2].Y) {
                        p2 = p[1];
                        p3 = p[2];
                    }
                    else {
                        p2 = p[2];
                        p3 = p[1];
                    }
                }
            }
            else if (p1 == p[1]) {
                if (p[0].X > p[2].X) {
                    if (p[0].Y > p[2].Y) {
                        p2 = p[2];
                        p3 = p[0];
                    }
                    else {
                        p2 = p[0];
                        p3 = p[2];
                    }
                }
                else {
                    if (p[0].Y > p[2].Y) {
                        p2 = p[0];
                        p3 = p[2];
                    }
                    else {
                        p2 = p[2];
                        p3 = p[0];
                    }
                }
            }
            else {
                if (p[1].X > p[0].X) {
                    if (p[1].Y > p[0].Y) {
                        p2 = p[0];
                        p3 = p[1];
                    }
                    else {
                        p2 = p[1];
                        p3 = p[0];
                    }
                }
                else {
                    if (p[1].Y > p[0].Y) {
                        p2 = p[1];
                        p3 = p[0];
                    }
                    else {
                        p2 = p[0];
                        p3 = p[1];
                    }
                }
            }
            return new Point[] { p1,p2,p3};
        }

    }

    class TrianglePoint {
        Point coord;
        Color color;
        public TrianglePoint(Point coord, Color color) {
            this.color = color;
            this.coord = coord;
        }

        public TrianglePoint(int x, int y, int a, int r, int g, int b) {
            this.coord = new Point(x, y);
            this.color = Color.FromArgb(a, r, g, b);
        }

        public int[] getMatrix(int code) {
            if(code == 0) {
                return new int[]{ coord.X, coord.Y, color.R};
            }
            else if(code == 1) {
                return new int[]{ coord.X, coord.Y, color.G};
            }
            else if(code == 2) {
                return new int[]{ coord.X, coord.Y, color.B};
            }
            else {
                return null;
            }
        }
    }

    class MatrixOperator {
        public static int[] crossProduct(int[] m1, int[] m2) {
            return new int[] { 
                Math.Abs(m1[1]*m2[2] - m1[2]*m2[1]),
                Math.Abs(m1[2]*m2[0] - m1[0]*m2[2]),
                Math.Abs(m1[0]*m2[1] - m1[1]*m2[0])
            };
        }

        public static int[] getVector(int[] m1, int[] m2) {
            return new int[] { 
                m2[0] - m1[0],
                m2[1] - m1[1],
                m2[2] - m1[2] 
            };
        }
    }
}
