using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using YouTubePlaylistDownloader.DTO.Common;

namespace YouTubePlaylistDownloader.Services
{
    public class YouTubeServices
    {
        private string USER_EMAIL;

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

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            return youtubeService;
        }

        public async Task<string> GetUserChannelId(string email)
        {
            USER_EMAIL = email;
            var service = await GetYouTubeService();
            ChannelsResource.ListRequest channelsListRequest = service.Channels.List("id");
            channelsListRequest.Mine = true;
            ChannelListResponse channelsListResponse = await channelsListRequest.ExecuteAsync();
            return channelsListResponse.Items[0].Id;
        }

        public async Task GetUserPlaylists(List<YouTubePlaylist> playlists)
        {
            var service = await GetYouTubeService();
            var request = service.Playlists.List("snippet");
            request.PageToken = "";
            request.MaxResults = 50;
            request.Mine = true;
            PlaylistListResponse response = await request.ExecuteAsync();
            foreach (var playlist in response.Items)
            {
                playlists.Add(new YouTubePlaylist(playlist.Snippet.Title, playlist.Id));
            }
        }

        public async Task GetPlaylistVideos(string playlistId, List<IYouTubeVideo> playlistVideos)
        {
            var service = await GetYouTubeService();
            var nextPageToken = "";
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
                        string playlistItemId = playlistItem.Id;
                        string title = videoResponse.Items[0].Snippet.Title;
                        string videoId = videoResponse.Items[0].Id;
                        string url = string.Concat("https://www.youtube.com/watch?v=", videoId);
                        Thumbnail thumbnail = videoResponse.Items[0].Snippet.Thumbnails.Default__;
                        IYouTubeVideo video = new YouTubeVideo(playlistItemId, title, videoId, url, thumbnail.Url);
                        playlistVideos.Add(video);
                    }
                }
                nextPageToken = playlistItemsResponse.NextPageToken;
            }
        }

        public async Task RemoveSongFromPlaylistAsync(string playlistItemId)
        {
            var youtubeService = await GetYouTubeService();
            PlaylistItemsResource.DeleteRequest deleteRequest = youtubeService.PlaylistItems.Delete(playlistItemId);
            string response = await deleteRequest.ExecuteAsync();
        }
    }
}
