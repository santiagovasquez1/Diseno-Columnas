using B_Operaciones_Matricialesl;
using DisenoColumnas.Clases;
using DisenoColumnas.Secciones;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Interfaz_Seccion
{
    public enum Tipo_Edicion
    {
        Secciones_predef,
        Secciones_modelo
    }

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
        private static ISeccion seccion { get; set; }
        private static Columna Columna_i { get; set; }
        private static int Width { get; set; }
        private static int Height { get; set; }
        public double EscalaX { get; set; }
        public double EscalaY { get; set; }
        public double EscalaR { get; set; }
        public static string Piso { get; set; }
        public Tipo_Edicion edicion { get; set; }

        public FInterfaz_Seccion(Tipo_Edicion pedicion)
        {
            edicion = pedicion;
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            Grafica.Invalidate();
        }

        private void Interfaz_Seccion_Load(object sender, EventArgs e)
        {
            Paint_Formulario();
            Grafica.Invalidate();
        }

        private void Paint_Formulario()
        {
            if (edicion == Tipo_Edicion.Secciones_modelo)
            {
                Load_Pisos();

                groupBox1.Text = "Lista de pisos";
                gbSecciones.Visible = false;
                gbSecciones.Enabled = false;

                groupBox1.Size = new Size(new Point(166, 489));
                groupBox1.Location = new Point(735, 9);
            }
            if (edicion == Tipo_Edicion.Secciones_predef)
            {
                Load_predef();

                groupBox1.Text = "Secciones predefinidas";
                gbSecciones.Visible = true;
                gbSecciones.Enabled = true;
                gbSecciones.Size = new Size(new Point(166, 47));

                groupBox1.Size = new Size(new Point(166, 438));
                groupBox1.Location = new Point(735, 61);
            }
        }

        #region Metodos de picture box

        private void Grafica_Paint(object sender, PaintEventArgs e)
        {
            int X, Y;
            //EscalaX = Grafica.Width / (2 * Xmax);
            //EscalaY = Grafica.Height / (2 * Ymax);

            if (Grafica.Width / (2 * Xmax) < Grafica.Height / (2 * Ymax))
            {
                EscalaX = Grafica.Width / (2 * Xmax);
                EscalaY = EscalaX;
            }
            else
            {
                EscalaX = Grafica.Height / (2 * Ymax);
                EscalaY = EscalaX;
            }

            EscalaR = EscalaX;

            if (seccion != null)
            {
                Graphics g = e.Graphics;
                Grafica.CreateGraphics().Clear(Color.White);

                X = Grafica.Width / 2;
                Y = Grafica.Height / 2;

                Crear_grilla(g, Grafica.Height, Grafica.Width);
                g.TranslateTransform(X, Y);
                Crear_ejes(g, Grafica.Height, Grafica.Width);
                seccion.Dibujo_Seccion(g, EscalaX, EscalaY, Over);

                if (seccion.Estribo == null)
                {
                    seccion.Calc_vol_inex(Form1.Proyecto_.R / 100, Form1.Proyecto_.FY, Form1.Proyecto_.DMO_DES);
                }

                Dibujo_Estribo(g, seccion);

                seccion.Add_Ref_graph(EscalaX, EscalaY, EscalaR);
                seccion.CalcNoDBarras();
                Dibujo_Refuerzo(g, seccion);

                Add_Texto_Seccion(g, seccion);
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
                Alignment = PenAlignment.Center
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
            if (Columna_i != null)
            {
                var pisos = Columna_i.Seccions.Select(x => x.Item2).ToArray();
                lbPisos.Items.Clear();
                lbPisos.Items.AddRange(pisos);
                lbPisos.SelectedItem = lbPisos.Items[lbPisos.Items.Count - 1];
                Piso = lbPisos.SelectedItem.ToString();
            }
        }

        private void Load_predef()
        {
            ISeccion[] Secciones = { };
            string[] Fc_secciones = { };
            cbSecciones.Items.Clear();

            if (Form1.Proyecto_.DMO_DES == GDE.DMO)
            {
                Secciones = Form1.secciones_predef.Secciones_DMO.ToArray();
            }
            else
            {
                Secciones = Form1.secciones_predef.Secciones_DES.ToArray();
            }

            Fc_secciones = Secciones.Select(x => x.Material.Name).Distinct().ToArray();
            cbSecciones.Items.Clear();
            cbSecciones.Items.AddRange(Fc_secciones);
            cbSecciones.Text = cbSecciones.Items[0].ToString();
        }

        public void Get_section()
        {
            int indice;
            if (Columna_i != null)
            {
                indice = Columna_i.Seccions.FindIndex(x => x.Item2 == Piso);

                List<ISeccion> Temp = new List<ISeccion>();

                if (Form1.Proyecto_.DMO_DES == GDE.DMO)
                {
                    Temp = Form1.secciones_predef.Secciones_DMO;
                }
                else
                {
                    Temp = Form1.secciones_predef.Secciones_DES;
                }

                if (Temp.Exists(x => x.Equals(Columna_i.Seccions[indice].Item1)) == true & Columna_i.Seccions[indice].Item1.Editado == false)
                {
                    int m = 0;

                    seccion = FunctionsProject.DeepClone(Temp.Find(x => x.Equals(Columna_i.Seccions[indice].Item1)));
                    seccion.Name = Columna_i.Seccions[indice].Item1.Name;
                    seccion.Material = Columna_i.Seccions[indice].Item1.Material;
                    seccion.B = Columna_i.Seccions[indice].Item1.B;
                    seccion.H = Columna_i.Seccions[indice].Item1.H;
                    //seccion.Refuerzos= Columna_i.Seccions[indice].Item1.Refuerzos;
                    seccion.CoordenadasSeccion = Columna_i.Seccions[indice].Item1.CoordenadasSeccion;

                    if (seccion.Refuerzos.Count > 0 & seccion.B > seccion.H & seccion.Shape == TipodeSeccion.Rectangular)
                    {
                        double[] Rotacion;
                        foreach (CRefuerzo refuerzo in seccion.Refuerzos)
                        {
                            Rotacion = Operaciones.Rotacion(refuerzo.Coord[0], refuerzo.Coord[1], Math.PI / 2).ToArray();
                            refuerzo.Coord[0] = Rotacion[0];
                            refuerzo.Coord[1] = Rotacion[1];

                            refuerzo.Alzado = Columna_i.Seccions[indice].Item1.Refuerzos[m].Alzado;
                            m++;
                        }
                    }
                }
                else
                {
                    seccion = FunctionsProject.DeepClone(Columna_i.Seccions[indice].Item1);
                }

                Grafica.Invalidate();
            }
        }

        public void Get_Predef_Secction()
        {
            int indice;
            string Seccion_name = "";

            Seccion_name = lbPisos.SelectedItem.ToString();

            if (Form1.Proyecto_.DMO_DES == GDE.DMO)
            {
                indice = Form1.secciones_predef.Secciones_DMO.FindIndex(x => x.ToString() == Seccion_name);
                seccion = FunctionsProject.DeepClone(Form1.secciones_predef.Secciones_DMO[indice]);
            }
            else
            {
                indice = Form1.secciones_predef.Secciones_DES.FindIndex(x => x.ToString() == Seccion_name);
                seccion = FunctionsProject.DeepClone(Form1.secciones_predef.Secciones_DES[indice]);
            }
        }

        private bool MouseOverPoligono(PointF mouse_pt)
        {
            GraphicsPath path = new GraphicsPath();
            PointF Temp;
            float X_r, Y_r;

            X_r = mouse_pt.X - Grafica.Width / 2;
            Y_r = mouse_pt.Y - Grafica.Height / 2;

            Temp = new PointF(X_r, Y_r);

            if (seccion.Seccion_path != null)
            {
                path = seccion.Seccion_path;
            }

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
            Get_coordinates(sender, e);

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
            FAgregarRef agregarRef = null;
            FEditarPredef editarPredef = null;

            if (MouseOverPoligono(e.Location))
            {
                Over = true;
                Seleccionado = true;
                Grafica.Invalidate();

                if (edicion == Tipo_Edicion.Secciones_modelo)
                {
                    agregarRef = new FAgregarRef(seccion, Piso, this);
                    agregarRef.ShowDialog();
                }
                if (edicion == Tipo_Edicion.Secciones_predef)
                {
                    editarPredef = new FEditarPredef(seccion, this, GDE.DMO);
                    editarPredef.ShowDialog();
                }
            }
        }

        private void Get_coordinates(object sender, MouseEventArgs e)
        {
            float X = e.X - Grafica.Width / 2;
            float Y = e.Y - Grafica.Height / 2;
            double X_r, Y_r;

            X_r = X / EscalaX;
            Y_r = Y / EscalaX;

            label1.Text = "X:" + Math.Round(X_r, 2) + " Y:" + Math.Round(-Y_r, 2);
            label1.Update();
        }

        private void BSeleccionar_columna_Click(object sender, EventArgs e)
        {
            if (edicion == Tipo_Edicion.Secciones_modelo)
            {
                FAgregarRef fseleccion = new FAgregarRef(seccion, Piso, this);
                fseleccion.ShowDialog();
            }

            if (edicion == Tipo_Edicion.Secciones_predef)
            {
                FAgregarSeccion fseccion = new FAgregarSeccion();
                fseccion.ShowDialog();
            }
        }

        private void Dibujo_Estribo(Graphics g, ISeccion seccioni)
        {
            GraphicsPath path = new GraphicsPath();
            SolidBrush br = new SolidBrush(Color.FromArgb(100, Color.Black));
            Pen P1;

            P1 = new Pen(Color.Black, 2.5f)
            {
                Brush = Brushes.DarkGray,
                Color = Color.DarkGray,
                DashStyle = DashStyle.Solid,
                LineJoin = LineJoin.Round,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            };

            path = seccioni.Add_Estribos(EscalaX, EscalaY, 0.04f);
            g.DrawPath(P1, path);
            g.FillPath(br, path);
        }

        private void Add_Texto_Seccion(Graphics g, ISeccion seccioni)
        {
            float TamanoFuente = 0;
            string Ref_Seccion = "Refuerzo Sección :";
            SolidBrush br = new SolidBrush(Color.Black);
            PointF PS = new PointF();

            TamanoFuente = Convert.ToSingle(5 * EscalaR);

            if (TamanoFuente > 12.7f)
            {
                TamanoFuente = 12.7f;
            }

            Font Fuente = new Font("Calibri", TamanoFuente, FontStyle.Bold);

            PS.X = (-Grafica.Width / 2) + 30;
            PS.Y = (-Grafica.Height / 2) + 30;

            g.DrawString(seccioni.ToString(), Fuente, br, PS);

            #region Texto refuerzo

            PS.Y += 20;

            for (int i = 0; i < seccioni.No_D_Barra.Count; i++)
            {
                if (i == seccioni.No_D_Barra.Count - 1)
                {
                    Ref_Seccion += $"{seccioni.No_D_Barra[i].Item1}#{seccioni.No_D_Barra[i].Item2}";
                }
                else
                {
                    Ref_Seccion += $"{seccioni.No_D_Barra[i].Item1}#{seccioni.No_D_Barra[i].Item2}+";
                }
            }

            g.DrawString(Ref_Seccion, Fuente, br, PS);

            #endregion Texto refuerzo

            #region Info Estribos

            PS.Y += 20;

            Ref_Seccion = seccioni.Estribo.ToString();
            g.DrawString(Ref_Seccion, Fuente, br, PS);

            PS.Y += 20;

            Ref_Seccion = $"Ramas dir X: {seccioni.Estribo.NoRamasV1 + seccioni.Estribo.NoRamasV2}";
            g.DrawString(Ref_Seccion, Fuente, br, PS);

            PS.Y += 20;
            Ref_Seccion = $"Ramas dir Y: {seccioni.Estribo.NoRamasH1 + seccioni.Estribo.NoRamasH2}";
            g.DrawString(Ref_Seccion, Fuente, br, PS);

            #endregion Info Estribos
        }

        private void Dibujo_Refuerzo(Graphics g, ISeccion seccioni)
        {
            SolidBrush br = new SolidBrush(Color.Black);
            SolidBrush br_T = new SolidBrush(Color.Black);
            int cont = 1;
            int Diametro;
            float TamanoFuente = 0;
            PointF PS = new PointF();

            Pen P1;
            float DeltaX, DeltaY;

            DeltaX = Convert.ToSingle(4 * Xmax / (Grafica.Width / 2));
            DeltaY = Convert.ToSingle(4 * Ymax / (Grafica.Height / 2));
            TamanoFuente = Convert.ToSingle(2.5 * EscalaR);

            if (TamanoFuente < 6)
            {
                TamanoFuente = 6f;
            }

            if (TamanoFuente > 14)
            {
                TamanoFuente = 14f;
            }

            Font Fuente = new Font("Calibri", TamanoFuente, FontStyle.Bold);

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
                Diametro = Convert.ToInt32(seccioni.Refuerzos[i].Diametro.Substring(1));
                br = FunctionsProject.ColorBarra(Diametro);

                PS.X = seccioni.Shapes_ref[i].PathPoints[0].X + DeltaX;
                PS.Y = seccioni.Shapes_ref[i].PathPoints[0].Y + DeltaY;

                if (edicion == Tipo_Edicion.Secciones_modelo)
                {
                    g.DrawString(seccion.Refuerzos[i].Alzado.ToString(), Fuente, br_T, PS);
                }
                else
                {
                    g.DrawString(cont.ToString(), Fuente, br_T, PS);
                }                

                g.DrawPath(P1, seccioni.Shapes_ref[i]);
                g.FillPath(br, seccioni.Shapes_ref[i]);
                cont++;
            }
        }

        private void Grafica_MouseDown(object sender, MouseEventArgs e)
        {
            Seleccionar(sender, e);
        }

        private void lbPisos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Piso = lbPisos.SelectedItem.ToString();
            if (edicion == Tipo_Edicion.Secciones_modelo)
            {
                Get_section();
            }
            else
            {
                Get_Predef_Secction();
            }
            Grafica.Invalidate();
        }

        private void Grafica_Click(object sender, EventArgs e)
        {
        }

        private void Grafica_MouseHover(object sender, EventArgs e)
        {
        }

        private void cbSecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISeccion[] Secciones = { };
            string Fc_secciones = "";

            Fc_secciones = cbSecciones.Text;

            if (Form1.Proyecto_.DMO_DES == GDE.DMO)
            {
                Secciones = Form1.secciones_predef.Secciones_DMO.FindAll(x => x.Material.Name == Fc_secciones).ToArray();
            }
            else
            {
                Secciones = Form1.secciones_predef.Secciones_DES.FindAll(x => x.Material.Name == Fc_secciones).ToArray();
            }

            lbPisos.Items.Clear();
            lbPisos.Items.AddRange(Secciones);
            lbPisos.SelectedItem = lbPisos.Items[0];
        }
    }
}