using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisenoColumnas.Clases
{
    interface IEventosSeccion
    {
        void MouseDown(object sender, MouseEventArgs e);
        void MouseUp(object sender, MouseEventArgs e);
        void MouseMove_NotDrawing(object sender, MouseEventArgs e);
        bool MouseIsOverPolygon(Point mouse_pt, out List<Point> hit_polygon);
        bool MouseIsOverRebar(Point mouse_pt, out List<Point> hit_polygon);
    }
}
