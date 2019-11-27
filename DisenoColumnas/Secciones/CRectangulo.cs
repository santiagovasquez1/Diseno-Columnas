using DisenoColumnas.Clases;
using DisenoColumnas.Secciones_Predefinidas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

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

    internal delegate void DRefuerzo_negativo(int Num_barras);

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

                Ash1 = (FactorDisipacion1 * S * bc * Material.FC / FY) * ((Area / Ach) - 1);  //C.21-2
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
            var P_As1 = new List<double>();     //'Peso total As1
            var P_As2 = new List<double>();     //'Peso total As2

            var Sep = new List<double>();
            int Indice_min;

            s_min = 7.5;

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

            if (Estribo == null)
            {
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
                        Separacion = Convert.ToSingle(Sep[Indice_min]),
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

                Cuanti_Vol(FD1, FD2, r, 4220);
            }
            else
            {
                double S1, S2, SH, SV, Sdef1, Sdef2;
                double PAs1, PAs2;
                float Ach = (B - 2 * r) * (H - 2 * r);

                #region Estribo  #3

                S1 = Estribo.NoRamasV1 * Form1.Proyecto_.AceroBarras[3] / (FD1 * B * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasV1 * FY * Form1.Proyecto_.AceroBarras[3] / (FD2 * (B - 2 * r) * Material.FC);
                SV = new double[] { S1 * 100, S2 * 100, s_max }.Min();

                S1 = Estribo.NoRamasH1 * Form1.Proyecto_.AceroBarras[3] / (FD1 * H * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasH1 * FY * Form1.Proyecto_.AceroBarras[3] / (FD2 * (H - 2 * r) * Material.FC);
                SH = new double[] { S1 * 100, S2 * 100, s_max }.Min();

                Sdef1 = Math.Round(Math.Min(SV, SH), 1);
                Estribo.NoEstribo = 3;
                Estribo.Separacion = (float)Sdef1;

                PAs1 = Peso_Estribo(Estribo, r);

                #endregion Estribo  #3

                #region Estribo  #4

                S1 = Estribo.NoRamasV1 * Form1.Proyecto_.AceroBarras[4] / (FD1 * B * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasV1 * FY * Form1.Proyecto_.AceroBarras[4] / (FD2 * (B - 2 * r) * Material.FC);
                SV = new double[] { S1 * 100, S2 * 100, s_max }.Min();

                S1 = Estribo.NoRamasH1 * Form1.Proyecto_.AceroBarras[4] / (FD1 * H * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasH1 * FY * Form1.Proyecto_.AceroBarras[4] / (FD2 * (H - 2 * r) * Material.FC);
                SH = new double[] { S1 * 100, S2 * 100, s_max }.Min();

                Sdef2 = Math.Round(Math.Min(SV, SH), 1);
                Estribo.NoEstribo = 4;
                Estribo.Separacion = (float)Sdef2;

                PAs2 = Peso_Estribo(Estribo, r);

                #endregion Estribo  #4

                if (PAs1 < PAs2)
                {
                    Estribo.NoEstribo = 3;
                    Estribo.Separacion = (float)Sdef1;
                }
                else
                {
                    Estribo.NoEstribo = 4;
                    Estribo.Separacion = (float)Sdef2;
                }
            }
        }

        public void Refuerzo_Base(double recub)
        {
            int Num_Barras = 0;
            int Barra_aux = 0;
            int Diametro1 = 0;
            int Diametro2 = 0;
            double As_min;
            double As_i;
            double p_error;
            int Cont_Aux1 = 0;
            int Cont_Aux2 = 0;
            double DeltaX, DeltaY;
            int CapasX = 0;
            int CapasY = 0;
            int X1 = 0; //Cantidad de barras para diametro1
            int X2 = 0; //Cantida de barras para diametro2
            bool Desc_X = true;

            Num_Barras = Estribo.NoRamasH1 * 2 + 2 * (Estribo.NoRamasV1 - 2);
            As_min = 0.01 * Area;
            As_i = As_min / Num_Barras;

            while (As_i > Form1.Proyecto_.AceroBarras[6])
            {
                Num_Barras += 2;
                As_i = (As_min / Num_Barras);
            }

            //Asociar As_i a un diametro de barra
            Barra_aux = FunctionsProject.Find_Barra(As_i);

            //Encontrar Combinatoria optima para el acero base mas aproximado al 1%
            p_error = Math.Abs(((Form1.Proyecto_.AceroBarras[Barra_aux] * Num_Barras) - As_min) / As_min) * 100;

            if (p_error >= 1.05)

            {
                if (Form1.Proyecto_.AceroBarras[Barra_aux] * Num_Barras > As_min)
                {
                    Diametro1 = Barra_aux;
                    if (Diametro1 == 4)
                    {
                        Diametro2 = 0;
                    }
                    else
                    {
                        Diametro2 = Barra_aux - 1;
                    }
                   
                }
                else
                {
                    Diametro1 = Barra_aux;
                    Diametro2 = Barra_aux + 1;
                }

                X2 = Convert.ToInt32((As_min - Form1.Proyecto_.AceroBarras[Diametro1] * Num_Barras) / (Form1.Proyecto_.AceroBarras[Diametro2] - Form1.Proyecto_.AceroBarras[Diametro1]));

                if (X2 % 2 != 0)
                {
                    X2 = FunctionsProject.Redondear_Decimales(X2, 4);
                }

                X1 = Num_Barras - X2;
            }
            else
            {
                Diametro1 = Barra_aux;
                Diametro2 = 0;
                X1 = Num_Barras;
                X2 = 0;
            }

            Cont_Aux1 = X1;
            Cont_Aux2 = X2;

            CapasX = Estribo.NoRamasV1;
            CapasY = (Num_Barras - 2 * (Estribo.NoRamasV1 - 2)) / 2;

            if (Num_Barras != Estribo.NoRamasH1 * 2 + 2 * (Estribo.NoRamasV1 - 2))
            {
                DeltaX = (B * 100 - 2 * recub) / (CapasX - 1);
                DeltaY = (H * 100 - 2 * recub) / (CapasY - 1);

                while (DeltaY <= 12)
                {
                    CapasX += 1;
                    CapasY -= 1;
                    DeltaY = (H * 100 - 2 * recub) / (CapasY - 1);
                }
            }

            int[] Aux_Refuerzos = new int[Num_Barras];
            int Aux_num_barras = Num_Barras;
            int i = 0;

            while (Aux_num_barras > 0)
            {
                if (Cont_Aux1 > 0)
                {
                    if (Cont_Aux1 > 0)
                    {
                        Aux_Refuerzos[i] = Diametro1;
                        Cont_Aux1 -= 1;
                        Aux_num_barras -= 1;
                    }

                    if (Cont_Aux1 > 0)
                    {
                        Aux_Refuerzos[i + (Num_Barras / 2) + CapasX - 2] = Diametro1;
                        Cont_Aux1 -= 1;
                        Aux_num_barras -= 1;
                    }

                    if (Cont_Aux1 > 0)
                    {
                        Aux_Refuerzos[Num_Barras - 1 - i - (Num_Barras / 2) - (CapasX - 2)] = Diametro1;
                        Cont_Aux1 -= 1;
                        Aux_num_barras -= 1;
                    }

                    if (Cont_Aux1 > 0)
                    {
                        Aux_Refuerzos[Num_Barras - 1 - i] = Diametro1;
                        Cont_Aux1 -= 1;
                        Aux_num_barras -= 1;
                    }

                    if (CapasX - 2 > 0)
                    {
                        if (Desc_X == true)
                        {
                            for (int j = 0; j < (CapasX - 2) * 2; j++)
                            {
                                if (Cont_Aux1 > 0)
                                {
                                    Aux_Refuerzos[CapasY + j] = Diametro1;
                                    Cont_Aux1 -= 1;
                                }
                                else
                                {
                                    Aux_Refuerzos[CapasY + j] = Diametro2;
                                    Cont_Aux2 -= 1;
                                }
                                Aux_num_barras -= 1;
                            }
                        }

                        Desc_X = false;
                    }
                }
                else
                {
                    if (Aux_Refuerzos[i] == 0)
                    {
                        Aux_Refuerzos[i] = Diametro2;
                        Cont_Aux2 -= 1;
                        Aux_num_barras -= 1;
                    }
                }
                i++;
            }

            Estribo.NoRamasH1 = CapasY;
            Estribo.NoRamasV1 = CapasX;
            Refuerzos = Main_Secciones.Set_Refuerzo_Seccion(Aux_Refuerzos, CapasX, CapasY, 0, 0, B * 100, H * 100, 0, 0);
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
                circulo.Set_puntos(10, r);

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

        public void Dibujo_Autocad(double Xi, double Yi,int Num_Despiece)
        {
            string LayerCuadro = "FC_BORDES";
            double[] Vertices = new double[] { Xi, Yi, Xi, Yi - H, Xi + B, Yi - H, Xi + B, Yi };
            double Dist1 = 0;
            double Dist2 = 0;
            double[] P_XYZ = { };
            string Layer_aux = "";
            string Nom_Seccion = "";
            string Escala = "1:15";
            short Flip_state = 0;

            var X_unicos = Refuerzos.Select(x => Math.Round(x.Coord[0], 2)).ToList().Distinct().ToList();
            var Y_unicos = Refuerzos.Select(x => Math.Round(x.Coord[1], 2)).ToList().Distinct().ToList();

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vertices, LayerCuadro, true);
            FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ: new double[] { Xi + B / 2, Yi - 0.04, 0 }, Layer: "FC_ESTRIBOS", Base: B - 0.02 * Form1.Proyecto_.R, Altura: H - 0.02 * Form1.Proyecto_.R, Xscale: 1, Yscale: 1, Zscale: 1, Rotation: 0);

            #region Dibujo de refuerzo en seccion

            foreach (CRefuerzo refi in Refuerzos)
            {
                refi.Dibujo_Ref_Autocad(Xi + B / 2, Yi - H / 2, X_unicos.Max(), X_unicos.Min(), Y_unicos.Max(), Y_unicos.Min());
            }

            #endregion Dibujo de refuerzo en seccion

            #region Adicion de ganchos seccion

            //Dibujo de Ganchos verticales
            Dist1 = Math.Abs(Y_unicos.Max() - Y_unicos.Min()) / 100;
            Dist2 = Math.Abs(X_unicos.Max() - X_unicos.Min()) / 100;
            Layer_aux = "FC_GANCHOS";

            for (int i = 1; i < X_unicos.Count - 1; i++)
            {
                P_XYZ = new double[] { Xi + (B / 2) + X_unicos[i] / 100, Yi - (H / 2) + Y_unicos.Max() / 100, 0 };
                FunctionsAutoCAD.FunctionsAutoCAD.B_Gancho(P_XYZ, Layer_aux, Dist1, 1, 1, 1, 270, Flip_state);
                Flip_state = Flip_state == 0 ? (Int16)1 : (Int16)0;
            }

            Flip_state = 1;
            for (int i = 1; i < Y_unicos.Count - 1; i++)
            {
                P_XYZ = new double[] { Xi + (B / 2) + X_unicos.Min() / 100, Yi - (H / 2) + Y_unicos[i] / 100, 0 };
                FunctionsAutoCAD.FunctionsAutoCAD.B_Gancho(P_XYZ, Layer_aux, Dist2, 1, 1, 1, 0, Flip_state);

                Flip_state = Flip_state == 0 ? (Int16)1 : (Int16)0;
            }

            #endregion Adicion de ganchos seccion

            #region Nombre_Seccion
            Nom_Seccion = "%%USeccion " + Num_Despiece;
            FunctionsAutoCAD.FunctionsAutoCAD.B_NombreSeccion(P_XYZ: new double[] { Xi + (B / 2), Yi - (H / 2) - 0.40, 0 }, Seccion: Nom_Seccion, Escala: Escala, Layer: "FC_R-200", Xscale: 15, Yscale: 15, Zscale: 15, Rotation: 0);
            #endregion
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