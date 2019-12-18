using DisenoColumnas.Clases;
using DisenoColumnas.Secciones;
using DisenoColumnas.Utilidades;
using System;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class FEditarRef : Form
    {
        private static FInterfaz_Seccion FInterfaz_ { get; set; } = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
        private ISeccion Seccion { get; set; }
        private GDE GDE { get; set; }
        private string piso { get; set; }
        private int index { get; set; } = -1;

        public FEditarRef(ISeccion pseccion, string ppiso, int pindice, FInterfaz_Seccion pInterfaz)
        {
            piso = ppiso;
            Seccion = pseccion;
            FInterfaz_ = pInterfaz;
            index = pindice;
            InitializeComponent();
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FEditarRef_Load(object sender, EventArgs e)
        {
            ID_Ref.Text = Convert.ToString(Seccion.Refuerzos[index].id);
            cbDiametros.Text = Seccion.Refuerzos[index].Diametro;
            tbAlzado.Text= Seccion.Refuerzos[index].Alzado.ToString();
            tbXc.Text = $"{Math.Round(Seccion.Refuerzos[index].Coord[0], 2)}";
            tbYc.Text = $"{Math.Round(Seccion.Refuerzos[index].Coord[1], 2)}";
        }

        private void Reload_Seccion()
        {
            CRefuerzo refuerzo;
            string diametro;
            int Alzado = 0;
            double x, y;
            double[] coord;
            int indice = 0;

            diametro = cbDiametros.Text;
            Alzado = Convert.ToInt32(tbAlzado.Text);
            x = Convert.ToDouble(tbXc.Text);
            y = Convert.ToDouble(tbYc.Text);
            coord = new double[] { x, y };

            refuerzo = new CRefuerzo(Seccion.Refuerzos[index].id, diametro, coord, TipodeRefuerzo.longitudinal);
            refuerzo.Alzado = Alzado;
            Seccion.Refuerzos[index] = FunctionsProject.DeepClone(refuerzo);
            Seccion.Editado = true;

            if (FInterfaz_.edicion == Tipo_Edicion.Secciones_modelo)
            {
                indice = Form1.Proyecto_.ColumnaSelect.Seccions.FindIndex(x1 => x1.Item2 == piso);
                Form1.Proyecto_.ColumnaSelect.Seccions[indice] = new Tuple<ISeccion, string>(Seccion, piso);
            }

            if (FInterfaz_.edicion == Tipo_Edicion.Secciones_predef & GDE==GDE.DMO)
            {
                indice = Form1.secciones_predef.Secciones_DMO.FindIndex(x1 => x1.ToString() == Seccion.ToString());
                Form1.secciones_predef.Secciones_DMO[indice] = Seccion;
            }

            if (FInterfaz_.edicion == Tipo_Edicion.Secciones_predef & GDE == GDE.DES)
            {
                indice = Form1.secciones_predef.Secciones_DES.FindIndex(x1 => x1.ToString() == Seccion.ToString());
                Form1.secciones_predef.Secciones_DES[indice] = Seccion;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FEditarRef_MouseMove(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label6_MouseMove(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label8_MouseMove(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bAceptar_Click(object sender, EventArgs e)
        {
            Reload_Seccion();
            Close();
        }
    }
}