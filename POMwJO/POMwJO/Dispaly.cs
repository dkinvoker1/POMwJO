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
    class Dispaly
    {
        private System.Windows.Controls.Image output;

        public Dispaly(System.Windows.Controls.Image output)
        {
            this.output = output;
        }

        public void Draw(itk.simple.Image image)
        {
            ImageFileWriter writer = new ImageFileWriter();
            writer.SetFileName("bufor.png");
            writer.Execute(image);

            string wanted_path = System.IO.Directory.GetCurrentDirectory();
            ImageSource imageSource = new BitmapImage(new Uri(wanted_path+"\\bufor.png"));
            
            //ImageSource imageSource = new BitmapImage(new Uri("bufor.png", UriKind.Relative));
            output.Source = imageSource;
        }
    }
}
