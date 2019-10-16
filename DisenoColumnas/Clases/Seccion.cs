using System;
using System.Collections.Generic;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public enum TipodeSeccion
    {
        None,
        Rectangular,
        Circle,
        Tee,
        L
    }

    [Serializable]
    public class Seccion
    {
        public Seccion(string Nombre, float B_, float H_, float Tf, float Tw, MAT_CONCRETE Material_, TipodeSeccion Shape_, List<float[]> Coordenadas = null)
        {
            Name = Nombre;
            B = B_;
            H = H_;
            Material = Material_;
            Shape = Shape_;
            TW = Tw;
            TF = Tf;
            CoordenadasSeccion = Coordenadas;
            CalcularArea();
        }

        public string Name { get; set; }
        public MAT_CONCRETE Material { get; set; }
        public float B { get; set; }
        public float H { get; set; }

        public float TF { get; set; }
        public float TW { get; set; }

        public TipodeSeccion Shape { get; set; }

        public double Area { get; set; }

        private List<float[]> CoordenadasSeccion { get; set; }

        private void CalcularArea()
        {
            if (TW == 0 && TF == 0 && H != 0)
            {
                Area = B * H;
            }
            else if (TW == 0 && TF == 0 && H == 0 && B != 0)
            {
                Area = Math.PI * Math.Pow(B, 2) / 4;
            }
            else if (TW != 0 | TF != 0)
            {
                Area = (H * TW) + ((B - TW) * TF);
            }
            else if (CoordenadasSeccion != null)
            {
                double SumA1 = 0; double SumA2 = 0;

                for (int i = 0; i < CoordenadasSeccion.Count; i++)
                {
                    try
                    {
                        SumA1 = SumA1 + CoordenadasSeccion[i][0] * CoordenadasSeccion[i + 1][1];
                        SumA2 = SumA2 + CoordenadasSeccion[i][1] * CoordenadasSeccion[i + 1][0];
                    }
                    catch
                    {
                        SumA1 = SumA1 + CoordenadasSeccion[i][0] * CoordenadasSeccion[0][1];
                        SumA2 = SumA2 + CoordenadasSeccion[i][1] * CoordenadasSeccion[0][0];
                    }
                }

                ///CORREGIR B Y H PARA T CON COORDENADAS
                Area = Math.Abs(SumA1 - SumA2) / 2;
                double DistMayor = -99999;
                for (int i = 0; i < CoordenadasSeccion.Count; i++)
                {
                    try
                    {
                        double Distancia = Math.Sqrt(Math.Pow(CoordenadasSeccion[i][0] - CoordenadasSeccion[i + 1][0], 2) + Math.Pow(CoordenadasSeccion[i][1] - CoordenadasSeccion[i + 1][1], 2));
                        if (Distancia > DistMayor)
                        {
                            DistMayor = Distancia;
                        }
                    }
                    catch
                    { }
                }

                B = (float)DistMayor;
            }
        }
    }
}