using DisenoColumnas.Secciones;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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
            Prueba.AddRange(new int[] { 1, 2, 3, 4, 5 });
        }

        #endregion Constructor

        #region Propeidades Para el Diseño

        public List<string[]> AlzadoBaseSugerido { get; set; }

        public List<List<string>> Col_Row_AlzadoBaseSugerido { get; set; }

        #endregion Propeidades Para el Diseño

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

        #endregion Propiedades- Paint

        #region Propeidades - Calculos

        public string Name { get; set; }

        public List<Tuple<ISeccion, string>> Seccions { get; set; } = new List<Tuple<ISeccion, string>>();
        public List<object[]> CantEstribos_Sepa { get; set; }

        public Viga VigaMayor { get; set; }

        public List<float> LuzLibre { get; set; }

        public List<float> LuzAcum { get; set; }

        public List<ResultadosETABS> resultadosETABs { get; set; }

        public List<Alzado> Alzados { get; set; } = new List<Alzado>();

        public List<int> Prueba = new List<int>();

        public List<float> KgRefuerzoforColumAlzado { get; set; } = new List<float>();

        public float KgRefuerzo { get; set; }

        #endregion Propeidades - Calculos

        #region Propiedades - Auxiliares

        public bool Maestro { get; set; } = false;
        public bool Disenar { get; set; } = false;
        public bool aGraficar { get; set; } = false;
        public bool Ready { get; set; } = false;

        /// <summary>
        /// Propiedad util para saber todos los similares que tiene la columna maestra.
        /// </summary>
        public List<string> NamesSimilares { get; set; } = new List<string>();

        public string Label { get; set; } = "";
        public string ColSimilName { get; set; }

        #endregion Propiedades - Auxiliares

        #region Propiedades - Cantidades DL NET

        public List<string> Lista_RefuerzoLongitudinal_DLNET { get; set; }
        public List<string> Lista_RefuerzoTransversal_DLNET { get; set; }

        #endregion Propiedades - Cantidades DL NET

        #region Metodos-Calculos

        public void CrearCuantiaProyectoAntiguos(int NoPiso)
        {
            float Area = (float)Seccions[NoPiso].Item1.Area;
   
            resultadosETABs[NoPiso].prequerida = new float[] { ((float)resultadosETABs[NoPiso].AsTopMediumButton[0]/Area)*100,
                                                               ((float)resultadosETABs[NoPiso].AsTopMediumButton[1]/Area)*100,
                                                               ((float)resultadosETABs[NoPiso].AsTopMediumButton[2]/Area)*100 };

            resultadosETABs[NoPiso].pasignada = new float[] { (resultadosETABs[NoPiso].As_asignado[0]/Area)*100,
                                                               (resultadosETABs[NoPiso].As_asignado[1]/Area)*100,
                                                               (resultadosETABs[NoPiso].As_asignado[2]/Area)*100 };

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
                        if (alzado.Colum_Alzado[i].Tipo != "A" && alzado.Colum_Alzado[i].Tipo != "Botton")
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
                        else if (alzado.Colum_Alzado[i].Tipo == "A")
                        {
                            float AreaBarra = (float)Form1.Proyecto_.AceroBarras[alzado.Colum_Alzado[i].NoBarra];
                            try
                            {
                                if (alzado.Colum_Alzado[i].Tipo2 == "+" | alzado.Colum_Alzado[i].Tipo2 == "" | alzado.Colum_Alzado[i].Tipo2 == null)
                                {
                                    resultadosETABs[i - 1].As_asignado[2] += alzado.Colum_Alzado[i].CantBarras * AreaBarra;//Acero Botton - Vecino
                                }
                            }
                            catch { }
                            if (alzado.Colum_Alzado[i].Tipo2 == "" | alzado.Colum_Alzado[i].Tipo2 == "-" | alzado.Colum_Alzado[i].Tipo2 == null)
                            {
                                resultadosETABs[i].As_asignado[0] += alzado.Colum_Alzado[i].CantBarras * AreaBarra;
                            }

                            resultadosETABs[i].Porct_Refuerzo[0] = resultadosETABs[i].As_asignado[0] / ((float)resultadosETABs[i].AsTopMediumButton[0]) * 100;

                            if (alzado.Colum_Alzado[i].UnitarioAdicional != null)
                            {
                                AreaBarra = (float)Form1.Proyecto_.AceroBarras[alzado.Colum_Alzado[i].UnitarioAdicional.NoBarra];

                                resultadosETABs[i].As_asignado[2] += AreaBarra * alzado.Colum_Alzado[i].UnitarioAdicional.CantBarras;

                                try
                                {
                                    resultadosETABs[i + 1].As_asignado[0] += AreaBarra * alzado.Colum_Alzado[i].UnitarioAdicional.CantBarras;    //Acero Botton - Vecino
                                }
                                catch { }
                            }
                        }
                        else if (alzado.Colum_Alzado[i].Tipo == "Botton")
                        {
                            float AreaBarra;
                            try
                            {
                                AreaBarra = (float)Form1.Proyecto_.AceroBarras[alzado.Colum_Alzado[i].NoBarra];
                            }
                            catch
                            {
                                AreaBarra = 0;
                            }
                            resultadosETABs[i].As_asignado[2] += AreaBarra * alzado.Colum_Alzado[i].CantBarras;

                            try
                            {
                                resultadosETABs[i + 1].As_asignado[0] += AreaBarra * alzado.Colum_Alzado[i].CantBarras;    //Acero Botton - Vecino
                            }
                            catch { }
                        }
                        Seccions[i].Item1.Actualizar_Ref(alzado, i, Form1.mIntefazSeccion);
                    }
                }
            }

            for (int i = 0; i < resultadosETABs.Count; i++)
            {
                resultadosETABs[i].Porct_Refuerzo[0] = (resultadosETABs[i].As_asignado[0] / (float)resultadosETABs[i].AsTopMediumButton[0]) * 100;
                resultadosETABs[i].Porct_Refuerzo[1] = (resultadosETABs[i].As_asignado[1] / (float)resultadosETABs[i].AsTopMediumButton[1]) * 100;
                resultadosETABs[i].Porct_Refuerzo[2] = (resultadosETABs[i].As_asignado[2] / (float)resultadosETABs[i].AsTopMediumButton[2]) * 100;
                float Area = (float)Seccions[i].Item1.Area;
                //Cuantia
                if (resultadosETABs[i].pasignada == null)
                {
                    CrearCuantiaProyectoAntiguos(i);
                }

                resultadosETABs[i].prequerida[0] = ((float)resultadosETABs[i].AsTopMediumButton[0] / Area) * 100;
                resultadosETABs[i].prequerida[1] = ((float)resultadosETABs[i].AsTopMediumButton[1] / Area) * 100;
                resultadosETABs[i].prequerida[2] = ((float)resultadosETABs[i].AsTopMediumButton[2] / Area) * 100;

                resultadosETABs[i].pasignada[0] = (resultadosETABs[i].As_asignado[0] / Area) * 100;
                resultadosETABs[i].pasignada[1] = (resultadosETABs[i].As_asignado[1] / Area) * 100;
                resultadosETABs[i].pasignada[2] = (resultadosETABs[i].As_asignado[2] / Area) * 100;




            }


        }

        public void AsignarAsTopMediumButton_()
        {
            for (int i = 0; i < resultadosETABs.Count; i++) { resultadosETABs[i].AsignarAsTopMediumButton(); }
        }

        #endregion Metodos-Calculos

        #region MetodosPaint

        public void Paint_(PaintEventArgs e, float HeightForm, float WidthForm, float SX, float SY, float WX1, float HY1, float XI, float YI, ISeccion seccion,bool MostrarLabels)
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

            if (BrushesColor != Brushes.Red)
            {
                if (Ready)
                {
                    if (BrushesColor != Brushes.Black)
                    {
                        BrushesColor = Brushes.Black;
                    }
                    else if (BrushesColor != Brushes.Blue)
                    {
                        BrushesColor = Brushes.Blue;
                    }
                }
                else
                {
                    if (BrushesColor != Brushes.Red)
                    {
                        BrushesColor = Brushes.Black;
                    }
                }
            }

            if (w > 15)
            {
                w = 15;
            }
            if (h > 15)
            {
                h = 15;
            }

            Graphics graphics = e.Graphics;

            if (seccion is CCirculo)
            {
                w = 0.35f * (SX + SY) * 0.5f;
                h = 0.35f * (SX + SY) * 0.5f;
                if (w > 10.5)
                {
                    w = 9;
                }

                GraphicsPath Path = new GraphicsPath();
                List<PointF> PuntosCircle = FunctionsProject.CreatePointsForCircle(100, w, X_Colum, Y_Colum);

                Path.AddClosedCurve(PuntosCircle.ToArray());
                graphics.DrawPath(Pens.Black, Path);
                graphics.FillPath(BrushesColor, Path);
            }
            else
            {
                graphics.DrawRectangle(Pens.Black, X_Colum - w / 2, Y_Colum - h / 2, w, h);
                graphics.FillRectangle(BrushesColor, X_Colum - w / 2, Y_Colum - h / 2, w, h);
            }

            float Tamano_Text = (SX + SY) * 0.6f * 0.3f;

            if (Tamano_Text > 15)
            {
                Tamano_Text = 15;
            }

            float X_string = X_Colum + w;

            if (X_string + 3 * Tamano_Text >= WidthForm)
            {
                X_string = X_Colum - w - 0.5f * SX;
            }

            float Y_string = Y_Colum;
            string NameAGraficar = Name;
            if (MostrarLabels)
            {
                NameAGraficar = Label;
            }
            graphics.DrawString(NameAGraficar, new Font("Calibri", Tamano_Text, FontStyle.Bold), Brushes.DarkRed, X_string, Y_string);
        }

        public Columna MouseDown(MouseEventArgs mouse)
        {
            if (X_Colum - w / 2 <= mouse.X && X_Colum + w / 2 >= mouse.X && Y_Colum - h / 2 <= mouse.Y && Y_Colum + h / 2 >= mouse.Y)
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

        public bool MouseMove(MouseEventArgs mouse, ref Cursor cursor)
        {
            RectangleF rectangle = new RectangleF(X_Colum - w / 2, Y_Colum - h / 2, w, h);
            GraphicsPath Path = new GraphicsPath();
            Path.AddRectangle(rectangle);
            Cursor new_cursor = Cursors.Default;

            if (Path.IsVisible(mouse.Location))
            {
                cursor = Cursors.Hand;
                return true;
            }
            if (cursor != new_cursor)
            {
                cursor = new_cursor;
            }
            return false;
        }

        public void MouseDobleClick(MouseEventArgs mouse)
        {
            if (X_Colum - w / 2 <= mouse.X && X_Colum + w / 2 >= mouse.X && Y_Colum - h / 2 <= mouse.Y && Y_Colum + h / 2 >= mouse.Y)
            {
                if (Ready == true)
                {
                    Ready = false;
                    ;
                }
                else { Ready = true; }
                Form1.m_Despiece.Invalidate();
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

            SolidBrush brush = new SolidBrush(Color.FromArgb(129, 111, 83));
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

            ////Nivel Fundación

            //float x5, y5, x6, y6;

            //x5 = CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][0];
            //y5 = -Y - CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][1] * SY + HeightForm;

            //x6 = CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][0] + WidthForm;
            //y6 = -Y - CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][1] * SY + HeightForm;

            //Pen Pen_Nivel = new Pen(Brushes.Red);
            //Pen_Nivel.DashPattern = new float[] { 5, 2, 15, 4 };
            //e.Graphics.DrawLine(Pen_Nivel, x5, y5, x6, y6);
        }

        #endregion MetodosPaint

        #region Metodos - Agregar Alzado Sugerido

        public void AgregarAlzadoSugerido()
        {
            Columna columna = this;

            Alzados.Clear();
            KgRefuerzoforColumAlzado.Clear();
            Col_Row_AlzadoBaseSugerido = new List<List<string>>();

            int CantAlzadosCrearPrimerPiso = 0;
            List<int> CambiosSeccion_CantAlzados = new List<int>();

            for (int i = columna.AlzadoBaseSugerido.Count - 1; i >= 0; i--)
            {
                if (i == columna.AlzadoBaseSugerido.Count - 1)
                {
                    CantAlzadosCrearPrimerPiso = columna.AlzadoBaseSugerido[i].Length;
                }

                try
                {
                    if (columna.Seccions[i].Item1.B != columna.Seccions[i - 1].Item1.B | columna.Seccions[i].Item1.H != columna.Seccions[i - 1].Item1.H)
                    {
                        CambiosSeccion_CantAlzados.Add(columna.AlzadoBaseSugerido[i - 1].Length);
                    }
                }
                catch
                {
                }
            }

            if (CambiosSeccion_CantAlzados.Count != 0)
            {
                for (int j = 0; j < CambiosSeccion_CantAlzados.Sum() + CantAlzadosCrearPrimerPiso; j++)
                {
                    Alzado alzado = new Alzado(j + 1, columna.Seccions.Count);
                    Alzados.Add(alzado);
                    Col_Row_AlzadoBaseSugerido.Add(new List<string>());
                }
            }
            else
            {
                for (int i = 0; i < CantAlzadosCrearPrimerPiso; i++)
                {
                    Alzado alzado = new Alzado(i + 1, columna.Seccions.Count);
                    Alzados.Add(alzado);
                    Col_Row_AlzadoBaseSugerido.Add(new List<string>());
                }
            }

            for (int i = 0; i < columna.Col_Row_AlzadoBaseSugerido.Count; i++)
            {
                for (int j = 0; j < columna.LuzLibre.Count; j++)
                {
                    columna.Col_Row_AlzadoBaseSugerido[i].Add("");
                }
            }

            int CorrerAlzado = 0;
            for (int i = columna.AlzadoBaseSugerido.Count - 1; i >= 0; i--)
            {
                try
                {
                    if (columna.Seccions[i].Item1.B != columna.Seccions[i - 1].Item1.B | columna.Seccions[i].Item1.H != columna.Seccions[i - 1].Item1.H)
                    {
                        CorrerAlzado += columna.AlzadoBaseSugerido[i - 1].Length;
                    }
                }
                catch { }
            }

            for (int i = columna.AlzadoBaseSugerido.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < columna.AlzadoBaseSugerido[i].Length; j++)
                {
                    string Combinacion = "";

                    if (j % 2 == 0) //Refuerzo Base Tipo 1
                    {
                        try
                        {
                            Combinacion = columna.AlzadoBaseSugerido[i][j];
                        }
                        catch
                        {
                            //   Combinacion = columna.AlzadoBaseSugerido[i][1];
                        }

                        if (columna.AlzadoBaseSugerido.Count == 1 | columna.AlzadoBaseSugerido.Count == 2)
                        {
                            Combinacion += "T4";
                        }
                        else
                        {
                            if (i == columna.AlzadoBaseSugerido.Count - 1)   //Si es el Primer Piso
                            {
                                Combinacion += "T1";
                            }
                            else if (i == 0) //Ultimo Piso
                            {
                                if (columna.AlzadoBaseSugerido.Count % 2 != 0)
                                {
                                    Combinacion += "T1";
                                }
                                else
                                {
                                    Combinacion += "T3";
                                }
                            }
                            else   //Resto de Pisos
                            {
                                if ((columna.AlzadoBaseSugerido.Count - i) % 2 == 0)
                                {
                                    Combinacion += "T2";
                                }

                                try
                                {
                                    if (columna.Seccions[i].Item1.B != columna.Seccions[i - 1].Item1.B | columna.Seccions[i].Item1.H != columna.Seccions[i - 1].Item1.H)
                                    {
                                        if ((columna.AlzadoBaseSugerido.Count - i) % 2 == 0)
                                        {
                                            Combinacion += "T3";
                                        }
                                        else
                                        { Combinacion += "T1"; }
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                    else  //Refuerzo Base Tipo 2
                    {
                        try
                        {
                            Combinacion = columna.AlzadoBaseSugerido[i][j];
                        }
                        catch
                        {
                            // Combinacion = columna.AlzadoBaseSugerido[i][1];
                        }

                        if (columna.AlzadoBaseSugerido.Count == 1 | columna.AlzadoBaseSugerido.Count == 2)
                        {
                            Combinacion += "T4";
                        }
                        else
                        {
                            if (i == columna.AlzadoBaseSugerido.Count - 1)   //Si es el Primer Piso
                            {
                                Combinacion += "T3";
                            }
                            else if (i == 0) //Ultimo Piso
                            {
                                if (columna.AlzadoBaseSugerido.Count % 2 != 0)
                                {
                                    Combinacion += "T3";
                                }
                                else
                                {
                                    Combinacion += "T1";
                                }
                            }
                            else   //Resto de Pisos
                            {
                                if ((columna.AlzadoBaseSugerido.Count - i) % 2 != 0)
                                {
                                    Combinacion += "T2";
                                }

                                if (columna.Seccions[i].Item1.B != columna.Seccions[i - 1].Item1.B | columna.Seccions[i].Item1.H != columna.Seccions[i - 1].Item1.H)
                                {
                                    if ((columna.AlzadoBaseSugerido.Count - i) % 2 == 0)
                                    {
                                        Combinacion += "T1";
                                    }
                                    else
                                    { Combinacion += "T3"; }
                                }
                            }
                        }
                    }

                    if (j + CorrerAlzado < columna.Col_Row_AlzadoBaseSugerido.Count)
                    {
                        columna.Col_Row_AlzadoBaseSugerido[j + CorrerAlzado][i] = Combinacion;
                    }
                    if (j == columna.AlzadoBaseSugerido[i].Length - 1)
                    {
                        try
                        {
                            if (columna.Seccions[i].Item1.B != columna.Seccions[i - 1].Item1.B | columna.Seccions[i].Item1.H != columna.Seccions[i - 1].Item1.H)
                            {
                                CorrerAlzado -= columna.AlzadoBaseSugerido[i - 1].Length;
                                break;
                            }
                        }
                        catch { }
                    }
                }
            }

            for (int i = 0; i < columna.Col_Row_AlzadoBaseSugerido.Count; i++)
            {
                for (int j = columna.Col_Row_AlzadoBaseSugerido[i].Count - 1; j >= 0; j--)
                {
                    CrearAlzado(i, j, columna, columna.Col_Row_AlzadoBaseSugerido[i][j]);
                }
            }

            ActualizarRefuerzo();

            // Acero Adiciónal
            int CantBarras = 0;
            int Dmenor = 99999;
            int MenorAux = 0;

            for (int i = 0; i < Alzados.Count; i++)
            {
                try
                {
                    CantBarras += Alzados[i].Colum_Alzado[Alzados[i].Colum_Alzado.Count - 1].CantBarras;
                }
                catch { }

                if (Alzados[i].Colum_Alzado.FindAll(X => X != null).ToList().Count != 0)
                {
                    MenorAux = Alzados[i].Colum_Alzado.FindAll(X => X != null).ToList().Min(x => x.NoBarra);
                }

                if (Dmenor > MenorAux)
                {
                    Dmenor = MenorAux;
                }
            }
            if (Alzados.Count != 0 && Dmenor != 0)
            {
                List<int> No_BarraaDecidir = new List<int>();

                No_BarraaDecidir.Add(Dmenor - 2);
                No_BarraaDecidir.Add(Dmenor - 1);
                No_BarraaDecidir.Add(Dmenor);
                No_BarraaDecidir.Add(Dmenor + 1);
                No_BarraaDecidir.Add(Dmenor + 2);

                for (int i = 0; i < No_BarraaDecidir.Count; i++)
                {
                    if (No_BarraaDecidir[i] < 4)
                    {
                        No_BarraaDecidir[i] = 4;
                    }
                }

                List<Tuple<string, int, int, int, int>> Conve_C_B_C_BP = new List<Tuple<string, int, int, int, int>>();

                List<List<string>> AcerosAdicionales_Sugerdio = new List<List<string>>();
                List<float> AceroMayorPorPiso = new List<float>();
                List<string> AdicionalArribaOAbajo = new List<string>();
                List<List<int>> NoBarras8DecisionesPorPiso = new List<List<int>>();
                List<int> NoBarrasPrimerPiso = new List<int>();
                List<List<int>> RestanteCantidad = new List<List<int>>();

                for (int i = resultadosETABs.Count - 1; i >= 0; i--)
                {
                    List<int> Aux = new List<int>(); Aux.Add(99999); Aux.Add(9999);
                    RestanteCantidad.Add(Aux);
                    AcerosAdicionales_Sugerdio.Add(new List<string>());
                    Conve_C_B_C_BP.Add(new Tuple<string, int, int, int, int>("", 0, 0, 0, 0));
                    AceroMayorPorPiso.Add(0);
                    AdicionalArribaOAbajo.Add("");
                    NoBarras8DecisionesPorPiso.Add(new List<int>());
                }

                for (int i = resultadosETABs.Count - 1; i >= 0; i--)
                {
                    //Econtrar Acero Mayor Entre Top del mismo piso y Bottom del piso vecino
                    try
                    {
                        float AceroQueFalta1 = (float)(resultadosETABs[i].AsTopMediumButton[0] - resultadosETABs[i].As_asignado[0]);

                        if (resultadosETABs[i].Porct_Refuerzo[0] <= 105f & resultadosETABs[i].Porct_Refuerzo[0] >= 95f)
                        {
                            AceroQueFalta1 = 0;
                        }

                        float AceroQueFalta2 = (float)(resultadosETABs[i - 1].AsTopMediumButton[2] - resultadosETABs[i - 1].As_asignado[2]);
                        if (resultadosETABs[i - 1].Porct_Refuerzo[2] <= 105f & resultadosETABs[i - 1].Porct_Refuerzo[2] >= 95f)
                        {
                            AceroQueFalta2 = 0;
                        }

                        if (resultadosETABs[i].Porct_Refuerzo[0] < 95f)
                        {
                            AdicionalArribaOAbajo[i] = "-";
                        }
                        if (resultadosETABs[i - 1].Porct_Refuerzo[2] < 95f)
                        {
                            AdicionalArribaOAbajo[i] = "+";
                        }
                        if (resultadosETABs[i - 1].Porct_Refuerzo[2] < 95f && resultadosETABs[i].Porct_Refuerzo[0] < 95)
                        {
                            AdicionalArribaOAbajo[i] = "";
                        }
                        AceroMayorPorPiso[i] = AceroQueFalta1 > AceroQueFalta2 ? AceroQueFalta1 : AceroQueFalta2;
                    }
                    catch
                    {
                        AceroMayorPorPiso[i] = (float)(resultadosETABs[i].AsTopMediumButton[0] - resultadosETABs[i].As_asignado[0]);
                        if (resultadosETABs[i].Porct_Refuerzo[0] <= 105f & resultadosETABs[i].Porct_Refuerzo[0] >= 95f)
                        {
                            AceroMayorPorPiso[i] = 0;
                        }
                    }

                    //Agregar Barras  con Redondeo a Mas
                    for (int j = 0; j < No_BarraaDecidir.Count; j++)
                    {
                        int Decision = FunctionsProject.Redondear_Entero(AceroMayorPorPiso[i] / Form1.Proyecto_.AceroBarras[No_BarraaDecidir[j]], 4);
                        NoBarras8DecisionesPorPiso[i].Add(Decision);
                        if (i == resultadosETABs.Count - 1)
                        {
                            float AceroPrimerPiso = (float)(resultadosETABs[i].AsTopMediumButton[2] - resultadosETABs[i].As_asignado[2]);
                            if (AceroPrimerPiso < 0) { AceroPrimerPiso = 0; }
                            Decision = FunctionsProject.Redondear_Entero(AceroPrimerPiso / Form1.Proyecto_.AceroBarras[No_BarraaDecidir[j]], 4);
                            NoBarrasPrimerPiso.Add(Decision);
                        }
                    }
                    //Agregar Barras  con Redondeo a Menos
                    for (int j = 0; j < No_BarraaDecidir.Count; j++)
                    {
                        int Decision = FunctionsProject.Redondear_Entero(AceroMayorPorPiso[i] / Form1.Proyecto_.AceroBarras[No_BarraaDecidir[j]], 4, true);
                        NoBarras8DecisionesPorPiso[i].Add(Decision);

                        if (i == resultadosETABs.Count - 1)
                        {
                            float AceroPrimerPiso = (float)(resultadosETABs[i].AsTopMediumButton[2] - resultadosETABs[i].As_asignado[2]);
                            if (AceroPrimerPiso > 0) { AceroPrimerPiso = 0; }
                            Decision = FunctionsProject.Redondear_Entero(AceroPrimerPiso / Form1.Proyecto_.AceroBarras[No_BarraaDecidir[j]], 4, true);
                            NoBarrasPrimerPiso.Add(Decision);
                        }
                    }
                }

                for (int i = NoBarras8DecisionesPorPiso.Count - 1; i >= 0; i--)
                {
                    int BarraAdeci = 0;
                    int CantBarrasaDecidir = 0;

                    string AlzadoAdicionalPrimerPiso = "";
                    int CantBarrasaDecidir_PrimerPiso = 0;
                    int BarraAdeci_PrimerPiso = 0;

                    Tuple<int, int> Resultados = CantBarrasaDecidir_BarraAdeci(AceroMayorPorPiso[i], NoBarras8DecisionesPorPiso[i], No_BarraaDecidir, CantBarras);

                    CantBarrasaDecidir = Resultados.Item1;
                    BarraAdeci = Resultados.Item2;

                    //PrimerPiso
                    if (i == NoBarras8DecisionesPorPiso.Count - 1)
                    {
                        float AceroPrimerPiso = (float)(resultadosETABs[i].AsTopMediumButton[2] - resultadosETABs[i].As_asignado[2]);
                        if (AceroPrimerPiso > 0 & resultadosETABs[i].Porct_Refuerzo[2] < 95f)
                        {
                            Tuple<int, int> Resultados2 = CantBarrasaDecidir_BarraAdeci(AceroPrimerPiso, NoBarrasPrimerPiso, No_BarraaDecidir, CantBarras);
                            CantBarrasaDecidir_PrimerPiso = Resultados2.Item1;
                            BarraAdeci_PrimerPiso = Resultados2.Item2;
                            AlzadoAdicionalPrimerPiso = "-" + CantBarrasaDecidir_PrimerPiso + "#" + BarraAdeci_PrimerPiso;
                        }
                    }

                    Conve_C_B_C_BP[i] = new Tuple<string, int, int, int, int>(AdicionalArribaOAbajo[i], CantBarrasaDecidir, BarraAdeci, CantBarrasaDecidir_PrimerPiso, BarraAdeci_PrimerPiso);

                    RestanteCantidad[i][0] = CantBarrasaDecidir;
                    RestanteCantidad[i][1] = CantBarrasaDecidir_PrimerPiso;
                }

                int Menor = 0;
                while (Menor != 99999)
                {
                    Menor = 99999;
                    for (int i = RestanteCantidad.Count - 1; i >= 0; i--)
                    {
                        if (Menor > RestanteCantidad[i][0] && RestanteCantidad[i][0] > 0)
                        {
                            Menor = RestanteCantidad[i][0];
                        }

                        if (Menor > RestanteCantidad[i][1] && RestanteCantidad[i][1] > 0)
                        {
                            Menor = RestanteCantidad[i][1];
                        }
                    }

                    for (int i = RestanteCantidad.Count - 1; i >= 0; i--)
                    {
                        if (RestanteCantidad[i][0] - Menor >= 0)
                        {
                            string ConvencioBarra = Conve_C_B_C_BP[i].Item1 + Menor + "#" + Conve_C_B_C_BP[i].Item3 + "A";
                            AcerosAdicionales_Sugerdio[i].Add(ConvencioBarra);
                        }

                        if (RestanteCantidad[i][1] - Menor >= 0)
                        {
                            if (AcerosAdicionales_Sugerdio[i].Count > 0)
                            {
                                bool Adicionar = true;
                                for (int s = 0; s < AcerosAdicionales_Sugerdio[i].Count; s++)
                                {
                                    int CantidaddeNumeral = (from c in AcerosAdicionales_Sugerdio[i][s] where c == Convert.ToChar("#") select c).Count();
                                    if (CantidaddeNumeral < 2 & AcerosAdicionales_Sugerdio[i][s].Contains("A"))
                                    {
                                        int Inicial = 0;
                                        //Encontrar Cantidad
                                        if (AcerosAdicionales_Sugerdio[i][s].Contains("+") | AcerosAdicionales_Sugerdio[i][s].Contains("-"))
                                        {
                                            Inicial += 1;
                                        }
                                        int Final = AcerosAdicionales_Sugerdio[i][s].IndexOf("#");

                                        int Cant = Convert.ToInt32(AcerosAdicionales_Sugerdio[i][s].Substring(Inicial, Final - Inicial));

                                        if (Cant == Menor)
                                        {
                                            AcerosAdicionales_Sugerdio[i][s] += "-" + Menor + "#" + Conve_C_B_C_BP[i].Item5;
                                            Adicionar = false;
                                            break;
                                        }
                                    }
                                }

                                if (Adicionar)
                                {
                                    string ConvencioBarra = "-" + Menor + "#" + Conve_C_B_C_BP[i].Item5;
                                    AcerosAdicionales_Sugerdio[i].Add(ConvencioBarra);
                                }
                            }
                            else
                            {
                                string ConvencioBarra = "-" + Menor + "#" + Conve_C_B_C_BP[i].Item5;
                                AcerosAdicionales_Sugerdio[i].Add(ConvencioBarra);
                            }
                        }
                    }

                    for (int i = RestanteCantidad.Count - 1; i >= 0; i--)
                    {
                        RestanteCantidad[i][0] -= Menor;
                        RestanteCantidad[i][1] -= Menor;
                    }
                }

                int CantAlzadosAnteriores = Alzados.Count;
                int CantAlzadosACrear = AcerosAdicionales_Sugerdio.Max(x => x.Count);

                for (int i = 0; i < CantAlzadosACrear; i++)
                {
                    Alzado alzado = new Alzado(Alzados[Alzados.Count - 1].ID + 1, NoBarras8DecisionesPorPiso.Count);
                    Alzados.Add(alzado);
                }
                for (int i = AcerosAdicionales_Sugerdio.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < AcerosAdicionales_Sugerdio[i].Count; j++)
                    {
                        CrearAlzado(CantAlzadosAnteriores + j, i, this, AcerosAdicionales_Sugerdio[i][j]);
                    }
                }

                for (int i = 0; i < columna.Alzados.Count; i++)
                {
                    ModificarTraslapo(i, this);

                    DeterminarCoordAlzado(i, this);
                }
            }
            ActualizarRefuerzo();
            CrearListaPesosRefuerzos(0);
            CalcularPesoAcero();
        }

        private Tuple<int, int> CantBarrasaDecidir_BarraAdeci(float AceroRestante, List<int> CantBarrasaDecidir, List<int> No_BarraaDecidir, int CantBarras)
        {
            float DeltaAceroMenor = 999999;
            int BarraAdeci = 0;
            int CantBarrasaDecidir_A = 0;

            List<Tuple<int, float, int>> NoBarras_Delta_Posibles_Barra = new List<Tuple<int, float, int>>();

            if (AceroRestante != 0)
            {
                int IndiceBarra = 0;
                for (int j = 0; j < CantBarrasaDecidir.Count; j++)
                {
                    float DeltaAcero = Math.Abs((float)(CantBarrasaDecidir[j] * Form1.Proyecto_.AceroBarras[No_BarraaDecidir[IndiceBarra]] - AceroRestante));
                    if (CantBarrasaDecidir[j] == 0)
                    {
                        DeltaAcero = 99999;
                    }

                    DeltaAceroMenor = DeltaAcero;
                    BarraAdeci = No_BarraaDecidir[IndiceBarra];
                    CantBarrasaDecidir_A = CantBarrasaDecidir[j];
                    if (CantBarrasaDecidir_A <= CantBarras)
                    {
                        NoBarras_Delta_Posibles_Barra.Add(new Tuple<int, float, int>(CantBarrasaDecidir_A, DeltaAceroMenor, BarraAdeci));
                    }
                    IndiceBarra += 1;
                    if (IndiceBarra >= No_BarraaDecidir.Count)
                    {
                        IndiceBarra = 0;
                    }
                }
            }

            try { DeltaAceroMenor = NoBarras_Delta_Posibles_Barra.Min(x => x.Item2); } catch { DeltaAceroMenor = 999999; }

            try
            {
                CantBarrasaDecidir_A = NoBarras_Delta_Posibles_Barra.Find(x => x.Item2 == DeltaAceroMenor).Item1;
                BarraAdeci = NoBarras_Delta_Posibles_Barra.Find(x => x.Item2 == DeltaAceroMenor).Item3;
            }
            catch { CantBarrasaDecidir_A = 0; }

            return new Tuple<int, int>(CantBarrasaDecidir_A, BarraAdeci);
        }

        public void CrearAlzado(int IndiceC, int IndiceR, Columna ColumnaSelect, string ValorCelda)
        {
            if (IndiceR < ColumnaSelect.LuzLibre.Count)
            {
                object[] Clasficiacion = ClasificarCelda(ValorCelda);

                if (Clasficiacion[0].ToString() != "Error")
                {
                    int Cant_Barras = (int)Clasficiacion[1];
                    int NoBarra = (int)Clasficiacion[2];
                    int NoPiso = ColumnaSelect.LuzLibre.Count - IndiceR;
                    float H = ColumnaSelect.LuzLibre[IndiceR];
                    float Hviga = ColumnaSelect.VigaMayor.Seccions[IndiceR].Item1.H;
                    string T = (string)Clasficiacion[3];
                    string Tipo2 = (string)Clasficiacion[7];
                    float HV1, HV2, HVV1, HVV2, Hacum; bool UltimPiso = false;
                    Hacum = 0;
                    try { HV1 = ColumnaSelect.LuzLibre[IndiceR + 1]; HVV1 = ColumnaSelect.Seccions[IndiceR + 1].Item1.H; } catch { HV1 = 0; HVV1 = 0; }
                    try { HV2 = ColumnaSelect.LuzLibre[IndiceR - 1]; HVV2 = ColumnaSelect.Seccions[IndiceR - 1].Item1.H; } catch { HV2 = 0; HVV2 = 0; }

                    for (int i = ColumnaSelect.LuzLibre.Count - 1; i >= IndiceR; i--)
                    {
                        Hacum += ColumnaSelect.LuzLibre[i] + ColumnaSelect.VigaMayor.Seccions[i].Item1.H;
                    }
                    Hacum += Form1.Proyecto_.e_Fundacion;

                    if (NoPiso == ColumnaSelect.LuzLibre.Count)
                    {
                        UltimPiso = true;
                    }

                    AlzadoUnitario unitario = new AlzadoUnitario(Cant_Barras, NoBarra, T, NoPiso, IndiceC + 1, H, Hviga, Form1.Proyecto_.e_Fundacion, UltimPiso, Hacum);
                    unitario.Tipo2 = Tipo2;
                    //AÑADIR REFUERZO ADICIONAL

                    string RefAd_Str = (string)Clasficiacion[4];

                    if (RefAd_Str == "-")
                    {
                        int CantBarrasA = (int)Clasficiacion[5];
                        int NoBarraA = (int)Clasficiacion[6];
                        unitario.UnitarioAdicional = new AlzadoUnitario(CantBarrasA, NoBarraA, "Botton", NoPiso, IndiceC + 1, H, Hviga, Form1.Proyecto_.e_Fundacion, UltimPiso, Hacum);
                    }

                    ColumnaSelect.Alzados[IndiceC].Colum_Alzado[IndiceR] = unitario;
                }
                else
                {
                    ColumnaSelect.Alzados[IndiceC].Colum_Alzado[IndiceR] = null;
                }
            }
            else if (IndiceR < ColumnaSelect.LuzLibre.Count)
            {
                ColumnaSelect.Alzados[IndiceC].Colum_Alzado[IndiceR] = null;
            }
        }

        private object[] ClasificarCelda(string Celda)
        {
            object[] Clasf = new object[] { "Error" };

            if (Celda.Contains("#"))
            {
                int CantidadBarras = 0;
                int NoBarra = 0;
                string Traslap = "";
                int CantidadBarrasA = 0;
                int NoBarraA = 0;
                string Raya = "";
                string Tipo2 = "";

                int VecesNumeral = 0;
                for (int i = 0; i < Celda.Length; i++)
                {
                    if (Celda.Substring(i, 1) == "#" && VecesNumeral < 1)
                    {
                        VecesNumeral += 1;
                        try
                        {
                            CantidadBarras = Convert.ToInt32(Celda.Substring(0, i));
                            if (Celda.Substring(0, 1) == "-" | Celda.Substring(0, 1) == "+")
                            {
                                Tipo2 = Celda.Substring(0, 1);
                            }
                        }
                        catch
                        {
                            CantidadBarras = 0;
                        }
                        try
                        {
                            NoBarra = Convert.ToInt32(Celda.Substring(i + 1, 2));
                        }
                        catch
                        {
                            try
                            {
                                NoBarra = Convert.ToInt32(Celda.Substring(i + 1, 1));
                            }
                            catch { NoBarra = 0; }
                        }
                    }
                    if (Celda.Substring(i, 1) == "T")
                    {
                        Traslap = Celda.Substring(i);
                    }

                    if (Celda.Substring(i, 1) == "A")
                    {
                        Traslap = Celda.Substring(i, 1);

                        string AuxAd = Celda.Substring(i);

                        if (AuxAd.Contains("-"))
                        {
                            Raya = "-";
                            for (int j = 0; j < AuxAd.Length; j++)
                            {
                                if (AuxAd.Substring(j, 1) == "#")
                                {
                                    int InicioRaya = AuxAd.IndexOf("-") + 1;

                                    CantidadBarrasA = Convert.ToInt32(AuxAd.Substring(InicioRaya, j - InicioRaya));
                                    try
                                    {
                                        NoBarraA = Convert.ToInt32(AuxAd.Substring(j + 1, 2));
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            NoBarraA = Convert.ToInt32(AuxAd.Substring(j + 1, 1));
                                        }
                                        catch { NoBarraA = 0; }
                                    }
                                }
                            }
                        }
                    }
                }
                try
                {
                    var o = Form1.Proyecto_.AceroBarras[NoBarra];
                    if (Raya == "-") { var m = Form1.Proyecto_.AceroBarras[NoBarraA]; }
                    if (CantidadBarras < 0)
                    {
                        CantidadBarras = -CantidadBarras;
                        if (Traslap != "A")
                        {
                            Traslap = "Botton";
                            Tipo2 = "";
                        }
                        else
                        {
                            Traslap = "A";
                        }
                    }
                    Clasf = new object[] { "Ok", CantidadBarras, NoBarra, Traslap, Raya, CantidadBarrasA, NoBarraA, Tipo2 };
                }
                catch
                {
                }
            }

            return Clasf;
        }

        public void DeterminarCoordAlzado(int Col, Columna ColumnaSelect)
        {
            float PR = Form1.Proyecto_.P_R;
            float eF = Form1.Proyecto_.e_Fundacion;
            float r = eF - PR;
            float r2 = Form1.Proyecto_.R / 100;

            float LdAd = 0.4f;

            Alzado a = ColumnaSelect.Alzados[Col];
            float diX = 0;
            //Agregar Distancia X a cada Alzado
            for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
            {
                AlzadoUnitario au = a.Colum_Alzado[i];
                if (au != null)
                {
                    au.x1 = diX;
                }

                diX += 0.1f;
                if (diX > 0.1f)
                {
                    diX = 0f;
                }
            }

            //Agregar Distancias entre Alzados
            for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
            {
                AlzadoUnitario au = a.Colum_Alzado[i];
                if (au != null)
                {
                    for (int j = a.Colum_Alzado.Count - 1; j >= i; j--)
                    {
                        AlzadoUnitario au2 = a.Colum_Alzado[j];

                        if (i != j && au2 != null)
                        {
                            if (au.Tipo == "T2" && au2.Tipo == "T2" || au.Tipo == "T2" && au2.Tipo == "T3" || au.Tipo == "T2" && au2.Tipo == "T1" || au.Tipo == "T2" && au2.Tipo == "T4")
                            {
                                if (au.x1 == au2.x1)
                                {
                                    au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                }
                            }

                            if (au.Tipo == "T4" && au2.Tipo == "T4")
                            {
                                au.x1 = au2.x1;
                            }

                            if (au.Tipo == "T1" && au2.Tipo == "T2" || au.Tipo == "T1" && au2.Tipo == "T4")
                            {
                                if (au.x1 == au2.x1)
                                {
                                    au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                }
                            }
                            if (au.Tipo == "T3" && au2.Tipo == "T3" || au.Tipo == "T3" && au2.Tipo == "T2" || au.Tipo == "T3" && au2.Tipo == "T4")
                            {
                                if (au.x1 == au2.x1)
                                {
                                    au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                }
                            }
                        }
                    }
                }
            }

            for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
            {
                AlzadoUnitario au = a.Colum_Alzado[i];

                if (au != null)
                {
                    float Hacum1 = 0; float Hviga1 = 0; float Hacum2 = 0; float Hviga2 = 0; float H1 = 0; float H2 = 0; string Nom3 = "Not"; string Nom4 = "Not"; float x31 = 0; float x41 = 0;
                    string Nom1 = "Not"; string Nom2 = "Not"; float x11 = 0; float x12 = 0;
                    au.Coord_Alzado_PB = new List<float[]>();

                    #region Determinar Variables de Pisos Vecinos

                    //Determinar Variables de Pisos Vecinos

                    try
                    {
                        Nom3 = a.Colum_Alzado[i + 2].Tipo;
                        x31 = a.Colum_Alzado[i + 2].x1;
                    }
                    catch
                    { }

                    try
                    {
                        Nom4 = a.Colum_Alzado[i - 2].Tipo;
                        x41 = a.Colum_Alzado[i - 2].x1;
                    }
                    catch
                    { }

                    try
                    {
                        Nom1 = a.Colum_Alzado[i + 1].Tipo;
                        x11 = a.Colum_Alzado[i + 1].x1;
                    }
                    catch { }

                    try
                    {
                        Nom2 = a.Colum_Alzado[i - 1].Tipo;
                        x12 = a.Colum_Alzado[i - 1].x1;
                    }
                    catch { }

                    try
                    {
                        Hacum1 = ColumnaSelect.LuzAcum[i + 1];
                        Hviga1 = ColumnaSelect.VigaMayor.Seccions[i + 1].Item1.H;
                        H1 = ColumnaSelect.LuzLibre[i + 1];
                    }
                    catch { }
                    try
                    {
                        Hacum2 = ColumnaSelect.LuzAcum[i - 1];
                        Hviga2 = ColumnaSelect.VigaMayor.Seccions[i - 1].Item1.H;
                        H2 = ColumnaSelect.LuzLibre[i - 1];
                    }
                    catch { }

                    #endregion Determinar Variables de Pisos Vecinos

                    float DisG = Form1.Proyecto_.G90[au.NoBarra];
                    if (au.NoStory == 1 && au.Tipo == "T1") //Si es Primer Piso y  Si Tiene Traslapo Tipo1
                    {
                        float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                        float[] XY2 = new float[] { au.x1 + a.DistX, r };
                        float[] XY3 = new float[] { au.x1 + a.DistX, eF + au.H_Stroy / 2 + au.Traslapo / 2 };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);
                    }

                    if (au.NoStory != 1 && au.Tipo == "T1")
                    {
                        float[] XY1 = new float[] { au.x1 + a.DistX, au.Hacum - au.Hviga - au.H_Stroy / 2 - au.Traslapo / 2 };
                        float[] XY2 = new float[] { au.x1 + a.DistX, au.Hacum - r2 };
                        float[] XY3 = new float[] { au.x1 + a.DistX + DisG, au.Hacum - r2 };

                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);
                    }
                    else if (au.NoStory == 1 && au.Tipo == "T3") //Si es Primer Piso y  Si Tiene Traslapo Tipo 3
                    {
                        float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                        float[] XY2 = new float[] { au.x1 + a.DistX, r };
                        float[] XY3 = new float[] { au.x1 + a.DistX, Hacum2 - Hviga2 - H2 / 2 + au.Traslapo / 2 };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);
                    }
                    else if (au.NoStory != 1 && au.Tipo == "T3") // Traslapo Tipo 3
                    {
                        float[] XY1 = new float[] { au.x1 + a.DistX, Hacum1 - Hviga1 - H1 / 2 - au.Traslapo / 2 };
                        float[] XY2 = new float[] { au.x1 + a.DistX, au.Hacum - r2 };
                        float[] XY3 = new float[] { au.x1 + a.DistX + DisG, au.Hacum - r2 };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);
                    }

                    if (au.UltPiso == false && au.NoStory != 1 && au.Tipo == "T2")
                    {
                        float[] XY1 = new float[] { au.x1 + a.DistX, au.Hacum + H2 / 2 + au.Traslapo / 2 };
                        float[] XY2 = new float[] { au.x1 + a.DistX, Hacum1 - Hviga1 - H1 / 2 - au.Traslapo / 2 };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                    }

                    if (au.NoStory == 1 && au.Tipo == "T4") //Traslapo Tipo 4
                    {
                        if (Nom2 != "T4")
                        {
                            float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                            float[] XY2 = new float[] { au.x1 + a.DistX, r };
                            float[] XY3 = new float[] { au.x1 + a.DistX, au.Hacum - r2 };
                            float[] XY4 = new float[] { au.x1 + a.DistX + DisG, au.Hacum - r2 };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3); au.Coord_Alzado_PB.Add(XY4);
                        }
                        else
                        {
                            float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                            float[] XY2 = new float[] { au.x1 + a.DistX, r };
                            float[] XY3 = new float[] { au.x1 + a.DistX, Hacum2 - r2 };
                            float[] XY4 = new float[] { au.x1 + a.DistX + DisG, Hacum2 - r2 };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3); au.Coord_Alzado_PB.Add(XY4);
                        }
                    }

                    if (au.UltPiso == false && au.Tipo == "A")  //Refuerzo Adicional Parte Superior "Cualquier Piso"
                    {
                        if (Nom1 == "A")
                        {
                            if (a.Colum_Alzado[i + 1].CantBarras + "#" + a.Colum_Alzado[i + 1].NoBarra == au.CantBarras + "#" + au.NoBarra)
                            {
                                if (a.Colum_Alzado[i + 1].Traslapo + LdAd + au.Traslapo + LdAd >= au.H_Stroy)
                                {
                                    if (a.Colum_Alzado[i + 1].Tipo2 == "+")
                                    {
                                        a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();
                                        float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                        float[] XY2 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum - au.Traslapo };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                    else
                                    {
                                        a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();
                                        float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                        float[] XY2 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum - a.Colum_Alzado[i + 1].Hviga - au.Traslapo - LdAd };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                }
                                else

                                {
                                    if (au.Tipo2 == "-")
                                    {
                                        float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum - au.Hviga + au.Traslapo };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                    else if (au.Tipo2 == "+")
                                    {
                                        float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum - au.Traslapo };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                    else
                                    {
                                        float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                }
                            }
                            else
                            {
                                if (au.Tipo2 == "-")
                                {
                                    float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - au.Hviga + au.Traslapo };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                }
                                else if (au.Tipo2 == "+")
                                {
                                    float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - au.Traslapo };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                }
                                else
                                {
                                    float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                }
                            }
                        }
                        else
                        {
                            if (au.Tipo2 == "-")
                            {
                                float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, au.Hacum - au.Hviga + au.Traslapo };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                            else if (au.Tipo2 == "+")
                            {
                                float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                float[] XY2 = new float[] { a.DistX, au.Hacum - au.Traslapo };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                            else
                            {
                                float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                        }
                    }

                    if (au.NoStory != 1 && au.UnitarioAdicional != null)  //Refuerzo Adicional Parte Inferior "Cualquier Piso"
                    {
                        if (au.CantBarras + "#" + au.NoBarra == au.UnitarioAdicional.CantBarras + "#" + au.UnitarioAdicional.NoBarra & au.UnitarioAdicional.UltPiso == false)
                        {
                            if (au.Traslapo + LdAd + au.UnitarioAdicional.Traslapo + LdAd >= au.H_Stroy)
                            {
                                au.Coord_Alzado_PB = new List<float[]>();
                                au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, au.Hacum + au.UnitarioAdicional.Traslapo + LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                            else
                            {
                                au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, Hacum1 + au.UnitarioAdicional.Traslapo + LdAd };
                                au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2);
                            }
                        }
                        else
                        {
                            au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                            float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                            float[] XY2 = new float[] { a.DistX, Hacum1 + au.UnitarioAdicional.Traslapo + LdAd };
                            au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2);
                        }
                    }
                    if (au.NoStory == 1 && au.UnitarioAdicional != null)  //Primer Piso Con Refuerzo Adicional Parte Inferior
                    {
                        if (au.CantBarras + "#" + au.NoBarra == au.UnitarioAdicional.CantBarras + "#" + au.UnitarioAdicional.NoBarra)
                        {
                            if (au.Traslapo + LdAd + au.UnitarioAdicional.Traslapo + LdAd >= au.H_Stroy)
                            {
                                au.Coord_Alzado_PB = new List<float[]>();
                                au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                float[] XY1 = new float[] { a.DistX + DisG, r };
                                float[] XY2 = new float[] { a.DistX, r };
                                float[] XY3 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                            }
                            else
                            {
                                au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                float[] XY1 = new float[] { a.DistX + DisG, r };
                                float[] XY2 = new float[] { a.DistX, r };
                                float[] XY3 = new float[] { a.DistX, eF + au.UnitarioAdicional.Traslapo + LdAd };
                                au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY3);
                            }
                        }
                        else
                        {
                            au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                            float[] XY1 = new float[] { a.DistX + DisG, r };
                            float[] XY2 = new float[] { a.DistX, r };
                            float[] XY3 = new float[] { a.DistX, eF + au.UnitarioAdicional.Traslapo + LdAd };
                            au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY3);
                        }
                    }

                    if (au.UltPiso && au.Tipo == "A")   // Ultimo Piso Con Refuerzo Adicional Parte Superior
                    {
                        if (Nom1 == "A")
                        {
                            if (a.Colum_Alzado[i + 1].CantBarras + "#" + a.Colum_Alzado[i + 1].NoBarra == au.CantBarras + "#" + au.NoBarra)
                            {
                                if (a.Colum_Alzado[i + 1].Traslapo + LdAd + au.Traslapo + LdAd >= au.H_Stroy)
                                {
                                    if (a.Colum_Alzado[i + 1].Tipo2 == "+")
                                    {
                                        a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();
                                        float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                        float[] XY3 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum - au.Traslapo };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                    }
                                    else
                                    {
                                        a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();
                                        float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                        float[] XY3 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum - a.Colum_Alzado[i + 1].Hviga - au.Traslapo - LdAd };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                    }
                                }
                                else
                                {
                                    float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                    float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                }
                            }
                            else
                            {
                                float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                            }
                        }
                        else if (au.UnitarioAdicional != null)
                        {
                            if (au.UnitarioAdicional.CantBarras + "#" + au.UnitarioAdicional.NoBarra == au.CantBarras + "#" + au.NoBarra)
                            {
                                if (au.Traslapo + LdAd + au.Traslapo + LdAd >= au.H_Stroy)
                                {
                                    au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                    float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                    float[] XY3 = new float[] { a.DistX, Hacum1 - Hviga1 - au.UnitarioAdicional.Traslapo - LdAd };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                }
                                else
                                {
                                    float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                    float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                }
                            }
                            else
                            {
                                float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                            }
                        }
                        else
                        {
                            float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                            float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                            float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                        }
                    }

                    if (au.NoStory != 1 && au.Tipo == "Botton")  //Refuerzo Adicional Parte Inferior "Cualquier Piso"
                    {
                        if (Nom1 == "Botton")
                        {
                            if (au.ToString() == a.Colum_Alzado[i + 1].ToString() && a.Colum_Alzado[i + 1].NoStory == 1 && au.Traslapo + a.Colum_Alzado[i + 1].Traslapo + LdAd * 2 >= a.Colum_Alzado[i + 1].H_Stroy)
                            {
                                au.Coord_Alzado_PB = new List<float[]>();
                                a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();

                                float[] XY1 = new float[] { a.DistX + DisG, r };
                                float[] XY2 = new float[] { a.DistX, r };
                                float[] XY3 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum + au.Traslapo + LdAd };
                                a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY1); a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY2); a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY3);
                            }
                            else if (au.ToString() == a.Colum_Alzado[i + 1].ToString() && a.Colum_Alzado[i + 1].NoStory != 1 && au.Traslapo + a.Colum_Alzado[i + 1].Traslapo + LdAd * 2 >= a.Colum_Alzado[i + 1].H_Stroy)
                            {
                                au.Coord_Alzado_PB = new List<float[]>();
                                a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();

                                float[] XY1 = new float[] { a.DistX, a.Colum_Alzado[i - 1].Hacum - a.Colum_Alzado[i - 1].Hviga - LdAd - au.Traslapo };
                                float[] XY2 = new float[] { a.DistX, au.Hacum + LdAd + au.Traslapo };
                                a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY1); a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY2);
                            }
                            else
                            {
                                float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, Hacum1 + au.Traslapo + LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                        }
                        else
                        {
                            float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                            float[] XY2 = new float[] { a.DistX, Hacum1 + au.Traslapo + LdAd };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                        }
                    }

                    if (au.NoStory == 1 && au.Tipo == "Botton")  //Primer Piso Con Refuerzo Adicional Parte Inferior
                    {
                        float[] XY1 = new float[] { a.DistX + DisG, r };
                        float[] XY2 = new float[] { a.DistX, r };
                        float[] XY3 = new float[] { a.DistX, eF + au.Traslapo + LdAd };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                    }

                    if (Form1.Proyecto_.Redondear)
                    {
                        ModificarCoordParaEntero(au);
                        if (au.UnitarioAdicional != null)
                        {
                            ModificarCoordParaEntero(au.UnitarioAdicional);
                        }
                    }
                }
            }
        }

        private void ModificarCoordParaEntero(AlzadoUnitario au)
        {
            if (au.Coord_Alzado_PB != null)
            {
                if (au.Coord_Alzado_PB.Count != 0)
                {
                    float LongitudInicial = (float)Math.Round(au.CalcularLongitudRefuerzo(au.Coord_Alzado_PB), 2);
                    float LongitudQueDebeSer = FunctionsProject.RedondearDecimales(LongitudInicial, 5);

                    if (LongitudInicial != LongitudQueDebeSer)
                    {
                        float Faltante = (float)Math.Round(Math.Abs(LongitudInicial - LongitudQueDebeSer), 2);

                        if (au.Coord_Alzado_PB.Count == 2)
                        {
                            if (au.Coord_Alzado_PB[0][1] > au.Coord_Alzado_PB[1][1])
                            {
                                au.Coord_Alzado_PB[0][1] += Faltante / 2;
                                au.Coord_Alzado_PB[1][1] -= Faltante / 2;
                            }
                            else
                            {
                                au.Coord_Alzado_PB[0][1] -= Faltante / 2;
                                au.Coord_Alzado_PB[1][1] += Faltante / 2;
                            }
                        }
                        if (au.Coord_Alzado_PB.Count == 3)
                        {
                            if (au.NoStory != 1)
                            {
                                if (au.Coord_Alzado_PB[2][1] > au.Coord_Alzado_PB[0][1])
                                {
                                    au.Coord_Alzado_PB[0][1] -= Faltante;
                                }
                                else
                                {
                                    au.Coord_Alzado_PB[2][1] -= Faltante;
                                }
                            }
                            else
                            {
                                au.Coord_Alzado_PB[2][1] += Faltante;
                            }
                        }
                    }
                }
            }
        }

        public void ModificarTraslapo(int Indice, Columna columna)
        {
            for (int i = columna.Alzados[Indice].Colum_Alzado.Count - 1; i >= 0; i--)
            {
                if (columna.Alzados[Indice].Colum_Alzado[i] != null)
                {
                    float TraslapoFinal;
                    if (columna.Alzados[Indice].Colum_Alzado[i].Tipo.Contains("T"))
                    {
                        float TV1 = 0; float TV2 = 0; float TI = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i].NoBarra, columna.Seccions[i].Item1.Material.FC);

                        try { TV1 = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra, columna.Seccions[i + 1].Item1.Material.FC); } catch { }
                        try { TV2 = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i - 1].NoBarra, columna.Seccions[i - 1].Item1.Material.FC); } catch { }
                        if (TV1 < TV2)
                        {
                            TV1 = TV2;
                        }
                        TraslapoFinal = TI > TV1 ? TI : TV1;
                    }
                    else
                    {
                        TraslapoFinal = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i].NoBarra, columna.Seccions[i].Item1.Material.FC);
                    }

                    columna.Alzados[Indice].Colum_Alzado[i].Traslapo = TraslapoFinal;

                    if (columna.Alzados[Indice].Colum_Alzado[i].UnitarioAdicional != null)
                    {
                        columna.Alzados[Indice].Colum_Alzado[i].UnitarioAdicional.Traslapo = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i].UnitarioAdicional.NoBarra, columna.Seccions[i].Item1.Material.FC);
                    }
                }
            }
        }

        #endregion Metodos - Agregar Alzado Sugerido

        #region Metodos - Dibujo Automático AutoCAD

        public void DrawColumAutoCAD(double X, double Y, string Names, int NoDespiece)
        {
            #region Dibujar Columna

            float DPR = 0.6f;
            string LayerCuadro = "FC_BORDE COLUMNA";
            string LayerTitiles = "FC_R-100";

            float MaxB1 = -999999;
            float MaxH1 = -999999;
            float B_CajonEstribos = 1.8f;

            MaxB1 = Seccions.FindAll(y => y.Item1 != null).ToList().Max(x => x.Item1.B);

            MaxH1 = Seccions.FindAll(y => y.Item1 != null).ToList().Max(x => x.Item1.H);

            bool ExisteCambioenB = false;

            for (int i = Seccions.Count - 1; i >= 0; i--)
            {
                if (Seccions[i].Item1 != null)
                {
                    try { if (Seccions[i].Item1.B - Seccions[i - 1].Item1.B != 0) { ExisteCambioenB = true; break; } }
                    catch { }
                }
            }

            //Dibujar Cuadro Fundación

            float B_Fund = Alzados[Alzados.Count - 1].DistX;
            double[] Ver_Fund = new double[] {X,Y+Form1.Proyecto_.e_Fundacion,
                                               X,Y,
                                               X+B_Fund+DPR,Y,
                                               X+B_Fund+DPR,Y+Form1.Proyecto_.e_Fundacion };

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Ver_Fund, LayerCuadro, false);

            //Cuadro Adicional Para Estribos En Fundación
            Ver_Fund = new double[] {X-B_CajonEstribos,Y+Form1.Proyecto_.e_Fundacion,
                                               X-B_CajonEstribos,Y,
                                               X,Y,
                                               X,Y+Form1.Proyecto_.e_Fundacion };

            //Agregar Texto Estribos en Fundación
            int C_F = (int)Math.Ceiling(((Form1.Proyecto_.e_Fundacion * 100 - 10) / (Form1.Proyecto_.SE_F * 100)) + 1);//Cantidad de Estribos

            string TextoEstribos2 = C_F + "#" + Seccions[Seccions.Count - 1].Item1.Estribo.NoEstribo + "/" + Form1.Proyecto_.SE_F * 100;
            double DistCorrerText2;
            if (TextoEstribos2.Length < 7)
            {
                DistCorrerText2 = 0.15 * (8);
            }
            else
            {
                DistCorrerText2 = 0.15 * (TextoEstribos2.Length);
            }

            double[] P_XYZf = new double[] { X - B_CajonEstribos + B_CajonEstribos / 2 - DistCorrerText2 / 2, Y + Form1.Proyecto_.e_Fundacion / 2 + 0.05, 0 };
            FunctionsAutoCAD.FunctionsAutoCAD.AddText(TextoEstribos2, P_XYZf, 0.75, 0.10, "FC_R-80", "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Ver_Fund, LayerCuadro, false);

            //Cota espesor Fundación

            double[] P1_CotaF = new double[] { X - B_CajonEstribos, Y, 0 };
            double[] P2_CotaF = new double[] { X - B_CajonEstribos, Y + Form1.Proyecto_.e_Fundacion, 0 };
            FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaF, P2_CotaF, "FC_COTAS", "FC_TEXT1", -0.3f);

            //Dibujar Cada Entre Piso
            for (int i = LuzAcum.Count - 1; i >= 0; i--)
            {
                float B_Draw;
                if (ExisteCambioenB)
                {
                    B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[i].Item1.B) / MaxB1) + DPR;
                }
                else
                {
                    B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[i].Item1.H) / MaxH1) + DPR;
                }

                double[] Ver_Colum = new double[] {X,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H,
                                                   X,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H-LuzLibre[i],
                                                   X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H-LuzLibre[i],
                                                   X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H };
                FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Ver_Colum, LayerCuadro);

                Ver_Colum = new double[] {X-B_CajonEstribos,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H,
                                                   X-B_CajonEstribos,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H-LuzLibre[i],
                                                   X,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H-LuzLibre[i],
                                                   X,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H };
                FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Ver_Colum, LayerCuadro);

                float DesCota = -0.3f;
                //Cotas entre piso y espesor de Viga

                //Cotas entre Piso

                double[] P1_CotaT = new double[] { X - B_CajonEstribos, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i], 0 };
                double[] P2_CotaT = new double[] { X - B_CajonEstribos, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H, 0 };

                if (Form1.Proyecto_.e_acabados != 0 & i == LuzAcum.Count - 1)
                {
                    P1_CotaT = new double[] { X - B_CajonEstribos, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i] + Form1.Proyecto_.e_acabados, 0 };
                    P2_CotaT = new double[] { X - B_CajonEstribos, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);

                    P1_CotaT = new double[] { X - B_CajonEstribos, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i], 0 };
                    P2_CotaT = new double[] { X - B_CajonEstribos, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i] + Form1.Proyecto_.e_acabados, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota, -0.2f);

                    FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(new double[] { X, Y + Form1.Proyecto_.e_Fundacion + Form1.Proyecto_.e_acabados, X + B_Draw, Y + Form1.Proyecto_.e_Fundacion + Form1.Proyecto_.e_acabados }
                    , LayerCuadro, false);

                    //Agregar Nivel de Sección de Acabados

                    float N_A = Form1.Proyecto_.Nivel_Fundacion + Form1.Proyecto_.e_acabados;
                    string NivelAcabado = N_A >= 0 ? "N.P.A" + @"\P" + "N+" + String.Format("{0:0.00}", N_A) : "N.P.A" + @"\P" + "N" + String.Format("{0:0.00}", N_A);

                    double[] PXYZ = new double[] { X + B_Draw, Y + Form1.Proyecto_.e_Fundacion + Form1.Proyecto_.e_acabados, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.B_NivelSeccion(PXYZ, NivelAcabado, 1.08, 1.90, true, "FC_COTAS", 75, 75, 75, 0);
                }
                else
                {
                    FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                }
                //Cotas Viga
                P1_CotaT = new double[] { X - B_CajonEstribos, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H, 0 };
                P2_CotaT = new double[] { X - B_CajonEstribos, Y + LuzAcum[i], 0 };
                FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);

                //Agregar Textos De Estribos

                double[] P_XYZ1;
                if ((int)CantEstribos_Sepa[i][2] == 0)
                {
                    // Zona Confinada

                    string TextoEstribos = ((int)CantEstribos_Sepa[i][1] + (int)CantEstribos_Sepa[i][3]) + "#" + Seccions[i].Item1.Estribo.NoEstribo + "/" + (float)CantEstribos_Sepa[i][4] * 100;
                    double DistCorrerText;
                    if (TextoEstribos.Length < 7)
                    {
                        DistCorrerText = 0.15 * (8);
                    }
                    else
                    {
                        DistCorrerText = 0.15 * (TextoEstribos.Length);
                    }

                    P_XYZ1 = new double[] { X - B_CajonEstribos + B_CajonEstribos / 2 - DistCorrerText / 2, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i] / 2 + 0.05, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.AddText(TextoEstribos, P_XYZ1, 0.75, 0.10, "FC_R-80", "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);
                }
                else
                {
                    //Zona No Confinada
                    string TextoEstribos = CantEstribos_Sepa[i][2] + "#" + Seccions[i].Item1.Estribo.NoEstribo + "/" + (float)CantEstribos_Sepa[i][5] * 100;
                    double DistCorrerText;
                    if (TextoEstribos.Length < 7)
                    {
                        DistCorrerText = 0.15 * (8);
                    }
                    else
                    {
                        DistCorrerText = 0.15 * (TextoEstribos.Length);
                    }

                    P_XYZ1 = new double[] { X - B_CajonEstribos + B_CajonEstribos / 2 - DistCorrerText / 2, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i] / 2 + 0.05, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.AddText(TextoEstribos, P_XYZ1, 0.75, 0.10, "FC_R-80", "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);

                    //Zona Confinada (Arriba)

                    float DistYubicacion = 0.25f;

                    TextoEstribos = CantEstribos_Sepa[i][1] + "#" + Seccions[i].Item1.Estribo.NoEstribo + "/" + (float)CantEstribos_Sepa[i][4] * 100;

                    if (TextoEstribos.Length < 7)
                    {
                        DistCorrerText = 0.15 * (8);
                    }
                    else
                    {
                        DistCorrerText = 0.15 * (TextoEstribos.Length);
                    }
                    P_XYZ1 = new double[] { X - B_CajonEstribos + B_CajonEstribos / 2 - DistCorrerText / 2, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - DistYubicacion + 0.05, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.AddText(TextoEstribos, P_XYZ1, 0.75, 0.10, "FC_R-80", "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);

                    //Zona Confinada (Abajo)
                    TextoEstribos = CantEstribos_Sepa[i][3] + "#" + Seccions[i].Item1.Estribo.NoEstribo + "/" + (float)CantEstribos_Sepa[i][4] * 100;
                    if (TextoEstribos.Length < 7)
                    {
                        DistCorrerText = 0.15 * (8);
                    }
                    else
                    {
                        DistCorrerText = 0.15 * (TextoEstribos.Length);
                    }
                    P_XYZ1 = new double[] { X - B_CajonEstribos + B_CajonEstribos / 2 - DistCorrerText / 2, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i] + DistYubicacion + 0.05, 0 };
                    FunctionsAutoCAD.FunctionsAutoCAD.AddText(TextoEstribos, P_XYZ1, 0.75, 0.10, "FC_R-80", "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);
                }

                //En Viga
                string TextoEstribos1 = CantEstribos_Sepa[i][0] + "#" + Seccions[i].Item1.Estribo.NoEstribo + "/" + (float)CantEstribos_Sepa[i][4] * 100;
                double DistCorrerText1;
                if (TextoEstribos1.Length < 7)
                {
                    DistCorrerText1 = 0.15 * (8);
                }
                else
                {
                    DistCorrerText1 = 0.15 * (TextoEstribos1.Length);
                }

                P_XYZ1 = new double[] { X - B_CajonEstribos + B_CajonEstribos / 2 - DistCorrerText1 / 2, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H / 2 + 0.05, 0 };
                FunctionsAutoCAD.FunctionsAutoCAD.AddText(TextoEstribos1, P_XYZ1, 0.75, 0.10, "FC_R-80", "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);

                if (i != 0)
                {
                    //Lineas entre Piso

                    double[] VerLosa1 = new double[] {X,Y+LuzAcum[i] -VigaMayor.Seccions[i].Item1.H,
                                                   X,Y+LuzAcum[i] };

                    FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa1, LayerCuadro, false);

                    //Para Cajon de Estribos
                    VerLosa1 = new double[] {X-B_CajonEstribos,Y+LuzAcum[i] -VigaMayor.Seccions[i].Item1.H,
                                                   X-B_CajonEstribos,Y+LuzAcum[i] };

                    FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa1, LayerCuadro, false);

                    double[] VerLosa2 = new double[] {X+B_Draw,Y+LuzAcum[i],
                                                   X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H};

                    FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa2, LayerCuadro, false);
                }

                try
                {
                    if (ExisteCambioenB)
                    {
                        if (Seccions[i].Item1.B != Seccions[i - 1].Item1.B)
                        {
                            float B_Draw2 = ((Alzados[Alzados.Count - 1].DistX * Seccions[i - 1].Item1.B) / MaxB1) + DPR;

                            double[] LineaFaltante1 = new double[] { X+B_Draw, Y + LuzAcum[i],
                                                                 X+B_Draw2,Y+ LuzAcum[i]};
                            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(LineaFaltante1, LayerCuadro, false);
                        }
                    }
                    else
                    {
                        if (Seccions[i].Item1.H != Seccions[i - 1].Item1.H)
                        {
                            float B_Draw2 = ((Alzados[Alzados.Count - 1].DistX * Seccions[i - 1].Item1.H) / MaxH1) + DPR;

                            double[] LineaFaltante1 = new double[] { X+B_Draw, Y + LuzAcum[i],
                                                                 X+B_Draw2,Y+ LuzAcum[i]};
                            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(LineaFaltante1, LayerCuadro, false);
                        }
                    }
                }
                catch { }
            }

            #region Resitencia

            //Agregar Cotas de Resistencia

            float Fc = Seccions[Seccions.Count - 1].Item1.Material.FC;
            List<float> Resitencias = new List<float>();
            Resitencias.Add(Fc);
            for (int i = Seccions.Count - 1; i >= 0; i--)
            {
                if (Fc != Seccions[i].Item1.Material.FC)
                {
                    Resitencias.Add(Seccions[i].Item1.Material.FC);
                    Fc = Seccions[i].Item1.Material.FC;
                }
            }

            for (int i = 0; i < Resitencias.Count; i++)
            {
                int IndiceI = Seccions.FindLastIndex(x => x.Item1.Material.FC == Resitencias[i]);
                int IndiceF = Seccions.FindIndex(x => x.Item1.Material.FC == Resitencias[i]);

                float B_Draw;
                if (ExisteCambioenB)
                {
                    B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[IndiceI].Item1.B) / MaxB1) + DPR;
                }
                else
                {
                    B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[IndiceI].Item1.H) / MaxH1) + DPR;
                }

                double[] CotaFc1 = new double[] { X + B_Draw, Y + LuzAcum[IndiceI] - VigaMayor.Seccions[IndiceI].Item1.H - LuzLibre[IndiceI], 0 };

                if (ExisteCambioenB)
                {
                    B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[IndiceI].Item1.B) / MaxB1) + DPR;
                }
                else
                {
                    B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[IndiceI].Item1.H) / MaxH1) + DPR;
                }

                double[] CotaFc2 = new double[] { X + B_Draw, Y + LuzAcum[IndiceF], 0 };
                string TextCota; float DesplazCota = 0.5f;
                if (IndiceI - IndiceF > 2)
                {
                    TextCota = @"{\H1.33333x;\C11; Resistencia a la compresión del concreto a los 28 días\Pf'c=" + Resitencias[i] + @"kgf/cm²\C256; }";
                }
                else if (IndiceI - IndiceF == 0)
                {
                    DesplazCota = 0.7f;
                    TextCota = @"{\H1.33333x;\C11; Resistencia a la \Pcompresión del\Pconcreto a los 28 días\Pf'c=" + Resitencias[i] + @"kgf/cm²\C256; }";
                }
                else
                {
                    DesplazCota = 0.7f;
                    TextCota = @"{\H1.33333x;\C11; Resistencia a la compresión del\Pconcreto a los 28 días\Pf'c=" + Resitencias[i] + @"kgf/cm²\C256; }";
                }
                FunctionsAutoCAD.FunctionsAutoCAD.AddCota(CotaFc1, CotaFc2, "FC_COTAS", "FC_TEXT1", DesplazCota, headType1: FunctionsAutoCAD.ArrowHeadType.ArrowDefault, Text: TextCota,
                     headType2: FunctionsAutoCAD.ArrowHeadType.ArrowDefault, TextRotation: 90, ArrowheadSize: 0.002);

                double[] P_XYZRes = new double[] { CotaFc1[0] + 0.4f, CotaFc1[1] + (CotaFc2[1] - CotaFc1[1]) / 2 - 3f / 2, 0 };

                //FunctionsAutoCAD.FunctionsAutoCAD.AddText(MsgFC + Resitencias[i] + " kgf/cm²", P_XYZRes, 6f, 0.1, LayerString, "ROMANS",
                // 90, justifyText: FunctionsAutoCAD.JustifyText.Center, Width2: 3f);
            }

            #endregion Resitencia

            // Espesor  Losa Final
            double B_DrawF;

            if (ExisteCambioenB)
            {
                B_DrawF = ((Alzados[Alzados.Count - 1].DistX * Seccions[0].Item1.B) / MaxB1) + DPR;
            }
            else
            {
                B_DrawF = ((Alzados[Alzados.Count - 1].DistX * Seccions[0].Item1.H) / MaxH1) + DPR;
            }

            double[] VerLosa_Final = new double[] {X,Y+LuzAcum[0] -VigaMayor.Seccions[0].Item1.H,
                                                   X,Y+LuzAcum[0],
                                                   X+B_DrawF,Y+LuzAcum[0],
                                                   X+B_DrawF,Y+LuzAcum[0]-VigaMayor.Seccions[0].Item1.H };
            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa_Final, LayerCuadro, false);

            // LOSA FINAL PARA ESTRIBOS
            VerLosa_Final = new double[] {X-B_CajonEstribos,Y+LuzAcum[0] -VigaMayor.Seccions[0].Item1.H,
                                                   X-B_CajonEstribos,Y+LuzAcum[0],
                                                   X,Y+LuzAcum[0],
                                                   X,Y+LuzAcum[0]-VigaMayor.Seccions[0].Item1.H };
            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa_Final, LayerCuadro, false);

            //Agregar Cuadro, Titulo
            double DistCT = 0.5f;

            double[] Vert_CT = new[] {X,Y+ LuzAcum[0],
                                      X,Y+LuzAcum[0]+DistCT,
                                      X+B_DrawF,Y+LuzAcum[0]+DistCT,
                                      X+B_DrawF,Y+ LuzAcum[0]};
            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vert_CT, LayerCuadro, false);

            // Agregar Cuadro, Titulo de Estribos
            Vert_CT = new[] {X-B_CajonEstribos,Y+ LuzAcum[0],
                                      X-B_CajonEstribos,Y+LuzAcum[0]+DistCT,
                                      X,Y+LuzAcum[0]+DistCT,
                                      X,Y+ LuzAcum[0]};
            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vert_CT, LayerCuadro, false);

            string TitleDespi = "DESPIECE " + NoDespiece;
            double DistCorrerTextB = 0.15 * (TitleDespi.Length - 1);
            double[] P_XYZ = new double[] { X + B_DrawF / 2 - DistCorrerTextB / 2, Y + LuzAcum[0] + DistCT / 2 + 0.05, 0 };
            FunctionsAutoCAD.FunctionsAutoCAD.AddText(TitleDespi, P_XYZ, 0.75, 0.10, LayerTitiles, "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);

            //Agregar Titulo de Estribos
            TitleDespi = "ESTRIBOS ";
            DistCorrerTextB = 0.15 * (TitleDespi.Length - 1);
            P_XYZ = new double[] { X - B_CajonEstribos + B_CajonEstribos / 2 - DistCorrerTextB / 2, Y + LuzAcum[0] + DistCT / 2 + 0.05, 0 };
            FunctionsAutoCAD.FunctionsAutoCAD.AddText(TitleDespi, P_XYZ, 0.75, 0.10, LayerTitiles, "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);

            //Agregar Cuadro, Nombre Columnas

            //Caso cuando las secciones son Rectangulares
            string Dimensiones = "(" + Seccions[Seccions.Count - 1].Item1.B * 100 + "x" + Seccions[Seccions.Count - 1].Item1.H * 100 + ")";
            if (Seccions[Seccions.Count - 1].Item1.H == 0)
            {
                Dimensiones = "(" + "∅" + Seccions[Seccions.Count - 1].Item1.B * 100 + ")";
            }

            string NamesColumns = Names + @"\P" + Dimensiones;

            float Altu_Names = 1f;
            DistCorrerTextB = 0.15 * (Dimensiones.Length + 1.5);

            double B_DrawI;
            if (ExisteCambioenB)
            {
                B_DrawI = ((Alzados[Alzados.Count - 1].DistX * Seccions[Seccions.Count - 1].Item1.B) / MaxB1) + DPR;
            }
            else
            {
                B_DrawI = ((Alzados[Alzados.Count - 1].DistX * Seccions[Seccions.Count - 1].Item1.H) / MaxH1) + DPR;
            }

            P_XYZ = new double[] { X + B_DrawI / 2 - DistCorrerTextB / 2, Y - Altu_Names / 2 + 0.2, 0 };

            double[] Vert_CI = new[] {X,Y,
                                      X,Y-Altu_Names,
                                      X+B_DrawI,Y-Altu_Names,
                                      X+B_DrawI,Y };
            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vert_CI, LayerCuadro, false);
            FunctionsAutoCAD.FunctionsAutoCAD.AddText(NamesColumns, P_XYZ, 0.75, 0.10, LayerTitiles, "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);

            #endregion Dibujar Columna

            #region Dibujar Refuerzo

            foreach (Alzado a in Alzados)
            {
                double[] PXYZ = new double[] { X + a.DistX - 0.15, Y - 0.12, 0 };

                //Agregar Nomenclatura de Refuerzo
                if (Alzados.Count == 2 & a.ID == 1)
                {
                    PXYZ = new double[] { X + a.DistX - DPR / 2 - 0.15, Y - 0.12, 0 };
                }
                else if (Alzados.Count == 2 & a.ID == 2)
                {
                    PXYZ = new double[] { X + a.DistX + DPR / 2 - 0.15, Y - 0.12, 0 };
                }

                FunctionsAutoCAD.FunctionsAutoCAD.AddText(Convert.ToString(a.ID), PXYZ, 0.5, 0.08, "FC_R-100", "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center, Width2: 0.5);

                for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
                {
                    AlzadoUnitario aux = a.Colum_Alzado[i];
                    if (aux != null)
                    {
                        DrawAutoCAD_Refuerzo(a, aux, X, Y, i);

                        if (aux.UnitarioAdicional != null)
                        {
                            DrawAutoCAD_Refuerzo(a, aux.UnitarioAdicional, X, Y, i);
                        }
                    }
                }
            }

            #endregion Dibujar Refuerzo

            #region Agregar Nivel de Sección

            float N_F = Form1.Proyecto_.Nivel_Fundacion;
            float eF = Form1.Proyecto_.e_Fundacion;
            string NivelFund = N_F >= 0 ? "N+" + String.Format("{0:0.00}", N_F) + @"\P" + "Fundación" : "N" + String.Format("{0:0.00}", N_F) + @"\P" + "Fundación";

            FunctionsAutoCAD.FunctionsAutoCAD.B_NivelSeccion(new double[] { X - B_CajonEstribos, Y + eF, 0 }, NivelFund, 1.08, 1.90, false, "FC_COTAS", 75, 75, 75, 0);
            float NivelN = N_F;
            int NoLosa = 0;
            for (int i = LuzAcum.Count - 1; i >= 0; i--)
            {
                NoLosa += 1;
                string Nivel;
                double[] PXYZ;

                PXYZ = new double[] { X - B_CajonEstribos, Y + LuzAcum[i], 0 };
                NivelN += LuzLibre[i] + VigaMayor.Seccions[i].Item1.H;

                if (NivelN >= 0)
                {
                    Nivel = "N+" + String.Format("{0:0.00}", NivelN) + @"\P" + "Losa " + NoLosa;
                }
                else
                {
                    Nivel = "N" + String.Format("{0:0.00}", NivelN) + @"\P" + "Losa " + NoLosa;
                }

                FunctionsAutoCAD.FunctionsAutoCAD.B_NivelSeccion(PXYZ, Nivel, 1.08, 1.90, false, "FC_COTAS", 75, 75, 75, 0);
            }

            #endregion Agregar Nivel de Sección
        }

        private void DrawAutoCAD_Refuerzo(Alzado a, AlzadoUnitario aux, double X, double Y, int i)
        {
            float DPR = 0.6f;
            string LayerRefuerzo = "FC_REFUERZO";
            string LayerString = "FC_R-80";
            float LdAd = 0.4f;

            if (aux.Coord_Alzado_PB.Count == 2)
            {
                double DistCorrerText = 0.2;
                string NomeRefuerz = aux.CantBarras + "#" + aux.NoBarra + " L=";
                double DistCorrerTextY = 0.15 * (NomeRefuerz.Length + 3);
                double[] Coord_Refuerz; double[] P_XYZ_Text; double[] P1_CotaT, P2_CotaT;

                float DesCota = -0.2f;
                if (aux.x1 != 0)
                {
                    DesCota = -0.4f;
                }

                if (Alzados.Count == 2 && a.ID == 1)
                {
                    DesCota = 0.3f;
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2) };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText - DPR / 2, Y + aux.Hacum - aux.Hviga - aux.H_Stroy / 2 - DistCorrerTextY / 2, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[1][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[1][1] + aux.Traslapo, 0 };
                }
                else if (Alzados.Count == 2 && a.ID == 2)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2) };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText + DPR / 2, Y + aux.Hacum - aux.Hviga - aux.H_Stroy / 2 - DistCorrerTextY / 2, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Y + aux.Coord_Alzado_PB[1][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Y + aux.Coord_Alzado_PB[1][1] + aux.Traslapo, 0 };
                }
                else
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0], Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0], Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2) };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum - aux.Hviga - aux.H_Stroy / 2 - DistCorrerTextY / 2, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[1][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[1][1] + aux.Traslapo, 0 };

                    if (aux.Tipo == "A")
                    {
                        try
                        {
                            if (a.Colum_Alzado[i + 1] != null)
                            {
                                if (a.Colum_Alzado[i + 1].Tipo == "A" && a.Colum_Alzado[i + 1].ToString() == aux.ToString() && a.Colum_Alzado[i + 1].Traslapo + aux.Traslapo + LdAd * 2 >= aux.H_Stroy)
                                {
                                    DesCota = -0.2f;
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum, 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };

                                    FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);

                                    DesCota = -0.2f;
                                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum - aux.H_Stroy / 2 - DistCorrerTextY / 2, 0 };
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[1][1], 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + a.Colum_Alzado[i + 1].Hacum - a.Colum_Alzado[i + 1].Hviga, 0 };
                                }
                                else
                                {
                                    DesCota = -0.2f;
                                    float DistAd = 0.3f;
                                    if (aux.Tipo2 == "+")
                                    {
                                        P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] + DistAd - DistCorrerText, Y + aux.Hacum - DistCorrerTextY / 2, 0 };
                                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum, 0 };
                                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                    }
                                    else if (aux.Tipo2 == "-")
                                    {
                                        P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] + DistAd - DistCorrerText, Y + aux.Hacum - DistCorrerTextY / 2, 0 };
                                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
                                    }
                                    else
                                    {
                                        P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum, 0 };
                                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
                                    }
                                }
                            }
                            else if (aux.UnitarioAdicional != null)
                            {
                                if (aux.UnitarioAdicional.CantBarras + "#" + aux.UnitarioAdicional.NoBarra == aux.CantBarras + "#" + aux.NoBarra && aux.Traslapo + aux.Traslapo + LdAd * 2 >= aux.H_Stroy)
                                {
                                    DesCota = -0.2f;
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum, 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[1][1], 0 };

                                    FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);

                                    DesCota = -0.2f;
                                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum - aux.H_Stroy / 2 - DistCorrerTextY / 2, 0 };
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + LuzAcum[i + 1] - VigaMayor.Seccions[i + 1].Item1.H, 0 };
                                }
                                else
                                {
                                    DesCota = -0.2f;
                                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum, 0 };
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
                                }
                            }
                            else
                            {
                                DesCota = -0.2f;
                                float DistAd = 0.3f;
                                if (aux.Tipo2 == "+")
                                {
                                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] + DistAd - DistCorrerText, Y + aux.Hacum - DistCorrerTextY / 2, 0 };
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum, 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                }
                                else if (aux.Tipo2 == "-")
                                {
                                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] + DistAd - DistCorrerText, Y + aux.Hacum - DistCorrerTextY / 2, 0 };
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
                                }
                                else
                                {
                                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum, 0 };
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
                                }
                            }
                        }
                        catch
                        {
                            DesCota = -0.2f;
                            float DistAd = 0.3f;
                            if (aux.Tipo2 == "+")
                            {
                                P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] + DistAd - DistCorrerText, Y + aux.Hacum - DistCorrerTextY / 2, 0 };
                                P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum, 0 };
                                P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                            }
                            else if (aux.Tipo2 == "-")
                            {
                                P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] + DistAd - DistCorrerText, Y + aux.Hacum - DistCorrerTextY / 2, 0 };
                                P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
                            }
                            else
                            {
                                P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum, 0 };
                                P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
                            }
                        }
                    }
                    else if (aux.Tipo == "Botton" | aux.Tipo == "ABotton")
                    {
                        DesCota = -0.2f;
                        P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + LuzAcum[i + 1], 0 };
                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + LuzAcum[i + 1] - VigaMayor.Seccions[i + 1].Item1.H, 0 };
                    }
                }

                FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2DWithLengthText(Coord_Refuerz, LayerRefuerzo, NomeRefuerz, P_XYZ_Text, 0.75, 0.10, LayerString, "FC_TEXT1", 90, 1.1);

                FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
            }

            if (aux.Coord_Alzado_PB.Count == 3)
            {
                double DistCorrerText = 0.2;
                string NomeRefuerz = aux.CantBarras + "#" + aux.NoBarra + " L=";
                double DistCorrerTextY = 0.15 * (NomeRefuerz.Length + 3);
                double Ytext = Y + aux.Hacum - aux.Hviga - aux.H_Stroy / 2 - DistCorrerTextY / 2;
                double[] Coord_Refuerz; double[] P_XYZ_Text;
                double[] P1_CotaT, P2_CotaT; float DesCota = -0.2f;

                if (aux.Tipo == "T1" && aux.NoStory != 1 || aux.Tipo == "A" && aux.NoStory != 1 || aux.NoStory == 1 && aux.UltPiso)
                {
                    Ytext = Y + aux.Coord_Alzado_PB[0][1] + (aux.Coord_Alzado_PB[2][1] - aux.Coord_Alzado_PB[0][1]) / 2;
                }
                else if (aux.Tipo == "T1" && aux.NoStory == 1 || aux.Tipo == "Botton" && aux.NoStory == 1 || aux.Tipo == "ABotton" && aux.NoStory == 1)
                {
                    Ytext = Y + aux.Coord_Alzado_PB[0][1] + Form1.Proyecto_.e_Fundacion;
                }

                if (Alzados.Count == 2 && a.ID == 1)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2), X + aux.Coord_Alzado_PB[2][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[2][1], 2) };

                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText - DPR / 2, Ytext, 0 };
                    DesCota = 0.3f;
                    if (aux.NoStory != 1)
                    {
                        if (aux.Tipo == "A")
                        {
                        }
                        else
                        {
                        }
                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[0][1], 0 };
                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[0][1] + aux.Traslapo, 0 };
                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                    }
                    else
                    {
                        DesCota = 0;
                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[0][1], 0 };
                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                    }
                }
                else if (Alzados.Count == 2 && a.ID == 2)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2), X + aux.Coord_Alzado_PB[2][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[2][1], 2) };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText + DPR / 2, Ytext, 0 };

                    if (aux.NoStory != 1)
                    {
                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Y + aux.Coord_Alzado_PB[0][1], 0 };
                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Y + aux.Coord_Alzado_PB[0][1] + aux.Traslapo, 0 };
                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                    }
                    else
                    {
                        DesCota = 0;
                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Y + aux.Coord_Alzado_PB[0][1], 0 };
                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                    }
                }
                else
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0], Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0], Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2), X + aux.Coord_Alzado_PB[2][0], Math.Round(Y + aux.Coord_Alzado_PB[2][1], 2) };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText, Ytext, 0 };

                    if (aux.NoStory != 1)
                    {
                        if (aux.Tipo == "A")
                        {
                            if (a.Colum_Alzado[i + 1] != null)
                            {
                                if (a.Colum_Alzado[i + 1].ToString() == aux.ToString())
                                {
                                    if (a.Colum_Alzado[i + 1].Traslapo + LdAd + aux.Traslapo + LdAd >= aux.H_Stroy)
                                    {
                                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], 0 };
                                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + a.Colum_Alzado[i + 1].Hacum - a.Colum_Alzado[i + 1].Hviga, 0 };
                                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                                    }
                                    else
                                    {
                                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], 0 };
                                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Hacum - aux.Hviga, 0 };
                                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                                    }
                                }
                                else
                                {
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Hacum - aux.Hviga, 0 };
                                    FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                                }
                            }
                            else if (aux.UnitarioAdicional != null)
                            {
                                if (aux.UnitarioAdicional.CantBarras + "#" + aux.UnitarioAdicional.NoBarra == aux.CantBarras + "#" + aux.NoBarra)
                                {
                                    if (aux.Traslapo + LdAd + aux.Traslapo + LdAd >= aux.H_Stroy)
                                    {
                                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], 0 };
                                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + LuzAcum[i + 1] - VigaMayor.Seccions[i + 1].Item1.H, 0 };

                                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                                    }
                                    else
                                    {
                                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], 0 };
                                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Hacum - aux.Hviga, 0 };
                                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                                    }
                                }
                                else
                                {
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Hacum - aux.Hviga, 0 };
                                    FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                                }
                            }
                            else
                            {
                                P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], 0 };
                                P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Hacum - aux.Hviga, 0 };
                                FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                            }
                        }
                        else
                        {
                            P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                            P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1] + aux.Traslapo, 0 };
                            FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                        }
                    }
                    else
                    {
                        if (aux.UltPiso & aux.Tipo == "A")
                        {
                            P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], 0 };
                            P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[2][0], Y + aux.Hacum - aux.Hviga, 0 };
                            FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                        }
                        else
                        {
                            DesCota = 0;
                            P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                            P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                            FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                        }
                    }
                }

                FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2DWithLengthText(Coord_Refuerz, LayerRefuerzo, NomeRefuerz, P_XYZ_Text, 0.75, 0.10, LayerString, "FC_TEXT1", 90, 1.1);
            }

            if (aux.Coord_Alzado_PB.Count == 4)
            {
                double DistCorrerText = 0.2;
                string NomeRefuerz = aux.CantBarras + "#" + aux.NoBarra + " L=";
                double DistCorrerTextY = 0.15 * (NomeRefuerz.Length + 3);
                double[] Coord_Refuerz; double[] P_XYZ_Text; double[] P1_CotaT, P2_CotaT;
                double Ytext = Y + aux.Hacum - aux.Hviga - aux.H_Stroy / 2 - DistCorrerTextY / 2;

                float DesCota = 0;

                if (Alzados.Count == 2 && a.ID == 1)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2), X + aux.Coord_Alzado_PB[2][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[2][1], 2), X + aux.Coord_Alzado_PB[3][0] - DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[3][1], 2) };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText - DPR / 2, Ytext, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[0][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                }
                else if (Alzados.Count == 2 && a.ID == 2)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2), X + aux.Coord_Alzado_PB[2][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[2][1], 2), X + aux.Coord_Alzado_PB[3][0] + DPR / 2, Math.Round(Y + aux.Coord_Alzado_PB[3][1], 2) };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText + DPR / 2, Ytext, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Y + aux.Coord_Alzado_PB[0][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 2, Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                }
                else
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0], Math.Round(Y + aux.Coord_Alzado_PB[0][1], 2), X + aux.Coord_Alzado_PB[1][0], Math.Round(Y + aux.Coord_Alzado_PB[1][1], 2), X + aux.Coord_Alzado_PB[2][0], Math.Round(Y + aux.Coord_Alzado_PB[2][1], 2), X + aux.Coord_Alzado_PB[3][0], Math.Round(Y + aux.Coord_Alzado_PB[3][1], 2) };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText, Ytext, 0 };

                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                }

                FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2DWithLengthText(Coord_Refuerz, LayerRefuerzo, NomeRefuerz, P_XYZ_Text, 0.75, 0.10, LayerString, "FC_TEXT1", 90, 1.1);

                FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
            }
        }

        #endregion Metodos - Dibujo Automático AutoCAD

        #region Metodos - Calcular Peso de Acero

        public void CrearListaPesosRefuerzos(int CantidadAnteior)
        {
            for (int i = 0; i < Alzados.Count - CantidadAnteior; i++)
            {
                KgRefuerzoforColumAlzado.Add(0);
            }
        }

        public void CalcularPesoAcero(int Col = -1)
        {
            if (Col != -1)
            {
                KgRefuerzoforColumAlzado[Col] = 0;
                for (int i = Alzados[Col].Colum_Alzado.Count - 1; i >= 0; i--)
                {
                    AlzadoUnitario aux = Alzados[Col].Colum_Alzado[i];
                    if (aux != null)
                    {
                        if (aux.Coord_Alzado_PB.Count != 0)
                        {
                            KgRefuerzoforColumAlzado[Col] += CalcularLongitudRefuerzo(aux.Coord_Alzado_PB) * Form1.Proyecto_.MasaNominalBarras[aux.NoBarra] * aux.CantBarras;
                        }
                        if (aux.UnitarioAdicional != null)
                        {
                            KgRefuerzoforColumAlzado[Col] += CalcularLongitudRefuerzo(aux.UnitarioAdicional.Coord_Alzado_PB) * Form1.Proyecto_.MasaNominalBarras[aux.UnitarioAdicional.NoBarra] * aux.UnitarioAdicional.CantBarras;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < Alzados.Count; j++)
                {
                    Alzado a = Alzados[j];
                    KgRefuerzoforColumAlzado[j] = 0;
                    for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
                    {
                        AlzadoUnitario aux = a.Colum_Alzado[i];

                        if (aux != null)
                        {
                            if (aux.Coord_Alzado_PB.Count != 0)
                            {
                                KgRefuerzoforColumAlzado[j] += CalcularLongitudRefuerzo(aux.Coord_Alzado_PB) * Form1.Proyecto_.MasaNominalBarras[aux.NoBarra] * aux.CantBarras;
                            }
                            if (aux.UnitarioAdicional != null)
                            {
                                KgRefuerzoforColumAlzado[j] += CalcularLongitudRefuerzo(aux.UnitarioAdicional.Coord_Alzado_PB) * Form1.Proyecto_.MasaNominalBarras[aux.UnitarioAdicional.NoBarra] * aux.UnitarioAdicional.CantBarras;
                            }
                        }
                    }
                }
            }

            KgRefuerzo = KgRefuerzoforColumAlzado.Sum();
        }

        private float CalcularLongitudRefuerzo(List<float[]> Coordenadas)
        {
            float Longitud = 0;
            for (int i = 1; i < Coordenadas.Count; i++)
            {
                Longitud += (float)Math.Sqrt(Math.Pow(Coordenadas[i][0] - Coordenadas[i - 1][0], 2) + Math.Pow(Coordenadas[i][1] - Coordenadas[i - 1][1], 2));
            }
            return Longitud;
        }

        #endregion Metodos - Calcular Peso de Acero

        #region Metodos - Calcular Cantidad de Estribos VIGA,ZC,ZNC

        public void CantidadEstribos(int i)
        {
            //Calcular Lo -- Longitud de Confinamiento
            float S1 = Seccions[i].Item1.Estribo.Separacion;
            float S2 = 0;
            int C_Viga = 0;
            int ZC1 = 0;
            int ZNC = 0;
            int ZC2 = 0;
            int MenorDiametro = 999999;
            for (int j = 0; j < Alzados.Count; j++)
            {
                int MenorAux = 0;
                if (Alzados[j].Colum_Alzado.FindAll(X => X != null).ToList().Count != 0)
                {
                    MenorAux = Alzados[j].Colum_Alzado.FindAll(X => X != null).ToList().Min(x => x.NoBarra);
                }
                if (MenorDiametro > MenorAux)
                {
                    MenorDiametro = MenorAux;
                }
            }

            float Lo = 0;
            Lo = Seccions[i].Item1.B * 100 > Seccions[i].Item1.H * 100 ? Seccions[i].Item1.B * 100 : Seccions[i].Item1.H * 100;

            if ((LuzLibre[i] * 100) / 6 > Lo)
            {
                Lo = (LuzLibre[i] * 100) / 6;
            }
            if (Form1.Proyecto_.DMO_DES == GDE.DMO)
            {
                S2 = 2 * S1;
                if (0.5f > Lo)
                {
                    Lo = 0.5f;
                }
            }
            else
            {
                try
                {
                    S2 = (Form1.Proyecto_.Diametro_ref[MenorDiametro]) * 6 > 15f ? (Form1.Proyecto_.Diametro_ref[MenorDiametro]) * 6 : 15f;
                }
                catch
                {
                    S2 = 15f;
                }
                if (45f > Lo)
                {
                    Lo = 50f;
                }
            }

            C_Viga = (int)Math.Ceiling((VigaMayor.Seccions[i].Item1.H * 100 - 10) / S1 + 1);

            if (2 * Lo >= LuzLibre[i] * 100)
            {
                ZNC = 0;
                ZC2 = (int)Math.Ceiling((((Lo - 5) / (int)(S1)) + 1));
                ZC1 = (int)Math.Ceiling((((Lo - 5) / (int)(S1)) + 1));
            }
            else
            {
                ZC2 = (int)Math.Ceiling((((Lo - 5) / (int)(S1)) + 1));
                ZC1 = (int)Math.Ceiling((((Lo - 5) / (int)(S1)) + 1));

                ZNC = (int)((((LuzLibre[i] * 100 - S1 * (ZC1 * 2 - 2)) / (int)(S2))));
            }
            CantEstribos_Sepa[i] = new object[] { C_Viga, ZC1, ZNC, ZC2, S1 / 100, S2 / 100 };
        }

        #endregion Metodos - Calcular Cantidad de Estribos VIGA,ZC,ZNC

        #region Metodos - Cantidades de Obra DL NET

        public void CalcularCantidadesDLNET()
        {
            float R = Form1.Proyecto_.R / 100;

            List<Tuple<int, string>> ListaGanchos_Cant_Nomenclatura = new List<Tuple<int, string>>();

            List<Tuple<int, string>> ListaEstribos_Cant_Nomenclatura = new List<Tuple<int, string>>();
            //CANTIDADES DE ESTRIBOS Y GANCHOS
            for (int i = Seccions.Count - 1; i >= 0; i--)
            {
                //Sección Rectangular
                int Cantidad = (int)CantEstribos_Sepa[i][0] + (int)CantEstribos_Sepa[i][1] + (int)CantEstribos_Sepa[i][2] + (int)CantEstribos_Sepa[i][3];

                if (Seccions[i].Item1 is CRectangulo)
                {
                    CRectangulo seccion2 = (CRectangulo)Seccions[i].Item1;
                    seccion2.CalcularDimensionesEstribo_Gancho(seccion2.Estribo, R);
                    ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoH_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * Cantidad, seccion2.GanchoH_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                    ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoV_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * Cantidad, seccion2.GanchoV_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                    ListaEstribos_Cant_Nomenclatura.Add(new Tuple<int, string>(Cantidad, seccion2.Estribo_Dimensiones_B_H_G_Nomenclatura.Item4));
                    if (i == Seccions.Count - 1)
                    {
                        //Agregar a Lista de Ganchos Y Estribos correspondientes a la Fundación
                        int C_F = (int)Math.Ceiling(((Form1.Proyecto_.e_Fundacion * 100 - 10) / (Form1.Proyecto_.SE_F * 100)) + 1);
                        ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(C_F * seccion2.GanchoH_Dim_Cant_Ltotal_G_Nomenclatura.Item1, seccion2.GanchoH_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                        ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(C_F * seccion2.GanchoV_Dim_Cant_Ltotal_G_Nomenclatura.Item1, seccion2.GanchoV_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                        ListaEstribos_Cant_Nomenclatura.Add(new Tuple<int, string>(C_F, seccion2.Estribo_Dimensiones_B_H_G_Nomenclatura.Item4));
                    }
                }
                if (Seccions[i].Item1 is CSD)
                {
                    CSD seccion2 = (CSD)Seccions[i].Item1;
                    seccion2.CalcularDimensionesEstribo_Gancho(seccion2.Estribo, R);

                    ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoHAleta_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * Cantidad, seccion2.GanchoHAleta_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                    ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoVAleta_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * Cantidad, seccion2.GanchoVAleta_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                    ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoHAlma_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * Cantidad, seccion2.GanchoHAlma_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                    ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoVAlma_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * Cantidad, seccion2.GanchoVAlma_Dim_Cant_Ltotal_G_Nomenclatura.Item4));

                    ListaEstribos_Cant_Nomenclatura.Add(new Tuple<int, string>(Cantidad, seccion2.EstriboAleta_Dimensiones_B_H_G_Nomenclatura.Item4));
                    ListaEstribos_Cant_Nomenclatura.Add(new Tuple<int, string>(Cantidad, seccion2.EstriboAlma_Dimensiones_B_H_G_Nomenclatura.Item4));

                    if (i == Seccions.Count - 1)
                    {
                        //Agregar a Lista de Ganchos Y Estribos correspondientes a la Fundación
                        int C_F = (int)Math.Ceiling(((Form1.Proyecto_.e_Fundacion * 100 - 10) / (Form1.Proyecto_.SE_F * 100)) + 1);
                        ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoHAleta_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * C_F, seccion2.GanchoHAleta_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                        ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoVAleta_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * C_F, seccion2.GanchoVAleta_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                        ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoHAlma_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * C_F, seccion2.GanchoHAlma_Dim_Cant_Ltotal_G_Nomenclatura.Item4));
                        ListaGanchos_Cant_Nomenclatura.Add(new Tuple<int, string>(seccion2.GanchoVAlma_Dim_Cant_Ltotal_G_Nomenclatura.Item1 * C_F, seccion2.GanchoVAlma_Dim_Cant_Ltotal_G_Nomenclatura.Item4));

                        ListaEstribos_Cant_Nomenclatura.Add(new Tuple<int, string>(C_F, seccion2.EstriboAleta_Dimensiones_B_H_G_Nomenclatura.Item4));
                        ListaEstribos_Cant_Nomenclatura.Add(new Tuple<int, string>(C_F, seccion2.EstriboAlma_Dimensiones_B_H_G_Nomenclatura.Item4));
                    }
                }
            }

            Lista_RefuerzoTransversal_DLNET = new List<string>();
            RetornarListaDepurada(Lista_RefuerzoTransversal_DLNET, ListaGanchos_Cant_Nomenclatura);
            RetornarListaDepurada(Lista_RefuerzoTransversal_DLNET, ListaEstribos_Cant_Nomenclatura);

            //CANTIDADES DE REFUERZO LONGITUDINAL

            List<Tuple<int, string>> ListaRefuerzoLongitudinal = new List<Tuple<int, string>>();

            foreach (Alzado alzado in Alzados)
            {
                for (int i = alzado.Colum_Alzado.Count - 1; i >= 0; i--)
                {
                    AlzadoUnitario aux = alzado.Colum_Alzado[i];
                    if (aux != null)
                    {
                        aux.CreararTuplesParaCantidades();
                        if (aux.Cant_Nomenclatura_DLNET != null)
                        {
                            ListaRefuerzoLongitudinal.Add(new Tuple<int, string>(aux.Cant_Nomenclatura_DLNET.Item1, aux.Cant_Nomenclatura_DLNET.Item2));
                        }

                        if (aux.UnitarioAdicional != null)
                        {
                            if (aux.UnitarioAdicional.Cant_Nomenclatura_DLNET != null)
                            {
                                aux.UnitarioAdicional.CreararTuplesParaCantidades();
                                ListaRefuerzoLongitudinal.Add(new Tuple<int, string>(aux.UnitarioAdicional.Cant_Nomenclatura_DLNET.Item1, aux.UnitarioAdicional.Cant_Nomenclatura_DLNET.Item2));
                            }
                        }
                    }
                }
            }
            Lista_RefuerzoLongitudinal_DLNET = new List<string>();
            RetornarListaDepurada(Lista_RefuerzoLongitudinal_DLNET, ListaRefuerzoLongitudinal);
        }

        private void RetornarListaDepurada(List<string> Lista_DllNetFinal, List<Tuple<int, string>> ListaaDepurar)
        {
            List<int> VectorIndices = new List<int>();

            for (int i = 0; i < ListaaDepurar.Count; i++)
            {
                int Cantidad1 = ListaaDepurar[i].Item1;
                string Nomenclatura1 = ListaaDepurar[i].Item2;

                if (VectorIndices.Exists(x => x == i) == false)
                {
                    for (int j = i + 1; j < ListaaDepurar.Count; j++)
                    {
                        string Nomenclatura2 = ListaaDepurar[j].Item2;
                        if (Nomenclatura1 == Nomenclatura2)
                        {
                            Cantidad1 += ListaaDepurar[j].Item1;
                            VectorIndices.Add(j);
                        }
                    }
                    if (Cantidad1 != 0)
                    {
                        Lista_DllNetFinal.Add(Cantidad1 + Nomenclatura1);
                    }
                }
            }
        }

        #endregion Metodos - Cantidades de Obra DL NET

        #region Propiedades - Cargas que deben cumplor 0.4Ag*fc

        public List<List<Tuple<float,string,string,float>>> Panalizar { get; set; }


        #endregion
    }
}