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
    public partial class FAgregarRefMultiple : Form
    {
        public static FAgregarRefMultiple FormuAgregarMultipe { get; set; }

        public static float Xii { get; set; }
        public static float Yii { get; set; }
        public static float Xff { get; set; }
        public static float Yff { get; set; }
        public static float Separacion { get; set; }
        public static string Diametro { get; set; }
        public static bool Close1 { get; set; } = false;
        public float Dx { get; set; }
        public float Dy { get; set; }
        public double EscalaX { get; set; }
        public double EscalaY { get; set; }

        public FAgregarRefMultiple()
        {
            InitializeComponent();
            cbDiametros.SelectedItem = cbDiametros.Items[0];
            FormuAgregarMultipe = this;
        }

        private void BAceptar_Click(object sender, EventArgs e)
        {
            float xii;
            float yii; float xff;
            float yff;
            float ss;
            if (Single.TryParse(Xi.Text, out xii) && Single.TryParse(Xf.Text, out xff)
                && Single.TryParse(Yi.Text, out yii) && Single.TryParse(Yf.Text, out yff) && cbDiametros.Text!="" && Single.TryParse(Se.Text, out ss))
            {

                Xii = xii;
                Yii = yii;
                Xff = xff;
                Yff = yff;
                Diametro = cbDiametros.Text;
                Separacion = ss;
                Close1 = false;
                Close();
            }


        }

        private void BCancelar_Click(object sender, EventArgs e)
        {
            Close1 = true;
            Close();
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }


        private void CalcularCantidad()
        {
            float xii;
            float yii; float xff;
            float yff;
            float ss;
            if (Single.TryParse(Xi.Text, out xii) && Single.TryParse(Xf.Text, out xff)
                && Single.TryParse(Yi.Text, out yii) && Single.TryParse(Yf.Text, out yff) && cbDiametros.Text != "" && Single.TryParse(Se.Text, out ss))
            {

                Xii = xii;
                Yii = yii;
                Xff = xff;
                Yff = yff;
                Diametro = cbDiametros.Text;
                Separacion = ss;
                float x = Xii + (Dx / (float)EscalaX);
                float y = Yii - (Dy / (float)EscalaY);
                float xf = Xff + (Dx / (float)EscalaX);
                float yf = Yff - (Dy / (float)EscalaY);
                float DistanciaDiagonal = FunctionsProject.DistanciaEntrePuntos(x, y, xf, yf);
                int CantBarras = (int)Math.Round(DistanciaDiagonal / Separacion);
                CantBarrasBox.Text = (CantBarras+1).ToString();

            }

        }
        private void DeterminarSeparacion()
        {
            float xii;
            float yii; float xff;
            float yff;
            int CantBarras;
            if (Single.TryParse(Xi.Text, out xii) && Single.TryParse(Xf.Text, out xff)
                && Single.TryParse(Yi.Text, out yii) && Single.TryParse(Yf.Text, out yff) && cbDiametros.Text != "" 
                && Int32.TryParse(CantBarrasBox.Text, out CantBarras))
            {

                Xii = xii;
                Yii = yii;
                Xff = xff;
                Yff = yff;
                float x = Xii + (Dx / (float)EscalaX);
                float y = Yii - (Dy / (float)EscalaY);
                float xf = Xff + (Dx / (float)EscalaX);
                float yf = Yff - (Dy / (float)EscalaY);
                float DistanciaDiagonal = FunctionsProject.DistanciaEntrePuntos(x, y, xf, yf);
                Separacion = DistanciaDiagonal / (CantBarras-1);
                Se.Text = (String.Format("{0:0.00}", Separacion).ToString());

            }
        }


        private void Se_TextChanged(object sender, EventArgs e)
        {
            if (SepRadio.Checked)
            {
                CalcularCantidad();
            }
        }

        private void CantBarrasBox_TextChanged(object sender, EventArgs e)
        {
            if (CantRad.Checked)
            {
                DeterminarSeparacion();
            }
        }

        private void SepRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (SepRadio.Checked)
            {
                CantBarrasBox.Enabled = false;
                Se.Enabled = true;
                float SeAnterior = Convert.ToSingle(Se.Text);
                Se.Text = 0.ToString();
                Se.Text = SeAnterior.ToString();
            }
            else
            {
                CantBarrasBox.Enabled = true;
                Se.Enabled = false;
                int CantAnterior = Convert.ToInt32(CantBarrasBox.Text);
                CantBarrasBox.Text = 0.ToString();
                CantBarrasBox.Text = CantAnterior.ToString();

            }
       }

        private void FAgregarRefMultiple_Load(object sender, EventArgs e)
        {
           
            SepRadio.Checked = true;
            Se.Text = "10";
        }
    }
}
