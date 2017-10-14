using System.Windows;
using System.Windows.Controls;

namespace YouTubePlaylistDownloader.WPF.Components
{
    public class VideoCheck : CheckBox
    {
        public VideoCheck()
        {
            Height = 15;
            Width = 15;
            Margin = new Thickness(50, 0, 0, 0);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
