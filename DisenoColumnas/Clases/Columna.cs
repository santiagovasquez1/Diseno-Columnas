using DisenoColumnas.Secciones;
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
        public Viga VigaMayor { get; set; }

        public List<float> LuzLibre { get; set; }

        public List<float> LuzAcum { get; set; }

        public List<ResultadosETABS> resultadosETABs { get; set; }

        public List<Estribo> estribos { get; set; } = new List<Estribo>();

        public List<Alzado> Alzados { get; set; } = new List<Alzado>();

        public List<int> Prueba = new List<int>();

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

        public string ColSimilName { get; set; }

        #endregion Propiedades - Auxiliares

        #region Metodos-Calculos

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
                                resultadosETABs[i - 1].As_asignado[2] += alzado.Colum_Alzado[i].CantBarras * AreaBarra;     //Acero Botton - Vecino
                            }
                            catch { }
                            resultadosETABs[i].As_asignado[0] += alzado.Colum_Alzado[i].CantBarras * AreaBarra;
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

        #endregion Metodos-Calculos


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

            if (Ready)
            {
                BrushesColor = Brushes.Blue;
            }
            else
            {
                if (BrushesColor != Brushes.Red)
                {
                    BrushesColor = Brushes.Black;
                }
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
                Ready = false;
                return this;
            }
            else
            {
                BrushesColor = Brushes.Black;
                return null;
            }

        }

        public void MouseDobleClick(MouseEventArgs mouse)
        {
            if (X_Colum <= mouse.X && X_Colum + w >= mouse.X && Y_Colum <= mouse.Y && Y_Colum + h >= mouse.Y)
            {
                if (Ready == true)
                {
                    Ready = false;
;
                }
                else { Ready = true; }
         
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

            columna.Col_Row_AlzadoBaseSugerido = new List<List<string>>();
            for (int i = 0; i < columna.AlzadoBaseSugerido[0].Length; i++)
            {
                Alzado alzado = new Alzado(i + 1, columna.Seccions.Count);
                columna.Alzados.Add(alzado);
                columna.Col_Row_AlzadoBaseSugerido.Add(new List<string>());
            }

            for (int i = columna.Seccions.Count - 1; i >= 0; i--)
            {
                try
                {
                    if (columna.Seccions[i].Item1.B != columna.Seccions[i - 1].Item1.B)
                    {
                        Alzado alzado = new Alzado(columna.Alzados.Count + 1, columna.Seccions.Count);
                        columna.Alzados.Add(alzado);
                        columna.Col_Row_AlzadoBaseSugerido.Add(new List<string>());

                        Alzado alzado2 = new Alzado(columna.Alzados.Count + 1, columna.Seccions.Count);
                        columna.Alzados.Add(alzado2);
                        columna.Col_Row_AlzadoBaseSugerido.Add(new List<string>());
                    }
                }
                catch
                {
                }
            }

            for (int i = 0; i < columna.Col_Row_AlzadoBaseSugerido.Count; i++)
            {
                for (int j = 0; j < columna.LuzLibre.Count; j++)
                {
                    columna.Col_Row_AlzadoBaseSugerido[i].Add("");
                }
            }

            for (int j = 0; j < columna.Alzados.Count; j++)
            {
                int CorrerAlzado = 0;
                for (int i = columna.AlzadoBaseSugerido.Count - 1; i >= 0; i--)
                {
                    string Combinacion = "";

                    try
                    {
                        if (columna.Seccions[i].Item1.B != columna.Seccions[i + 1].Item1.B)
                        {
                            CorrerAlzado = 2;
                        }
                    }
                    catch { }

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
                                    if (columna.Seccions[i].Item1.B != columna.Seccions[i - 1].Item1.B && CorrerAlzado == 0)
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

                                if (columna.Seccions[i].Item1.B != columna.Seccions[i + 1].Item1.B && CorrerAlzado == 2)
                                {
                                    Combinacion += "T2";
                                }
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

                                if (columna.Seccions[i].Item1.B != columna.Seccions[i - 1].Item1.B && CorrerAlzado == 0)
                                {
                                    if ((columna.AlzadoBaseSugerido.Count - i) % 2 == 0)
                                    {
                                        Combinacion += "T1";
                                    }
                                    else
                                    { Combinacion += "T3"; }
                                }

                                if (columna.Seccions[i].Item1.B != columna.Seccions[i + 1].Item1.B && CorrerAlzado == 2)
                                {
                                    Combinacion += "T2";
                                }
                            }
                        }
                    }

                    if (j + CorrerAlzado < columna.Col_Row_AlzadoBaseSugerido.Count)
                    {
                        columna.Col_Row_AlzadoBaseSugerido[j + CorrerAlzado][i] = Combinacion;
                    }
                }
            }

            for (int i = 0; i < columna.Col_Row_AlzadoBaseSugerido.Count; i++)
            {
                for (int j = columna.Col_Row_AlzadoBaseSugerido[i].Count - 1; j >= 0; j--)
                {
                    CrearAlzado(i, j, false, columna, columna.Col_Row_AlzadoBaseSugerido[i][j]);
                }
            }
            CalcularPesoAcero();
        }

        private void CrearAlzado(int IndiceC, int IndiceR, bool isNotPaste, Columna ColumnaSelect, string ValorCelda)
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

                    //AÑADIR REFUERZO ADICIONAL

                    string RefAd_Str = (string)Clasficiacion[4];

                    if (RefAd_Str == "-")
                    {
                        int CantBarrasA = (int)Clasficiacion[5];
                        int NoBarraA = (int)Clasficiacion[6];
                        unitario.UnitarioAdicional = new AlzadoUnitario(CantBarrasA, NoBarraA, "ABotton", NoPiso, IndiceC + 1, H, Hviga, Form1.Proyecto_.e_Fundacion, UltimPiso, Hacum);
                    }

                    ColumnaSelect.Alzados[IndiceC].Colum_Alzado[IndiceR] = unitario;
                    ModificarTraslapo(IndiceC, ref ColumnaSelect);
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
            ColumnaSelect.ActualizarRefuerzo();

            if (isNotPaste == false)
            {
                DeterminarCoordAlzado(IndiceC, ColumnaSelect);
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

                int VecesNumeral = 0;
                for (int i = 0; i < Celda.Length; i++)
                {
                    if (Celda.Substring(i, 1) == "#" && VecesNumeral < 1)
                    {
                        VecesNumeral += 1;
                        CantidadBarras = Convert.ToInt32(Celda.Substring(0, i));
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
                            for (int j = 0; j < AuxAd.Length; j++)
                            {
                                if (Celda.Substring(j, 1) == "-")
                                {
                                    Raya = Celda.Substring(j, 1);
                                }

                                if (Celda.Substring(j, 1) == "#")
                                {
                                    CantidadBarrasA = Convert.ToInt32(Celda.Substring(0, j));
                                    try
                                    {
                                        NoBarraA = Convert.ToInt32(Celda.Substring(j + 1, 2));
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            NoBarraA = Convert.ToInt32(Celda.Substring(j + 1, 1));
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
                    var o = Form1.Proyecto_.Ld_210[NoBarra];
                    if (Raya == "-") { var m = Form1.Proyecto_.Ld_210[NoBarraA]; }
                    if (CantidadBarras < 0)
                    {
                        CantidadBarras = -CantidadBarras;
                        Traslap = "Botton";
                    }
                    Clasf = new object[] { "Ok", CantidadBarras, NoBarra, Traslap, Raya, CantidadBarrasA, NoBarraA };
                }
                catch
                {
                }
            }

            return Clasf;
        }

        public void DeterminarCoordAlzado(int Col, Columna ColumnaSelect)
        {
            float DisG = 0.2f; float r = 0.08f; float eF = Form1.Proyecto_.e_Fundacion;
            float LdAd = 0.4f;

            Alzado a = ColumnaSelect.Alzados[Col];
            float diX = 0;
            //Agregar Distancia X a cada Alzado
            for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
            {
                AlzadoUnitario au = a.Colum_Alzado[i];
                try
                {
                    au.x1 = diX;
                }
                catch { }
                diX += 0.1f;
                if (diX > 0.1f)
                {
                    diX = 0f;
                }
            }

            for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
            {
                AlzadoUnitario au = a.Colum_Alzado[i];
                if (au != null)
                {
                    for (int j = a.Colum_Alzado.Count - 1; j >= i; j--)
                    {
                        if (i != j)
                        {
                            try
                            {
                                if (au.Tipo == "T2" && a.Colum_Alzado[j].Tipo == "T2" || au.Tipo == "T2" && a.Colum_Alzado[j].Tipo == "T3" || au.Tipo == "T2" && a.Colum_Alzado[j].Tipo == "T1" || au.Tipo == "T2" && a.Colum_Alzado[j].Tipo == "T4")
                                {
                                    if (au.x1 == a.Colum_Alzado[j].x1)
                                    {
                                        au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                    }
                                }

                                if (au.Tipo == "T4" && a.Colum_Alzado[j].Tipo == "T4")
                                {
                                    au.x1 = a.Colum_Alzado[j].x1;
                                }

                                if (au.Tipo == "T1" && a.Colum_Alzado[j].Tipo == "T2" || au.Tipo == "T1" && a.Colum_Alzado[j].Tipo == "T4")
                                {
                                    if (au.x1 == a.Colum_Alzado[j].x1)
                                    {
                                        au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                    }
                                }
                                if (au.Tipo == "T3" && a.Colum_Alzado[j].Tipo == "T3" || au.Tipo == "T3" && a.Colum_Alzado[j].Tipo == "T2" || au.Tipo == "T3" && a.Colum_Alzado[j].Tipo == "T4")
                                {
                                    if (au.x1 == a.Colum_Alzado[j].x1)
                                    {
                                        au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                    }
                                }
                            }
                            catch { }
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
                        float[] XY2 = new float[] { au.x1 + a.DistX, au.Hacum - r };
                        float[] XY3 = new float[] { au.x1 + a.DistX + DisG, au.Hacum - r };
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
                        float[] XY2 = new float[] { au.x1 + a.DistX, au.Hacum - r };
                        float[] XY3 = new float[] { au.x1 + a.DistX + DisG, au.Hacum - r };
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
                            float[] XY3 = new float[] { au.x1 + a.DistX, au.Hacum - r };
                            float[] XY4 = new float[] { au.x1 + a.DistX + DisG, au.Hacum - r };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3); au.Coord_Alzado_PB.Add(XY4);
                        }
                        else
                        {
                            float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                            float[] XY2 = new float[] { au.x1 + a.DistX, r };
                            float[] XY3 = new float[] { au.x1 + a.DistX, Hacum2 - r };
                            float[] XY4 = new float[] { au.x1 + a.DistX + DisG, Hacum2 - r };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3); au.Coord_Alzado_PB.Add(XY4);
                        }
                    }

                    if (au.UltPiso && au.Tipo == "A")   // Ultimo Piso Con Refuerzo Adicional Parte Superior
                    {
                        float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r };
                        float[] XY2 = new float[] { a.DistX, au.Hacum - r };
                        float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                    }

                    if (au.NoStory == 1 && au.UnitarioAdicional != null)  //Primer Piso Con Refuerzo Adicional Parte Inferior
                    {
                        au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                        float[] XY1 = new float[] { a.DistX + DisG, r };
                        float[] XY2 = new float[] { a.DistX, r };
                        float[] XY3 = new float[] { a.DistX, eF + au.Traslapo + LdAd };
                        au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY3);
                    }

                    if (au.NoStory != 1 && au.UnitarioAdicional != null)  //Refuerzo Adicional Parte Inferior "Cualquier Piso"
                    {
                        au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                        float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                        float[] XY2 = new float[] { a.DistX, Hacum1 + au.Traslapo + LdAd };
                        au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2);
                    }

                    if (au.UltPiso == false && au.Tipo == "A")  //Refuerzo Adicional Parte Superior "Cualquier Piso"
                    {
                        float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                        float[] XY2 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                    }

                    if (au.NoStory == 1 && au.Tipo == "Botton")  //Primer Piso Con Refuerzo Adicional Parte Inferior
                    {
                        float[] XY1 = new float[] { a.DistX + DisG, r };
                        float[] XY2 = new float[] { a.DistX, r };
                        float[] XY3 = new float[] { a.DistX, eF + au.Traslapo + LdAd };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                    }

                    if (au.NoStory != 1 && au.Tipo == "Botton")  //Refuerzo Adicional Parte Inferior "Cualquier Piso"
                    {
                        float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                        float[] XY2 = new float[] { a.DistX, Hacum1 + au.Traslapo + LdAd };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                    }
                }
            }
        }

        private void ModificarTraslapo(int Indice, ref Columna columna)
        {
            for (int i = columna.Alzados[Indice].Colum_Alzado.Count - 1; i >= 0; i--)
            {
                if (columna.Alzados[Indice].Colum_Alzado[i] != null)
                {
                    try
                    {
                        if (columna.Alzados[Indice].Colum_Alzado[i].NoBarra > columna.Alzados[Indice].Colum_Alzado[i - 1].NoBarra)
                        {
                            float FC = columna.Seccions[i].Item1.Material.FC;
                            if (FC == 210f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_210[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 280f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_280[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 350f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_350[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 420f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_420[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 490f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_490[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 560f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_560[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        }
                        else
                        {
                            float FC = columna.Seccions[i].Item1.Material.FC;
                            if (FC == 210f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_210[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 280f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_280[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 350f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_350[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 420f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_420[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 490f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_490[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 560f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_560[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                        }
                    }
                    catch
                    {
                        float FC = columna.Seccions[i].Item1.Material.FC;
                        if (FC == 210f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_210[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 280f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_280[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 350f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_350[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 420f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_420[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 490f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_490[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 560f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_560[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
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

            float MaxB = -999999;
            for (int i = 0; i < Seccions.Count; i++)
            {
                if (Seccions[i].Item1 != null)
                {
                    if (Seccions[i].Item1.B > MaxB)
                    {
                        MaxB = Seccions[i].Item1.B;
                    }
                }
            }

            //Dibujar Cuadro Fundación

            float B_Fund = Alzados[Alzados.Count - 1].DistX;
            double[] Ver_Fund = new double[] {X,Y+Form1.Proyecto_.e_Fundacion,
                                               X,Y,
                                               X+B_Fund+DPR,Y,
                                               X+B_Fund+DPR,Y+Form1.Proyecto_.e_Fundacion };

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Ver_Fund, LayerCuadro, false);

            //Cota espesor Fundación

            double[] P1_CotaF = new double[] { X, Y, 0 };
            double[] P2_CotaF = new double[] { X, Y + Form1.Proyecto_.e_Fundacion, 0 };
            FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaF, P2_CotaF, "FC_COTAS", "FC_TEXT1", -0.3f);

            //Dibujar Cada Entre Piso
            for (int i = LuzAcum.Count - 1; i >= 0; i--)
            {
                float B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[i].Item1.B) / MaxB) + DPR;

                double[] Ver_Colum = new double[] {X,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H,
                                                   X,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H-LuzLibre[i],
                                                   X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H-LuzLibre[i],
                                                   X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H };
                FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Ver_Colum, LayerCuadro);

                float DesCota = -0.3f;
                //Cotas entre piso y espesor de Viga

                //Cotas entre Piso

                double[] P1_CotaT = new double[] { X, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i], 0 };
                double[] P2_CotaT = new double[] { X, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H, 0 };
                FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);

                //Cotas Viga
                P1_CotaT = new double[] { X, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H, 0 };
                P2_CotaT = new double[] { X, Y + LuzAcum[i], 0 };
                FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);

                if (i != 0)
                {
                    //Lineas entre Piso

                    double[] VerLosa1 = new double[] {X,Y+LuzAcum[i] -VigaMayor.Seccions[i].Item1.H,
                                                   X,Y+LuzAcum[i] };

                    FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa1, LayerCuadro, false);

                    double[] VerLosa2 = new double[] {X+B_Draw,Y+LuzAcum[i],
                                                   X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H};

                    FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa2, LayerCuadro, false);
                }


                try
                {
                    if(Seccions[i].Item1.B != Seccions[i - 1].Item1.B)
                    {
                        float B_Draw2 = ((Alzados[Alzados.Count - 1].DistX * Seccions[i-1].Item1.B) / MaxB) + DPR;

                        double[] LineaFaltante1 = new double[] { X+B_Draw, Y + LuzAcum[i],
                                                                 X+B_Draw2,Y+ LuzAcum[i]};
                        FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(LineaFaltante1, LayerCuadro, false);


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

                float B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[IndiceI].Item1.B) / MaxB) + DPR;
                double[] CotaFc1 = new double[] { X + B_Draw, Y + LuzAcum[IndiceI] - VigaMayor.Seccions[IndiceI].Item1.H - LuzLibre[IndiceI], 0 };
                B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[IndiceI].Item1.B) / MaxB) + DPR;
                double[] CotaFc2 = new double[] { X + B_Draw, Y + LuzAcum[IndiceF], 0 };
                string TextCota; float DesplazCota = 0.5f;
                if (IndiceI - IndiceF > 2)
                {
                    TextCota = @"{\H1.33333x;\C11; Resistencia a la compresión del concreto a los 28 días\Pf'c=" + Resitencias[i] + @"kgf/cm²\C256; }";
                }
                else
                {
                    DesplazCota = 0.7f;
                    TextCota = @"{\H1.33333x;\C11; Resistencia a la compresión del\Pconcreto a los 28 días\Pf'c=" + Resitencias[i] + @"kgf/cm²\C256; }";
                }
                //FunctionsAutoCAD.FunctionsAutoCAD.AddCota(CotaFc1, CotaFc2, "FC_COTAS", "FC_TEXT1", DesplazCota, headType1: FunctionsAutoCAD.ArrowHeadType.ArrowDefault, Text: TextCota,
                //    headType2: FunctionsAutoCAD.ArrowHeadType.ArrowDefault, TextRotation: 90, ArrowheadSize: 0.002);

                double[] P_XYZRes = new double[] { CotaFc1[0] + 0.4f, CotaFc1[1] + (CotaFc2[1] - CotaFc1[1]) / 2 - 3f / 2, 0 };

                //FunctionsAutoCAD.FunctionsAutoCAD.AddText(MsgFC + Resitencias[i] + " kgf/cm²", P_XYZRes, 6f, 0.1, LayerString, "ROMANS",
                // 90, justifyText: FunctionsAutoCAD.JustifyText.Center, Width2: 3f);
            }

            #endregion Resitencia

            // Espesor  Losa Final

            double B_DrawF = ((Alzados[Alzados.Count - 1].DistX * Seccions[0].Item1.B) / MaxB) + DPR;
            double[] VerLosa_Final = new double[] {X,Y+LuzAcum[0] -VigaMayor.Seccions[0].Item1.H,
                                                   X,Y+LuzAcum[0],
                                                   X+B_DrawF,Y+LuzAcum[0],
                                                   X+B_DrawF,Y+LuzAcum[0]-VigaMayor.Seccions[0].Item1.H };
            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa_Final, LayerCuadro, false);

            //Agregar Cuadro, Titulo
            double DistCT = 0.5f;

            double[] Vert_CT = new[] {X,Y+ LuzAcum[0],
                                      X,Y+LuzAcum[0]+DistCT,
                                      X+B_DrawF,Y+LuzAcum[0]+DistCT,
                                      X+B_DrawF,Y+ LuzAcum[0]};
            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vert_CT, LayerCuadro, false);
            string TitleDespi = "DESPIECE " + NoDespiece;
            double DistCorrerTextB = 0.15 * (TitleDespi.Length - 1);
            double[] P_XYZ = new double[] { X + B_DrawF / 2 - DistCorrerTextB / 2, Y + LuzAcum[0] + DistCT / 2 + 0.05, 0 };
            FunctionsAutoCAD.FunctionsAutoCAD.AddText(TitleDespi, P_XYZ, 0.75, 0.10, LayerTitiles, "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center);

            //Agregar Cuadro, Nombre Columnas

            //Caso cuando las secciones son Rectangulares
            string Dimensiones = "(" + Seccions[Seccions.Count - 1].Item1.B * 100 + "x" + Seccions[Seccions.Count - 1].Item1.H * 100 + ")";
            string NamesColumns = Names + @"\P" + Dimensiones;

            float Altu_Names = 1f;
            DistCorrerTextB = 0.15 * (Dimensiones.Length + 1.5);

            double B_DrawI = ((Alzados[Alzados.Count - 1].DistX * Seccions[Seccions.Count - 1].Item1.B) / MaxB) + DPR;

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
                if (Alzados.Count == 2  & a.ID == 1)
                {
                    PXYZ = new double[] { X + a.DistX -DPR/2-0.15, Y - 0.12, 0 };
                }
                else if (Alzados.Count == 2 & a.ID == 2)
                {
                    PXYZ = new double[] { X + a.DistX  + DPR / 3-0.15, Y - 0.12, 0 };
                }
               

                FunctionsAutoCAD.FunctionsAutoCAD.AddText(Convert.ToString(a.ID), PXYZ, 0.5, 0.08, "FC_R-100", "FC_TEXT1", 0, justifyText: FunctionsAutoCAD.JustifyText.Center,Width2: 0.5);

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

            FunctionsAutoCAD.FunctionsAutoCAD.B_NivelSeccion(new double[] { X, Y + eF, 0 }, NivelFund, 1.08, 1.90, false, "FC_COTAS", 75, 75, 75, 0);
            float NivelN = N_F;
            int NoLosa = 0;
            for (int i = LuzAcum.Count - 1; i >= 0; i--)
            {
                NoLosa += 1;
                string Nivel;
                double[] PXYZ;

                PXYZ = new double[] { X, Y + LuzAcum[i], 0 };
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
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0] - DPR / 2, Y + aux.Coord_Alzado_PB[1][1] };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText - DPR / 2, Y + aux.Hacum - aux.Hviga - aux.H_Stroy / 2 - DistCorrerTextY / 2, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[1][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[1][1] + aux.Traslapo, 0 };
                }
                else if (Alzados.Count == 2 && a.ID == 2)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0] + DPR / 3, Y + aux.Coord_Alzado_PB[1][1] };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText + DPR / 3, Y + aux.Hacum - aux.Hviga - aux.H_Stroy / 2 - DistCorrerTextY / 2, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[1][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[1][1] + aux.Traslapo, 0 };
                }
                else
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0], Y + aux.Coord_Alzado_PB[1][1] };
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
                                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum, 0 };
                                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
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
                                P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum, 0 };
                                P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                                P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
                            }
                        }
                        catch
                        {
                            DesCota = -0.2f;
                            P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[0][0] - DistCorrerText, Y + aux.Hacum, 0 };
                            P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                            P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga, 0 };
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

                if (aux.Tipo == "T1" && aux.NoStory != 1 || aux.Tipo == "A" && aux.NoStory != 1)
                {
                    Ytext = Y + aux.Coord_Alzado_PB[0][1] + (aux.Coord_Alzado_PB[2][1] - aux.Coord_Alzado_PB[0][1]) / 2;
                }
                else if (aux.Tipo == "T1" && aux.NoStory == 1 || aux.Tipo == "Botton" && aux.NoStory == 1 || aux.Tipo == "ABotton" && aux.NoStory == 1)
                {
                    Ytext = Y + aux.Coord_Alzado_PB[0][1] + Form1.Proyecto_.e_Fundacion;
                }

                if (Alzados.Count == 2 && a.ID == 1)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0] - DPR / 2, Y + aux.Coord_Alzado_PB[1][1], X + aux.Coord_Alzado_PB[2][0] - DPR / 2, Y + aux.Coord_Alzado_PB[2][1] };

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
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0] + DPR / 3, Y + aux.Coord_Alzado_PB[1][1], X + aux.Coord_Alzado_PB[2][0] + DPR / 3, Y + aux.Coord_Alzado_PB[2][1] };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText + DPR / 3, Ytext, 0 };

                    if (aux.NoStory != 1)
                    {
                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[0][1], 0 };
                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[0][1] + aux.Traslapo, 0 };
                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                    }
                    else
                    {
                        DesCota = 0;
                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[0][1], 0 };
                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
                    }
                }
                else
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0], Y + aux.Coord_Alzado_PB[1][1], X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1] };
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
                        DesCota = 0;
                        P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], 0 };
                        P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);
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
                double Ytext = Y + aux.Coord_Alzado_PB[0][1] + Form1.Proyecto_.e_Fundacion;

                double XCota = X + aux.Coord_Alzado_PB[0][0];
                float DesCota = 0;

                if (Alzados.Count == 2 && a.ID == 1)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0] - DPR / 2, Y + aux.Coord_Alzado_PB[1][1], X + aux.Coord_Alzado_PB[2][0] - DPR / 2, Y + aux.Coord_Alzado_PB[2][1], X + aux.Coord_Alzado_PB[3][0] - DPR / 2, Y + aux.Coord_Alzado_PB[3][1] };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText - DPR / 2, Ytext, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Coord_Alzado_PB[0][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] - DPR / 2, Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                }
                else if (Alzados.Count == 2 && a.ID == 2)
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0] + DPR / 3, Y + aux.Coord_Alzado_PB[1][1], X + aux.Coord_Alzado_PB[2][0] + DPR / 3, Y + aux.Coord_Alzado_PB[2][1], X + aux.Coord_Alzado_PB[3][0] + DPR / 3, Y + aux.Coord_Alzado_PB[3][1] };
                    P_XYZ_Text = new double[] { X + aux.Coord_Alzado_PB[1][0] - DistCorrerText + DPR / 3, Ytext, 0 };
                    P1_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Coord_Alzado_PB[0][1], 0 };
                    P2_CotaT = new double[] { X + aux.Coord_Alzado_PB[0][0] + DPR / 3, Y + aux.Hacum - aux.Hviga - aux.H_Stroy, 0 };
                }
                else
                {
                    Coord_Refuerz = new double[] { X + aux.Coord_Alzado_PB[0][0], Y + aux.Coord_Alzado_PB[0][1], X + aux.Coord_Alzado_PB[1][0], Y + aux.Coord_Alzado_PB[1][1], X + aux.Coord_Alzado_PB[2][0], Y + aux.Coord_Alzado_PB[2][1], X + aux.Coord_Alzado_PB[3][0], Y + aux.Coord_Alzado_PB[3][1] };
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

        public void CalcularPesoAcero()
        {
            KgRefuerzo = 0;
            foreach (Alzado a in Alzados)
            {
                for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
                {
                    AlzadoUnitario aux = a.Colum_Alzado[i];

                    if (aux != null)
                    {
                        if (aux.Coord_Alzado_PB.Count != 0)
                        {
                            KgRefuerzo += CalcularLongitudRefuerzo(aux.Coord_Alzado_PB) * Form1.Proyecto_.MasaNominalBarras[aux.NoBarra] * aux.CantBarras;
                        }
                        if (aux.UnitarioAdicional != null)
                        {
                            KgRefuerzo += CalcularLongitudRefuerzo(aux.UnitarioAdicional.Coord_Alzado_PB) * Form1.Proyecto_.MasaNominalBarras[aux.UnitarioAdicional.NoBarra] * aux.UnitarioAdicional.CantBarras;
                        }
                    }
                }
            }
        }

        private float CalcularLongitudRefuerzo(List<float[]> Coordenadas)
        {
            float Longitud = 0;
            for (int i = 0; i < Coordenadas.Count; i++)
            {
                try
                {
                    Longitud += (float)Math.Sqrt(Math.Pow(Coordenadas[i][0] - Coordenadas[i - 1][0], 2) + Math.Pow(Coordenadas[i][1] - Coordenadas[i - 1][1], 2));
                }
                catch { }
            }
            return Longitud;
        }

        #endregion Metodos - Calcular Peso de Acero
    }
}