using System;

namespace ClassLayer
{
    public class YouTubeVideo : IYouTubeVideo
    {
        private string playlistItemId;
        private string title;
        private string videoId;
        private string url;
        private object thumbnail;

        public YouTubeVideo()
        {
        }

        public YouTubeVideo(string _playlistItemId, string _title, string _videoId, string _url, object _thumbnail)
        {
            PlaylistItemId = _playlistItemId;
            Title = _title;
            VideoId = _videoId;
            Url = _url;
            Thumbnail = _thumbnail;
        }

        public YouTubeVideo(IYouTubeVideo iYouTubeVideo)
        {
            PlaylistItemId = iYouTubeVideo.PlaylistItemId;
            Title = iYouTubeVideo.Title;
            VideoId = iYouTubeVideo.VideoId;
            Url = iYouTubeVideo.Url;
        }

        public string PlaylistItemId
        {
            get { return playlistItemId; }
            set { playlistItemId = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string VideoId
        {
            get { return videoId; }
            set { videoId = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public object Thumbnail
        {
            get { return thumbnail; }
            set { thumbnail = value; }
        }
    }
}
