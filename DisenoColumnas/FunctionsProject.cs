using DisenoColumnas.Secciones_Predefinidas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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
            try {

                Decimal = Convert.ToSingle(NumeroDividido[1]);
                if (NumeroDividido[1].IndexOf("0") ==0 && NumeroDividido[1].Length ==2)
                {
                    string Entero2 = Entero + "." + "1";
                    return Convert.ToSingle(Entero2);
                }

                if (Convert.ToString(Decimal).Length <2)
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
                DecimilaRedondeado=(valor + 1) * multiplo;
                if (DecimilaRedondeado == 100)
                {
                    Entero += 1;
                    DecimilaRedondeado = 0;
                }
            }
            else
            {
                DecimilaRedondeado=valor* multiplo;
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
        public static List<PointF> CreatePointsForCircle(int numero_puntos, double pradio,double XC,double YC)
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

            foreach (KeyValuePair<int, double> As_d in Form1.Proyecto_.AceroBarras)
            {
                p_error = Math.Abs((Asi - As_d.Value) / As_d.Value) * 100;
                Dif.Add(p_error);
                Diametros.Add(As_d.Key);
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
    }
}