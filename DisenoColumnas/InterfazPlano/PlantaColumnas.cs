using DisenoColumnas.Clases;
using DisenoColumnas.InterfazViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            //Graficar Cuadicula


            int No_CuadrosX = 25;
            int No_CuadrosY =7;

            float Ancho_Cuadro = Grafica.Width / No_CuadrosX;
            float Alto_Cuadro = Grafica.Height / No_CuadrosY;
            for (int i=1; i < No_CuadrosX; i++)
            {
                 e.Graphics.DrawLine(new Pen(Brushes.LightGray, 1), Ancho_Cuadro*i, 0, Ancho_Cuadro*i, Grafica.Height);
           

            }
            for (int i = 1; i < No_CuadrosY; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.LightGray, 1), 0, Alto_Cuadro*i, Grafica.Width, Alto_Cuadro*i);
        

            }

   
            //Mayor negativo y Positivo
            float MPX =(float)Form1.Proyecto_.Lista_Columnas.Max(x => x.CoordXY[0]);
            float MNX = (float)Form1.Proyecto_.Lista_Columnas.Min(x => x.CoordXY[0]);


            float MPY = (float)Form1.Proyecto_.Lista_Columnas.Max(x => x.CoordXY[1]);
            float MNY = (float)Form1.Proyecto_.Lista_Columnas.Min(x => x.CoordXY[1]);

            if (MNX > 0) { MNX = 0;}
            if (MPX < 0) {  MPX = 0; }

            if (MNY > 0) { MNY = 0; }
            if (MPY < 0) { MPY = 0; }

            float XI = 15;
            float YI = 0;

            float SX = (Width-15)/ (Math.Abs( MPX-MNX));
            float SY = (Height-15) / (Math.Abs(MPY-MNY));

            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                columna.Paint_(e, Height, Width, SX, SY, -MNX, -MNY,XI,YI);

            }
    


        }

        private void PlantaColumnas_Paint(object sender, PaintEventArgs e)
        {
         
            foreach (Columna columna1 in Form1.Proyecto_.Lista_Columnas)
            {

                if (columna1 != Form1.Proyecto_.ColumnaSelect)
                {
                    columna1.BrushesColor = Brushes.Black;
                    Form1.m_Informacion.Invalidate();
                    Form1.m_Despiece.Invalidate();
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
            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                Form1.Proyecto_.ColumnaSelect = columna.MouseDown(e);
                Grafica.Invalidate();
             


                if (Form1.Proyecto_.ColumnaSelect != null) { Form1.mLcolumnas.Text= Form1.Proyecto_.ColumnaSelect.Name;  break; }
               
            }

            foreach (Columna columna1 in Form1.Proyecto_.Lista_Columnas)
            {

                if (columna1 != Form1.Proyecto_.ColumnaSelect)
                {
                    columna1.BrushesColor = Brushes.Black;
                    Form1.m_Informacion.Invalidate();
                    Form1.m_Despiece.Invalidate();
                }

            }


        }
    }
}
