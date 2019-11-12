﻿using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DisenoColumnas.Secciones
{
    [Serializable]
    public class CSD : ISeccion
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
                bc = (H - TF) - -2 * r;
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

        public void Calc_vol_inex(float r, float FY)
        {
            float FD1, FD2;
            double s_max, s_min;
            double s_d;
            double Ast1, Ast2, G_As1, G_As2, LG_As1, LG_As2; //Variables en la aleta
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

            if (Form1.Proyecto_.DMO_DES == GDE.DMO)
            {
                FD1 = 0.20f;
                FD2 = 0.06f;
            }
            else
            {
                FD1 = 0.30f;
                FD2 = 0.09f;
            }

            Ast1 = 0.71; //'Estribo #3
            Ast2 = 1.29; //'Estribo #4

            s_min = 7.5;

            if (Form1.Proyecto_.DMO_DES == GDE.DMO)
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

                Num_Ramas_V.Add(Convert.ToInt32(Math.Round((100) / s_d, 0) + 1));
                Cuanti_Vol(FD1, FD2, r, FY);

                GT_As1.Add(Num_Ramas_V.Last() * (G_As1 * Estribo.NoRamasH1 + LG_As1 * Estribo.NoRamasV1));
                GTw_As1.Add(Num_Ramas_V.Last() * (Gw_As1 * Estribo.NoRamasH2 + LGw_As1 * Estribo.NoRamasV2));

                P_As1.Add((GT_As1.Last() + GTw_As1.Last()) * Ast1 * 7850 / Math.Pow(100, 3));

                #endregion Estribo #3

                #region Estribo #4

                Estribo = new Estribo(4) //Estribo temporal
                {
                    Separacion = Convert.ToSingle(s_d)
                };

                Num_Ramas_V.Add(Convert.ToInt32(Math.Round((100) / s_d, 0) + 1));
                Cuanti_Vol(FD1, FD2, r, FY);

                GT_As2.Add(Num_Ramas_V.Last() * (G_As2 * Estribo.NoRamasH1 + LG_As2 * Estribo.NoRamasV1));
                GTw_As2.Add(Num_Ramas_V.Last() * (Gw_As2 * Estribo.NoRamasH2 + LGw_As2 * Estribo.NoRamasV2));

                P_As2.Add((GT_As2.Last() + GTw_As2.Last()) * Ast2 * 7850 / Math.Pow(100, 3));

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
                circulo.Set_puntos(10);

                path.AddClosedCurve(circulo.Puntos.ToArray());
                Shapes_ref.Add(path);
            }
        }

        public GraphicsPath Add_Estribos(double EscalaX, double EscalaY, float rec)
        {
            GraphicsPath path = new GraphicsPath();
            List<PointF> Vertices = new List<PointF>();
            List<PointF> Vertices2 = new List<PointF>();

            double X1, Y1, X2, Y2, X3, X4, Y3, Y4;
            double Xr1, Yr1, Xr2, Yr2, Xr3, Xr4, Yr3, Yr4;

            X1 = 0;
            Y1 = 0;
            X2 = 0;
            Y2 = 0;

            X3 = 0;
            Y3 = 0;
            X4 = 0;
            Y4 = 0;

            var Xunicos = CoordenadasSeccion.Select(x => x[0]).Distinct().ToList();
            var Yunicos = CoordenadasSeccion.Select(x => x[1]).Distinct().ToList();

            if (Shape == TipodeSeccion.L)
            {
                //Aleta seccion
                X1 = (Xunicos.Min() + rec) * 100 * EscalaX;
                X2 = (Xunicos.Min() + B - rec) * 100 * EscalaX;

                var aux = CoordenadasSeccion.FindAll(x => x[1] == Yunicos.Min()).ToList();
                var aux2 = CoordenadasSeccion.FindAll(x => x[0] == Xunicos.Min()).ToList();

                if (aux.Exists(x => x[0] == Xunicos.Min()) & aux.Exists(x => x[0] == Xunicos.Max()))
                {
                    Y1 = (Yunicos.Min() - rec) * 100 * EscalaY;
                    Y2 = (Yunicos.Min() + TF + rec) * 100 * EscalaY;
                }
                else
                {
                    Y1 = (Yunicos.Max() - rec) * 100 * EscalaY;
                    Y2 = (Yunicos.Max() - TF + rec) * 100 * EscalaY;
                }

                if (aux2.Exists(x => x[1] == Yunicos.Min()) & aux.Exists(x => x[1] == Yunicos.Max()))
                {
                    X3 = (Xunicos.Min() + rec + 0.02) * 100 * EscalaX;
                    X4 = (Xunicos.Min() + TW + rec) * 100 * EscalaX;
                }
                else
                {
                    X3 = (Xunicos.Max() - rec - 0.02) * 100 * EscalaX;
                    X4 = (Xunicos.Max() - TW + rec) * 100 * EscalaX;
                }

                Y3 = (Yunicos.Min() + rec) * 100 * EscalaY;
                Y4 = (Yunicos.Max() - rec - 0.02) * 100 * EscalaY;
            }

            //Dibujo estribo en el alma
            Xr1 = X1 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo] * EscalaX;
            Yr1 = Y1 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo] * EscalaY;
            Xr2 = X2 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo] * EscalaX;
            Yr2 = Y2 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo] * EscalaY;

            Xr3 = X3 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo] * EscalaX;
            Yr3 = Y3 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo] * EscalaY;
            Xr4 = X4 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo] * EscalaX;
            Yr4 = Y4 + Form1.Proyecto_.Diametro_ref[Estribo.NoEstribo] * EscalaY;

            Vertices.Add(new PointF((float)X1, (float)Y1));
            Vertices.Add(new PointF((float)X2, (float)Y1));
            Vertices.Add(new PointF((float)X2, (float)Y2));
            Vertices.Add(new PointF((float)X1, (float)Y2));

            Vertices2.Add(new PointF((float)Xr1, (float)Yr1));
            Vertices2.Add(new PointF((float)Xr2, (float)Yr1));
            Vertices2.Add(new PointF((float)Xr2, (float)Yr2));
            Vertices2.Add(new PointF((float)Xr1, (float)Yr2));

            path.AddPolygon(Vertices.ToArray());
            path.AddPolygon(Vertices2.ToArray());

            //Dibujo estribo en la aleta

            Vertices = new List<PointF>();
            Vertices2 = new List<PointF>();

            Vertices.Add(new PointF((float)X3, (float)Y3));
            Vertices.Add(new PointF((float)X4, (float)Y3));
            Vertices.Add(new PointF((float)X4, (float)Y4));
            Vertices.Add(new PointF((float)X3, (float)Y4));

            Vertices2.Add(new PointF((float)Xr3, (float)Yr3));
            Vertices2.Add(new PointF((float)Xr4, (float)Yr3));
            Vertices2.Add(new PointF((float)Xr4, (float)Yr4));
            Vertices2.Add(new PointF((float)Xr3, (float)Yr4));

            path.AddPolygon(Vertices.ToArray());
            path.AddPolygon(Vertices2.ToArray());
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
    }
}