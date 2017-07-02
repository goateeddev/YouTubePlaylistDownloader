namespace ClassLayer
{
    public class YouTubePlaylist
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public YouTubePlaylist(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }
    }
}
