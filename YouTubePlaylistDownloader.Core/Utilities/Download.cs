using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YouTubePlaylistDownloader.Core.Handlers;
using YouTubePlaylistDownloader.DTO.Enums;
using YouTubePlaylistDownloader.Services.YoutubeExtractor;

namespace YouTubePlaylistDownloader.Core.Utilities
{
    public class Download
    {
        public event EventHandler<ProgressBarEventArgs> EventHandler;

        public bool DownloadMethod1(ActionType action, string title, string url, string path)
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
                    if (action.Equals(ActionType.Download)) return (info.VideoType == VideoType.Mp4 && (info.Resolution == 720 || info.Resolution == 480 || info.Resolution == 360));
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

                // Register the ProgressChanged event and return current progress to ProgressBar
                videoDownloader.DownloadProgressChanged += (sender, args) => EventHandler(this, new ProgressBarEventArgs(args.ProgressPercentage));
                
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
