using System.Collections.Generic;

namespace YouTubePlaylistDownloader.DTO.Interfaces
{
    public interface IYouTubePlaylists : IEnumerable<IYouTubePlaylist>
    {
        List<IYouTubePlaylist> Playlists { get; set; }

        void Add(IYouTubePlaylist playlist);

        bool Exists(string value);
    }
}
