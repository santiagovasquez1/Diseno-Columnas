using DisenoColumnas.Clases;
using DisenoColumnas.Utilidades;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class FAgregarRef : Form
    {
        private static FInterfaz_Seccion FInterfaz_ { get; set; } = new FInterfaz_Seccion(pedicion:Tipo_Edicion.Secciones_modelo);
        private Seccion Seccion { get; set; }
        private string piso { get; set; }
        private int index { get; set; } = -1;

        public FAgregarRef(Seccion pseccion, string ppiso, FInterfaz_Seccion pInterfaz)
        {
            piso = ppiso;
            FInterfaz_ = pInterfaz;
            Seccion = pseccion;
            InitializeComponent();
        }

        private void FAgregarRef_Load(object sender, EventArgs e)
        {
            int CX, Cy, CXw, CYw;
            CX = 0; Cy = 0; CXw = 0; CYw = 0;

            if (Seccion.Refuerzos.Count == 0)
            {
                if (nuCX.Value < 2) nuCX.Value = 2;
                if (nuCY.Value < 2) nuCY.Value = 2;

                CX = Convert.ToInt32(nuCX.Value);
                Cy = Convert.ToInt32(nuCY.Value);

                if (Seccion.Shape == TipodeSeccion.Rectangular)
                {
                    groupBox2.Text = "Capas Sección";
                    groupBox3.Enabled = false;
                    nuCXw.Value = 0;
                    nuCYw.Value = 0;
                    CYw = 0;
                    CXw = 0;
                }

                if (Seccion.Shape == TipodeSeccion.Tee | Seccion.Shape == TipodeSeccion.L)
                {
                    groupBox3.Visible = true;
                    groupBox3.Enabled = true;
                    groupBox3.Text = "Capas Aleta";
                    groupBox2.Text = "Capas Alma";

                    if (nuCXw.Value < 2) nuCXw.Value = 2;
                    if (nuCYw.Value < 2) nuCYw.Value = 2;

                    CXw = Convert.ToInt32(nuCXw.Value);
                    CYw = Convert.ToInt32(nuCYw.Value);
                }
                Crear_tabla(CX, Cy, CXw, CYw, dataGridView1);
            }
            else
            {
                Cargar_Datos(dataGridView1);
                nuCX.Enabled = false;
                nuCY.Enabled = false;
                nuCXw.Enabled = false;
                nuCYw.Enabled = false;
            }
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

        private void Cargar_Datos(DataGridView data)
        {
            data.Rows.Clear();
            data.Rows.Add(Seccion.Refuerzos.Count);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i].Cells[0].Value = Seccion.Refuerzos[i].id;
                data.Rows[i].Cells[1].Value = Seccion.Refuerzos[i].Diametro;
                data.Rows[i].Cells[2].Value = Math.Round(Seccion.Refuerzos[i].Coord[0], 2);
                data.Rows[i].Cells[3].Value = Math.Round(Seccion.Refuerzos[i].Coord[1], 2);
            }
        }

        private void Crear_tabla(int CapasX, int CapasY, int CapasXw, int CapasYw, DataGridView data)
        {
            int numero_barras = CapasXw > 0 ? (CapasY + CapasYw) * 2 + (CapasX - 2) * 2 + (CapasXw - 2) * 2 : (CapasY) * 2 + (CapasX - 2) * 2;
            int id = 1;

            double H = Seccion.H * 100;
            double b = Seccion.B * 100;
            double Tw = Seccion.TW * 100;
            double tf = Seccion.TF * 100;
            double r = Form1.Proyecto_.R + 1; //1 es el espesor del estribo #3
            double posx, posy;
            double DeltaX1, DeltaY1, DeltaX2, DeltaY2;
            int ContX, ContY;

            data.Rows.Clear();
            data.Rows.Add(numero_barras);

            DeltaX1 = (b - 2 * r) / (CapasX - 1);
            DeltaY1 = (H - 2 * r) / (CapasY - 1);
            DeltaX2 = (Tw - 2 * r) / (CapasXw - 1);
            DeltaY2 = (tf - 2 * r) / (CapasYw - 1);

            posx = -(b / 2) + r; posy = (H / 2) - r;
            ContX = CapasX - 2; ContY = CapasY;

            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i].Cells[0].Value = id;
                data.Rows[i].Cells[1].Value = "#4";

                if (Seccion.Shape == TipodeSeccion.Rectangular)
                {
                    data.Rows[i].Cells[2].Value = Math.Round(posx, 2);
                    data.Rows[i].Cells[3].Value = Math.Round(posy, 2);

                    posy -= DeltaY1;
                    ContY--;

                    if (ContY == 0 & ContX > 0)
                    {
                        posx += DeltaX1;
                        posy = (H / 2) - r;
                        ContY = 2;
                        DeltaY1 = (H - 2 * r) / (ContY - 1);
                        ContX--;
                    }

                    if (ContX == 0 & ContY == 0)
                    {
                        ContY = CapasY;
                        DeltaY1 = (H - 2 * r) / (ContY - 1);
                        posx = (b / 2) - r;
                        posy = (H / 2) - r;
                    }
                }

                id++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reload_Seccion();
            FInterfaz_.Over = false;
            FInterfaz_.Seleccionado = false;
            FInterfaz_.Invalidate();
            Close();
        }

        private void Reload_Seccion()
        {
            CRefuerzo refuerzo;
            DataGridViewComboBoxCell boxCell;
            string diametro;
            int id, indice;
            double x, y;
            double[] coord;
            Seccion.Refuerzos.Clear();

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

            Seccion.Acero_Long = Seccion.Refuerzos.Sum(x1 => x1.As_Long);
            Seccion.Editado = true;

            if (FInterfaz_.edicion == Tipo_Edicion.Secciones_modelo)
            {
                indice = Form1.Proyecto_.ColumnaSelect.Seccions.FindIndex(x1 => x1.Item2 == piso);
                Form1.Proyecto_.ColumnaSelect.Seccions[indice] = new Tuple<Seccion, string>(Seccion, piso);
            }

            if (FInterfaz_.edicion == Tipo_Edicion.Secciones_predef)
            {
                indice = Form1.secciones_predef.Secciones.FindIndex(x1 => x1.ToString() == Seccion.ToString());
                Form1.secciones_predef.Secciones[indice] = Seccion;
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
            int CX, Cy, CXw, CYw;

            if (nuCX.Value < 2) nuCX.Value = 2;
            if (nuCY.Value < 2) nuCY.Value = 2;

            CX = Convert.ToInt32(nuCX.Value);
            Cy = Convert.ToInt32(nuCY.Value);
            CXw = Convert.ToInt32(nuCXw.Value);
            CYw = Convert.ToInt32(nuCYw.Value);

            Crear_tabla(CX, Cy, CXw, CYw, dataGridView1);
        }

        private void nuCY_ValueChanged(object sender, EventArgs e)
        {
            int CX, Cy, CXw, CYw;

            if (nuCX.Value < 2) nuCX.Value = 2;
            if (nuCY.Value < 2) nuCY.Value = 2;

            CX = Convert.ToInt32(nuCX.Value);
            Cy = Convert.ToInt32(nuCY.Value);
            CXw = Convert.ToInt32(nuCXw.Value);
            CYw = Convert.ToInt32(nuCYw.Value);

            Crear_tabla(CX, Cy, CXw, CYw, dataGridView1);
        }

        private void nuCXw_ValueChanged(object sender, EventArgs e)
        {
            int CX, Cy, CXw, CYw;

            if (nuCX.Value < 2) nuCX.Value = 2;
            if (nuCY.Value < 2) nuCY.Value = 2;

            if (Seccion.Shape == TipodeSeccion.L | Seccion.Shape == TipodeSeccion.Tee)
            {
                if (nuCXw.Value < 2) nuCXw.Value = 2;
                if (nuCYw.Value < 2) nuCY.Value = 2;
            }

            CX = Convert.ToInt32(nuCX.Value);
            Cy = Convert.ToInt32(nuCY.Value);
            CXw = Convert.ToInt32(nuCXw.Value);
            CYw = Convert.ToInt32(nuCYw.Value);

            Crear_tabla(CX, Cy, CXw, CYw, dataGridView1);
        }

        private void nuCYw_ValueChanged(object sender, EventArgs e)
        {
            int CX, Cy, CXw, CYw;

            if (nuCX.Value < 2) nuCX.Value = 2;
            if (nuCY.Value < 2) nuCY.Value = 2;

            if (Seccion.Shape == TipodeSeccion.L | Seccion.Shape == TipodeSeccion.Tee)
            {
                if (nuCXw.Value < 2) nuCXw.Value = 2;
                if (nuCYw.Value < 2) nuCY.Value = 2;
            }

            CX = Convert.ToInt32(nuCX.Value);
            Cy = Convert.ToInt32(nuCY.Value);
            CXw = Convert.ToInt32(nuCXw.Value);
            CYw = Convert.ToInt32(nuCYw.Value);

            Crear_tabla(CX, Cy, CXw, CYw, dataGridView1);
        }

        private void agregarRefuerzoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Tabla_madre = (DataGridView)cmEditar.SourceControl;
            int id,Last_index;
            
            Tabla_madre.Rows.Add();
            Last_index = Tabla_madre.Rows.Count - 1;
            id = Convert.ToInt32(Tabla_madre.Rows[Last_index - 1].Cells[0].Value) + 1;

            Tabla_madre.Rows[Last_index].Cells[0].Value=id;
            Tabla_madre.Rows[Last_index].Cells[1].Value = Seccion.Refuerzos.Last().Diametro;
        } 

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ContextMenuStrip = cmEditar;
                cmEditar.Enabled = true;
                index = e.RowIndex;
            }
            else
                index = -1;
        }

        private void eliminarRefuerzoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Tabla_madre = (DataGridView)cmEditar.SourceControl;
            Tabla_madre.Rows.RemoveAt(index);

            for(int i = index; i < Tabla_madre.RowCount; i++)
            {
                Tabla_madre.Rows[i].Cells[0].Value = i+1;
            }
        }
    }
}