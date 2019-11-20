using DisenoColumnas.Clases;
using DisenoColumnas.Secciones;
using DisenoColumnas.Utilidades;
using System;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class FAgregarSeccion : Form
    {
        public FAgregarSeccion()
        {
            InitializeComponent();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Label6_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Button_Cerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FAgregarSeccion_Load(object sender, EventArgs e)
        {
            cbSeccion.Items.Clear();
            cbSeccion.Items.AddRange(new string[] { TipodeSeccion.Rectangular.ToString(), TipodeSeccion.Circle.ToString(), TipodeSeccion.L.ToString(), TipodeSeccion.Tee.ToString() });
            cbSeccion.Text = cbSeccion.Items[0].ToString();
        }

        private void cbSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSeccion.Text == TipodeSeccion.Rectangular.ToString())
            {
                LAncho.Text = "B (cm) :";
                LAncho.Update();
                tbAlto.Enabled = true;
                tbTf.Enabled = false;
                tbTw.Enabled = false;

                tbTf.Text = "0";
                tbTw.Text = "0";
            }

            if (cbSeccion.Text == TipodeSeccion.Tee.ToString() | cbSeccion.Text == TipodeSeccion.L.ToString())
            {
                LAncho.Text = "B (cm) :";
                LAncho.Update();
                tbAlto.Enabled = true;
                tbTf.Enabled = true;
                tbTw.Enabled = true;
            }

            if (cbSeccion.Text == TipodeSeccion.Circle.ToString())
            {
                LAncho.Text = "D (cm) :";
                LAncho.Update();
                tbAlto.Enabled = false;
                tbTf.Enabled = false;
                tbTw.Enabled = false;

                tbAlto.Text = "0";
                tbTf.Text = "0";
                tbTw.Text = "0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Tipo_seccion;
            float b, h, tw, tf, r;

            Tipo_seccion = cbSeccion.Text;
            b = Convert.ToSingle(tbAncho.Text);
            h = Convert.ToSingle(tbAlto.Text);
            tw = Convert.ToSingle(tbTw.Text);
            tf = Convert.ToSingle(tbTf.Text);
            r = Convert.ToSingle(tb_r.Text);

            Crear_Seccion(Tipo_seccion, b, h, tw, tf, r);
            Close();
        }

        private void Crear_Seccion(string Tipo_Seccion, float b, float h, float tw, float tf, float r)
        {
            ISeccion N_Seccion = null;
            string Nombre_Seccion = "";
            GDE gde = GDE.DMO;
            float FD1 = 0;float FD2 = 0;
            MAT_CONCRETE material = new MAT_CONCRETE()
            {
                Name = "H" + tbFc.Text,
                FC = Convert.ToSingle(tbFc.Text)
            };

            if (Radio_Dmo.Checked)
            {
                gde = GDE.DMO;
                FD1 = 0.20f;
                FD2 = 0.06f;
            }

            if (Radio_Des.Checked)
            {
                gde = GDE.DES;
                FD1 = 0.30f;
                FD2 = 0.09f;
            }

            if (Tipo_Seccion == TipodeSeccion.Rectangular.ToString())
            {
                Nombre_Seccion = $"C{b}X{h}{material.Name}";
                N_Seccion = new CRectangulo(Nombre_Seccion, b / 100, h / 100, material, TipodeSeccion.Rectangular, null);
                N_Seccion.Calc_vol_inex(r / 100, 4220, gde);
                N_Seccion.Cuanti_Vol(FD1, FD2, r / 100, 4220);
                N_Seccion.Refuerzo_Base(r);
            }

            if (Tipo_Seccion == TipodeSeccion.Circle.ToString())
            {
                Nombre_Seccion = $"C{b}{material.Name}";
                N_Seccion = new CCirculo(Nombre_Seccion, b / 200, new double[] { 0, 0 }, material, TipodeSeccion.Circle, null);
                N_Seccion.Calc_vol_inex(r / 100, 4220, gde);
            }

            if (Tipo_Seccion == TipodeSeccion.Tee.ToString() | Tipo_Seccion == TipodeSeccion.L.ToString())
            {
                Nombre_Seccion = $"C{b}X{h}X{tw}X{tf}{Tipo_Seccion}{material.Name}";

                if (Tipo_Seccion == TipodeSeccion.Tee.ToString())
                    N_Seccion = new CSD(Nombre_Seccion, b / 100, h / 100, tw / 100, tf / 100, material, TipodeSeccion.Tee, null);
                if (Tipo_Seccion == TipodeSeccion.L.ToString())
                    N_Seccion = new CSD(Nombre_Seccion, b / 100, h / 100, tw / 100, tf / 100, material, TipodeSeccion.L, null);

                N_Seccion.Calc_vol_inex(r / 100, 4220, gde);
            }

            if (N_Seccion != null)
            {
                if (Radio_Dmo.Checked)
                {
                    if (Form1.secciones_predef.Secciones_DMO.Exists(x => x.Equals(N_Seccion)) == false)
                    {
                        Form1.secciones_predef.Secciones_DMO.Add(N_Seccion);
                    }
                }
                if (Radio_Des.Checked)
                {
                    if (Form1.secciones_predef.Secciones_DES.Exists(x => x.Equals(N_Seccion)) == false)
                    {
                        Form1.secciones_predef.Secciones_DES.Add(N_Seccion);
                    }
                }
            }
        }
    }
}