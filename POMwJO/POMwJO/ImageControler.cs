using itk.simple;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace POMwJO
{
    class ImageControler
    {
        /// <summary>
        /// Windows Image Control to be output for visualisation
        /// </summary>
        private System.Windows.Controls.Image output;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="output">Windows Image Control to be output for visualisation</param>
        public ImageControler(System.Windows.Controls.Image output)
        {
            this.output = output;
        }
        /// <summary>
        /// Function that displays provided itkImage on Windows Image Control 
        /// </summary>
        /// <param name="image">itkImage to be displayed</param>
        public void Draw(itk.simple.Image image)
        {
            ImageFileWriter writer = new ImageFileWriter();
            writer.SetFileName("bufor.png");
            writer.Execute(image);
            writer.Dispose();

            string wanted_path = System.IO.Directory.GetCurrentDirectory();

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.UriSource = new Uri(wanted_path + "\\bufor.png");
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            //ImageSource imageSource = new BitmapImage(new Uri(wanted_path+"\\bufor.png"));
            output.Source = bitmap;
        }
        /// <summary>
        /// Function that displays provided itkImage on Windows Image Control 
        /// </summary>
        /// <param name="image">itkImage to be displayed</param>
        public void Draw2(itk.simple.Image image)
        {
            var height= 100;
            var width = 100;
            var bitmap = new Bitmap(width, height);
            for(int i = 0; i < height; i++)
			{
                for (int j = 0; j < width; j++)
                {

                    VectorUInt32 vec = new VectorUInt32();
                    vec.Add((uint)i);
                    vec.Add((uint)j);
                    var pixel = image.GetPixelAsVectorUInt8(vec);
                    bitmap.SetPixel(i, j, System.Drawing.Color.FromArgb(pixel[0], pixel[1], pixel[2]));
                }
            }
            var bitmapImage = new BitmapImage();
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }

            //ImageSource imageSource = new BitmapImage(new Uri(wanted_path+"\\bufor.png"));
            output.Source = bitmapImage;
        }
    }
}
