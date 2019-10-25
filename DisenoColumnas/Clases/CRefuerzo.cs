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
        public double[] Coord { get; set; }
        public TipodeRefuerzo TipodeRefuerzo { get; set; }

        public CRefuerzo(int pid, string pdiametro, double[] pcoord, TipodeRefuerzo ptipo)
        {
            id = pid;
            Diametro = pdiametro;
            Coord = pcoord;
            TipodeRefuerzo = ptipo;
        }

        public override string ToString()
        {
            return string.Format("{0}", Diametro);
        }
    }
}