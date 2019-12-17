using DisenoColumnas.Clases;
using DisenoColumnas.Interfaz_Seccion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DisenoColumnas.Secciones
{
    [Serializable]
    public class CCirculo : ISeccion, IComparable
    {
        public string Name { get; set; }
        public MAT_CONCRETE Material { get; set; }
        public double radio { get; set; }
        public double[] Centro { get; set; } = { };
        public List<PointF> Puntos { get; set; } = new List<PointF>();
        public TipodeSeccion Shape { get; set; }
        [NonSerialized] private GraphicsPath pSeccion_path;
        public GraphicsPath Seccion_path { get { return pSeccion_path; } set { pSeccion_path = value; } }
        public double Area { get; set; }
        public double Acero_Long { get; set; }
        public Estribo Estribo { get; set; }
        public List<CRefuerzo> Refuerzos { get; set; } = new List<CRefuerzo>();
        [NonSerialized] private List<GraphicsPath> pShapes_ref = new List<GraphicsPath>();
        public List<float[]> CoordenadasSeccion { get; set; }
        public bool Editado { get; set; } = false;
        public List<Tuple<int, int>> No_D_Barra { get; set; }
        public float B { get { return 2 * (float)radio; } set { B = value; } }
        public float H { get { return 2 * (float)radio; } set { H = value; } }
        public List<GraphicsPath> Shapes_ref { get { return pShapes_ref; } set { pShapes_ref = value; } }

        #region Propiedades y Metodos para verificación de Vc

        public List<float[]> PM2M3V2V3 { get; set; }
        public List<float> Vcx { get; set; }
        public List<float> Vcy { get; set; }
        public List<float> Vsx { get; set; }
        public List<float> Vsy { get; set; }

        #endregion Propiedades y Metodos para verificación de Vc

        public CCirculo(string Nombre, double pradio, double[] pCentro, MAT_CONCRETE Material_, TipodeSeccion Shape_, List<float[]> pCoord)
        {
            Name = Nombre;
            Material = Material_;
            Shape = Shape_;
            radio = pradio;
            Centro = pCentro;
            CoordenadasSeccion = pCoord;
            CalcularArea();
        }

        public void Set_puntos(int numero_puntos, double pradio)
        {
            double delta_angulo = 2 * Math.PI / numero_puntos;
            double angulo = 0;

            PointF pi = new PointF();
            double xc = Centro[0];
            double yc = Centro[1];

            Puntos = new List<PointF>();

            for (int i = 0; i < numero_puntos; i++)
            {
                pi.X = Convert.ToSingle(xc + Math.Cos(angulo) * pradio);
                pi.Y = Convert.ToSingle(yc + Math.Sin(angulo) * pradio);
                Puntos.Add(pi);
                angulo += delta_angulo;
            }
        }

        public void Cuanti_Vol(float FactorDisipacion1, float FactorDisipacion2, float r, float FY = 4220)
        {
            double Ash;
            float S = Estribo.Separacion / 100;

            Ash = FactorDisipacion1 * (Material.FC / FY) * (2 * (radio - r) * S * 0.25);
            Estribo.NoRamasV1 = 1;
        }

        public void Calc_vol_inex(float r, float FY, GDE gDE)
        {
            float FD1, FD2;
            if (gDE == GDE.DMO)
            {
                FD1 = 0.08f;
                FD2 = 0.08f;
            }
            else
            {
                FD1 = 0.12f;
                FD2 = 0.12f;
            }

            int pasos;
            int Indice_min;
            double s_max, s_min, s_d;
            double Ast1, Ast2, G_As1, G_As2;
            float delta = 0.50f;

            var Num_Ramas_V = new List<int>();    //Numero de ramas en altura del muro para ambos casos de ast
            var GT_As1 = new List<double>();   //Longitud total de los gancho para As1, bajo cada una de las variaciones de la separacion
            var GT_As2 = new List<double>();   //Longitud total de los gancho para As1, bajo cada una de las variaciones de la separacion

            var P_As1 = new List<double>();     //'Peso total As1
            var P_As2 = new List<double>();     //'Peso total As2

            var Sep = new List<double>();

            Ast1 = 0.71; //Estribo #3
            Ast2 = 1.29; //Estribo #4

            s_min = 7.5;
            s_max = gDE == GDE.DMO ? 2 * radio * 100 / 3 : 2 * radio * 100 / 4;

            G_As1 = 2 * Math.PI * 2 * (radio - r) * 100 + 2 * 14; //Longitud de gancho a 180 de #3
            G_As2 = 2 * Math.PI * 2 * (radio - r) * 100 + 2 * 16.7; //Longitud de gancho a 180 de #3

            pasos = Convert.ToInt32((s_max - s_min) / delta);
            s_d = s_min;

            for (int i = 0; i < pasos; i++)
            {
                #region Estribo #3

                Estribo = new Estribo(3) //Estribo temporal
                {
                    Separacion = Convert.ToSingle(s_d)
                };

                Num_Ramas_V.Add(Convert.ToInt32(100 / s_d) + 1);
                Cuanti_Vol(FD1, FD2, r, FY);

                GT_As1.Add(Num_Ramas_V.Last() * (G_As1 * Estribo.NoRamasV1));
                P_As1.Add(GT_As1.Last() * Ast1 * 7850 / Math.Pow(100, 3));

                #endregion Estribo #3

                #region Estribo #4

                Estribo = new Estribo(4) //Estribo temporal
                {
                    Separacion = Convert.ToSingle(s_d)
                };
                Cuanti_Vol(FD1, FD2, r, FY);

                GT_As2.Add(Num_Ramas_V.Last() * (G_As2 * Estribo.NoRamasV1));
                P_As2.Add(GT_As2.Last() * Ast2 * 7850 / Math.Pow(100, 3));

                #endregion Estribo #4

                Sep.Add(s_d);
                s_d += delta;
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
            int X1 = 0; //Cantidad de barras para diametro1
            int X2 = 0; //Cantida de barras para diametro2
            Refuerzos.Clear();
            Num_Barras = 4;
            As_min = 0.01 * Area;
            As_i = As_min / Num_Barras;

            while (As_i > FunctionsProject.Find_As(6))
            {
                Num_Barras += 2;
                As_i = (As_min / Num_Barras);
            }

            //Asociar As_i a un diametro de barra
            Barra_aux = FunctionsProject.Find_Barra(As_i);

            //Encontrar Combinatoria optima para el acero base mas aproximado al 1%
            p_error = Math.Abs(((FunctionsProject.Find_As(Barra_aux) * Num_Barras) - As_min) / As_min) * 100;

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

                if (Diametro2 > 0)
                {
                    X2 = Convert.ToInt32((As_min - Form1.Proyecto_.AceroBarras[Diametro1] * Num_Barras) / (Form1.Proyecto_.AceroBarras[Diametro2] - Form1.Proyecto_.AceroBarras[Diametro1]));
                }
                else
                {
                    X2 = 0;
                }

                if (X2 % 2 != 0)
                {
                    X2 = FunctionsProject.Redondear_Entero(X2, 4, true);
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

            int[] Aux_Refuerzos = new int[Num_Barras];
            int Aux_num_barras = Num_Barras;
            int i = 0;

            Cont_Aux1 = X1;
            Cont_Aux2 = X2;

            while (Aux_num_barras > 0)
            {
                if (X2 > 0)
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
                            Aux_Refuerzos[i + (Num_Barras / 2) - 1] = Diametro1;
                            Cont_Aux1 -= 1;
                            Aux_num_barras -= 1;
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
                }
                else
                {
                    Aux_Refuerzos[i] = Diametro1;
                    Cont_Aux1 -= 1;
                    Aux_num_barras -= 1;
                }
                i++;
            }
            Set_Refuerzo_Seccion(Aux_Refuerzos, recub);
        }

        public void Add_Ref_graph(double EscalaX, double EscalaY, double EscalaR)
        {
            GraphicsPath path;
            double r = 0;
            double[] pcentro;
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
                r = FunctionsProject.Find_Diametro(Convert.ToInt32(refuerzoi.Diametro.Substring(1))) / 2;
                r = r * EscalaR;

                xc = refuerzoi.Coord[0] * EscalaX;
                yc = -refuerzoi.Coord[1] * EscalaY;
                pcentro = new double[] { xc, yc };

                MAT_CONCRETE material = new MAT_CONCRETE
                {
                    FC = 4220,
                    Name = "FY4220"
                };

                circulo = new CCirculo("Refuerzo", r, pcentro, material, TipodeSeccion.Circle, pCoord: null);
                circulo.Set_puntos(10, r);

                path.AddClosedCurve(circulo.Puntos.ToArray());
                Shapes_ref.Add(path);
            }
        }

        public GraphicsPath Add_Estribos(double EscalaX, double EscalaY, float rec)
        {
            GraphicsPath path = new GraphicsPath();
            CCirculo circulo1, circulo2;
            double r1 = (radio - rec) * 100;
            double r2 = (r1 + FunctionsProject.Find_Diametro(Estribo.NoEstribo));

            MAT_CONCRETE material = new MAT_CONCRETE
            {
                FC = 4220,
                Name = "FY4220"
            };

            circulo1 = new CCirculo("Refuerzo", r1, Centro, material, TipodeSeccion.Circle, pCoord: null);
            circulo1.Set_puntos(50, r1 * EscalaX);

            circulo2 = new CCirculo("Refuerzo", r2, Centro, material, TipodeSeccion.Circle, pCoord: null);
            circulo2.Set_puntos(50, r2 * EscalaX);

            path.AddClosedCurve(circulo1.Puntos.ToArray());
            path.AddClosedCurve(circulo2.Puntos.ToArray());

            return path;
        }

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

        public void CalcularArea()
        {
            Area = Math.PI * Math.Pow(radio, 2);
        }

        public void Dibujo_Seccion(Graphics g, double EscalaX, double EscalaY, bool seleccion)
        {
            SolidBrush br = new SolidBrush(Color.FromArgb(150, Color.Gray));
            Pen P1;
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
            }

            Set_puntos(50, radio * 100 * EscalaX);
            g.DrawClosedCurve(P1, Puntos.ToArray());
            g.FillClosedCurve(br, Puntos.ToArray());
            Seccion_path.AddClosedCurve(Puntos.ToArray());
        }

        public void Set_Refuerzo_Seccion(int[] Refuerzos_temp, double Recubrimiento)
        {
            double Long_arco = 0;
            double Theta = 0;
            double Radio_interno = 0;
            double Perimetro_interno = 0;
            double[] Coord = new double[2];
            int id = 0;
            CRefuerzo refuerzoi = null;
            Refuerzos.Clear();

            Radio_interno = ((2 * radio * 100) - 2 * Recubrimiento - 2) / 2;
            Perimetro_interno = 2 * Math.PI * Radio_interno;
            Long_arco = Perimetro_interno / Refuerzos_temp.Count();

            foreach (int Diametroi in Refuerzos_temp)
            {
                Coord[0] = Radio_interno * Math.Cos((Math.PI / 2) - Theta);
                Coord[1] = Radio_interno * Math.Sin((Math.PI / 2) - Theta);
                refuerzoi = FunctionsProject.DeepClone(new CRefuerzo(id, "#" + Diametroi, Coord, TipodeRefuerzo.longitudinal));
                refuerzoi.Alzado = 1;
                Refuerzos.Add(refuerzoi);
                id++;
                Theta += Long_arco / Radio_interno;
            }
        }

        public override string ToString()
        {
            string Nombre_seccion;
            Nombre_seccion = $"C{Math.Round(radio * 2 * 100, 0)}{Material.Name}";
            return string.Format("{0}", Nombre_seccion);
        }

        public override bool Equals(object obj)
        {
            if (obj is CCirculo)
            {
                CCirculo temp = (CCirculo)obj;

                if (temp.radio == radio & Material == temp.Material)
                    return true;
            }

            return false;
        }

        public int CompareTo(object obj)
        {
            if (obj is CCirculo)
            {
                CCirculo temp = (CCirculo)obj;
                if (Area > temp.Area) return 1;
                if (Area < temp.Area) return -1;
            }
            return 0;
        }

        public double Peso_Estribo(Estribo pEstribo, float recubrimiento)
        {
            return 0;
        }

        public void Dibujo_Autocad(double Xi, double Yi, int Num_Alzado)
        {
            // throw new NotImplementedException();
        }

        public void Actualizar_Ref(Alzado palzado, int indice, FInterfaz_Seccion fInterfaz)
        {
            if (palzado.Colum_Alzado[indice] != null)
            {
                var Refuerzo_alzado = Refuerzos.FindAll(x => x.Alzado == palzado.ID);
                foreach (var refuerzoi in Refuerzo_alzado)
                {
                    refuerzoi.Diametro = $"#{palzado.Colum_Alzado[indice].NoBarra}";
                }
            }

            if (fInterfaz != null)
            {
                fInterfaz.edicion = Tipo_Edicion.Secciones_modelo;
                fInterfaz.Get_Columna();
                fInterfaz.Load_Pisos();
                fInterfaz.Get_section();
                fInterfaz.Invalidate();
            }
        }

        public void Refueroz_Adicional(Alzado palzado, int indice, FInterfaz_Seccion fInterfaz)
        {
            if (palzado.Colum_Alzado[indice] != null)
            {
            }

            if (fInterfaz != null)
            {
                fInterfaz.edicion = Tipo_Edicion.Secciones_modelo;
                fInterfaz.Get_Columna();
                fInterfaz.Load_Pisos();
                fInterfaz.Get_section();
                fInterfaz.Invalidate();
            }
        }

        public static bool operator ==(CCirculo s1, CCirculo s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(CCirculo s1, CCirculo s2)
        {
            try
            {
                return !s1.Equals(s2);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool operator <(CCirculo s1, CCirculo s2)
        {
            if (s1.CompareTo(s2) < 0)
                return true;
            else
                return false;
        }

        public static bool operator >(CCirculo s1, CCirculo s2)
        {
            if (s1.CompareTo(s2) > 0)
                return true;
            else
                return false;
        }

        #region Diagrama de iteraccion: Propiedades y Metodos

        public List<Tuple<List<float[]>, int>> MnPn3D { get; set; }
        public List<Tuple<List<float[]>, int>> PnMn2D { get; set; }
        public List<Tuple<List<float[]>, int>> MuPu3D { get; set; }
        public List<Tuple<List<float[]>, int>> PuMu2D { get; set; }
        public List<Tuple<List<float[]>, int>> PnMn2D_v1 { get; set; }
        public List<Tuple<List<float[]>, int>> PbMb2D { get; set; } = new List<Tuple<List<float[]>, int>>();
        public List<Tuple<float[], int>> PbMb3D { get; set; } = new List<Tuple<float[], int>>();
        private List<Tuple<List<float>, int>> AreaComprimida = new List<Tuple<List<float>, int>>();
        private List<Tuple<List<float[]>, int>> CentroideAreaComprimida = new List<Tuple<List<float[]>, int>>();

        public void DiagramaInteraccion()
        {
            float ecu = 0.003f;
            float Fy = 4220;
            float fc = Material.FC;
            float Es = 2000000;
            float beta = 0.85f - 0.05f * (fc - 280) / 70f;

            foreach (CRefuerzo cRefuerzo in Refuerzos)
            {
                cRefuerzo.Coordenadas_PorCadaAngulo = new List<Tuple<float[], int>>();
                cRefuerzo.Esfuerzos_PorCadaCPorCadaAngulo = new List<Tuple<List<float>, int>>();
                cRefuerzo.Fuerzas_PorCadaCPorCadaAngulo = new List<Tuple<List<float>, int>>();
                cRefuerzo.Momento_PorCadaCPorCadaAngulo = new List<Tuple<List<float>, int>>();
                cRefuerzo.Deformacion_PorCadaCPorCadaAngulo = new List<Tuple<List<float>, int>>();
            }

            int DeltasVariacionC = 20;
            int Delta = 10;

            for (int Angulo = 0; Angulo <= 360; Angulo += Delta)
            {
                List<float[]> PorCadaRotacion = new List<float[]>();
                List<float> ms = new List<float>();

                //Rotacion de la Sección

                Refuerzos.ForEach(x => x.CalcularCoordenadasPorCadaAngulo(Angulo));

                List<float> C_Variando = new List<float>();
                List<float> a_Variando = new List<float>();
                List<float> AreaComprimida1 = new List<float>();
                List<float[]> CentroideAreaComprimida1 = new List<float[]>();

                float Ymin = -(float)radio * 100;
                float Ymax = (float)radio * 100;

                for (float C = Ymin + (Ymax - Ymin) / DeltasVariacionC; C <= Ymax; C += (Ymax - Ymin) / DeltasVariacionC)
                {
                    a_Variando.Add(C + (Ymax - C) - (Ymax - C) * beta);
                    C_Variando.Add(C);
                }

                Refuerzos.ForEach(x => x.CalcularDeformacion(C_Variando, ecu, Angulo, Fy, Es, Ymax,Shape));

                //Calculo de PnMn para cada variacion de c
                for (int i = 0; i < a_Variando.Count; i++)
                {
                    float A1, A2;
                    float[] Centroide_comp;

                    A1 = Area_Segmento(a_Variando[i]);
                    A2 = Area_Segmento(-(float)Math.Round(radio, 2) * 100);

                    AreaComprimida1.Add((float)(Math.Pow(100, 2) * Area) - (A1 - A2));
                    Centroide_comp = new float[] { 0, (float)Centroide_Segmento(a_Variando[i], A1 - A2) };
                    CentroideAreaComprimida1.Add(Centroide_comp);
                }

                CentroideAreaComprimida.Add(new Tuple<List<float[]>, int>(CentroideAreaComprimida1, Angulo));
                AreaComprimida.Add(new Tuple<List<float>, int>(AreaComprimida1, Angulo));
            }

            PnMn2D = new List<Tuple<List<float[]>, int>>();
            PuMu2D = new List<Tuple<List<float[]>, int>>();

            for (int i = 0; i < AreaComprimida.Count; i++)
            {
                List<float[]> PnMnAux = new List<float[]>();
                List<float[]> PuMuAux = new List<float[]>();

                for (int j = 0; j < AreaComprimida[i].Item1.Count; j++)
                {
                    float Cc = 0.85f * fc * AreaComprimida[i].Item1[j];
                    float Fs = 0; float Ms = 0;
                    float Ast = 0;

                    Ast = (float)Refuerzos.Select(x => x.As_Long).Sum();
                    foreach (CRefuerzo cRefuerzo in Refuerzos)
                    {
                        Fs += cRefuerzo.Fuerzas_PorCadaCPorCadaAngulo[i].Item1[j];
                        Ms += cRefuerzo.Momento_PorCadaCPorCadaAngulo[i].Item1[j];
                    }

                    float Pmax = 0.75f * (0.85f * fc * ((float)Area * 10000 - Ast) + Fy * Ast);
                    float Pn_ = Cc + Fs;
                    float Mn_ = Cc * (-CentroideAreaComprimida[i].Item1[j][1]) + Ms;

                    float minY = Refuerzos.Min(x => x.Coordenadas_PorCadaAngulo[i].Item1[1]);
                    float esi = Refuerzos.Find(x => x.Coordenadas_PorCadaAngulo[i].Item1[1] == minY).Deformacion_PorCadaCPorCadaAngulo[i].Item1[j];

                    float et = Math.Abs(esi);
                    float fi = DeterminarFi(et);

                    float Pu = Pn_ * fi;
                    float Mu = Mn_ * fi;

                    if (Pn_ > Pmax)
                    {
                        Pn_ = Pmax;
                        Pu = Pmax * 0.65f;
                    }

                    if (Pn_ < 0)
                    {
                        if (PnMnAux.Exists(x => x[0] == 0) == false)
                        {
                            PnMnAux.Add(new float[] { 0, (-Ast * Fy) });
                            PuMuAux.Add(new float[] { 0, (-Ast * Fy) });
                        }
                    }
                    else
                    {
                        PnMnAux.Add(new float[] { Mn_, Pn_ });
                        PuMuAux.Add(new float[] { Mu, Pu });
                    }
                }
            }
        }

        private float DeterminarFi(float et)
        {
            float fi;
            if (et <= 0.002f)
            {
                fi = 0.65f;
            }
            else if (et > 0.002 && et < 0.005f)
            {
                fi = 0.65f + (et - 0.002f) * (250f / 3f);
            }
            else
            {
                fi = 0.90f;
            }
            return fi;
        }

        public void Pn_Balanceado(double recubrimiento, double DiyMax, int Angulo)
        {
            double Magnitud_Cb, Magnitud_ab;
            double Pc_b, Mc_B;
            double Pb, Mb;
            double cb, ab;
            double Centroide;
            double fsl, Msl;

            float ecu = 0.003f;
            float Fy = 4220;
            float fc = Material.FC;
            float Es = 2000000;
            float beta = 0.85f - 0.05f * (fc - 280) / 70f;
            float ey = Fy / Es;
            double A1, A2,AreaComp;

            Magnitud_Cb = (DiyMax + radio * 100) * ecu / (ey + ecu);
            Magnitud_ab = beta * Magnitud_Cb;

            cb = Magnitud_Cb - (radio * 100);
            ab = Magnitud_ab - (radio * 100);

            A1 = Area_Segmento((float)ab);
            A2 = Area_Segmento(-(float)radio * 100);
            AreaComp =(A1 - A2);

            Pc_b = 0.85 * fc * AreaComp;

            Centroide = Centroide_Segmento(ab, AreaComp);
            Mc_B = Pc_b * Math.Abs(Centroide);

            List<float> temp_c = new List<float>();
            temp_c.Add((float)cb);

            foreach (CRefuerzo refuezo in Refuerzos)
            {
                refuezo.CalcularDeformacion(temp_c, ecu, Angulo, Fy, Es, (float)radio * 100, Shape);
            }

            fsl = Refuerzos.Select(x => x.Fuerzas_PorCadaCPorCadaAngulo.Select(x1 => x1.Item1).Select(x2 => x2[0]).First()).ToList().Sum();
            Msl = Refuerzos.Select(x => x.Momento_PorCadaCPorCadaAngulo.Select(x1 => x1.Item1).Select(x2 => x2[0]).First()).ToList().Sum();

            Pb = Pc_b + fsl;
            Mb = Mc_B + Msl;

            float[] Temp = new float[] { (float)Mb, (float)Pb };
            PbMb3D.Add(new Tuple<float[], int>(Temp, Angulo));
        }

        public float Area_Segmento(float Y)
        {
            float a, b;

            a = Y / ((float)radio * 100);
            b = (float)Math.Sqrt(Math.Pow(radio * 100, 2) - Math.Pow(Math.Round(Y, 2), 2));
            return (float)(Math.Pow(radio * 100, 2) * Math.Asin(a) + Y * b);
        }

        public double Centroide_Segmento(double Y, double Area)
        {
            double a, b;
            a = Math.Pow(radio * 100, 2) - Math.Pow(Y, 2);
            b = Math.Pow(a, 3f / 2f);
            return (-2 * b / 3) / Area;
        }

        #endregion Diagrama de iteraccion: Propiedades y Metodos
    }
}