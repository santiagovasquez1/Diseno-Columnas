using System;
using System.Collections.Generic;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public enum TipodeRefuerzo
    {
        longitudinal,
        Gancho,
        Estribo
    }

    [Serializable]
    public class CRefuerzo : IComparable
    {
        public int id { get; set; }
        public string Diametro { get; set; }
        public double As_Long { get; set; }
        public double[] Coord { get; set; }
        public int Alzado { get; set; }
        public TipodeRefuerzo TipodeRefuerzo { get; set; }

        public CRefuerzo(int pid, string pdiametro, double[] pcoord, TipodeRefuerzo ptipo)
        {
            int d1;
            id = pid;
            Diametro = pdiametro;
            Coord = pcoord;
            TipodeRefuerzo = ptipo;
            d1 = Convert.ToInt32(Diametro.Substring(1));
            As_Long = FunctionsProject.Find_As(d1) * Math.Pow(100, 2);
            Alzado = 1;
        }

        #region Propiedades y Metodos para Diagrama

        public List<Tuple<float[], int>> Coordenadas_PorCadaAngulo { get; set; } = new List<Tuple<float[], int>>();
        public List<Tuple<List<float>, int>> Esfuerzos_PorCadaCPorCadaAngulo { get; set; } = new List<Tuple<List<float>, int>>();
        public List<Tuple<List<float>, int>> Deformacion_PorCadaCPorCadaAngulo { get; set; } = new List<Tuple<List<float>, int>>();
        public List<Tuple<List<float>, int>> Fuerzas_PorCadaCPorCadaAngulo { get; set; } = new List<Tuple<List<float>, int>>();
        public List<Tuple<List<float>, int>> Momento_PorCadaCPorCadaAngulo { get; set; } = new List<Tuple<List<float>, int>>();

        public float[] Coordenadas_Angulo { get; set; } = new float[] { };
        public List<float> Esfuerzos_Angulo { get; set; } = new List<float>();
        public List<float> Deformacion_Angulo { get; set; } = new List<float>();
        public List<float> Fuerzas_Angulo { get; set; } = new List<float>();
        public List<float> Momento_Angulo { get; set; } = new List<float>();

        public void CalcularCoordenadasPorCadaAngulo(int Angulo)
        {
            List<double> CoordRotacion = B_Operaciones_Matricialesl.Operaciones.Rotacion(Coord[0], Coord[1], (Angulo * Math.PI) / 180);
            float[] CoordeRotadas = new float[] { (float)CoordRotacion[0], (float)CoordRotacion[1] };
            Coordenadas_PorCadaAngulo.Add(new Tuple<float[], int>(CoordeRotadas, Angulo));
        }

        public void CalcularDeformacion(List<float> C, float ecu, int Angulo, float FY, float ES, float Ymax, Secciones.TipodeSeccion pseccion)
        {
            float[] CoordenadasAngulo = Coordenadas_PorCadaAngulo.Find(x => x.Item2 == Angulo).Item1;
            List<float> Fuerzas = new List<float>();
            List<float> Deform = new List<float>();
            List<float> Esfuerz = new List<float>();
            List<float> Momento = new List<float>();

            if (pseccion != Secciones.TipodeSeccion.Circle)
            {
                for (int i = 0; i < C.Count; i++)
                {
                    float C_Original = Ymax - C[i];
                    float d = Ymax - CoordenadasAngulo[1];
                    float esi = ((C_Original - d) / C_Original) * ecu;

                    float fs = ES * esi;

                    if (Math.Abs(fs) > FY)
                    {
                        if (esi < 0)
                        {
                            fs = -FY;
                        }
                        else
                        {
                            fs = FY;
                        }
                    }

                    float FS;
                    float Mi;

                    FS = fs * (float)As_Long;
                    Mi = Math.Abs(FS) * Math.Abs(CoordenadasAngulo[1]);

                    Fuerzas.Add(FS);
                    Momento.Add(Mi);
                    Esfuerz.Add(fs);
                    Deform.Add(esi);
                }
            }
            else
            {
                for (int i = 0; i < C.Count; i++)
                {
                    float Magnitud_Di, Magnitud_Ci;
                    Magnitud_Ci = C[i] + Ymax;
                    Magnitud_Di = CoordenadasAngulo[1] + Ymax;

                    float esi = ((Magnitud_Ci - Magnitud_Di) / Magnitud_Ci) * ecu;

                    float fs = ES * esi;

                    if (Math.Abs(fs) > FY)
                    {
                        if (esi < 0)
                        {
                            fs = -FY;
                        }
                        else
                        {
                            fs = FY;
                        }
                    }

                    float FS;
                    float Mi;

                    FS = fs * (float)As_Long;
                    Mi = Math.Abs(FS) * Math.Abs(Ymax - Magnitud_Di);

                    Fuerzas.Add(FS);
                    Momento.Add(Mi);
                    Esfuerz.Add(fs);
                    Deform.Add(esi);
                }
            }

            Esfuerzos_PorCadaCPorCadaAngulo.Add(new Tuple<List<float>, int>(Esfuerz, Angulo));
            Deformacion_PorCadaCPorCadaAngulo.Add(new Tuple<List<float>, int>(Deform, Angulo));
            Fuerzas_PorCadaCPorCadaAngulo.Add(new Tuple<List<float>, int>(Fuerzas, Angulo));
            Momento_PorCadaCPorCadaAngulo.Add(new Tuple<List<float>, int>(Momento, Angulo));
        }

        #endregion Propiedades y Metodos para Diagrama

        public void Dibujo_Ref_Autocad(double Xi, double Yi, double Xmax, double Xmin, double Ymax, double Ymin)
        {
            double[] P_XYZ = { };
            double[] T_XYZ = { };
            string Layer = "FC_REFUERZO 2";

            P_XYZ = new double[] { Xi + Coord[0] / 100, Yi + Coord[1] / 100, 0 };
            FunctionsAutoCAD.FunctionsAutoCAD.Add_ref(P_XYZ, Layer, Alzado, 1, 1, 1, 0);

            if (Math.Round(Coord[1], 2) == Ymax)
            {
                T_XYZ = new double[] { Xi + (Coord[0] / 100) - 0.007, Yi + (Coord[1] / 100) + 0.05, 0 };
            }

            if (Math.Round(Coord[1], 2) == Ymin)
            {
                T_XYZ = new double[] { Xi + (Coord[0] / 100) - 0.007, Yi + (Coord[1] / 100) - 0.03, 0 };
            }

            if (Math.Round(Coord[1], 2) > Ymin & Math.Round(Coord[1], 2) < Ymax)
            {
                if (Math.Round(Coord[0], 2) >= Xmin)
                {
                    T_XYZ = new double[] { Xi + (Coord[0] / 100) - 0.05, Yi + (Coord[1] / 100) - 0.007, 0 };
                }
                if (Math.Round(Coord[0], 2) <= Xmax)
                {
                    T_XYZ = new double[] { Xi + (Coord[0] / 100) + 0.03, Yi + (Coord[1] / 100) - 0.007, 0 };
                }
            }

            FunctionsAutoCAD.FunctionsAutoCAD.AddText(Alzado.ToString(), T_XYZ, 0.075, 0.0225, "FC_R-100", "FC_TEXT", 0);
        }

        public static double operator +(CRefuerzo r1, CRefuerzo r2)
        {
            double Suma_as;
            Suma_as = r1.As_Long + r2.As_Long;
            return Suma_as;
        }

        public override string ToString()
        {
            return string.Format("{0}", Diametro);
        }

        public int CompareTo(object obj)
        {
            if (obj is CRefuerzo)
            {
                CRefuerzo temp = (CRefuerzo)obj;
                if (As_Long > temp.As_Long) return 1;
                if (As_Long < temp.As_Long) return +1;
            }
            return 0;
        }

        public static bool operator ==(CRefuerzo r1, CRefuerzo r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(CRefuerzo r1, CRefuerzo r2)
        {
            try
            {
                return !r1.Equals(r2);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool operator <(CRefuerzo r1, CRefuerzo r2)
        {
            if (r1.CompareTo(r2) < 0)
                return true;
            else
                return false;
        }

        public static bool operator >(CRefuerzo r1, CRefuerzo r2)
        {
            if (r1.CompareTo(r2) > 0)
                return true;
            else
                return false;
        }
    }
}