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
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Diseño
{
    public partial class AgregarAlzado : DockContent
    {
        public AgregarAlzado()
        {
            InitializeComponent();
        }

   
        public void CrearDataGrid()
        {
     

            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;


            if(ColumnaSelect != null)
            {


                
                D_Alzado.Columns.Clear();
                D_Alzado.Rows.Clear();
                NameColum.Text = "Columna: " + ColumnaSelect.Name;


                DataGridViewCell dataGridView = new DataGridViewTextBoxCell();
                DataGridViewColumn NewColumStory = new DataGridViewColumn();
                NewColumStory.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                NewColumStory.SortMode = DataGridViewColumnSortMode.NotSortable;
                NewColumStory.Name = "story";
                NewColumStory.HeaderText = "Story ";
                NewColumStory.CellTemplate = dataGridView;
                NewColumStory.ReadOnly = true;
                D_Alzado.Columns.Add(NewColumStory);

                for (int i=0; i< ColumnaSelect.Seccions.Count;i++)
                {
                    if (ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        D_Alzado.Rows.Add();
                        D_Alzado.Rows[D_Alzado.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;

                    }
                }


                for (int i = 0; i < ColumnaSelect.Alzados.Count ; i++)
                {

                    DataGridViewColumn NewColum = new DataGridViewColumn(D_Alzado.Rows[0].Cells[0]);
                    NewColum.Name = ColumnaSelect.Alzados[i].ID.ToString();
                    NewColum.HeaderText = "Alzado " + ColumnaSelect.Alzados[i].ID.ToString();
                    NewColum.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    NewColum.SortMode = DataGridViewColumnSortMode.NotSortable;
                    D_Alzado.Columns.Add(NewColum);


                }

                EstiloDatGridView();
        }
            


        }
        private void EstiloDatGridView()
        {
            DataGridViewCellStyle StyleC = new DataGridViewCellStyle();
            StyleC.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleC.Font = new Font("Vderdana", 8, FontStyle.Bold);

            DataGridViewCellStyle StyleR = new DataGridViewCellStyle();
            StyleR.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleR.Font = new Font("Vderdana", 8, FontStyle.Regular);


            foreach (DataGridViewColumn column in D_Alzado.Columns)
            {

                column.HeaderCell.Style = StyleC;

            }
            foreach (DataGridViewRow row in D_Alzado.Rows)
            {
                row.DefaultCellStyle = StyleR;
            }
        }
        private void AgregarAlzado_Paint(object sender, PaintEventArgs e)
        {
            CrearDataGrid();

        }
    }
}
