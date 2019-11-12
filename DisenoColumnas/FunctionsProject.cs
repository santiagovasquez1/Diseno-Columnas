using DisenoColumnas.Secciones_Predefinidas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        public static int Redondear_Decimales(int N_decimal, int multiplo)
        {
            int valor = 0;

            valor = N_decimal / multiplo;

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
    }
}