using DisenoColumnas.Clases;
using System;
using System.Drawing;
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
        }

        private void PlantaColumnas_Load(object sender, EventArgs e)
        {
            Grafica.Invalidate();
        }

        private void Grafica_Paint(object sender, PaintEventArgs e)
        {
            Grafica.CreateGraphics().Clear(Color.White);
            float Height = Grafica.Height - 30;
            float Width = Grafica.Width - 30;
            int No_CuadrosX, No_CuadrosY;
            float Ancho_Cuadro, Alto_Cuadro;

          
            //Graficar Cuadicula

       
            float AnchoCuadroD = 20;
            float AltoCuadroD = 20;
            No_CuadrosX = (int)(Grafica.Width / AnchoCuadroD)+1;
            No_CuadrosY= (int)(Grafica.Width / AltoCuadroD);

            Ancho_Cuadro = AnchoCuadroD;
            Alto_Cuadro = AltoCuadroD;

            // No_CuadrosX = 



            for (int i = 1; i < No_CuadrosX; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.LightGray, 1), Ancho_Cuadro * i, 0, Ancho_Cuadro * i, Grafica.Height);
            }
            for (int i = 1; i < No_CuadrosY; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.LightGray, 1), 0, Alto_Cuadro * i, Grafica.Width, Alto_Cuadro * i);
            }

            //Mayor negativo y Positivo
            float MPX = (float)Form1.Proyecto_.Lista_Columnas.Max(x => x.CoordXY[0]);
            float MNX = (float)Form1.Proyecto_.Lista_Columnas.Min(x => x.CoordXY[0]);

            float MPY = (float)Form1.Proyecto_.Lista_Columnas.Max(x => x.CoordXY[1]);
            float MNY = (float)Form1.Proyecto_.Lista_Columnas.Min(x => x.CoordXY[1]);

            if (MNX > 0) { MNX = 0; }
            if (MPX < 0) { MPX = 0; }

            if (MNY > 0) { MNY = 0; }
            if (MPY < 0) { MPY = 0; }

            float XI = 15;
            float YI = 0;

            float SX = (Width - 15) / (Math.Abs(MPX - MNX));
            float SY = (Height - 15) / (Math.Abs(MPY - MNY));

            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                columna.Paint_(e, Height, Width, SX, SY, -MNX, -MNY, XI, YI);
            }
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
    }
}