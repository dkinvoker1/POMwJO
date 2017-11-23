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
        /// <summary>
        /// image container to display original image
        /// </summary>
        private ImageControler display1; 
        /// <summary>
        /// image container to display image wit hanges in it
        /// </summary>
        private ImageControler display2;
        /// <summary>
        /// Image on which we work with all filters aplied
        /// </summary>
        private itk.simple.Image currentImage;

        public MainWindow()
        {
            InitializeComponent();
            display1 = new ImageControler(imgImage1);
            display2 = new ImageControler(imgImage2);
        }
//=========================================================================================
        /// <summary>
        /// Chose file path and rea current image from it
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
                    OdczytZPliku(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
//=========================================================================================
        /// <summary>
        /// Load image from file
        /// </summary>
        /// <param name="path">file path</param>
        private void OdczytZPliku(string path)
        {
            try
            {
                //Read imput image
                //Remember to set read pixeltype so it doesnt make errors later!
                itk.simple.Image image = SimpleITK.ReadImage(path, PixelIDValueEnum.sitkUInt8);
                //display original image
                display1.Draw(image);


                //binarize image
                var BinaryFilter=new itk.simple.BinaryThresholdImageFilter();
                BinaryFilter.SetUpperThreshold(200);//max original pixel value to be considered object
                BinaryFilter.SetLowerThreshold(0);//min original pixel value to be consideredobject
                BinaryFilter.SetInsideValue(0);//Black as detected object
                BinaryFilter.SetOutsideValue(255);//white as background
                var image2=BinaryFilter.Execute(image);
                display2.Draw(image2);

                currentImage = image2;

                //var image3 = SimpleITK.BinaryErode(image2, 8);
                //display2.Draw(image3);
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
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                MessageBox.Show(ex.ToString());
            }
        }
        //=========================================================================================
        /// <summary>
        /// Chose filepath and write current image into it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bZapisz_Click(object sender, RoutedEventArgs e)
        {
            //Stream myStream = null;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            string wanted_path = System.IO.Directory.GetCurrentDirectory();
            //wanted_path = System.IO.Directory.GetCurrentDirectory();
            for (int i = 0; i < 5; i++)
            {
                wanted_path = System.IO.Path.GetDirectoryName(wanted_path);
            }
            wanted_path += "\\Obrazy";
            saveFileDialog1.InitialDirectory = wanted_path;
            saveFileDialog1.Filter = "jpeg files (*.jpeg)|*.jpeg|png files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 3;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                try
                {
                    ZapisDoPliku(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        //=========================================================================================
        /// <summary>
        /// Save image with all its hanges to file
        /// </summary>
        /// <param name="path">File path</param>
        private void ZapisDoPliku(string path)
        {
            try
            {
                if (currentImage!=null)
                {
                    //Write current image
                    SimpleITK.WriteImage(currentImage, path);
                    MessageBox.Show("Chyba udało się zapisać, srawdź :P"); 
                }
                else
                {
                    MessageBox.Show("Nie istnieje obraz który można zapisać.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //=========================================================================================
        /// <summary>
        /// Erode current image 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bErode_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null)
            {
                try
                {
                    itk.simple.BinaryErodeImageFilter binaryErodeFilter = new BinaryErodeImageFilter();
                    binaryErodeFilter.SetBackgroundValue(255);
                    binaryErodeFilter.SetForegroundValue(0);
                    var image = binaryErodeFilter.Execute(currentImage);
                    display2.Draw(image);
                    currentImage = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Obraz nie istniej, spróbuj go wczytać.");
            }
        }
        //=========================================================================================
        /// <summary>
        /// Dilate current image 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDilate_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage!=null)
            {
                try
                {
                    itk.simple.BinaryDilateImageFilter binaryDilateFilter = new BinaryDilateImageFilter();
                    binaryDilateFilter.SetBackgroundValue(255);
                    binaryDilateFilter.SetForegroundValue(0);
                    var image = binaryDilateFilter.Execute(currentImage);
                    display2.Draw(image);
                    currentImage = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                } 
            }
            else
            {
                MessageBox.Show("Obraz nie istniej, spróbuj go wczytać.");
            }
        }
        //=========================================================================================
        /// <summary>
        /// Rotate current image 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bRotate_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage!=null)
            {
                try
                {
                    int xx = (int)imgImage2.ActualWidth / 2;
                    int yy = (int)imgImage2.ActualHeight / 2;
                    int x = (int)currentImage.GetWidth() / 2;
                    int y = (int)currentImage.GetHeight() / 2;

                    int degrees = 20;
                    double radians = Math.PI * degrees / 180;

                    itk.simple.AffineTransform rotateTransformation = new itk.simple.AffineTransform(2);
                    rotateTransformation.Rotate(0, 1, radians);
                    var center = new itk.simple.VectorDouble();
                    center.Add(xx);
                    center.Add(yy);
                    rotateTransformation.SetCenter(center);

                    itk.simple.ResampleImageFilter imageResampleFilter = new ResampleImageFilter();
                    imageResampleFilter.SetTransform(rotateTransformation);
                    imageResampleFilter.SetReferenceImage(currentImage);
                    var trImage = new itk.simple.Image(currentImage);
                    var image = imageResampleFilter.Execute(trImage);
                    display2.Draw(image);
                    currentImage = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                } 
            }
            else
            {
                MessageBox.Show("Obraz nie istniej, spróbuj go wczytać.");
            }
        }
        //=========================================================================================

    }
}
