using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WebCamLib;

namespace Subtraction
{
    public partial class Form1 : Form
    {
        Bitmap imageB, imageA, colorgreen, resultImage;
        Bitmap loadImg, processedImage;
        int a, r, g, b, tr, tg, tb;
        Device[] cam = DeviceManager.GetAllDevices();

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int[] redH = new int[256];
            int[] greenH = new int[256];
            int[] blueH = new int[256];
            loadImg = new Bitmap(openFileDialog1.FileName);
            Color Pixel;
            for (int x = 0; x < loadImg.Width; x++)
            {
                for (int y = 0; y < loadImg.Height; y++)
                {
                    Pixel = loadImg.GetPixel(x, y);
                    redH[Pixel.R]++;
                    greenH[Pixel.G]++;
                    blueH[Pixel.B]++;

                }
            }

            // create series
            Series series1 = new Series("Red");
            series1.ChartType = SeriesChartType.Column;

            Series series2 = new Series("Green");
            series2.ChartType = SeriesChartType.Column;

            Series series3 = new Series("Blue");
            series3.ChartType = SeriesChartType.Column;



            // Adding series
            for (int i = 0; i < redH.Length; i++)
            {
                series1.Points.AddXY(i, redH[i]);
            }
            for (int i = 0; i < greenH.Length; i++)
            {
                series2.Points.AddXY(i, greenH[i]);
            }

            for (int i = 0; i < blueH.Length; i++)
            {
                series3.Points.AddXY(i, blueH[i]);
            }

            // show to chart
            chart1.Series.Add(series1);
            chart2.Series.Add(series2);
            chart3.Series.Add(series3);

        }

        private void loadImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK) { }
        }

        private void loadbackground_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog2.ShowDialog() == DialogResult.OK) { }
        }

        private void subtract_Click(object sender, EventArgs e)
        {
            Color mygreen = Color.FromArgb(0, 0, 255);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;
            resultImage = new Bitmap(imageA.Width, imageA.Height);


            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageB.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backpixel = imageA.GetPixel(x, y);

                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);

                    if (subtractvalue < threshold)
                    {
                        resultImage.SetPixel(x, y, backpixel);

                        Console.WriteLine($"Position ({x}, {y}):");
                        Console.WriteLine($"  ImageA: {backpixel}");
                        Console.WriteLine($"  ImageB: {pixel}");
                        Console.WriteLine($"  SubtractValue: {subtractvalue}");

                    }
                    else
                        resultImage.SetPixel(x, y, pixel);
                }

            }

            pictureBox3.Image = resultImage;
        
    }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            imageB = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = new Bitmap(imageB);
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = new Bitmap(imageA);
        }

        private void openDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cam.Length > 0)
            {
                Device c = DeviceManager.GetDevice(0);
                c.ShowWindow(pictureBox1);
            }

        }

        private void closeDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Device c = DeviceManager.GetDevice(0);
            c.Stop();
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadImg = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            //int gray;
            processedImage = new Bitmap(loadImg.Width, loadImg.Height);

            for (int y = 0; y < loadImg.Height; y++)
            {
                for (int x = 0; x < loadImg.Width; x++)
                {
                    pixel = loadImg.GetPixel(x, y);

                    a = pixel.A;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;

                    tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                    tr = (tr > 255) ? 255 : tr;
                    tg = (tg > 255) ? 255 : tg;
                    tb = (tb > 255) ? 255 : tb;

                    processedImage.SetPixel(x, y, Color.FromArgb(a, tr, tg, tb));
                }
            }
            pictureBox2.Image = processedImage;
        }

        private void yToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadImg = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            //int gray;
            processedImage = new Bitmap(loadImg.Width, loadImg.Height);

            for (int x = 0; x < loadImg.Width; x++)
            {
                for (int y = 0; y < loadImg.Height; y++)
                {
                    pixel = loadImg.GetPixel(x, y);
                    processedImage.SetPixel(x, (loadImg.Height - 1) - y, pixel);
                }
            }
            pictureBox2.Image = processedImage;
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadImg = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            //int gray;
            processedImage = new Bitmap(loadImg.Width, loadImg.Height);

            for (int x = 0; x < loadImg.Width; x++)
            {
                for (int y = 0; y < loadImg.Height; y++)
                {
                    pixel = loadImg.GetPixel(x, y);
                    processedImage.SetPixel((loadImg.Width - 1) - x, y, pixel);
                }
            }
            pictureBox2.Image = processedImage;
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadImg = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            //int gray;
            processedImage = new Bitmap(loadImg.Width, loadImg.Height);

            for (int x = 0; x < loadImg.Width; x++)
            {
                for (int y = 0; y < loadImg.Height; y++)
                {
                    pixel = loadImg.GetPixel(x, y);
                    processedImage.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
            pictureBox2.Image = processedImage;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadImg = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            int gray;
            processedImage = new Bitmap(loadImg.Width, loadImg.Height);

            for (int x = 0; x < loadImg.Width; x++)
            {
                for (int y = 0; y < loadImg.Height; y++)
                {
                    pixel = loadImg.GetPixel(x, y);
                    gray = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    processedImage.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }
            }
            pictureBox2.Image = processedImage;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK) { }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Image Files|*.jpg;*.jpeg;*.png;";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image.Save(sf.FileName);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadImg = new Bitmap(openFileDialog1.FileName);
            pictureBox2.Image = loadImg;
        }
    }
}
