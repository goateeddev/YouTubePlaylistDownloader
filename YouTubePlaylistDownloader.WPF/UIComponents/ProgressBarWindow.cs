using System;
using System.Windows;
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

            pb_progressbar.Minimum = 0;
            pb_progressbar.Maximum = 100;
            //SetDuration(percent);
        }

        public void SetProgressValue(double value)
        {
            pb_progressbar.Value = value;
        }

        Duration duration;
        DoubleAnimation doubleanimation;

        private void SetDuration(double percent)
        {
            duration = new Duration(TimeSpan.FromSeconds(percent));
            doubleanimation = new DoubleAnimation(0.0, 100.0, duration);
            pb_progressbar.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, doubleanimation);
        }

        public void UpdateProgress(double progress)
        {
            doubleanimation.Duration = new Duration(TimeSpan.FromSeconds(progress));

        }
    }
}
