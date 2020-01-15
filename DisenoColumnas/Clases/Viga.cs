using DisenoColumnas.Secciones;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public class Viga
    {
        public Viga(string Nombre)
        {
            Name = Nombre;
        }

        public string Name { get; set; }
        public string[] Points { get; set; }

        public double[] CoordXY1 { get; set; } = new double[2];   /// Cordenadas en Planta

        public double[] CoordXY2 { get; set; } = new double[2];   /// Cordenadas en Planta

        public void Paint_(Graphics graphics, float HeightForm, float WidthForm, float SX, float SY, float WX1, float HY1, float XI, float YI)
        {
            float X_Colum1, X_Colum2, Y_Colum1, Y_Colum2;

            if (CoordXY1[0] < 0)
            {
                X_Colum1 = WX1 * SX - Math.Abs((float)CoordXY1[0]) * SX;
            }
            else
            {
                X_Colum1 = WX1 * SX + Math.Abs((float)CoordXY1[0]) * SX;
            }

            if (CoordXY1[1] < 0)
            {
                Y_Colum1 = -HY1 * SY + Math.Abs((float)CoordXY1[1]) * SY + HeightForm;
            }
            else
            {
                Y_Colum1 = -HY1 * SY - Math.Abs((float)CoordXY1[1]) * SY + HeightForm;
            }

            if (CoordXY2[0] < 0)
            {
                X_Colum2 = WX1 * SX - Math.Abs((float)CoordXY2[0]) * SX;
            }
            else
            {
                X_Colum2 = WX1 * SX + Math.Abs((float)CoordXY2[0]) * SX;
            }

            if (CoordXY2[1] < 0)
            {
                Y_Colum2 = -HY1 * SY + Math.Abs((float)CoordXY2[1]) * SY + HeightForm;
            }
            else
            {
                Y_Colum2 = -HY1 * SY - Math.Abs((float)CoordXY2[1]) * SY + HeightForm;
            }

            X_Colum1 += XI;
            Y_Colum1 += YI;

            X_Colum2 += XI;
            Y_Colum2 += YI;

     
            Pen pen = new Pen(Color.FromArgb(108, 121, 180));

            graphics.DrawLine(pen, X_Colum1, Y_Colum1, X_Colum2, Y_Colum2);
        }

        public List<Tuple<CRectangulo, string>> Seccions { get; set; } = new List<Tuple<CRectangulo, string>>();
    }
}