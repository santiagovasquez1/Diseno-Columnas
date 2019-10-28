using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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
        public string Piso { get; set; }
        public MAT_CONCRETE Material { get; set; }
        public float B { get; set; }
        public float H { get; set; }

        public float TF { get; set; }
        public float TW { get; set; }

        public TipodeSeccion Shape { get; set; }

        public double Area { get; set; }

        public List<Point> Vertices { get; set; } = new List<Point>();

        public List<CRefuerzo> Refuerzos { get; set; } = new List<CRefuerzo>();

        [NonSerialized]public List<GraphicsPath> Shapes_ref  = new List<GraphicsPath>();

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

        public void Add_Ref_graph(double EscalaX, double EscalaY, double EscalaR)
        {
            GraphicsPath path;
            double r = 0;
            double[] Centro;
            double xc, yc;
            CCirculo circulo;

            if (Shapes_ref != null)
            {
                Shapes_ref.Clear();
            }
            else
            {
                Shapes_ref = new List<GraphicsPath>();
            }


            foreach (CRefuerzo refuerzoi in Refuerzos)
            {
                path = new GraphicsPath();
                r = Form1.Proyecto_.Diametro_ref[Convert.ToInt32(refuerzoi.Diametro.Substring(1))] / 2;
                r = r * EscalaR;

                xc = refuerzoi.Coord[0] * EscalaX;
                yc = -refuerzoi.Coord[1] * EscalaY;
                Centro = new double[] { xc, yc };

                circulo = new CCirculo(r, Centro);
                circulo.Set_puntos(10);

                path.AddClosedCurve(circulo.Puntos.ToArray());
                Shapes_ref.Add(path);
            }
        }

        public object Clone()
        {
            Seccion temp = new Seccion(Name, B, H, TF, TW, Material, Shape, CoordenadasSeccion)
            {
                Refuerzos = Refuerzos,
                Vertices = Vertices,
                Area = Area
            };
            return temp;
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
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