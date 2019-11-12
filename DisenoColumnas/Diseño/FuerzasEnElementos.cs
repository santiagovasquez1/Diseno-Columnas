using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Diseño
{
    public partial class FuerzasEnElementos : DockContent
    {
        public FuerzasEnElementos()
        {
            InitializeComponent();
        }
        private DataSet DataVirtual;

        private void FuerzasEnElementos_Load(object sender, EventArgs e)
        {


        }




        Clases.Columna columnaAnte;




        private void CargarDataGrideView()
        {

            //          EstiloDatGridView(D_Fuerzas);
            Clases.Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            if (ColumnaSelect != null)
            {
                NameColum.Text = "Columna: " + ColumnaSelect.Name;
                if (ColumnaSelect != columnaAnte)
                {
                    CreateNewDataVirutal();
                    D_Fuerzas.VirtualMode = true;
                    D_Fuerzas.DataSource = DataVirtual.Tables[0];
                    EstiloDatGridView(D_Fuerzas);
                }
            }
            columnaAnte = ColumnaSelect;
        }









        private void CreateNewDataVirutal()
        {

            Clases.Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            if (ColumnaSelect != null)
            {


                DataVirtual = new DataSet("DS_Fuerzas");

                DataVirtual.Tables.Clear();
                List<string> Encabezados = new List<string> { "Story", "Load", "Loc", "P" + Environment.NewLine + "Tonf", "V2" + Environment.NewLine + "Tonf", "V3" + Environment.NewLine + "Tonf", "M2" + Environment.NewLine + "Tonf-m", "M3" + Environment.NewLine + "Tonf-m" };

                DataTable T_Fuerzas = new DataTable("Fuerzas");
                DataVirtual.Tables.Add(T_Fuerzas);


                for (int i = 0; i < Encabezados.Count; i++)
                {
                    DataColumn dataColumn = new DataColumn(Encabezados[i]);
                    T_Fuerzas.Columns.Add(dataColumn);
                }


                for (int i = 0; i < ColumnaSelect.resultadosETABs.Count; i++)
                {
                    for (int j = 0; j < ColumnaSelect.resultadosETABs[i].Load.Count; j++)
                    {

                        DataRow dataRow = T_Fuerzas.NewRow();

                        dataRow[0] = ColumnaSelect.resultadosETABs[i].Story_Result[j];
                        dataRow[1] = ColumnaSelect.resultadosETABs[i].Loc[j];
                        dataRow[2] = ColumnaSelect.resultadosETABs[i].Load[j];
                        dataRow[3] = ColumnaSelect.resultadosETABs[i].P[j];
                        dataRow[4] = ColumnaSelect.resultadosETABs[i].V2[j];
                        dataRow[5] = ColumnaSelect.resultadosETABs[i].V3[j];
                        dataRow[6] = ColumnaSelect.resultadosETABs[i].M2[j];
                        dataRow[7] = ColumnaSelect.resultadosETABs[i].M3[j];
                        T_Fuerzas.Rows.Add(dataRow);
                    }


                }

            }


        }



        private void EstiloDatGridView(DataGridView dataGridView)
        {
            DataGridViewCellStyle StyleC = new DataGridViewCellStyle();
            StyleC.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleC.Font = new Font("Vderdana", 8, FontStyle.Bold);


            DataGridViewCellStyle StyleR = new DataGridViewCellStyle();
            StyleR.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleR.Font = new Font("Vderdana", 8, FontStyle.Regular);


            foreach (DataGridViewColumn column in dataGridView.Columns)
            {

                column.HeaderCell.Style = StyleC;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            }
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.DefaultCellStyle = StyleR;
            }
        }

        private void FuerzasEnElementos_Paint(object sender, PaintEventArgs e)
        {
            CargarDataGrideView();
        }
    }
}
