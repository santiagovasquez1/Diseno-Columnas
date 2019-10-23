using System;
using System.Collections.Generic;
using System.Drawing;

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
    public class Seccion : ICloneable, IComparable
    {
        public string Name { get; set; }
        public MAT_CONCRETE Material { get; set; }
        public float B { get; set; }
        public float H { get; set; }

        public float TF { get; set; }
        public float TW { get; set; }

        public TipodeSeccion Shape { get; set; }

        public double Area { get; set; }

        public List<Point> Vertices { get; set; } = new List<Point>();

        public List<CRefuerzo> Refuerzos { get; set; } = new List<CRefuerzo>();

        private List<float[]> CoordenadasSeccion { get; set; }

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

        #region Metodos - Resultados

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

        public object Clone()
        {
            Seccion temp = new Seccion(Name, B, H, TF, TW, Material, Shape, CoordenadasSeccion);
            return temp;
        }

        public override bool Equals(object obj)
        {
            if (obj is Seccion)
            {
                Seccion temp = (Seccion)obj;

                if (Name == temp.Name && Material == temp.Material && Shape == temp.Shape && Area == temp.Area && B == temp.B
                    && H == temp.H && TF == temp.TF && TW == temp.TW)
                    return true;
            }

            return false;
        }

        public int CompareTo(object obj)
        {
            if (obj is Seccion)
            {
                Seccion temp = (Seccion)obj;
                if (Area > temp.Area) return 1;
                if (Area < temp.Area) return -1;
            }
            return 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Seccion s1, Seccion s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(Seccion s1, Seccion s2)
        {
            try
            {
                return !s1.Equals(s2);
            }
            catch (Exception)
            {
                return false;
                //throw;
            }

        }

        public static bool operator <(Seccion s1, Seccion s2)
        {
            if (s1.CompareTo(s2) < 0)
                return true;
            else
                return false;
        }

        public static bool operator >(Seccion s1, Seccion s2)
        {
            if (s1.CompareTo(s2) > 0)
                return true;
            else
                return false;
        }

        #endregion Metodos - Resultados
    }
}