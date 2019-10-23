using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
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


            if (ColumnaSelect != null)
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

                for (int i = 0; i < ColumnaSelect.Seccions.Count; i++)
                {
                    if (ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        D_Alzado.Rows.Add();
                        D_Alzado.Rows[D_Alzado.Rows.Count - 1].Cells[0].Value = ColumnaSelect.Seccions[i].Item2;

                    }
                }


                for (int i = 0; i < ColumnaSelect.Alzados.Count; i++)
                {

                    DataGridViewColumn NewColum = new DataGridViewColumn(D_Alzado.Rows[0].Cells[0]);
                    NewColum.Name = ColumnaSelect.Alzados[i].ID.ToString();
                    NewColum.HeaderText = "Alzado " + ColumnaSelect.Alzados[i].ID.ToString();
                    NewColum.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    NewColum.SortMode = DataGridViewColumnSortMode.NotSortable;
                    D_Alzado.Columns.Add(NewColum);


                }


                for (int i = 0; i < ColumnaSelect.Alzados.Count; i++)
                {

                    for (int j = 0; j < ColumnaSelect.Alzados[i].Colum_Alzado.Count; j++)
                    {
                        if (ColumnaSelect.Alzados[i].Colum_Alzado[j] != null)
                        {
                            D_Alzado.Rows[j].Cells[ColumnaSelect.Alzados[i].ID].Value = ColumnaSelect.Alzados[i].Colum_Alzado[j].ToString();
                        }

                    }

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



        private void ModificarTraslapo(int Indice, ref Columna columna)
        {

            for (int i = columna.Alzados[Indice].Colum_Alzado.Count - 1; i >= 0; i--)
            {

                if (columna.Alzados[Indice].Colum_Alzado[i] != null)
                {
                    try
                    {
                        if (columna.Alzados[Indice].Colum_Alzado[i].NoBarra > columna.Alzados[Indice].Colum_Alzado[i - 1].NoBarra)
                        {
                            float FC = columna.Seccions[i].Item1.Material.FC;
                            if (FC == 210f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_210[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 280f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_280[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 350f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_350[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 420f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_420[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 490f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_490[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                            if (FC == 560f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_560[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        }
                        else
                        {
                            float FC = columna.Seccions[i].Item1.Material.FC;
                            if (FC == 210f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_210[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 280f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_280[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 350f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_350[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 420f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_420[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 490f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_490[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                            if (FC == 560f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_560[columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra]; }
                        }


                    }
                    catch
                    {
                        float FC = columna.Seccions[i].Item1.Material.FC;
                        if (FC == 210f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_210[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 280f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_280[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 350f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_350[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 420f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_420[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 490f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_490[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                        if (FC == 560f) { columna.Alzados[Indice].Colum_Alzado[i].Traslapo = Form1.Proyecto_.Ld_560[columna.Alzados[Indice].Colum_Alzado[i].NoBarra]; }
                    }


                }




            }


        }








        private void EndCellEdit(int IndiceC, int IndiceR, bool isNotPaste)
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            if (D_Alzado.Rows[IndiceR].Cells[IndiceC].Value != null && IndiceR < ColumnaSelect.LuzLibre.Count)
            {
                string ValorCelda = D_Alzado.Rows[IndiceR].Cells[IndiceC].Value.ToString();
                object[] Clasficiacion = ClasificarCelda(ValorCelda);

                if (Clasficiacion[0].ToString() != "Error")
                {
                    int Cant_Barras = (int)Clasficiacion[1];
                    int NoBarra = (int)Clasficiacion[2];
                    int NoPiso = (D_Alzado.Rows.Count) - IndiceR;
                    float H = ColumnaSelect.LuzLibre[IndiceR];
                    float Hviga = ColumnaSelect.VigaMayor.Seccions[IndiceR].Item1.H;
                    string T = (string)Clasficiacion[3];

                    float HV1, HV2, HVV1, HVV2, Hacum; bool UltimPiso = false;
                    Hacum = 0;
                    try { HV1 = ColumnaSelect.LuzLibre[IndiceR + 1]; HVV1 = ColumnaSelect.Seccions[IndiceR + 1].Item1.H; } catch { HV1 = 0; HVV1 = 0; }
                    try { HV2 = ColumnaSelect.LuzLibre[IndiceR - 1]; HVV2 = ColumnaSelect.Seccions[IndiceR - 1].Item1.H; } catch { HV2 = 0; HVV2 = 0; }


                    for (int i = ColumnaSelect.LuzLibre.Count - 1; i >= IndiceR; i--)
                    {
                        Hacum += ColumnaSelect.LuzLibre[i] + ColumnaSelect.VigaMayor.Seccions[i].Item1.H;
                    }
                    Hacum += Form1.Proyecto_.e_Fundacion;



                    if (NoPiso == ColumnaSelect.LuzLibre.Count)
                    {
                        UltimPiso = true;
                    }


                    AlzadoUnitario unitario = new AlzadoUnitario(Cant_Barras, NoBarra, T, NoPiso, IndiceC, H, HV1, HV2, Hviga, HVV1, HVV2, Form1.Proyecto_.e_Fundacion, UltimPiso, Hacum);


                    ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] = unitario;

                    ModificarTraslapo(IndiceC - 1, ref ColumnaSelect);


                }
                else
                {
                    ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] = null;
                    D_Alzado.Rows[IndiceR].Cells[IndiceC].Value = "";
                }

            }
            else if (IndiceR < ColumnaSelect.LuzLibre.Count)
            {
                ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] = null;
            }
            ColumnaSelect.ActualizarRefuerzo();
            Form1.m_Informacion.Invalidate();

            if (isNotPaste == false)
            {
                DeterminarCoordAlzado();
                Form1.m_Despiece.Invalidate();
            }


        }


        public void DeterminarCoordAlzado()
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;
            float DisG = 0.2f; float r = 0.08f; float eF = Form1.Proyecto_.e_Fundacion;

            foreach (Alzado a in ColumnaSelect.Alzados)
            {
               
                for(int i= a.Colum_Alzado.Count - 1; i >= 0; i--)
                {

                    AlzadoUnitario au = a.Colum_Alzado[i];
                    if(au != null)
                    {
                        au.Coord_Alzado_PB = new List<float[]>();
                        if (au.NoStory == 1 & au.Traslapo_Nomenc == "T1") //Si es Primer Piso y  Si Tiene Traslapo Tipo1
                        {
                            float[] XY1 = new float[] { a.DistX + DisG, r };
                            float[] XY2 = new float[] { a.DistX, r };
                            float[] XY3 = new float[] { a.DistX, eF + au.H_Stroy / 2 + au.Traslapo / 2 };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);

                        }


                        









                    }
        

                }



            }
        

















            


        }










        private object[] ClasificarCelda(string Celda)
        {

            object[] Clasf = new object[] { "Error" };


            if (Celda.Contains("#"))
            {
                int CantidadBarras = 0;
                int NoBarra = 0;
                string Traslap = "";

                for (int i = 0; i < Celda.Length; i++)
                {
                    if (Celda.Substring(i, 1) == "#")
                    {
                        CantidadBarras = Convert.ToInt32(Celda.Substring(0, i));
                        try
                        {
                            NoBarra = Convert.ToInt32(Celda.Substring(i + 1, 2));
                        }
                        catch
                        {
                            NoBarra = Convert.ToInt32(Celda.Substring(i + 1, 1));
                        }

                    }
                    if (Celda.Substring(i, 1) == "T")
                    {
                        Traslap = Celda.Substring(i);
                       
                    }

                }

                Clasf = new object[] { "Ok", CantidadBarras, NoBarra, Traslap };

            }

            return Clasf;

        }


        private void D_Alzado_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            EndCellEdit(e.ColumnIndex, e.RowIndex, false);
        }

        private void D_Alzado_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (D_Alzado.SelectedCells.Count > 0)
            {
                D_Alzado.ContextMenuStrip = contextMenuStrip2;
                if (D_Alzado.CurrentCell.ReadOnly == true)
                {
                    contextMenuStrip2.Enabled = false;
                }
                else
                {
                    contextMenuStrip2.Enabled = true;
                }

            }
        }










        private void CopyToClipboard(DataGridView data)
        {
            DataObject dataObject = data.GetClipboardContent();
            if (dataObject != null)
            {
                Clipboard.SetDataObject(dataObject);
            }
        }

        private void CutToClipboard(DataGridView data)
        {
            CopyToClipboard(data);
            for (int i = 0; i < D_Alzado.SelectedCells.Count; i++)
            {
                D_Alzado.SelectedCells[i].Value = string.Empty;
            }
        }


        private void PasteClipboard(DataGridView data)
        {

            char[] rowSplitter = { '\r', '\n' };
            char[] columnSplitter = { '\t' };

            // Get the text from clipboard

            IDataObject dataInClipboard = Clipboard.GetDataObject();
            string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);

            // Split it into lines
            string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

            // Get the row and column of selected cell in grid
            int r = data.SelectedCells[0].RowIndex;
            int c = data.SelectedCells[0].ColumnIndex;

            // Add rows into grid to fit clipboard lines


            // Loop through the lines, split them into cells and place the values in the corresponding cell.
            for (int iRow = 0; iRow < rowsInClipboard.Length; iRow++)
            {
                // Split row into cell values
                string[] valuesInRow = rowsInClipboard[iRow].Split(columnSplitter);

                // Cycle through cell values
                for (int iCol = 0; iCol < valuesInRow.Length; iCol++)
                {

                    // Assign cell value, only if it within columns of the grid
                    if (data.ColumnCount - 1 >= c + iCol && r + iRow < data.Rows.Count)
                    {
                        DataGridViewCell cell = data.Rows[r + iRow].Cells[c + iCol];

                        if (!cell.ReadOnly)
                        {
                            cell.Value = valuesInRow[iCol];
                        }
                    }
                }
            }



            for (int Col = 1; Col < data.Columns.Count; Col++)
            {

                for (int Row = 0; Row < data.Rows.Count; Row++)
                {

                    EndCellEdit(Col, Row, true);

                }
                DeterminarCoordAlzado();
                Form1.m_Despiece.Invalidate();
            }



        }




        private void CopiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyToClipboard(D_Alzado);
        }


        private void CortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutToClipboard(D_Alzado);
        }

        private void PegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteClipboard(D_Alzado);


        }
    }

}
