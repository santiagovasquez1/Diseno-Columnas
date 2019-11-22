using System;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public class Estribo
    {
        public Estribo(int noEstribo)
        {
            NoEstribo = noEstribo;
            CalcularArea();
        }

        public int NoEstribo { get; set; }
        public double Area { get; set; }
        public float Separacion { get; set; }

        public int NoRamasV1 { get; set; }
        public int NoRamasV2 { get; set; }
        public int NoRamasH1 { get; set; }
        public int NoRamasH2 { get; set; }


        public float S_NC { get; set; }

        public void CalcularArea()
        {
            Area = Form1.Proyecto_.AceroBarras[NoEstribo];
        }

        public override string ToString()
        {
            string Texto = "";
            Texto = $"E#{NoEstribo} a {Separacion}cm";
            return string.Format("{0}", Texto);
        }
    }
}