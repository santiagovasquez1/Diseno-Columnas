using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenoColumnas.Clases
{

    [Serializable]
    public class Estribo
    {


        public Estribo(int noEstribo)
        {
            
            NoEstribo = noEstribo;

        }
        public int NoEstribo { get; set; }
        public double Area { get; set; }
        public float Separacion { get; set; }

        public int NoRamasV1 { get; set; }
        public int NoRamasV2 { get; set; }
        public int NoRamasH1 { get; set; }
        public int NoRamasH2 { get; set; }


        public void CalcularArea()
        {
            Area = Form1.Proyecto_.AceroBarras[NoEstribo];
        }

    }
}
