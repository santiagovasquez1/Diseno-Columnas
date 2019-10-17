using System.Windows.Forms;

namespace DisenoColumnas.Controles
{
    public class CDoubleBufferedPictureBox : PictureBox
    {
        public CDoubleBufferedPictureBox()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.ResizeRedraw |
              ControlStyles.ContainerControl |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}