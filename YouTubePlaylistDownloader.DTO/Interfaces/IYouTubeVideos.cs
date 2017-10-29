using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubePlaylistDownloader.DTO.Interfaces
{
    public interface IYouTubeVideos : IEnumerable<IYouTubeVideo>
    {
        List<IYouTubeVideo> Videos { get; set; }

        int Count { get; set; }

        void Add(IYouTubeVideo video);

        IYouTubeVideo Find(string match);

        void Remove(IYouTubeVideo video);
    }
}
