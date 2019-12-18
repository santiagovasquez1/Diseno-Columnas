using DisenoColumnas.Secciones_Predefinidas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DisenoColumnas
{
    public static class FunctionsProject
    {
        public static List<string> AbrirArchivoE2K2009(string Ruta)
        {
            List<string> ArhcivoE2K2009 = new List<string>();
            StreamReader colReader = new StreamReader(Ruta);
            string colLineReader;

            do
            {
                colLineReader = colReader.ReadLine();
                ArhcivoE2K2009.Add(colLineReader);
            } while (!(colLineReader == null));

            return ArhcivoE2K2009;
        }

        public static Tuple<List<string>, string> AbriArchivoResultados2009(string Ruta)
        {
            StreamReader colReader = null;
            string Mensaje;
            List<string> ArhcivoResultados2009 = new List<string>();
            Tuple<List<string>, string> tuple_aux;
            try
            {
                colReader = new StreamReader(Ruta);
                Mensaje = "OK";
                string colLineReader;
                do
                {
                    colLineReader = colReader.ReadLine();
                    ArhcivoResultados2009.Add(colLineReader);
                } while (!(colLineReader == null));
            }
            catch (Exception Causa)
            {
                Mensaje = Causa.Message;
            }

            tuple_aux = new Tuple<List<string>, string>(ArhcivoResultados2009, Mensaje);

            return tuple_aux;
        }

        public static void Serializar(string Ruta, Proyecto proyecto)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, proyecto);
            stream.Close();
        }

        public static void Deserealizar(string Ruta, ref Proyecto proyecto)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Stream streamReader = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.None);
            var proyectoDeserializado = (Proyecto)formatter.Deserialize(streamReader);

            proyecto = proyectoDeserializado;
            streamReader.Close();
        }

        public static void Serializar_Secciones(string Ruta, CLista_Secciones Secciones_predef)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, Secciones_predef);
            stream.Close();
        }

        public static void Deserealizar_Secciones(string Ruta, ref CLista_Secciones Secciones_prede)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            Stream streamReader = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.None);
            var proyectoDeserializado = (CLista_Secciones)formatter.Deserialize(streamReader);

            Secciones_prede = proyectoDeserializado;
            streamReader.Close();
        }

        public static float RedondearDecimales(float N_decimal, int multiplo)
        {
            string[] NumeroDividido = Convert.ToString(N_decimal).Split(Convert.ToChar("."));

            int Entero = Convert.ToInt32(NumeroDividido[0]);

            float Decimal;
            try
            {
                Decimal = Convert.ToSingle(NumeroDividido[1]);
                if (NumeroDividido[1].IndexOf("0") == 0 && NumeroDividido[1].Length == 2)
                {
                    string Entero2 = Entero + "." + "1";
                    return Convert.ToSingle(Entero2);
                }

                if (Convert.ToString(Decimal).Length < 2)
                {
                    return N_decimal;
                }
            }
            catch { Decimal = 0; }

            float Comparacion = (Decimal / multiplo);
            int valor = (int)(Decimal / multiplo);
            int DecimilaRedondeado;
            if (valor < (Comparacion))
            {
                DecimilaRedondeado = (valor + 1) * multiplo;
                if (DecimilaRedondeado == 100)
                {
                    Entero += 1;
                    DecimilaRedondeado = 0;
                }
            }
            else
            {
                DecimilaRedondeado = valor * multiplo;
            }

            return Convert.ToSingle(Entero + "." + DecimilaRedondeado);
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

        public static List<PointF> CreatePointsForCircle(int numero_puntos, double pradio, double XC, double YC)
        {
            double delta_angulo = 2 * Math.PI / numero_puntos;
            double angulo = 0;

            PointF pi = new PointF();
            List<PointF> Puntos = new List<PointF>();

            for (int i = 0; i < numero_puntos; i++)
            {
                pi.X = Convert.ToSingle(XC + Math.Cos(angulo) * pradio);
                pi.Y = Convert.ToSingle(YC + Math.Sin(angulo) * pradio);
                Puntos.Add(pi);
                angulo += delta_angulo;
            }
            return Puntos;
        }

        public static SolidBrush ColorBarra(int Barra)
        {
            SolidBrush brush = new SolidBrush(Color.Black);

            if (Barra == 2)
            {
                brush.Color = Color.FromArgb(215, 78, 34);
            }
            else if (Barra == 3)
            {
                brush.Color = Color.FromArgb(229, 106, 9);
            }
            else if (Barra == 4)
            {
                brush.Color = Color.FromArgb(197, 194, 10);
            }
            else if (Barra == 5)
            {
                brush.Color = Color.FromArgb(173, 229, 14);
            }
            else if (Barra == 6)
            {
                brush.Color = Color.FromArgb(6, 176, 12);
            }
            else if (Barra == 7)
            {
                brush.Color = Color.FromArgb(12, 198, 192);
            }
            else if (Barra == 8)
            {
                brush.Color = Color.FromArgb(10, 104, 218);
            }
            else if (Barra == 10)
            {
                brush.Color = Color.FromArgb(38, 10, 218);
            }
            return brush;
        }

        public static float FindTraslapo(int NoBarra, float FC)
        {
            float Traslapo = 0;
            if (FC == 210f) { Traslapo = Form1.Proyecto_.Ld_210[NoBarra]; }
            if (FC == 280f) { Traslapo = Form1.Proyecto_.Ld_280[NoBarra]; }
            if (FC == 350f) { Traslapo = Form1.Proyecto_.Ld_350[NoBarra]; }
            if (FC == 420f) { Traslapo = Form1.Proyecto_.Ld_420[NoBarra]; }
            if (FC == 490f) { Traslapo = Form1.Proyecto_.Ld_490[NoBarra]; }
            if (FC == 560f) { Traslapo = Form1.Proyecto_.Ld_560[NoBarra]; }
            return Traslapo;
        }

        public static int Redondear_Entero(double N_decimal, int multiplo, bool RedondInferior = false)
        {
            int valor = 0;

            valor = (int)(N_decimal / multiplo);
            if (RedondInferior)
            {
                return valor * multiplo;
            }

            if (valor < (N_decimal / multiplo))
            {
                return (valor + 1) * multiplo;
            }
            else
            {
                return valor * multiplo;
            }
        }

        public static float Dimension(List<float> P_unicos, bool lado)
        {
            float Max = -(float)Math.Pow(10, 6);
            float Min = (float)Math.Pow(10, 6);

            for (int i = 0; i < P_unicos.Count; i++)
            {
                for (int j = 0; j < P_unicos.Count; j++)
                {
                    if (i != j)
                    {
                        if (Math.Abs(P_unicos[i] - P_unicos[j]) >= Max)
                        {
                            Max = Math.Abs(P_unicos[i] - P_unicos[j]);
                        }

                        if (Math.Abs(P_unicos[i] - P_unicos[j]) <= Min)
                        {
                            Min = Math.Abs(P_unicos[i] - P_unicos[j]);
                        }
                    }
                }
            }

            if (lado == true)
            {
                return Max;
            }
            else
            {
                return Min;
            }
        }

        public static int Find_Barra(double Asi)
        {
            double p_error = 0;
            int indice = 0;
            int Barra_def = 0;
            List<double> Dif = new List<double>();
            List<int> Diametros = new List<int>();

            Dictionary<int, double> AceroBarras = new Dictionary<int, double>();
            AceroBarras = new Dictionary<int, double>();
            AceroBarras.Add(2, 0.32 / 10000);
            AceroBarras.Add(3, 0.71 / 10000);
            AceroBarras.Add(4, 1.29 / 10000);
            AceroBarras.Add(5, 1.99 / 10000);
            AceroBarras.Add(6, 2.84 / 10000);
            AceroBarras.Add(7, 3.87 / 10000);
            AceroBarras.Add(8, 5.10 / 10000);
            AceroBarras.Add(10, 8.09 / 10000);

            foreach (KeyValuePair<int, double> As_d in AceroBarras)
            {
                if (As_d.Value >= 0.98 * Asi)
                {
                    p_error = Math.Abs((Asi - As_d.Value) / As_d.Value) * 100;
                    Dif.Add(p_error);
                    Diametros.Add(As_d.Key);
                }
            }

            indice = Dif.FindIndex(x => x == Dif.Min());
            if (Diametros[indice] < 4)
            {
                Barra_def = 4;
            }
            else
            {
                Barra_def = Diametros[indice];
            }

            return Barra_def;
        }

        /// <summary>
        /// Determina el Área de un polígono por coordenadas
        /// </summary>
        /// <param name="CoordenadasXY">Coordenadas del Polígono (Nota: las coordenadas no son repetidas, es decir no se cierra el polígono)</param>
        /// <returns></returns>
        public static float DeterminarArea(List<float[]> CoordenadasXY)
        {
            float Area1 = 0;
            float Area2 = 0;
            for (int i = 0; i < CoordenadasXY.Count; i++)
            {
                if (i + 1 == CoordenadasXY.Count)
                {
                    Area1 += CoordenadasXY[i][0] * CoordenadasXY[0][1];
                    Area2 += CoordenadasXY[i][1] * CoordenadasXY[0][0];
                }
                else
                {
                    Area1 += CoordenadasXY[i][0] * CoordenadasXY[i + 1][1];
                    Area2 += CoordenadasXY[i][1] * CoordenadasXY[i + 1][0];
                }
            }
            return Math.Abs(Area1 - Area2) / 2; ;
        }

        public static float[] DeterminarCentroideSentidoAntiHorario(List<float[]> CoordenadasXY)
        {
            float Mx = 0; float My = 0;
            float Area = DeterminarArea(CoordenadasXY);

            for (int i = CoordenadasXY.Count - 1; i >= 0; i--)
            {
                float xi;
                float xi_1;
                float yi;
                float yi_1;

                if (i - 1 == -1)
                {
                    xi = CoordenadasXY[i][0];
                    xi_1 = CoordenadasXY[CoordenadasXY.Count - 1][0];
                    yi = CoordenadasXY[i][1];
                    yi_1 = CoordenadasXY[CoordenadasXY.Count - 1][1];
                }
                else
                {
                    xi = CoordenadasXY[i][0];
                    xi_1 = CoordenadasXY[i - 1][0];
                    yi = CoordenadasXY[i][1];
                    yi_1 = CoordenadasXY[i - 1][1];
                }

                Mx += (float)((xi_1 - xi) * ((Math.Pow(yi_1, 2) + Math.Pow(yi, 2) + yi * yi_1) / 6));
                My += (float)((yi_1 - yi) * ((Math.Pow(xi_1, 2) + Math.Pow(xi, 2) + xi * xi_1) / 6));
            }

            return new float[] { My / Area, Mx / Area };
        }

        public static float[] DeterminarCentroideSentidoHorario(List<float[]> CoordenadasXY)
        {
            float Mx = 0; float My = 0;
            float Area = DeterminarArea(CoordenadasXY);

            for (int i = 0; i < CoordenadasXY.Count; i++)
            {
                float xi;
                float xi_1;
                float yi;
                float yi_1;

                if (i + 1 == CoordenadasXY.Count)
                {
                    xi = CoordenadasXY[i][0];
                    xi_1 = CoordenadasXY[0][0];
                    yi = CoordenadasXY[i][1];
                    yi_1 = CoordenadasXY[0][1];
                }
                else
                {
                    xi = CoordenadasXY[i][0];
                    xi_1 = CoordenadasXY[i + 1][0];
                    yi = CoordenadasXY[i][1];
                    yi_1 = CoordenadasXY[i+1][1];
                }

                Mx += (float)((xi_1 - xi) * ((Math.Pow(yi_1, 2) + Math.Pow(yi, 2) + yi * yi_1) / 6));
                My += (float)((yi_1 - yi) * ((Math.Pow(xi_1, 2) + Math.Pow(xi, 2) + xi * xi_1) / 6));
            }

            return new float[] { My / Area, Mx / Area };
        }





        public static double Find_As(int diametro)
        {
            Dictionary<int, double> AceroBarras = new Dictionary<int, double>();
            double Asi = 0;

            AceroBarras = new Dictionary<int, double>();
            AceroBarras.Add(2, 0.32 / 10000);
            AceroBarras.Add(3, 0.71 / 10000);
            AceroBarras.Add(4, 1.29 / 10000);
            AceroBarras.Add(5, 1.99 / 10000);
            AceroBarras.Add(6, 2.84 / 10000);
            AceroBarras.Add(7, 3.87 / 10000);
            AceroBarras.Add(8, 5.10 / 10000);
            AceroBarras.Add(10, 8.09 / 10000);

            Asi = AceroBarras[diametro];

            return Asi;
        }

        public static double Find_Diametro(int diametro)
        {
            double Di = 0;
            Dictionary<int, float> Diametro_ref = new Dictionary<int, float>();
            Diametro_ref.Add(3, 0.95f);
            Diametro_ref.Add(4, 1.27f);
            Diametro_ref.Add(5, 1.59f);
            Diametro_ref.Add(6, 1.91f);
            Diametro_ref.Add(7, 2.22f);
            Diametro_ref.Add(8, 2.54f);
            Diametro_ref.Add(9, 2.87f);
            Diametro_ref.Add(10, 3.23f);
            Diametro_ref.Add(11, 3.58f);
            Diametro_ref.Add(14, 4.30f);

            Di = Diametro_ref[diametro];

            return Di;
        }

        public static double Gancho(int diametro, int Angulo)
        {
            double Long_gancho = 0;
            Dictionary<int, float> G90 = new Dictionary<int, float>();
            Dictionary<int, float> G135 = new Dictionary<int, float>();
            Dictionary<int, float> G180 = new Dictionary<int, float>();

            G180 = new Dictionary<int, float>
            {
                { 2, 0.116f },
                { 3, 0.14f },
                { 4, 0.167f },
                { 5, 0.192f },
                { 6, 0.228f },
                { 7, 0.266f },
                { 8, 0.305f },
                { 10, 0.457f }
            };

            G135 = new Dictionary<int, float>
            {
                { 2, 0.063f },
                { 3, 0.094f },
                { 4, 0.125f },
                { 5, 0.157f },
                { 6, 0.214f },
                { 7, 0.249f },
                { 8, 0.286f },
                { 10, 0.363f }
            };

            G90 = new Dictionary<int, float>
            {
                { 2, 0.09f },
                { 3, 0.14f },
                { 4, 0.18f },
                { 5, 0.23f },
                { 6, 0.27f },
                { 7, 0.32f },
                { 8, 0.36f },
                { 10, 0.47f }
            };

            if (Angulo == 90)
            {
                Long_gancho = G90[diametro];
            }
            if (Angulo == 135)
            {
                Long_gancho = G135[diametro];
            }
            if (Angulo == 180)
            {
                Long_gancho = G180[diametro];
            }

            return Long_gancho;
        }

        public static void EstiloDatGridView(DataGridView dataGrid)
        {
            DataGridViewCellStyle StyleC = new DataGridViewCellStyle();
            StyleC.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleC.Font = new Font("Vderdana", 8, FontStyle.Bold);

            DataGridViewCellStyle StyleR = new DataGridViewCellStyle();
            StyleR.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleR.Font = new Font("Vderdana", 8, FontStyle.Regular);

            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.HeaderCell.Style = StyleC;
            }
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                row.DefaultCellStyle = StyleR;
            }
        }

        //HALLAR VC


        /// <summary>
        /// Determinar Vc (kgf/cm²)---> Norma C.11.2.1.1
        /// </summary>
        /// <param name="d">Si se analiza Vc2, d=h-r, si se analiza Vc3, d=b-d</param>
        /// <param name="bw">Si se analiza Vc2, bw=bw, si se analiza Vc3, bw=h</param>
        /// <returns></returns>

        private static float Vc_11_2_1_1(float d, float bw, float fc)
        {
            return 0.53f * (float)Math.Sqrt(fc) * bw * d;
        }


        /// <summary>
        /// Determinar Vc (kgf/cm²) ----> Norma C.11.2.1.2
        /// </summary>
        /// <param name="Nu">Carga Axial</param>
        /// <param name="d">Si se analiza Vc2, d=h-r, si se analiza Vc3, d=b-r</param>
        /// <param name="bw">Si se analiza Vc2, bw=bw, si se analiza Vc3, bw=h</param>
        /// <param name="Area"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        private static float Vc_11_2_1_2(float Nu, float d, float bw, float Area, float fc)
        {
            return 0.53f * (1 + ((Nu) / (140 * Area))) * (float)Math.Sqrt(fc) * bw * d;
        }

        /// <summary>
        /// Determiniar Vc (kgf/cm²) --> Norma C.11.2.2.1 a C.11.2.2.3
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="As"></param>
        /// <param name="Vu"></param>
        /// <param name="d">Si se analiza Vc2, d=h-r, si se analiza Vc3, d=b-r</param>
        /// <param name="bw">Si se analiza Vc2, bw=bw, si se analiza Vc3, bw=h</param>
        /// <param name="Mu">Puede ser M2 o M3</param>
        /// <param name="h">Si se analiza Vc2, h=h, si se analiza Vc3, h=bw</param>
        /// <param name="Nu"></param>
        /// <param name="Area"></param>
        /// <returns></returns>

        private static float Vc_11_2_2_1(float fc, float As, float Vu, float d, float bw, float Mu, float h, float Nu, float Area)
        {
            float pw=As/ (bw * d);

            float Mm = Mu - (Nu * (4 * h - d) / 8);
   
            float Vc1 = (0.5f * (float)Math.Sqrt(fc) + ((176 * pw * Vu * d) / Mm)) * bw * d;
            if (Mm < 0)
            {
                Vc1 = 999999;
            }
            float Vc2 = 0.93f * (float)Math.Sqrt(fc) * bw * d * (float)Math.Sqrt(1 + (Nu / (35 * Area)));

            return Vc1 > Vc2 ? Vc2 : Vc1;
        }

        public static bool Find_Coord(List<float[]> Coordenadas,float X, float Y)
        {
            for(int i = 0; i < Coordenadas.Count; i++)
            {
                if (Coordenadas[i][0] == X & Coordenadas[i][1] == Y) 
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determiniar Vc (Tonf) --> Norma C.11.2.1.1 a C.11.2.2.3
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="pw">Si se analiza Vc2, pw= As/(bw*d), si se analiza Vc3, pw= As/(h*d)</param>
        /// <param name="Vu"></param>
        /// <param name="d">Si se analiza Vc2, d=h-r, si se analiza Vc3, d=b-r</param>
        /// <param name="bw">Si se analiza Vc2, bw=bw, si se analiza Vc3, bw=h</param>
        /// <param name="Mu">Puede ser M2 o M3</param>
        /// <param name="h">Si se analiza Vc2, h=h, si se analiza Vc3, h=bw</param>
        /// <param name="Nu"></param>
        /// <param name="Area"></param>
        /// <returns></returns>
        public static float VcDef(float fc, float As, float Vu, float d, float bw, float Mu, float h, float Nu, float Area)
        {

            float[] Vc = new float[] { Vc_11_2_1_1(d, bw, fc), Vc_11_2_1_2(Nu, d, bw, Area, fc), Vc_11_2_2_1(fc, As, Vu, d, bw, Mu, h, Nu, Area) };

            return Vc.Min()/1000;
        }

        public static PointF CoversionaPuntos(float[] FloatArray)
        {
            return new PointF(FloatArray[0], FloatArray[1]);
        }

    }
}