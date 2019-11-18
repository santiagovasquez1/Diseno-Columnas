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
        }
    }
}