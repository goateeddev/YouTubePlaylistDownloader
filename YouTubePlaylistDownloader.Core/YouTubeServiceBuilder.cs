using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YouTubePlaylistDownloader.DTO.Common;
using YouTubePlaylistDownloader.Services;

namespace YouTubePlaylistDownloader.Core
{
    public class YouTubeServiceBuilder
    {
        YouTubeServices services = new YouTubeServices();

        public YouTubeServiceBuilder()
        {
            // Default Constructor
        }

        public async Task<string> GetAccountUsername(string email)
        {
            string webData;
            string channelId = await services.GetUserChannelId(email);
            webData = new WebClient().DownloadString("https://www.youtube.com/channel/" + channelId);
            return Regex.Match(webData, @"<title>  (.+)\n - YouTube</title>", RegexOptions.Singleline).Groups[1].Value;
        }

        public async Task<List<YouTubePlaylist>> GetUserPlaylists(List<YouTubePlaylist> playlists)
        {
            try
            {
                await services.GetUserPlaylists(playlists);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("|| ERROR || " + e);
                }
            }
            return playlists;
        }

        public string GetPlaylistsId(string playlistName, List<YouTubePlaylist> playlists)
        {
            return (from p in playlists where p.Name == playlistName select p.Id).First();
        }

        public async Task<List<IYouTubeVideo>> GetPlaylistVideos(string playlistId, List<IYouTubeVideo> playlistVideos)
        {
            try
            {
                await services.GetPlaylistVideos(playlistId, playlistVideos);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("|| ERROR || " + e);
                }
            }
            return playlistVideos;
        }

        public async Task<bool> RemoveSongFromPlaylist(string playlistItemId)
        {
            bool removed = false;
            try
            {
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

        public async Task<bool> PlaylistExists(string name)
        {
            List<YouTubePlaylist> playlists = new List<YouTubePlaylist>();
            playlists = await GetUserPlaylists(playlists);
            foreach (var pl in playlists)
            {
                if (pl.Name.Equals(name)) return true;
            }
            return false;
        }

    }
}
