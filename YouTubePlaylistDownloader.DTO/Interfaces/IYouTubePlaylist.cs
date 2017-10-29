namespace YouTubePlaylistDownloader.DTO.Interfaces
{
    public interface IYouTubePlaylist
    {
        string Name { get; set; }

        string Id { get; set; }

        void Create(string name, string id);
    }
}
