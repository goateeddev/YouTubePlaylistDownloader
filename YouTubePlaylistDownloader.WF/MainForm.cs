using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using YouTubePlaylistDownloader.WF.Components;
using YouTubePlaylistDownloader.DTO.Interfaces;
using YouTubePlaylistDownloader.Core;
using YouTubePlaylistDownloader.Core.Utilities;
using YouTubePlaylistDownloader.DTO;
using YouTubePlaylistDownloader.DTO.DependencyInjection;
using YouTubePlaylistDownloader.DTO.Enums;

namespace YouTubePlaylistDownloader.WF
{
    public partial class MainForm : Form
    {
        YouTubeServiceBuilder YouTubeServiceBuilder = new YouTubeServiceBuilder();
        IYouTubePlaylists playlists = DependencyManager.Resolve<IYouTubePlaylists>();
        IYouTubeVideos playlistVideos = DependencyManager.Resolve<IYouTubeVideos>();
        List<string> UrlDowloadList = new List<string>();
        VideoList pnl_videos = new VideoList();
        string playlistId, downloadPath;
        bool first = true;

        public MainForm()
        {
            InitializeComponent();
            
            Task.Run(() => Initialisations()).Wait();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int thickness = 1;
            int halfThickness = thickness / 2;
            using (Pen p = new Pen(Color.Black, thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(31, 67, 641, 411));
            }
        }

        private void Initialisations()
        {
            BackColor = Color.WhiteSmoke;
            Controls.Add(pnl_videos);
            tb_path.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            downloadPath = tb_path.Text;
            ButtonsEnabled(false);
        }

        private async void btn_verify_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_username.Text = await YouTubeServiceBuilder.GetAccountUsername("email_here");
                playlists = await YouTubeServiceBuilder.GetUserPlaylists();

                foreach (var item in playlists)
                {
                    cb_playlists.Items.Add(item.Name);
                }
                ButtonsEnabled(true);
                Controls.Remove(pnl_verify);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Please check your internet connect and try again.", "No internet connection detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_fetch_Click(object sender, EventArgs e)
        {
            ButtonsEnabled(false);
            if (first) first = false;
            if (!first) resetVariables(); clearVideoList();

            PictureBox loader = new PictureBox();
            loader.Image = Image.FromFile("img/loader.gif");
            loader.SizeMode = PictureBoxSizeMode.AutoSize;
            loader.BackColor = Color.Transparent;
            loader.Location = new Point(pnl_videos.Width/2, 200);
            pnl_videos.Controls.Add(loader);

            try
            {
                playlists = await YouTubeServiceBuilder.GetUserPlaylists();
                playlistVideos = await YouTubeServiceBuilder.GetPlaylistVideos(playlists, cb_playlists.Text);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Please check your internet connect and try again.", "No Internet Connection Detected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Please select a playlist.", "No Playlist Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            pnl_videos.Controls.Remove(loader);

            populateVideoList();
            ButtonsEnabled(true);
        }

        private void btn_selectall_Click(object sender, EventArgs e)
        {
            foreach (VideoField panel in pnl_videos.Controls)
            {
                foreach (var item in panel.Controls)
                {
                    if (item is VideoCheck)
                    {
                        (item as VideoCheck).Checked = true;
                    }
                }
            }
        }

        private void btn_deselectall_Click(object sender, EventArgs e)
        {
            foreach (VideoField panel in pnl_videos.Controls)
            {
                foreach (var item in panel.Controls)
                {
                    if (item is VideoCheck)
                    {
                        (item as VideoCheck).Checked = false;
                    }
                }
            }
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                downloadPath = fbd.SelectedPath;
            }
            tb_path.Text = downloadPath;
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            ButtonsEnabled(false);
            downloadPath = tb_path.Text;
            DownloadVideos(ActionType.Download);
            ButtonsEnabled(true);
        }

        private void btn_convert_Click(object sender, EventArgs e)
        {
            ButtonsEnabled(false);
            downloadPath = tb_path.Text;
            DownloadVideos(ActionType.Convert);
            ButtonsEnabled(true);
        }

