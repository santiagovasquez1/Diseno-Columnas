using System;

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
    public class CRefuerzo
    {
        public int id { get; set; }
        public string Diametro { get; set; }
        public double As_Long { get; set; }
        public double[] Coord { get; set; }
        public TipodeRefuerzo TipodeRefuerzo { get; set; }

        public CRefuerzo(int pid, string pdiametro, double[] pcoord, TipodeRefuerzo ptipo)
        {
            int d1;
            id = pid;
            Diametro = pdiametro;
            Coord = pcoord;
            TipodeRefuerzo = ptipo;
            d1 = Convert.ToInt32(Diametro.Substring(1));
            As_Long = Form1.Proyecto_.AceroBarras[d1] * Math.Pow(100, 2);
        }

        public void Dibujo_Ref_Autocad(double Xi, double Yi, double Xmax, double Xmin, double Ymax, double Ymin)
        {
            double[] P_XYZ = { };
            double[] T_XYZ = { };
            string Layer = "FC_REFUERZO 2";

            P_XYZ = new double[] { Xi + Coord[0] / 100, Yi + Coord[1] / 100, 0 };
            FunctionsAutoCAD.FunctionsAutoCAD.Add_ref(P_XYZ, Layer, 1, 1, 1, 1, 0);

            if (Math.Round(Coord[1], 2) == Ymax) 
            {
                T_XYZ = new double[] { Xi + (Coord[0] / 100) - 0.007, Yi + (Coord[1] / 100) + 0.05, 0 };
            }

            if (Math.Round(Coord[1], 2) == Ymin)
            {
                T_XYZ = new double[] { Xi + (Coord[0] / 100) - 0.007, Yi + (Coord[1] / 100) - 0.03, 0 };
            }

            if(Math.Round(Coord[1], 2) > Ymin & Math.Round(Coord[1], 2) < Ymax)
            {
                if(Math.Round(Coord[0], 2) == Xmin)
                {
                    T_XYZ = new double[] { Xi + (Coord[0] / 100) - 0.05, Yi + (Coord[1] / 100) - 0.007, 0 };
                }
                if (Math.Round(Coord[0], 2) == Xmax)
                {
                    T_XYZ = new double[] { Xi + (Coord[0] / 100) + 0.03, Yi + (Coord[1] / 100) - 0.007, 0 };
                }
            }

            FunctionsAutoCAD.FunctionsAutoCAD.AddText("1", T_XYZ, 0.075, 0.0225, "FC_R-100", "FC_TEXT", 0);
        }

        public static double operator +(CRefuerzo r1, CRefuerzo r2)
        {
            double Suma_as;
            Suma_as = r1.As_Long + r2.As_Long;
            return Suma_as;
        }

        public override string ToString()
        {
            return string.Format("{0}", Diametro);
        }
    }
}