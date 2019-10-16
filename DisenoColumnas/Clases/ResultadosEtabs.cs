using System;
using System.Collections.Generic;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public class ResultadosETABS
    {
        public List<float> Estacion { get; set; } = new List<float>();
        public List<double> Asmin { get; set; } = new List<double>();
        public List<double> As { get; set; } = new List<double>();
    }
}