using DisenoColumnas.Clases;
using DisenoColumnas.DefinirColumnas;
using SpannedDataGridViewNet2;
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


          Columna ColumnaSelect=Form1.Proyecto_.ColumnaSelect;


            if (ColumnaSelect != null)
            {
            
                Info_D.Rows.Clear();
                NameColum.Text = "Columna: " + ColumnaSelect.Name;


                for(int i=0; i < ColumnaSelect.Seccions.Count; i++)
                {

                    if (ColumnaSelect.Seccions[i].Item1 != null )
                    {
                        Info_D.Rows.Add();
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();
                        Info_D.Rows.Add();
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();
                        Info_D.Rows.Add();
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();

                        DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)Info_D[0, Info_D.Rows.Count-3];
                        cell.RowSpan = 3;
                        DataGridViewTextBoxCellEx cell2 = (DataGridViewTextBoxCellEx)Info_D[1, Info_D.Rows.Count - 3];
                        cell2.RowSpan = 3;

                        double FactorConversion = 10000;

                        Info_D.Rows[Info_D.Rows.Count - 3].Cells["Locali"].Value = "Top";
                        Info_D.Rows[Info_D.Rows.Count - 2].Cells["Locali"].Value = "Medium";
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells["Locali"].Value = "Bottom";


                        double ptop = ColumnaSelect.resultadosETABs[i].AsTopMediumButton[0] / ColumnaSelect.Seccions[i].Item1.Area;
                        double pmedium = ColumnaSelect.resultadosETABs[i].AsTopMediumButton[1] / ColumnaSelect.Seccions[i].Item1.Area;
                        double pbutton = ColumnaSelect.resultadosETABs[i].AsTopMediumButton[2] / ColumnaSelect.Seccions[i].Item1.Area;

                        //Cuantia Mayor al 4% 

                        if (ptop >= 0.04)
                        {
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["AceroR"].Style.BackColor = Color.FromArgb(247, 114, 114);
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["AceroR"].ToolTipText = "Acero requerido mayor a la cuantía máxima permisible.";
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["AceroR"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);

                        }
                        if (pmedium >= 0.04)
                        {
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["AceroR"].Style.BackColor = Color.FromArgb(247, 114, 114);
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["AceroR"].ToolTipText = "Acero requerido mayor a la cuantía máxima permisible.";
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["AceroR"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                        }
                        if (pbutton >= 0.04)
                        {
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["AceroR"].Style.BackColor = Color.FromArgb(247, 114, 114);
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["AceroR"].ToolTipText = "Acero requerido mayor a la cuantía máxima permisible.";
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["AceroR"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                        }

                        //Cuantia Mayor al 4% 

                        Info_D.Rows[Info_D.Rows.Count - 3].Cells["AceroR"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].AsTopMediumButton[0] * FactorConversion,2);
                        Info_D.Rows[Info_D.Rows.Count - 2].Cells["AceroR"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].AsTopMediumButton[1] * FactorConversion, 2);
                        Info_D.Rows[Info_D.Rows.Count - 1].Cells["AceroR"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].AsTopMediumButton[2] * FactorConversion, 2);




                        if (ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.Rectangular)
                        {

                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["TF"].Style.BackColor = Color.LightGray;


                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["TF"].Style.BackColor = Color.LightGray;

                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Style.BackColor = Color.LightGray;


                            DataGridViewTextBoxCellEx cell3 = (DataGridViewTextBoxCellEx)Info_D["B", Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell4 = (DataGridViewTextBoxCellEx)Info_D["H", Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell5 = (DataGridViewTextBoxCellEx)Info_D["TW", Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell6 = (DataGridViewTextBoxCellEx)Info_D["TF", Info_D.Rows.Count - 3];
                            cell3.RowSpan = 3; cell4.RowSpan = 3; cell5.RowSpan = 3; ; cell6.RowSpan = 3;
                        }

                        if (ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.Circle)
                        {

                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["H"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["TF"].Style.BackColor = Color.LightGray;


                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["H"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["TF"].Style.BackColor = Color.LightGray;


                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Style.BackColor = Color.LightGray;

                            DataGridViewTextBoxCellEx cell3 = (DataGridViewTextBoxCellEx)Info_D["B", Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell4 = (DataGridViewTextBoxCellEx)Info_D["H",  Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell5 = (DataGridViewTextBoxCellEx)Info_D["TW", Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell6 = (DataGridViewTextBoxCellEx)Info_D["TF", Info_D.Rows.Count - 3];
                            cell3.RowSpan = 3; cell4.RowSpan = 3; cell5.RowSpan = 3; ; cell6.RowSpan = 3;
                        }

                        if (ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.Tee | ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.L)
                        {

                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["TW"].Value = ColumnaSelect.Seccions[i].Item1.TW;
                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["TF"].Value = ColumnaSelect.Seccions[i].Item1.TF;

                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["TW"].Value = ColumnaSelect.Seccions[i].Item1.TW;
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["TF"].Value = ColumnaSelect.Seccions[i].Item1.TF;


                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Value = ColumnaSelect.Seccions[i].Item1.TW;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Value = ColumnaSelect.Seccions[i].Item1.TF;

                            DataGridViewTextBoxCellEx cell3 = (DataGridViewTextBoxCellEx)Info_D["B", Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell4 = (DataGridViewTextBoxCellEx)Info_D["H",  Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell5 = (DataGridViewTextBoxCellEx)Info_D["TW", Info_D.Rows.Count - 3];
                            DataGridViewTextBoxCellEx cell6 = (DataGridViewTextBoxCellEx)Info_D["TF", Info_D.Rows.Count - 3];
                            cell3.RowSpan = 3; cell4.RowSpan = 3; cell5.RowSpan = 3; ; cell6.RowSpan = 3;
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
