using DisenoColumnas.Clases;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Diseño
{
    public partial class Despiece : DockContent
    {
        public Despiece()
        {
            InitializeComponent();
            
        }

        private void Draw_Column_Paint(object sender, PaintEventArgs e)
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;
            if (ColumnaSelect != null)
            {

                KgRefuerzo_L.Text = Math.Round(ColumnaSelect.KgRefuerzo,2) + " kg";

                KgRefuerzo_L.Location = new Point(250- KgRefuerzo_L.Width, 7);

                float MaxB = -999999;
                for (int i = 0; i < ColumnaSelect.Seccions.Count; i++)
                {
                    if (ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        if (ColumnaSelect.Seccions[i].Item1.B > MaxB)
                        {
                            MaxB = ColumnaSelect.Seccions[i].Item1.B;
                        }
                    }
                }

                float SX = (Draw_Column.Width - 15) / MaxB;

                float Altura = 0;

                for (int i = 0; i < ColumnaSelect.LuzLibre.Count; i++)
                {
                    Altura += ColumnaSelect.LuzLibre[i] + ColumnaSelect.VigaMayor.Seccions[i].Item1.H;


                }
                Altura += Form1.Proyecto_.e_Fundacion;
                float SY = (Draw_Column.Height - 5) / (Altura);
                float X = 7.5f;
                float Y = 5;

                e.Graphics.Clear(Color.White);
                Title_Colum_Model.Text = "Columna: " + ColumnaSelect.Name;
                ColumnaSelect.Paint_Alzado1(e, Draw_Column.Height, Draw_Column.Width, SX, SY, X, Y);
            }
        }

        private void Despiece_Paint(object sender, PaintEventArgs e)
        {
            Draw_Column.Invalidate();
            Draw_Colum_Alzado.Invalidate();

        }

        private void Draw_Colum_Alzado_Paint(object sender, PaintEventArgs e)
        {

            float Height = Draw_Colum_Alzado.Height;
            float Width = Draw_Colum_Alzado.Width;
            float MaxB = -99999;

            float SX, SY, YI, XI;
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            if (ColumnaSelect != null)
            {

                float Altura = 0;

                for (int i = 0; i < ColumnaSelect.LuzLibre.Count; i++)
                {
                    Altura += ColumnaSelect.LuzLibre[i] + ColumnaSelect.VigaMayor.Seccions[i].Item1.H;


                }
                Altura += Form1.Proyecto_.e_Fundacion;

                for (int i = 0; i < ColumnaSelect.Seccions.Count; i++)
                {
                    if (ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        if (ColumnaSelect.Seccions[i].Item1.B > MaxB)
                        {
                            MaxB = ColumnaSelect.Seccions[i].Item1.B;
                        }
                    }
                }


                SX = (Width - 15) / 10;
                SY = (Height - 5) / Altura;
                YI = 5f;
                XI = 30;


                //Crear Lineas Divisorias
                Pen P_LD = new Pen(Color.LightGray);
                P_LD.StartCap = System.Drawing.Drawing2D.LineCap.Triangle;
                P_LD.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                P_LD.Width = 1;
                float TamanoFuente = 0.07f * (SX+SY);
                if (ColumnaSelect.LuzLibre[0] * SY - ColumnaSelect.VigaMayor.Seccions[0].Item1.H * SY < TamanoFuente)
                {
                    TamanoFuente = ColumnaSelect.LuzLibre[0] * SY - ColumnaSelect.VigaMayor.Seccions[0].Item1.H * SY;
                }


                Font Fuente = new Font("Calibri", TamanoFuente, FontStyle.Bold);
                if (Fuente.Height > 0.4f * SX)
                {
                    Fuente = new Font("Calibri", 0.05f * (SX + SY), FontStyle.Bold);
                }
                e.Graphics.DrawLine(P_LD, 0, Height - YI - Form1.Proyecto_.e_Fundacion * SY, Width, Height - YI - Form1.Proyecto_.e_Fundacion*SY);
                PointF PointStringF = new PointF(0.01f * SX, Height - YI - Form1.Proyecto_.e_Fundacion/2*SY - Fuente.Height / 2);
                e.Graphics.DrawString("Fund.", Fuente, Brushes.Black, PointStringF);

                for (int i = 0; i < ColumnaSelect.LuzAcum.Count; i++)
                {
                    //Pisos
                    PointF PointString = new PointF(0.01f * SX, Height - YI - ColumnaSelect.LuzAcum[i] * SY+ColumnaSelect.VigaMayor.Seccions[i].Item1.H*SY/2- Fuente.Height/2);
                    e.Graphics.DrawString(ColumnaSelect.Seccions[i].Item2, Fuente, Brushes.Black, PointString);

                    e.Graphics.DrawLine(P_LD, 0, Height - YI - ColumnaSelect.LuzAcum[i] * SY, Width, Height - YI - ColumnaSelect.LuzAcum[i] * SY);
                    e.Graphics.DrawLine(P_LD, 0, Height - YI - (ColumnaSelect.LuzAcum[i] - ColumnaSelect.VigaMayor.Seccions[i].Item1.H) * SY, Width, Height - YI - (ColumnaSelect.LuzAcum[i] - ColumnaSelect.VigaMayor.Seccions[i].Item1.H) * SY);

                }



                //Graficar Alzado
                for (int i = 0; i < ColumnaSelect.Alzados.Count; i++)
                {
                    for (int j = 0; j < ColumnaSelect.Alzados[i].Colum_Alzado.Count; j++)
                    {

                        if (ColumnaSelect.Alzados[i].Colum_Alzado[j] != null)
                        {
                            ColumnaSelect.Alzados[i].Colum_Alzado[j].Paint(e, SX, SY, Height, YI, XI);

                            if (ColumnaSelect.Alzados[i].Colum_Alzado[j].UnitarioAdicional != null)
                            {
                                ColumnaSelect.Alzados[i].Colum_Alzado[j].UnitarioAdicional.Paint(e, SX, SY, Height, YI, XI);
                            }
                        }
                    }

                }














            }


        }






        private void MaxXyY(Columna ColumnaSelect, ref float MaxX, ref float MaxY)
        {
            for (int i = 0; i < ColumnaSelect.Alzados.Count; i++)
            {

                for (int j = 0; j < ColumnaSelect.Alzados[i].Colum_Alzado.Count; j++)
                {
                    if (ColumnaSelect.Alzados[i].Colum_Alzado[j].Coord_Alzado_PB != null)
                    {
                        for (int s = 0; s < ColumnaSelect.Alzados[i].Colum_Alzado[j].Coord_Alzado_PB.Count; s++)
                        {

                            if (ColumnaSelect.Alzados[i].Colum_Alzado[j].Coord_Alzado_PB[s][0] > MaxX)
                            {
                                MaxX = ColumnaSelect.Alzados[i].Colum_Alzado[j].Coord_Alzado_PB[s][0];
                            }

                            if (ColumnaSelect.Alzados[i].Colum_Alzado[j].Coord_Alzado_PB[s][1] > MaxY)
                            {
                                MaxY = ColumnaSelect.Alzados[i].Colum_Alzado[j].Coord_Alzado_PB[s][0];
                            }

                        }
                    }
                }
            }



        }



        //

        //private void PictureBox2_Paint(object sender, PaintEventArgs e)
        //{
        //    if (ColumnaSelect != null)
        //    {
        //        if(ColumnaSelect.StoryMostrar == -1)
        //        {
        //            ColumnaSelect.StoryMostrar = ColumnaSelect.LuzLibre.Count - 1;

        //        }
        //        e.Graphics.Clear(Color.White);

        //        double MaxAs = -999999;
        //        for (int i = 0; i < ColumnaSelect.resultadosETABs.Count; i++)
        //        {
        //            if (ColumnaSelect.resultadosETABs[i] != null)
        //            {
        //                for (int j = 0; j < ColumnaSelect.resultadosETABs[i].As.Count; j++)
        //                {
        //                    if (ColumnaSelect.resultadosETABs[i].As[j] > MaxAs)
        //                    {
        //                        MaxAs = ColumnaSelect.resultadosETABs[i].As[j];
        //                    }
        //                }
        //            }
        //        }

        //        float SX =  (RefuerzoRequerido.Width - 10) / (float)MaxAs;
        //        int IndiceN = ColumnaSelect.StoryMostrar;
        //        float AltoEscalar;
        //        try
        //        {
        //            AltoEscalar = ColumnaSelect.LuzLibre[IndiceN]+ ColumnaSelect.LuzLibre[IndiceN-1];
        //        }
        //        catch
        //        {
        //            AltoEscalar = ColumnaSelect.LuzLibre[IndiceN];
        //        }

        //        float SY = (RefuerzoRequerido.Height-30) / AltoEscalar;
        //        float X = 7.5f;
        //        float Y = 5;

        //        string MensajeaMostrar;

        //        try
        //        {
        //            MensajeaMostrar = ColumnaSelect.Seccions[IndiceN].Item2 +" - "+ ColumnaSelect.Seccions[IndiceN-1].Item2;
        //        }
        //        catch
        //        {
        //            MensajeaMostrar = ColumnaSelect.Seccions[IndiceN].Item2;
        //        }
        //        CambiarPiso.Text = MensajeaMostrar;

        //        //Lineas Inicales
        //        float x1, y1, x2, y2;

        //        x1 = 5;
        //        x2 = 5;
        //        float YI = -15;
        //        y1 = 0;
        //        //y1 = (float)ColumnaSelect.resultadosETABs[ColumnaSelect.StoryMostrar].Estacion[ColumnaSelect.resultadosETABs[ColumnaSelect.StoryMostrar].Estacion.Count-1];

        //        try
        //        {
        //            y2= ColumnaSelect.LuzLibre[IndiceN] + ColumnaSelect.LuzLibre[IndiceN - 1] + (float)ColumnaSelect.resultadosETABs[IndiceN - 1 ].Estacion[0];

        //        }
        //        catch
        //        {
        //           y2 = (float)ColumnaSelect.LuzLibre[IndiceN] + (float)ColumnaSelect.resultadosETABs[IndiceN].Estacion[0];

        //        }

        //        e.Graphics.DrawLine(new Pen(Brushes.Black), x1, YI- y1*SY+RefuerzoRequerido.Height, x2, YI-y2*SY+RefuerzoRequerido.Height);

        //        for (int i = 0; i < ColumnaSelect.resultadosETABs[IndiceN].Estacion.Count; i++)
        //        {
        //            try
        //            {
        //                x2 = (float)ColumnaSelect.resultadosETABs[IndiceN - 1].As[i];
        //                y2 = ColumnaSelect.LuzLibre[IndiceN] + ColumnaSelect.LuzLibre[IndiceN - 1]-(float)ColumnaSelect.resultadosETABs[IndiceN - 1].Estacion[i];

        //                e.Graphics.DrawLine(new Pen(Brushes.Red), x1,YI -y2 * SY + RefuerzoRequerido.Height, x2 * SX,YI -y2 * SY + RefuerzoRequerido.Height);

        //            }
        //            catch {                    }

        //            x2 = (float)ColumnaSelect.resultadosETABs[IndiceN].As[i];
        //            y2 = ColumnaSelect.LuzLibre[IndiceN]  - ColumnaSelect.resultadosETABs[IndiceN].Estacion[i];

        //            e.Graphics.DrawLine(new Pen(Brushes.Red), x1,YI -y2 * SY + RefuerzoRequerido.Height, x2 * SX,YI -y2 * SY + RefuerzoRequerido.Height);

        //        }

        //    }
        //}

        //private void Up_Click(object sender, EventArgs e)
        //{
        //    if(ColumnaSelect != null)
        //    {
        //            ColumnaSelect.StoryMostrar = ColumnaSelect.StoryMostrar - 2;
        //            if (ColumnaSelect.StoryMostrar < 0)
        //            {
        //                ColumnaSelect.StoryMostrar = ColumnaSelect.LuzLibre.Count-1;
        //            }
        //            RefuerzoRequerido.Invalidate();

        //    }
        //}

        //private void Down_Click(object sender, EventArgs e)
        //{
        //    if (ColumnaSelect != null)
        //    {
        //        if (ColumnaSelect.StoryMostrar < ColumnaSelect.LuzLibre.Count - 1)
        //        {
        //            ColumnaSelect.StoryMostrar = ColumnaSelect.StoryMostrar + 2;
        //            RefuerzoRequerido.Invalidate();

        //        }

        //    }
        //}
    }
}
