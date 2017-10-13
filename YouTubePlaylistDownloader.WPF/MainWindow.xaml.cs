using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Windows.Forms;
using YouTubePlaylistDownloader.DTO.Common;
using YouTubePlaylistDownloader.WPF.Components;
using YouTubePlaylistDownloader.Core;
using YouTubePlaylistDownloader.Core.Utilities;

namespace YouTubePlaylistDownloader.WPF
{
    public partial class MainWindow : Window
    {
        YouTubeServiceBuilder Logic = new YouTubeServiceBuilder();
        List<YouTubePlaylist> playlists = new List<YouTubePlaylist>();
        List<IYouTubeVideo> playlistVideos = new List<IYouTubeVideo>();
        List<string> UrlDowloadList = new List<string>();
        VideoList pnl_videos = new VideoList();
        string playlistId, downloadPath;
        bool first = true;

        public MainWindow()
        {
            InitializeComponent();

            Initialisations();
        }

        private void Initialisations()
        {
            Border border = new Border();
            border.BorderThickness = new Thickness(1);
            border.Height = 350;
            border.Width = 620;
            border.BorderBrush = Brushes.Black;
            border.Margin = new Thickness(-144,-31,0,0);
            window.Children.Add(border);

            scrollview.Content = pnl_videos;

            tb_path.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            downloadPath = tb_path.Text;
            ButtonsEnabled(false);
        }

        private async void btn_verify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //lbl_username.Text
                string username = await Logic.GetAccountUsername("email_here");
                playlists = await Logic.GetUserPlaylists(playlists);

