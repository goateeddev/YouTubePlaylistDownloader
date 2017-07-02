using System.Windows;
using System.Windows.Controls;

namespace ClassLayer.WPF
{
    public class VideoLabel : TextBlock
    {
        public VideoLabel(string title)
        {
            Text = title;
            Height = 20;
            Width = 350;
            Margin = new Thickness(140, 0, 0, 0);
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
        }
    }
}
