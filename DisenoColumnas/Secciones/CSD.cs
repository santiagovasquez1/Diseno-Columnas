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
        public ConcreteSections Type { get; set; }
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

        public List<Tuple<ISeccion, string>> SeccionesVecinosCambios { get; set; } = new List<Tuple<ISeccion, string>>();
       

        #region Propiedades y Metodos para verificación de Vc

        public List<float[]> PM2M3V2V3 { get; set; }
        public List<float> Vcx { get; set; }
        public List<float> Vcy { get; set; }
        public List<float> Vsx { get; set; }
        public List<float> Vsy { get; set; }

        #endregion Propiedades y Metodos para verificación de Vc

        #region Diagrama de Interacción: Propiedades y Metodos

        public List<Tuple<List<float[]>, int>> MnPn3D { get; set; }
        public List<Tuple<List<float[]>, int>> PnMn2D { get; set; }

        public List<Tuple<List<float[]>, int>> MuPu3D { get; set; }
        public List<Tuple<List<float[]>, int>> PuMu2D { get; set; }

        public List<Tuple<List<float[]>, int>> PnMn2D_v1 { get; set; }

        public Tuple<List<float[]>, List<float[]>> DiagramaInteraccionParaUnAngulo(int Angulo, bool MPUiltimos)
        {
            List<PointF> Coordenadas = new List<PointF>();
            Coordenadas = CoordenadasSeccion.ConvertAll(new Converter<float[], PointF>(FunctionsProject.CoversionaPuntos));
            float ecu = 0.003f;
            float Fy = 4220f;
            float r = 4;
            if (Form1.Proyecto_ != null)
            {
                Fy = Form1.Proyecto_.FY;
                r = Form1.Proyecto_.R;
            }

            float fc = Material.FC;
            float Es = 2000000;
            float b1 = 0.85f - 0.05f * (fc - 280) / 70f;

            List<float> m_PorCadaAngulo = new List<float>();
            List<float> YVariacionC = new List<float>();
            List<float> YVariaciona = new List<float>();

            List<float> AreaComprimida = new List<float>();
            List<float[]> CentroideAreaComprimida = new List<float[]>();

            List<float[]> Coordenadas_Angulo = new List<float[]>();

            foreach (CRefuerzo cRefuerzo in Refuerzos)
            {
                cRefuerzo.Coordenadas_Angulo = new float[] { };
                cRefuerzo.Esfuerzos_Angulo = new List<float>();
                cRefuerzo.Momento_Angulo = new List<float>();
                cRefuerzo.Fuerzas_Angulo = new List<float>();
                cRefuerzo.Deformacion_Angulo = new List<float>();
            }

            //Rotacion de la Sección
            for (int i = 0; i < Coordenadas.Count; i++)
            {
                List<double> CoordRotadas = Operaciones.Rotacion(Coordenadas[i].X, Coordenadas[i].Y, (Angulo * Math.PI) / 180);
                Coordenadas_Angulo.Add(new float[] { (float)CoordRotadas[0] * 100, (float)CoordRotadas[1] * 100 });
            }

            float[] CentroideFigura2 = FunctionsProject.DeterminarCentroideSentidoAntiHorario(Coordenadas_Angulo);
            float Xmax = Coordenadas_Angulo.Max(x => x[0]);
            float Xmin = Coordenadas_Angulo.Min(x => x[0]);
            float ymax = Coordenadas_Angulo.Max(x => x[1]);
            float ymin = Coordenadas_Angulo.Min(x => x[1]);
            List<float> ms = new List<float>();

            for (int i = 0; i < Coordenadas_Angulo.Count; i++)
            {
                float m;
                if (i + 1 == Coordenadas_Angulo.Count)
                {
                    m = (Coordenadas_Angulo[i][1] - Coordenadas_Angulo[0][1]) / (Coordenadas_Angulo[i][0] - Coordenadas_Angulo[0][0]);
                }
                else
                {
                    m = (Coordenadas_Angulo[i][1] - Coordenadas_Angulo[i + 1][1]) / (Coordenadas_Angulo[i][0] - Coordenadas_Angulo[i + 1][0]);
                }

                ms.Add(m);
            }
            foreach (CRefuerzo cRefuerzo in Refuerzos)
            {
                List<double> CoordRotadas = Operaciones.Rotacion(cRefuerzo.Coord[0], cRefuerzo.Coord[1], (Angulo * Math.PI) / 180);
                cRefuerzo.Coordenadas_Angulo = new float[] { (float)CoordRotadas[0], (float)CoordRotadas[1] };
            }

            int DeltasVariacionC = 20;

            for (float C = ymin / b1; C <= ymax; C += (ymax - ymin) / DeltasVariacionC)
            {
                YVariaciona.Add(C + (ymax - C) - (ymax - C) * b1);
                YVariacionC.Add(C);
            }

            foreach (CRefuerzo cRefuerzo in Refuerzos)
            {
                for (int i = 0; i < YVariacionC.Count; i++)
                {
                    float C_Original = ymax - YVariacionC[i];
                    float d = ymax - cRefuerzo.Coordenadas_Angulo[1];
                    float esi = ((C_Original - d) / C_Original) * ecu;

                    float fs = Es * esi;

                    if (Math.Abs(fs) > Fy)
                    {
                        if (esi < 0)
                        {
                            fs = -Fy;
                        }
                        else
                        {
                            fs = Fy;
                        }
                    }

                    float FS;
                    float Mi;

                    FS = fs * (float)cRefuerzo.As_Long;
                    Mi = Math.Abs(FS) * Math.Abs(cRefuerzo.Coordenadas_Angulo[1]);

                    cRefuerzo.Fuerzas_Angulo.Add(FS);
                    cRefuerzo.Momento_Angulo.Add(Mi);
                    cRefuerzo.Esfuerzos_Angulo.Add(fs);
                    cRefuerzo.Deformacion_Angulo.Add(esi);
                }
            }

            for (int i = 0; i < YVariaciona.Count; i++)
            {
                List<float[]> PuntosInter = DeterminarPuntosInterceptos(ms, Coordenadas_Angulo, YVariaciona[i]);

                float AreaComprimida_Aux = FunctionsProject.DeterminarArea(PuntosInter);

                AreaComprimida.Add(AreaComprimida_Aux);

                CentroideAreaComprimida.Add(FunctionsProject.DeterminarCentroideSentidoAntiHorario(PuntosInter));
            }

            List<float[]> MP = new List<float[]>();

            for (int i = 0; i < AreaComprimida.Count; i++)
            {
                float Cc = 0.85f * fc * AreaComprimida[i];
                float Fs = 0; float Ms = 0;
                float Ast = 0;

                foreach (CRefuerzo cRefuerzo in Refuerzos)
                {
                    Fs += cRefuerzo.Fuerzas_Angulo[i];
                    Ms += Math.Abs(cRefuerzo.Fuerzas_Angulo[i]) * Math.Abs(cRefuerzo.Coordenadas_Angulo[1] - CentroideFigura2[1]);
                    Ast += (float)FunctionsProject.Find_As(Convert.ToInt32(cRefuerzo.Diametro.Substring(1))) * 10000;
                }


                float Mnx = Cc * (CentroideAreaComprimida[i][0] - CentroideFigura2[0]);
                float Mny = Cc * (CentroideAreaComprimida[i][1] - CentroideFigura2[1]) + Ms;

                float Mn_ = (float)Math.Sqrt(Math.Pow(Mnx, 2) + Math.Pow(Mny, 2));
                float Pn_ = Cc + Fs;

                float minY = Refuerzos.Min(x => x.Coordenadas_Angulo[1]);
                float esi = Refuerzos.Find(x => x.Coordenadas_Angulo[1] == minY).Deformacion_Angulo[i];
                float et = Math.Abs(esi);

                float fi = 1;
                if (MPUiltimos)
                {
                    fi = DeterminarFi(et);
                }
                Mn_ = Mn_ * fi;
                Pn_ = Pn_ * fi;
                float Pmax = fi * 0.75f * (0.85f * fc * ((float)Area * 10000 - Ast) + Fy * Ast);

                if (Pn_ > Pmax)
                {
                    Pn_ = Pmax;
                }

                if (Pn_ < 0)
                {
                    if (MP.Exists(x => x[0] == 0) == false)
                    {
                        MP.Add(new float[] { 0, (-Ast * Fy) });
                    }
                }
                else
                {
                    MP.Add(new float[] { Mn_, Pn_ });
                }

                if (i == AreaComprimida.Count - 1)
                {
                    float Pmax1 = MP.Max(x => x[1]);
                    int IndicePmax1 = MP.FindIndex(x => x[1] == Pmax1);

                    MP.Insert(IndicePmax1, new float[] { 0, Pmax1 });
                }
            }

            List<float[]> MP3D = new List<float[]>();

            for (int i = 0; i < MP.Count; i++)
            {
                float X1 = (float)(MP[i][0] * Math.Cos((Angulo * Math.PI / 180)));
                float Y2 = (float)(MP[i][0] * Math.Sin((Angulo * Math.PI / 180)));
                float Z2 = MP[i][1];
                float[] PuntosDescompuestos = new float[] { X1, Y2, Z2 };
                MP3D.Add(PuntosDescompuestos);
            }

            return new Tuple<List<float[]>, List<float[]>>(MP, MP3D);
        }

        public void DiagramaInteraccion()
        {
            int Delta = 10;
            PnMn2D = new List<Tuple<List<float[]>, int>>();
            PuMu2D = new List<Tuple<List<float[]>, int>>();
            MnPn3D = new List<Tuple<List<float[]>, int>>();
            MuPu3D = new List<Tuple<List<float[]>, int>>();
            for (int Angulo = 0; Angulo <= 360; Angulo += Delta)
            {
                Tuple<List<float[]>, List<float[]>> ResultadosNominales = DiagramaInteraccionParaUnAngulo(Angulo, false);
                List<float[]> PnMn2D_ = ResultadosNominales.Item1;
                List<float[]> MnPn3D_ = ResultadosNominales.Item2;

                PnMn2D.Add(new Tuple<List<float[]>, int>(PnMn2D_, Angulo));
                MnPn3D.Add(new Tuple<List<float[]>, int>(MnPn3D_, Angulo));

                Tuple<List<float[]>, List<float[]>> ResultadosUltimos = DiagramaInteraccionParaUnAngulo(Angulo, true);
                List<float[]> PuMu2D_ = ResultadosUltimos.Item1;
                List<float[]> MuPu3D_ = ResultadosUltimos.Item2;
                PuMu2D.Add(new Tuple<List<float[]>, int>(PuMu2D_, Angulo));
                MuPu3D.Add(new Tuple<List<float[]>, int>(MuPu3D_, Angulo));
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

        private List<float[]> DeterminarPuntosInterceptos(List<float> m, List<float[]> XY, float Yperteneciente)
        {
            List<float[]> PuntosHallados = new List<float[]>();
            List<PointF[]> PuntosPendie = new List<PointF[]>();

            for (int i = 0; i < m.Count; i++)
            {
                PointF[] points;
                if (i + 1 == m.Count)
                {
                    points = new PointF[] { new PointF(XY[i][0], XY[i][1]), new PointF(XY[0][0], XY[0][1]) };
                }
                else
                {
                    points = new PointF[] { new PointF(XY[i][0], XY[i][1]), new PointF(XY[i + 1][0], XY[i + 1][1]) };
                }
                PuntosPendie.Add(points);
            }

            for (int i = 0; i < PuntosPendie.Count; i++)
            {
                float[] Punto = (PuntoIntercepto(PuntosPendie[i], m[i], Yperteneciente));

                if (i == 0)
                {
                    if (Punto.Length != 0)
                    {
                        PuntosHallados.Add(Punto);
                    }
                    else
                    {
                        if (PuntosPendie[i][0].Y > Yperteneciente)
                        {
                            if (FunctionsProject.Find_Coord(PuntosHallados, PuntosPendie[i][0].X, PuntosPendie[i][0].Y) == false)
                            {
                                PuntosHallados.Add(new float[] { PuntosPendie[i][0].X, PuntosPendie[i][0].Y });
                            }
                        }
                        if (PuntosPendie[i][1].Y > Yperteneciente)
                        {
                            if (FunctionsProject.Find_Coord(PuntosHallados, PuntosPendie[i][1].X, PuntosPendie[i][1].Y) == false)
                            {
                                PuntosHallados.Add(new float[] { PuntosPendie[i][1].X, PuntosPendie[i][1].Y });
                            }
                        }
                    }
                }
                else
                {
                    if (Punto.Length != 0)
                    {
                        PuntosHallados.Add(Punto);
                    }

                    if (PuntosPendie[i][0].Y > Yperteneciente)
                    {
                        if (FunctionsProject.Find_Coord(PuntosHallados, PuntosPendie[i][0].X, PuntosPendie[i][0].Y) == false)
                        {
                            PuntosHallados.Add(new float[] { PuntosPendie[i][0].X, PuntosPendie[i][0].Y });
                        }
                    }
                    if (PuntosPendie[i][1].Y > Yperteneciente)
                    {
                        if (FunctionsProject.Find_Coord(PuntosHallados, PuntosPendie[i][1].X, PuntosPendie[i][1].Y) == false)
                        {
                            PuntosHallados.Add(new float[] { PuntosPendie[i][1].X, PuntosPendie[i][1].Y });
                        }
                    }
                }



            }

            return PuntosHallados;
        }

        private float[] PuntoIntercepto(PointF[] Puntos, float m, float Yperteneciente)
        {
            float[] XY = new float[] { };
            float X = ((Yperteneciente - Puntos[0].Y) / m) + Puntos[0].X;
            float MaxY = Puntos.Max(Z => Z.Y);
            float MinY = Puntos.Min(Z => Z.Y);
            float Xmin = Puntos.Min(Z => Z.X);
            float Xmax = Puntos.Max(Z => Z.X);

            if (X >= Xmin && X <= Xmax && Yperteneciente >= MinY && Yperteneciente <= MaxY)
            {
                XY = new float[] { X, Yperteneciente };
            }

            return XY;
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
                    int RamasMenos = (int)Math.Round(Math.Round(Ash / Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));
                    int RamasMas = (int)Math.Ceiling(Math.Round(Ash / Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));

                    Estribo.NoRamasV1 = CalcularRamasDefinitivas((float)Ash, (float)Estribo.Area * RamasMenos, RamasMenos, RamasMas);

                    RamasMenos = (int)Math.Round(Asht / Estribo.Area < 2 ? 2 : (float)Math.Round(Asht / Estribo.Area, 2), 2);
                    RamasMas = (int)Math.Ceiling(Math.Round(Asht / Estribo.Area < 2 ? 2 : (float)Math.Round(Asht / Estribo.Area, 2), 2));

                    Estribo.NoRamasV2 = CalcularRamasDefinitivas((float)Asht, (float)Estribo.Area * RamasMenos, RamasMenos, RamasMas);
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
                    int RamasMenos = (int)Math.Round(Math.Round(Ash / Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));
                    int RamasMas = (int)Math.Ceiling(Math.Round(Ash / Estribo.Area < 2 ? 2 : (float)Math.Round(Ash / Estribo.Area, 2), 2));
                    Estribo.NoRamasH1 = CalcularRamasDefinitivas((float)Ash, (float)Estribo.Area * RamasMenos, RamasMenos, RamasMas);

                    RamasMenos = (int)Math.Round(Asht / Estribo.Area < 2 ? 2 : (float)Math.Round(Asht / Estribo.Area, 2), 2);
                    RamasMas = (int)Math.Ceiling(Math.Round(Asht / Estribo.Area < 2 ? 2 : (float)Math.Round(Asht / Estribo.Area, 2), 2));

                    Estribo.NoRamasH2 = CalcularRamasDefinitivas((float)Asht, (float)Estribo.Area * RamasMenos, RamasMenos, RamasMas);
                }
                else
                {
                    Estribo.NoRamasH1 = 0;
                    Estribo.NoRamasH2 = 0;
                }
            }
        }

        private int CalcularRamasDefinitivas(float AshNecesario, float AsMenos, int RamasMenor, int RamasMayor)
        {
            float Porcentaje = AsMenos / AshNecesario;

            if (Porcentaje >= 0.95f)
            {
                return RamasMenor;

            }
            else
            {
                return RamasMayor;
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

            var Num_Ramas_V = new List<int>();  //Numero de ramas en altura del muro para ambos casos de ast
            var GT_As1 = new List<double>();   //Longitud total de los gancho para As1, bajo cada una de las variaciones de la separacion
            var GT_As2 = new List<double>();   //Longitud total de los gancho para As2, bajo cada una de las variaciones de la separacion
            var GTw_As1 = new List<double>();  //Longitud total de los gancho para As1, bajo cada una de las variaciones de la separacion
            var GTw_As2 = new List<double>();  //Longitud total de los gancho para As2, bajo cada una de las variaciones de la separacion

            var P_As1 = new List<double>();    //'Peso total As1
            var P_As2 = new List<double>();    //'Peso total As2

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
                P_As1.Add(Peso_Estribo(Estribo, r,1));

                #endregion Estribo #3

                #region Estribo #4

                Estribo = new Estribo(4) //Estribo temporal
                {
                    Separacion = Convert.ToSingle(s_d)
                };

                Cuanti_Vol(FD1, FD2, r, FY);
                P_As2.Add(Peso_Estribo(Estribo, r,1));

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

        public void Add_Ref_graph(double EscalaX, double EscalaY, double EscalaR, float Dx, float Dy)
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
                r = FunctionsProject.Find_Diametro(Convert.ToInt32(refuerzoi.Diametro.Substring(1))) / 2;
                r = r * EscalaR;

                xc = Dx + refuerzoi.Coord[0] * EscalaX;
                yc = Dy - refuerzoi.Coord[1] * EscalaY;
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

        public GraphicsPath Add_Estribos(double EscalaX, double EscalaY, float rec, float Dx, float Dy)
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
            D_off2 = rec + (float)FunctionsProject.Find_Diametro(Estribo.NoEstribo) / 100;

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

                if (Math.Round(Xtf + TF, 2) == Math.Round(Xunicos.Max(), 2) & FunctionsProject.Find_Coord(CoordenadasSeccion, Xunicos.Min(), Yunicos.Max()) == false)
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

            var Coord_aletas1 = Operaciones.OffSet(D_off1, FunctionsProject.DeepClone(Coord_aletas), false);
            var Coord_aletas2 = Operaciones.OffSet(D_off2, FunctionsProject.DeepClone(Coord_aletas), false);
            var Coord_alma1 = Operaciones.OffSet(D_off1, FunctionsProject.DeepClone(Coord_alma), false);
            var Coord_alma2 = Operaciones.OffSet(D_off2, FunctionsProject.DeepClone(Coord_alma), false);

            #region Dibujo aleta

            for (int i = 0; i < Coord_aletas1.Count; i++)
            {
                Vertices.Add(new PointF(Dx + Coord_aletas1[i][0] * (float)EscalaX * 100, Dy - Coord_aletas1[i][1] * (float)EscalaX * 100));
                Vertices2.Add(new PointF(Dx + Coord_aletas2[i][0] * (float)EscalaX * 100, Dy - Coord_aletas2[i][1] * (float)EscalaX * 100));
            }

            path.AddPolygon(Vertices.ToArray());
            path.AddPolygon(Vertices2.ToArray());

            #endregion Dibujo aleta

            #region Dibujo alma

            Vertices.Clear();
            Vertices2.Clear();

            for (int i = 0; i < Coord_alma1.Count; i++)
            {
                Vertices.Add(new PointF(Dx + Coord_alma1[i][0] * (float)EscalaX * 100, Dy - Coord_alma1[i][1] * (float)EscalaX * 100));
                Vertices2.Add(new PointF(Dx + Coord_alma2[i][0] * (float)EscalaX * 100, Dy - Coord_alma2[i][1] * (float)EscalaX * 100));
            }
            path.AddPolygon(Vertices.ToArray());
            path.AddPolygon(Vertices2.ToArray());

            #endregion Dibujo alma

            return path;
        }

        public void Dibujo_Seccion(Graphics g, double EscalaX, double EscalaY, bool seleccion, float Dx, float Dy)
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
                Vertices.Add(new PointF(Dx + CoordenadasSeccion[i][0] * 100 * (float)EscalaX, Dy - CoordenadasSeccion[i][1] * 100 * (float)EscalaY));
            }

            #endregion Vertices

            g.DrawPolygon(P1, Vertices.ToArray());
            g.FillPolygon(br, Vertices.ToArray());
            Seccion_path.AddPolygon(Vertices.ToArray());
        }
        public void Dibujo_SeccionVecina(Graphics g, double EscalaX, double EscalaY, float Dx, float Dy) 
        {
            SolidBrush br = new SolidBrush(Color.FromArgb(150, Color.Gray));
            Pen P1;
            Seccion_path = new GraphicsPath();
            Vertices = new List<PointF>();
            P1 = new Pen(Color.Black, 3f)
            {
                Brush = Brushes.DarkGray,
                Color = Color.DarkBlue,
                DashStyle = DashStyle.Dash,
                LineJoin = LineJoin.Round,
                Alignment = PenAlignment.Center
            };

            #region Vertices

            for (int i = 0; i < CoordenadasSeccion.Count; i++)
            {
                Vertices.Add(new PointF(Dx + CoordenadasSeccion[i][0] * 100 * (float)EscalaX, Dy - CoordenadasSeccion[i][1] * 100 * (float)EscalaY));
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
                if (Math.Round(Xtf + TF, 2) == Math.Round(Xunicos.Max(), 2) & FunctionsProject.Find_Coord(CoordenadasSeccion, Xunicos.Min(), Xunicos.Max()) == true)
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

        public double Peso_Estribo(Estribo pEstribo, float recubrimiento, int Cantidad)
        {
            double PAuxiliar = 0;
            double Long_Estibo1 = 0;
            double Long_Estibo2 = 0;
            double Long_GanchoV1 = 0;
            double Long_GanchoV2 = 0;
            double Long_GanchoH1 = 0;
            double Long_GanchoH2 = 0;
           // int Numero_Estribos = 0;

            Long_Estibo1 = 2 * (B - 2 * recubrimiento) + 2 * (TF - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 135); //Estribo aleta
            Long_Estibo2 = 2 * (TW - 2 * recubrimiento) + 2 * (H - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 135); //Estribo alma

            Long_GanchoH1 = (B - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180); //Aleta
            Long_GanchoH2 = (TW - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180); //Alma

            Long_GanchoV1 = (H - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180); //Aleta
            Long_GanchoV2 = (TF - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180); //Alma
            float PesoEstribo = FunctionsProject.Find_MasaNominal(Estribo.NoEstribo);
            //Numero_Estribos = Convert.ToInt32(Math.Round((100) / pEstribo.Separacion, 0) + 1);

            PAuxiliar = (Long_Estibo1 + Long_Estibo2 + (pEstribo.NoRamasH1 - 2) * Long_GanchoH1 + (pEstribo.NoRamasH2 - 2) * Long_GanchoH2 + (pEstribo.NoRamasV1 - 2) * Long_GanchoV1
                + (pEstribo.NoRamasV2 - 2) * Long_GanchoV2) * Cantidad* PesoEstribo;

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
            string Nom_Seccion = "";
            string Escala = "1:15";

            var Escalar = Operaciones.Escalar(0.50, FunctionsProject.DeepClone(CoordenadasSeccion));

            for (int i = 0; i < Escalar.Count; i++)
            {
                var Aux = Operaciones.Traslacion(Escalar[i][0] + Xi, Escalar[i][1] + Yi - 0.60, Escalar[i][0], Escalar[i][1]);
                Vertices.Add(Aux[0]);
                Vertices.Add(Aux[1]);
            }

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vertices.ToArray(), LayerCuadro, true);
            var X_unicos = Refuerzos.Select(x => Math.Round(x.Coord[0], 2)).ToList().Distinct().ToList();
            var Y_unicos = Refuerzos.Select(x => Math.Round(x.Coord[1], 2)).ToList().Distinct().ToList();

            foreach (CRefuerzo cRefuerzo in Refuerzos)
            {
                cRefuerzo.Dibujo_Ref_Autocad(Xi, Yi - 0.60, X_unicos.Max(), X_unicos.Min(), Y_unicos.Max(), Y_unicos.Min());
            }

            #region Estribos y ramas

            //Estribos Aleta
            var Xmin = CoordenadasSeccion.Select(x => x[0]).Min();
            var Xmax = CoordenadasSeccion.Select(x => x[0]).Max();
            var Ymin = CoordenadasSeccion.Select(x => x[1]).Min();
            var Ymax = CoordenadasSeccion.Select(x => x[1]).Max();

            if (FunctionsProject.Find_Coord(CoordenadasSeccion, Xmax, Ymax) == true)
            {
                P_XYZ = new double[] { Xi + 0.15, Yi - 0.04, 0 };
            }
            else
            {
                P_XYZ = new double[] { Xi + 0.15, Yi - H - 0.04, 0 };
            }
            FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ, "FC_ESTRIBOS", B - 0.02 * Form1.Proyecto_.R, TF - 0.02 * Form1.Proyecto_.R, 1, 1, 1, 0);

            //Estribos Alma

            if (Shape == TipodeSeccion.L)
            {
                if (FunctionsProject.Find_Coord(CoordenadasSeccion, Xmin, Ymax) == true & FunctionsProject.Find_Coord(CoordenadasSeccion, Xmin + TW, Ymax) == true)
                {
                    P_XYZ = new double[] { Xi, Yi - 0.04, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ, "FC_ESTRIBOS", TW - 0.02 * Form1.Proyecto_.R, H - 0.02 * Form1.Proyecto_.R, 1, 1, 1, 0);
                }

                if (FunctionsProject.Find_Coord(CoordenadasSeccion, Xmax - TW, Ymax) == true & FunctionsProject.Find_Coord(CoordenadasSeccion, Xmax, Ymax) == true)
                {
                    P_XYZ = new double[] { Xi + Xmax - TW, Yi - 0.04, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ, "FC_ESTRIBOS", TW - 0.02 * Form1.Proyecto_.R, H - 0.02 * Form1.Proyecto_.R, 1, 1, 1, 0);
                }

                if (FunctionsProject.Find_Coord(CoordenadasSeccion, Xmin, Ymin) == true & FunctionsProject.Find_Coord(CoordenadasSeccion, Xmin + TW, Ymin) == true)
                {
                    P_XYZ = new double[] { Xi, Yi - 0.04, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ, "FC_ESTRIBOS", TW - 0.02 * Form1.Proyecto_.R, H - 0.02 * Form1.Proyecto_.R, 1, 1, 1, 0);
                }

                if (FunctionsProject.Find_Coord(CoordenadasSeccion, Xmax - TW, Ymin) == true & FunctionsProject.Find_Coord(CoordenadasSeccion, Xmax, Ymin) == true)
                {
                    P_XYZ = new double[] { Xi + Xmax - TW, Yi - 0.04, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ, "FC_ESTRIBOS", TW - 0.02 * Form1.Proyecto_.R, H - 0.02 * Form1.Proyecto_.R, 1, 1, 1, 0);
                }
            }
            else
            {
                var Punto1 = CoordenadasSeccion.FindAll(Y => Y[1] == Ymin);
                var Punto2 = CoordenadasSeccion.FindAll(Y => Y[1] == Ymax);

                var L1 = Math.Abs(Punto1[0][1] - Punto1[1][1]);
                var L2 = Math.Abs(Punto2[0][1] - Punto2[1][1]);

                if (L1 < L2)
                {
                    var Xtw = Math.Abs(Xi - Punto1[0][1]);
                    P_XYZ = new double[] { Xi + Xtw, Yi - 0.04, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ, "FC_ESTRIBOS", TW - 0.02 * Form1.Proyecto_.R, H - 0.02 * Form1.Proyecto_.R, 1, 1, 1, 0);
                }
                else
                {
                    var Xtw = Math.Abs(Xi - Punto2[0][1]);
                    P_XYZ = new double[] { Xi + Xtw, Yi - 0.04, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ, "FC_ESTRIBOS", TW - 0.02 * Form1.Proyecto_.R, H - 0.02 * Form1.Proyecto_.R, 1, 1, 1, 0);
                }
            }

            #endregion Estribos y ramas

            #region Nombre_Seccion

            Nom_Seccion = "%%USeccion " + Num_Alzado;

            FunctionsAutoCAD.FunctionsAutoCAD.B_NombreSeccion(P_XYZ: new double[] { Xi + (B / 2), Yi - H - 0.2, 0 }, Seccion: Nom_Seccion, Escala: Escala, Layer: "FC_R-200", Xscale: 15, Yscale: 15, Zscale: 15, Rotation: 0);

            #endregion Nombre_Seccion
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
                fInterfaz.Get_section(true);
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
