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