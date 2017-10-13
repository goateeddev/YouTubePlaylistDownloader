namespace YouTubePlaylistDownloader.DTO.Common
{
    public interface IYouTubeVideo
    {
        string Title { get; set; }

        string PlaylistItemId { get; set; }

        string VideoId { get; set; }

        string Url { get; set; }

        object Thumbnail { get; set; }
    }
}
