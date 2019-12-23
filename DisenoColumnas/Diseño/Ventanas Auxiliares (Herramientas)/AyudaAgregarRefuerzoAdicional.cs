using DisenoColumnas.Clases;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DisenoColumnas.Diseño.Ventanas_Auxiliares__Herramientas_
{
    public partial class AyudaAgregarRefuerzoAdicional : Form
    {
        public AlzadoUnitario ConvecionAlzado { get; set; }
        public static string PisoCorrespondiente { get; set; }
        public static int AlzadoCorrespondiente { get; set; }
        public static int RowCorrespondiente { get; set; }

        public AyudaAgregarRefuerzoAdicional()
        {
            InitializeComponent();
        }

        private void AyudaAgregarRefuerzoAdicional_Load(object sender, EventArgs e)
        {
            CantBarras.Text = Convert.ToString(5);
            NoBarra.Text = Convert.ToString(6);
            ConvecionesRefuerzoAdicional.SelectedIndex = 0;
            Text = $"Refuerzo Adicional – {PisoCorrespondiente} - Alzado: {AlzadoCorrespondiente + 1}";
            PisoAagregar.Text = PisoCorrespondiente;
            AlzadoAagregar.Text = (AlzadoCorrespondiente + 1).ToString();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            float LuzLibre = 2.3f;
            float VigaH = 0.5f;
            float B = 1f;
            float XI = 20f;
            float YI = 5f;
            float SX = ((DrawAyuda.Width - XI) / B);
            float eF = 0.5f;
            float SY = (DrawAyuda.Height - 20) / (LuzLibre + VigaH + eF + LuzLibre / 2);
            e.Graphics.Clear(Color.White);
            if (ConvecionAlzado != null)
            {
                GraficarEntrePisos_Ejemplo(e, LuzLibre, B, XI, YI, SX, SY, DrawAyuda.Height - YI);
            }
        }

        private void ClasificarConvenciones(string Nomenclatura)
        {
            object[] Clasificacion = ClasificarCelda(Nomenclatura);

            int Cant_Barras = (int)Clasificacion[1];
            int NoBarra = (int)Clasificacion[2];
            string T = (string)Clasificacion[3];
            string Tipo2 = (string)Clasificacion[7];
            string RefAd_Str = (string)Clasificacion[4];

            ConvecionAlzado = new AlzadoUnitario(Cant_Barras, NoBarra, T, 1, 1, 2.3f, 0.5f, 0.8f, false, 2.8f);
            ConvecionAlzado.Tipo2 = Tipo2;
            if (RefAd_Str == "-")
            {
                ConvecionAlzado.UnitarioAdicional = new AlzadoUnitario(Cant_Barras, NoBarra, T, 1, 1, 2.3f, 0.5f, 0.8f, false, 3.2f);
            }
        }

        private void GraficarEntrePisos_Ejemplo(PaintEventArgs e, float LuzLibreAux, float B, float XI, float YI, float SX, float SY, float HeightPicture)
        {
            float H_Viga = 0.5f;

            if (ConvecionAlzado.Tipo == "A" && ConvecionAlzado.UnitarioAdicional == null)
            {
                Pen Puntuda = new Pen(Color.Black, 2);
                Puntuda.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                LuzLibreAux += 0.5f + LuzLibreAux / 2;
                PointF point1 = new PointF(XI, HeightPicture - YI);
                PointF point2 = new PointF(XI, HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY);

                e.Graphics.DrawLine(Puntuda, point1, point2);

                PointF point3 = new PointF(B * SX, HeightPicture - YI);
                PointF point4 = new PointF(B * SX, HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY);
                PointF[] pointFs1 = new PointF[] { point1, point2, point4, point3 };
                SolidBrush brush2 = new SolidBrush(Color.FromArgb(169, 172, 173));
                e.Graphics.FillPolygon(brush2, pointFs1);

                e.Graphics.DrawLine(Puntuda, point3, point4);

                //Dibujar Viga
                SolidBrush brush3 = new SolidBrush(Color.FromArgb(198, 198, 198));
                RectangleF rectangleF = new RectangleF(XI, HeightPicture - YI - (LuzLibreAux * SY) / 2 - H_Viga * SY, B * SX - XI, H_Viga * SY);

                e.Graphics.FillRectangle(brush3, rectangleF);

                PointF[] pointFs = new PointF[] {new PointF(XI, HeightPicture - YI - (LuzLibreAux * SY) / 2 - H_Viga * SY),
                                                 new PointF(XI+B*SX-XI, HeightPicture - YI - (LuzLibreAux * SY) / 2 - H_Viga * SY),
                                                 new PointF(XI + B * SX - XI, HeightPicture - YI - (LuzLibreAux * SY) / 2  ),
                                                 new PointF(XI, HeightPicture - YI - (LuzLibreAux * SY) / 2 )  };
                e.Graphics.DrawLines(new Pen(Color.Black), pointFs);

                if (ConvecionAlzado.Tipo2 == "")
                {
                    float Distx = 0.3f;
                    float Traslapo__ = 0.2f;
                    PointF pointI = new PointF(Distx * SX, HeightPicture - YI - Traslapo__ * SY);
                    PointF pointF = new PointF(Distx * SX, HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY + Traslapo__ * SY);

                    Pen BarraPen = new Pen(Color.FromArgb(32, 52, 110), 2);
                    e.Graphics.DrawLine(BarraPen, pointI, pointF);

                    SolidBrush brush4 = new SolidBrush(Color.FromArgb(186, 48, 48));

                    e.Graphics.DrawString("Ld + 40cm", new Font("Calibri", 8), brush4, Distx * SX + 0.1f * SX, HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY + Traslapo__ * SY + (HeightPicture - YI - H_Viga * SY - LuzLibreAux * SY / 2 - (HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY + Traslapo__ * SY)) / 2);
                    e.Graphics.DrawString("Ld + 40cm", new Font("Calibri", 8), brush4, Distx * SX + 0.1f * SX, HeightPicture - YI - LuzLibreAux / 2 * SY + Traslapo__ * SY);
                }
                if (ConvecionAlzado.Tipo2 == "+")
                {
                    float Distx = 0.3f;
                    float Traslapo__ = 0.4f;
                    PointF pointI = new PointF(Distx * SX, HeightPicture - YI - 1.2f * Traslapo__ * SY);
                    PointF pointF = new PointF(Distx * SX, HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY - 0.1f * Traslapo__ * SY);

                    Pen BarraPen = new Pen(Color.FromArgb(32, 52, 110), 2);
                    e.Graphics.DrawLine(BarraPen, pointI, pointF);

                    SolidBrush brush4 = new SolidBrush(Color.FromArgb(186, 48, 48));

                    e.Graphics.DrawString("Ld + 40cm", new Font("Calibri", 8), brush4, Distx * SX + 0.1f * SX, HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY + 0.6f * Traslapo__ * SY + (HeightPicture - YI - H_Viga * SY - LuzLibreAux * SY / 2 - (HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY + Traslapo__ * SY)) / 2);
                    e.Graphics.DrawString("Ld", new Font("Calibri", 8), brush4, Distx * SX + 0.1f * SX, HeightPicture - YI - LuzLibreAux / 1.8f * SY + Traslapo__ * SY);
                }

                if (ConvecionAlzado.Tipo2 == "-")
                {
                    float Distx = 0.3f;
                    float Traslapo__ = 0.4f;
                    PointF pointI = new PointF(Distx * SX, HeightPicture - YI + 0.05f * Traslapo__ * SY);
                    PointF pointF = new PointF(Distx * SX, HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY + 1.2f * Traslapo__ * SY);

                    Pen BarraPen = new Pen(Color.FromArgb(32, 52, 110), 2);
                    e.Graphics.DrawLine(BarraPen, pointI, pointF);

                    SolidBrush brush4 = new SolidBrush(Color.FromArgb(186, 48, 48));

                    e.Graphics.DrawString("Ld", new Font("Calibri", 8), brush4, Distx * SX + 0.1f * SX, HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY + 0.9f * Traslapo__ * SY + (HeightPicture - YI - H_Viga * SY - LuzLibreAux * SY / 2 - (HeightPicture - YI - LuzLibreAux * SY - H_Viga * SY + Traslapo__ * SY)) / 2);
                    e.Graphics.DrawString("Ld + 40cm", new Font("Calibri", 8), brush4, Distx * SX + 0.1f * SX, HeightPicture - YI - LuzLibreAux / 2.2f * SY + Traslapo__ * SY);
                }
            }

            if (ConvecionAlzado.Tipo == "Botton" | ConvecionAlzado.UnitarioAdicional != null)
            {
                float eF = 0.5f; float Hacum = LuzLibreAux + H_Viga + eF;

                //Dibujar Losa de Fundación
                PointF[] PuntosFundacion = new PointF[] { new PointF(XI, HeightPicture - YI), new PointF(B * SX, HeightPicture - YI), new PointF(B * SX, HeightPicture - YI - eF * SY), new PointF(XI, HeightPicture - YI - eF * SY) };

                SolidBrush brush = new SolidBrush(Color.FromArgb(129, 111, 83));

                e.Graphics.FillPolygon(brush, PuntosFundacion);
                e.Graphics.DrawPolygon(new Pen(Color.Black), PuntosFundacion);

                //Dibujar Entre Piso

                PointF[] PuntosStory = new PointF[] { new PointF(B*SX,HeightPicture-YI+(-Hacum+H_Viga)*SY), new PointF(B*SX,HeightPicture-YI+(-Hacum+LuzLibreAux+H_Viga)*SY),
                                                       new PointF(XI,HeightPicture-YI+(-Hacum+LuzLibreAux+H_Viga)*SY),new PointF(XI,HeightPicture-YI+(-Hacum+H_Viga)*SY)};

                SolidBrush brush2 = new SolidBrush(Color.FromArgb(169, 172, 173));

                e.Graphics.FillPolygon(brush2, PuntosStory);
                e.Graphics.DrawPolygon(new Pen(Color.Black), PuntosStory);

                //Dibujar Entre Piso 2

                PointF EntrePiso2_Linea1 = new PointF(XI, HeightPicture - Hacum * SY);
                PointF EntrePiso2_Linea1_1 = new PointF(XI, HeightPicture + (-Hacum - LuzLibreAux / 2) * SY);
                Pen Puntuda = new Pen(Color.Black, 2);
                Puntuda.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                PointF EntrePiso2_Linea2 = new PointF(B * SX, HeightPicture - Hacum * SY);
                PointF EntrePiso2_Linea2_1 = new PointF(B * SX, HeightPicture + (-Hacum - LuzLibreAux / 2) * SY);
                Puntuda.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                PointF[] pointFs1 = new PointF[] { EntrePiso2_Linea1, EntrePiso2_Linea1_1, EntrePiso2_Linea2_1, EntrePiso2_Linea2 };
                e.Graphics.FillPolygon(brush2, pointFs1);
                e.Graphics.DrawLine(Puntuda, EntrePiso2_Linea1, EntrePiso2_Linea1_1);
                e.Graphics.DrawLine(Puntuda, EntrePiso2_Linea2, EntrePiso2_Linea2_1);

                //Dibujar Viga

                PointF[] PuntosViga = new PointF[] { new PointF(B*SX,HeightPicture-YI+(-Hacum)*SY), new PointF(B*SX,HeightPicture-YI+(-Hacum+H_Viga)*SY),
                                                       new PointF(XI,HeightPicture-YI+(-Hacum+H_Viga)*SY),new PointF(XI,HeightPicture-YI+(-Hacum)*SY)};

                SolidBrush brush3 = new SolidBrush(Color.FromArgb(198, 198, 198));

                e.Graphics.FillPolygon(brush3, PuntosViga);
                e.Graphics.DrawPolygon(new Pen(Color.Black), PuntosViga);

                Pen BarraPen = new Pen(Color.FromArgb(32, 52, 110), 2);

                if (ConvecionAlzado.Tipo == "Botton")
                {
                    float r = 0.08f; float Gancho = 0.15f; float DistX = 0.3f;
                    float Traslapo = 0.7f; float LdAd = 0.4f;

                    PointF[] PointBarras = new PointF[] { new PointF(DistX*SX+Gancho*SX,HeightPicture-YI-r*SY),
                                                          new PointF( DistX * SX, HeightPicture-YI - r * SY),
                                                          new PointF( DistX * SX, HeightPicture-YI - eF * SY-LdAd*SY-Traslapo*SY)};

                    e.Graphics.DrawLines(BarraPen, PointBarras);

                    SolidBrush brush4 = new SolidBrush(Color.FromArgb(186, 48, 48));

                    e.Graphics.DrawString("Ld + 40cm", new Font("Calibri", 8), brush4, DistX * SX + 0.1f * SX, HeightPicture - eF * SY - 0.6f * SY - YI);
                }
                if (ConvecionAlzado.Tipo == "A")
                {
                    float r = 0.08f; float Gancho = 0.15f; float DistX = 0.3f;
                    float Traslapo = 0.5f; float LdAd = 0.4f;

                    PointF[] PointBarras = new PointF[] { new PointF(DistX*SX+Gancho*SX,HeightPicture-YI-r*SY),
                                                          new PointF( DistX * SX, HeightPicture-YI - r * SY),
                                                          new PointF( DistX * SX, HeightPicture-YI - eF * SY-LdAd*SY-Traslapo*SY)};

                    e.Graphics.DrawLines(BarraPen, PointBarras);

                    SolidBrush brush4 = new SolidBrush(Color.FromArgb(186, 48, 48));

                    e.Graphics.DrawString("Ld + 40cm", new Font("Calibri", 8), brush4, DistX * SX + 0.1f * SX, HeightPicture - eF * SY - 0.6f * SY - YI);

                    PointF[] PointBarra2 = new PointF[] { new PointF(DistX*SX,HeightPicture-Hacum*SY-LdAd*SY-Traslapo*SY-YI),
                                                          new PointF( DistX * SX, HeightPicture-Hacum*SY+LdAd*SY+Traslapo*SY+H_Viga*SY-YI),};

                    e.Graphics.DrawLines(BarraPen, PointBarra2);

                    e.Graphics.DrawString("Ld + 40cm", new Font("Calibri", 8), brush4, DistX * SX + 0.1f * SX, HeightPicture - YI - Hacum * SY - 0.4f * SY);

                    e.Graphics.DrawString("Ld + 40cm", new Font("Calibri", 8), brush4, DistX * SX + 0.1f * SX, HeightPicture - YI - Hacum * SY + H_Viga * SY + 0.4f * SY);
                }
            }
        }

        private object[] ClasificarCelda(string Celda)
        {
            object[] Clasf = new object[] { "Error" };

            if (Celda.Contains("#"))
            {
                int CantidadBarras = 0;
                int NoBarra = 0;
                string Traslap = "";
                int CantidadBarrasA = 0;
                int NoBarraA = 0;
                string Raya = "";
                string Tipo2 = "";

                int VecesNumeral = 0;
                for (int i = 0; i < Celda.Length; i++)
                {
                    if (Celda.Substring(i, 1) == "#" && VecesNumeral < 1)
                    {
                        VecesNumeral += 1;
                        try
                        {
                            CantidadBarras = Convert.ToInt32(Celda.Substring(0, i));
                            if (Celda.Substring(0, 1) == "-" | Celda.Substring(0, 1) == "+")
                            {
                                Tipo2 = Celda.Substring(0, 1);
                            }
                        }
                        catch
                        {
                            CantidadBarras = 0;
                        }
                        try
                        {
                            NoBarra = Convert.ToInt32(Celda.Substring(i + 1, 2));
                        }
                        catch
                        {
                            try
                            {
                                NoBarra = Convert.ToInt32(Celda.Substring(i + 1, 1));
                            }
                            catch { NoBarra = 0; }
                        }
                    }
                    if (Celda.Substring(i, 1) == "T")
                    {
                        Traslap = Celda.Substring(i);
                    }

                    if (Celda.Substring(i, 1) == "A")
                    {
                        Traslap = Celda.Substring(i, 1);

                        string AuxAd = Celda.Substring(i);

                        if (AuxAd.Contains("-"))
                        {
                            Raya = "-";
                            for (int j = 0; j < AuxAd.Length; j++)
                            {
                                if (AuxAd.Substring(j, 1) == "#")
                                {
                                    int InicioRaya = AuxAd.IndexOf("-") + 1;

                                    CantidadBarrasA = Convert.ToInt32(AuxAd.Substring(InicioRaya, j - InicioRaya));
                                    try
                                    {
                                        NoBarraA = Convert.ToInt32(AuxAd.Substring(j + 1, 2));
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            NoBarraA = Convert.ToInt32(AuxAd.Substring(j + 1, 1));
                                        }
                                        catch { NoBarraA = 0; }
                                    }
                                }
                            }
                        }
                    }
                }
                try
                {
                    var o = Form1.Proyecto_.AceroBarras[NoBarra];
                    if (Raya == "-") { var m = Form1.Proyecto_.AceroBarras[NoBarraA]; }
                    if (CantidadBarras < 0)
                    {
                        CantidadBarras = -CantidadBarras;
                        if (Traslap != "A")
                        {
                            Traslap = "Botton";
                            Tipo2 = "";
                        }
                        else
                        {
                            Traslap = "A";
                        }
                    }
                    Clasf = new object[] { "Ok", CantidadBarras, NoBarra, Traslap, Raya, CantidadBarrasA, NoBarraA, Tipo2 };
                }
                catch
                {
                }
            }

            return Clasf;
        }

        private void CantBarras_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(CantBarras.Text, out int CantBarras_i) && Int32.TryParse(NoBarra.Text, out int NoBarra_i))
            {
                LlenarConvencionesList(CantBarras_i, NoBarra_i);
            }
        }

        private void LlenarConvencionesList(int CantBarras_i, int NoBarra_i)
        {
            ConvecionesRefuerzoAdicional.Items.Clear();
            ConvecionesRefuerzoAdicional.Items.Add(CantBarras_i + "#" + NoBarra_i + "A");
            ConvecionesRefuerzoAdicional.Items.Add("+" + CantBarras_i + "#" + NoBarra_i + "A");
            ConvecionesRefuerzoAdicional.Items.Add("-" + CantBarras_i + "#" + NoBarra_i + "A");
            ConvecionesRefuerzoAdicional.Items.Add("-" + CantBarras_i + "#" + NoBarra_i);
            ConvecionesRefuerzoAdicional.Items.Add(CantBarras_i + "#" + NoBarra_i + "A" + "-" + CantBarras_i + "#" + NoBarra_i);
        }

        private void NoBarra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(CantBarras.Text, out int CantBarras_i) && Int32.TryParse(NoBarra.Text, out int NoBarra_i))
            {
                LlenarConvencionesList(CantBarras_i, NoBarra_i);
            }
        }

        private void ConvecionesRefuerzoAdicional_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Nomenclatura = ConvecionesRefuerzoAdicional.SelectedItem.ToString();
            ClasificarConvenciones(Nomenclatura);
            DrawAyuda.Invalidate();
            textBox1.Text = Nomenclatura;
            string TextoPanel = "";
            Size sizePanel = new Size(10, 10);
            TextoEnPanel(ref TextoPanel, ref sizePanel);
            Nom.Size = sizePanel;
            TextNomPanel.Text = TextoPanel;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextoEnPanel(ref string Text, ref Size size)
        {
            if (ConvecionAlzado.Tipo == "A" && ConvecionAlzado.UnitarioAdicional == null)
            {
                if (ConvecionAlzado.Tipo2 == "")
                {
                    Text = $"Aporta acero en el Top del piso en el que está{Environment.NewLine}posicionado y el Bottom del piso subsiguiente.";
                    size = new Size(278, 42);
                }
                if (ConvecionAlzado.Tipo2 == "+")
                {
                    Text = $"Aporta acero en el Top del piso en el que está{Environment.NewLine}posicionado y el Bottom del piso subsiguiente.";
                    size = new Size(278, 39);
                }

                if (ConvecionAlzado.Tipo2 == "-")
                {
                    Text = $"Aporta acero en el Top del piso en el que está{Environment.NewLine}posicionado.";
                    size = new Size(268, 39);
                }
            }

            if (ConvecionAlzado.Tipo == "Botton" | ConvecionAlzado.UnitarioAdicional != null)
            {
                if (ConvecionAlzado.Tipo == "Botton")
                {
                    Text = $"Aporta acero en el Bottom del piso en el que está{Environment.NewLine}posicionado (Nomenclatura recomendada solo para{Environment.NewLine}el primer piso)";
                    size = new Size(299, 54);
                }
                if (ConvecionAlzado.Tipo == "A")
                {
                    Text = $"Aporta acero en el Top del piso en el que está posicionado,{Environment.NewLine}el Bottom del piso subsiguiente y el Buttom del piso en el que{Environment.NewLine}está posicionado.";
                    size = new Size(353, 56);
                }
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Nom.Location = new Point(e.X + pictureBox1.Location.X + 10, e.Y + pictureBox1.Location.Y + 10);
            Nom.Visible = true;
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Nom.Visible = false;
        }

        private void Cb_Aceptar_Click(object sender, EventArgs e)
        {
            if (ConvecionAlzado != null && textBox1.Text != "")
            {
                string nomenclatura = textBox1.Text;
                Columna col = Form1.Proyecto_.ColumnaSelect;

                col.CrearAlzado(AlzadoCorrespondiente, RowCorrespondiente, col, nomenclatura);
                col.ModificarTraslapo(AlzadoCorrespondiente, col);
                col.DeterminarCoordAlzado(AlzadoCorrespondiente, col);
                col.ActualizarRefuerzo();
                col.CalcularPesoAcero(AlzadoCorrespondiente);
                Form1.mAgregarAlzado.D_Alzado.Rows[RowCorrespondiente].Cells[AlzadoCorrespondiente + 1].Value = nomenclatura;
                Form1.m_Informacion.MostrarAcero();
                Form1.m_Despiece.Draw_Colum_Alzado.Invalidate();
                Form1.m_Despiece.Draw_Column.Invalidate();
                Form1.mAgregarAlzado.D_Alzado.RefreshEdit();
            }
        }

        #region Sombra Formulario

        //SOMBRA
        private const int WM_NCHITTEST = 0x84;

        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private bool m_aeroEnabled;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
         );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;

                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;
        }

        #endregion Sombra Formulario

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void AyudaAgregarRefuerzoAdicional_Paint(object sender, PaintEventArgs e)
        {
            PisoAagregar.Text = PisoCorrespondiente;
            AlzadoAagregar.Text = (AlzadoCorrespondiente + 1).ToString();
            Text = $"Refuerzo Adicional – {PisoCorrespondiente} - Alzado: {AlzadoCorrespondiente + 1}";
        }

        private void AyudaAgregarRefuerzoAdicional_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void ConvecionesRefuerzoAdicional_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}