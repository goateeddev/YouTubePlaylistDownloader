using System.Drawing;
using System.Windows.Forms;

namespace ClassLayer.WinForms
{
    public class VideoLabel : Label
    {
        public VideoLabel(string title, int y)
        {
            Text = title;
            Font = new Font(Font.Name, 9.0F, FontStyle.Bold);
            MaximumSize = new Size(350, 0);
            AutoSize = true;
            if (title.Length < 48) y += 5;
            Location = new Point(140, y);
        }
    }
}
