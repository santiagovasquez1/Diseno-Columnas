using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Interfaz_Seccion
{
    public delegate void Paint_box(object sender, PaintEventArgs e);

    public partial class FInterfaz_Seccion : DockContent
    {
        private string Nombre_Columna { get; set; }
        public bool Over { get; set; } = false;
        public bool Seleccionado { get; set; } = false;
        private static double Xmax { get; set; } = 150; //[cm]
        private static double Ymax { get; set; } = 75;  //[cm]
        private List<PointF> Vertices { get; set; } = new List<PointF>();
        private static FAgregarRef Fseleccion_Columnas { get; set; }
        private static Seccion seccion { get; set; }
        private static Columna Columna_i { get; set; }
        private static int Width { get; set; }
        private static int Height { get; set; }
        public double EscalaX { get; set; }
        public double EscalaY { get; set; }
        public double EscalaR { get; set; }
        public static string Piso { get; set; }

        public FInterfaz_Seccion()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            Grafica.Invalidate();
        }

        private void Interfaz_Seccion_Load(object sender, EventArgs e)
        {
            Load_Pisos();
            Grafica.Invalidate();
        }

        #region Metodos de picture box

        private void Grafica_Paint(object sender, PaintEventArgs e)
        {
            int X, Y;

            EscalaX = Grafica.Width / (2 * Xmax);
            EscalaY = Grafica.Height / (2 * Ymax);
            EscalaR = 0.30 * EscalaX * EscalaY;

            if (Columna_i != null)
            {
                Graphics g = e.Graphics;
                Grafica.CreateGraphics().Clear(Color.White);

                X = Grafica.Width / 2;
                Y = Grafica.Height / 2;

                Crear_grilla(g, Grafica.Height, Grafica.Width);
                g.TranslateTransform(X, Y);
                Crear_ejes(g, Grafica.Height, Grafica.Width);
                Dibujo_Seccion(g, seccion, Grafica.Height, Grafica.Width, Over);
                seccion.Add_Ref_graph(EscalaX, EscalaY, EscalaR);
                Dibujo_Refuerzo(g, seccion);
            }
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

        public void Get_Columna()
        {
            Columna_i = Form1.Proyecto_.ColumnaSelect;
        }

        public void Load_Pisos()
        {
            var pisos = Columna_i.Seccions.Select(x => x.Item2).ToArray();
            lbPisos.Items.Clear();
            lbPisos.Items.AddRange(pisos);
            lbPisos.SelectedItem = lbPisos.Items[lbPisos.Items.Count - 1];
            Piso = lbPisos.SelectedItem.ToString();
        }

        public void get_section()
        {
            int indice;
            indice = Columna_i.Seccions.FindIndex(x => x.Item2 == Piso);
            seccion = Seccion.DeepClone(Columna_i.Seccions[indice].Item1);
            Grafica.Invalidate();
        }

        private bool MouseOverPoligono(PointF mouse_pt)
        {
            GraphicsPath path = new GraphicsPath();
            PointF Temp;
            float X_r, Y_r;

            X_r = mouse_pt.X - Grafica.Width / 2;
            Y_r = mouse_pt.Y - Grafica.Height / 2;

            Temp = new PointF(X_r, Y_r);
            path.AddPolygon(Vertices.ToArray());

            if (path.IsVisible(Temp))
            {
                return true;
            }

            return false;
        }

        private bool MouseOverPoint(PointF mouse_pt)
        {
            PointF Temp;
            float X_r, Y_r;

            X_r = mouse_pt.X - Grafica.Width / 2;
            Y_r = mouse_pt.Y - Grafica.Height / 2;
            Temp = new PointF(X_r, Y_r);

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(Vertices.ToArray());

            var pr = path.PathPoints;

            foreach (PointF pi in path.PathPoints)
            {
                if (Temp.X == pi.X & Temp.Y == pi.Y)
                {
                    return true;
                    break;
                }
            }

            return false;
        }

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
            Cursor new_cursor = Cursors.Default;
            Grafica.MouseMove += Get_coordinates;

            if (MouseOverPoligono(e.Location))
            {
                new_cursor = Cursors.Hand;
            }

            if (Grafica.Cursor != new_cursor)
            {
                Grafica.Cursor = new_cursor;
            }
        }

        private void Seleccionar(object sender, MouseEventArgs e)
        {
            Over = false;
            Seleccionado = false;
            FAgregarRef agregarRef = new FAgregarRef(seccion,Piso,this);

            if (MouseOverPoligono(e.Location))
            {
                Over = true;
                Seleccionado = true;
                Grafica.Invalidate();
                agregarRef.ShowDialog();
            }
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
            FAgregarRef fseleccion = new FAgregarRef(seccion,Piso,this);
            fseleccion.ShowDialog();
        }

        private void Dibujo_Seccion(Graphics g, Seccion seccioni, int Height, int Width, bool seleccion)
        {
            double X, Y;

            SolidBrush br = new SolidBrush(Color.FromArgb(150, Color.Gray));
            Pen P1;

            if (Over == false)
            {
                P1 = new Pen(Color.Black, 2.5f)
                {
                    Brush = Brushes.Gray,
                    Color = Color.Black,
                    DashStyle = DashStyle.Solid,
                    LineJoin = LineJoin.MiterClipped,
                    Alignment = System.Drawing.Drawing2D.PenAlignment.Center
                };
            }
            else
            {
                P1 = new Pen(Color.Black, 3f)
                {
                    Brush = Brushes.DarkRed,
                    Color = Color.DarkRed,
                    DashStyle = DashStyle.Dash,
                    LineJoin = LineJoin.Round,
                    Alignment = PenAlignment.Center
                };
            }

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
                Y = ((-seccioni.H / 2) + seccioni.TF) * 100 * (Height / 2) / Ymax;
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

                #endregion Vertices
            }

            if (seccioni.Shape == TipodeSeccion.Circle)
            {
                CCirculo circulo = new CCirculo(seccioni.B / 2, pCentro: new double[] { 0, 0 });
                circulo.Set_puntos(50);

                g.FillClosedCurve(br, circulo.Puntos.ToArray());
                g.DrawClosedCurve(P1, circulo.Puntos.ToArray());
            }

            if (seccioni.Shape != TipodeSeccion.Circle)
            {
                g.DrawPolygon(P1, Vertices.ToArray());
                g.FillPolygon(br, Vertices.ToArray());
            }
        }

        private void Dibujo_Refuerzo(Graphics g, Seccion seccioni)
        {
            SolidBrush br = new SolidBrush(Color.Black);
            Pen P1;

            P1 = new Pen(Color.Black, 2.5f)
            {
                Brush = Brushes.Black,
                Color = Color.Black,
                DashStyle = DashStyle.Solid,
                LineJoin = LineJoin.MiterClipped,
                Alignment = PenAlignment.Center
            };

            for (int i = 0; i < seccioni.Shapes_ref.Count; i++)
            {
                g.DrawPath(P1, seccioni.Shapes_ref[i]);
                g.FillPath(br, seccioni.Shapes_ref[i]);
            }
        }

        private void Grafica_MouseDown(object sender, MouseEventArgs e)
        {
            Seleccionar(sender, e);
        }

        private void lbPisos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Piso = lbPisos.SelectedItem.ToString();
            get_section();
            Grafica.Invalidate();
        }
    }
}