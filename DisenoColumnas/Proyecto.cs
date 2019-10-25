using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisenoColumnas
{





    [Serializable]
    public enum GDE
    {
        DMO, DES
    }
    [Serializable]
    public class Proyecto
    {
        #region Diccionarios

        [NonSerialized]
        public Dictionary<int, float> Diametro_ref;
        public Dictionary<int, double> AceroBarras;
        public Dictionary<int, float> Ld_210;
        public Dictionary<int, float> Ld_280;
        public Dictionary<int, float> Ld_350;
        public Dictionary<int, float> Ld_420;
        public Dictionary<int, float> Ld_490;
        public Dictionary<int, float> Ld_560;
        
        #endregion

        public string Empresa { get; } = "efe Prima Ce";
        public string Ruta { get; set; } = "";

        public float e_Fundacion { get; set; }

        public float Nivel_Fundacion { get; set; }

        public List<Tuple<string, float>> Stories { get; set; }

        public List<MAT_CONCRETE> Lista_Materiales { get; set; }
        public List<Seccion> Lista_Secciones { get; set; }
        public List<Columna> Lista_Columnas { get; set; }

        public List<Viga> Lista_Vigas { get; set; }

        public Columna ColumnaSelect { get; set; }

        public float AlturaEdificio { get; set; }

        public GDE DMO_DES { get; set; }

        public float R { get; set; }
        public float FY { get; set; }

        public void AlturaEdificio_()
        {
            AlturaEdificio = Stories.Sum(x => x.Item2) + e_Fundacion;
        }
    }
}
