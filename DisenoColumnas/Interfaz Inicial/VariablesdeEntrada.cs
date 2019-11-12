using DisenoColumnas.Clases;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Inicial
{
    public partial class VariablesdeEntrada : Form
    {
        public bool ProyectoPV = false;

        public VariablesdeEntrada(bool ProyectoPV_)
        {
            InitializeComponent();
            ProyectoPV = ProyectoPV_;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cb_Aceptar_Click(object sender, EventArgs e)
        {
            if (Radio_Des.Checked | Radio_Dmo.Checked)
            {
                float r;
                bool IsNumeric = Single.TryParse(T_Vf.Text, out r);

                if (T_Vf.Text == "" | IsNumeric == false | r == 0)
                {
                    MessageBox.Show("El espesor asignado en la fundación es incorrecto.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Form1.Proyecto_.Nivel_Fundacion = Convert.ToSingle(T_arranque.Text);
                    if (Convert.ToSingle(T_Vf.Text) != Form1.Proyecto_.e_Fundacion)
                    {
                        Form1.Proyecto_.e_Fundacion = Convert.ToSingle(T_Vf.Text);
                        if (ProyectoPV)
                        {
                            MessageBox.Show("Debido al cambio realizado deberá volver a diseñar las Columnas.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    Form1.Proyecto_.R = Convert.ToSingle(R_Box.Text);
                    Form1.Proyecto_.FY = Convert.ToSingle(Fy_Box.Text);

                    Form1.Proyecto_.AlturaEdificio_();
                    if (Radio_Des.Checked)
                    {
                        Form1.Proyecto_.DMO_DES = GDE.DES;
                    }
                    else
                    {
                        Form1.Proyecto_.DMO_DES = GDE.DMO;
                    }

                    //CalcularAlturaAcumulada
                    foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
                    {
                        columna.LuzAcum = new System.Collections.Generic.List<float>();
                        float DisAcum = Form1.Proyecto_.e_Fundacion;
                        for (int i = columna.LuzLibre.Count - 1; i >= 0; i--) { columna.LuzAcum.Add(0); }

                        for (int i = columna.LuzLibre.Count - 1; i >= 0; i--)
                        {
                            DisAcum += columna.LuzLibre[i] + columna.VigaMayor.Seccions[i].Item1.H;
                            columna.LuzAcum[i] = DisAcum;
                        }
                    }

                    Close();
                }
            }
            else
            {
                MessageBox.Show("Defina el grado de disipación de la estructura.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void VariablesdeEntrada_Load(object sender, EventArgs e)
        {
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            PictureBox1.BackColor = Color.Transparent;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox1.BackColor = Color.White;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}