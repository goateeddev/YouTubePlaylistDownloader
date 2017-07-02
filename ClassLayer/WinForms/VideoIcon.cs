using System.Drawing;
using System.Windows.Forms;

namespace ClassLayer.WinForms
{
    public class VideoIcon : PictureBox
    {
        public VideoIcon(object thumbnail, int y)
        {
            Load(thumbnail as string);
            Location = new Point(5, y);
            SizeMode = PictureBoxSizeMode.AutoSize; // new Size(120, 90);
        }
    }
}
