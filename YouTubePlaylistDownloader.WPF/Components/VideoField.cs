using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YouTubePlaylistDownloader.WPF.Components
{
    public class VideoField : StackPanel
    {
        string URL = "";

        public VideoField(VideoIcon thumbnail, VideoLabel title, VideoCheck checkbox, string url, int marginTop)
        {
            URL = url;
            Height = 120;
            Width = 610;
            Margin = new Thickness(5, 10, 0, 0);

            
            Children.Add(thumbnail);
            Children.Add(title);
            Children.Add(checkbox);
        }
    }
}
