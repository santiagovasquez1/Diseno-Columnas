using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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

        public double Acero_Long { get; set; }

        public Estribo Estribo { get; set; }

        public List<Point> Vertices { get; set; } = new List<Point>();

        public List<CRefuerzo> Refuerzos { get; set; } = new List<CRefuerzo>();

        [NonSerialized] public List<GraphicsPath> Shapes_ref = new List<GraphicsPath>();

        public List<float[]> CoordenadasSeccion { get; set; }

        public bool Editado { get; set; } = false;

        public Seccion(string Nombre, float B_, float H_, float Tf, float Tw, MAT_CONCRETE Material_, TipodeSeccion Shape_, List<float[]> Coordenadas = null)
        {
            //Unidades en metros
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

        public void Cuanti_Vol(float FactorDisipacion1, float FactorDisipacion2, float r, float FY)
        {
            double Ash1, Ash2, Ash;
            if (Shape == TipodeSeccion.Rectangular)
            {
                //VERTICAL
                float Ach = (B - 2 * r) * (H - 2 * r);
                float bc = B - 2 * r;
                float S = Estribo.Separacion / 100;

                Ash1 = FactorDisipacion1 * S * bc * Material.FC / FY * (Area / Ach - 1);  //C.21-2
                Ash2 = FactorDisipacion2 * S * bc * Material.FC / FY;  //C.21-3

                Ash = Ash1 > Ash2 ? Ash1 : Ash2;
                if (S != 0 && Estribo.Area != 0)
                {
                    Estribo.NoRamasV1 = Convert.ToInt32(Math.Round(Ash / Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));
                }
                else
                {
                    Estribo.NoRamasV1 = 0;
                }

                //HORIZONTAL
                bc =H - 2 * r;

                Ash1 = (FactorDisipacion1 * S * bc * Material.FC / FY) * (Area / Ach - 1);  //C.21-2
                Ash2 = FactorDisipacion2 * S * bc *Material.FC / FY;  //C.21-3

                Ash = Ash1 > Ash2 ? Ash1 : Ash2;

                if (S != 0 && Estribo.Area != 0)
                {
                    Estribo.NoRamasH1 = Convert.ToInt32(Math.Round(Ash /Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));
                }
                else
                {
                    Estribo.NoRamasH1 = 0;
                }
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

        public GraphicsPath Add_Estribos(double EscalaX, double EscalaY, float rec)
        {
            GraphicsPath path = new GraphicsPath();
            List<PointF> Vertices = new List<PointF>();
            List<PointF> Vertices2 = new List<PointF>();
            float x = 0; float y = 0;
            float x1 = 0; float y1 = 0;

            if (Shape == TipodeSeccion.Rectangular)
            {
                x = Convert.ToSingle(((B / 2) - rec) * 100 * EscalaX);
                y = Convert.ToSingle(((H / 2) - rec) * 100 * EscalaY);

                x1 = Convert.ToSingle(x + (Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo]) * EscalaX);
                y1 = Convert.ToSingle(y + (Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo]) * EscalaY);

                Vertices.Add(new PointF(-x, -y));
                Vertices.Add(new PointF(x, -y));
                Vertices.Add(new PointF(x, y));
                Vertices.Add(new PointF(-x, y));

                Vertices2.Add(new PointF(-x1, -y1));
                Vertices2.Add(new PointF(x1, -y1));
                Vertices2.Add(new PointF(x1, y1));
                Vertices2.Add(new PointF(-x1, y1));

                path.AddPolygon(Vertices.ToArray());
                path.AddPolygon(Vertices2.ToArray());

                return path;
            }

            if (Shape == TipodeSeccion.Tee)
            {
                return path;
            }

            return path;
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

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is Seccion)
            {
                Seccion temp = (Seccion)obj;

                if (Name == temp.Name && Material == temp.Material && Shape == temp.Shape && Area == temp.Area && B == temp.B
                    && H == temp.H && TF == temp.TF && TW == temp.TW && temp.Shape != TipodeSeccion.Rectangular)
                    return true;

                //Necesario para cargar secciones predefinidas

                if (Shape == TipodeSeccion.Rectangular & B == 0.35f & H == 0.80f & Material.FC == 280)
                {
                }

                if (temp.Shape == TipodeSeccion.Rectangular & temp.B == B & temp.H == H & Material == temp.Material || temp.Shape == TipodeSeccion.Rectangular & temp.H == B & temp.B == H & Material == temp.Material)
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

        #region Propiedades y Metodos - Secciones Predefinidas

        public List<Tuple<int, int>> No_D_Barra { get; set; }

        public void CalcNoDBarras()
        {
            No_D_Barra = new List<Tuple<int, int>>();

            var results = from p in Refuerzos
                          group p.Diametro by p.Diametro into g
                          select new { Diametro = g.Key, Refuerzo = g.ToList() };

            foreach (var Refuer in results)
            {
                Tuple<int, int> TupleAux = new Tuple<int, int>(Refuer.Refuerzo.Count, Convert.ToInt32(Refuer.Diametro.Replace("#", "")));
                No_D_Barra.Add(TupleAux);
            }
        }

        #endregion Propiedades y Metodos - Secciones Predefinidas
    }
}