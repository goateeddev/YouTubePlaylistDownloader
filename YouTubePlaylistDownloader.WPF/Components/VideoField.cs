using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YouTubePlaylistDownloader.WPF.Components
{
    public class VideoField : StackPanel
    {
        string URL = "";

        public VideoField(VideoIcon thumbnail, VideoLabel title, VideoCheck checkbox, string url)
        {
            URL = url;
            //Height = 120;
            //Width = 700;
            Margin = new Thickness(5, 10, 0, 10);
            Orientation = Orientation.Horizontal;
            
            Children.Add(thumbnail);
            Children.Add(title);
            Children.Add(checkbox);
        }
    }
}
