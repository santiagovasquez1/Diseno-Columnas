using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Inicial
{
    public partial class ChequeoDeCargas : Form
    {
        public ChequeoDeCargas()
        {
            InitializeComponent();
        }

        private void ChequeoDeCargas_Load(object sender, EventArgs e)
        {
            Size = new Size(340, 246);
            Location = new Point(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);
            CargarCombiaciones();

        }

        private void CargarCombiaciones()
        {
            List<string> AllCombinaciones = Form1.Proyecto_.Lista_Columnas[0].resultadosETABs[0].Load;

            List<string> Combinaciones = AllCombinaciones.Distinct().ToList();

            CASOSCARGA.Items.AddRange(Combinaciones.ToArray());

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            List<string> CargasAnalizar = new List<string>();
            for (int i = 0; i < CASOSCARGA.SelectedItems.Count; i++)
            {
                CargasAnalizar.Add(CASOSCARGA.SelectedItems[i].ToString());
            }
            if (CargasAnalizar.Count != 0)
            {
                string[] ColNames = Form1.Proyecto_.Lista_Columnas.Select(x => x.Name).ToArray();
                Columnas_List.Items.AddRange(ColNames);
                foreach (Columna col in Form1.Proyecto_.Lista_Columnas)
                {

                    col.Panalizar = new List<List<Tuple<float, string, string, float>>>();

                    for (int i = col.resultadosETABs.Count - 1; i >= 0; i--)
                    {
                        float Ag = (float)col.Seccions[i].Item1.Area * 10000;
                        float fc = col.Seccions[i].Item1.Material.FC;
                        float Factor = 0.4f * Ag * fc;
                        List<Tuple<float, string, string, float>> PqCumplen = new List<Tuple<float, string, string, float>>();
                        for (int j = 0; j < col.resultadosETABs[i].Load.Count; j++)
                        {

                            if (CargasAnalizar.Contains(col.resultadosETABs[i].Load[j]))
                            {
                                Tuple<float, string, string, float> tupleAux = new Tuple<float, string, string, float>(col.resultadosETABs[i].P[j], col.resultadosETABs[i].Load[j], col.Seccions[i].Item2, Factor);
                                PqCumplen.Add(tupleAux);
                            }

                        }
                        col.Panalizar.Add(PqCumplen);
                    }

                }

                Location = new Point(Location.X - Width / 2, Location.Y - Height / 2);
                Size = new Size(760, 620);
                Analizar.Enabled = false;
                CASOSCARGA.Enabled = false;


                List<string> NamesColumnasQueNoCumplen = new List<string>();

                foreach (Columna col in Form1.Proyecto_.Lista_Columnas)
                {

                    for (int i = col.Seccions.Count - 1; i >= 0; i--)
                    {

                        for (int j = 0; j < col.Panalizar[i].Count; j++)
                        {
                            if (col.Panalizar[i][j].Item4 < (col.Panalizar[i][j].Item1) * 1000)
                            {
                                string ColumnaQueNoCumple = col.Name + " - " + col.Panalizar[i][j].Item3 + " - " + col.Panalizar[i][j].Item2;
                                NamesColumnasQueNoCumplen.Add(ColumnaQueNoCumple);
                                Button_OK.Enabled = false;
                                B_Salir.Enabled = true;
                            }

                        }


                    }

                }

                Columnas_List.SelectedItem = Columnas_List.Items[0];
                ReporteColumnas.Items.AddRange(NamesColumnasQueNoCumplen.ToArray());



            }
            else
            {
                return;
            }


        }

        private void Columnas_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateDataGridView();
        }





        private void CreateDataGridView()
        {

            if (Columnas_List.SelectedIndex != -1 && Columnas_List.Text != "")
            {


                DataInfo.Rows.Clear();


                Columna ColumnaSelect = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == Columnas_List.Text);

                for (int i = ColumnaSelect.Panalizar.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < ColumnaSelect.Panalizar[i].Count; j++)
                    {
                        DataGridViewCellStyle StyleR = new DataGridViewCellStyle();
                        StyleR.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        StyleR.Font = new Font("Vderdana", 8, FontStyle.Regular);
                        DataInfo.Rows.Add();
                        DataInfo.Rows[DataInfo.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Name;
                        DataInfo.Rows[DataInfo.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.ToString();
                        DataInfo.Rows[DataInfo.Rows.Count - 1].Cells[2].Value = ColumnaSelect.Seccions[i].Item2;
                        DataInfo.Rows[DataInfo.Rows.Count - 1].Cells[3].Value = ColumnaSelect.Panalizar[i][j].Item2;
                        DataInfo.Rows[DataInfo.Rows.Count - 1].Cells[4].Value = String.Format("{0:0.00}", ColumnaSelect.Panalizar[i][j].Item4 / 1000);
                        DataInfo.Rows[DataInfo.Rows.Count - 1].Cells[5].Value = String.Format("{0:0.00}", ColumnaSelect.Panalizar[i][j].Item1);
                        if (ColumnaSelect.Panalizar[i][j].Item4 < ColumnaSelect.Panalizar[i][j].Item1 * 1000)
                        {
                            StyleR.BackColor = Color.FromArgb(248, 134, 134);
                            StyleR.Font = new Font("Vderdana", 8, FontStyle.Bold);
                        }
                        else
                        {
                            StyleR.BackColor = Color.White;
                            StyleR.ForeColor = Color.Black;
                        }

                        DataInfo.Rows[DataInfo.Rows.Count - 1].DefaultCellStyle = StyleR;
                    }
                }


                EstiloDatGridView(DataInfo);

            }

        }


        private void EstiloDatGridView(DataGridView dataGrid)
        {
            DataGridViewCellStyle StyleC = new DataGridViewCellStyle();
            StyleC.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleC.Font = new Font("Vderdana", 8, FontStyle.Bold);

            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.HeaderCell.Style = StyleC;
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Button_OK.Enabled)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Modifique las secciones de las columnas que no cumple mostradas en el recuadro localizado en la parte derecha superior de esta ventana.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form1.Proyecto_ = null;
            Environment.Exit(-1);

        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }
}
