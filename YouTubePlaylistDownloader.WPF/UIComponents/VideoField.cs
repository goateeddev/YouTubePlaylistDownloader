using System.Windows;
using System.Windows.Controls;

namespace YouTubePlaylistDownloader.WPF.UIComponents
{
    public class VideoField : StackPanel
    {
        public string URL = "";

        public VideoField(VideoIcon thumbnail, VideoLabel title, VideoCheck checkbox, string url)
        {
            URL = url;
            Margin = new Thickness(5, 10, 0, 10);
            Orientation = Orientation.Horizontal;
            
            Children.Add(thumbnail);
            Children.Add(title);
            Children.Add(checkbox);
        }
    }
}
