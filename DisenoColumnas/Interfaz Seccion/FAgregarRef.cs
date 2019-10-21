using DisenoColumnas.Clases;
using DisenoColumnas.Utilidades;
using System;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class FAgregarRef : Form
    {
        private static FInterfaz_Seccion FInterfaz_ { get; set; } = new FInterfaz_Seccion();
        private Seccion Seccion { get; set; }


        public FAgregarRef(Seccion pseccion)
        {
            Seccion = pseccion;
            InitializeComponent();
        }

        private void FAgregarRef_Load(object sender, EventArgs e)
        {
            int CX, Cy;

            if (nuCX.Value < 2) nuCX.Value = 2;
            if (nuCY.Value < 2) nuCY.Value = 2;

            CX = Convert.ToInt32(nuCX.Value);
            Cy = Convert.ToInt32(nuCY.Value);
            Crear_tabla(CX, Cy, dataGridView1);
        }

        private void Button_Cerrar_Click(object sender, EventArgs e)
        {
            FInterfaz_.Over = false;
            FInterfaz_.Seleccionado = false;
            FInterfaz_.Invalidate();
            Close();
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

        private void Crear_tabla(int CapasX,int CapasY,DataGridView data)
        {
            int numero_barras = CapasY * 2 + (CapasX - 2);
            int id=1;

            double H = Seccion.H * 100;
            double b = Seccion.B * 100;
            double r = Form1.Proyecto_.R;

            data.Rows.Clear();
            data.Rows.Add(numero_barras);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i].Cells[0].Value = id;
                data.Rows[i].Cells[1].Value = "#4";

                id++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CRefuerzo refuerzo;
            DataGridViewComboBoxCell boxCell;
            string diametro;
            int id;
            double x, y;
            double[] coord;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                boxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[1];
                id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                diametro = boxCell.Value.ToString();
                x = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                y = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                coord = new double[] { x, y };

                refuerzo = new CRefuerzo(id, diametro, coord, TipodeRefuerzo.longitudinal);
                Seccion.Refuerzos.Add(refuerzo);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FInterfaz_.Over = false;
            FInterfaz_.Seleccionado = false;
            FInterfaz_.Invalidate();
            Close();
        }

        private void nuCX_ValueChanged(object sender, EventArgs e)
        {
            int CX, Cy;

            if (nuCX.Value < 2) nuCX.Value = 2;
            if (nuCY.Value < 2) nuCY.Value = 2;

            CX = Convert.ToInt32(nuCX.Value);
            Cy = Convert.ToInt32(nuCY.Value);
            Crear_tabla(CX, Cy, dataGridView1);
        }

        private void nuCY_ValueChanged(object sender, EventArgs e)
        {
            int CX, Cy;

            if (nuCX.Value < 2) nuCX.Value = 2;
            if (nuCY.Value < 2) nuCY.Value = 2;

            CX = Convert.ToInt32(nuCX.Value);
            Cy = Convert.ToInt32(nuCY.Value);
            Crear_tabla(CX, Cy, dataGridView1);
        }
    }
}