using DisenoColumnas.Clases;
using SpannedDataGridViewNet2;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Diseño
{
    public partial class CuantiaVolumetrica : DockContent
    {
        public CuantiaVolumetrica()
        {
            InitializeComponent();
        }

        private void CambiosDataGridView()
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            NameColum.Text = "Columna: " + ColumnaSelect.Name;

            for (int i = 0; i < ColumnaSelect.Seccions.Count; i++)
            {
                if (ColumnaSelect.Seccions[i].Item1 != null)
                {
                    int IndiceRow = 0;
                    for (int j = 0; j < Info_Es_Col.Rows.Count; j++) { if (Info_Es_Col.Rows[j].Cells[0].Value.ToString() == ColumnaSelect.Seccions[i].Item2) { IndiceRow = j; } }

                    Info_Es_Col.Rows[IndiceRow].Cells["NoEstribo"].Value = ColumnaSelect.estribos[i].NoEstribo.ToString();

                    Info_Es_Col.Rows[IndiceRow].Cells["S_value"].Value = ColumnaSelect.estribos[i].Separacion.ToString();

                    if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Rectangular)
                    {
                        DataGridViewTextBoxCellEx cell0 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamasV_1", 0];
                        cell0.ColumnSpan = 2;
                        DataGridViewTextBoxCellEx cell00 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamas_H1", 0];
                        cell00.ColumnSpan = 2;

                        Info_Es_Col.Rows[IndiceRow].Cells["NoRamasV_1"].Value = ColumnaSelect.estribos[i].NoRamasV1;
                        Info_Es_Col.Rows[IndiceRow].Cells["NoRamasV_2"].Value = ColumnaSelect.estribos[i].NoRamasV1;

                        DataGridViewTextBoxCellEx cell1 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamasV_1", Info_Es_Col.Rows.Count - 1];
                        cell1.ColumnSpan = 2;

                        Info_Es_Col.Rows[IndiceRow].Cells["NoRamas_H1"].Value = ColumnaSelect.estribos[i].NoRamasH1;
                        Info_Es_Col.Rows[IndiceRow].Cells["NoRamas_H2"].Value = ColumnaSelect.estribos[i].NoRamasH1;
                        DataGridViewTextBoxCellEx cell2 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamas_H1", Info_Es_Col.Rows.Count - 1];
                        cell2.ColumnSpan = 2;
                    }

                    if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Circle)
                    {
                    }

                    if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Tee | ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.L)
                    {
                    }
                }
            }
        }

        private void CreateDataGridView()
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            if (ColumnaSelect != null)
            {
                Info_Es_Col.Rows.Clear();

                NameColum.Text = "Columna: " + ColumnaSelect.Name;

                float FCMetros = 100;
                Info_Es_Col.Rows.Add();

                EncabezadosDataGridView();

                for (int i = 0; i < ColumnaSelect.Seccions.Count; i++)
                {
                    if (ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        Info_Es_Col.Rows.Add(ColumnaSelect.Seccions[i].Item2);
                        Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;
                        Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();

                        Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[0].ReadOnly = true;
                        Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[1].ReadOnly = true;
                        if (ColumnaSelect.estribos[i].NoEstribo != 0)
                        {
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoEstribo"].Value = ColumnaSelect.estribos[i].NoEstribo.ToString();
                        }
                        Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["S_value"].Value = ColumnaSelect.estribos[i].Separacion.ToString();

                        Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoEstribo"].ReadOnly = false;
                        Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["S_value"].ReadOnly = false;

                        if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Rectangular)
                        {
                            DataGridViewTextBoxCellEx cell0 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamasV_1", 0];
                            cell0.ColumnSpan = 2;
                            DataGridViewTextBoxCellEx cell00 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamas_H1", 0];
                            cell00.ColumnSpan = 2;

                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H * FCMetros;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TF"].Style.BackColor = Color.LightGray;

                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_1"].Value = ColumnaSelect.estribos[i].NoRamasV1;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_2"].Value = ColumnaSelect.estribos[i].NoRamasV1;

                            DataGridViewTextBoxCellEx cell1 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamasV_1", Info_Es_Col.Rows.Count - 1];
                            cell1.ColumnSpan = 2;

                            Info_Es_Col.Rows[0].Cells["NoRamasV_1"].Value = "No. Ramas Vertical";
                            Info_Es_Col.Rows[0].Cells["NoRamas_H1"].Value = "No. Ramas Horizontal";

                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H1"].Value = ColumnaSelect.estribos[i].NoRamasH1;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H2"].Value = ColumnaSelect.estribos[i].NoRamasH1;
                            DataGridViewTextBoxCellEx cell2 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamas_H1", Info_Es_Col.Rows.Count - 1];
                            cell2.ColumnSpan = 2;
                        }

                        if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Circle)
                        {
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["H"].Style.BackColor = Color.LightGray;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TW"].Style.BackColor = Color.LightGray;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TF"].Style.BackColor = Color.LightGray;
                        }

                        if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Tee | ColumnaSelect.Seccions[i].Item1.Shape == Clases.TipodeSeccion.L)
                        {
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["B"].Value = ColumnaSelect.Seccions[i].Item1.B * FCMetros;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["H"].Value = ColumnaSelect.Seccions[i].Item1.H * FCMetros;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TW"].Value = ColumnaSelect.Seccions[i].Item1.TW * FCMetros;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TF"].Value = ColumnaSelect.Seccions[i].Item1.TF * FCMetros;
                        }
                    }
                }
                EstiloDatGridView();
            }
        }

        private void EncabezadosDataGridView()
        {
            Font fontBold = new Font("Vderdana", 8, FontStyle.Bold);

            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[0].Value = "Story";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[1].Value = "F'c [kgf/cm²]";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["B"].Value = "B[cm]";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["H"].Value = "H [cm]";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TW"].Value = "Tw [cm";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TF"].Value = "Tf [cm]";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_1"].Value = "No. Ramas Vertical (Aleta)";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_2"].Value = "No. Ramas Vertical (Alma)";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H1"].Value = "No. Ramas Horizontal (Aleta)";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H2"].Value = "No. Ramas Horizontal (Alma)";
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["S_value"].Value = "Separación [cm]";

            //ReadOnly
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["S_value"].ReadOnly = true;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoEstribo"].ReadOnly = true;

            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[0].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[1].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["B"].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["H"].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TW"].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TF"].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_1"].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_2"].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H1"].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H2"].Style.Font = fontBold;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["S_value"].Style.Font = fontBold;

            Info_Es_Col.Rows[0].Frozen = true;

            DataGridViewTextBoxCell CelldaEstribo = new DataGridViewTextBoxCell();
            CelldaEstribo.Value = "# Estribo";
            CelldaEstribo.Style.Font = fontBold;

            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoEstribo"] = CelldaEstribo;
        }

        private void CalCuantiaVol()
        {
            if (Form1.Proyecto_.ColumnaSelect != null)
            {
                float FD1, FD2;

                if (Form1.Proyecto_.DMO_DES == GDE.DMO)
                {
                    FD1 = 0.20f;
                    FD2 = 0.06f;
                }
                else
                {
                    FD1 = 0.30f;
                    FD2 = 0.09f;
                }

                Form1.Proyecto_.ColumnaSelect.CalcularCuantiaVolumetrica(FD1, FD2, Form1.Proyecto_.R / 100, Form1.Proyecto_.FY);
                CambiosDataGridView();
            }
        }

        private void EstiloDatGridView()
        {
            DataGridViewCellStyle StyleC = new DataGridViewCellStyle();
            StyleC.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleC.Font = new Font("Vderdana", 8, FontStyle.Regular);

            DataGridViewCellStyle StyleR = new DataGridViewCellStyle();
            StyleR.Alignment = DataGridViewContentAlignment.MiddleCenter;
            StyleR.Font = new Font("Vderdana", 8, FontStyle.Regular);

            foreach (DataGridViewColumn column in Info_Es_Col.Columns)
            {
                column.HeaderCell.Style = StyleC;
            }
            foreach (DataGridViewRow row in Info_Es_Col.Rows)
            {
                row.DefaultCellStyle = StyleR;
            }
        }

        private void CuantiaVolumetrica_Paint(object sender, PaintEventArgs e)
        {
            CreateDataGridView();
        }

        private void EditEndCell(int IndiceR, int IndiceC)
        {
            #region Modficiación No. y Area del Estribo

            if (Info_Es_Col.Columns[IndiceC].Name == "NoEstribo" && IndiceR != 0)
            {
                if (Form1.Proyecto_.ColumnaSelect != null && Info_Es_Col.Rows[IndiceR].Cells["NoEstribo"].Value != null && Info_Es_Col.Rows[IndiceR].Cells["NoEstribo"].Value.ToString() != "")
                {
                    int NoBarra = Convert.ToInt32(Info_Es_Col.Rows[IndiceR].Cells["NoEstribo"].Value);

                    string Story = Info_Es_Col.Rows[IndiceR].Cells[0].Value.ToString();
                    int IndiceaM = Form1.Proyecto_.ColumnaSelect.Seccions.FindIndex(x => x.Item2 == Story);
                    Form1.Proyecto_.ColumnaSelect.estribos[IndiceaM].NoEstribo = NoBarra;
                    Form1.Proyecto_.ColumnaSelect.estribos[IndiceaM].CalcularArea();
                    CalCuantiaVol();
                }
            }

            #endregion Modficiación No. y Area del Estribo

            if (Info_Es_Col.Columns[IndiceC].Name == "S_value" && IndiceR != 0)
            {
                if (Form1.Proyecto_.ColumnaSelect != null && Info_Es_Col.Rows[IndiceR].Cells["S_value"].Value != null && Info_Es_Col.Rows[IndiceR].Cells["S_value"].Value.ToString() != "")
                {
                    float Separacion;
                    try
                    {
                        Separacion = Convert.ToSingle(Info_Es_Col.Rows[IndiceR].Cells["S_value"].Value);
                    }
                    catch
                    {
                        Separacion = 0;
                    }
                    string Story = Info_Es_Col.Rows[IndiceR].Cells[0].Value.ToString();
                    int IndiceaM = Form1.Proyecto_.ColumnaSelect.Seccions.FindIndex(x => x.Item2 == Story);
                    Form1.Proyecto_.ColumnaSelect.estribos[IndiceaM].Separacion = Separacion;
                    CalCuantiaVol();
                }
            }
        }

        private void Info_Es_Col_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            EditEndCell(e.RowIndex, e.ColumnIndex);
        }
    }
}