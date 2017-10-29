using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using YouTubePlaylistDownloader.DTO.Enums;

namespace YouTubePlaylistDownloader.WPF.UIComponents
{
    public class ProgressBarWindow : PopupWindow
    {
        public ProgressBarWindow(ActionType action, string title, double percent, string filepath)
        {
            tb_action.Text = action.Equals(ActionType.Download) ? "Downloading..." : "Converting...";
            tb_filename.Text += title;
            tb_destination.Text += filepath;
            SetDuration(percent);
        }

        private void SetDuration(double percent)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(percent));
            DoubleAnimation doubleanimation = new DoubleAnimation(0.0, 100.0, duration);
            pb_progressbar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, doubleanimation);
        }
    }
}
