using YouTubePlaylistDownloader.DTO.Interfaces;

namespace YouTubePlaylistDownloader.DTO
{
    public class YouTubePlaylist : IYouTubePlaylist
    {
        private string _name;
        private string _id;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public void Create(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
