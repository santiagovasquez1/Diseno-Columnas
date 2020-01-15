using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.DefinirColumnas
{
    [Serializable]
    public partial class PlantaColumnas : DockContent
    {
        public PlantaColumnas()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            Grafica.Invalidate();

            if (Form1.Proyecto_.ColumnaSelect != null)
            {
                NoPiso = Form1.Proyecto_.Lista_Columnas[0].Seccions.Count - 1;
            }
        }

        private void PlantaColumnas_Load(object sender, EventArgs e)
        {
            Grafica.Invalidate();

        }

        private void Crear_grilla(Graphics g, int Height, int Width)
        {
            int No_CuadrosX = 25;
            int No_CuadrosY = 25;

            float Ancho_Cuadro = Width / No_CuadrosX;
            float Alto_Cuadro = Height / No_CuadrosY;

            Pen P = new Pen(Color.Black, 1)
            {
                Brush = Brushes.LightGray,
                Color = Color.LightGray,
                Alignment = PenAlignment.Center
            };

            for (int i = 1; i < No_CuadrosX; i++)
            {
                g.DrawLine(P, Ancho_Cuadro * i, 0, Ancho_Cuadro * i, Height);
            }

            for (int i = 1; i < No_CuadrosY; i++)
            {
                g.DrawLine(P, 0, Alto_Cuadro * i, Width, Alto_Cuadro * i);
            }
        }

        private void Grafica_Paint(object sender, PaintEventArgs e)
        {
         
            float Height = Grafica.Height - 30;
            float Width = Grafica.Width - 30;

            //Bitmap GraficaTemporal = new Bitmap(Grafica.Width, Grafica.Height);
            Graphics graphics = e.Graphics;
            graphics.Clear(Color.White);



            //graphics.Clear(Color.White);

            //Graficar Cuadicula

            //Graficar Cuadicula

            //Crear_grilla(e.Graphics, Grafica.Height, Grafica.Width);

            List<float> MAXX = new List<float>();
            List<float> MINXX = new List<float>();
            List<float> MAXY = new List<float>();
            List<float> MINXY = new List<float>();

            float MPX1 = (float)Form1.Proyecto_.Lista_Vigas.Max(x => x.CoordXY1[0]);
            MAXX.Add(MPX1);
            float MNX1 = (float)Form1.Proyecto_.Lista_Vigas.Min(x => x.CoordXY1[0]);
            MINXX.Add(MNX1);
            float MPY1 = (float)Form1.Proyecto_.Lista_Vigas.Max(x => x.CoordXY1[1]);
            MAXY.Add(MPY1);
            float MNY1 = (float)Form1.Proyecto_.Lista_Vigas.Min(x => x.CoordXY1[1]);
            MINXY.Add(MNY1);
            float MPX2 = (float)Form1.Proyecto_.Lista_Vigas.Max(x => x.CoordXY2[0]);
            MAXX.Add(MPX2);
            float MNX2 = (float)Form1.Proyecto_.Lista_Vigas.Min(x => x.CoordXY2[0]);
            MINXX.Add(MNX2);
            float MPY2 = (float)Form1.Proyecto_.Lista_Vigas.Max(x => x.CoordXY2[1]);
            MAXY.Add(MPY2);
            float MNY2 = (float)Form1.Proyecto_.Lista_Vigas.Min(x => x.CoordXY2[1]);
            MINXY.Add(MNY2);
            float MPX = (float)Form1.Proyecto_.Lista_Columnas.Max(x => x.CoordXY[0]);
            MAXX.Add(MPX);
            float MNX = (float)Form1.Proyecto_.Lista_Columnas.Min(x => x.CoordXY[0]);
            MINXX.Add(MNX1);
            float MPY = (float)Form1.Proyecto_.Lista_Columnas.Max(x => x.CoordXY[1]);
            MAXY.Add(MPY);
            float MNY = (float)Form1.Proyecto_.Lista_Columnas.Min(x => x.CoordXY[1]);
            MINXY.Add(MNY);

            MPX = MAXX.Max();
            MNX = MINXX.Max();
            MPY = MAXY.Max();
            MNY = MINXY.Max();

            if (MNX > 0) { MNX = 0; }
            if (MPX < 0) { MPX = 0; }

            if (MNY > 0) { MNY = 0; }
            if (MPY < 0) { MPY = 0; }

            float XI = 15;
            float YI = 0;

            float SX = (Width - 15) / (Math.Abs(MPX - MNX));
            float SY = (Height - 15) / (Math.Abs(MPY - MNY));

            string Nomb_PrimerPiso = "";
            float DisAcum = 0;
            if (Form1.Proyecto_.ColumnaSelect != null)
            {
                try
                {
                    Nomb_PrimerPiso = Form1.Proyecto_.ColumnaSelect.Seccions[NoPiso].Item2;
                }
                catch { Nomb_PrimerPiso = Form1.Proyecto_.ColumnaSelect.Seccions[Form1.Proyecto_.ColumnaSelect.Seccions.Count - 1].Item2; }
                try
                {
                    DisAcum = Form1.Proyecto_.ColumnaSelect.LuzAcum[NoPiso];
                }
                catch { DisAcum = Form1.Proyecto_.ColumnaSelect.LuzAcum[Form1.Proyecto_.ColumnaSelect.Seccions.Count - 1]; }
            }

            foreach (Viga viga in Form1.Proyecto_.Lista_Vigas)
            {
                for (int i = viga.Seccions.Count - 1; i >= 0; i--)
                {
                    if (viga.Seccions[i].Item2 == Nomb_PrimerPiso)
                        viga.Paint_(graphics, Height, Width, SX, SY, -MNX, -MNY, XI, YI);
                }
            }
            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                for (int i = columna.Seccions.Count - 1; i >= 0; i--)
                {
                    if (columna.Seccions[i].Item2 == Nomb_PrimerPiso)
                    {
                        columna.Paint_(graphics, Height, Width, SX, SY, -MNX, -MNY, XI, YI, columna.Seccions[i].Item1, CheckedLabels, TamanoText);
                    }
                }

            }


            Text = "Planta de Columnas - " + Nomb_PrimerPiso + "- Elevación: " + Math.Round(DisAcum, 2);


            Text = "Planta de Columnas - " + Nomb_PrimerPiso + "- Elevación: " + Math.Round(DisAcum, 2);
        }

        private void PlantaColumnas_Paint(object sender, PaintEventArgs e)
        {
            foreach (Columna columna1 in Form1.Proyecto_.Lista_Columnas)
            {
                if (columna1 != Form1.Proyecto_.ColumnaSelect)
                {
                    columna1.BrushesColor = Brushes.Black;
                    if (Form1.m_Informacion != null)
                    {
                        Form1.m_Informacion.Invalidate();
                    }
                    if (Form1.m_Despiece != null)
                    {
                        Form1.m_Despiece.Invalidate();
                    }
                }
                else
                {
                    columna1.BrushesColor = Brushes.Red;
                }
            }

            Grafica.Invalidate();
        }

        private void Grafica_MouseDown(object sender, MouseEventArgs e)
        {
            Columna ColumaSelectInPlantas;
            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                ColumaSelectInPlantas = columna.MouseDown(e);

                Grafica.Invalidate();

                if (ColumaSelectInPlantas != null)
                {
                    Form1.Proyecto_.ColumnaSelect = ColumaSelectInPlantas;
                    Form1.mLcolumnas.Text = Form1.Proyecto_.ColumnaSelect.Name;
                    NoPiso = Form1.Proyecto_.ColumnaSelect.LuzAcum.Count - 1;
                    break;
                }

            }

            foreach (Columna columna1 in Form1.Proyecto_.Lista_Columnas)
            {
                if (columna1 != Form1.Proyecto_.ColumnaSelect)
                {
                    columna1.BrushesColor = Brushes.Black;
                    Form1.m_Informacion.Invalidate();
                    Form1.m_Despiece.Invalidate();
                    Form1.mCuantiaVolumetrica.Invalidate();
                    Form1.mFuerzasEnElmentos.Invalidate();
                    if (Form1.mIntefazSeccion != null)
                    {
                        Form1.mIntefazSeccion.Get_Columna();
                        Form1.mIntefazSeccion.Load_Pisos();
                        Form1.mIntefazSeccion.Get_section();
                        Form1.mIntefazSeccion.Invalidate();
                    }
                }
            }
        }

        private void Grafica_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                columna.MouseDobleClick(e);

            }
            Grafica.Invalidate();
            Invalidate();
        }

        public int NoPiso = 0;

        private void Up()
        {
            if (Form1.Proyecto_.ColumnaSelect != null)
            {
                if (NoPiso == 0)
                {
                    NoPiso = Form1.Proyecto_.ColumnaSelect.Seccions.Count - 1;
                }
                else if (NoPiso > Form1.Proyecto_.ColumnaSelect.Seccions.Count - 1)
                {
                    NoPiso = Form1.Proyecto_.ColumnaSelect.Seccions.Count - 1;
                }
                else
                {
                    NoPiso -= 1;
                }

                Invalidate();
            }
        }

        private void Down()
        {
            if (Form1.Proyecto_.ColumnaSelect != null)
            {
                if (NoPiso >= Form1.Proyecto_.ColumnaSelect.Seccions.Count - 1)
                {
                    NoPiso = 0;
                }
                else
                {
                    NoPiso += 1;
                }

                Invalidate();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Up();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Down();
        }

        private void AllReadyColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Columna col in Form1.Proyecto_.Lista_Columnas)
            {
                col.Ready = allReadyColumnsToolStripMenuItem.Checked;
            }
            Invalidate();
        }

        private void Grafica_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                Cursor cursor = null;
                if (columna.MouseMove(e, ref cursor))
                {
                    Grafica.Cursor = cursor;
                    break;
                }
                else
                {
                    Grafica.Cursor = Cursors.Default;
                }


            }
        }

        private void PlantaColumnas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void PlantaColumnas_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Up:
                    Up();
                    break;

                case (char)Keys.Down:
                    Down();
                    break;
            }
        }

        private bool CheckedLabels { get; set; }

        private void MostrarLabels_CheckedChanged(object sender, EventArgs e)
        {
            CheckedLabels = mostrarLabels.Checked;
            Invalidate();
        }

        private float TamanoText=0;
        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            float tamT = 0;
            if (Single.TryParse(toolStripComboBox1.Text, out tamT))
                TamanoText = tamT;
                 Grafica.Invalidate();

        }
    }
}
