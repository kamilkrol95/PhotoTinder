using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoTinderController
{
    public interface ILoadImages
    {
        BitmapImage LoadNextImage(Queue<String> ImagesQueue);
        BitmapImage CopyImageToDir(String folderName);
        BitmapImage DeleteImage();
        BitmapImage AddImages();

    }
}
