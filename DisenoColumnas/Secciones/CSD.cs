﻿using B_Operaciones_Matricialesl;
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
    public class CSD : ISeccion, IComparable
    {
        public string Name { get; set; }
        public MAT_CONCRETE Material { get; set; }
        public float B { get; set; }
        public float H { get; set; }
        public float TW { get; set; }
        public float TF { get; set; }
        public TipodeSeccion Shape { get; set; }
        [NonSerialized] private GraphicsPath pSeccion_path;
        public GraphicsPath Seccion_path { get { return pSeccion_path; } set { pSeccion_path = value; } }
        public double Area { get; set; }
        public double Acero_Long { get; set; }
        public Estribo Estribo { get; set; }
        public List<PointF> Vertices { get; set; } = new List<PointF>();
        public List<CRefuerzo> Refuerzos { get; set; } = new List<CRefuerzo>();
        [NonSerialized] private List<GraphicsPath> pShapes_ref = new List<GraphicsPath>();
        public List<float[]> CoordenadasSeccion { get; set; }
        public bool Editado { get; set; } = false;
        public List<GraphicsPath> Shapes_ref { get { return pShapes_ref; } set { pShapes_ref = value; } }
        public List<Tuple<int, int>> No_D_Barra { get; set; }

        #region Diagrama de Interacción: Propiedades y Metodos

        public List<Tuple<List<float[]>, int>> MnPn3D { get; set; }
        public List<Tuple<List<float[]>, int>> PnMn2D { get; set; }

        public List<Tuple<List<float[]>, int>> MuPu3D { get; set; }
        public List<Tuple<List<float[]>, int>> PuMu2D { get; set; }

        public List<Tuple<List<float[]>, int>> PnMn2D_v1 { get; set; }

        #region Propiedades y Metodos para verificación de Vc

        public List<float[]> PM2M3V2V3 { get; set; }
        public List<float> Vcx { get; set; }
        public List<float> Vcy { get; set; }
        public List<float> Vsx { get; set; }
        public List<float> Vsy { get; set; }

        #endregion Propiedades y Metodos para verificación de Vc

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
                MuPu3D.Add(new Tuple<List<float[]>, int>(SeriePuntos, PnMn2D[i].Item2));
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

        #endregion Diagrama de Interacción: Propiedades y Metodos

        public CSD(string Nombre, float pB, float pH, float pTw, float pTf, MAT_CONCRETE pMaterial, TipodeSeccion pShape, List<float[]> pCoord = null)
        {
            //Unidades en metros
            Name = Nombre;
            B = pB;
            H = pH;
            TW = pTw;
            TF = pTf;
            Material = pMaterial;
            Shape = pShape;
            CoordenadasSeccion = pCoord;
            CalcularArea();
        }

        public void CalcularArea()
        {
            Area = (H * TW) + ((B - TW) * TF);
        }

        public void Cuanti_Vol(float FactorDisipacion1, float FactorDisipacion2, float r, float FY)
        {
            double Ash1 = 0; double Ash2 = 0; double Ash = 0;     //Cuantia volumetrica en el alma de la seccion
            double Asht1 = 0; double Asht2 = 0; double Asht = 0; //Cuantia volumetrica en la aleta de la seccion
            double Ach = 0; double Achw = 0;
            double bc = 0; double bwc = 0;
            double S = 0;

            if (Estribo != null)
            {
                //Vertical
                S = Estribo.Separacion / 100;
                Ach = (B - 2 * r) * (H - TF - 2 * r);
                Achw = (TW - 2 * r) * (TF - 2 * r);

                bc = B - 2 * r;
                bwc = TW - 2 * r;

                Ash1 = FactorDisipacion1 * S * bc * Material.FC / FY * (B * (H - TF) / Ach - 1);  //C.21-2
                Ash2 = FactorDisipacion2 * S * bc * Material.FC / FY;  //C.21-3

                Asht1 = FactorDisipacion1 * S * bwc * Material.FC / FY * (TW * TF / Achw - 1);  //C.21-2
                Asht2 = FactorDisipacion2 * S * bwc * Material.FC / FY;  //C.21-3

                Ash = Ash1 > Ash2 ? Ash1 : Ash2;
                Asht = Asht1 > Asht2 ? Asht1 : Asht2;

                Ash = Ash1 > Ash2 ? Ash1 : Ash2;

                if (S != 0 && Estribo.Area != 0)
                {
                    Estribo.NoRamasV1 = Convert.ToInt32(Math.Round(Ash / Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));
                    Estribo.NoRamasV2 = Convert.ToInt32(Math.Round(Asht / Estribo.Area < 2 ? 2 : (float)Math.Round(Asht / Estribo.Area, 2), 2));
                }
                else
                {
                    Estribo.NoRamasV1 = 0;
                    Estribo.NoRamasV2 = 0;
                }

                //Horizontal
                bc = (H - TF) - 2 * r;
                bwc = TF - 2 * r;

                Ash1 = FactorDisipacion1 * S * bc * Material.FC / FY * (B * (H - TF) / Ach - 1);  //C.21-2
                Ash2 = FactorDisipacion2 * S * bc * Material.FC / FY;  //C.21-3

                Asht1 = FactorDisipacion1 * S * bwc * Material.FC / FY * (TW * TF / Achw - 1);  //C.21-2
                Asht2 = FactorDisipacion2 * S * bwc * Material.FC / FY;  //C.21-3

                Ash = Ash1 > Ash2 ? Ash1 : Ash2;
                Asht = Asht1 > Asht2 ? Asht1 : Asht2;

                Ash = Ash1 > Ash2 ? Ash1 : Ash2;

                if (S != 0 && Estribo.Area != 0)
                {
                    Estribo.NoRamasH1 = Convert.ToInt32(Math.Round(Ash / Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));
                    Estribo.NoRamasH2 = Convert.ToInt32(Math.Round(Asht / Estribo.Area < 2 ? 2 : (float)Math.Round(Asht / Estribo.Area, 2), 2));
                }
                else
                {
                    Estribo.NoRamasH1 = 0;
                    Estribo.NoRamasH2 = 0;
                }
            }
        }

        public void Calc_vol_inex(float r, float FY, GDE gDE)
        {
            float FD1, FD2;
            double s_max, s_min;
            double s_d;
            double G_As1, G_As2, LG_As1, LG_As2; //Variables en la aleta
            double Gw_As1, Gw_As2, LGw_As1, LGw_As2; //Variables en el alma
            int pasos;
            int Indice_min;

            var Num_Ramas_V = new List<int>();    //Numero de ramas en altura del muro para ambos casos de ast
            var GT_As1 = new List<double>();   //Longitud total de los gancho para As1, bajo cada una de las variaciones de la separacion
            var GT_As2 = new List<double>();   //Longitud total de los gancho para As2, bajo cada una de las variaciones de la separacion
            var GTw_As1 = new List<double>();   //Longitud total de los gancho para As1, bajo cada una de las variaciones de la separacion
            var GTw_As2 = new List<double>();   //Longitud total de los gancho para As2, bajo cada una de las variaciones de la separacion

            var P_As1 = new List<double>();     //'Peso total As1
            var P_As2 = new List<double>();     //'Peso total As2

            var Sep = new List<double>();
            float delta = 0.50f;

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

            s_min = 7.5;

            if (gDE == GDE.DMO)
            {
                s_max = B / 3 < H / 3 ? B * 100 / 3 : H * 100 / 3;
            }
            else
            {
                s_max = B / 4 < H / 4 ? B * 100 / 4 : H * 100 / 4;
            }

            G_As1 = B * 100 - 2 * 2 + 2 * 17; //'Longitud del gancho transversal para Ast1
            G_As2 = B * 100 - 2 * 2 + 2 * 20.5; //'Longitud del gancho transversal para Ast2

            Gw_As1 = TW * 100 - 2 * 2 + 2 * 17; //'Longitud del gancho transversal para Ast1
            Gw_As2 = TW * 100 - 2 * 2 + 2 * 20.5; //'Longitud del gancho transversal para Ast2

            LG_As1 = H * 100 - 2 * r * 100 + 2 * 17; //'Longitud del gancho longitudinal para Ast1
            LG_As2 = H * 100 - 2 * r * 100 + 2 * 20.5; //'Longitud del gancho longitudinal para Ast2

            LGw_As1 = (H - TF) * 100 - 2 * r * 100 + 2 * 17; //'Longitud del gancho longitudinal para Ast1
            LGw_As2 = (H - TF) * 100 - 2 * r * 100 + 2 * 20.5; //'Longitud del gancho longitudinal para Ast2

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

                #endregion Estribo #4

                Sep.Add(s_d);
                s_d += delta;
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
                yc = refuerzoi.Coord[1] * EscalaY;
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
            List<float[]> Coord_aletas = new List<float[]>();
            List<float[]> Coord_alma = new List<float[]>();

            GraphicsPath path = new GraphicsPath();
            List<PointF> Vertices = new List<PointF>();
            List<PointF> Vertices2 = new List<PointF>();

            float D_off1 = 0;
            float D_off2 = 0;

            var Xunicos = CoordenadasSeccion.Select(x => x[0]).Distinct().ToList();
            var Yunicos = CoordenadasSeccion.Select(x => x[1]).Distinct().ToList();

            D_off1 = rec;
            D_off2 = rec +  (float)FunctionsProject.Find_Diametro(Estribo.NoEstribo) / 100;

            //Aleta seccion

            if (Shape == TipodeSeccion.Tee)
            {
                Coord_aletas = CoordenadasSeccion.FindAll(x => x[0] == Xunicos.Min() | x[0] == Xunicos.Max()).ToList();
                Coord_alma = CoordenadasSeccion.FindAll(x => x[0] != Xunicos.Min() & x[0] != Xunicos.Max()).ToList();
            }
            else
            {
                var Xtf = Xunicos.Find(x => x != Xunicos.Min() & x != Xunicos.Max());
                var Ytw = Yunicos.Find(y => y != Yunicos.Min() & y != Yunicos.Max());

                if (Math.Round(Ytw + TW, 2) == Math.Round(Yunicos.Max(), 2))
                {
                    Coord_aletas.Add(new float[] { Xunicos.Min(), Yunicos.Max() });
                    Coord_aletas.Add(new float[] { Xunicos.Max(), Yunicos.Max() });
                    Coord_aletas.Add(new float[] { Xunicos.Max(), Ytw });
                    Coord_aletas.Add(new float[] { Xunicos.Min(), Ytw });
                }
                else
                {
                    Coord_aletas.Add(new float[] { Xunicos.Min(), Ytw });
                    Coord_aletas.Add(new float[] { Xunicos.Min(), Yunicos.Min() });
                    Coord_aletas.Add(new float[] { Xunicos.Max(), Yunicos.Min() });
                    Coord_aletas.Add(new float[] { Xunicos.Max(), Ytw });
                }

                if (Math.Round(Xtf + TF, 2) == Math.Round(Xunicos.Max(), 2))
                {
                    Coord_alma.Add(new float[] { Xtf, Yunicos.Max() });
                    Coord_alma.Add(new float[] { Xunicos.Max(), Yunicos.Max() });
                    Coord_alma.Add(new float[] { Xunicos.Max(), Yunicos.Min() });
                    Coord_alma.Add(new float[] { Xtf, Yunicos.Min() });
                }
                else
                {
                    Coord_alma.Add(new float[] { Xunicos.Min(), Yunicos.Max() });
                    Coord_alma.Add(new float[] { Xtf, Yunicos.Max() });
                    Coord_alma.Add(new float[] { Xtf, Yunicos.Min() });
                    Coord_alma.Add(new float[] { Xunicos.Min(), Yunicos.Min() });
                }
            }

            var Coord_aletas1 = B_Operaciones_Matricialesl.Operaciones.OffSet(D_off1, FunctionsProject.DeepClone(Coord_aletas), false);
            var Coord_aletas2 = B_Operaciones_Matricialesl.Operaciones.OffSet(D_off2, FunctionsProject.DeepClone(Coord_aletas), false);
            var Coord_alma1 = B_Operaciones_Matricialesl.Operaciones.OffSet(D_off1, FunctionsProject.DeepClone(Coord_alma), false);
            var Coord_alma2 = B_Operaciones_Matricialesl.Operaciones.OffSet(D_off2, FunctionsProject.DeepClone(Coord_alma), false);

            #region Dibujo aleta

            for (int i = 0; i < Coord_aletas1.Count; i++)
            {
                Vertices.Add(new PointF(Coord_aletas1[i][0] * (float)EscalaX * 100, Coord_aletas1[i][1] * (float)EscalaX * 100));
                Vertices2.Add(new PointF(Coord_aletas2[i][0] * (float)EscalaX * 100, Coord_aletas2[i][1] * (float)EscalaX * 100));
            }

            path.AddPolygon(Vertices.ToArray());
            path.AddPolygon(Vertices2.ToArray());

            #endregion Dibujo aleta

            #region Dibujo alma

            Vertices.Clear();
            Vertices2.Clear();

            for (int i = 0; i < Coord_alma1.Count; i++)
            {
                Vertices.Add(new PointF(Coord_alma1[i][0] * (float)EscalaX * 100, Coord_alma1[i][1] * (float)EscalaX * 100));
                Vertices2.Add(new PointF(Coord_alma2[i][0] * (float)EscalaX * 100, Coord_alma2[i][1] * (float)EscalaX * 100));
            }
            path.AddPolygon(Vertices.ToArray());
            path.AddPolygon(Vertices2.ToArray());

            #endregion Dibujo alma

            return path;
        }

        public void Dibujo_Seccion(Graphics g, double EscalaX, double EscalaY, bool seleccion)
        {
            SolidBrush br = new SolidBrush(Color.FromArgb(150, Color.Gray));
            Pen P1;
            Seccion_path = new GraphicsPath();
            Vertices = new List<PointF>();

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

            #region Vertices

            for (int i = 0; i < CoordenadasSeccion.Count; i++)
            {
                Vertices.Add(new PointF(CoordenadasSeccion[i][0] * 100 * (float)EscalaX, CoordenadasSeccion[i][1] * 100 * (float)EscalaY));
            }

            #endregion Vertices

            g.DrawPolygon(P1, Vertices.ToArray());
            g.FillPolygon(br, Vertices.ToArray());
            Seccion_path.AddPolygon(Vertices.ToArray());
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

        public void Set_Refuerzo_Seccion(int[] Refuerzos_temp, double Recubrimiento)
        {
            int ContX, ContY, ContT, id;
            double posx, posy;
            double[] Coord_ref = new double[2];
            int CapasX1, CapasX2, CapasY1, CapasY2;
            int Barras_Aleta, Barras_alma;
            CRefuerzo refuerzoi;
            double DeltaX1, DeltaY1, DeltaX2, DeltaY2;
            double r = Recubrimiento / 100 + 2 * FunctionsProject.Find_Diametro(Estribo.NoEstribo) / 100;
            Refuerzos = new List<CRefuerzo>();

            if (Estribo.NoRamasV1 > Estribo.NoRamasV2)
            {
                CapasX1 = Estribo.NoRamasV1;
                CapasX2 = Estribo.NoRamasV2;
            }
            else
            {
                CapasX1 = Estribo.NoRamasV2;
                CapasX2 = Estribo.NoRamasV1;
            }

            if (Estribo.NoRamasH1 > Estribo.NoRamasH2)
            {
                CapasY1 = Estribo.NoRamasH1;
                CapasY2 = Estribo.NoRamasH2;
            }
            else
            {
                CapasY1 = Estribo.NoRamasH2;
                CapasY2 = Estribo.NoRamasH1;
            }

            DeltaX1 = (B - 2 * r) / (CapasX1 - 1); //Separacion horizontal del refuerzo en la aleta
            DeltaY1 = (TW - 2 * r) / (CapasY2 - 1); //Separacion vertical del refuerzo en la aleta
            DeltaX2 = (TF - 2 * r) / (CapasX2 - 1); //Separacion horizontal del refuerzo en el alma
            DeltaY2 = ((H - TW) - 2 * r) / (CapasY1 - 1); //Separacion vertical del refuerzo en el alma

            Barras_Aleta = 2 * CapasY2 + 2 * (CapasX1 - 2);
            Barras_alma = 2 * CapasY1 + 2 * (CapasX2 - 2);

            var Xunicos = CoordenadasSeccion.Select(x => x[0]).Distinct().ToList();
            var Yunicos = CoordenadasSeccion.Select(x => x[1]).Distinct().ToList();

            var Xtf = Xunicos.Find(x => x != Xunicos.Min() & x != Xunicos.Max());
            var Ytw = Yunicos.Find(y => y != Yunicos.Min() & y != Yunicos.Max());

            #region Barras Alteta

            posx = Xunicos.Min() + r;

            if (Math.Round(Ytw + TW, 2) == Math.Round(Yunicos.Max(), 2))
                posy = Yunicos.Max() - r;
            else
                posy = Yunicos.Min() + TW - r;

            ContX = CapasX1; ContY = CapasY2 - 2;
            ContT = 0;
            id = 1;

            for (int i = 0; i < Barras_Aleta; i++)
            {
                Coord_ref[0] = posx;
                Coord_ref[1] = posy;

                refuerzoi = new CRefuerzo(id, "#" + Refuerzos_temp[ContT], new double[] { posx * 100, posy * 100 }, ptipo: TipodeRefuerzo.longitudinal);
                Refuerzos.Add(refuerzoi);

                posx += DeltaX1;
                ContX--;

                if (ContX == 0 & ContY > 0)
                {
                    posx = Xunicos.Min() + r;
                    posy -= DeltaY1;
                    ContX = 2;
                    DeltaX1 = (B - 2 * r) / (CapasX1 - 1);
                    ContY--;
                }

                if (ContX == 0 & ContY == 0)
                {
                    ContX = CapasX1;
                    DeltaX1 = (B - 2 * r) / (CapasX1 - 1);
                    posx = Xunicos.Min() + r;
                    posy -= DeltaY1;
                }

                id++;
                ContT++;
            }

            #endregion Barras Alteta

            #region Barras alma

            ContX = CapasX2 - 2; ContY = CapasY1;

            if (Shape == TipodeSeccion.L)
            {
                if (Math.Round(Xtf + TF, 2) == Math.Round(Xunicos.Max(), 2))
                {
                    posx = Xtf + r;
                }
                else
                {
                    posx = Xunicos.Min() + r;
                }
            }
            else
                posx = Xtf + r;

            if (Math.Round(Ytw + TW, 2) == Math.Round(Yunicos.Max(), 2))
                posy = Yunicos.Max() - TW - r;
            else
                posy = Yunicos.Max() - r;

            for (int i = 0; i < Barras_alma; i++)
            {
                Coord_ref[0] = posx;
                Coord_ref[1] = posy;

                refuerzoi = new CRefuerzo(id, "#" + Refuerzos_temp[ContT], new double[] { posx * 100, posy * 100 }, ptipo: TipodeRefuerzo.longitudinal);
                Refuerzos.Add(refuerzoi);

                posy -= DeltaY2;
                ContY--;

                if (ContY == 0 & ContX > 0)
                {
                    posx += DeltaX2;
                    if (Math.Round(Ytw + TW, 2) == Math.Round(Yunicos.Max(), 2))
                        posy = Yunicos.Max() - TW - r;
                    else
                        posy = Yunicos.Max() - r;
                    ContY = 2;
                    DeltaY2 = ((H - TW) - 2 * r) / (ContY - 1);
                    ContX--;
                }

                if (ContX == 0 & ContY == 0)
                {
                    ContY = CapasY1;
                    DeltaY2 = ((H - TW) - 2 * r) / (ContY - 1);
                    posx += DeltaX2;

                    if (Math.Round(Ytw + TW, 2) == Math.Round(Yunicos.Max(), 2))
                        posy = Yunicos.Max() - TW - r;
                    else
                        posy = Yunicos.Max() - r;
                }

                id++;
                ContT++;
            }

            #endregion Barras alma
        }

        public override string ToString()
        {
            string Nombre_seccion;
            Nombre_seccion = $"C{B * 100}X{H * 100}X{TW * 100}X{TF * 100}{Shape}{Material.Name}";
            return string.Format("{0}", Nombre_seccion);
        }

        public override bool Equals(object obj)
        {
            if (obj is CSD)
            {
                CSD temp = (CSD)obj;

                if (temp.B == B & temp.H == H & Material == temp.Material & temp.TF == TF & temp.TW == TW || temp.H == B & temp.B == H & Material == temp.Material & temp.TF == TW & temp.TW == TF)
                    return true;
            }

            return false;
        }

        public int CompareTo(object obj)
        {
            if (obj is CSD)
            {
                CSD temp = (CSD)obj;
                if (Area > temp.Area) return 1;
                if (Area < temp.Area) return -1;
            }
            return 0;
        }

        public double Peso_Estribo(Estribo pEstribo, float recubrimiento)
        {
            double PAuxiliar = 0;
            double Long_Estibo1 = 0;
            double Long_Estibo2 = 0;
            double Long_GanchoV1 = 0;
            double Long_GanchoV2 = 0;
            double Long_GanchoH1 = 0;
            double Long_GanchoH2 = 0;
            int Numero_Estribos = 0;

            Long_Estibo1 = 2 * (B - 2 * recubrimiento) + 2 * (TF - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 135); //Estribo aleta
            Long_Estibo2 = 2 * (TW - 2 * recubrimiento) + 2 * (H - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 135); //Estribo alma

            Long_GanchoH1 = (B - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180); //Aleta
            Long_GanchoH2 = (TW - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180); //Alma

            Long_GanchoV1 = (H - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180); //Aleta
            Long_GanchoV2 = (TF - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180); //Alma

            Numero_Estribos = Convert.ToInt32(Math.Round((100) / pEstribo.Separacion, 0) + 1);

            PAuxiliar = (Long_Estibo1 + Long_Estibo2 + (pEstribo.NoRamasH1 - 2) * Long_GanchoH1 + (pEstribo.NoRamasH2 - 2) * Long_GanchoH2 + (pEstribo.NoRamasV1 - 2) * Long_GanchoV1
                + (pEstribo.NoRamasV2 - 2) * Long_GanchoV2) * pEstribo.Area * 7850 * Numero_Estribos;

            return PAuxiliar;
        }

        public void Refuerzo_Base(double recub)
        {
            int Num_Barras = 0;
            int Barra_aux = 0;
            int Diametro1 = 0;
            double As_min;
            double As_i;
            double p_error;
            double Relacion_dim = 0;
            int CapasX1 = 0;
            int CapasX2 = 0;
            int CapasY1 = 0;
            int CapasY2 = 0;

            Num_Barras = Estribo.NoRamasH1 * 2 + 2 * (Estribo.NoRamasV1 - 2) + Estribo.NoRamasH2 * 2 + 2 * (Estribo.NoRamasV2 - 2);
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

            Diametro1 = Barra_aux;

            int[] Aux_Refuerzos = new int[Num_Barras];
            int Aux_num_barras = Num_Barras;

            for (int i = 0; i < Aux_Refuerzos.Count(); i++)
            {
                Aux_Refuerzos[i] = Diametro1;
            }

            if (Num_Barras != Estribo.NoRamasH1 * 2 + 2 * (Estribo.NoRamasV1 - 2) + Estribo.NoRamasH2 * 2 + 2 * (Estribo.NoRamasV2 - 2))
            {
                CapasX1 = Estribo.NoRamasV1;
                CapasY2 = Estribo.NoRamasH2;

                if (B > (H - TF))
                    Relacion_dim = B / (H - TF);
                else
                    Relacion_dim = (H - TF) / B;

                if (Relacion_dim < 1)
                {
                    CapasY1 = Convert.ToInt32(0.5 * Relacion_dim * (Num_Barras - 2 * (CapasX1 - CapasY2)) / 2);
                    CapasX2 = 2 + (Num_Barras - 2 * CapasY1 - 2 * (CapasX1 - 2) - 2 * CapasY2) / 2;
                }
                else
                {
                    CapasX2 = Convert.ToInt32(0.5 * Relacion_dim * (Num_Barras - 2 * (CapasX1 - CapasY2)) / 2);
                    CapasY1 = (Num_Barras - 2 * (CapasX1 - 2) - 2 * CapasY2 - 2 * (CapasX2 - 2)) / 2;
                }

                Estribo.NoRamasV1 = CapasX1;
                Estribo.NoRamasH1 = CapasY1;
                Estribo.NoRamasV2 = CapasX2;
                Estribo.NoRamasH2 = CapasY2;
            }

            Set_Refuerzo_Seccion(Aux_Refuerzos, recub);
        }

        #region Metodos y Propiedades: Cantidades

        public Tuple<float, float, float, string> EstriboAleta_Dimensiones_B_H_G_Nomenclatura { get; set; }
        public Tuple<float, float, float, string> EstriboAlma_Dimensiones_B_H_G_Nomenclatura { get; set; }

        public Tuple<int, float, float, string> GanchoHAleta_Dim_Cant_Ltotal_G_Nomenclatura { get; set; }
        public Tuple<int, float, float, string> GanchoVAleta_Dim_Cant_Ltotal_G_Nomenclatura { get; set; }

        public Tuple<int, float, float, string> GanchoHAlma_Dim_Cant_Ltotal_G_Nomenclatura { get; set; }
        public Tuple<int, float, float, string> GanchoVAlma_Dim_Cant_Ltotal_G_Nomenclatura { get; set; }

        public void CalcularDimensionesEstribo_Gancho(Estribo estribo, float R)
        {
            float B_Estribo;
            float H_Estribo;
            float G135_Estribo = Form1.Proyecto_.G135[estribo.NoEstribo];
            float Gancho180_G = Form1.Proyecto_.G180[estribo.NoEstribo];

            int CantEstribosVAleta;
            int CantEstribosVAlma;
            int CantEstribosHAleta;
            int CantEstribosHAlma;

            float Long_GanchoHAleta;
            float Long_GanchoVAleta;
            float Long_GanchoHAlma;
            float Long_GanchoVAlma;

            string Conve_Estribo;
            string Conve_Gancho;

            //------------ALETA -----------------
            //Estribo
            B_Estribo = (B - 2 * R);
            H_Estribo = TW - 2 * R;
            Conve_Estribo = $" E  #{estribo.NoEstribo}  {B_Estribo}*{H_Estribo}  G{G135_Estribo}  F0/45";
            EstriboAleta_Dimensiones_B_H_G_Nomenclatura = new Tuple<float, float, float, string>(B_Estribo, H_Estribo, G135_Estribo, Conve_Estribo);

            //Gancho Vertical
            CantEstribosVAleta = estribo.NoRamasV1 - 2;
            Long_GanchoVAleta = TW - 2 * R + 2 * Gancho180_G;
            Conve_Gancho = $" #{estribo.NoEstribo}  {Long_GanchoVAleta}  U{Gancho180_G}  U{Gancho180_G}";
            GanchoVAleta_Dim_Cant_Ltotal_G_Nomenclatura = new Tuple<int, float, float, string>(CantEstribosVAleta, Long_GanchoVAleta, Gancho180_G, Conve_Gancho);

            //Gancho Horizontal
            CantEstribosHAleta = estribo.NoRamasH1 - 2;
            Long_GanchoHAleta = B - 2 * R + 2 * Gancho180_G;
            Conve_Gancho = $" #{estribo.NoEstribo}  {Long_GanchoVAleta}  U{Gancho180_G}  U{Gancho180_G}";
            GanchoHAleta_Dim_Cant_Ltotal_G_Nomenclatura = new Tuple<int, float, float, string>(CantEstribosHAleta, Long_GanchoHAleta, Gancho180_G, Conve_Gancho);

            //------------ALMA -----------------

            //Estribo
            B_Estribo = (TF - 2 * R);
            H_Estribo = H - 2 * R;
            Conve_Estribo = $" E  #{estribo.NoEstribo}  {B_Estribo}*{H_Estribo}  G{G135_Estribo}  F0/45";
            EstriboAlma_Dimensiones_B_H_G_Nomenclatura = new Tuple<float, float, float, string>(B_Estribo, H_Estribo, G135_Estribo, Conve_Estribo);

            //Gancho Vertical
            CantEstribosVAlma = estribo.NoRamasV2 - 2;
            Long_GanchoVAlma = H - 2 * R + 2 * Gancho180_G;
            Conve_Gancho = $" #{estribo.NoEstribo}  {Long_GanchoVAlma}  U{Gancho180_G}  U{Gancho180_G}";
            GanchoVAlma_Dim_Cant_Ltotal_G_Nomenclatura = new Tuple<int, float, float, string>(CantEstribosVAlma, Long_GanchoVAlma, Gancho180_G, Conve_Gancho);

            //Gancho Horizontal
            CantEstribosHAlma = estribo.NoRamasH2 - 2;
            Long_GanchoHAlma = TF - 2 * R + 2 * Gancho180_G;
            Conve_Gancho = $" #{estribo.NoEstribo}  {Long_GanchoHAlma}  U{Gancho180_G}  U{Gancho180_G}";
            GanchoHAlma_Dim_Cant_Ltotal_G_Nomenclatura = new Tuple<int, float, float, string>(CantEstribosHAlma, Long_GanchoHAlma, Gancho180_G, Conve_Gancho);
        }

        #endregion Metodos y Propiedades: Cantidades

        public void Dibujo_Autocad(double Xi, double Yi, int Num_Alzado)
        {
            string LayerCuadro = "FC_BORDES";
            double[] P_XYZ = { };
            List<double> Vertices = new List<double>();

            var Escalar = Operaciones.Escalar(0.50, FunctionsProject.DeepClone(CoordenadasSeccion));

            for (int i = 0; i < Escalar.Count; i++)
            {
                var Aux = Operaciones.Traslacion(Escalar[i][0] + Xi, Escalar[i][1] + Yi - TW, Escalar[i][0], Escalar[i][1]);
                Vertices.Add(Aux[0]);
                Vertices.Add(Aux[1]);
            }

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vertices.ToArray(), LayerCuadro, true);
            var X_unicos = Refuerzos.Select(x => Math.Round(x.Coord[0], 2)).ToList().Distinct().ToList();
            var Y_unicos = Refuerzos.Select(x => Math.Round(x.Coord[1], 2)).ToList().Distinct().ToList();

            foreach (CRefuerzo cRefuerzo in Refuerzos)
            {
                cRefuerzo.Dibujo_Ref_Autocad(Xi + B / 2, Yi - H / 2, X_unicos.Max(), X_unicos.Min(), Y_unicos.Max(), Y_unicos.Min());
            }
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

        public static bool operator ==(CSD s1, CSD s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(CSD s1, CSD s2)
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

        public static bool operator <(CSD s1, CSD s2)
        {
            if (s1.CompareTo(s2) < 0)
                return true;
            else
                return false;
        }

        public static bool operator >(CSD s1, CSD s2)
        {
            if (s1.CompareTo(s2) > 0)
                return true;
            else
                return false;
        }
    }
}