using DisenoColumnas.Clases;
using DisenoColumnas.Secciones;

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Diseño
{
    public partial class CuantiaVolumetrica : DockContent
    {
        public CuantiaVolumetrica()
        {
            InitializeComponent();
        }

        private Columna ColumnaSelectAnt;

        private void CambiosDataGridView(int i)
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;
            NameColum.Text = "Columna: " + ColumnaSelect.Name;

            if (ColumnaSelect.Seccions[i].Item1 != null)
            {
                int IndiceRow = 0;
                for (int j = 0; j < Info_Es_Col.Rows.Count; j++) { if (Info_Es_Col.Rows[j].Cells[0].Value.ToString() == ColumnaSelect.Seccions[i].Item2) { IndiceRow = j; } }

                Info_Es_Col.Rows[IndiceRow].Cells["NoEstribo"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoEstribo.ToString();
                Info_Es_Col.Rows[IndiceRow].Cells["S_value"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.Separacion.ToString();
                 Info_Es_Col.Rows[IndiceRow].Cells["CantEstribos"].Value = (int)ColumnaSelect.CantEstribos_Sepa[i][0] + (int)ColumnaSelect.CantEstribos_Sepa[i][1]+(int)ColumnaSelect.CantEstribos_Sepa[i][2] + (int)ColumnaSelect.CantEstribos_Sepa[i][3];

                if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Rectangular)
                {
                    DataGridViewTextBoxCellEx cell0 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamasV_1", 0];
                    cell0.ColumnSpan = 2;
                    DataGridViewTextBoxCellEx cell00 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamas_H1", 0];
                    cell00.ColumnSpan = 2;

                    Info_Es_Col.Rows[IndiceRow].Cells["NoRamasV_1"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasV1;
                    Info_Es_Col.Rows[IndiceRow].Cells["NoRamasV_2"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasV1;

                    DataGridViewTextBoxCellEx cell1 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamasV_1", Info_Es_Col.Rows.Count - 1];
                    cell1.ColumnSpan = 2;

                    Info_Es_Col.Rows[IndiceRow].Cells["NoRamas_H1"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasH1;
                    Info_Es_Col.Rows[IndiceRow].Cells["NoRamas_H2"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasH1;
                    DataGridViewTextBoxCellEx cell2 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamas_H1", Info_Es_Col.Rows.Count - 1];
                    cell2.ColumnSpan = 2;
                }

                if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Circle)
                {
                }

                if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Tee | ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.L)
                {
                    Info_Es_Col.Rows[IndiceRow].Cells["NoRamasV_1"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasV1;
                    Info_Es_Col.Rows[IndiceRow].Cells["NoRamasV_2"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasV2;

                    Info_Es_Col.Rows[IndiceRow].Cells["NoRamas_H1"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasH1;
                    Info_Es_Col.Rows[IndiceRow].Cells["NoRamas_H2"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasH2;
                }
            }
        }

        private void CreateDataGridView()
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            if (ColumnaSelect != null)
            {
                if (ColumnaSelect.CantEstribos_Sepa == null)
                {
                    ColumnaSelect.CantEstribos_Sepa =  new System.Collections.Generic.List<object[]>();
                    for (int i = ColumnaSelect.LuzAcum.Count - 1; i >= 0; i--)
                    {
                        ColumnaSelect.CantEstribos_Sepa.Add(new object[] { 0, 0, 0, 0,0,0 });
                    }
                    for (int i = ColumnaSelect.LuzAcum.Count - 1; i >= 0; i--)
                    {
                        ColumnaSelect.CantidadEstribos(i);
                    }
                }

                if (ColumnaSelectAnt != ColumnaSelect)
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
                            if (ColumnaSelect.Seccions[i].Item1.Estribo == null)
                            {
                                CalCuantiaVol(ColumnaSelect.Seccions[i].Item1, false, i);
                            }

                            Info_Es_Col.Rows.Add(ColumnaSelect.Seccions[i].Item2);
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[1].Value = ColumnaSelect.Seccions[i].Item1.Material.FC.ToString();

                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[0].ReadOnly = true;
                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells[1].ReadOnly = true;

                            if (ColumnaSelect.Seccions[i].Item1.Estribo != null)
                            {
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoEstribo"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoEstribo.ToString();
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["S_value"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.Separacion.ToString();

                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["CantEstribos"].Value = (int)ColumnaSelect.CantEstribos_Sepa[i][0] + (int)ColumnaSelect.CantEstribos_Sepa[i][1] + (int)ColumnaSelect.CantEstribos_Sepa[i][2] + (int)ColumnaSelect.CantEstribos_Sepa[i][3];

                            }
                            else
                            {
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["S_value"].Value = 0;
                            }

                            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["CantEstribos"].ReadOnly = true;
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

                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_1"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasV1;
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_2"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasV2;

                                DataGridViewTextBoxCellEx cell1 = (DataGridViewTextBoxCellEx)Info_Es_Col["NoRamasV_1", Info_Es_Col.Rows.Count - 1];
                                cell1.ColumnSpan = 2;

                                Info_Es_Col.Rows[0].Cells["NoRamasV_1"].Value = "No. Ramas Vertical";
                                Info_Es_Col.Rows[0].Cells["NoRamas_H1"].Value = "No. Ramas Horizontal";

                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H1"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasH1;
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H2"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasH2;
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

                            if (ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.Tee | ColumnaSelect.Seccions[i].Item1.Shape == TipodeSeccion.L)
                            {
                                CSD SeccionTL = (CSD)ColumnaSelect.Seccions[i].Item1;
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["B"].Value = SeccionTL.B * FCMetros;
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["H"].Value = SeccionTL.H * FCMetros;
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TW"].Value = SeccionTL.TW * FCMetros;
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["TF"].Value = SeccionTL.TF * FCMetros;

                                Info_Es_Col.Rows[0].Cells["NoRamasV_1"].Value = "No. Ramas Vertical";
                                Info_Es_Col.Rows[0].Cells["NoRamas_H1"].Value = "No. Ramas Horizontal";

                                Info_Es_Col.Rows[0].Cells["NoRamasV_2"].Value = "No. Ramas Vertical";
                                Info_Es_Col.Rows[0].Cells["NoRamas_H2"].Value = "No. Ramas Horizontal";

                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_1"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasV1;
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamasV_2"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasV2;

                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H1"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasH1;
                                Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoRamas_H2"].Value = ColumnaSelect.Seccions[i].Item1.Estribo.NoRamasH2;
                            }
                        }
                    }
                    ColumnaSelectAnt = ColumnaSelect;
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
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["CantEstribos"].Value = "No. Estribos";
            //ReadOnly
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["S_value"].ReadOnly = true;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoEstribo"].ReadOnly = true;
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["CantEstribos"].ReadOnly = true;

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
            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["CantEstribos"].Style.Font = fontBold;

            Info_Es_Col.Rows[0].Frozen = true;

            DataGridViewTextBoxCell CelldaEstribo = new DataGridViewTextBoxCell();
            CelldaEstribo.Value = "# Estribo";
            CelldaEstribo.Style.Font = fontBold;

            Info_Es_Col.Rows[Info_Es_Col.Rows.Count - 1].Cells["NoEstribo"] = CelldaEstribo;
        }

        private void CalCuantiaVol(ISeccion seccioni, bool Cambio_Data, int index)
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

                if (seccioni.Estribo == null)
                {
                    seccioni.Calc_vol_inex(Form1.Proyecto_.R / 100, Form1.Proyecto_.FY, Form1.Proyecto_.DMO_DES);
                }

                seccioni.Cuanti_Vol(FD1, FD2, Form1.Proyecto_.R / 100, Form1.Proyecto_.FY);

                if (Cambio_Data == true)
                {
                    CambiosDataGridView(index);
                }
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
            ISeccion seccioni = null;
            string piso = "";

            float Separacion;
            try
            {
                Separacion = Convert.ToSingle(Info_Es_Col.Rows[IndiceR].Cells["S_value"].Value);
            }
            catch
            {
                Separacion = 0;
            }

            int NoBarra = Convert.ToInt32(Info_Es_Col.Rows[IndiceR].Cells["NoEstribo"].Value);
            string Story = Info_Es_Col.Rows[IndiceR].Cells[0].Value.ToString();
            int IndiceaM = Form1.Proyecto_.ColumnaSelect.Seccions.FindIndex(x => x.Item2 == Story);
            piso = Form1.Proyecto_.ColumnaSelect.Seccions[IndiceaM].Item2;
            seccioni = FunctionsProject.DeepClone(Form1.Proyecto_.ColumnaSelect.Seccions[IndiceaM].Item1);

            seccioni.Estribo.NoEstribo = NoBarra;
            seccioni.Estribo.Separacion = Separacion;
            seccioni.Estribo.CalcularArea();
            CalCuantiaVol(seccioni, false, IndiceaM);

            Form1.Proyecto_.ColumnaSelect.Seccions[IndiceaM] = new Tuple<ISeccion, string>(seccioni, piso);
            Form1.Proyecto_.ColumnaSelect.CantidadEstribos(IndiceR - 1);
            CambiosDataGridView(IndiceaM);
        }

        private void Info_Es_Col_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            EditEndCell(e.RowIndex, e.ColumnIndex);
        }
    }
}
