using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public enum TipodeRefuerzo
    {
        longitudinal,
        Gancho,
        Estribo
    }

    [Serializable]
    internal class CRefuerzo : IEvenetosSeccion
    {
        public string Diametro { get; set; }
        public Point Coord { get; set; }
        public TipodeRefuerzo TipodeRefuerzo { get; set; }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool MouseIsOverPolygon(Point mouse_pt, out List<Point> hit_polygon)
        {
            throw new NotImplementedException();
        }

        public bool MouseIsOverRebar(Point mouse_pt, out List<Point> hit_polygon)
        {
            throw new NotImplementedException();
        }

        public void MouseMove_NotDrawing(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}