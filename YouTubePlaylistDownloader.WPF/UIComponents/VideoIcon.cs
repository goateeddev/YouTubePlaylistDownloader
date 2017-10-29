using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace YouTubePlaylistDownloader.WPF.UIComponents
{
    public class VideoIcon : Image
    {
        public VideoIcon(string url)
        {
            Source = new BitmapImage(new Uri(url, UriKind.Absolute));
            Height = 90;
            Width = 120;
            Margin = new Thickness(5,0,0,0);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
