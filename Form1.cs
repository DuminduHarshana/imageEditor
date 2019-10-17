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

namespace Image_Editor_By_DH
{
    public partial class Form1 : Form
    {
        PictureBox[] pictureBoxes = new PictureBox[4];
        Image[] imagelst = new Image[4];
        Image file;
        Boolean fileopen = false;
        static readonly object _object = new object();
        private static object Lock = new object();
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeOpenFileDialog();
        }

        public Form1()
        {
            InitializeComponent();

            pictureBoxes[0] = pictureBox1;
            pictureBoxes[1] = pictureBox2;
            pictureBoxes[2] = pictureBox3;
            pictureBoxes[3] = pictureBox4;
        }
        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.openFileDialog1.Filter =
                "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" +
                "All files (*.*)|*.*";

            // Allow the user to select multiple images.
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "My Image Browser";
        }

        void openimage1()
        {
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = file;
                imagelst[0] = file;
                fileopen = true;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;


                //    OpenFileDialog f = new OpenFileDialog();
                //    f.Filter = "JPEG(*.JPEG)|jpeImages |*.png;*.bmp;*.jpeg,*.jpg";

                //    if(f.ShowDialog() == DialogResult.OK)
                //    {
                //        file = Image.FromFile(f.FileName);
                //        pictureBox1.Image = file;
                //    }
                //if (f.ShowDialog() == DialogResult.OK)
                //{
                //    file = Image.FromFile(f.FileName);
                //    pictureBox2.Image = file;
                //}
            }
        }
        void openimage2()
        {
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                pictureBox2.Image = file;
                imagelst[1] = file;
                fileopen = true;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }
        void openimage3()
        {
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                pictureBox3.Image = file;
                imagelst[2] = file;
                fileopen = true;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }
        void openimage4()
        {
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                pictureBox4.Image = file;
                imagelst[3] = file;
                fileopen = true;
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        public Image Crop(string img, int width, int height, int x, int y)
        {
            try
            {
                Image image = Image.FromFile(img);
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                Graphics gfx = Graphics.FromImage(bmp);
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gfx.DrawImage(image, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
                // Dispose to free up resources
                image.Dispose();
                bmp.Dispose();
                gfx.Dispose();

                return bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        void save1()
        {
            if (fileopen)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "Images |*.png;*.bmp;*.jpeg,*.jpg;*";
                ImageFormat format = ImageFormat.Png;
                if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    String t = Path.GetExtension(s.FileName);
                    switch (t)
                    {
                        case ".jpeg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }
                    pictureBox1.Image.Save(s.FileName, format);

                }
                else
                {
                    MessageBox.Show("error");

                }
            }
        }
        void save2()
        {
            if (fileopen)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Filter = "Images |*.png;*.bmp;*.jpeg,*.jpg;*";
                ImageFormat format = ImageFormat.Png;
                if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    String t = Path.GetExtension(s.FileName);
                    switch (t)
                    {
                        case ".jpeg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }
                    pictureBox2.Image.Save(s.FileName, format);

                }
                else
                {
                    MessageBox.Show("error");

                }
            }
        }
        void resetImage()
        {
            if (!fileopen)
            {
                MessageBox.Show("Picture is not loaded");

            }
            else
            {
                if (fileopen)
                {
                    file = Image.FromFile(openFileDialog1.FileName);
                    pictureBox1.Image = imagelst[0];
                    pictureBox2.Image = imagelst[1];
                    pictureBox3.Image = imagelst[2];
                    pictureBox4.Image = imagelst[3];
                }
            }
        }

        void grayscale(Bitmap bmpin, Image img)
        {

            if (!fileopen)

            {
                MessageBox.Show("Open an Image then apply changes");

            }
            else
            {
                ImageAttributes imgAtt = new ImageAttributes();
                ColorMatrix colour = new ColorMatrix(new float[][]
                {
                    new float[]{0.299f,0.299f,0.299f,0,0},
                    new float[]{0.587f,0.587f,0.587f,0,0},
                    new float[]{0.114f, 0.114f, 0.114f, 0,0},
                    new float[]{0,0,0,1,0},
                     new float[]{0,0,0,0,0},
                });
                imgAtt.SetColorMatrix(colour);
                Graphics gr = Graphics.FromImage(bmpin);

                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAtt);
                gr.Dispose();


            }
        }
        void rotate(Image bmp)
        {
            if (!fileopen)
            {
                MessageBox.Show("eroor");

            }
            else
            {
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
        }
        void blossome(Bitmap bmpin, Image img)
        {

            if (!fileopen)

            {
                MessageBox.Show("Open an Image then apply changes");

            }
            else
            {
                ImageAttributes imgAtt = new ImageAttributes();
                ColorMatrix colour = new ColorMatrix(new float[][]
                {
                    new float[]{1,0,0,0,0},
                    new float[]{0,1,0,0,0},
                    new float[]{.50f, 0,1,0,0},
                    new float[]{0,0,0,1,0},
                     new float[]{0,0,0,0,1},
                });
                imgAtt.SetColorMatrix(colour);
                Graphics gr = Graphics.FromImage(bmpin);

                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAtt);
                gr.Dispose();


            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            openimage1();
        }



        private void Button3_Click(object sender, EventArgs e)
        {
            save1();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            resetImage();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (!fileopen)
            {
                MessageBox.Show("open a picture");
            }
            else
            {


                try
                {


                    Parallel.ForEach(pictureBoxes, p =>
                 {
                     lock (_object)
                     {
                         Image image = p.Image;
                         Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                         grayscale(bmpInverted, image);
                         p.Image = bmpInverted;

                     }

                 });
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (!fileopen)
            {
                MessageBox.Show("open a picture");
            }
            else
            {


                try
                {


                    Parallel.ForEach(pictureBoxes, p =>
                    {
                        lock (_object)
                        {
                            Image image = p.Image;
                            Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                            blossome(bmpInverted, image);
                            p.Image = bmpInverted;

                        }

                    });
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)



        {
            if (!fileopen)
            {
                MessageBox.Show("open a picture");
            }
            else
            {

                try
                {
                    Parallel.ForEach(pictureBoxes, p =>
                    {
                        lock (_object)
                        {
                            Image image = p.Image;

                            rotate(image);
                            p.Image = image;

                        }

                    });

                  
                }
                catch (Exception ext)
                {
                    MessageBox.Show(ext.ToString());
                }
            }
        }
        void sapia(Bitmap bmpin, Image img)
        {

            if (!fileopen)

            {
                MessageBox.Show("Open an Image then apply changes");

            }
            else
            {
                ImageAttributes imgAtt = new ImageAttributes();
                ColorMatrix colour = new ColorMatrix(new float[][]
                {
                    new float[]{ 0.393f, 0.349f, 0.272f, 0,0},
                    new float[]{ 0.769f, 0.686f, 0.534f, 0,0},
                    new float[]{ 0.189f, 0.168f, 0.131f, 0,0},
                    new float[]{0,0,0,1,0},
                     new float[]{0,0,0,0,1},
                });
                imgAtt.SetColorMatrix(colour);
                Graphics gr = Graphics.FromImage(bmpin);

                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAtt);
                gr.Dispose();


            }
        }

        void Invert(Bitmap bmpin, Image img)
        {

            if (!fileopen)

            {
                MessageBox.Show("Open an Image then apply changes");

            }
            else
            {
                ImageAttributes imgAtt = new ImageAttributes();
                ColorMatrix colour = new ColorMatrix(new float[][]
                {
                    new float[] {-1, 0, 0, 0, 0},
                    new float[] {0, -1, 0, 0, 0},
                    new float[] {0, 0, -1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {1, 1, 1, 0, 1}
                });
                imgAtt.SetColorMatrix(colour);
                Graphics gr = Graphics.FromImage(bmpin);

                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAtt);
                gr.Dispose();


            }
        }
        void ImageFlip(Image img)
        {
            if (!fileopen)
            {
                MessageBox.Show("error");
            }
            else
            {
                img.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
        }


        private void Button10_Click(object sender, EventArgs e)
        {
            if (!fileopen)
            {
                MessageBox.Show("open a picture");
            }
            else
            {


                try
                {


                    Parallel.ForEach(pictureBoxes, p =>
                    {
                        lock (_object)
                        {
                            Image image = p.Image;
                            Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                            sapia(bmpInverted, image);
                            p.Image = bmpInverted;

                        }

                    });
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (fileopen)
            {


                Task inver = Task.Factory.StartNew(() =>
                  {
                      Image image = pictureBox1.Image;
                      Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                      Invert(bmpInverted, image);

                      this.pictureBox1.Image = bmpInverted;
                  });

                Task inver1 = Task.Factory.StartNew(() =>
                {
                    Image image = pictureBox2.Image;
                    Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                    Invert(bmpInverted, image);

                    this.pictureBox2.Image = bmpInverted;
                });
                Task inver2 = Task.Factory.StartNew(() =>
                {
                    Image image = pictureBox3.Image;
                    Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                    Invert(bmpInverted, image);

                    this.pictureBox3.Image = bmpInverted;
                });
                Task inver4 = Task.Factory.StartNew(() =>
                {
                    Image image = pictureBox4.Image;
                    Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                    Invert(bmpInverted, image);

                    this.pictureBox4.Image = bmpInverted;
                });
            }
            else
            {
                MessageBox.Show("open a picture");
            }
            //if (!fileopen)
            //{
            //    MessageBox.Show("open a picture");
            //}
            //else
            //{


            //    try
            //    {


            //        Parallel.ForEach(pictureBoxes, p =>
            //        {
            //            lock (_object)
            //            {
            //                Image image = p.Image;
            //                Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
            //                Invert(bmpInverted, image);
            //                p.Image = bmpInverted;

            //            }

            //        });
            //    }
            //    catch (Exception ex)
            //    {

            //        MessageBox.Show(ex.ToString());
            //    }
            //}
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            openimage2();
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            openimage3();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            openimage4();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Task T5 = Task.Factory.StartNew(() =>
              {
                  Image image = pictureBox1.Image;
                  ImageFlip(image);
                  pictureBox1.Image = image;
              });
            try
            {
                T5.Wait();
            }
            catch (AggregateException ae)
            {

                Console.WriteLine(ae);
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {

            if (fileopen)
            {
                Parallel.ForEach(pictureBoxes, p =>
                {
                    lock (_object)
                    {
                        Image image = p.Image;
                        ImageFlip(image);
                        p.Image = image;
                    }
                });
                Parallel.ForEach(pictureBoxes, p =>
                {
                    lock (_object)
                    {
                        Image image = p.Image;
                        rotate(image);
                        p.Image = image;
                    }
                });
            }
            else
            {
                MessageBox.Show("open images");
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Task filter1 = Task.Factory.StartNew(() =>
            {
                Image image = pictureBox1.Image;
                Bitmap bmp = new Bitmap(image.Width, image.Height);
                sapia(bmp, image);
                this.pictureBox1.Image = bmp;
            });

            Task filter2 = filter1.ContinueWith((antecedent) =>
            {
                Image image = pictureBox2.Image;
                Bitmap bmp = new Bitmap(image.Width, image.Height);
                grayscale(bmp, image);
                this.pictureBox2.Image = bmp;
            });

            Task filter3 = Task.Factory.StartNew(() =>
            {
                Image image = pictureBox3.Image;
                Bitmap bmp = new Bitmap(image.Width, image.Height);
                Invert(bmp, image);
                this.pictureBox3.Image = bmp;
            });
            Task T2 = filter3.ContinueWith((antecedent) =>
            {

                Image image = pictureBox4.Image;
                Bitmap bmp = new Bitmap(image.Width, image.Height);
                blossome(bmp, image);
                this.pictureBox4.Image = bmp;

            }
            );

            try
            {
                filter1.Wait();
                filter2.Wait();
                filter3.Wait();
            }
            catch (AggregateException ae)
            {

                MessageBox.Show(ae.ToString());
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (fileopen)
            {
                Parallel.ForEach(pictureBoxes, p =>
                {
                    lock (_object)
                    {
                        Image image = p.Image;
                        ImageFlip(image);
                        p.Image = image;
                    }
                });
                Parallel.ForEach(pictureBoxes, p =>
                {
                    lock (_object)
                    {
                        Image image = p.Image;
                        Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                        grayscale(bmpInverted, image);
                        p.Image = bmpInverted;
                    }
                });
            }
            else
            {
                MessageBox.Show("open images");
            }
        }



        private void Button15_Click_1(object sender, EventArgs e)
        {

            if (fileopen)
            {
                Parallel.ForEach(pictureBoxes, p =>
                {
                    lock (_object)
                    {
                        Image image = p.Image;
                        rotate(image);
                        p.Image = image;
                    }
                });
                Parallel.ForEach(pictureBoxes, p =>
                {
                    lock (_object)
                    {
                        Image image = p.Image;
                        Bitmap bmpInverted = new Bitmap(image.Width, image.Height);
                        sapia(bmpInverted, image);
                        p.Image = bmpInverted;
                    }
                });
            }
            else
            {
                MessageBox.Show("open images");
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
      
    
    

    


