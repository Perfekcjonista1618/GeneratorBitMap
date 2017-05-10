using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private Pen _Pen = new Pen(Color.Black, 9);

        public static Bitmap Resize(Bitmap imgPhoto, Size objSize, ImageFormat enuType)
        {
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = objSize.Width;
            int destHeight = objSize.Height;

            Bitmap bmPhoto;

            bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, imgPhoto.Width, imgPhoto.Height),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }


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
                    g.SmoothingMode = SmoothingMode.AntiAlias;
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
            string path = @"learningBase.data";
            StringBuilder sb = new StringBuilder();

            Bitmap bmp = new Bitmap(pictureBox1.Image);
           
           
            Bitmap objImage = new Bitmap(bmp);
            Size objNewSize = new Size(20, 10);
            Bitmap objNewImage = Resize(objImage, objNewSize, ImageFormat.MemoryBmp);



            for (int i = 0; i < objNewImage.Height; i++)
            {
                for (int j = 0; j < objNewImage.Width; j++)
                {
                    if (objNewImage.GetPixel(j, i) == Color.FromArgb(0, 0, 0))
                    {
                        sb.Append("1 ");
                    }
                    else
                    {
                        sb.Append("0 ");
                    }
                    
                }
            }
            File.AppendAllText(path, sb + Environment.NewLine);

            int jakaToLiczba = Int32.Parse(txtNumber.Text);
            int[] jakaToLiczbaBinary;
            jakaToLiczbaBinary = new int[10];

            

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

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            pictureBox1.Image = bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            pictureBox1.Image = bmp;
        }
    }
}
