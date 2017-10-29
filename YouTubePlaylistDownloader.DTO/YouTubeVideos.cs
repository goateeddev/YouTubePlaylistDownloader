using System.Collections;
using System.Collections.Generic;
using YouTubePlaylistDownloader.DTO.Interfaces;

namespace YouTubePlaylistDownloader.DTO
{
    public class YouTubeVideos : IYouTubeVideos
    {
        private List<IYouTubeVideo> _videos { get; set; }

        private int _count { get; set; }

        public List<IYouTubeVideo> Videos
        {
            get { return _videos.Equals(null) ? new List<IYouTubeVideo>() : _videos; }
            set { _videos = value; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = _videos.Count; }
        }

        public void Add(IYouTubeVideo video)
        {
            _videos = _videos ?? new List<IYouTubeVideo>();
            _videos.Add(video);
        }

        public IEnumerator<IYouTubeVideo> GetEnumerator()
        {
            return _videos.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _videos.GetEnumerator();
        }

        public IYouTubeVideo Find(string match)
        {
            return _videos.Find(v => v.Url.Equals(match));
        }

        public void Remove(IYouTubeVideo video)
        {
            _videos.Remove(video);
        }
    }
}
