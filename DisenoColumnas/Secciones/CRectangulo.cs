using B_Operaciones_Matricialesl;
using DisenoColumnas.Clases;
using DisenoColumnas.Interfaz_Seccion;
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

        #region Propiedades y Metodos para verificación de Vc

        public List<float[]> PM2M3V2V3 { get; set; }
        public List<float> Vcx { get; set; }
        public List<float> Vcy { get; set; }
        public List<float> Vsx { get; set; }
        public List<float> Vsy { get; set; }

        #endregion Propiedades y Metodos para verificación de Vc

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


        public Tuple<List<float[]>,List<float[]>> DiagramaInteraccionParaUnAngulo(int Angulo, bool MPUiltimos)
        {

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
            float Fy = 4220f;
            if (Form1.Proyecto_ != null)
            {
                Fy = Form1.Proyecto_.FY;
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
                Coordenadas_Angulo.Add(new float[] { (float)CoordRotadas[0], (float)CoordRotadas[1] });
            }

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

            for (float C = ymin + (ymax - ymin) / DeltasVariacionC; C <= ymax; C += (ymax - ymin) / DeltasVariacionC)
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
                List<float[]> PuntosInter = HallarPuntosDeInter(ms, Coordenadas_Angulo, YVariaciona[i], Xmax, Xmin);

                List<float[]> PuntosPorEncimadeA = new List<float[]>();

                for (int j = 0; j < Coordenadas_Angulo.Count; j++)
                {
                    if (YVariaciona[i] <= Coordenadas_Angulo[j][1])
                    {
                        PuntosPorEncimadeA.Add(Coordenadas_Angulo[j]);
                    }
                }
                PuntosInter = PuntosInter.OrderByDescending(x => x[0]).ToList();
                PuntosPorEncimadeA = PuntosPorEncimadeA.OrderByDescending(x => x[0]).ToList();

                List<float[]> PuntosParaArea = new List<float[]>();

                PuntosParaArea.Add(PuntosInter[0]); PuntosParaArea.AddRange(PuntosPorEncimadeA); PuntosParaArea.Add(PuntosInter[1]);

                float AreaComprimida_Aux = FunctionsProject.DeterminarArea(PuntosParaArea);

                AreaComprimida.Add(AreaComprimida_Aux);

                CentroideAreaComprimida.Add(FunctionsProject.DeterminarCentroideSentidoAntiHorario(PuntosParaArea));
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
                    Ms += Math.Abs(cRefuerzo.Fuerzas_Angulo[i]) * Math.Abs(cRefuerzo.Coordenadas_Angulo[1]);
                    Ast += (float)FunctionsProject.Find_As(Convert.ToInt32(cRefuerzo.Diametro.Substring(1))) * 10000;
                }


                float Mn_ = Cc * (CentroideAreaComprimida[i][1]) + Ms;
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
                    MP.Add(new float[] {Mn_,Pn_ });
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


            return new Tuple<List<float[]>, List<float[]>>(MP,MP3D);
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

                Tuple<List<float[]>, List<float[]>> ResultadosNominales = DiagramaInteraccionParaUnAngulo(Angulo,false);
                List<float[]> PnMn2D_ = ResultadosNominales.Item1;
                List<float[]> MnPn3D_ = ResultadosNominales.Item2;


                PnMn2D.Add(new Tuple<List<float[]>, int>(PnMn2D_,Angulo));
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

            if (gDE == GDE.DMO)
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
                double limite1, limite2, limite3, limite4, limite_def;
                double PAs1, PAs2;
                float Ach = (B - 2 * r) * (H - 2 * r);

                S1 = Estribo.NoRamasV1 * FunctionsProject.Find_As(3) / (FD1 * (B - 2 * r) * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasV1 * FY * FunctionsProject.Find_As(3) / (FD2 * (B - 2 * r) * Material.FC);

                #region Estribo  #3

                SV = new double[] { S1 * 100, S2 * 100 }.Min();

                S1 = Estribo.NoRamasH1 * FunctionsProject.Find_As(3) / (FD1 * (H - 2 * r) * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasH1 * FY * FunctionsProject.Find_As(3) / (FD2 * (H - 2 * r) * Material.FC);

                SH = new double[] { S1 * 100, S2 * 100 }.Min();
                Sdef1 = Math.Round(Math.Min(SV, SH), 1);

                if (gDE == GDE.DMO)
                {
                    var Db = Refuerzos.Min();
                    int Db1 = Convert.ToInt16(Db.Diametro.Substring(1));
                    limite1 = 8 * FunctionsProject.Find_Diametro(Db1);
                    limite2 = 16 * FunctionsProject.Find_Diametro(3);
                    limite3 = 15;
                    limite4 = B / 3 < H / 3 ? B * 100 / 3 : H * 100 / 3;
                    limite_def = new double[] { limite1, limite2, limite3, limite4 }.Min();
                    if (Sdef1 > limite_def) Sdef1 = Math.Round(limite_def, 1);
                }

                if (gDE == GDE.DES)
                {
                    var Db = Refuerzos.Min();
                    int Db1 = Convert.ToInt16(Db.Diametro.Substring(1));
                    limite1 = 6 * FunctionsProject.Find_Diametro(Db1);
                    limite2 = 15;
                    limite4 = B / 4 < H / 4 ? B * 100 / 4 : H * 100 / 4;
                    limite_def = new double[] { limite1, limite2, limite4 }.Min();
                    if (Sdef1 > limite_def) Sdef1 = Math.Round(limite_def, 2);
                }

                Estribo.NoEstribo = 3;
                Estribo.Separacion = (float)Sdef1;

                PAs1 = Peso_Estribo(Estribo, r);

                #endregion Estribo  #3

                #region Estribo  #4

                S1 = Estribo.NoRamasV1 * FunctionsProject.Find_As(4) / (FD1 * (B - 2 * r) * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasV1 * FY * FunctionsProject.Find_As(4) / (FD2 * (B - 2 * r) * Material.FC);
                SV = new double[] { S1 * 100, S2 * 100 }.Min();

                S1 = Estribo.NoRamasH1 * FunctionsProject.Find_As(4) / (FD1 * (H - 2 * r) * (Material.FC / FY) * ((Area / Ach) - 1));
                S2 = Estribo.NoRamasH1 * FY * FunctionsProject.Find_As(4) / (FD2 * (H - 2 * r) * Material.FC);
                SH = new double[] { S1 * 100, S2 * 100 }.Min();

                Sdef2 = Math.Round(Math.Min(SV, SH), 1);

                if (gDE == GDE.DMO)
                {
                    var Db = Refuerzos.Min();
                    int Db1 = Convert.ToInt16(Db.Diametro.Substring(1));
                    limite1 = 8 * FunctionsProject.Find_Diametro(Db1);
                    limite2 = 16 * FunctionsProject.Find_Diametro(4);
                    limite3 = 15;
                    limite4 = B / 3 < H / 3 ? B * 100 / 3 : H * 100 / 3;
                    limite_def = new double[] { limite1, limite2, limite3,limite4 }.Min();
                    if (Sdef2 > limite_def) Sdef2 = Math.Round(limite_def, 1);
                }

                if (gDE == GDE.DES)
                {
                    var Db = Refuerzos.Min();
                    int Db1 = Convert.ToInt16(Db.Diametro.Substring(1));
                    limite1 = 6 * FunctionsProject.Find_Diametro(Db1);
                    limite2 = 15;
                    limite4 = B / 4 < H / 4 ? B * 100 / 4 : H * 100 / 4;
                    limite_def = new double[] { limite1, limite2,limite4 }.Min();
                    if (Sdef2 > limite_def) Sdef2 = Math.Round(limite_def, 2);
                }

                Estribo.NoEstribo = 4;
                Estribo.Separacion = (float)Sdef2;

                PAs2 = Peso_Estribo(Estribo, r);

                #endregion Estribo  #4

                if (PAs1 < PAs2)
                {
                    Estribo.NoEstribo = 3;
                    Estribo.Area = FunctionsProject.Find_As(Estribo.NoEstribo);
                    Estribo.Separacion = (float)Sdef1;
                }
                else
                {
                    Estribo.NoEstribo = 4;
                    Estribo.Area = FunctionsProject.Find_As(Estribo.NoEstribo);
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
                r = FunctionsProject.Find_Diametro(Convert.ToInt32(refuerzoi.Diametro.Substring(1))) / 2;
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

            x1 = Convert.ToSingle(x + (FunctionsProject.Find_Diametro(Estribo.NoEstribo) * EscalaX));
            y1 = Convert.ToSingle(y + (FunctionsProject.Find_Diametro(Estribo.NoEstribo) * EscalaY));

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
            string Nom_Seccion = "";
            string Escala = "1:15";

            var X_unicos = Refuerzos.Select(x => Math.Round(x.Coord[0], 2)).ToList().Distinct().ToList();
            var Y_unicos = Refuerzos.Select(x => Math.Round(x.Coord[1], 2)).ToList().Distinct().ToList();

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vertices, LayerCuadro);
            FunctionsAutoCAD.FunctionsAutoCAD.B_Estribo(P_XYZ: new double[] { Xi + B / 2, Yi - 0.04, 0 }, Layer: "FC_ESTRIBOS", Base: B - 0.02 * Form1.Proyecto_.R, Altura: H - 0.02 * Form1.Proyecto_.R, Xscale: 1, Yscale: 1, Zscale: 1, Rotation: 0);

            #region Dibujo de refuerzo en seccion

            foreach (CRefuerzo refi in Refuerzos)
            {
                refi.Dibujo_Ref_Autocad(Xi + B / 2, Yi - H / 2, X_unicos.Max(), X_unicos.Min(), Y_unicos.Max(), Y_unicos.Min());
            }

            #endregion Dibujo de refuerzo en seccion

            #region Adicion de ganchos seccion

            ////Dibujo de Ganchos verticales
            Dist1 = Math.Abs(Y_unicos.Max() - Y_unicos.Min()) / 100;
            Dist2 = Math.Abs(X_unicos.Max() - X_unicos.Min()) / 100;

            //Determinar coordenadas ganchos

            float SepX = (B * 100 - 2 * 4f) / (Estribo.NoRamasV1 - 1);
            float SepY = (H * 100 - 2 * 4f) / (Estribo.NoRamasH1 - 1);
            float DeltaX = -(B * 100 / 2) + 4f; float DeltaY = -(H * 100 / 2) + 4f;
            List<double[]> CoordX = new List<double[]>();
            List<double[]> CoordY = new List<double[]>();
            var lista2 = Refuerzos.Select(x => x.Coord).ToList();

            for (int i = 0; i < Estribo.NoRamasV1; i++)
            {
                if (i>0 & i < Estribo.NoRamasV1 - 1)
                {
                    CoordX.Add(FunctionsProject.Distancias(new double[] { DeltaX, -(H * 100 / 2) + 4f }, lista2));
                }

                DeltaX += SepX;
            }

            for (int i = 0; i < Estribo.NoRamasH1; i++)
            {
                if (i > 0 & i < Estribo.NoRamasH1 - 1)
                {
                    CoordY.Add(FunctionsProject.Distancias(new double[] { -(B * 100 / 2) + 4f, DeltaY }, lista2));
                }
                DeltaY += SepY;
            }

            short Flip_state=0;
            string Layer_aux = "FC_GANCHOS";

            for (int i = 0; i < CoordX.Count; i++) 
            {
                P_XYZ = new double[] { Xi + (B / 2) + CoordX[i][0] / 100, Yi - (H / 2) + Y_unicos.Max() / 100, 0 };
                FunctionsAutoCAD.FunctionsAutoCAD.B_Gancho(P_XYZ, Layer_aux, Dist1, 1, 1, 1, 270, Flip_state);
                Flip_state = Flip_state == 0 ? (short)1 : (short)0;
            }
            Flip_state = 1;

            for (int i = 0; i < CoordY.Count; i++) 
            {
                P_XYZ = new double[] { Xi + (B / 2) + X_unicos.Min() / 100, Yi - (H / 2) + CoordY[i][1] / 100, 0 };
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

            Long_Estibo = 2 * (B - 2 * recubrimiento) + 2 * (H - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 135);
            Long_GanchoH = (B - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180);
            Long_GanchoV = (H - 2 * recubrimiento) + 2 * FunctionsProject.Gancho(pEstribo.NoEstribo, 180);

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
            //CRefuerzo nRefuerzo = null;
            //double[] coord1 = new double[2];
            //int id = 0;
            //double Xmin, Xmax, Ymin, Ymax;

            //Xmin = Refuerzos.Select(x => x.Coord[0]).Min();
            //Xmax = Refuerzos.Select(x => x.Coord[0]).Max();
            //Ymin = Refuerzos.Select(x => x.Coord[1]).Min();
            //Ymax = Refuerzos.Select(x => x.Coord[1]).Max();

            //if (palzado.Colum_Alzado[indice] != null)
            //{
            //    if (Refuerzos.Exists(x => x.Alzado == palzado.ID))
            //    {
            //        var Refuerzo_alzado = Refuerzos.FindAll(x => x.Alzado == palzado.ID);
            //        foreach (var refuerzoi in Refuerzo_alzado)
            //        {
            //            refuerzoi.Diametro = $"#{palzado.Colum_Alzado[indice].NoBarra}";
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 0; i < palzado.Colum_Alzado[indice].CantBarras; i += 2)
            //        {
            //            id = Refuerzos.Last().id + 1;

            //            if (i % 2 == 0)
            //            {
            //                coord1[0] = Refuerzos[i].Coord[0] + Form1.Proyecto_.Diametro_ref[Convert.ToInt16(Refuerzos[i].Diametro.Substring(1))] / 2 +
            //                    Form1.Proyecto_.Diametro_ref[Convert.ToInt16(palzado.Colum_Alzado[indice].NoBarra)] / 2;
            //                coord1[1] = Refuerzos[i].Coord[1];
            //            }
            //            else
            //            {
            //                coord1[0] = Refuerzos[Refuerzos.Count - i - 1].Coord[0] + Form1.Proyecto_.Diametro_ref[Convert.ToInt16(Refuerzos[Refuerzos.Count - i - 1].Diametro.Substring(1))] / 2 +
            //                    Form1.Proyecto_.Diametro_ref[Convert.ToInt16(palzado.Colum_Alzado[indice].NoBarra)] / 2;
            //                coord1[1] = Refuerzos[Refuerzos.Count - i - 1].Coord[1];
            //            }

            //            nRefuerzo = new CRefuerzo(id, $"#{palzado.Colum_Alzado[indice].NoBarra}", coord1, TipodeRefuerzo.longitudinal);
            //            nRefuerzo.Alzado = palzado.ID;
            //            Refuerzos.Add(nRefuerzo);
            //        }
            //    }
            //}

            //if (fInterfaz != null)
            //{
            //    fInterfaz.edicion = Tipo_Edicion.Secciones_modelo;
            //    fInterfaz.Get_Columna();
            //    fInterfaz.Load_Pisos();
            //    fInterfaz.Get_section();
            //    fInterfaz.Invalidate();
            //}
        }

        #endregion Metodos: Cantidades

        #endregion Propiedades y Metodos - Secciones Predefinidas
    }
}