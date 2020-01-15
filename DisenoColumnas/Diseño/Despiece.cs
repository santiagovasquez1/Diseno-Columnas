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

                bool ExisteCambioenB = false;
                for (int i = ColumnaSelect.Seccions.Count - 1; i >= 0; i--)
                {
                    if (ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        //try { if (ColumnaSelect.Seccions[i].Item1.Shape != ColumnaSelect.Seccions[i - 1].Item1.Shape) { break; } } catch { }
                        try { if (ColumnaSelect.Seccions[i].Item1.B - ColumnaSelect.Seccions[i - 1].Item1.B != 0) { ExisteCambioenB = true; break; } }
                        catch { }
                    }
                }
                if (ExisteCambioenB == false)
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
                        TamanoFuente = ColumnaSelect.LuzLibre[ColumnaSelect.LuzLibre.Count - 1] * SY - ColumnaSelect.VigaMayor.Seccions[ColumnaSelect.VigaMayor.Seccions.Count - 1].Item1.H * SY;
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

                if (mostrarCajonToolStripMenuItem.Checked)
                {
                    MostrarCajon(XI, YI, e, SX, SY, Height);
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

        private void MostrarCajon(float X, float Y, PaintEventArgs e, float SX, float SY, float HeighForm)
        {
            float DPR = 0.6f;

            if (Form1.Proyecto_.ColumnaSelect != null & Form1.Proyecto_.ColumnaSelect.Alzados.Count != 0)
            {
                Columna col = Form1.Proyecto_.ColumnaSelect;

                float MaxB1 = -999999;
                float MaxH1 = -999999;

                MaxB1 = col.Seccions.FindAll(y => y.Item1 != null).ToList().Max(x => x.Item1.B);

                MaxH1 = col.Seccions.FindAll(y => y.Item1 != null).ToList().Max(x => x.Item1.H);

                bool ExisteCambioenB = false;

                for (int i = col.Seccions.Count - 1; i >= 0; i--)
                {
                    if (col.Seccions[i].Item1 != null)
                    {
                        
                        try { if (col.Seccions[i].Item1.B - col.Seccions[i - 1].Item1.B != 0) { ExisteCambioenB = true; break; } }
                        catch { }
                    }
                }

                float B_Fund = col.Alzados[col.Alzados.Count - 1].DistX;
                PointF[] Ver_Fund = new PointF[] {new PointF(X,HeighForm-Y-Form1.Proyecto_.e_Fundacion*SY),
                                               new PointF(X,HeighForm-Y),
                                               new PointF(X+(B_Fund+DPR)*SX,HeighForm-Y),
                                               new PointF(X+(B_Fund+DPR)*SX,HeighForm-Y-Form1.Proyecto_.e_Fundacion*SY) };

                Pen pen = new Pen(Brushes.LightGray, 2);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLines(pen, Ver_Fund);

                //Dibujar Cada Entre Piso
                for (int i = col.LuzAcum.Count - 1; i >= 0; i--)
                {
                    float B_Draw;
                    if (ExisteCambioenB)
                    {
                        B_Draw = ((col.Alzados[col.Alzados.Count - 1].DistX * col.Seccions[i].Item1.B) / MaxB1) + DPR;
                    }
                    else
                    {
                        B_Draw = ((col.Alzados[col.Alzados.Count - 1].DistX * col.Seccions[i].Item1.H) / MaxH1) + DPR;
                    }

                    PointF[] Ver_Colum = new PointF[] {new PointF( X,HeighForm-Y-col.LuzAcum[i]*SY+col.VigaMayor.Seccions[i].Item1.H*SY),
                                                   new PointF(X,HeighForm-Y-col.LuzAcum[i]*SY+col.VigaMayor.Seccions[i].Item1.H*SY+col.LuzLibre[i]*SY),
                                                   new PointF( X+B_Draw*SX,HeighForm-Y-col.LuzAcum[i]*SY+col.VigaMayor.Seccions[i].Item1.H*SY+col.LuzLibre[i]*SY),
                                                  new PointF(X+B_Draw*SX,HeighForm-Y-col.LuzAcum[i]*SY+col.VigaMayor.Seccions[i].Item1.H*SY)};

                    e.Graphics.DrawPolygon(pen, Ver_Colum);

                    if (i != 0)
                    {
                        //Lineas entre Piso

                        e.Graphics.DrawLine(pen, X, HeighForm - Y - (col.LuzAcum[i] - col.VigaMayor.Seccions[i].Item1.H) * SY, X, HeighForm - Y - col.LuzAcum[i] * SY);

                        e.Graphics.DrawLine(pen, X + B_Draw * SX, HeighForm - Y - (col.LuzAcum[i] - col.VigaMayor.Seccions[i].Item1.H) * SY, X + B_Draw * SX, HeighForm - Y - col.LuzAcum[i] * SY);
                    }

                    try
                    {
                        if (ExisteCambioenB)
                        {
                            if (col.Seccions[i].Item1.B != col.Seccions[i - 1].Item1.B)
                            {
                                float B_Draw2 = ((col.Alzados[col.Alzados.Count - 1].DistX * col.Seccions[i - 1].Item1.B) / MaxB1) + DPR;

                                e.Graphics.DrawLine(pen, X + B_Draw * SX, HeighForm - Y - col.LuzAcum[i] * SY, X + B_Draw2 * SX, HeighForm - Y - col.LuzAcum[i] * SY);
                            }
                        }
                        else
                        {
                            if (col.Seccions[i].Item1.H != col.Seccions[i - 1].Item1.H)
                            {
                                float B_Draw2 = ((col.Alzados[col.Alzados.Count - 1].DistX * col.Seccions[i - 1].Item1.H) / MaxH1) + DPR;

                                e.Graphics.DrawLine(pen, X + B_Draw * SX, HeighForm - Y - col.LuzAcum[i] * SY, X + B_Draw2 * SX, HeighForm - Y - col.LuzAcum[i] * SY);
                            }
                        }
                    }
                    catch { }
                }

                float B_DrawF;

                if (ExisteCambioenB)
                {
                    B_DrawF = ((col.Alzados[col.Alzados.Count - 1].DistX * col.Seccions[0].Item1.B) / MaxB1) + DPR;
                }
                else
                {
                    B_DrawF = ((col.Alzados[col.Alzados.Count - 1].DistX * col.Seccions[0].Item1.H) / MaxH1) + DPR;
                }

                PointF[] VerLosa_Final = new PointF[] {new PointF( X,HeighForm-Y-col.LuzAcum[0]*SY +col.VigaMayor.Seccions[0].Item1.H*SY),
                                                   new PointF(X,HeighForm-Y-col.LuzAcum[0]*SY),
                                                   new PointF(X+B_DrawF*SX,HeighForm-Y-col.LuzAcum[0]*SY),
                                                   new PointF(X+B_DrawF*SX,HeighForm-Y-col.LuzAcum[0]*SY+col.VigaMayor.Seccions[0].Item1.H *SY)};

                e.Graphics.DrawPolygon(pen, VerLosa_Final);
            }
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

        private void MostrarCajonToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Draw_Colum_Alzado.Invalidate();
        }


    }
}