        private bool checkboxChecked()
        {
            foreach (VideoField panel in pnl_videos.Controls)
            {
                foreach (var item in panel.Controls)
                {
                    if (item is VideoCheck)
                    {
                        if((item as VideoCheck).Checked) return true;
                    }
                }
            }
            return false;
        }

        private void ButtonsEnabled(bool enabled)
        {
            if (enabled)
            {
                btn_fetch.Enabled = true;
                btn_download.Enabled = true;
                btn_convert.Enabled = true;
                btn_selectall.Enabled = true;
                btn_deselectall.Enabled = true;
                btn_browse.Enabled = true;
            }
            else
            {
                btn_fetch.Enabled = false;
                btn_download.Enabled = false;
                btn_convert.Enabled = false;
                btn_selectall.Enabled = false;
                btn_deselectall.Enabled = false;
                btn_browse.Enabled = false;
            }
        }

        private void populateVideoList()
        {
            int y = 5;
            foreach (var video in playlistVideos)
            {
                VideoIcon vi = new VideoIcon(video.Thumbnail as string, 5);
                VideoLabel vl = new VideoLabel(video.Title, 35);
                VideoCheck vc = new VideoCheck(35);
                VideoField vf = new VideoField(vi, vl, vc, video.Url, y);
                vf.Name = video.Title;
                pnl_videos.Controls.Add(vf);
                y += 105;
            }
        }

        private void repopulateVideoList()
        {
            clearVideoList();
            int y = 5;
            foreach (var video in playlistVideos)
            {
                VideoIcon vi = new VideoIcon(video.Thumbnail as string, 5);
                VideoLabel vl = new VideoLabel(video.Title, 35);
                VideoCheck vc = new VideoCheck(35);
                VideoField vf = new VideoField(vi, vl, vc, video.Url, y);
                vf.Name = video.Title;
                pnl_videos.Controls.Add(vf);
                y += 105;
            }
        }

        private void clearVideoList()
        {
            while (pnl_videos.Controls.Count > 0)
            {
                foreach (Control item in pnl_videos.Controls)
                {
                    if(!(item is PictureBox)) pnl_videos.Controls.Remove(item);
                }
            }
        }

        private void resetVariables()
        {
            playlists = DependencyManager.Resolve<IYouTubePlaylists>();
            playlistVideos = DependencyManager.Resolve<IYouTubeVideos>();
        }

        private readonly Download download = new Download();
        private void DownloadVideos(ActionType action)
        {
            if (checkboxChecked())
            {
                PopulateUrlDownloadList();
                foreach (string url in UrlDowloadList)
                {
                    var title = playlistVideos.Find(url).Title;
                    bool downloaded = download.DownloadMethod1(action, title, url, downloadPath);
                    if (action.Equals(ActionType.Convert) && downloaded)
                    {
                        Converter.ConvertMP4ToMP3(downloadPath, title);
                        File.Delete(Path.Combine(downloadPath, title + ".mp4"));
                    }
                }
                DialogResult remove = MessageBox.Show("Downloads competed. Some of your videos may not have been downloaded due to copyrights." + "\n\n" + "Do you want to remove all downloaded videos from your YouTube playlist?", "Update Playlist", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (remove == DialogResult.Yes)
                {
                    removeDownloads();
                    repopulateVideoList();
                }
            }
            else
            {
                MessageBox.Show("Please select a video to download.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PopulateUrlDownloadList()
        {
            UrlDowloadList = new List<string>();
            foreach (VideoField field in pnl_videos.Controls)
            {
                VideoCheck vc = field.GetChildAtPoint(new Point(545, 45)) as VideoCheck;
                if (vc.Checked)
                {
                    UrlDowloadList.Add(field.Text);
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
                        foreach (VideoField field in pnl_videos.Controls)
                        {
                            if (field.Name.Equals(video.Title))
                            {
                                pnl_videos.Controls.Remove(field);
                            }
                        }
                        Task.Run(() => YouTubeServiceBuilder.RemoveSongFromPlaylist(video.PlaylistItemId).Wait());
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
