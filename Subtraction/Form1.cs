using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Subtraction
{
    public partial class Form1 : Form
    {
        Bitmap imageB, imageA, colorgreen, resultImage;

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

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = new Bitmap(imageA);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = new Bitmap(imageB);
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

                    if (subtractvalue > threshold)
                    {
                        Console.WriteLine($"Position ({x}, {y}):");
                        Console.WriteLine($"  ImageA: {backpixel}");
                        Console.WriteLine($"  ImageB: {pixel}");
                        Console.WriteLine($"  SubtractValue: {subtractvalue}");
                        resultImage.SetPixel(x, y, backpixel);
                    }  else
                        resultImage.SetPixel(x, y, pixel);
                }
                
            }

            pictureBox3.Image = resultImage;
        }

        

        public Form1()
        {
            InitializeComponent();
        }

       
    }
}
