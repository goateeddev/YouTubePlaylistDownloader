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
            Border border = new Border
            {
                BorderThickness = new Thickness(1),
                Height = 350,
                Width = 620,
                BorderBrush = Brushes.Black,
                Margin = new Thickness(-144, -31, 0, 0)
            };
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
                //TODO: work out why any string passed authenticates my user account
                string username = await Logic.GetAccountUsername("email_here");
                tb_email.Visibility = Visibility.Collapsed;
                btn_verify.Visibility = Visibility.Collapsed;
                tb_username.Text = username;
                vb_username.Visibility = Visibility.Visible;

                playlists = await Logic.GetUserPlaylists(playlists);

                foreach (var item in playlists)
                {
                    cb_playlists.Items.Add(item.Name);
                }
                ButtonsEnabled(true);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                System.Windows.MessageBox.Show("Please check your internet connection and try again.", "No internet connection detected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btn_fetch_Click(object sender, RoutedEventArgs e)
        {
            ButtonsEnabled(false);
            if (first) first = false;
            if (!first) ResetVariables(); ClearVideoList();
            img_loader.Visibility = Visibility.Visible;
            img_loader.StartAnimation();

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

            img_loader.StopAnimation();
            img_loader.Visibility = Visibility.Hidden;
            PopulateVideoList();
            ButtonsEnabled(true);
        }

        private void btn_selectall_Click(object sender, RoutedEventArgs e)
        {
            SelectOrDeselectAllCheckBoxes(true);
        }
        private void btn_deselectall_Click(object sender, RoutedEventArgs e)
        {
            SelectOrDeselectAllCheckBoxes(false);
        }

        void SelectOrDeselectAllCheckBoxes(bool value)
        {
            foreach (var panel in pnl_videos.Children)
            {
                if (panel.GetType().Equals(typeof(VideoField)))
                {
                    foreach (var item in (panel as VideoField).Children)
                    {
                        if (item is VideoCheck)
                        {
                            (item as VideoCheck).IsChecked = value;
                        }
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
            DownloadVideos(false);
            ButtonsEnabled(true);
        }

        private void btn_convert_Click(object sender, RoutedEventArgs e)
        {
            ButtonsEnabled(false);
            downloadPath = tb_path.Text;
            DownloadVideos(true);
            ButtonsEnabled(true);
        }

        private bool CheckboxChecked()
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

        private void PopulateVideoList()
        {
            int fieldCount = 0;
            foreach (var video in playlistVideos)
            {
                VideoIcon vi = new VideoIcon(video.Thumbnail as string);
                VideoLabel vl = new VideoLabel(video.Title);
                VideoCheck vc = new VideoCheck();
                VideoField vf = new VideoField(vi, vl, vc, video.Url);
                pnl_videos.Children.Add(vf);
                fieldCount++;
                if (fieldCount < playlistVideos.Count)
                {
                    Border separator = new Border
                    {
                        BorderThickness = new Thickness(0.5),
                        BorderBrush = Brushes.Black,
                        Margin = new Thickness(1, 0, 0, 0)
                    };
                    pnl_videos.Children.Add(separator);
                }
            }
        }

        private void RepopulateVideoList()
        {
            ClearVideoList();
            PopulateVideoList();
        }

        private void ClearVideoList()
        {
            pnl_videos.Children.Clear();
        }

        private void ResetVariables()
        {
            playlists = new List<YouTubePlaylist>();
            playlistVideos = new List<IYouTubeVideo>();
        }

        private void DownloadVideos(bool convert)
        {
            if (CheckboxChecked())
            {
                PopulateUrlDownloadList();
                foreach (string url in UrlDowloadList)
                {
                    bool downloaded = Download.DownloadMethod1(url, downloadPath);
                    string title = playlistVideos.Find(t => t.Url.Equals(url)).Title;
                    if (convert && downloaded)
                    {
                        Converter.ConvertMP4ToMP3(downloadPath, title);
                        File.Delete(Path.Combine(downloadPath, title + ".mp4"));
                    }
                }
                MessageBoxResult remove = System.Windows.MessageBox.Show("Downloads competed. Some of your videos may not have been downloaded due to copyrights." + "\n\n" + "Do you want to remove all downloaded videos from your YouTube playlist?", "Update Playlist", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (remove == MessageBoxResult.Yes)
                {
                    RemoveDownloads();
                    RepopulateVideoList();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a video to download.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void PopulateUrlDownloadList()
        {
            UrlDowloadList = new List<string>();
            foreach (VideoField field in pnl_videos.Children)
            {
                foreach (VideoCheck vc in field.Children)
                    if (vc.IsChecked == true)
                    {
                        UrlDowloadList.Add(field.Name);
                    }
            }
        }

        private void RemoveDownloads()
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
                RemoveDownloads();
            }
        }
    }
}