                foreach (var item in playlists)
                {
                    cb_playlists.Items.Add(item.Name);
                }
                ButtonsEnabled(true);
                //Controls.Remove(pnl_verify);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                System.Windows.MessageBox.Show("Please check your internet connect and try again.", "No internet connection detected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btn_fetch_Click(object sender, RoutedEventArgs e)
        {
            ButtonsEnabled(false);
            if (first) first = false;
            if (!first) resetVariables(); clearVideoList();

            /*PictureBox loader = new PictureBox();
            loader.Image = Image.FromFile("img/loader.gif");
            loader.SizeMode = PictureBoxSizeMode.AutoSize;
            loader.BackColor = Color.Transparent;
            loader.Location = new Point(pnl_videos.Width / 2, 200);
            pnl_videos.Controls.Add(loader);*/

            try
            {
                playlists = await Logic.GetUserPlaylists(playlists);
                playlistId = Logic.GetPlaylistsId(cb_playlists.Text, playlists);
                playlistVideos = await Logic.GetPlaylistVideos(playlistId, playlistVideos);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                System.Windows.MessageBox.Show("Please check your internet connect and try again.", "No Internet Connection Detected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException)
            {
                System.Windows.MessageBox.Show("Please select a playlist.", "No Playlist Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //pnl_videos.Controls.Remove(loader);

            populateVideoList();
            ButtonsEnabled(true);
        }

        private void btn_selectall_Click(object sender, RoutedEventArgs e)
        {
            foreach (VideoField panel in pnl_videos.Children)
            {
                foreach (var item in panel.Children)
                {
                    if (item is VideoCheck)
                    {
                        (item as VideoCheck).IsChecked = true;
                    }
                }
            }
        }

        private void btn_deselectall_Click(object sender, RoutedEventArgs e)
        {
            foreach (VideoField panel in pnl_videos.Children)
            {
                foreach (var item in panel.Children)
                {
                    if (item is VideoCheck)
                    {
                        (item as VideoCheck).IsChecked = false;
                    }
                }
            }
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                downloadPath = fbd.SelectedPath;
            }
            tb_path.Text = downloadPath;
        }

        private void btn_download_Click(object sender, RoutedEventArgs e)
        {
            ButtonsEnabled(false);
            downloadPath = tb_path.Text;
            downloadVideos(false);
            ButtonsEnabled(true);
        }

        private void btn_convert_Click(object sender, RoutedEventArgs e)
        {
            ButtonsEnabled(false);
            downloadPath = tb_path.Text;
            downloadVideos(true);
            ButtonsEnabled(true);
        }

        private bool checkboxChecked()
        {
            foreach (VideoField panel in pnl_videos.Children)
            {
                foreach (var item in panel.Children)
                {
                    if (item is VideoCheck)
                    {
                        if ((item as VideoCheck).IsChecked == true) return true;
                    }
                }
            }
            return false;
        }

        private void ButtonsEnabled(bool enabled)
        {
            if (enabled)
            {
                btn_fetch.IsEnabled = true;
                btn_download.IsEnabled = true;
                btn_convert.IsEnabled = true;
                btn_selectall.IsEnabled = true;
                btn_deselectall.IsEnabled = true;
                btn_browse.IsEnabled = true;
            }
            else
            {
                btn_fetch.IsEnabled = false;
                btn_download.IsEnabled = false;
                btn_convert.IsEnabled = false;
                btn_selectall.IsEnabled = false;
                btn_deselectall.IsEnabled = false;
                btn_browse.IsEnabled = false;
            }
        }

        private void populateVideoList()
        {
            int marginTop = 5;
            foreach (var video in playlistVideos)
            {
                VideoIcon vi = new VideoIcon(video.Thumbnail as string);
                VideoLabel vl = new VideoLabel(video.Title);
                VideoCheck vc = new VideoCheck();
                VideoField vf = new VideoField(vi, vl, vc, video.Url, marginTop);
                pnl_videos.Children.Add(vf);

                //vf.Name = video.Title;
                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.Height = vf.Height;
                border.Width = vf.Width;
                border.BorderBrush = Brushes.Black;
                //pnl_videos.Children.Add(border);

                //marginTop += 5;
            }
        }

        private void repopulateVideoList()
        {
            clearVideoList();
            populateVideoList();
        }

        private void clearVideoList()
        {
            pnl_videos.Children.Clear();
        }

        private void resetVariables()
        {
            playlists = new List<YouTubePlaylist>();
            playlistVideos = new List<IYouTubeVideo>();
        }

        private void downloadVideos(bool convert)
        {
            if (checkboxChecked())
            {
                populateUrlDownloadList();
                foreach (string url in UrlDowloadList)
                {
                    bool downloaded = Download.DownloadMethod1(url, downloadPath);
                    string title = playlistVideos.Find(t => t.Url.Equals(url)).Title;
                    if (convert && downloaded)
                    {
                        Converter.ConvertMP4ToMP3(downloadPath, title);
                        File.Delete(System.IO.Path.Combine(downloadPath, title + ".mp4"));
                    }
                }
                MessageBoxResult remove = System.Windows.MessageBox.Show("Downloads competed. Some of your videos may not have been downloaded due to copyrights." + "\n\n" + "Do you want to remove all downloaded videos from your YouTube playlist?", "Update Playlist", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (remove == MessageBoxResult.Yes)
                {
                    removeDownloads();
                    repopulateVideoList();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a video to download.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void populateUrlDownloadList()
        {
            UrlDowloadList = new List<string>();
            foreach (VideoField field in pnl_videos.Children)
            {
                foreach(VideoCheck vc in field.Children)
                if (vc.IsChecked == true)
                {
                    UrlDowloadList.Add(field.Name);
                }
            }
        }

        private void removeDownloads()
        {
            try
            {
                foreach (IYouTubeVideo video in playlistVideos)
                {
                    if (File.Exists(downloadPath + "\\" + video.Title + ".mp4"))
                    {
                        playlistVideos.Remove(video);
                        foreach (VideoField field in pnl_videos.Children)
                        {
                            if (field.Name.Equals(video.Title))
                            {
                                pnl_videos.Children.Remove(field);
                            }
                        }
                        Task.Run(() => Logic.RemoveSongFromPlaylist(video.PlaylistItemId).Wait());
                    }
                }
            }
            catch (InvalidOperationException)
            {
                removeDownloads();
            }
        }
    }
}
