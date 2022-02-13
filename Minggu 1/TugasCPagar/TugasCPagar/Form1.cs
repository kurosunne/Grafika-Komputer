using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TugasCPagar
{
    public partial class Form1 : Form
    {
        int mode = 1;
        int count = 0;
        int[] posy = new int[2];
        int[] posx = new int[2];
        List<Rectangle> pixel = new List<Rectangle>(); 
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle temp = new Rectangle(e.X, e.Y, 1, 1);
            count++;
            if (count%2==1)
            {
                posx[0] = e.X;
                posy[0] = e.Y * -1;
                pixel.Add(new Rectangle(posx[0] - 5, posy[0] *-1 - 5, 10, 10));
            }
            else
            {
                posx[1] = e.X;
                posy[1] = e.Y * -1;
                pixel.Add(new Rectangle(posx[1] - 5, posy[1] *-1 - 5, 10, 10));

                if (mode==1)
                {
                    int dx = posx[1] - posx[0];
                    int dy = posy[1] - posy[0];
                    double m = dy * 1.0 / dx * 1.0;
                    int a, b;
                    double c;

                    if (posx[0]<posx[1])
                    {
                        a = posx[0];
                        b = posx[1];
                        c = posy[0];
                    }
                    else
                    {
                        a = posx[1];
                        b = posx[0];
                        c = posy[1];
                    }

                    for (int i = a; i <= b; i++)
                    {
                        pixel.Add(new Rectangle(i  , Math.Abs(Convert.ToInt32(Math.Round(c))) , 1, 1));
                        c += m;
                    }
                }
                else if (mode==2)
                {
                    int w = posx[1] - posx[0];
                    int h = posy[1] - posy[0];
                    int f = 2 * h - w;
                    int a, b,cek;
                    double c;

                    if (posx[0] < posx[1])
                    {
                        a = posx[0];
                        b = posx[1];
                        c = posy[0];

                        for (int i = a; i <= b; i++)
                        {
                            Console.WriteLine(f);
                            pixel.Add(new Rectangle(i, Math.Abs(Convert.ToInt32(Math.Round(c))), 1, 1));
                            if (f < 0)
                            {
                                f += (2 * Math.Abs(h));
                            }
                            else
                            {
                                if (posy[0]<posy[1])
                                {
                                    c++;
                                }
                                else
                                {
                                    c--;
                                }
                                f += 2 * (Math.Abs(h) - Math.Abs(w));
                            }
                        }
                    }
                    else
                    {
                        a = posx[1];
                        b = posx[0];
                        c = posy[1];

                        for (int i = a; i <= b; i++)
                        {
                            Console.WriteLine(f);
                            pixel.Add(new Rectangle(i, Math.Abs(Convert.ToInt32(Math.Round(c))), 1, 1));
                            if (f < 0)
                            {
                                f += (2 * Math.Abs(h));
                            }
                            else
                            {
                                if (posy[0] > posy[1])
                                {
                                    c++;
                                }
                                else
                                {
                                    c--;
                                }
                                f += 2 * (Math.Abs(h) - Math.Abs(w));
                            }
                        }
                    }
                }
                else if (mode == 3)
                {
                    double jarak = Math.Sqrt(Math.Pow(Math.Abs(posy[0]-posy[1]),2) + Math.Pow(Math.Abs(posx[0] - posx[1]), 2));
                    int x = 0;
                    int y = Convert.ToInt32(jarak);
                    double d = 3 - 2 * jarak;

                    while (x<=y)
                    {
                        Console.WriteLine(posx[0] + x +" - "+ posy[0] + y);
                        pixel.Add(new Rectangle(posx[0] + x, posy[0] + y, 1, 1));
                        if (d<0)
                        {
                            d = d + 4 * x * 6;
                        }
                        else
                        {
                            d = d + 4 * (x - y) + 10;
                            y++;
                            x++;
                        }
                    }
                }
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < pixel.Count; i++)
            {
                g.FillRectangle(new SolidBrush(Color.Red), pixel[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mode = 1;
            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.LightGray;
            button3.BackColor = Color.LightGray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mode = 2;
            button2.BackColor = Color.AliceBlue;
            button1.BackColor = Color.LightGray;
            button3.BackColor = Color.LightGray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mode = 3;
            button3.BackColor = Color.AliceBlue;
            button2.BackColor = Color.LightGray;
            button1.BackColor = Color.LightGray;
        }
    }
}
