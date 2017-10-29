using Unity;
using YouTubePlaylistDownloader.DTO.Interfaces;

namespace YouTubePlaylistDownloader.DTO.DependencyInjection
{
    public static class DependencyManager
    {
        private static IUnityContainer _unityContainer;
        
        #region Public Methods

        public static T Resolve<T>()
        {
            if (_unityContainer == null)
            {
                SetupContainer();
            }
            return _unityContainer.Resolve<T>();
        }
        
        #endregion Public Methods

        #region Private Methods

        private static void SetupContainer()
        {
            _unityContainer = new UnityContainer();
            RegisterTypes(_unityContainer);
        }

        private static void RegisterTypes(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IYouTubeVideo, YouTubeVideo>();
            unityContainer.RegisterType<IYouTubePlaylist, YouTubePlaylist>();
            unityContainer.RegisterType<IYouTubePlaylists, YouTubePlaylists>();
            unityContainer.RegisterType<IYouTubeVideos, YouTubeVideos>();
        }

        #endregion Private Methods
    }
}
