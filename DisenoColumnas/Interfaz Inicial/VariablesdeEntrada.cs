using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Form1.Proyecto_.Nivel_Fundacion = Convert.ToSingle(T_arranque.Text);
            Form1.Proyecto_.e_Fundacion = Convert.ToSingle(T_Vf.Text);
            Form1.Proyecto_.AlturaEdificio_();
            Close();
        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }
}
