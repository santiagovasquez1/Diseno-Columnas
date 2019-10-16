using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class Interfaz_Seccion : DockContent
    {
        public Interfaz_Seccion()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            Grafica.Invalidate();
        }

        private void Interfaz_Seccion_Load(object sender, EventArgs e)
        {
            Grafica.Invalidate();
        }

        private void Grafica_Paint(object sender, PaintEventArgs e)
        {
            int X, Y;

            Graphics g = e.Graphics;
            Grafica.CreateGraphics().Clear(Color.White);

            X = Grafica.Width / 2;
            Y = Grafica.Height / 2;

            Crear_ejes(g, Grafica.Height, Grafica.Width);
            g.TranslateTransform(X, Y);
        }

        private void Crear_ejes(Graphics g, int Height, int Width)
        {
            int No_CuadrosX = 25;
            int No_CuadrosY = 25;

            float Ancho_Cuadro = Width / No_CuadrosX;
            float Alto_Cuadro = Height / No_CuadrosY;

            Pen P = new Pen(Color.Black, 1)
            {
                Brush = Brushes.LightGray,
                Color = Color.LightGray,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            };
            SolidBrush br = new SolidBrush(Color.LightGray);

            for (int i = 1; i < No_CuadrosX; i++)
            {
                g.DrawLine(P, Ancho_Cuadro * i, 0, Ancho_Cuadro * i, Height);
            }

            for (int i = 1; i < No_CuadrosY; i++)
            {
                g.DrawLine(P, 0, Alto_Cuadro * i, Width, Alto_Cuadro * i);
            }
        }
    }
}