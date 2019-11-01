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
