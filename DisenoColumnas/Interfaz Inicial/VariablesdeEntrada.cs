using System;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Inicial
{
    public partial class VariablesdeEntrada : Form
    {
        public VariablesdeEntrada()
        {
            InitializeComponent();
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
                    Form1.Proyecto_.e_Fundacion = Convert.ToSingle(T_Vf.Text);
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


    }
}
