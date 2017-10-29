using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using YouTubePlaylistDownloader.DTO.DependencyInjection;
using YouTubePlaylistDownloader.DTO.Interfaces;
using System.Net;

namespace YouTubePlaylistDownloader.Services
{
    public class YouTubeServices
    {
        private string USER_EMAIL;
        
        # region Private Methods

        private async Task<YouTubeService> GetYouTubeService()
        {
            UserCredential credential;
            using (var stream = new FileStream("json/client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[]
                    {
                        YouTubeService.Scope.Youtube,
                        YouTubeService.Scope.Youtubepartner,
                        YouTubeService.Scope.YoutubeUpload,
                        YouTubeService.Scope.YoutubepartnerChannelAudit,
                        YouTubeService.Scope.YoutubeReadonly
                    },
                    USER_EMAIL,
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString()));
            }

            return new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });
        }

        private async Task<string> GetUserChannelId(string email)
        {
            USER_EMAIL = email;
            ChannelsResource.ListRequest channelsListRequest = (await GetYouTubeService()).Channels.List("id");
            channelsListRequest.Mine = true;
            ChannelListResponse channelsListResponse = await channelsListRequest.ExecuteAsync();
            return channelsListResponse.Items[0].Id;
        }

        #endregion Private Methods

        #region Public Methods

        public async Task<string> GetAccountUsername(string email)
        {
            string webData;
            string channelId = await GetUserChannelId(email);
            webData = new WebClient().DownloadString("https://www.youtube.com/channel/" + channelId);
            return "Username Here";// Regex.Match(webData, @"<title>  (.+)\n - YouTube</title>", RegexOptions.Singleline).Groups[1].Value;
        }

        public async Task<IYouTubePlaylists> GetUserPlaylists()
        {
            var playlists = DependencyManager.Resolve<IYouTubePlaylists>();
            var request = (await GetYouTubeService()).Playlists.List("snippet");
            request.PageToken = "";
            request.MaxResults = 50;
            request.Mine = true;
            PlaylistListResponse response = await request.ExecuteAsync();
            foreach (var playlist in response.Items)
            {
                var _playlist = DependencyManager.Resolve<IYouTubePlaylist>();
                _playlist.Create(playlist.Snippet.Title, playlist.Id);
                playlists.Add(_playlist);
            }
            return playlists;
        }

        public async Task<IYouTubeVideos> GetPlaylistVideos(string playlistId)
        {
            var service = await GetYouTubeService();
            var nextPageToken = "";
            var playlistVideos = DependencyManager.Resolve<IYouTubeVideos>();
            while (nextPageToken != null)
            {
                PlaylistItemsResource.ListRequest playlistItemsRequest = service.PlaylistItems.List("contentDetails");
                playlistItemsRequest.MaxResults = 50;
                playlistItemsRequest.PlaylistId = playlistId;
                playlistItemsRequest.PageToken = nextPageToken;
                var playlistItemsResponse = await playlistItemsRequest.ExecuteAsync();
                foreach (var playlistItem in playlistItemsResponse.Items)
                {
                    VideosResource.ListRequest videoRequest = service.Videos.List("snippet,contentDetails,status");
                    videoRequest.Id = playlistItem.ContentDetails.VideoId;
                    var videoResponse = await videoRequest.ExecuteAsync();

                    if (videoResponse.Items.Count > 0)
                    {
                        var video = DependencyManager.Resolve<IYouTubeVideo>();
                        video.Create(playlistItem.Id, videoResponse.Items[0].Snippet.Title, videoResponse.Items[0].Id, string.Concat("https://www.youtube.com/watch?v=", videoResponse.Items[0].Id), videoResponse.Items[0].Snippet.Thumbnails.Default__.Url);
                        playlistVideos.Add(video);
                    }
                }
                nextPageToken = playlistItemsResponse.NextPageToken;
            }
            return playlistVideos;
        }

        public async Task RemoveSongFromPlaylistAsync(string playlistItemId)
        {
            var youtubeService = await GetYouTubeService();
            PlaylistItemsResource.DeleteRequest deleteRequest = youtubeService.PlaylistItems.Delete(playlistItemId);
            string response = await deleteRequest.ExecuteAsync();
            // TODO: Return true if playlist was removed based of response.
        }
        
        # endregion Public Methods
    }
}
