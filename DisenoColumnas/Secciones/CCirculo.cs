﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public class CCirculo : ISeccion
    {
        public string Name { get; set; }
        public MAT_CONCRETE Material { get; set; }
        public double radio { get; set; }
        public double[] Centro { get; set; } = { };
        public List<PointF> Puntos { get; set; } = new List<PointF>();
        public TipodeSeccion Shape { get; set; }
        public double Area { get; set; }
        public double Acero_Long { get; set; }
        public Estribo Estribo { get; set; }
        public List<CRefuerzo> Refuerzos { get; set; } = new List<CRefuerzo>();
        [NonSerialized] public List<GraphicsPath> Shapes_ref = new List<GraphicsPath>();
        public List<float[]> CoordenadasSeccion { get; set; }
        public bool Editado { get; set; } = false;
        public List<Tuple<int, int>> No_D_Barra { get; set; }
        public float B { get { return 2 * (float)radio; } set { B=value; } }
        public float H { get { return 2 * (float)radio; } set { H = value; } }        

        public CCirculo(string Nombre, double pradio, double[] pCentro, MAT_CONCRETE Material_, TipodeSeccion Shape_)
        {
            Name = Nombre;
            Material = Material_;
            Shape = Shape_;
            CalcularArea();
            radio = pradio;
            Centro = pCentro;
        }

        public void Set_puntos(int numero_puntos)
        {
            double delta_angulo = 2 * Math.PI / numero_puntos;
            double angulo = 0;

            PointF pi = new PointF();
            double xc = Centro[0];
            double yc = Centro[1];

            for (int i = 0; i < numero_puntos; i++)
            {
                pi.X = Convert.ToSingle(xc + Math.Cos(angulo) * radio);
                pi.Y = Convert.ToSingle(yc + Math.Sin(angulo) * radio);
                Puntos.Add(pi);
                angulo += delta_angulo;
            }
        }

        public void Cuanti_Vol(float FactorDisipacion1, float FactorDisipacion2, float r, float FY)
        {
            double Ash;
            float S = Estribo.Separacion / 100;

            Ash = FactorDisipacion1 * (Material.FC / FY) * (2 * (radio - r) * S * 0.25);
            Estribo.NoRamasV1 = S != 0 && Estribo.Area != 0 ? Convert.ToInt32(Ash / Estribo.Area) : 0;
        }

        public void Calc_vol_inex(float r, float FY)
        {
            float FD1, FD2;
            if (Form1.Proyecto_.DMO_DES == GDE.DMO)
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
            s_max = Form1.Proyecto_.DMO_DES == GDE.DMO ? 2 * radio / 3 : 2 * radio / 4;

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

                GT_As1.Add(Num_Ramas_V.Last() * (G_As1 * Estribo.NoRamasH1));
                P_As1.Add(GT_As1.Last() * Ast1 * 7850 / Math.Pow(100, 3));

                #endregion Estribo #3

                #region Estribo #4

                Estribo = new Estribo(4) //Estribo temporal
                {
                    Separacion = Convert.ToSingle(s_d)
                };
                Cuanti_Vol(FD1, FD2, r, FY);

                GT_As2.Add(Num_Ramas_V.Last() * (G_As2 * Estribo.NoRamasH1));
                P_As2.Add(GT_As2.Last() * Ast2 * 7850 / Math.Pow(100, 3));

                #endregion Estribo #4
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
                r = Form1.Proyecto_.Diametro_ref[Convert.ToInt32(refuerzoi.Diametro.Substring(1))] / 2;
                r = r * EscalaR;

                xc = refuerzoi.Coord[0] * EscalaX;
                yc = -refuerzoi.Coord[1] * EscalaY;
                pcentro = new double[] { xc, yc };

                MAT_CONCRETE material = new MAT_CONCRETE
                {
                    FC = 4220,
                    Name = "FY4220"
                };

                circulo = new CCirculo("Refuerzo", r, pcentro, material, TipodeSeccion.Circle);
                circulo.Set_puntos(10);

                path.AddClosedCurve(circulo.Puntos.ToArray());
                Shapes_ref.Add(path);
            }
        }

        public GraphicsPath Add_Estribos(double EscalaX, double EscalaY, float rec)
        {
            GraphicsPath path = new GraphicsPath();
            CCirculo circulo1, circulo2;
            double r1 = (radio - Form1.Proyecto_.R);
            double r2 = (r1 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo]);

            MAT_CONCRETE material = new MAT_CONCRETE
            {
                FC = 4220,
                Name = "FY4220"
            };

            circulo1 = new CCirculo("Refuerzo", r1, Centro, material, TipodeSeccion.Circle);
            circulo1.Set_puntos(50);

            circulo2 = new CCirculo("Refuerzo", r2, Centro, material, TipodeSeccion.Circle);
            circulo2.Set_puntos(50);

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
            throw new NotImplementedException();
        }
    }
}