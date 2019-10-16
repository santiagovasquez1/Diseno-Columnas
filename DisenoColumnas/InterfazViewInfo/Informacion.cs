using DisenoColumnas.DefinirColumnas;
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

namespace DisenoColumnas.InterfazViewInfo
{
    public partial class Informacion : DockContent
    {
        public Informacion()
        {
            InitializeComponent();
            EstiloDatGridView();

        }


   
 
        private void DataGridView1_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void Informacion_Paint(object sender, PaintEventArgs e)
        {
            CrearDataGriedView();
        }




        private void CrearDataGriedView() 
        {

            
            if (PlantaColumnas.ColumnaSelect != null)
            {
            
                Info_D.Rows.Clear();
                NameColum.Text = "Columna: " + PlantaColumnas.ColumnaSelect.Name;


                for(int i=0; i < PlantaColumnas.ColumnaSelect.Seccions.Count; i++)
                {

                    if (PlantaColumnas.ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        Info_D.Rows.Add();
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells[0].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item2;
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells[1].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();


                        if (PlantaColumnas.ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.Rectangular)
                        {

                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.H;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Style.BackColor = Color.LightGray;
                        }

                        if (PlantaColumnas.ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.Circle)
                        {

                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Style.BackColor = Color.LightGray;
                        }

                        if (PlantaColumnas.ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.Tee | PlantaColumnas.ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.L)
                        {

                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.H;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.TW;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Value = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.TF;

                        }

                    }

                }



            }

            EstiloDatGridView();
        }



   


        private void EstiloDatGridView()
        {
            DataGridViewCellStyle StyleC = new DataGridViewCellStyle();
            StyleC.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleC.Font = new Font("Vderdana", 8, FontStyle.Regular);

            DataGridViewCellStyle StyleR = new DataGridViewCellStyle();
            StyleR.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleR.Font = new Font("Vderdana", 8, FontStyle.Regular);


            foreach (DataGridViewColumn column in Info_D.Columns)
            {

                column.HeaderCell.Style = StyleC;

            }
            foreach (DataGridViewRow row in Info_D.Rows)
            {
                row.DefaultCellStyle = StyleR;
            }
        }
    }
}
