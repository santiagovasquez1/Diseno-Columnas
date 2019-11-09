using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using DisenoColumnas.Clases;
using System.Windows.Forms;

namespace DisenoColumnas.Clases
{
    public interface ISeccion
    {
        float B { get; set; }
        MAT_CONCRETE Material { get; set; }
        TipodeSeccion Shape { get; set; }
        double Area { get; set; }
        float H { get; set; }
        Estribo Estribo { get; set; }
        string Name { get; set; }
        bool Editado { get; set; }
        List<GraphicsPath> Shapes_ref { get; set; }
        List<CRefuerzo> Refuerzos { get; set; }
        double Acero_Long { get; set; }
        void CalcularArea();
        void Cuanti_Vol(float FactorDisipacion1, float FactorDisipacion2, float r, float FY);
        void Calc_vol_inex(float r, float FY);
        void Add_Ref_graph(double EscalaX, double EscalaY, double EscalaR);
        GraphicsPath Add_Estribos(double EscalaX, double EscalaY, float rec);
        void Dibujo_Seccion(Graphics g, double EscalaX, double EscalaY, bool seleccion);
        void CalcNoDBarras();

    }
}
