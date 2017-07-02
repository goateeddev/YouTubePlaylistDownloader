using System.Drawing;
using System.Windows.Forms;

namespace ClassLayer.WinForms
{
    public class VideoField : Panel
    {
        public VideoField(VideoIcon thumbnail, VideoLabel title, VideoCheck checkbox, string text, int y)
        {
            Size = new Size(610, 102);
            Location = new Point(5, y);
            Text = text;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(thumbnail);
            Controls.Add(title);
            Controls.Add(checkbox);
            BackColor = Color.WhiteSmoke;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int thickness = 1;
            int halfThickness = thickness / 2;
            using (Pen p = new Pen(Color.Black, thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(halfThickness, halfThickness,
                                                            ClientSize.Width - thickness,
                                                            ClientSize.Height - thickness));
            }
        }
    }
}
