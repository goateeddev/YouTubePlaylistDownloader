using System;
using System.Collections.Generic;
using System.Linq;
using ClassLayer;
using ServiceLayer;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using YoutubeExtractor;
using System.IO;
using System.Diagnostics;

namespace LogicLayer
{
    public class Logic
    {
        YouTubeServices services = new YouTubeServices();

        public Logic()
        {
            // Default Constructor
        }

        public async Task<string> GetAccountUsername(string email)
        {
            string webData;
            string channelId = await services.GetUserChannelId(email);
            webData = new WebClient().DownloadString("https://www.youtube.com/channel/" + channelId);
            return Regex.Match(webData, @"<title>  (.+)\n - YouTube</title>", RegexOptions.Singleline).Groups[1].Value;
        }

        public async Task<List<YouTubePlaylist>> GetUserPlaylists(List<YouTubePlaylist> playlists)
        {
            try
            {
                await services.GetUserPlaylists(playlists);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("|| ERROR || " + e);
                }
            }
            return playlists;
        }

        public string GetPlaylistsId(string playlistName, List<YouTubePlaylist> playlists)
        {
            return (from p in playlists where p.Name == playlistName select p.Id).First();
        }

        public async Task<List<IYouTubeVideo>> GetPlaylistVideos(string playlistId, List<IYouTubeVideo> playlistVideos)
        {
            try
            {
                await services.GetPlaylistVideos(playlistId, playlistVideos);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("|| ERROR || " + e);
                }
            }
            return playlistVideos;
        }

        public async Task<bool> RemoveSongFromPlaylist(string playlistItemId)
        {
            bool removed = false;
            try
            {
                await services.RemoveSongFromPlaylistAsync(playlistItemId);
                removed = true;
            }
            catch (AggregateException ex)
            {
                removed = false;
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("|| ERROR || " + e);
                }
            }
            return removed;
        }

        public async Task<bool> PlaylistExists(string name)
        {
            List<YouTubePlaylist> playlists = new List<YouTubePlaylist>();
            playlists = await GetUserPlaylists(playlists);
            foreach (var pl in playlists)
            {
                if (pl.Name.Equals(name)) return true;
            }
            return false;
        }

        public bool DownloadMethod1(string url, string path)
        {
            try
            {
                /*
                 * Get the available video formats.
                 * We'll work with them in the video and audio download examples.
                 */
                IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url);

                /*
                 * Select the first .mp4 video with 360p resolution
                 */
                VideoInfo video = videoInfos
                    .First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

                /*
                 * If the video has a decrypted signature, decipher it
                 */
                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }

                /*
                 * Create the video downloader.
                 * The first argument is the video to download.
                 * The second argument is the path to save the video file.
                 */
                var videoDownloader = new VideoDownloader(video, Path.Combine(path, video.Title + video.VideoExtension));

                // Register the ProgressChanged event and print the current progress
                //videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

                /*
                 * Execute the video downloader.
                 * For GUI applications note, that this method runs synchronously.
                 */
                videoDownloader.Execute();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public bool ConvertMP4ToMP3(string path, string filename)
        {
            string input_file = Path.Combine(path, filename + ".mp4");
            string output_name = Path.Combine(path, filename + ".mp3");
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                cmd.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                cmd.StartInfo.WorkingDirectory = @"";
                cmd.StartInfo.Arguments = "/C ffmpeg -i \"" + input_file + "\" -vn -b:a 320k -c:a libmp3lame \"" + output_name + "\"";
                cmd.Start();
                cmd.WaitForExit();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return false;
        }
    }
}
