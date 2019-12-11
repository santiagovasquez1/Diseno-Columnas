using DisenoColumnas.Clases;
using DisenoColumnas.Interfaz_Seccion;
using DisenoColumnas.Secciones_Predefinidas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using B_Operaciones_Matricialesl;
using System.Windows.Forms.DataVisualization.Charting;

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

        #region Propiedades: Cantidades -Estribos

        public Tuple<float, float, float, string> Estribo_Dimensiones_B_H_G_Nomenclatura { get; set; }

        public Tuple<int, float, float, string> GanchoV_Dim_Cant_Ltotal_G_Nomenclatura { get; set; }
        public Tuple<int, float, float, string> GanchoH_Dim_Cant_Ltotal_G_Nomenclatura { get; set; }

        #endregion Propiedades: Cantidades -Estribos

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


        #region Diagrama de Interacción: Propiedades y Metodos
        public List<Tuple<List<float[]>, int>> MnPn3D { get; set; }
        public List<Tuple<List<float[]>, int>> PnMn2D { get; set; }

        public List<Tuple<List<float[]>, int>> MuPu3D { get; set; }
        public List<Tuple<List<float[]>, int>> PuMu2D { get; set; }

        public List<Tuple<List<float[]>, int>> PnMn2D_v1 { get; set; }
    

        public void DiagramaInteraccion()
        {

            float b1 = 0.85f;

            List<PointF> Coordenadas = new List<PointF>();
            float X, Y;
            X = -(B * 100 / 2);
            Y = -(H * 100 / 2);
            Coordenadas.Add(new PointF(X, Y));

            X = (B * 100 / 2);
            Y = -(H * 100 / 2);
            Coordenadas.Add(new PointF(X, Y));

            X = (B * 100 / 2);
            Y = (H * 100 / 2);
            Coordenadas.Add(new PointF(X, Y));

            X = -(B * 100 / 2);
            Y = (H * 100 / 2);
            Coordenadas.Add(new PointF(X, Y));
            float ecu = 0.003f;
            float Fy = Form1.Proyecto_.FY;
            //float fc = Material.FC;
            float fc = 280f;
            float Es = 2000000;

            List<List<float[]>> Coordenadas_PorCadaAngulo = new List<List<float[]>>();

            List<Tuple<List<float>, int>> m_PorCadaAngulo = new List<Tuple<List<float>, int>>();
            List<Tuple<List<float>, int>> YVariacionC = new List<Tuple<List<float>, int>>();
            List<Tuple<List<float>, int>> YVariaciona = new List<Tuple<List<float>, int>>();

            List<Tuple<List<float>, int>> AreaComprimida = new List<Tuple<List<float>, int>>();
            List<Tuple<List<float[]>, int>> CentroideAreaComprimida = new List<Tuple<List<float[]>, int>>();

            foreach (CRefuerzo cRefuerzo in Refuerzos)
            {
                cRefuerzo.Coordenadas_PorCadaAngulo = new List<Tuple<float[], int>>();
                cRefuerzo.Esfuerzos_PorCadaCPorCadaAngulo = new List<Tuple<List<float>, int>>();
                cRefuerzo.Fuerzas_PorCadaCPorCadaAngulo = new List<Tuple<List<float>, int>>();
                cRefuerzo.Deformacion_PorCadaCPorCadaAngulo = new List<Tuple<List<float>, int>>();
            }


            int DeltasVariacionC = 20;

            int Delta = 10;
            for (int Angulo = 0; Angulo <= 360; Angulo += Delta)
            {
                List<float[]> PorCadaRotacion = new List<float[]>();
                List<float> ms = new List<float>();
                //Rotacion de la Sección 
                for (int i = 0; i < Coordenadas.Count; i++)
                {
                    List<double> CoordRotadas = Operaciones.Rotacion(Coordenadas[i].X, Coordenadas[i].Y, (Angulo * Math.PI) / 180);

                    PorCadaRotacion.Add(new float[] { (float)CoordRotadas[0], (float)CoordRotadas[1] });
                }


                float Ymax = -99999; float Ymin = 999999;
                float Xmax = -99999; float Xmin = 999999;

                for (int i = 0; i < PorCadaRotacion.Count; i++)
                {

                    if (PorCadaRotacion[i][1] > Ymax)
                    {
                        Ymax = PorCadaRotacion[i][1];
                    }
                    if (PorCadaRotacion[i][1] < Ymin)
                    {
                        Ymin = PorCadaRotacion[i][1];
                    }

                    if (PorCadaRotacion[i][0] > Xmax)
                    {
                        Xmax = PorCadaRotacion[i][0];
                    }
                    if (PorCadaRotacion[i][0] < Xmin)
                    {
                        Xmin = PorCadaRotacion[i][0];
                    }
                    float m;
                    if (i + 1 == PorCadaRotacion.Count)
                    {
                        m = (PorCadaRotacion[i][1] - PorCadaRotacion[0][1]) / (PorCadaRotacion[i][0] - PorCadaRotacion[0][0]);
                    }
                    else
                    {
                        m = (PorCadaRotacion[i][1] - PorCadaRotacion[i + 1][1]) / (PorCadaRotacion[i][0] - PorCadaRotacion[i + 1][0]);
                    }

                    ms.Add(m);

                }


                Refuerzos.ForEach(x => x.CalcularCoordenadasPorCadaAngulo(Angulo));



                List<float> C_Variando = new List<float>();
                List<float> a_Variando = new List<float>();
                List<float> AreaComprimida1 = new List<float>();
                List<float[]> CentroideAreaComprimida1 = new List<float[]>();

                for (float C = Ymin + (Ymax - Ymin) / DeltasVariacionC; C <= Ymax; C += (Ymax - Ymin) / DeltasVariacionC)
                {
                    a_Variando.Add(C + (Ymax - C) - (Ymax - C) * b1);
                    C_Variando.Add(C);
                }


                Refuerzos.ForEach(x => x.CalcularDeformacion(C_Variando, ecu, Angulo, Fy, Es, Ymax));

                for (int i = 0; i < a_Variando.Count; i++)
                {

                    List<float[]> PuntosInter = HallarPuntosDeInter(ms, PorCadaRotacion, a_Variando[i], Xmax, Xmin);

                    List<float[]> PuntosPorEncimadeA = new List<float[]>();

                    for (int j = 0; j < PorCadaRotacion.Count; j++)
                    {
                        if (a_Variando[i] <= PorCadaRotacion[j][1])
                        {
                            PuntosPorEncimadeA.Add(PorCadaRotacion[j]);
                        }

                    }
                    PuntosInter = PuntosInter.OrderByDescending(x => x[0]).ToList();
                    PuntosPorEncimadeA = PuntosPorEncimadeA.OrderByDescending(x => x[0]).ToList();

                    List<float[]> PuntosParaArea = new List<float[]>();

                    PuntosParaArea.Add(PuntosInter[0]); PuntosParaArea.AddRange(PuntosPorEncimadeA); PuntosParaArea.Add(PuntosInter[1]);

                    float AreaComprimida_Aux = FunctionsProject.DeterminarArea(PuntosParaArea);


                    AreaComprimida1.Add(AreaComprimida_Aux);

                    CentroideAreaComprimida1.Add(FunctionsProject.DeterminarCentroide(PuntosParaArea));

                }

                CentroideAreaComprimida.Add(new Tuple<List<float[]>, int>(CentroideAreaComprimida1, Angulo));
                AreaComprimida.Add(new Tuple<List<float>, int>(AreaComprimida1, Angulo));
                YVariaciona.Add(new Tuple<List<float>, int>(a_Variando, Angulo));
                YVariacionC.Add(new Tuple<List<float>, int>(C_Variando, Angulo));
                Coordenadas_PorCadaAngulo.Add(PorCadaRotacion);
                m_PorCadaAngulo.Add(new Tuple<List<float>, int>(ms, Angulo));


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
                    //Pmax
                    float Ast = 0;
                    foreach (CRefuerzo cRefuerzo in Refuerzos)
                    {
                        Fs += cRefuerzo.Fuerzas_PorCadaCPorCadaAngulo[i].Item1[j];
                        Ms += Math.Abs(cRefuerzo.Fuerzas_PorCadaCPorCadaAngulo[i].Item1[j]) * Math.Abs(cRefuerzo.Coordenadas_PorCadaAngulo[i].Item1[1]);

                        Ast += (float)Form1.Proyecto_.AceroBarras[Convert.ToInt32(cRefuerzo.Diametro.Substring(1))] * 10000;
                    }

                    float Mn_ = Cc * (CentroideAreaComprimida[i].Item1[j][1]) + Ms;
                    float Pn_ = Cc + Fs;

                    float minY = Refuerzos.Min(x => x.Coordenadas_PorCadaAngulo[i].Item1[1]);
                    float esi = Refuerzos.Find(x => x.Coordenadas_PorCadaAngulo[i].Item1[1] == minY).Deformacion_PorCadaCPorCadaAngulo[i].Item1[j];

                    float et = Math.Abs(esi);
                    float fi = DeterminarFi(et);

                    float Pu = Pn_ * fi;
                    float Mu = Mn_ * fi;



                    float Pmax = 0.75f * (0.85f * fc * ((float)Area * 10000 - Ast) + Fy * Ast);


                    if (Pn_ > Pmax)
                    {
                        Pn_ = Pmax;
                        Pu = Pmax * 0.65f;
                    }

                    if (Pn_ < 0)
                    {
                        if (PnMnAux.Exists(x => x[0] == 0) == false)
                        {
                            PnMnAux.Add(new float[] { 0, -Ast * Fy });
                            PuMuAux.Add(new float[] { 0, -Ast * Fy });


                        }
                    }
                    else
                    {
                        PnMnAux.Add(new float[] { Mn_, Pn_ });
                        PuMuAux.Add(new float[] { Mu, Pu });
                    }


                    if (j == AreaComprimida[i].Item1.Count - 1)
                    {
                        float Pmax1 = PnMnAux.Max(x => x[1]);
                        int IndicePmax1 = PnMnAux.FindIndex(x => x[1] == Pmax1);

                        PnMnAux.Insert(IndicePmax1, new float[] { 0, Pmax1 });
                        PuMuAux.Insert(IndicePmax1, new float[] { 0, Pmax1 * 0.65f });
                    }

                }
                PnMn2D.Add(new Tuple<List<float[]>, int>(PnMnAux, AreaComprimida[i].Item2));
                PuMu2D.Add(new Tuple<List<float[]>, int>(PuMuAux, AreaComprimida[i].Item2));
            }


            MnPn3D = new List<Tuple<List<float[]>, int>>();
            MuPu3D = new List<Tuple<List<float[]>, int>>();


            for (int i = 0; i < PnMn2D.Count; i++)
            {
                int Angulo = PnMn2D[i].Item2;
                List<float[]> SeriePuntos = new List<float[]>();
                List<float[]> SeriePuntosU = new List<float[]>();

                for (int j = 0; j < PnMn2D[i].Item1.Count; j++)
                {
                    float X1 = (float)(PnMn2D[i].Item1[j][0] * Math.Cos((Angulo * Math.PI / 180)));
                    float Y2 = (float)(PnMn2D[i].Item1[j][0] * Math.Sin((Angulo * Math.PI / 180)));
                    float Z2 = PnMn2D[i].Item1[j][1];
                    float[] PuntosDescompuestos = new float[] { X1, Y2, Z2 };

                    SeriePuntos.Add(PuntosDescompuestos);

                    float X1U = (float)(PuMu2D[i].Item1[j][0] * Math.Cos((Angulo * Math.PI / 180)));
                    float Y2U = (float)(PuMu2D[i].Item1[j][0] * Math.Sin((Angulo * Math.PI / 180)));
                    float Z2U = PuMu2D[i].Item1[j][1];
                    float[] PuntosDescompuestosUltimos = new float[] { X1U, Y2U, Z2U };

                    SeriePuntosU.Add(PuntosDescompuestosUltimos);


                }
                MnPn3D.Add(new Tuple<List<float[]>, int>(SeriePuntos, PnMn2D[i].Item2));
                MuPu3D.Add(new Tuple<List<float[]>, int>(SeriePuntosU, PnMn2D[i].Item2));

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
        private List<float[]> HallarPuntosDeInter(List<float> m, List<float[]> XY, float Yperteneciente, float Xmax, float Xmin)
        {
            List<float[]> PuntosHallados = new List<float[]>();

            for (int i = 0; i < m.Count; i++)
            {
                float X = ((Yperteneciente - XY[i][1]) / m[i]) + XY[i][0];

                if (X >= Xmin && X <= Xmax)
                {
                    PuntosHallados.Add(new float[] { X, Yperteneciente });
                }
            }

            return PuntosHallados;


        }

        #endregion


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

            if (s_max > 15)
            {
                s_max = 15;
            }

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
                double limite1, limite2, limite3, limite_def;
                double PAs1, PAs2;
                float Ach = (B - 2 * r) * (H - 2 * r);

                S1 = Estribo.NoRamasV1 * Form1.Proyecto_.AceroBarras[3] / (FD1 * B * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasV1 * FY * Form1.Proyecto_.AceroBarras[3] / (FD2 * (B - 2 * r) * Material.FC);

                #region Estribo  #3

                SV = new double[] { S1 * 100, S2 * 100 }.Min();

                S1 = Estribo.NoRamasH1 * Form1.Proyecto_.AceroBarras[3] / (FD1 * H * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasH1 * FY * Form1.Proyecto_.AceroBarras[3] / (FD2 * (H - 2 * r) * Material.FC);

                SH = new double[] { S1 * 100, S2 * 100 }.Min();
                Sdef1 = Math.Round(Math.Min(SV, SH), 1);

                if (gDE == GDE.DMO)
                {
                    var Db = Refuerzos.Min();
                    int Db1 = Convert.ToInt16(Db.Diametro.Substring(1));
                    limite1 = 8 * Form1.Proyecto_.Diametro_ref[Db1];
                    limite2 = 16 * Form1.Proyecto_.Diametro_ref[3];
                    limite3 = 15;
                    limite_def = new double[] { limite1, limite2, limite3 }.Min();
                    if (Sdef1 > limite_def) Sdef1 = limite_def;
                }

                if (gDE == GDE.DES)
                {
                    var Db = Refuerzos.Min();
                    int Db1 = Convert.ToInt16(Db.Diametro.Substring(1));
                    limite1 = 6 * Form1.Proyecto_.Diametro_ref[Db1];
                    limite2 = 15;
                    limite_def = new double[] { limite1, limite2 }.Min();
                    if (Sdef1 > limite_def) Sdef1 = limite_def;
                }

                Estribo.NoEstribo = 3;
                Estribo.Separacion = (float)Sdef1;

                PAs1 = Peso_Estribo(Estribo, r);

                #endregion Estribo  #3

                #region Estribo  #4

                S1 = Estribo.NoRamasV1 * Form1.Proyecto_.AceroBarras[4] / (FD1 * B * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasV1 * FY * Form1.Proyecto_.AceroBarras[4] / (FD2 * (B - 2 * r) * Material.FC);
                SV = new double[] { S1 * 100, S2 * 100 }.Min();

                S1 = Estribo.NoRamasH1 * Form1.Proyecto_.AceroBarras[4] / (FD1 * H * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasH1 * FY * Form1.Proyecto_.AceroBarras[4] / (FD2 * (H - 2 * r) * Material.FC);
                SH = new double[] { S1 * 100, S2 * 100 }.Min();

                Sdef2 = Math.Round(Math.Min(SV, SH), 1);

                if (gDE == GDE.DMO)
                {
                    var Db = Refuerzos.Min();
                    int Db1 = Convert.ToInt16(Db.Diametro.Substring(1));
                    limite1 = 8 * Form1.Proyecto_.Diametro_ref[Db1];
                    limite2 = 16 * Form1.Proyecto_.Diametro_ref[4];
                    limite3 = 15;
                    limite_def = new double[] { limite1, limite2, limite3 }.Min();
                    if (Sdef2 > limite_def) Sdef2 = limite_def;
                }

                if (gDE == GDE.DES)
                {
                    var Db = Refuerzos.Min();
                    int Db1 = Convert.ToInt16(Db.Diametro.Substring(1));
                    limite1 = 6 * Form1.Proyecto_.Diametro_ref[Db1];
                    limite2 = 15;
                    limite_def = new double[] { limite1, limite2 }.Min();
                    if (Sdef2 > limite_def) Sdef2 = limite_def;
                }

                Estribo.NoEstribo = 4;
                Estribo.Separacion = (float)Sdef2;

                PAs2 = Peso_Estribo(Estribo, r);

                #endregion Estribo  #4

                if (PAs1 < PAs2)
                {
                    Estribo.NoEstribo = 3;
                    Estribo.Area = Form1.Proyecto_.AceroBarras[Estribo.NoEstribo];
                    Estribo.Separacion = (float)Sdef1;
                }
                else
                {
                    Estribo.NoEstribo = 4;
                    Estribo.Area = Form1.Proyecto_.AceroBarras[Estribo.NoEstribo];
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
                        X2 = 0;
                    }
                    else
                    {
                        Diametro2 = Barra_aux - 1;
                        X2 = Convert.ToInt32((As_min - Form1.Proyecto_.AceroBarras[Diametro1] * Num_Barras) / (Form1.Proyecto_.AceroBarras[Diametro2] - Form1.Proyecto_.AceroBarras[Diametro1]));
                    }
                }
                else
                {
                    Diametro1 = Barra_aux;
                    Diametro2 = Barra_aux + 1;
                    X2 = Convert.ToInt32((As_min - Form1.Proyecto_.AceroBarras[Diametro1] * Num_Barras) / (Form1.Proyecto_.AceroBarras[Diametro2] - Form1.Proyecto_.AceroBarras[Diametro1]));
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
                    X2 = FunctionsProject.Redondear_Entero(X2, 4);
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

        public void Dibujo_Autocad(double Xi, double Yi, int Num_Despiece)
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

            #endregion Nombre_Seccion
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

        #region Metodos: Cantidades

        public void CalcularDimensionesEstribo_Gancho(Estribo estribo, float R)
        {
            float B_Estribo = (B - 2 * R);
            float H_Estribo = (H - 2 * R);
            float G135_Estribo = Form1.Proyecto_.G135[estribo.NoEstribo];
            int CantEstribosV = estribo.NoRamasV1 - 2;
            int CantEstribosH = estribo.NoRamasH1 - 2;
            float Gancho180_G = Form1.Proyecto_.G180[estribo.NoEstribo];
            float Long_GanchoH = (B - 2 * R) + 2 * Gancho180_G;
            float Long_GanchoV = (H - 2 * R) + 2 * Gancho180_G;
            string Conve_Estribo = $" E  #{estribo.NoEstribo}  {B_Estribo}*{H_Estribo}  G{G135_Estribo}  F0/45";
            string Conve_Gancho_H = $" #{estribo.NoEstribo}  {Long_GanchoH}  U{Gancho180_G}  U{Gancho180_G}";
            string Conve_Gancho_V = $" #{estribo.NoEstribo}  {Long_GanchoV}  U{Gancho180_G}  U{Gancho180_G}";

            Estribo_Dimensiones_B_H_G_Nomenclatura = new Tuple<float, float, float, string>(B_Estribo, H_Estribo, G135_Estribo, Conve_Estribo);
            GanchoH_Dim_Cant_Ltotal_G_Nomenclatura = new Tuple<int, float, float, string>(CantEstribosH, Long_GanchoH, Gancho180_G, Conve_Gancho_H);
            GanchoV_Dim_Cant_Ltotal_G_Nomenclatura = new Tuple<int, float, float, string>(CantEstribosV, Long_GanchoV, Gancho180_G, Conve_Gancho_V);
        }

        public void Actualizar_Ref(Alzado palzado, int indice, FInterfaz_Seccion fInterfaz)
        {
            if (palzado.Colum_Alzado[indice] != null)
            {
                if (palzado.Colum_Alzado[indice].Tipo == "A" | palzado.Colum_Alzado[indice].Tipo == "Bottom")
                {
                    Refueroz_Adicional(palzado, indice, fInterfaz);
                }
                else
                {
                    var Refuerzo_alzado = Refuerzos.FindAll(x => x.Alzado == palzado.ID);
                    foreach (var refuerzoi in Refuerzo_alzado)
                    {
                        refuerzoi.Diametro = $"#{palzado.Colum_Alzado[indice].NoBarra}";
                    }
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
            CRefuerzo nRefuerzo = null;
            double[] coord1 = new double[2];
            int id = 0;
            double Xmin, Xmax, Ymin, Ymax;

            Xmin = Refuerzos.Select(x => x.Coord[0]).Min();
            Xmax = Refuerzos.Select(x => x.Coord[0]).Max();
            Ymin = Refuerzos.Select(x => x.Coord[1]).Min();
            Ymax = Refuerzos.Select(x => x.Coord[1]).Max();

            if (palzado.Colum_Alzado[indice] != null)
            {
                if (Refuerzos.Exists(x => x.Alzado == palzado.ID))
                {
                    var Refuerzo_alzado = Refuerzos.FindAll(x => x.Alzado == palzado.ID);
                    foreach (var refuerzoi in Refuerzo_alzado)
                    {
                        refuerzoi.Diametro = $"#{palzado.Colum_Alzado[indice].NoBarra}";
                    }
                }
                else
                {
                   for (int i = 0; i < palzado.Colum_Alzado[indice].CantBarras; i+=2)
                    {
                        id = Refuerzos.Last().id + 1;

                        if (i % 2 == 0)
                        {
                            coord1[0] = Refuerzos[i].Coord[0] + Form1.Proyecto_.Diametro_ref[Convert.ToInt16(Refuerzos[i].Diametro.Substring(1))] / 2 +
                                Form1.Proyecto_.Diametro_ref[Convert.ToInt16(palzado.Colum_Alzado[indice].NoBarra)] / 2;
                            coord1[1] = Refuerzos[i].Coord[1];
                        }
                        else
                        {
                            coord1[0] = Refuerzos[Refuerzos.Count - i - 1].Coord[0] + Form1.Proyecto_.Diametro_ref[Convert.ToInt16(Refuerzos[Refuerzos.Count - i - 1].Diametro.Substring(1))] / 2 +
                                Form1.Proyecto_.Diametro_ref[Convert.ToInt16(palzado.Colum_Alzado[indice].NoBarra)] / 2;
                            coord1[1] = Refuerzos[Refuerzos.Count - i - 1].Coord[1];
                        }

                        nRefuerzo = new CRefuerzo(id, $"#{palzado.Colum_Alzado[indice].NoBarra}",coord1, TipodeRefuerzo.longitudinal);
                        nRefuerzo.Alzado = palzado.ID;
                        Refuerzos.Add(nRefuerzo);
                    }       

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

        #endregion Metodos: Cantidades

        #endregion Propiedades y Metodos - Secciones Predefinidas
    }
}