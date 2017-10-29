using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using YouTubePlaylistDownloader.Services.YoutubeExtractor;

namespace YouTubePlaylistDownloader.Core.Utilities
{
    public static class Download
    {
        public delegate void ReturnDownloadPercent(double percent);

        public static bool DownloadMethod1(string url, string path, string action, ReturnDownloadPercent returnDownloadPercentage)
        {
            try
            {
                /*
                 * Get the available video formats.
                 * We'll work with them in the video and audio download examples.
                 */
                IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url);

                // Select the first .mp4 video with the highest resolution
                VideoInfo video = videoInfos.First(info =>
                {
                    if (action.Equals("download")) return (info.VideoType == VideoType.Mp4 && (info.Resolution == 720 || info.Resolution == 480 || info.Resolution == 360));
                    else return (info.VideoType == VideoType.Mp4 && info.Resolution == 360);
                });

                //If the video has a decrypted signature, decipher it
                if (video.RequiresDecryption) DownloadUrlResolver.DecryptDownloadUrl(video);
                /*
                 * Create the video downloader.
                 * The first argument is the video to download.
                 * The second argument is the path to save the video file.
                 */
                var videoDownloader = new VideoDownloader(video, Path.Combine(path, video.Title + video.VideoExtension));

                // Register the ProgressChanged event and print the current progress
                //videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
                videoDownloader.DownloadProgressChanged += (sender, args) => returnDownloadPercentage(args.ProgressPercentage);

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
    }
}
