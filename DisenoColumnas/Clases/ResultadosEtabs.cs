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

        public double[] AsTopMediumButton { get; set; } = { };

        //Propiedad Adicional

        public float[] As_asignado { get; set; } = { };

        public float[] Porct_Refuerzo { get; set; } = { };

        #region Fuerzas en Columnas

        /// <summary><c></c> Stories para resultados.
        ///
        /// </summary>
        public List<string> Story_Result { get; set; } = new List<string>();

        /// <summary><c></c> Localización de cada estacion por cada tipo de Fuerza.
        ///
        /// </summary>
        public List<float> Loc { get; set; } = new List<float>();

        /// <summary><c></c> Tipo de Carga: SU01, SU02, SU03, etc.
        ///
        /// </summary>
        public List<string> Load { get; set; } = new List<string>();

        /// <summary><c></c> Carga Axial: Compresión (+)   Tracción (-).
        ///
        /// </summary>
        public List<float> P { get; set; } = new List<float>();

        /// <summary><c></c> Cortante en dirección X
        ///
        /// </summary>
        public List<float> V2 { get; set; } = new List<float>();

        /// <summary><c></c> Cortante en dirección Y
        ///
        /// </summary>
        public List<float> V3 { get; set; } = new List<float>();

        /// <summary><c></c> Momento alrededor del eje Z
        ///
        /// </summary>
        public List<float> T { get; set; } = new List<float>();

        /// <summary><c></c> Momento alrededor del eje X
        ///
        /// </summary>
        public List<float> M2 { get; set; } = new List<float>();

        /// <summary><c></c> Momento alrededor del eje Y
        ///
        /// </summary>

        public List<float> M3 { get; set; } = new List<float>();

        #endregion Fuerzas en Columnas

        public void AsignarAsTopMediumButton()
        {

            As_asignado = new float[] { 0, 0, 0 };
            Porct_Refuerzo = new float[] { 0, 0, 0 };
            double Top = -999999; double Button = -99999; double medium = -999999;
            int Cant_Estaciones = As.Count;
            int Div_Esta =  Cant_Estaciones / 3;
            int AsBottom_I = Div_Esta;
            int AsMedium_I = Div_Esta;

            try
            {
             
                for (int i = 0; i < AsBottom_I; i++)
                {
                    if (Button < As[i])
                    {
                        Button = As[i];
                    }
                }
                for (int i = AsMedium_I; i < AsMedium_I+ AsBottom_I; i++)
                {
                    if (medium < As[i])
                    {
                        medium = As[i];
                    }
                }

                for (int i = AsMedium_I + AsBottom_I; i < As.Count; i++)
                {
                    if (Top < As[i])
                    {
                        Top = As[i];
                    }
                }
            }
            catch
            {
             
            }




            double[] AsTMB = new double[] { Top, medium, Button };
            AsTopMediumButton = AsTMB;
        }
    }
}