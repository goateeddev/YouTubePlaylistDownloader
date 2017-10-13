using System.Windows;
using System.Windows.Controls;

namespace YouTubePlaylistDownloader.WPF.Components
{
    public class VideoCheck : CheckBox
    {
        public VideoCheck()
        {
            Height = 13;
            Width = 13;
            Margin = new Thickness(500, 0, 0, 0);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
        }
    }
}
