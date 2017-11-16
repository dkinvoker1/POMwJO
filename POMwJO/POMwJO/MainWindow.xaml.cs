using itk.simple;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        private ImageControler display1;
        private ImageControler display2;
        public MainWindow()
        {
            InitializeComponent();
            display1 = new ImageControler(imgImage1);
            display2 = new ImageControler(imgImage2);
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
            openFileDialog1.Filter = "jpeg files (*.jpeg)|*.jpeg|png files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    TestowyOdczytIZapis(openFileDialog1.FileName);
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

            args[0] = patch;
            args[1] = "1";
            args[2] = patch.Insert(patch.LastIndexOf("."),"-kopia");

            try
            {
                if (args.Length < 3)
                {
                    lTest.Content = "Usage: SimpleGausian <imput> <sigma> <output>";
                    return;
                }
                //Read imput image
                //ImageFileReader reader = new ImageFileReader();
                //reader.SetFileName(args[0]);
                //itk.simple.Image image = reader.Execute();
                itk.simple.Image image = SimpleITK.ReadImage(args[0]);

                display1.Draw(image);


                //var image2 = new itk.simple.Image(image);
                var image2 = SimpleITK.Add(50,image);
                display2.Draw(image2);
                //Convert?
                //albo obraz wczytać w skali szarości(wymusić)
                //albo rozbić na skale kolorów itd(rgb to grayscale filter pewnie)
                //PixelIDValueEnum id = image.GetPixelID();
                //SimpleITK.Cast(image, itk.simple.PixelIDValueEnum.sitkUInt8);
                //id = image.GetPixelID();

                //Write output image
                //ImageFileWriter writer = new ImageFileWriter();
                //writer.SetFileName(args[2]);
                //writer.Execute(image);

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
