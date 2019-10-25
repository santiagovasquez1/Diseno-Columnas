using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public class Columna
    {
        #region Constructor
        public Columna(string Nombre)
        {
            Name = Nombre;
        }
        #endregion


        #region Propiedades- Paint

        [NonSerialized]
        public Brush BrushesColor = Brushes.Black;
        private float w;
        private float h;
        private float X_Colum;
        private float Y_Colum;
        public string Point { get; set; }
        private ColumnaAlzadoDrawing CoordForAlzado1 { get; set; }
        public int StoryMostrar { get; set; } = -1;
        public double[] CoordXY { get; set; } = new double[2];   /// Cordenadas en Planta
        #endregion


        #region Propeidades - Calculos
        public string Name { get; set; }


        public List<Tuple<Seccion, string>> Seccions { get; set; } = new List<Tuple<Seccion, string>>();
        public Viga VigaMayor { get; set; }

        public List<float> LuzLibre { get; set; }

        public List<float> LuzAcum { get; set; }

        public List<ResultadosETABS> resultadosETABs { get; set; }

        public List<Estribo> estribos { get; set; } = new List<Estribo>();


        public List<Alzado> Alzados { get; set; } = new List<Alzado>();



        #endregion


        #region Propiedades - Auxiliares

        public bool Maestro { get; set; } = false;

         public string ColSimilName { get; set; }

        #endregion






        #region Metodos-Calculos

        public void CalcularCuantiaVolumetrica(float FactorDisipacion1, float FactorDisipacion2, float r, float FY)
        {
            double Ash1, Ash2, Ash;

            for (int i = 0; i < Seccions.Count; i++)
            {


                if (Seccions[i].Item1.Shape == TipodeSeccion.Rectangular)
                {


                    //VERTICAL
                    float Ach = (Seccions[i].Item1.B - 2 * r) * (Seccions[i].Item1.H - 2 * r);
                    float bc = Seccions[i].Item1.B - 2 * r;
                    float S = estribos[i].Separacion / 100;


                    Ash1 = (FactorDisipacion1 * S * bc * Seccions[i].Item1.Material.FC / FY) * (Seccions[i].Item1.Area / Ach - 1);  //C.21-2

                    Ash2 = FactorDisipacion2 * S * bc * Seccions[i].Item1.Material.FC / FY;  //C.21-3

                    Ash = Ash1 > Ash2 ? Ash1 : Ash2;
                    if (S != 0 && estribos[i].Area != 0)
                    {
                        estribos[i].NoRamasV1 = Convert.ToInt32(Math.Round(Ash / estribos[i].Area < 2 ? 2 : (float)Math.Round(Ash / estribos[i].Area, 2), 2));
                    }
                    else
                    {
                        estribos[i].NoRamasV1 = 0;
                    }
                    //HORIZONTAL
                    bc = Seccions[i].Item1.H - 2 * r;

                    Ash1 = (FactorDisipacion1 * S * bc * Seccions[i].Item1.Material.FC / FY) * (Seccions[i].Item1.Area / Ach - 1);  //C.21-2

                    Ash2 = FactorDisipacion2 * S * bc * Seccions[i].Item1.Material.FC / FY;  //C.21-3

                    Ash = Ash1 > Ash2 ? Ash1 : Ash2;

                    if (S != 0 && estribos[i].Area != 0)
                    {
                        estribos[i].NoRamasH1 = Convert.ToInt32(Math.Round(Ash / estribos[i].Area < 2 ? 2 : (float)Math.Round(Ash / estribos[i].Area, 2), 2));
                    }
                    else
                    {
                        estribos[i].NoRamasH1 = 0;
                    }
                }


            }
        }




        public void ActualizarRefuerzo()
        {

            for (int i = 0; i < resultadosETABs.Count; i++)
            {
                resultadosETABs[i].As_asignado[0] = 0;
                resultadosETABs[i].As_asignado[1] = 0;
                resultadosETABs[i].As_asignado[2] = 0;

            }
            foreach (Alzado alzado in Alzados)
            {


                for (int i = 0; i < alzado.Colum_Alzado.Count; i++)
                {

                    if (alzado.Colum_Alzado[i] != null)
                    {

                        double AreaBarra;

                        try
                        {
                            AreaBarra = Form1.Proyecto_.AceroBarras[alzado.Colum_Alzado[i].NoBarra];
                        }
                        catch
                        {
                            AreaBarra = 0;
                        }
                        resultadosETABs[i].As_asignado[0] += (float)AreaBarra * alzado.Colum_Alzado[i].CantBarras;
                        resultadosETABs[i].As_asignado[1] += (float)AreaBarra * alzado.Colum_Alzado[i].CantBarras;
                        resultadosETABs[i].As_asignado[2] += (float)AreaBarra * alzado.Colum_Alzado[i].CantBarras;

                    }

                }

            }

            for (int i = 0; i < resultadosETABs.Count; i++)
            {

                resultadosETABs[i].Porct_Refuerzo[0] = (resultadosETABs[i].As_asignado[0] / (float)resultadosETABs[i].AsTopMediumButton[0]) * 100;
                resultadosETABs[i].Porct_Refuerzo[1] = (resultadosETABs[i].As_asignado[1] / (float)resultadosETABs[i].AsTopMediumButton[1]) * 100;
                resultadosETABs[i].Porct_Refuerzo[2] = (resultadosETABs[i].As_asignado[2] / (float)resultadosETABs[i].AsTopMediumButton[2]) * 100;
            }




        }







        public void AsignarAsTopMediumButton_()
        {
            for (int i = 0; i < resultadosETABs.Count; i++) { resultadosETABs[i].AsignarAsTopMediumButton(); }
        }











        #endregion


        #region MetodosPaint
        public void Paint_(PaintEventArgs e, float HeightForm, float WidthForm, float SX, float SY, float WX1, float HY1, float XI, float YI)
        {
            if (CoordXY[0] < 0)
            {
                X_Colum = WX1 * SX - Math.Abs((float)CoordXY[0]) * SX;
            }
            else
            {
                X_Colum = WX1 * SX + Math.Abs((float)CoordXY[0]) * SX;
            }

            if (CoordXY[1] < 0)
            {
                Y_Colum = -HY1 * SY + Math.Abs((float)CoordXY[1]) * SY + HeightForm;
            }
            else
            {
                Y_Colum = -HY1 * SY - Math.Abs((float)CoordXY[1]) * SY + HeightForm;
            }

            X_Colum += XI;
            Y_Colum += YI;

            w = 0.5f * (SX + SY) * 0.5f;
            h = 0.5f * (SX + SY) * 0.5f;

            if (BrushesColor == null)
            {
                BrushesColor = Brushes.Black;
            }

            Graphics graphics = e.Graphics;

            graphics.FillRectangle(BrushesColor, X_Colum, Y_Colum, w, h);


            float Tamano_Text = (SX + SY) * 0.6f * 0.3f;
            float X_string = X_Colum + w;

            if (X_string + 3 * Tamano_Text >= WidthForm)
            {
                X_string = X_Colum - w - 0.6f * SX;
            }

            float Y_string = Y_Colum;

            graphics.DrawString(Name, new Font("Calibri", Tamano_Text, FontStyle.Bold), Brushes.DarkRed, X_string, Y_string);
        }

        public Columna MouseDown(MouseEventArgs mouse)
        {
            if (X_Colum <= mouse.X && X_Colum + w >= mouse.X && Y_Colum <= mouse.Y && Y_Colum + h >= mouse.Y)
            {
                BrushesColor = Brushes.Red;
                return this;
            }
            else
            {
                BrushesColor = Brushes.Black;
                return null;
            }
        }


        private void CoordAlzado()
        {
            CoordForAlzado1 = new ColumnaAlzadoDrawing(this);
        }


        public void Paint_Alzado1(PaintEventArgs e, float HeightForm, float WidthForm, float SX, float SY, float X, float Y)
        {
            CoordAlzado();

            //Fundacion

            float x1 = X + CoordForAlzado1.Lista_CoordendasFundacion[0][0] * SX;
            float y1 = -Y - CoordForAlzado1.Lista_CoordendasFundacion[0][1] * SY + HeightForm;

            float x2 = X + CoordForAlzado1.Lista_CoordendasFundacion[1][0] * SX;
            float y2 = -Y - CoordForAlzado1.Lista_CoordendasFundacion[1][1] * SY + HeightForm;

            float x3 = X + CoordForAlzado1.Lista_CoordendasFundacion[2][0] * SX;
            float y3 = -Y - CoordForAlzado1.Lista_CoordendasFundacion[2][1] * SY + HeightForm;

            float x4 = X + CoordForAlzado1.Lista_CoordendasFundacion[3][0] * SX;
            float y4 = -Y - CoordForAlzado1.Lista_CoordendasFundacion[3][1] * SY + HeightForm;

            SolidBrush brush = new SolidBrush(Color.FromArgb(78, 59, 49));
            e.Graphics.FillPolygon(brush, new PointF[] { new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3), new PointF(x4, y4) });
            e.Graphics.DrawLine(new Pen(Brushes.Black), x1, y1, x2, y2);
            e.Graphics.DrawLine(new Pen(Brushes.Black), x2, y2, x3, y3);
            e.Graphics.DrawLine(new Pen(Brushes.Black), x3, y3, x4, y4);
            e.Graphics.DrawLine(new Pen(Brushes.Black), x4, y4, x1, y1);

            for (int i = 0; i < CoordForAlzado1.Lista_Coordenadas_Columna_Piso.Count; i++)
            {
                float x1_ = X + CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][0][0] * SX;
                float y1_ = -Y - CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][0][1] * SY + HeightForm;

                float x2_ = X + CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][1][0] * SX;
                float y2_ = -Y - CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][1][1] * SY + HeightForm;

                float x3_ = X + CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][2][0] * SX;
                float y3_ = -Y - CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][2][1] * SY + HeightForm;

                float x4_ = X + CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][3][0] * SX;
                float y4_ = -Y - CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][3][1] * SY + HeightForm;

                float x5_ = X + CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][0][0] * SX;
                float y5_ = -Y - CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][0][1] * SY + HeightForm;

                float x6_ = X + CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][1][0] * SX;
                float y6_ = -Y - CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][1][1] * SY + HeightForm;

                float x7_ = X + CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][2][0] * SX;
                float y7_ = -Y - CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][2][1] * SY + HeightForm;

                float x8_ = X + CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][3][0] * SX;
                float y8_ = -Y - CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][3][1] * SY + HeightForm;

                SolidBrush brush2 = new SolidBrush(Color.FromArgb(169, 172, 173));
                e.Graphics.FillPolygon(brush2, new PointF[] { new PointF(x1_, y1_), new PointF(x2_, y2_), new PointF(x3_, y3_), new PointF(x4_, y4_) });
                e.Graphics.DrawLine(new Pen(Brushes.Black), x1_, y1_, x2_, y2_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x2_, y2_, x3_, y3_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x3_, y3_, x4_, y4_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x4_, y4_, x1_, y1_);

                SolidBrush brush3 = new SolidBrush(Color.FromArgb(198, 198, 198));
                e.Graphics.FillPolygon(brush3, new PointF[] { new PointF(x5_, y5_), new PointF(x6_, y6_), new PointF(x7_, y7_), new PointF(x8_, y8_) });

                e.Graphics.DrawLine(new Pen(Brushes.Black), x5_, y5_, x6_, y6_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x6_, y6_, x7_, y7_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x7_, y7_, x8_, y8_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x8_, y8_, x5_, y5_);
            }

            //Nivel Fundación

            float x5, y5, x6, y6;

            x5 = CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][0];
            y5 = -Y - CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][1] * SY + HeightForm;

            x6 = CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][0] + WidthForm;
            y6 = -Y - CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][1] * SY + HeightForm;

            Pen Pen_Nivel = new Pen(Brushes.Red);
            Pen_Nivel.DashPattern = new float[] { 5, 2, 15, 4 };
            e.Graphics.DrawLine(Pen_Nivel, x5, y5, x6, y6);
        }


        #endregion
    }
}
