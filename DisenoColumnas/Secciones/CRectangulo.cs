using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;

namespace DisenoColumnas.Secciones
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
    public class CRectangulo : ISeccion, ICloneable, IComparable
    {
        public string Name { get; set; }
        public MAT_CONCRETE Material { get; set; }
        public float B { get; set; }
        public float H { get; set; }
        [NonSerialized] private GraphicsPath pSeccion_path;
        public GraphicsPath Seccion_path { get { return pSeccion_path; } set { pSeccion_path = value; } }
        public TipodeSeccion Shape { get; set; }
        public double Area { get; set; }
        public double Acero_Long { get; set; }
        public Estribo Estribo { get; set; }
        public List<PointF> Vertices { get; set; } = new List<PointF>();
        public List<CRefuerzo> Refuerzos { get; set; } = new List<CRefuerzo>();
        [NonSerialized] private List<GraphicsPath> pShapes_ref = new List<GraphicsPath>();
        public List<float[]> CoordenadasSeccion { get; set; }
        public bool Editado { get; set; } = false;
        public List<GraphicsPath> Shapes_ref { get { return pShapes_ref; } set { pShapes_ref = value; } }

        public CRectangulo(string Nombre, float B_, float H_, MAT_CONCRETE Material_, TipodeSeccion Shape_, List<float[]> Coordenadas = null)
        {
            //Unidades en metros
            Name = Nombre;
            B = B_;
            H = H_;
            Material = Material_;
            Shape = Shape_;
            CoordenadasSeccion = Coordenadas;
            CalcularArea();
        }

        #region Metodos - Resultados

        public void CalcularArea()
        {
            Area = B * H;
        }

        public void Cuanti_Vol(float FactorDisipacion1, float FactorDisipacion2, float r, float FY)
        {
            double Ash1, Ash2, Ash;

            if (Estribo != null)
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

        public void Calc_vol_inex(float r, float FY, GDE gDE)
        {
            float FD1, FD2;
            if (gDE == GDE.DMO)
            {
                FD1 = 0.20f;
                FD2 = 0.06f;
            }
            else
            {
                FD1 = 0.30f;
                FD2 = 0.09f;
            }

            double s_max, s_min;
            double s_d;
            string[] Vector_decimales = { };
            int pasos;
            float delta = 0.50f;

            char Separador_decimal;
            var P_As1 = new List<double>();     //'Peso total As1
            var P_As2 = new List<double>();     //'Peso total As2

            var Sep = new List<double>();
            int Indice_min;

            s_min = 7.5;
            Separador_decimal = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

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

                Cuanti_Vol(FD1, FD2, r, FY);
                P_As1.Add(Peso_Estribo(Estribo, r));

                #endregion Estribo #3

                #region Estribo #4

                Estribo = new Estribo(4) //Estribo temporal
                {
                    Separacion = Convert.ToSingle(s_d)
                };

                Cuanti_Vol(FD1, FD2, r, FY);
                P_As2.Add(Peso_Estribo(Estribo, r));

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

                MAT_CONCRETE material = new MAT_CONCRETE
                {
                    FC = 4220,
                    Name = "FY4220"
                };

                circulo = new CCirculo("Refuerzo", r, Centro, material, TipodeSeccion.Circle, pCoord: null);
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

        public object Clone()
        {
            CRectangulo temp = new CRectangulo(Name, B, H, Material, Shape, CoordenadasSeccion)
            {
                Refuerzos = Refuerzos,
                Vertices = Vertices,
                Area = Area
            };
            return temp;
        }

        public override string ToString()
        {
            string Nombre_seccion;
            Nombre_seccion = $"C{B * 100}X{H * 100}{Material.Name}";
            return string.Format("{0}", Nombre_seccion);
        }

        public override bool Equals(object obj)
        {
            if (obj is CRectangulo)
            {
                CRectangulo temp = (CRectangulo)obj;

                if (temp.B == B & temp.H == H & Material == temp.Material || temp.H == B & temp.B == H & Material == temp.Material)
                    return true;
            }

            return false;
        }

        public int CompareTo(object obj)
        {
            if (obj is CRectangulo)
            {
                CRectangulo temp = (CRectangulo)obj;
                if (Area > temp.Area) return 1;
                if (Area < temp.Area) return -1;
            }
            return 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(CRectangulo s1, CRectangulo s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(CRectangulo s1, CRectangulo s2)
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

        public static bool operator <(CRectangulo s1, CRectangulo s2)
        {
            if (s1.CompareTo(s2) < 0)
                return true;
            else
                return false;
        }

        public static bool operator >(CRectangulo s1, CRectangulo s2)
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

        public void Dibujo_Seccion(Graphics g, double EscalaX, double EscalaY, bool seleccion)
        {
            double X, Y;
            SolidBrush br = new SolidBrush(Color.FromArgb(150, Color.Gray));
            Pen P1;
            Vertices = new List<PointF>();
            Seccion_path = new GraphicsPath();

            if (seleccion == false)
            {
                P1 = new Pen(Color.Black, 2.5f)
                {
                    Brush = Brushes.Gray,
                    Color = Color.Black,
                    DashStyle = DashStyle.Solid,
                    LineJoin = LineJoin.MiterClipped,
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Center
                };
            }
            else
            {
                P1 = new Pen(Color.Black, 3f)
                {
                    Brush = Brushes.DarkRed,
                    Color = Color.DarkRed,
                    DashStyle = DashStyle.Dash,
                    LineJoin = LineJoin.Round,
                    Alignment = PenAlignment.Center
                };
                br = new SolidBrush(Color.FromArgb(150, Color.Gray));
            }

            #region Vertices

            X = -(B * 100 / 2) * EscalaX;
            Y = -(H * 100 / 2) * EscalaY;
            Vertices.Add(new PointF((float)X, (float)Y));

            X = (B * 100 / 2) * EscalaX;
            Y = -(H * 100 / 2) * EscalaY;
            Vertices.Add(new PointF((float)X, (float)Y));

            X = (B * 100 / 2) * EscalaX;
            Y = (H * 100 / 2) * EscalaY;
            Vertices.Add(new PointF((float)X, (float)Y));

            X = -(B * 100 / 2) * EscalaX;
            Y = (H * 100 / 2) * EscalaY;
            Vertices.Add(new PointF((float)X, (float)Y));

            #endregion Vertices

            g.DrawPolygon(P1, Vertices.ToArray());
            g.FillPolygon(br, Vertices.ToArray());
            Seccion_path.AddPolygon(Vertices.ToArray());
        }

        public double Peso_Estribo(Estribo pEstribo, float recubrimiento)
        {
            double PAuxiliar = 0;
            double Long_Estibo = 0;
            double Long_GanchoV = 0;
            double Long_GanchoH = 0;
            int Numero_Estribos = 0;

            Long_Estibo = 2 * (B - 2 * recubrimiento) + 2 * (H - 2 * recubrimiento) + 2 * Form1.Proyecto_.G135[pEstribo.NoEstribo];
            Long_GanchoH = (B - 2 * recubrimiento) + 2 * Form1.Proyecto_.G180[pEstribo.NoEstribo];
            Long_GanchoV = (H - 2 * recubrimiento) + 2 * Form1.Proyecto_.G180[pEstribo.NoEstribo];

            Numero_Estribos = Convert.ToInt32(Math.Round((100) / pEstribo.Separacion, 0) + 1);

            PAuxiliar = (Long_Estibo + (pEstribo.NoRamasH1 - 2) * Long_GanchoH + (pEstribo.NoRamasV1 - 2) * Long_GanchoV) * pEstribo.Area * 7850 * Numero_Estribos;

            return PAuxiliar;
        }

        #endregion Propiedades y Metodos - Secciones Predefinidas
    }
}