using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GeneratorBitMap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private Point? _Previous = null;
        private Pen _Pen = new Pen(Color.Red, 8);
        



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _Previous = new Point(e.X, e.Y);
            pictureBox1_MouseMove(sender, e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Previous != null)
            {
                if (pictureBox1.Image == null)
                {
                    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                    }
                    pictureBox1.Image = bmp;
                }
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
                }
                pictureBox1.Invalidate();
                _Previous = new Point(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _Previous = null;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bttnSave_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);

            ImageConverter converter = new ImageConverter();
            //pictureBox1.Image.

            //****************************************************
            //zapis "jaka to liczba" do pliku w postaci 10 cyfr(0,1) 
            int jakaToLiczba = Int32.Parse(txtNumber.Text);
            int[] jakaToLiczbaBinary;
            jakaToLiczbaBinary = new int[10];

            string path = @"C:\Users\MM\Desktop\CiagiBinarne.txt"; //ZMIEN SCIEZKE

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i != jakaToLiczba)
                    {
                        jakaToLiczbaBinary[i] = 0;
                    }
                    else
                    {
                        jakaToLiczbaBinary[i] = 1;
                    }
                    sw.Write(jakaToLiczbaBinary[i] + " ");
                }
                sw.WriteLine();
            }
            //****************************************************
        }
    }
}
