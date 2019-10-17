using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenoColumnas.Clases
{

    [Serializable]
    public class ResultadosETABS
    {

        public List<float> Estacion { get; set; } = new List<float>();
        public List<double> Asmin { get; set; } = new List<double>();
        public List<double> As { get; set; } = new List<double>();

        public double[] AsTopMediumButton { get; set; } = { };



        public void AsignarAsTopMediumButton()
        {
      
            double Top =-999999; double Button = -99999; double medium =-999999;
            for(int i=0; i < As.Count-6; i++)
            {
                if(Top< As[i])
                {
                    Top = As[i];
                }
                
            }
            for (int i = As.Count - 6; i < As.Count - 4; i++)
            {
                if (medium < As[i])
                {
                    medium = As[i];
                }

            }

            for (int i = As.Count - 4; i < As.Count; i++)
            {
                if (Button < As[i])
                {
                    Button = As[i];
                }

            }
            double[] AsTMB = new double[] { Top, medium, Button };
            AsTopMediumButton = AsTMB;

        }






    }

}
