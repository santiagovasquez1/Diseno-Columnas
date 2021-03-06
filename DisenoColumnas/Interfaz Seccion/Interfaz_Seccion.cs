﻿using B_Operaciones_Matricialesl;
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

    public delegate void Paint_box ( object sender , PaintEventArgs e );

    public partial class FInterfaz_Seccion : DockContent
    {
        private string Nombre_Columna { get; set; }
        public bool Over { get; set; } = false;
        public bool Over_ref { get; set; } = false;
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
        public int Indice_ref { get; set; } = -1;
        public FInfo_Ref Info_ref { get; set; }
        public FEditarRef EditarRef { get; set; }
        public GDE GDE { get; set; }
        public bool Add_Refuerzo { get; set; }
        public bool Seleccion { get; set; } = true;

        public bool Add_Refuerzo_Multiple_Linea { get; set; }
        public bool Add_Refuerzo_Multiple2_Linea { get; set; } = false;

        public bool Add_Refuerzo_Multiple_Rectangulo { get; set; }
        public bool Add_Refuerzo_Multiple2_Rectangulo { get; set; } = false;

        private float DesXMultiple = 0; private float DesYMultiple = 0;
        private float DesplazaXMultiple = 0; private float DesplazaYMultiple = 0;

        public int Indice_Lb { get; set; }
        public int ex { get; set; } = 1;
        public int ey { get; set; } = 1;
        public float Dx { get; set; } = 0;
        public float Dy { get; set; } = 0;
        public float offsetX { get; set; } = 0;
        public float offsetY { get; set; } = 0;
        public List<PointF> MovingPolygon { get; set; } = new List<PointF>();

        public FInterfaz_Seccion ( Tipo_Edicion pedicion )
        {
            edicion = pedicion;
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void Interfaz_Seccion_Load ( object sender , EventArgs e )
        {
            Paint_Formulario();
            Grafica.Invalidate();

            if ( edicion == Tipo_Edicion.Secciones_predef )
            {
                Radio_Dmo.Checked = true;
                groupBox2.Visible = true;
                groupBox2.Enabled = true;
                CreateSection.Visible = true;
                SaveSection.Visible = true;
                lbPisos.ContextMenuStrip = cmSecciones;
            }
            else
            {
                lbPisos.ContextMenuStrip = null;
                CreateSection.Visible = false;
                SaveSection.Visible = false;
            }
        }

        private void Paint_Formulario ()
        {
            if ( edicion == Tipo_Edicion.Secciones_modelo )
            {
                Load_Pisos();

                groupBox1.Text = "Lista de Pisos";
                gbSecciones.Visible = false;
                gbSecciones.Enabled = false;

                groupBox1.Size = new Size(166 , 485);
                groupBox1.Location = new Point(685 , 12);
                //    Button_Diagrama.Visible = true;
            }
            if ( edicion == Tipo_Edicion.Secciones_predef )
            {
                Load_predef();

                groupBox1.Text = "Secciones Predefinidas";
                gbSecciones.Visible = true;
                gbSecciones.Enabled = true;
                gbSecciones.Size = new Size(new Point(166 , 47));

                // Button_Diagrama.Visible = false;

                groupBox1.Size = new Size(new Point(166 , 393));
                groupBox1.Location = new Point(688 , 107);
            }
        }

        #region Metodos de Picture Box

        private void Grafica_Paint ( object sender , PaintEventArgs e )
        {
            toolStrip1.Refresh();
            int X, Y;

            if ( Grafica.Width / ( 2 * Xmax ) < Grafica.Height / ( 2 * Ymax ) )
            {
                EscalaX = Grafica.Width / ( 2 * Xmax ) * ex;
                EscalaY = EscalaX;
            }
            else
            {
                EscalaX = Grafica.Height / ( 2 * Ymax ) * ex;
                EscalaY = EscalaX;
            }

            EscalaR = EscalaX;

            if ( seccion != null )
            {
                Bitmap newImg = new Bitmap(Grafica.Width , Grafica.Height);
                Graphics g = Graphics.FromImage(newImg);
                g.Clear(Color.White);

                //Grafica.CreateGraphics().Clear(Color.White);

                X = Grafica.Width / 2;
                Y = Grafica.Height / 2;

                Crear_grilla(g , Grafica.Height , Grafica.Width);
                g.TranslateTransform(X , Y);
                Crear_ejes(g , Grafica.Height , Grafica.Width);
                seccion.Dibujo_Seccion(g , EscalaX , EscalaY , Over , Dx , Dy);

                if ( seccion.Estribo == null )
                {
                    seccion.Calc_vol_inex(Form1.Proyecto_.R / 100 , Form1.Proyecto_.FY , Form1.Proyecto_.DMO_DES);
                }

                Dibujo_Estribo(g , seccion);

                seccion.Add_Ref_graph(EscalaX , EscalaY , EscalaR , Dx , Dy);
                seccion.CalcNoDBarras();
                Dibujo_Refuerzo(g , seccion);

                Add_Texto_Seccion(g , seccion);

                #region Seccion Vecina

                if ( arrribaToolStripMenuItem.Enabled )
                {
                    if ( arrribaToolStripMenuItem.Checked )
                    {
                        seccion.SeccionesVecinosCambios[0].Item1.Dibujo_SeccionVecina(g , EscalaX , EscalaY , Dx , Dy);
                    }
                }
                if ( abajoToolStripMenuItem.Enabled )
                {
                    if ( abajoToolStripMenuItem.Checked )
                    {
                        seccion.SeccionesVecinosCambios[0].Item1.Dibujo_SeccionVecina(g , EscalaX , EscalaY , Dx , Dy);
                    }
                }
                #endregion Seccion Vecina

                if ( Add_Refuerzo_Multiple_Linea && Add_Refuerzo_Multiple2_Linea )

                {
                    Pen penSombreado = new Pen(Color.Black , 2);
                    penSombreado.DashStyle = DashStyle.Dot;
                    float x = ( DesXMultiple - Grafica.Width / 2 );
                    float y = ( DesYMultiple - Grafica.Height / 2 );
                    float x2 = x + ( DesplazaXMultiple );
                    float y2 = y + ( DesplazaYMultiple );
                    g.DrawLine(penSombreado , x , y , x2 , y2);
                }

                if ( Add_Refuerzo_Multiple_Rectangulo && Add_Refuerzo_Multiple2_Rectangulo )
                {
                    Pen penSombreado = new Pen(Color.Black , 2);
                    penSombreado.DashStyle = DashStyle.Dot;
                    float x = ( DesXMultiple - Grafica.Width / 2 );
                    float y = ( DesYMultiple - Grafica.Height / 2 );
                    float x2 = x + ( DesplazaXMultiple );
                    float y2 = y + ( DesplazaYMultiple );

                    GraphicsPath path = new GraphicsPath();

                    PointF[] PuntosRectangulo = new PointF[] { new PointF(x , y) , new PointF(x , y2) , new PointF(x2 , y2) , new PointF(x2 , y) , new PointF(x , y) };

                    path.AddLines(PuntosRectangulo);
                    g.DrawPath(penSombreado , path);
                }

                Grafica.Image = newImg;
            }
        }

        private void Crear_grilla ( Graphics g , int Height , int Width )
        {
            int No_CuadrosX = 25;
            int No_CuadrosY = 25;

            float Ancho_Cuadro = Width / No_CuadrosX;
            float Alto_Cuadro = Height / No_CuadrosY;

            Pen P = new Pen(Color.Black , 1)
            {
                Brush = Brushes.LightGray ,
                Color = Color.LightGray ,
                Alignment = PenAlignment.Center
            };

            SolidBrush br = new SolidBrush(Color.LightGray);

            for ( int i = 1 ; i < No_CuadrosX ; i++ )
            {
                g.DrawLine(P , Ancho_Cuadro * i , 0 , Ancho_Cuadro * i , Height);
            }

            for ( int i = 1 ; i < No_CuadrosY ; i++ )
            {
                g.DrawLine(P , 0 , Alto_Cuadro * i , Width , Alto_Cuadro * i);
            }
        }

        private void Crear_ejes ( Graphics g , int Height , int Width )
        {
            PointF ejex = new PointF(0 + Dx , 0 + Dy);

            Pen P1 = new Pen(Color.Black , 2)
            {
                Brush = Brushes.LightGray ,
                Color = Color.Red ,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            };

            Pen P2 = new Pen(Color.Black , 2)
            {
                Brush = Brushes.LightGray ,
                Color = Color.Blue ,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            };

            g.DrawLine(P1 , new PointF(0 + Dx , 0 + Dy) , new PointF(Dx + Width / 20 , Dy + 0));
            g.DrawLine(P2 , new PointF(Dx + 0 , Dy + 0) , new PointF(Dx + 0 , Dy - Height / 20));
        }

        #endregion Metodos de Picture Box

        public void Get_Columna ()
        {
            Columna_i = Form1.Proyecto_.ColumnaSelect;
        }

        public void Load_Pisos ()
        {
            if ( Columna_i != null )
            {
                var pisos = Columna_i.Seccions.Select(x => x.Item2).ToArray();
                lbPisos.Items.Clear();
                lbPisos.Items.AddRange(pisos);
                lbPisos.SelectedItem = lbPisos.Items[lbPisos.Items.Count - 1];
                Piso = lbPisos.SelectedItem.ToString();
            }
        }

        private void Load_predef ()
        {
            ISeccion[] Secciones = { };
            string[] Fc_secciones = { };
            cbSecciones.Items.Clear();

            if ( GDE == GDE.DMO )
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

        public void Get_section ( bool EditCuanVol = false )
        {
            int indice;
            if ( Columna_i != null )
            {
                indice = Columna_i.Seccions.FindIndex(x => x.Item2 == Piso);

                List<ISeccion> Temp = new List<ISeccion>();

                if ( Form1.Proyecto_.DMO_DES == GDE.DMO )
                {
                    Temp = Form1.secciones_predef.Secciones_DMO;
                }
                else
                {
                    Temp = Form1.secciones_predef.Secciones_DES;
                }

                if ( Temp.Exists(x => x.Equals(Columna_i.Seccions[indice].Item1)) == true & Columna_i.Seccions[indice].Item1.Editado == false && EditCuanVol == false )
                {
                    int m = 0;

                    seccion = FunctionsProject.DeepClone(Temp.Find(x => x.Equals(Columna_i.Seccions[indice].Item1)));
                    seccion.Name = Columna_i.Seccions[indice].Item1.Name;
                    seccion.Material = Columna_i.Seccions[indice].Item1.Material;
                    seccion.B = Columna_i.Seccions[indice].Item1.B;
                    seccion.H = Columna_i.Seccions[indice].Item1.H;
                    seccion.CoordenadasSeccion = Columna_i.Seccions[indice].Item1.CoordenadasSeccion;

                    foreach ( CRefuerzo refuerzo in seccion.Refuerzos )
                    {
                        if ( Columna_i.Seccions[indice].Item1.Refuerzos.Count > 0 )
                        {
                            refuerzo.Alzado = Columna_i.Seccions[indice].Item1.Refuerzos[m].Alzado;
                            refuerzo.Diametro = Columna_i.Seccions[indice].Item1.Refuerzos[m].Diametro;
                            refuerzo.As_Long = Columna_i.Seccions[indice].Item1.Refuerzos[m].As_Long;
                            refuerzo.id = Columna_i.Seccions[indice].Item1.Refuerzos[m].id;
                            refuerzo.TipodeRefuerzo = Columna_i.Seccions[indice].Item1.Refuerzos[m].TipodeRefuerzo;
                        }
                        m++;
                    }

                    if ( seccion.Refuerzos.Count > 0 & seccion.B > seccion.H & seccion.Shape == TipodeSeccion.Rectangular )
                    {
                        double[] Rotacion;
                        foreach ( CRefuerzo refuerzo in seccion.Refuerzos )
                        {
                            Rotacion = Operaciones.Rotacion(refuerzo.Coord[0] , refuerzo.Coord[1] , ( 3 * Math.PI ) / 2).ToArray();
                            refuerzo.Coord[0] = Rotacion[0];
                            refuerzo.Coord[1] = Rotacion[1];
                        }
                    }
                }
                else
                {
                    seccion = FunctionsProject.DeepClone(Columna_i.Seccions[indice].Item1);
                }
                AsignarSeccionesConCambios();

                Grafica.Invalidate();
            }
        }

        public void Get_Predef_Secction ()
        {
            int indice;
            string Seccion_name = "";

            Seccion_name = lbPisos.SelectedItem.ToString();

            if ( GDE == GDE.DMO )
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

        private bool MouseOverPoligono ( PointF mouse_pt )
        {
            GraphicsPath path = new GraphicsPath();
            PointF Temp;
            float X_r, Y_r;

            X_r = mouse_pt.X - Grafica.Width / 2;
            Y_r = mouse_pt.Y - Grafica.Height / 2;

            Temp = new PointF(X_r , Y_r);

            if ( seccion != null )
            {
                if ( seccion.Seccion_path != null )
                {
                    path = seccion.Seccion_path;
                }
            }

            if ( path.IsVisible(Temp) )
            {
                MovingPolygon = path.PathPoints.ToList();
                offsetX = MovingPolygon[0].X - mouse_pt.X;
                offsetY = MovingPolygon[0].Y - mouse_pt.Y;
                return true;
            }

            return false;
        }

        private bool MouseOverRefuerzo ( PointF mouse_pt )
        {
            GraphicsPath path = new GraphicsPath();
            PointF Temp;
            int i = 0;
            float X_r, Y_r;

            X_r = mouse_pt.X - Grafica.Width / 2;
            Y_r = mouse_pt.Y - Grafica.Height / 2;

            Temp = new PointF(X_r , Y_r);

            if ( seccion != null )
            {
                if ( seccion.Shapes_ref != null )
                {
                    foreach ( var refuerzoi in seccion.Shapes_ref )
                    {
                        if ( refuerzoi.IsVisible(Temp) )
                        {
                            Indice_ref = i;
                            return true;
                        }

                        i++;
                    }
                }
            }

            return false;
        }

        private void FInterfaz_Seccion_Paint ( object sender , PaintEventArgs e )
        {
            //Grafica.Invalidate();
        }

        private void BAcercar_Click ( object sender , EventArgs e )
        {
            Cursor pCursor = Cursors.Cross;
            Grafica.Cursor = pCursor;
        }

        private void Mover_Click ( object sender , EventArgs e )
        {
            Cursor pCursor = Cursors.Hand;
            Grafica.Cursor = pCursor;
        }

        private void BSeleccion_Click ( object sender , EventArgs e )
        {
            Cursor pCursor = Cursors.Arrow;
            Grafica.Cursor = pCursor;
        }

        private void Move_Draw ( object sender , MouseEventArgs e )
        {
            float new_x1 = e.X + offsetX;
            float new_y1 = e.Y + offsetY;

            Dx = new_x1 - MovingPolygon[0].X;
            Dy = new_y1 - MovingPolygon[0].Y;
        }

        private void Grafica_MouseMove ( object sender , MouseEventArgs e )
        {
            Cursor new_cursor = Cursors.Default;
            Icon ArrastreICO = Properties.Resources.Arrastre16x16;
            Cursor CursorArrastre = new Cursor(ArrastreICO.Handle);
            Get_coordinates(sender , e);

            if ( MouseOverPoligono(e.Location) )
            {
                if ( Seleccion )
                    new_cursor = Cursors.Hand;
                else
                    new_cursor = Cursors.Cross;
            }

            Over_ref = MouseOverRefuerzo(e.Location);

            if ( Info_ref == null )
            {
                Info_ref = new FInfo_Ref();
            }

            if ( Info_ref.Disposing )
            {
                Info_ref = new FInfo_Ref();
            }

            if ( Over_ref == true & edicion == Tipo_Edicion.Secciones_modelo )
            {
                try
                {
                    Info_ref.Visible = true;
                }
                catch
                {
                    Info_ref = new FInfo_Ref();
                    Info_ref.Visible = true;
                }
            }
            else
            {
                Info_ref.Visible = false;
            }

            if ( Info_ref.Visible )
            {
                if ( seccion.Refuerzos[Indice_ref].Coord[0] > 0 )
                {
                    Info_ref.Location = new Point(Cursor.Position.X + 100 , Cursor.Position.Y);
                }
                else
                {
                    Info_ref.Location = new Point(Cursor.Position.X - 30 - Info_ref.Width , Cursor.Position.Y);
                }

                Info_ref.D_Barra.Text = seccion.Refuerzos[Indice_ref].Diametro;
                Info_ref.ID_Ref.Text = Convert.ToString(seccion.Refuerzos[Indice_ref].id);
                Info_ref.Num_alzado.Text = Convert.ToString(seccion.Refuerzos[Indice_ref].Alzado);
                Info_ref.Xc.Text = $"{Math.Round(seccion.Refuerzos[Indice_ref].Coord[0] , 2) }";
                Info_ref.Yc.Text = $"{Math.Round(seccion.Refuerzos[Indice_ref].Coord[1] , 2) }";
            }

            if ( e.Button == MouseButtons.Middle )
            {
                float DesplazX = ( Dx ) + ( e.X - DesXButton );
                float DesplazY = ( Dy ) + ( e.Y - DesYButton );

                Dx = DesplazX;
                Dy = DesplazY;

                DesXButton = e.X;
                DesYButton = e.Y;
                new_cursor = CursorArrastre;
                Grafica.Invalidate();
            }

            if ( Add_Refuerzo_Multiple_Linea && Add_Refuerzo_Multiple2_Linea || Add_Refuerzo_Multiple_Rectangulo && Add_Refuerzo_Multiple2_Rectangulo )
            {
                DesplazaXMultiple = e.X - DesXMultiple;
                DesplazaYMultiple = e.Y - DesYMultiple;
                Grafica.Invalidate();
            }

            if ( Grafica.Cursor != new_cursor )
            {
                Grafica.Cursor = new_cursor;
            }
        }

        private float DesXButton = 0; private float DesYButton = 0;

        private void Seleccionar ( object sender , MouseEventArgs e )
        {
            Over = false;
            Seleccionado = false;
            FAgregarRef agregarRef = null;
            FEditarPredef editarPredef = null;

            if ( MouseOverPoligono(e.Location) & e.Button == MouseButtons.Left )
            {
                Over = true;
                Seleccionado = true;

                if ( Seleccion )
                {
                    if ( edicion == Tipo_Edicion.Secciones_modelo )
                    {
                        agregarRef = new FAgregarRef(seccion , Piso , this);
                        agregarRef.ShowDialog();
                    }
                    if ( edicion == Tipo_Edicion.Secciones_predef )
                    {
                        editarPredef = new FEditarPredef(seccion , this , GDE.DMO);
                        editarPredef.ShowDialog();
                    }
                }
                else if ( Add_Refuerzo )
                {
                    double[] Coord = { };
                    double x, y;
                    int pid;
                    try
                    {
                        pid = seccion.Refuerzos.Last().id + 1;
                    }
                    catch { pid = 1; }
                    x = ( e.Location.X - Grafica.Width / 2 ) / EscalaX;
                    y = -( e.Location.Y - Grafica.Height / 2 ) / EscalaY;

                    Coord = new double[] { x - ( Dx / EscalaX ) , y + ( Dy / EscalaY ) };
                    CRefuerzo new_refuerzo = new CRefuerzo(pid , "#4" , Coord , TipodeRefuerzo.longitudinal);
                    seccion.Refuerzos.Add(new_refuerzo);
                }
                else if ( Add_Refuerzo_Multiple_Linea && Add_Refuerzo_Multiple2_Linea )
                {
                    float x, y;
                    int pid;
                    x = ( DesXMultiple - Grafica.Width / 2 ) / (float)EscalaX;
                    y = -( DesYMultiple - Grafica.Height / 2 ) / (float)EscalaY;
                    FAgregarRefMultiple fAgregar = new FAgregarRefMultiple();

                    FAgregarRefMultiple.FormuAgregarMultipe.Xi.Text = String.Format("{0:0.00}" , x - ( Dx / EscalaX ));
                    FAgregarRefMultiple.FormuAgregarMultipe.Yi.Text = String.Format("{0:0.00}" , y + ( Dy / EscalaY ));

                    float xf = (float)x + ( DesplazaXMultiple / (float)EscalaX );
                    float yf = (float)y - ( DesplazaYMultiple / (float)EscalaY );

                    FAgregarRefMultiple.FormuAgregarMultipe.Xf.Text = String.Format("{0:0.00}" , xf - ( Dx / EscalaX ));
                    FAgregarRefMultiple.FormuAgregarMultipe.Yf.Text = String.Format("{0:0.00}" , yf + ( Dy / EscalaY ));

                    FAgregarRefMultiple.FormuAgregarMultipe.Dx = Dx; FAgregarRefMultiple.FormuAgregarMultipe.Dy = Dy; FAgregarRefMultiple.FormuAgregarMultipe.EscalaX = EscalaX; FAgregarRefMultiple.FormuAgregarMultipe.EscalaY = EscalaY;

                    fAgregar.ShowDialog();

                    if ( FAgregarRefMultiple.Close1 )
                    {
                        Add_Refuerzo_Multiple_Linea = false; Seleccion = true; DesplazaXMultiple = 0;
                        DesplazaYMultiple = 0; Add_Refuerzo_Multiple_Linea = false; Seleccion = true; Add_Refuerzo_Multiple2_Linea = false; return;
                    }
                    //TOMAR VALORES NUEVALMENTE
                    x = FAgregarRefMultiple.Xii + ( Dx / (float)EscalaX );
                    y = FAgregarRefMultiple.Yii - ( Dy / (float)EscalaY );
                    xf = FAgregarRefMultiple.Xff + ( Dx / (float)EscalaX );
                    yf = FAgregarRefMultiple.Yff - ( Dy / (float)EscalaY );
                    float S = FAgregarRefMultiple.Separacion;
                    string Diametro = FAgregarRefMultiple.Diametro;

                    float CA = Math.Abs(xf - x); float CO = Math.Abs(yf - y);
                    float Angulo = (float)Math.Atan(CO / CA);
                    float DistanciaDiagonal = FunctionsProject.DistanciaEntrePuntos(x , y , xf , yf);
                    int CantBarras = (int)Math.Round(DistanciaDiagonal / S);
                    float DeltaX = (float)( S * Math.Cos(Angulo) );
                    float DeltaY = (float)( S * Math.Sin(Angulo) );
                    if ( x > xf ) { DeltaX = -DeltaX; }
                    if ( y > yf ) { DeltaY = -DeltaY; }

                    for ( int i = 1 ; i < CantBarras ; i++ )
                    {
                        double[] Coord = new double[] { x - ( Dx / EscalaX ) + DeltaX * i , y + ( Dy / EscalaY ) + DeltaY * i };
                        try
                        {
                            pid = seccion.Refuerzos.Last().id + 1;
                        }
                        catch { pid = 1; }
                        CRefuerzo new_refuerzo21 = new CRefuerzo(pid , Diametro , Coord , TipodeRefuerzo.longitudinal);

                        seccion.Refuerzos.Add(new_refuerzo21);
                    }
                    //Barra Inical
                    double[] CoordInical = new double[] { x - ( Dx / EscalaX ) , y + ( Dy / EscalaY ) };
                    pid = seccion.Refuerzos.Last().id + 1;
                    CRefuerzo new_refuerzo = new CRefuerzo(pid , Diametro , CoordInical , TipodeRefuerzo.longitudinal);
                    seccion.Refuerzos.Add(new_refuerzo);

                    //Barra Fianl
                    double[] CoordFianal = new double[] { xf - ( Dx / EscalaX ) , yf + ( Dy / EscalaY ) };
                    pid = seccion.Refuerzos.Last().id + 1;
                    CRefuerzo new_refuerzo2 = new CRefuerzo(pid , Diametro , CoordFianal , TipodeRefuerzo.longitudinal);
                    seccion.Refuerzos.Add(new_refuerzo2);

                    DesplazaXMultiple = 0;
                    DesplazaYMultiple = 0;
                    Add_Refuerzo_Multiple_Linea = false;
                    Seleccion = true;
                }
                else if ( Add_Refuerzo_Multiple_Rectangulo && Add_Refuerzo_Multiple2_Rectangulo )
                {
                    float x, y; int pid;
                    x = ( DesXMultiple - Grafica.Width / 2 ) / (float)EscalaX;
                    y = -( DesYMultiple - Grafica.Height / 2 ) / (float)EscalaY;
                    FAgregarRefMultipleCuadro fAgregar = new FAgregarRefMultipleCuadro();

                    FAgregarRefMultipleCuadro.RefuerzoCuadro.XInicial = (float)Math.Round(x - ( Dx / EscalaX ) , 2);
                    FAgregarRefMultipleCuadro.RefuerzoCuadro.YInicial = (float)Math.Round(y + ( Dy / EscalaY ) , 2);

                    float xf = x + ( DesplazaXMultiple / (float)EscalaX );
                    float yf = y - ( DesplazaYMultiple / (float)EscalaY );

                    FAgregarRefMultipleCuadro.RefuerzoCuadro.XFinal = (float)Math.Round(xf - ( Dx / EscalaX ) , 2);
                    FAgregarRefMultipleCuadro.RefuerzoCuadro.YFinal = (float)Math.Round(yf + ( Dy / EscalaY ) , 2);
                    FAgregarRefMultipleCuadro.FormuAgregarMultipe.Dx = Dx; FAgregarRefMultipleCuadro.FormuAgregarMultipe.Dy = Dy; FAgregarRefMultipleCuadro.FormuAgregarMultipe.EscalaX = EscalaX; FAgregarRefMultipleCuadro.FormuAgregarMultipe.EscalaY = EscalaY;

                    fAgregar.ShowDialog();

                    if ( FAgregarRefMultipleCuadro.Close1 )
                    {
                        Add_Refuerzo_Multiple_Linea = false; Seleccion = true; DesplazaXMultiple = 0;
                        DesplazaYMultiple = 0; Add_Refuerzo_Multiple_Rectangulo = false; Seleccion = true; Add_Refuerzo_Multiple2_Rectangulo = false; return;
                    }

                    //TOMAR VALORES NUEVALMENTE
                    x = FAgregarRefMultipleCuadro.RefuerzoCuadro.XInicial + ( Dx / (float)EscalaX );
                    y = FAgregarRefMultipleCuadro.RefuerzoCuadro.YInicial - ( Dy / (float)EscalaY );
                    xf = FAgregarRefMultipleCuadro.RefuerzoCuadro.XFinal + ( Dx / (float)EscalaX );
                    yf = FAgregarRefMultipleCuadro.RefuerzoCuadro.YFinal - ( Dy / (float)EscalaY );

                    float Sh = FAgregarRefMultipleCuadro.RefuerzoCuadro.Sreal_Horizontal;
                    float Sv = FAgregarRefMultipleCuadro.RefuerzoCuadro.Sreal_Vertical;

                    string Diametro = FAgregarRefMultipleCuadro.Diametro;

                    int CantBarrasX = FAgregarRefMultipleCuadro.RefuerzoCuadro.Cantidad_Horizontal; int CantBarrasY = FAgregarRefMultipleCuadro.RefuerzoCuadro.Cantidad_VerticalUsar;

                    //Re calcular Separación
                    float DeltaX = Sh;
                    float DeltaY = Sv;

                    if ( x > xf ) { DeltaX = -DeltaX; }
                    if ( y > yf ) { DeltaY = -DeltaY; }

                    // Capa en X - Abajo;
                    for ( int i = 0 ; i < CantBarrasX ; i++ )
                    {
                        double[] Coord = new double[] { x - ( Dx / EscalaX ) + DeltaX * i , yf + ( Dy / EscalaY ) };
                        try
                        {
                            pid = seccion.Refuerzos.Last().id + 1;
                        }
                        catch { pid = 1; }
                        CRefuerzo new_refuerzo21 = new CRefuerzo(pid , Diametro , Coord , TipodeRefuerzo.longitudinal);
                        seccion.Refuerzos.Add(new_refuerzo21);
                    }
                    //Capa en X - Arriba
                    for ( int i = 0 ; i < CantBarrasX ; i++ )
                    {
                        double[] Coord = new double[] { x - ( Dx / EscalaX ) + DeltaX * i , y + ( Dy / EscalaY ) };
                        try
                        {
                            pid = seccion.Refuerzos.Last().id + 1;
                        }
                        catch { pid = 1; }
                        CRefuerzo new_refuerzo21 = new CRefuerzo(pid , Diametro , Coord , TipodeRefuerzo.longitudinal);
                        seccion.Refuerzos.Add(new_refuerzo21);
                    }

                    //Capa en Y - Izquierda

                    for ( int i = 1 ; i < CantBarrasY ; i++ )
                    {
                        double[] Coord = new double[] { x - ( Dx / EscalaX ) , y + ( Dy / EscalaY ) + DeltaY * i };
                        try
                        {
                            pid = seccion.Refuerzos.Last().id + 1;
                        }
                        catch { pid = 1; }
                        CRefuerzo new_refuerzo21 = new CRefuerzo(pid , Diametro , Coord , TipodeRefuerzo.longitudinal);
                        seccion.Refuerzos.Add(new_refuerzo21);
                    }

                    //Capa en Y - Derecha

                    for ( int i = 1 ; i < CantBarrasY ; i++ )
                    {
                        double[] Coord = new double[] { xf - ( Dx / EscalaX ) , y + ( Dy / EscalaY ) + DeltaY * i };
                        try
                        {
                            pid = seccion.Refuerzos.Last().id + 1;
                        }
                        catch { pid = 1; }
                        CRefuerzo new_refuerzo21 = new CRefuerzo(pid , Diametro , Coord , TipodeRefuerzo.longitudinal);
                        seccion.Refuerzos.Add(new_refuerzo21);
                    }

                    DesplazaXMultiple = 0;
                    DesplazaYMultiple = 0;
                    Add_Refuerzo_Multiple_Rectangulo = false;
                    Seleccion = true;
                }
                Reload_Seccion();

                if ( Add_Refuerzo_Multiple_Rectangulo && Add_Refuerzo_Multiple2_Rectangulo == false )
                {
                    DesXMultiple = e.X;
                    DesYMultiple = e.Y;
                    Add_Refuerzo_Multiple2_Rectangulo = true;
                }
                else
                {
                    Add_Refuerzo_Multiple2_Rectangulo = false;
                }

                if ( Add_Refuerzo_Multiple_Linea && Add_Refuerzo_Multiple2_Linea == false )
                {
                    DesXMultiple = e.X;
                    DesYMultiple = e.Y;
                    Add_Refuerzo_Multiple2_Linea = true;
                }
                else
                {
                    Add_Refuerzo_Multiple2_Linea = false;
                }

                Grafica.Invalidate();
            }
            else if ( Seleccion )
            {
                if ( MouseOverRefuerzo(e.Location) & e.Button == MouseButtons.Right )
                {
                    Grafica.ContextMenuStrip = cmEditar_Ref;
                }
                else
                {
                    Grafica.ContextMenuStrip = cmMenuSecciones;
                }
            }
        }

        private void Get_coordinates ( object sender , MouseEventArgs e )
        {
            float X = e.X - Grafica.Width / 2;
            float Y = e.Y - Grafica.Height / 2;
            double X_r, Y_r;

            X_r = X / EscalaX;
            Y_r = Y / EscalaX;

            var Coord = new double[] { X_r - ( Dx / EscalaX ) , Y_r + ( Dy / EscalaY ) };

            label1.Text = "X:" + Math.Round(Coord[0] , 2) + " Y:" + Math.Round(-Coord[1] , 2);
            label1.Update();
        }

        private void Dibujo_Estribo ( Graphics g , ISeccion seccioni )
        {
            GraphicsPath path = new GraphicsPath();
            SolidBrush br = new SolidBrush(Color.FromArgb(100 , Color.Black));
            Pen P1;

            P1 = new Pen(Color.Black , 2.5f)
            {
                Brush = Brushes.DarkGray ,
                Color = Color.DarkGray ,
                DashStyle = DashStyle.Solid ,
                LineJoin = LineJoin.Round ,
                Alignment = System.Drawing.Drawing2D.PenAlignment.Center
            };

            path = seccioni.Add_Estribos(EscalaX , EscalaY , 0.04f , Dx , Dy);
            g.DrawPath(P1 , path);
            g.FillPath(br , path);
        }

        private void Add_Texto_Seccion ( Graphics g , ISeccion seccioni )
        {
            float TamanoFuente = 0;
            string Ref_Seccion = "Refuerzo Sección :";
            SolidBrush br = new SolidBrush(Color.Black);
            PointF PS = new PointF();

            TamanoFuente = Convert.ToSingle(5 * EscalaR);

            if ( TamanoFuente > 12.7f )
            {
                TamanoFuente = 12.7f;
            }

            Font Fuente = new Font("Calibri" , TamanoFuente , FontStyle.Bold);

            PS.X = ( -Grafica.Width / 2 ) + 30;
            PS.Y = ( -Grafica.Height / 2 ) + 30;

            g.DrawString(seccioni.ToString() , Fuente , br , PS);

            #region Texto refuerzo

            PS.Y += 20;

            for ( int i = 0 ; i < seccioni.No_D_Barra.Count ; i++ )
            {
                if ( i == seccioni.No_D_Barra.Count - 1 )
                {
                    Ref_Seccion += $"{seccioni.No_D_Barra[i].Item1}#{seccioni.No_D_Barra[i].Item2}";
                }
                else
                {
                    Ref_Seccion += $"{seccioni.No_D_Barra[i].Item1}#{seccioni.No_D_Barra[i].Item2}+";
                }
            }

            g.DrawString(Ref_Seccion , Fuente , br , PS);

            #endregion Texto refuerzo

            #region Info Estribos

            PS.Y += 20;

            Ref_Seccion = seccioni.Estribo.ToString();
            g.DrawString(Ref_Seccion , Fuente , br , PS);

            PS.Y += 20;

            Ref_Seccion = $"Ramas dir X: {seccioni.Estribo.NoRamasV1 + seccioni.Estribo.NoRamasV2}";
            g.DrawString(Ref_Seccion , Fuente , br , PS);

            PS.Y += 20;
            Ref_Seccion = $"Ramas dir Y: {seccioni.Estribo.NoRamasH1 + seccioni.Estribo.NoRamasH2}";
            g.DrawString(Ref_Seccion , Fuente , br , PS);

            #endregion Info Estribos
        }

        private void Reload_Seccion ()
        {
            int indice = 0;

            if ( edicion == Tipo_Edicion.Secciones_modelo )
            {
                indice = Form1.Proyecto_.ColumnaSelect.Seccions.FindIndex(x1 => x1.Item2 == Piso);
                Form1.Proyecto_.ColumnaSelect.Seccions[indice] = new Tuple<ISeccion , string>(seccion , Piso);
            }

            if ( edicion == Tipo_Edicion.Secciones_predef & GDE == GDE.DMO )
            {
                indice = Form1.secciones_predef.Secciones_DMO.FindIndex(x1 => x1.ToString() == seccion.ToString());
                Form1.secciones_predef.Secciones_DMO[indice] = seccion;
            }

            if ( edicion == Tipo_Edicion.Secciones_predef & GDE == GDE.DES )
            {
                indice = Form1.secciones_predef.Secciones_DES.FindIndex(x1 => x1.ToString() == seccion.ToString());
                Form1.secciones_predef.Secciones_DES[indice] = seccion;
            }
        }

        private void Dibujo_Refuerzo ( Graphics g , ISeccion seccioni )
        {
            SolidBrush br = new SolidBrush(Color.Black);
            SolidBrush br_T = new SolidBrush(Color.Black);
            int cont = 1;
            int Diametro;
            float TamanoFuente = 0;
            PointF PS = new PointF();

            Pen P1;
            float DeltaX, DeltaY;

            DeltaX = Convert.ToSingle(4 * Xmax / ( Grafica.Width / 2 ));
            DeltaY = Convert.ToSingle(4 * Ymax / ( Grafica.Height / 2 ));
            TamanoFuente = Convert.ToSingle(2.5 * EscalaR);

            if ( TamanoFuente < 6 )
            {
                TamanoFuente = 6f;
            }

            if ( TamanoFuente > 14 )
            {
                TamanoFuente = 14f;
            }

            Font Fuente = new Font("Calibri" , TamanoFuente , FontStyle.Bold);

            P1 = new Pen(Color.Black , 2.5f)
            {
                Brush = Brushes.Black ,
                Color = Color.Black ,
                DashStyle = DashStyle.Solid ,
                LineJoin = LineJoin.MiterClipped ,
                Alignment = PenAlignment.Center
            };

            for ( int i = 0 ; i < seccioni.Shapes_ref.Count ; i++ )
            {
                Diametro = Convert.ToInt32(seccioni.Refuerzos[i].Diametro.Substring(1));
                br = FunctionsProject.ColorBarra(Diametro);

                PS.X = seccioni.Shapes_ref[i].PathPoints[0].X + DeltaX;
                PS.Y = seccioni.Shapes_ref[i].PathPoints[0].Y + DeltaY;

                if ( edicion == Tipo_Edicion.Secciones_modelo )
                {
                    g.DrawString(seccion.Refuerzos[i].Alzado.ToString() , Fuente , br_T , PS);
                }
                else
                {
                    g.DrawString(cont.ToString() , Fuente , br_T , PS);
                }

                g.DrawPath(P1 , seccioni.Shapes_ref[i]);
                g.FillPath(br , seccioni.Shapes_ref[i]);
                cont++;
            }
        }

        private void Grafica_MouseDown ( object sender , MouseEventArgs e )
        {
            Seleccionar(sender , e);
            if ( e.Button == MouseButtons.Middle && e.Clicks == 2 )
            {
                ex = 1; ey = 1;
                Dx = 0; Dy = 0;
                Grafica.Invalidate();
            }
            if ( e.Button == MouseButtons.Middle )
            {
                DesXButton = e.X;
                DesYButton = e.Y;
            }
        }

        private void lbPisos_SelectedIndexChanged ( object sender , EventArgs e )
        {
            Dx = 0; Dy = 0;
            Indice_Lb = lbPisos.SelectedIndex;
            Piso = lbPisos.SelectedItem.ToString();
            if ( edicion == Tipo_Edicion.Secciones_modelo )
            {
                Get_section(true);
            }
            else
            {
                Get_Predef_Secction();
            }
            Grafica.Invalidate();
        }

        private void cbSecciones_SelectedIndexChanged ( object sender , EventArgs e )
        {
            Dx = 0; Dy = 0;
            Actualizar_Lista();
        }

        private void editarRefuerzoToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            EditarRef = new FEditarRef(seccion , Piso , Indice_ref , this);
            EditarRef.Show();
        }

        private void eliminarRefuerzoToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            int indice = 0;
            seccion.Refuerzos.RemoveAt(Indice_ref);

            if ( edicion == Tipo_Edicion.Secciones_modelo )
            {
                indice = Form1.Proyecto_.ColumnaSelect.Seccions.FindIndex(x1 => x1.Item2 == Piso);
                Form1.Proyecto_.ColumnaSelect.Seccions[indice] = new Tuple<ISeccion , string>(seccion , Piso);
            }

            if ( edicion == Tipo_Edicion.Secciones_predef )
            {
                if ( GDE == GDE.DMO )
                {
                    indice = Form1.secciones_predef.Secciones_DMO.FindIndex(x1 => x1.ToString() == seccion.ToString());
                    Form1.secciones_predef.Secciones_DMO[indice] = seccion;
                }
                else
                {
                    indice = Form1.secciones_predef.Secciones_DES.FindIndex(x1 => x1.ToString() == seccion.ToString());
                    Form1.secciones_predef.Secciones_DES[indice] = seccion;
                }
            }
            Grafica.Invalidate();
        }

        private void BSeleccionar_columna_Click ( object sender , EventArgs e )
        {
            if ( edicion == Tipo_Edicion.Secciones_modelo )
            {
                FAgregarRef fseleccion = new FAgregarRef(seccion , Piso , this);
                fseleccion.ShowDialog();
            }

            if ( edicion == Tipo_Edicion.Secciones_predef )
            {
                FAgregarSeccion fseccion = new FAgregarSeccion(GDE , lbPisos);
                fseccion.ShowDialog();
            }
        }

        private void Button1_Click ( object sender , EventArgs e )
        {
            seccion.DiagramaInteraccion();

            DiagramaInteraccion diagramaInteraccion = new DiagramaInteraccion();
            DiagramaInteraccion.Seccion = seccion;

            if ( edicion == Tipo_Edicion.Secciones_modelo )
            {
                Columna col = Form1.Proyecto_.ColumnaSelect;
                panel2.Location = new Point(332 , 145);
                int indice = col.Seccions.FindIndex(x => x.Item2 == Piso);
                string[] CargasDistintas = col.resultadosETABs[indice].Load.Distinct().ToArray();
                CASOSCARGA.Items.Clear();
                CASOSCARGA.Items.AddRange(CargasDistintas);
                panel2.Visible = true;
                return;
            }

            diagramaInteraccion.ShowDialog();
        }

        private void Radio_Dmo_CheckedChanged ( object sender , EventArgs e )
        {
            GDE = GDE.DMO;
            Load_predef();
        }

        private void Radio_Des_CheckedChanged ( object sender , EventArgs e )
        {
            GDE = GDE.DES;
            Load_predef();
        }

        private void agregarSecciónToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            FAgregarSeccion agregarSeccion = new FAgregarSeccion(GDE , lbPisos);
            agregarSeccion.Show();
        }

        private void eliminarSecciónToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            string Nombre_Seccion = lbPisos.Items[Indice_Lb].ToString();
            int Indice_Lista = 0;

            if ( GDE == GDE.DMO )
            {
                Indice_Lista = Form1.secciones_predef.Secciones_DMO.FindIndex(x1 => x1.ToString() == Nombre_Seccion);
                Form1.secciones_predef.Secciones_DMO.RemoveAt(Indice_Lista);
                lbPisos.Items.Remove(Indice_Lista);
                Actualizar_Lista();
            }
            else
            {
                Indice_Lista = Form1.secciones_predef.Secciones_DES.FindIndex(x1 => x1.ToString() == Nombre_Seccion);
                Form1.secciones_predef.Secciones_DES.RemoveAt(Indice_Lista);
                lbPisos.Items.Remove(Indice_Lista);
                Actualizar_Lista();
            }
        }

        private void Actualizar_Lista ()
        {
            ISeccion[] Secciones = { };
            string Fc_secciones = "";

            Fc_secciones = cbSecciones.Text;

            if ( GDE == GDE.DMO )
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

        private void ToolStripButton4_Click ( object sender , EventArgs e )
        {
        }

        private void ToolStripButton2_Click ( object sender , EventArgs e )
        {
        }

        private void ToolStripButton3_Click ( object sender , EventArgs e )
        {
            Seleccion = true;
            Add_Refuerzo_Multiple_Linea = false;
            Add_Refuerzo = false;
        }

        private void BarraIndividualToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            Add_Refuerzo = true;
            Add_Refuerzo_Multiple_Linea = false;
            Seleccion = false;
        }

        private void tbSeleccionar_Click ( object sender , EventArgs e )
        {
            ButtonSeleccionar();
        }

        private void ButtonSeleccionar ()
        {
            Seleccion = true;
            Add_Refuerzo_Multiple_Linea = false;
            Add_Refuerzo = false;
            Add_Refuerzo_Multiple_Rectangulo = false;
        }

        private void ToolStripButton8_Click ( object sender , EventArgs e )
        {
            #region Guardado secciones predef

            CUsuario usuario = new CUsuario();
            string Ruta_Completa = @"\\servidor\\Dllo SW\\Secciones Predefinidas - Columnas\\Secciones.sec";
            usuario.Get_user();

            if ( usuario.Permiso == true )
            {
                FunctionsProject.Serializar_Secciones(Ruta_Completa , Form1.secciones_predef);
            }

            #endregion Guardado secciones predef
        }

        private void ToolStripButton7_Click ( object sender , EventArgs e )
        {
            if ( edicion == Tipo_Edicion.Secciones_predef )
            {
                FAgregarSeccion agregarSeccion = new FAgregarSeccion(GDE , lbPisos);
                agregarSeccion.Show();
            }
        }

        private void BarraIndiviudalToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            Add_Refuerzo = true;
            Add_Refuerzo_Multiple_Linea = false;
            Add_Refuerzo_Multiple_Rectangulo = false;
            Seleccion = false;
        }

        private void BarraMultipleToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            Add_Refuerzo_Multiple2_Rectangulo = false;
            Add_Refuerzo_Multiple_Rectangulo = false;
            Add_Refuerzo_Multiple2_Linea = false;
            Add_Refuerzo_Multiple_Linea = true;
            Seleccion = false;
            Add_Refuerzo = false;
        }

        private void FInterfaz_Seccion_Scroll ( object sender , MouseEventArgs e )
        {
            int vueltas = e.Delta;

            if ( vueltas > 0 )
            {
                ex++;
                ey++;
                Grafica.Invalidate();
            }
            else
            {
                if ( ex > 1 )
                {
                    ex--;
                    ey--;
                    Grafica.Invalidate();
                }
            }
        }

        private void Analizar_Click ( object sender , EventArgs e )
        {
            DiagramaInteraccion diagramaInteraccion = new DiagramaInteraccion();
            Columna col = Form1.Proyecto_.ColumnaSelect;
            int indice = col.Seccions.FindIndex(x => x.Item2 == Piso);
            List<float[]> MP_solic = new List<float[]>();

            foreach ( string Carga in CASOSCARGA.SelectedItems )
            {
                for ( int i = 0 ; i < col.resultadosETABs[indice].Load.Count ; i++ )
                {
                    if ( col.resultadosETABs[indice].Load[i].Contains(Carga) )
                    {
                        //   float[] MXPYPU = new float[] { col.resultadosETABs[indice].M3[i], col.resultadosETABs[indice].M2[i], col.resultadosETABs[indice].P[i] };
                        float[] MXPYPU = new float[] { col.resultadosETABs[indice].M2[i] , col.resultadosETABs[indice].M3[i] , col.resultadosETABs[indice].P[i] };
                        MP_solic.Add(MXPYPU);
                    }
                }
            }

            DiagramaInteraccion.MP_Soli3D = MP_solic;
            panel2.Visible = false;

            diagramaInteraccion.ShowDialog();
        }

        private void Analizar_KeyUp ( object sender , KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Escape )
            {
                panel2.Visible = false;
            }
        }

        private void button1_Click_1 ( object sender , EventArgs e )
        {
            panel2.Visible = false;
        }

        private void toolStripMenuItem2_Click ( object sender , EventArgs e )
        {
            Add_Refuerzo_Multiple2_Rectangulo = false;
            Add_Refuerzo_Multiple_Rectangulo = true;
            Add_Refuerzo_Multiple2_Linea = false;
            Add_Refuerzo_Multiple_Linea = false;
            Seleccion = false;
            Add_Refuerzo = false;
        }

        private void FInterfaz_Seccion_KeyDown ( object sender , KeyEventArgs e )
        {
            if ( e.KeyCode == Keys.Escape )
            {
                ButtonSeleccionar();
            }
        }

        private void AsignarSeccionesConCambios ()
        {
            arrribaToolStripMenuItem.Enabled = false;
            abajoToolStripMenuItem.Enabled = false;
            mostrarSecciónesVecinasToolStripMenuItem.Enabled = true;
            if ( seccion.SeccionesVecinosCambios == null )
            {
                seccion.SeccionesVecinosCambios = new List<Tuple<ISeccion , string>>();
            }
            else
            {
                seccion.SeccionesVecinosCambios.Clear();
            }

            for ( int i = Columna_i.Seccions.Count - 1 ; i >= 0 ; i-- )
            {
                Tuple<ISeccion , string> seccion1 = Columna_i.Seccions[i];
                if ( Piso == seccion1.Item2 )
                {
                    ISeccion seccionV1 = null; ISeccion seccionV2 = null; string PisoV1 = ""; string PisoV2 = "";

                    try { seccionV1 = FunctionsProject.DeepClone(Columna_i.Seccions[i + 1].Item1); PisoV1 = Columna_i.Seccions[i + 1].Item2; } catch { }
                    try { seccionV2 = FunctionsProject.DeepClone(Columna_i.Seccions[i - 1].Item1); PisoV2 = Columna_i.Seccions[i - 1].Item2; } catch { }

                    if ( CompararSecciones(seccion , seccionV2) != null )
                    {
                        seccion.SeccionesVecinosCambios.Add(new Tuple<ISeccion , string>(seccionV2 , PisoV2));
                        arrribaToolStripMenuItem.Enabled = true;
                    }
                    if ( CompararSecciones(seccion , seccionV1) != null )
                    {
                        seccion.SeccionesVecinosCambios.Add(new Tuple<ISeccion , string>(seccionV1 , PisoV1));
                        abajoToolStripMenuItem.Enabled = true;
                    }
                    break;
                }
            }

            if ( seccion.SeccionesVecinosCambios.Count == 0 )
            {
                mostrarSecciónesVecinasToolStripMenuItem.Enabled = false;
            }
        }

        private ISeccion CompararSecciones ( ISeccion seccionOrg , ISeccion seccionVecina )
        {
            if ( seccionVecina != null )
            {
                if ( seccionVecina.B != seccionOrg.B | seccionVecina.H != seccionOrg.H )
                {
                    return seccionVecina;
                }
            }
            return null;
        }
    }
}