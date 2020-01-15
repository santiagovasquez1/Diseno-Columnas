using DisenoColumnas.Clases;
using DisenoColumnas.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class FAgregarRefMultipleCuadro : Form
    {
        public static FAgregarRefMultipleCuadro FormuAgregarMultipe { get; set; }

        public static bool Close1 { get; set; } = false;
        public float Dx { get; set; }
        public float Dy { get; set; }
        public double EscalaX { get; set; }
        public double EscalaY { get; set; }
        public static string Diametro { get; set; }

        public static cRefuerzoCuadro RefuerzoCuadro { get; set; } = new cRefuerzoCuadro();


        public FAgregarRefMultipleCuadro()
        {
            InitializeComponent();
            FormuAgregarMultipe = this;
            cbDiametros.SelectedItem = cbDiametros.Items[0];
        }

        private void bAceptar_Click(object sender, EventArgs e)
        {
            Diametro = cbDiametros.Text;
            Close1 = false;
            Close();
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Close1 = true;
            Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void FAgregarRefMultipleCuadro_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = RefuerzoCuadro;
        }


        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
          
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                if (e.ChangedItem.PropertyDescriptor.Name.Contains("SMax_Horizontal"))
                {
                    RefuerzoCuadro.HallarCantidadHorizontal();
                }
                if (e.ChangedItem.PropertyDescriptor.Name.Contains("SMax_Vertical"))
                {
                    RefuerzoCuadro.HallarCantidadVertical();
                    propertyGrid1.Refresh();
                }


            }
            catch { }
        }
    }
}
