namespace YouTubePlaylistDownloader.DTO.Interfaces
{
    public interface IYouTubeVideo
    {
        string Title { get; set; }

        string PlaylistItemId { get; set; }

        string VideoId { get; set; }

        string Url { get; set; }

        object Thumbnail { get; set; }

        void Create(string playlistItemId, string title, string videoId, string url, object thumbnail);
    }
}
