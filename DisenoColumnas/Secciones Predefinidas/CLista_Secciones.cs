using DisenoColumnas.Clases;
using DisenoColumnas.Secciones;
using System;
using System.Collections.Generic;

namespace DisenoColumnas.Secciones_Predefinidas

{
    [Serializable]
    public class CLista_Secciones
    {
        public List<ISeccion> Secciones_DMO = new List<ISeccion>();
        public List<ISeccion> Secciones_DES = new List<ISeccion>();
    }
}