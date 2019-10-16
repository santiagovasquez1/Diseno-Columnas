using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Tuple<Seccion,string>> Seccions { get; set; } = new List<Tuple<Seccion, string>>();






    }







}

