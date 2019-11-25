using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisenoColumnas.Resultados
{
    public partial class Resultados_ : Form
    {

        private DataSet DataVirtual;
        public Resultados_()
        {
            InitializeComponent();
            DataVirtual = new DataSet("DATAVIRTUAL");
        }

        private void Resultados__Load(object sender, EventArgs e)
        {
            DGV_Estribos.VirtualMode = true;
            CrearTableVitualEstribos();

            DGV_Estribos.DataSource = DataVirtual.Tables["Estribos"];
            EstiloDatGridView(DGV_Estribos);

        }


        private void CrearTableVitualEstribos()
        {
            if (Form1.Proyecto_ != null)
            {

                string NameTable = "Estribos";
                List<string> Encabezados = new List<string> { "Columna", "Piso" , "Luz Libre" + Environment.NewLine + "(m)", "#E en Viga", "#E en Zona Confinada", "#E en Zona No Confinada", "#E en Zona Confinada " };
                DataTable table = new DataTable(NameTable);
                DataVirtual.Tables.Add(table);

                for (int i = 0; i < Encabezados.Count; i++)
                {
                    DataColumn dataColumn = new DataColumn(Encabezados[i]);
                    table.Columns.Add(dataColumn);
                }

                foreach (Columna col in Form1.Proyecto_.Lista_Columnas)
                {
               
                    if (col.CantEstribos_Sepa != null)
                    {
                        for (int i = 0; i < col.CantEstribos_Sepa.Count; i++)
                        {
                            DataRow dataRow = table.NewRow();

                            dataRow[0] = col.Name;
                            dataRow[1] = col.Seccions[i].Item2;
                            dataRow[2] = String.Format("{0:0.00}", col.LuzLibre[i]);
                            dataRow[3] = (int)col.CantEstribos_Sepa[i][0] == 0? "-": (col.CantEstribos_Sepa[i][0] + " # " + col.Seccions[i].Item1.Estribo.NoEstribo + " @" + col.Seccions[i].Item1.Estribo.Separacion + "cm");
                            dataRow[4] = (int)col.CantEstribos_Sepa[i][1] == 0 ?"-" : (col.CantEstribos_Sepa[i][1] + " # " + col.Seccions[i].Item1.Estribo.NoEstribo + " @" + col.Seccions[i].Item1.Estribo.Separacion + "cm");
                            dataRow[5] = (int)col.CantEstribos_Sepa[i][2] == 0 ?"-" : (col.CantEstribos_Sepa[i][2] + " # " + col.Seccions[i].Item1.Estribo.NoEstribo + " @" + Math.Round((float)col.CantEstribos_Sepa[i][5]*100,2) + "cm");
                            dataRow[6] = (int)col.CantEstribos_Sepa[i][3] == 0 ?"-" : (col.CantEstribos_Sepa[i][3] + " # " + col.Seccions[i].Item1.Estribo.NoEstribo + " @" + col.Seccions[i].Item1.Estribo.Separacion + "cm");

                            table.Rows.Add(dataRow);
                        }
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





    }
}
