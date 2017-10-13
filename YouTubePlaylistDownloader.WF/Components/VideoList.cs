using System.Drawing;
using System.Windows.Forms;

namespace YouTubePlaylistDownloader.WF.Components
{
    public class VideoList : Panel
    {
        public VideoList()
        {
            Location = new Point(32, 68);
            Size = new Size(640, 410);
            AutoScroll = true;
            BackColor = Color.White;
        }

        public VideoList(VideoList vl)
        {
            Location = vl.Location;
            Size = vl.Size;
            AutoScroll = vl.AutoScroll;
            BackColor = vl.BackColor;
        }
    }
}
