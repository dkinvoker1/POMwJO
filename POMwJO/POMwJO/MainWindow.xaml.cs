using itk.simple;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POMwJO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
//=========================================================================================
        /// <summary>
        /// Wybranie ścieżki do wczytania pliku i wykonanie funkcji testowej(tymczasowe)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bWczytaj_Click(object sender, RoutedEventArgs e)
        {
            //Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            string wanted_path = System.IO.Directory.GetCurrentDirectory();
            //wanted_path = System.IO.Directory.GetCurrentDirectory();
            for (int i = 0; i < 5; i++)
            {
                wanted_path = System.IO.Path.GetDirectoryName(wanted_path);
            }
            wanted_path += "\\Obrazy";
            openFileDialog1.InitialDirectory = wanted_path;
            openFileDialog1.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    TestowyOdczytIZapis(openFileDialog1.FileName);
                    //if ((myStream = openFileDialog1.OpenFile()) != null)
                    //{
                    //    using (myStream)
                    //    {
                    //        // Insert code to read the stream here.
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
//=========================================================================================
        /// <summary>
        /// Wczytanie obrazu z zadanie jścieżki i zapisanie go obok pod inną nazwą(tymczasowe)
        /// </summary>
        /// <param name="patch">Ścieżka do pliku</param>
        private void TestowyOdczytIZapis(string patch)
        {

            string[] args = new string[3];
            //string s = System.AppDomain.CurrentDomain.BaseDirectory;
            //for (int i = 0; i < 5; i++)
            //{
            //    s = s.Remove(s.LastIndexOf("\\", s.Length - 2) + 1);
            //}
            //args[0] = s + "Obrazy\\abc.png";
            //args[1] = "1";
            //args[2] = s + "Obrazy\\cba.png";

            args[0] = patch;
            args[1] = "1";
            args[2] = patch.Insert(patch.LastIndexOf(".png"),"-kopia");

            try
            {
                if (args.Length < 3)
                {
                    //Console.WriteLine("Usage: SimpleGausian <imput> <sigma> <output>");
                    lTest.Content = "Usage: SimpleGausian <imput> <sigma> <output>";
                    return;
                }
                //Read imput image
                ImageFileReader reader = new ImageFileReader();
                reader.SetFileName(args[0]);
                itk.simple.Image image = reader.Execute();

                //Remember format
                //itk.simple.Image image2 = new itk.simple.Image(image);

                //PixelIDValueEnum id = PixelIDValueEnum.sitkVectorUInt8;
                //PixelIDValueEnum id = image2.GetPixelID();

                //Execute Gausian smoothing filter
                //SmoothingRecursiveGaussianImageFilter gausian = new SmoothingRecursiveGaussianImageFilter();
                //gausian.SetSigma(Double.Parse(args[1]));
                //image = gausian.Execute(image);

                //Convert?
                //PixelIDValueEnum id = image.GetPixelID();
                //SimpleITK.Cast(image, id);
                //id = image.GetPixelID();

                //Write output image
                ImageFileWriter writer = new ImageFileWriter();
                writer.SetFileName(args[2]);
                writer.Execute(image);

                lTest.Content = "Chyba się udało, sprwadź :P";
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                lTest.Content = ex;
            }
        }
//=========================================================================================
    }
}
