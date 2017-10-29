using System;
using System.Linq;
using System.Threading.Tasks;
using YouTubePlaylistDownloader.DTO.Interfaces;
using YouTubePlaylistDownloader.Services;

namespace YouTubePlaylistDownloader.Core
{
    public class YouTubeServiceBuilder
    {
        private YouTubeServices services = new YouTubeServices();
        
        public async Task<string> GetAccountUsername(string email)
        {
            return await services.GetAccountUsername(email);
        }

        public async Task<IYouTubePlaylists> GetUserPlaylists()
        {
            try
            {
                return await services.GetUserPlaylists();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("|| ERROR || " + e);
                }
            }
            return null;
        }

        private string GetPlaylistsId(IYouTubePlaylists playlists, string playlistName)
        {
            return (from p in playlists where p.Name == playlistName select p.Id).First();
        }

        public async Task<IYouTubeVideos> GetPlaylistVideos(IYouTubePlaylists playlists, string playlistName)
        {
            string playlistId = GetPlaylistsId(playlists, playlistName);
            try
            {
                return await services.GetPlaylistVideos(playlistId);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("|| ERROR || " + e);
                }
            }
            return null;
        }

        public async Task<bool> RemoveSongFromPlaylist(string playlistItemId)
        {
            bool removed = false;
            try
            {
                // TODO: Return true if playlist was removed based of response.
                await services.RemoveSongFromPlaylistAsync(playlistItemId);
                removed = true;
            }
            catch (AggregateException ex)
            {
                removed = false;
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("|| ERROR || " + e);
                }
            }
            return removed;
        }

        private async Task<bool> PlaylistExists(string name)
        {
            var playlists = await GetUserPlaylists();
            return playlists.Exists(name);
        }
    }
}
