﻿using DisenoColumnas.Clases;
using DisenoColumnas.Secciones;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.InterfazViewInfo
{
    public partial class Informacion : DockContent
    {
        public Informacion()
        {
            InitializeComponent();
            EstiloDatGridView(Info_D);
        }

        public Columna ColumnaSelectAnt;

        private void DataGridView1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Informacion_Paint(object sender, PaintEventArgs e)
        {
            CrearDataGriedView();
        }

        public void CrearDataGriedView(bool Actuliazar = false)
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            if (ColumnaSelect != null)
            {
                if (ColumnaSelectAnt != ColumnaSelect)
                {
                    Info_D.Rows.Clear();
                    NameColum.Text = "Columna: " + ColumnaSelect.Name;

                    for (int i = 0; i < ColumnaSelect.Seccions.Count; i++)
                    {
                        if (ColumnaSelect.Seccions[i].Item1 != null | Actuliazar)
                        {
                            Color Color_RefMen = Color.FromArgb(250, 99, 99);
                            Color Color_RefCum = Color.FromArgb(29, 94, 243);
                            Info_D.Rows.Add();
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();
                            Info_D.Rows.Add();
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();
                            Info_D.Rows.Add();
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();

                            DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)Info_D[0, Info_D.Rows.Count - 3];
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

                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["AceroR"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].AsTopMediumButton[0] * FactorConversion, 2);
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["AceroR"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].AsTopMediumButton[1] * FactorConversion, 2);
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["AceroR"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].AsTopMediumButton[2] * FactorConversion, 2);

                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["Asasign"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].As_asignado[0] * FactorConversion, 2);
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["Asasign"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].As_asignado[1] * FactorConversion, 2);
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["Asasign"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].As_asignado[2] * FactorConversion, 2);

                            if (ColumnaSelect.resultadosETABs[i].prequerida == null)
                            {
                                ColumnaSelect.CrearCuantiaProyectoAntiguos(i);
                            }

                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["prequerida"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].prequerida[0], 2) + "%";
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["prequerida"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].prequerida[1], 2) + "%";
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["prequerida"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].prequerida[2], 2) + "%";

                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["pasignada"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].pasignada[0], 2) + "%";
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["pasignada"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].pasignada[1], 2) + "%";
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["pasignada"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].pasignada[2], 2) + "%";

                            if (Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[0], 3) > 105 | Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[0], 3) < 95)
                            {
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["Porc_Ref"].Style.ForeColor = Color_RefMen;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                            }
                            else
                            {
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["Porc_Ref"].Style.ForeColor = Color_RefCum;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                            }
                            if (Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[1], 3) > 105 | Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[1], 3) < 95)
                            {
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["Porc_Ref"].Style.ForeColor = Color_RefMen;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                            }
                            else
                            {
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["Porc_Ref"].Style.ForeColor = Color_RefCum;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                            }
                            if (Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[2], 3) > 105 | Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[2], 3) < 95)
                            {
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["Porc_Ref"].Style.ForeColor = Color_RefMen;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                            }
                            else
                            {
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["Porc_Ref"].Style.ForeColor = Color_RefCum;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                            }

                            Info_D.Rows[Info_D.Rows.Count - 3].Cells["Porc_Ref"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[0], 2) + "%";
                            Info_D.Rows[Info_D.Rows.Count - 2].Cells["Porc_Ref"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[1], 2) + "%";
                            Info_D.Rows[Info_D.Rows.Count - 1].Cells["Porc_Ref"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[2], 2) + "%";

                            float FCMetros = 100;

                            if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Rectangular)
                            {
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["TW"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["TF"].Style.BackColor = Color.LightGray;

                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["TW"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["TF"].Style.BackColor = Color.LightGray;

                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Style.BackColor = Color.LightGray;

                                DataGridViewTextBoxCellEx cell3 = (DataGridViewTextBoxCellEx)Info_D["B", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell4 = (DataGridViewTextBoxCellEx)Info_D["H", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell5 = (DataGridViewTextBoxCellEx)Info_D["TW", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell6 = (DataGridViewTextBoxCellEx)Info_D["TF", Info_D.Rows.Count - 3];
                                cell3.RowSpan = 3; cell4.RowSpan = 3; cell5.RowSpan = 3; ; cell6.RowSpan = 3;
                            }

                            if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Circle)
                            {
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["H"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["TW"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["TF"].Style.BackColor = Color.LightGray;

                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["H"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["TW"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["TF"].Style.BackColor = Color.LightGray;

                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Style.BackColor = Color.LightGray;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Style.BackColor = Color.LightGray;

                                DataGridViewTextBoxCellEx cell3 = (DataGridViewTextBoxCellEx)Info_D["B", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell4 = (DataGridViewTextBoxCellEx)Info_D["H", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell5 = (DataGridViewTextBoxCellEx)Info_D["TW", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell6 = (DataGridViewTextBoxCellEx)Info_D["TF", Info_D.Rows.Count - 3];
                                cell3.RowSpan = 3; cell4.RowSpan = 3; cell5.RowSpan = 3; ; cell6.RowSpan = 3;
                            }

                            if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Tee | ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.L)
                            {
                                CSD SeccionTL = (CSD)ColumnaSelect.Seccions[i].Item1;

                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["B"].Value = SeccionTL.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["H"].Value = SeccionTL.H * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["TW"].Value = SeccionTL.TW * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 3].Cells["TF"].Value = SeccionTL.TF * FCMetros;

                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["B"].Value = SeccionTL.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["H"].Value = SeccionTL.H * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["TW"].Value = SeccionTL.TW * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 2].Cells["TF"].Value = SeccionTL.TF * FCMetros;

                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["B"].Value = SeccionTL.B * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["H"].Value = SeccionTL.H * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["TW"].Value = SeccionTL.TW * FCMetros;
                                Info_D.Rows[Info_D.Rows.Count - 1].Cells["TF"].Value = SeccionTL.TF * FCMetros;

                                DataGridViewTextBoxCellEx cell3 = (DataGridViewTextBoxCellEx)Info_D["B", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell4 = (DataGridViewTextBoxCellEx)Info_D["H", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell5 = (DataGridViewTextBoxCellEx)Info_D["TW", Info_D.Rows.Count - 3];
                                DataGridViewTextBoxCellEx cell6 = (DataGridViewTextBoxCellEx)Info_D["TF", Info_D.Rows.Count - 3];
                                cell3.RowSpan = 3; cell4.RowSpan = 3; cell5.RowSpan = 3; ; cell6.RowSpan = 3;
                            }
                        }
                    }

                    ColumnaSelectAnt = ColumnaSelect;
                }
            }

            EstiloDatGridView(Info_D);
        }

        public void MostrarAcero()
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            Color Color_RefMen = Color.FromArgb(250, 99, 99);
            Color Color_RefCum = Color.FromArgb(29, 94, 243);
            if (Info_D.Rows.Count == Form1.mAgregarAlzado.D_Alzado.Rows.Count * 3)
            {
                for (int i = 0; i < Form1.mAgregarAlzado.D_Alzado.Rows.Count; i++)
                {
                    if (Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[0], 3) > 105 | Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[0], 3) < 95)
                    {
                        Info_D.Rows[(i * 3 + 3) - 3].Cells["Porc_Ref"].Style.ForeColor = Color_RefMen;
                        Info_D.Rows[(i * 3 + 3) - 3].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                    }
                    else
                    {
                        Info_D.Rows[(i * 3 + 3) - 3].Cells["Porc_Ref"].Style.ForeColor = Color_RefCum;
                        Info_D.Rows[(i * 3 + 3) - 3].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                    }
                    if (Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[1], 3) > 105 | Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[1], 3) < 95)
                    {
                        Info_D.Rows[(i * 3 + 3) - 2].Cells["Porc_Ref"].Style.ForeColor = Color_RefMen;
                        Info_D.Rows[(i * 3 + 3) - 2].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                    }
                    else
                    {
                        Info_D.Rows[(i * 3 + 3) - 2].Cells["Porc_Ref"].Style.ForeColor = Color_RefCum;
                        Info_D.Rows[(i * 3 + 3) - 2].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                    }
                    if (Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[2], 3) > 105 | Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[2], 3) < 95)
                    {
                        Info_D.Rows[(i * 3 + 3) - 1].Cells["Porc_Ref"].Style.ForeColor = Color_RefMen;
                        Info_D.Rows[(i * 3 + 3) - 1].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                    }
                    else
                    {
                        Info_D.Rows[(i * 3 + 3) - 1].Cells["Porc_Ref"].Style.ForeColor = Color_RefCum;
                        Info_D.Rows[(i * 3 + 3) - 1].Cells["Porc_Ref"].Style.Font = new Font("Vderdana", 8, FontStyle.Bold);
                    }
                    double FactorConversion = 10000;

                    Info_D.Rows[(i * 3 + 3) - 3].Cells["Asasign"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].As_asignado[0] * FactorConversion, 2);
                    Info_D.Rows[(i * 3 + 3) - 2].Cells["Asasign"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].As_asignado[1] * FactorConversion, 2);
                    Info_D.Rows[(i * 3 + 3) - 1].Cells["Asasign"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].As_asignado[2] * FactorConversion, 2);

                    Info_D.Rows[(i * 3 + 3) - 3].Cells["Porc_Ref"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[0], 2) + "%";
                    Info_D.Rows[(i * 3 + 3) - 2].Cells["Porc_Ref"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[1], 2) + "%";
                    Info_D.Rows[(i * 3 + 3) - 1].Cells["Porc_Ref"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].Porct_Refuerzo[2], 2) + "%";

                    if (ColumnaSelect.resultadosETABs[i].prequerida == null)
                    {
                        ColumnaSelect.CrearCuantiaProyectoAntiguos(i);
                    }

                    Info_D.Rows[(i * 3 + 3) - 3].Cells["pasignada"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].pasignada[0], 2) + "%";
                    Info_D.Rows[(i * 3 + 3) - 2].Cells["pasignada"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].pasignada[1], 2) + "%";
                    Info_D.Rows[(i * 3 + 3) - 1].Cells["pasignada"].Value = Math.Round(ColumnaSelect.resultadosETABs[i].pasignada[2], 2) + "%";

                    // Info_D.Refresh();
                }
            }
        }

        public void EstiloDatGridView(DataGridView dataGrid)
        {
            DataGridViewCellStyle StyleC = new DataGridViewCellStyle();
            StyleC.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleC.Font = new Font("Vderdana", 8, FontStyle.Bold);

            DataGridViewCellStyle StyleR = new DataGridViewCellStyle();
            StyleR.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleR.Font = new Font("Vderdana", 8, FontStyle.Regular);

            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.HeaderCell.Style = StyleC;
            }
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                row.DefaultCellStyle = StyleR;
            }
        }
    }
}