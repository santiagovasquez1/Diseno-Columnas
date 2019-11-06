﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

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

            if (Estribo != null)
            {
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
                    bc = H - 2 * r;

                    Ash1 = (FactorDisipacion1 * S * bc * Material.FC / FY) * (Area / Ach - 1);  //C.21-2
                    Ash2 = FactorDisipacion2 * S * bc * Material.FC / FY;  //C.21-3

                    Ash = Ash1 > Ash2 ? Ash1 : Ash2;

                    if (S != 0 && Estribo.Area != 0)
                    {
                        Estribo.NoRamasH1 = Convert.ToInt32(Math.Round(Ash / Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));
                    }
                    else
                    {
                        Estribo.NoRamasH1 = 0;
                    }
                }
            }
        }

        public void Calc_vol_inex(float FactorDisipacion1, float FactorDisipacion2, float r, float FY)
        {
            double s_max, s_min;
            double s_d;
            string[] Vector_decimales = { };
            int pasos;
            float delta = 0.50f;

            double Ast1, Ast2, G_As1, G_As2, LG_As1, LG_As2;
            char Separador_decimal;

            var Num_Ramas_V = new List<int>();    //Numero de ramas en altura del muro para ambos casos de ast

            var GT_As1 = new List<double>();   //Longitud total de los gancho para As1, bajo cada una de las variaciones de la separacion
            var GT_As2 = new List<double>();   //Longitud total de los gancho para As2, bajo cada una de las variaciones de la separacion

            var P_As1 = new List<double>();     //'Peso total As1
            var P_As2 = new List<double>();     //'Peso total As1

            var Sep = new List<double>();
            int Indice_min;

            Ast1 = 0.71; //'Estribo #3
            Ast2 = 1.29; //'Estribo #4

            s_min = 7.5;
            Separador_decimal = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            G_As1 = B * 100 - 2 * 2 + 2 * 17; //'Longitud del gancho transversal
            G_As2 = B * 100 - 2 * 2 + 2 * 20.5; //'Longitud del gancho transversal

            LG_As1 = H * 100 - 2 * r * 100 + 2 * 17; //'Longitud del gancho longitudinal
            LG_As2 = H * 100 - 2 * r * 100 + 2 * 20.5; //'Longitud del gancho longitudinal

            if (Form1.Proyecto_.DMO_DES == GDE.DMO)
            {
                s_max = B / 3 < H / 3 ? B * 100 / 3 : H * 100 / 3;
            }
            else
            {
                s_max = B / 4 < H / 4 ? B * 100 / 4 : H * 100 / 4;
            }

            s_max = Math.Round(s_max, 2);

            pasos = Convert.ToInt32((s_max - s_min) / delta);
            s_d = s_min;

            for (int i = 0; i < pasos; i++)
            {
                #region Estribo #3

                Estribo = new Estribo(3) //Estribo temporal
                {
                    Separacion = Convert.ToSingle(s_d)
                };

                Num_Ramas_V.Add(Convert.ToInt32(Math.Round((100) / s_d, 0) + 1));
                Cuanti_Vol(FactorDisipacion1, FactorDisipacion2, r, FY);

                GT_As1.Add(Num_Ramas_V.Last() * (G_As1 * Estribo.NoRamasH1 + LG_As1 * Estribo.NoRamasV1));
                P_As1.Add(GT_As1.Last() * Ast1 * 7850 / Math.Pow(100, 3));

                #endregion Estribo #3

                #region Estribo #4

                Estribo = new Estribo(4) //Estribo temporal
                {
                    Separacion = Convert.ToSingle(s_d)
                };

                Cuanti_Vol(FactorDisipacion1, FactorDisipacion2, r, FY);
                GT_As2.Add(Num_Ramas_V.Last() * (G_As2 * Estribo.NoRamasH1 + LG_As2 * Estribo.NoRamasV1));
                P_As2.Add(GT_As2.Last() * Ast2 * 7850 / Math.Pow(100, 3));

                Sep.Add(s_d);
                s_d += delta;

                #endregion Estribo #4
            }

            if (P_As1.Min() < P_As2.Min())
            {
                Indice_min = P_As1.FindIndex(x => x == P_As1.Min());
                Estribo = new Estribo(3)
                {
                    Separacion = Convert.ToSingle(Sep[Indice_min])
                };

            }
            else
            {
                Indice_min = P_As2.FindIndex(x => x == P_As2.Min());
                Estribo = new Estribo(4)
                {
                    Separacion = Convert.ToSingle(Sep[Indice_min])
                };
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