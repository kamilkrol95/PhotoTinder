using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace PhotoTinderController
{
    public class LoadImages : ILoadImages
    {

        private static Queue<String> PicturesPathsQueue;
        private static String FolderPath;        
        public static void Main()
        {

        }
        public BitmapImage LoadNextImage(Queue<String> ImagesQueue)
        {
            var src = new BitmapImage();

            if (ImagesQueue.Count > 0)
            {
                var imagePath = ImagesQueue.Peek();

                if (File.Exists(imagePath))
                {
                    src.BeginInit();
                    src.UriSource = new Uri(ImagesQueue.Peek(), UriKind.Absolute);
                    src.EndInit();

                    return src;
                }
            }
            return null;
        }
        public BitmapImage CopyImageToDir(String folderName)
        {

            if (PicturesPathsQueue.Count > 0)
            {
                var fileToCopy = PicturesPathsQueue.Peek();
                PicturesPathsQueue.Dequeue();
                var fileName = fileToCopy.Substring(FolderPath.Length);
                var newFolderPath = FolderPath + folderName;

                if (Directory.Exists(newFolderPath))
                {
                    if (File.Exists(fileToCopy))
                    {
                        File.Copy(fileToCopy, newFolderPath + fileName, true);
                        Console.WriteLine(fileName + " copied!");
                    }
                    else
                    {
                        Console.WriteLine("No such file!");
                    }
                }
                else
                {
                    Console.WriteLine("No such directory!");
                }
            }
            else
            {
                Console.WriteLine("No more photos!");
            }
            return LoadNextImage(PicturesPathsQueue);
        }

        public BitmapImage DeleteImage()
        {
            if (PicturesPathsQueue.Count > 0)
            {
                PicturesPathsQueue.Dequeue();
            }

            return LoadNextImage(PicturesPathsQueue);
        }
        
        public BitmapImage AddImages()
        {

            FolderBrowserDialog folderPathDialog = new FolderBrowserDialog();

            if (folderPathDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String folderPath = folderPathDialog.SelectedPath;
                FolderPath = folderPath;
                
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
                var files = GetFilesFrom(folderPath, filters);
                WritePaths(files);

                PicturesPathsQueue = ListToQueue(files);
                Directory.CreateDirectory(folderPath + "/all");
                Directory.CreateDirectory(folderPath + "/chosen");
               
                return LoadNextImage(ListToQueue(files));                
            }
            return null;
        }
        private static List<String> GetFilesFrom(String searchFolder, String[] filters)
        {
            List<String> picturesFound = new List<String>();
            foreach (var filter in filters)
            {
                picturesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter)));
            }
            return picturesFound;
        }

        private static void WritePaths(List<String> list)
        {
            foreach (var p in list)
            {
                Console.WriteLine(p.ToString());
            }
        }
        private static Queue<String> ListToQueue(List<String> list)
        {
            return new Queue<string>(list);
        }
    }
}
