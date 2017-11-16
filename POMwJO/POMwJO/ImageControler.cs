using itk.simple;
using System;
using System.Collections.Generic;
using System.Drawing;
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

    }
}
