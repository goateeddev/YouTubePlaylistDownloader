using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubePlaylistDownloader.Core.Handlers
{
    public class ProgressBarEventArgs : EventArgs
    {
        public double ProgressPercentage { get; private set; }

        public ProgressBarEventArgs(double progressPercentage)
        {
            ProgressPercentage = progressPercentage;
        }
    }
}
