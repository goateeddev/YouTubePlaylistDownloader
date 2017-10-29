using System.Collections;
using System.Collections.Generic;
using YouTubePlaylistDownloader.DTO.Interfaces;

namespace YouTubePlaylistDownloader.DTO
{
    public class YouTubePlaylists : IYouTubePlaylists
    {
        private List<IYouTubePlaylist> _playlists { get; set; }

        public List<IYouTubePlaylist> Playlists
        {
            get { return _playlists.Equals(null) ? new List<IYouTubePlaylist>() : _playlists; }
            set { _playlists = value; }
        }
        
        public void Add(IYouTubePlaylist playlist)
        {
            _playlists = _playlists ?? new List<IYouTubePlaylist>();
            _playlists.Add(playlist);
        }

        public bool Exists(string name)
        {
            return _playlists.Exists(pl => pl.Name.Equals(name));
        }

        public IEnumerator<IYouTubePlaylist> GetEnumerator()
        {
            return _playlists.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _playlists.GetEnumerator();
        }
    }
}
