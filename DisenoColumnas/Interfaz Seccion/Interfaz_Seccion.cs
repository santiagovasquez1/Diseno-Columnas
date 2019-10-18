﻿using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Interfaz_Seccion
{
    public delegate void Paint_box(object sender, PaintEventArgs e);

    public partial class FInterfaz_Seccion : DockContent
    {
        private string Nombre_Columna { get; set; }
        private double Xmax { get; set; } = 150; //[cm]
        private double Ymax { get; set; } = 75;  //[cm]
        private List<PointF> Vertices { get; set; } = new List<PointF>();

        public FInterfaz_Seccion(string pColumna)
        {
            Nombre_Columna = pColumna;
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            Grafica.Invalidate();
        }

        private void Interfaz_Seccion_Load(object sender, EventArgs e)
        {
            Grafica.Invalidate();
        }

        #region Metodos de picture box

        private void Grafica_Paint(object sender, PaintEventArgs e)
        {
            int X, Y;
            Columna Columna_i = Form1.Proyecto_.ColumnaSelect;
            Seccion seccion_i = Columna_i.Seccions[0].Item1;

            Graphics g = e.Graphics;
            Grafica.CreateGraphics().Clear(Color.White);

            X = Grafica.Width / 2;
            Y = Grafica.Height / 2;

            Crear_grilla(g, Grafica.Height, Grafica.Width);
            g.TranslateTransform(X, Y);
            Crear_ejes(g, Grafica.Height, Grafica.Width);
            Dibujo_Seccion(g, seccion_i, Grafica.Height, Grafica.Width);
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

        private void Crear_ejes(Graphics g, int Height, int Width)
        {
            Point ejex = new Point(0, 0);

            Pen P1 = new Pen(Color.Black, 2)
            {
                Brush = Brushes.LightGray,
                Color = Color.Red,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            };

            Pen P2 = new Pen(Color.Black, 2)
            {
                Brush = Brushes.LightGray,
                Color = Color.Blue,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            };

            g.DrawLine(P1, new Point(0, 0), new Point(Width / 20, 0));
            g.DrawLine(P2, new Point(0, 0), new Point(0, -Height / 20));
        }

        #endregion Metodos de picture box

        private void FInterfaz_Seccion_Paint(object sender, PaintEventArgs e)
        {
            Grafica.Invalidate();
        }

        private void BAcercar_Click(object sender, EventArgs e)
        {
            Cursor pCursor = Cursors.Cross;
            Grafica.Cursor = pCursor;
        }

        private void Mover_Click(object sender, EventArgs e)
        {
            Cursor pCursor = Cursors.Hand;
            Grafica.Cursor = pCursor;
        }

        private void BSeleccion_Click(object sender, EventArgs e)
        {
            Cursor pCursor = Cursors.Arrow;
            Grafica.Cursor = pCursor;
        }

        private void Grafica_MouseMove(object sender, MouseEventArgs e)
        {
            Grafica.MouseMove += Get_coordinates;
        }

        private void Get_coordinates(object sender, MouseEventArgs e)
        {
            float X = e.X - Grafica.Width / 2;
            float Y = e.Y - Grafica.Height / 2;
            double X_r, Y_r;

            X_r = Xmax * X / (Grafica.Width / 2);
            Y_r = Ymax * Y / (Grafica.Height / 2);

            label1.Text = "X:" + Math.Round(X_r, 2) + " Y:" + Math.Round(-Y_r, 2);
            label1.Update();

            Grafica.Invalidate();
        }

        private void BSeleccionar_columna_Click(object sender, EventArgs e)
        {
            Fseleccion_Columnas fseleccion = new Fseleccion_Columnas();
            fseleccion.ShowDialog();
        }

        private void Dibujo_Seccion(Graphics g, Seccion seccioni, int Height, int Width)
        {
            double X, Y;
            //SolidBrush br = new SolidBrush(Color.Gray));
            SolidBrush br = new SolidBrush(Color.FromArgb(150, Color.Gray));

            Pen P1 = new Pen(Color.Black, 2.5f)
            {
                Brush = Brushes.Gray,
                Color = Color.Black,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            };

            Vertices = new List<PointF>();

            if (seccioni.Shape == TipodeSeccion.Rectangular)
            {
                #region Vertices

                X = -((seccioni.B * 100 / 2) * (Width / 2)) / Xmax;
                Y = -((seccioni.H * 100 / 2) * (Height / 2)) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = ((seccioni.B * 100 / 2) * (Width / 2)) / Xmax;
                Y = -((seccioni.H * 100 / 2) * (Height / 2)) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = ((seccioni.B * 100 / 2) * (Width / 2)) / Xmax;
                Y = ((seccioni.H * 100 / 2) * (Height / 2)) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = -((seccioni.B * 100 / 2) * (Width / 2)) / Xmax;
                Y = ((seccioni.H * 100 / 2) * (Height / 2)) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                #endregion Vertices
            }

            if (seccioni.Shape == TipodeSeccion.Tee)
            {
                #region Vertices

                X = -(seccioni.TW * 100 / 2 * (Width / 2)) / Xmax;
                Y = -(seccioni.H * 100 / 2 * (Height / 2)) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = seccioni.TW * 100 / 2 * (Width / 2) / Xmax;
                Y = -(seccioni.H * 100 / 2 * (Height / 2)) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = seccioni.TW * 100 / 2 * (Width / 2) / Xmax;
                Y = ((seccioni.H / 2) - seccioni.TF) * 100 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = seccioni.B * 100 / 2 * (Width / 2) / Xmax;
                Y = ((seccioni.H / 2) - seccioni.TF) * 100 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = seccioni.B * 100 / 2 * (Width / 2) / Xmax;
                Y = seccioni.H * 100 / 2 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = -seccioni.B * 100 / 2 * (Width / 2) / Xmax;
                Y = seccioni.H * 100 / 2 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = -seccioni.B * 100 / 2 * (Width / 2) / Xmax;
                Y = ((seccioni.H / 2) - seccioni.TF) * 100 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = -seccioni.TW * 100 / 2 * (Width / 2) / Xmax;
                Y = ((seccioni.H / 2) - seccioni.TF) * 100 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                #endregion Vertices
            }

            if (seccioni.Shape == TipodeSeccion.L)
            {
                #region Vertices
                X = -(seccioni.B * 100 / 2 * (Width / 2)) / Xmax;
                Y = -(seccioni.H * 100 / 2 * (Height / 2)) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = seccioni.B * 100 / 2 * (Width / 2) / Xmax;
                Y = -(seccioni.H * 100 / 2 * (Height / 2)) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = seccioni.B * 100 / 2 * (Width / 2) / Xmax;
                Y= ((-seccioni.H / 2) + seccioni.TF) * 100 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = ((-seccioni.B / 2) + seccioni.TW) * 100 * (Width / 2) / Xmax;
                Y = ((-seccioni.H / 2) + seccioni.TF) * 100 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = ((-seccioni.B / 2) + seccioni.TW) * 100 * (Width / 2) / Xmax;
                Y = seccioni.H * 100 / 2 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));

                X = -(seccioni.B * 100 / 2 * (Width / 2)) / Xmax;
                Y = seccioni.H * 100 / 2 * (Height / 2) / Ymax;
                Vertices.Add(new PointF((float)X, (float)Y));
                #endregion
            }

            if (seccioni.Shape != TipodeSeccion.Circle)
            {
                g.FillPolygon(br, Vertices.ToArray());
                g.DrawPolygon(P1, Vertices.ToArray());
            }
            else
            {
            }
        }

        private void Grafica_Click(object sender, EventArgs e)
        {
        }
    }
}