using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using PhotoTinderController;

namespace PhotoTinderView
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Funkcja dodawania wszsytkich zdjec z folderu                     OK
        // Dodanie eventow od klawiszy                                      OK
        // Dodanie menu rozwijanego z dodawaniem sciezki (opcjonalnie)      OK
        // Funkcja kopiujaca zdjecia do podfolderow                         OK
        // Funkcja usuwajaca zdjecie    
        // Sprobowac zamienic klase na interfejs w MainWindow               OK

        public PhotoTinderController.ILoadImages _loadImages;

        public MainWindow()
        {
            InitializeComponent();
            _loadImages = new LoadImages();
            this.KeyDown += new System.Windows.Input.KeyEventHandler(MainWindow_KeyDown);
        }

        private void Button_Delete_Photo(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void Button_Move_Chosen_Photo(object sender, RoutedEventArgs e)
        {
            MoveChosen();
        }

        private void Button_Move_All_Photo(object sender, RoutedEventArgs e)
        {
            MoveAll();
        }

        private void Button_Load_Photo(object sender, RoutedEventArgs e)
        {
            LoadContent();
        }

        private void LoadFolder_Click(object sender, RoutedEventArgs e)
        {
            LoadContent();
            
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                MoveAll();
            }
            if (e.Key == Key.Left)
            {
                MoveChosen();
            }
            if (e.Key == Key.Down)
            {
                Delete();
            }
            if (e.Key == Key.Up)
            {
                LoadContent();
            }
        }

        public void ShowImage(BitmapImage imageSource)
        {
            ImageFrame.Source = imageSource;
        }
        
        public void LoadContent()
        {
            BitmapImage imageSource = _loadImages.AddImages();
            if (imageSource != null)
            {
                InformationLabel.Content = "";
            }
            else
            {
                InformationLabel.Content = "No images";                
            }
            ShowImage(imageSource);
        }

        public void MoveAll()
        {
            BitmapImage imageSource = _loadImages.CopyImageToDir("/all");
            if (imageSource != null)
            {
                InformationLabel.Content = "";
            }
            else
            {
                InformationLabel.Content = "No images";                
            }
            ShowImage(imageSource);
        }

        public void MoveChosen()
        {
            BitmapImage imageSource = _loadImages.CopyImageToDir("/chosen");
            if (imageSource != null) 
            {
                InformationLabel.Content = "";
            }
            else
            {
                InformationLabel.Content = "No images";                
            }
            ShowImage(imageSource);
        }

        public void Delete()
        {
            BitmapImage imageSource = _loadImages.DeleteImage();
            if (imageSource != null)
            {
                InformationLabel.Content = "";
            }
            else
            {
                InformationLabel.Content = "No images";               
            }
            ShowImage(imageSource);
        }
    }
}
