using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public class Alzado
    {

        public Alzado(int ID_,int NoPisos)
        {
            ID = ID_;
            Colum_Alzado = new List<string>();
            for(int i = 0; i < NoPisos; i++)
            {
                Colum_Alzado.Add("");
            }


        }


        public int ID { get; set; }
        public List<string> Colum_Alzado { get; set; }



    }
}
