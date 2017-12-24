using System.IO;
using System.Windows.Threading;
using YouTubePlaylistDownloader.Core.Utilities;
using YouTubePlaylistDownloader.DTO.Enums;

namespace YouTubePlaylistDownloader.WPF.UIComponents
{
    public class ProgressBarWindow : PopupWindow
    {
        private readonly Download download = new Download();

        public ProgressBarWindow(ActionType action, string title, string filepath)
        {
            tb_action.Text = action.Equals(ActionType.Download) ? "Downloading..." : "Converting...";
            tb_filename.Text += title;
            tb_destination.Text += filepath;

            pb_progressbar.Minimum = 0;
            pb_progressbar.Maximum = 100;
        }

        public bool Start(ActionType action, string title, string url, string path)
        {
            var downloaded = false;
            while (!downloaded)
            {
                downloaded = download.DownloadMethod1(action, title, url, path);
                download.EventHandler += (sender, args) => SetProgressValue(args.ProgressPercentage);
            }
            if (action.Equals(ActionType.Convert) && downloaded)
            {
                Converter.ConvertMP4ToMP3(path, title);
                File.Delete(Path.Combine(path, title + ".mp4"));
            }
            return downloaded;
        }

        private void SetProgressValue(double value)
        {
            pb_progressbar.Dispatcher.Invoke(() => pb_progressbar.Value = value, DispatcherPriority.Background);
        }
    }
}
