using System.Windows;
using System.Windows.Controls;

namespace YouTubePlaylistDownloader.WPF.UIComponents
{
    public class VideoLabel : TextBlock
    {
        public VideoLabel(string title)
        {
            Text = title;
            MaxHeight = 60;
            Width = 350;
            TextWrapping = TextWrapping.Wrap;
            Margin = new Thickness(10, 0, 0, 0);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
