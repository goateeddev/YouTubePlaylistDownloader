using YouTubePlaylistDownloader.DTO.Interfaces;

namespace YouTubePlaylistDownloader.DTO
{
    public class YouTubeVideo : IYouTubeVideo
    {
        private string _playlistItemId;
        private string _title;
        private string _videoId;
        private string _url;
        private object _thumbnail;

        public void Create(string playlistItemId, string title, string videoId, string url, object thumbnail)
        {
            PlaylistItemId = playlistItemId;
            Title = title;
            VideoId = videoId;
            Url = url;
            Thumbnail = thumbnail;
        }

        public void CreateFromYouTubeVideo(IYouTubeVideo iYouTubeVideo)
        {
            PlaylistItemId = iYouTubeVideo.PlaylistItemId;
            Title = iYouTubeVideo.Title;
            VideoId = iYouTubeVideo.VideoId;
            Url = iYouTubeVideo.Url;
        }

        public string PlaylistItemId
        {
            get { return _playlistItemId; }
            set { _playlistItemId = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string VideoId
        {
            get { return _videoId; }
            set { _videoId = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public object Thumbnail
        {
            get { return _thumbnail; }
            set { _thumbnail = value; }
        }
    }
}
