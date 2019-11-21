using DisenoColumnas.Clases;
using System;
using System.Drawing;
using System.Linq;
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
                KgRefuerzo_L.Text = Math.Round(ColumnaSelect.KgRefuerzo, 2) + " kg";

                KgRefuerzo_L.Location = new Point(250 - KgRefuerzo_L.Width, 7);

                float MaxB = -999999; float MaxH = -999999;

                MaxB = ColumnaSelect.Seccions.FindAll(m => m != null).ToList().Max(m => m.Item1.B);
                MaxH = ColumnaSelect.Seccions.FindAll(m => m != null).ToList().Max(m => m.Item1.H);

                if (MaxH > MaxB)
                {
                    MaxB = MaxH;
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
                Ready_CheckBox.Checked = ColumnaSelect.Ready;
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
                if (MostrarGrid.Checked)
                {
                    Pen P_LD = new Pen(Color.LightGray);
                    P_LD.StartCap = System.Drawing.Drawing2D.LineCap.Triangle;
                    P_LD.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                    P_LD.Width = 1;
                    float TamanoFuente = 0.07f * (SX + SY);
                    if (ColumnaSelect.LuzLibre[0] * SY - ColumnaSelect.VigaMayor.Seccions[0].Item1.H * SY < TamanoFuente)
                    {
                        TamanoFuente = ColumnaSelect.LuzLibre[0] * SY - ColumnaSelect.VigaMayor.Seccions[0].Item1.H * SY;
                    }

                    Font Fuente = new Font("Calibri", TamanoFuente, FontStyle.Bold);
                    if (Fuente.Height > 0.4f * SX)
                    {
                        Fuente = new Font("Calibri", 0.05f * (SX + SY), FontStyle.Bold);
                    }
                    e.Graphics.DrawLine(P_LD, 0, Height - YI - Form1.Proyecto_.e_Fundacion * SY, Width, Height - YI - Form1.Proyecto_.e_Fundacion * SY);
                    PointF PointStringF = new PointF(0.01f * SX, Height - YI - Form1.Proyecto_.e_Fundacion / 2 * SY - Fuente.Height / 2);
                    e.Graphics.DrawString("Fund.", Fuente, Brushes.Black, PointStringF);

                    for (int i = 0; i < ColumnaSelect.LuzAcum.Count; i++)
                    {
                        //Pisos
                        PointF PointString = new PointF(0.01f * SX, Height - YI - ColumnaSelect.LuzAcum[i] * SY + ColumnaSelect.VigaMayor.Seccions[i].Item1.H * SY / 2 - Fuente.Height / 2);
                        e.Graphics.DrawString(ColumnaSelect.Seccions[i].Item2, Fuente, Brushes.Black, PointString);

                        e.Graphics.DrawLine(P_LD, 0, Height - YI - ColumnaSelect.LuzAcum[i] * SY, Width, Height - YI - ColumnaSelect.LuzAcum[i] * SY);
                        e.Graphics.DrawLine(P_LD, 0, Height - YI - (ColumnaSelect.LuzAcum[i] - ColumnaSelect.VigaMayor.Seccions[i].Item1.H) * SY, Width, Height - YI - (ColumnaSelect.LuzAcum[i] - ColumnaSelect.VigaMayor.Seccions[i].Item1.H) * SY);
                    }


                }


                // Mostrar Cajon - AutoCAD?

            








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

        private void Draw_Colum_Alzado_MouseMove(object sender, MouseEventArgs e)
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;
            if (ColumnaSelect != null)
            {
                for (int i = 0; i < ColumnaSelect.Alzados.Count; i++)
                {
                    for (int j = 0; j < ColumnaSelect.Alzados[i].Colum_Alzado.Count; j++)
                    {
                        if (ColumnaSelect.Alzados[i].Colum_Alzado[j] != null)
                        {
                            ColumnaSelect.Alzados[i].Colum_Alzado[j].MouseMove(e);
                            Draw_Colum_Alzado.Invalidate();

                            if (ColumnaSelect.Alzados[i].Colum_Alzado[j].UnitarioAdicional != null)
                            {
                                ColumnaSelect.Alzados[i].Colum_Alzado[j].UnitarioAdicional.MouseMove(e);
                                Draw_Colum_Alzado.Invalidate();
                            }
                        }
                    }
                }
            }
        }

        private void MostrarCajon(float X, float Y)
        {


            ////Dibujar Cuadro Fundación
            //if (Form1.Proyecto_.ColumnaSelect != null)
            //{
            //    Columna col = Form1.Proyecto_.ColumnaSelect;
            //    float B_Fund = col.Alzados[col.Alzados.Count - 1].DistX;
            //    double[] Ver_Fund = new double[] {X,Y+Form1.Proyecto_.e_Fundacion,
            //                                   X,Y,
            //                                   X+B_Fund+DPR,Y,
            //                                   X+B_Fund+DPR,Y+Form1.Proyecto_.e_Fundacion };

            //    FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Ver_Fund, LayerCuadro, false);

            //    //Cota espesor Fundación

            //    double[] P1_CotaF = new double[] { X, Y, 0 };
            //    double[] P2_CotaF = new double[] { X, Y + Form1.Proyecto_.e_Fundacion, 0 };
            //    FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaF, P2_CotaF, "FC_COTAS", "FC_TEXT1", -0.3f);

            //    //Dibujar Cada Entre Piso
            //    for (int i = LuzAcum.Count - 1; i >= 0; i--)
            //    {

            //        float B_Draw;
            //        if (ExisteCambioenB)
            //        {
            //            B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[i].Item1.B) / MaxB1) + DPR;
            //        }
            //        else
            //        {
            //            B_Draw = ((Alzados[Alzados.Count - 1].DistX * Seccions[i].Item1.H) / MaxH1) + DPR;
            //        }

            //        double[] Ver_Colum = new double[] {X,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H,
            //                                       X,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H-LuzLibre[i],
            //                                       X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H-LuzLibre[i],
            //                                       X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H };
            //        FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Ver_Colum, LayerCuadro);

            //        float DesCota = -0.3f;
            //        //Cotas entre piso y espesor de Viga

            //        //Cotas entre Piso

            //        double[] P1_CotaT = new double[] { X, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H - LuzLibre[i], 0 };
            //        double[] P2_CotaT = new double[] { X, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H, 0 };
            //        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);

            //        //Cotas Viga
            //        P1_CotaT = new double[] { X, Y + LuzAcum[i] - VigaMayor.Seccions[i].Item1.H, 0 };
            //        P2_CotaT = new double[] { X, Y + LuzAcum[i], 0 };
            //        FunctionsAutoCAD.FunctionsAutoCAD.AddCota(P1_CotaT, P2_CotaT, "FC_COTAS", "FC_TEXT1", DesCota);

            //        if (i != 0)
            //        {
            //            //Lineas entre Piso

            //            double[] VerLosa1 = new double[] {X,Y+LuzAcum[i] -VigaMayor.Seccions[i].Item1.H,
            //                                       X,Y+LuzAcum[i] };

            //            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa1, LayerCuadro, false);

            //            double[] VerLosa2 = new double[] {X+B_Draw,Y+LuzAcum[i],
            //                                       X+B_Draw,Y+LuzAcum[i]-VigaMayor.Seccions[i].Item1.H};

            //            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(VerLosa2, LayerCuadro, false);
            //        }


            //        try
            //        {
            //            if (Seccions[i].Item1.B != Seccions[i - 1].Item1.B)
            //            {
            //                float B_Draw2;
            //                if (ExisteCambioenB)
            //                {
            //                    B_Draw2 = ((Alzados[Alzados.Count - 1].DistX * Seccions[i - 1].Item1.B) / MaxB1) + DPR;
            //                }
            //                else
            //                {
            //                    B_Draw2 = ((Alzados[Alzados.Count - 1].DistX * Seccions[i].Item1.H) / MaxH1) + DPR;
            //                }

            //                double[] LineaFaltante1 = new double[] { X+B_Draw, Y + LuzAcum[i],
            //                                                     X+B_Draw2,Y+ LuzAcum[i]};
            //                FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(LineaFaltante1, LayerCuadro, false);


            //            }


            //        }
            //        catch { }

            //    }
            //}

        }



        private void Ready_CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (Form1.Proyecto_.ColumnaSelect != null)
            {
                Form1.Proyecto_.ColumnaSelect.Ready = Ready_CheckBox.Checked;
                if (Form1.m_PlantaColumnas != null)
                {
                    Form1.m_PlantaColumnas.Invalidate();
                }
            }
        }

        private void MostrarGrid_CheckedChanged(object sender, EventArgs e)
        {
            Draw_Colum_Alzado.Invalidate();
        }
    }
}