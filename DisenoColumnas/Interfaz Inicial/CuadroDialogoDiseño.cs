using System;
using System.Drawing;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Inicial
{
    public partial class CuadroDialogoDiseño : Form
    {
        public CuadroDialogoDiseño()
        {
            TransparencyKey = Color.Crimson;
            BackColor = Color.Crimson;
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CuadroDialogoDiseño_Load(object sender, EventArgs e)
        {
        }
    }
}