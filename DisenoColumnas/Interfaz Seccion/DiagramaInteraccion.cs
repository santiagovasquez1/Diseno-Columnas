using DisenoColumnas.Clases.OpenGL;
using DisenoColumnas.Secciones;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class DiagramaInteraccion : Form
    {
        public DiagramaInteraccion()
        {
            InitializeComponent();
        }

        #region Para OpenGl

        private int eyeX = 100, eyeY = 100, eyeZ = 100; private bool Loaded = false;
        private List<Int32> GList;
        private List<Line> lines = new List<Line>();
        private List<Dot> dots = new List<Dot>();

        public static ISeccion Seccion;
        public static List<float[]> MP_Soli3D = new List<float[]>();

        private int Angulo = 0;
        private List<float[]> MP2D { get; set; }
        private List<float[]> MP3D { get; set; }
        private List<float[]> MP3D_SoloUnaRecta { get; set; }

        private List<Tuple<List<float[]>, int>> TuplesMP3D { get; set; }

        private bool ConFi { get; set; }

        private void lineGenerator(float width, Color color, int x1, int y1, int z1, int x2, int y2, int z2)
        {
            Line temp = new Line();

            temp.from[0] = x1;
            temp.from[1] = y1;
            temp.from[2] = z1;
            temp.to[0] = x2;
            temp.to[1] = y2;
            temp.to[2] = z2;
            temp.color = color;
            temp.width = width;
            lines.Add(temp);
        }

        private void CreateDots(Color color, decimal X, decimal Y, decimal Z)
        {
            Dot temp = new Dot();
            temp.dot[0] = X;
            temp.dot[1] = Z;
            temp.dot[2] = Y;
            temp.color = color;
            dots.Add(temp);
        }

        private void Redraw_Tick(object sender, EventArgs e)
        {
            GList = new List<Int32>();
            GList.Add(0);
            if (MostrarSolicita.Checked)
            {
                foreach (Dot dot in dots)
                {
                    GL.NewList(GList.Count, ListMode.Compile);
                    GL.PointSize(5);
                    GL.Begin(BeginMode.Points);
                    GL.Color3(dot.color);

                    decimal[] aux_dot = FactoryMatrix.xVxM(dot.matrix, dot.dot);
                    GL.Vertex3(Decimal.ToDouble(aux_dot[0]), Decimal.ToDouble(aux_dot[1]), Decimal.ToDouble(aux_dot[2]));

                    GL.End();
                    GL.EndList();
                    GList.Add(GList.Count);
                }
            }
            foreach (Line line in lines)
            {
                decimal[] from = FactoryMatrix.xVxM(line.matrix, line.from);
                decimal[] to = FactoryMatrix.xVxM(line.matrix, line.to);

                GL.NewList(GList.Count, ListMode.Compile);
                GL.Begin(BeginMode.Lines);

                GL.LineWidth(line.width);
                GL.Color3(line.color);
                GL.Vertex3(Decimal.ToDouble(from[0]), Decimal.ToDouble(from[1]), Decimal.ToDouble(from[2]));
                GL.Vertex3(Decimal.ToDouble(to[0]), Decimal.ToDouble(to[1]), Decimal.ToDouble(to[2]));
                GL.End();
                GL.EndList();
                GList.Add(GList.Count);
            }

            Gl_Paint(gl, null);
        }

        private void CreateLineDecimal(float width, Color color, decimal x1, decimal y1, decimal z1, decimal x2, decimal y2, decimal z2, int ID)
        {
            Line temp = new Line();
            temp.ID = ID;
            temp.from[0] = x1;
            temp.from[1] = z1;
            temp.from[2] = y1;
            temp.to[0] = x2;
            temp.to[1] = z2;
            temp.to[2] = y2;
            temp.color = color;
            temp.width = width;
            lines.Add(temp);
        }

        private void CrearLines()
        {
            if (Seccion != null)
            {
                lines.RemoveAll(x => x.ID != 0);
                lines.RemoveAll(x => x.ID == -999);

                decimal MaxX = -9999999;
                decimal MaxY = -9999999;
                decimal MaxZ = -9999999;

                for (int i = 0; i < TuplesMP3D.Count; i++)
                {
                    for (int j = 0; j < TuplesMP3D[i].Item1.Count; j++)
                    {
                        if (MaxX < (decimal)TuplesMP3D[i].Item1[j][0])
                        {
                            MaxX = (decimal)TuplesMP3D[i].Item1[j][0];
                        }
                        if (MaxY < (decimal)TuplesMP3D[i].Item1[j][1])
                        {
                            MaxY = (decimal)TuplesMP3D[i].Item1[j][1];
                        }
                        if (MaxZ < (decimal)TuplesMP3D[i].Item1[j][2])
                        {
                            MaxZ = (decimal)TuplesMP3D[i].Item1[j][2];
                        }
                    }
                }
                float Ancho = 60; float Alto = 60; float Largo = 100;

                decimal EscalaX = (decimal)Ancho / MaxX;
                decimal EscalaY = (decimal)Alto / MaxY;
                decimal EscalaZ = (decimal)Largo / MaxZ;

                int ID = 1;
                for (int i = 0; i < TuplesMP3D.Count; i++)
                {
                    for (int j = 0; j < TuplesMP3D[i].Item1.Count; j++)
                    {
                        try
                        {
                            decimal X1 = (decimal)TuplesMP3D[i].Item1[j][0] * EscalaX;
                            decimal Y1 = (decimal)TuplesMP3D[i].Item1[j][1] * EscalaY;
                            decimal Z1 = (decimal)TuplesMP3D[i].Item1[j][2] * EscalaZ;

                            decimal X2 = (decimal)TuplesMP3D[i].Item1[j + 1][0] * EscalaX;
                            decimal Y2 = (decimal)TuplesMP3D[i].Item1[j + 1][1] * EscalaY;
                            decimal Z2 = (decimal)TuplesMP3D[i].Item1[j + 1][2] * EscalaZ;

                            ID += 1;
                            Color color = Color.FromArgb(0, 255, 0);

                            CreateLineDecimal(1, color, X1, Y1, Z1, X2, Y2, Z2, ID);
                        }
                        catch { }
                    }
                }

                for (int i = 0; i < MP3D_SoloUnaRecta.Count; i++)
                {
                    try
                    {
                        decimal X1 = (decimal)MP3D_SoloUnaRecta[i][0] * EscalaX;
                        decimal Y1 = (decimal)MP3D_SoloUnaRecta[i][1] * EscalaY;
                        decimal Z1 = (decimal)MP3D_SoloUnaRecta[i][2] * EscalaZ;

                        decimal X2 = (decimal)MP3D_SoloUnaRecta[i + 1][0] * EscalaX;
                        decimal Y2 = (decimal)MP3D_SoloUnaRecta[i + 1][1] * EscalaY;
                        decimal Z2 = (decimal)MP3D_SoloUnaRecta[i + 1][2] * EscalaZ;
                        CreateLineDecimal(1, Color.FromArgb(255, 0, 255), X1, Y1, Z1, X2, Y2, Z2, -999);
                    }
                    catch
                    {
                    }
                }

                decimal FC1 = 1000;
                decimal FC2 = 100000;
                if (MostrarSolicita.Checked)
                {
                    dots.Clear();
                    for (int i = 0; i < MP_Soli3D.Count; i++)
                    {
                        decimal X = (decimal)MP_Soli3D[i][0] * FC2 * EscalaX;
                        decimal Y = (decimal)MP_Soli3D[i][1] * FC2 * EscalaY;
                        decimal Z = (decimal)MP_Soli3D[i][2] * FC1 * EscalaZ;
                        CreateDots(Color.Red, X, Y, Z);
                    }
                }
            }
        }

        private void TomarValores(bool conFi)
        {
            if (conFi)
            {
                MP2D = (Seccion.PuMu2D.Find(x => x.Item2 == Angulo).Item1);
                MP3D = (Seccion.MuPu3D.Find(x => x.Item2 == Angulo).Item1);
                TuplesMP3D = null;
                TuplesMP3D = (Seccion.MuPu3D);
                MP3D_SoloUnaRecta = Seccion.MuPu3D.Find(x => x.Item2 == Angulo).Item1;
            }
            else
            {
                MP2D = (Seccion.PnMn2D.Find(x => x.Item2 == Angulo).Item1);
                MP3D = (Seccion.MnPn3D.Find(x => x.Item2 == Angulo).Item1);
                MP3D_SoloUnaRecta = Seccion.MnPn3D.Find(x => x.Item2 == Angulo).Item1;
                TuplesMP3D = null;
                TuplesMP3D = (Seccion.MnPn3D);
            }
        }

        private float FC1 = 0.001f;
        private float FC2 = 1f / 100000f;

        private void CreateDataGrid()
        {
            D_MnPn.Rows.Clear();
            if (Seccion != null)
            {
                for (int i = 0; i < MP3D.Count; i++)
                {
                    D_MnPn.Rows.Add();
                    D_MnPn.Rows[D_MnPn.Rows.Count - 1].Cells[0].Value = String.Format("{0:0.00}", MP3D[i][2] * FC1);
                    D_MnPn.Rows[D_MnPn.Rows.Count - 1].Cells[1].Value = String.Format("{0:0.00}", MP3D[i][0] * FC2);
                    D_MnPn.Rows[D_MnPn.Rows.Count - 1].Cells[2].Value = String.Format("{0:0.00}", MP3D[i][1] * FC2);
                }
            }
            FunctionsProject.EstiloDatGridView(D_MnPn);
        }

        private void Graficar(Chart chart)
        {
            if (Seccion != null)
            {
                chart.Series[0].Points.Clear();
                chart.Series[0].Color = Color.Black;
                chart.Series[0].MarkerStyle = MarkerStyle.None;
                chart.Series[0].MarkerSize = 1;
                chart.Series[0].MarkerStep = 5;
                chart.Series[0].MarkerColor = Color.Black;
                chart.Series[0].LabelBackColor = Color.Transparent;
                chart.Series[0].ChartType = SeriesChartType.Line;

                chart.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Triangle;
                chart.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Triangle;

                chart.ChartAreas[0].AxisX.RoundAxisValues();
                chart.ChartAreas[0].AxisY.RoundAxisValues();
                chart.ChartAreas[0].AxisX.Minimum = 0;

                if (ConFi)
                {
                    chart.ChartAreas[0].AxisX.Title = "Mu (Ton-m)";
                    chart.ChartAreas[0].AxisY.Title = "Pu (Ton)";
                }
                else
                {
                    chart.ChartAreas[0].AxisX.Title = "Mn (Ton-m)";
                    chart.ChartAreas[0].AxisY.Title = "Pn (Ton)";
                }

                foreach (float[] Point in MP2D)
                {
                    chart.Series[0].Points.AddXY(Point[0] * FC2, Point[1] * FC1);
                }
            }
        }

        private void Gl_Load(object sender, EventArgs e)
        {
            Loaded = true;
            GL.ClearColor(Color.Black);
            generarFlechas();
            MostrarValores();
        }

        private void X_mas_Click(object sender, EventArgs e)
        {
            eyeX += 20;
        }

        private void X_menos_Click(object sender, EventArgs e)
        {
            eyeX -= 20;
        }

        private void Y_mas_Click(object sender, EventArgs e)
        {
            eyeZ += 20;
        }

        private void Y_menos_Click(object sender, EventArgs e)
        {
            eyeZ -= 20;
        }

        private void Z_mas_Click(object sender, EventArgs e)
        {
            eyeY += 20;
        }

        private void Z_menos_Click(object sender, EventArgs e)
        {
            eyeY -= 20;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Angulo += 10;
            if (Angulo > 350)
            {
                Angulo = 0;
            }
            MostrarValores();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Angulo -= 10;
            if (Angulo < 0)
            {
                Angulo = 350;
            }
            MostrarValores();
        }

        private void MostrarValores()
        {
            GroupBox_Grafica_Diagrama1.Text = $"Angulo de {Angulo}°";
            Title.Text = $"Diagrama de Interacción - {Angulo}°";
            TomarValores(ConFi);
            CreateDataGrid();
            CrearLines();
            Graficar(CharMomentos);
            Redraw_Tick(gl, null);
        }

        private void DiagramaInteraccion_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Panel5_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void DiagramaInteraccion_Load(object sender, EventArgs e)
        {
            MostrarValores();
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            PictureBox1.BackColor = Color.Transparent;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox1.BackColor = Color.White;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ConFi = false;
            }
            else
            {
                ConFi = true;
            }
            MostrarValores();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                ConFi = false;
            }
            else
            {
                ConFi = true;
            }
            MostrarValores();
        }

        private void MostrarSolicita_CheckedChanged(object sender, EventArgs e)
        {
            MostrarValores();
        }

        private void Gl_Paint(object sender, PaintEventArgs e)
        {
            if (!Loaded)
                return;

            if (GList == null)
                return;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 1F, 0.1F, 20000F);

            GL.LoadMatrix(ref perspective);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            Matrix4 lookat = Matrix4.LookAt(eyeX, eyeY, eyeZ, 0, 0, 0, 0, 1, 0);
            GL.LoadMatrix(ref lookat);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();
            for (int i = 0; i < GList.Count; i++)
                GL.CallList(GList[i]);

            GL.PopMatrix();
            GL.Finish();
            try
            {
                gl.SwapBuffers();
            }
            catch { }
        }

        private void generarFlechas()
        {
            lineGenerator(1, Color.Yellow, -100, 0, 0, 100, 0, 0);
            lineGenerator(1, Color.Yellow, -100, 0, 0, -90, 0, 5);
            lineGenerator(1, Color.Yellow, -100, 0, 0, -90, 0, -5);
            lineGenerator(1, Color.Yellow, 100, 0, 0, 90, 0, 5);
            lineGenerator(1, Color.Yellow, 100, 0, 0, 90, 0, -5);

            lineGenerator(1, Color.Red, 0, -100, 0, 0, 100, 0);
            lineGenerator(1, Color.Red, 0, -100, 0, 5, -90, 0);
            lineGenerator(1, Color.Red, 0, -100, 0, -5, -90, 0);
            lineGenerator(1, Color.Red, 0, 100, 0, 5, 90, 0);
            lineGenerator(1, Color.Red, 0, 100, 0, -5, 90, 0);

            lineGenerator(1, Color.Blue, 0, 0, -100, 0, 0, 100);
            lineGenerator(1, Color.Blue, 0, 0, -100, 0, 5, -90);
            lineGenerator(1, Color.Blue, 0, 0, -100, 0, -5, -90);
            lineGenerator(1, Color.Blue, 0, 0, 100, 0, 5, 90);
            lineGenerator(1, Color.Blue, 0, 0, 100, 0, -5, 90);
        }

        #endregion Para OpenGl
    }
}