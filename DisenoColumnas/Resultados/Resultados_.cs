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
using System.Windows.Forms.VisualStyles;

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

     
        private void CreateTableVirtualInfoColumnas()
        {

            string NameTable = "Información de Columnas";
            List<string> Encabezados = new List<string> {"Top/ Medium/ Button", "Label", "Nombre", "Story", "f'c [kgf/cm²]","Sección","Acero Requerido [cm²]", "rho R","Acero Asignado [cm²]", "rho A","%Ref" };
            DataTable table = new DataTable(NameTable);
            DataVirtual.Tables.Add(table);
            for (int i = 0; i < Encabezados.Count; i++)
            {
                DataColumn dataColumn = new DataColumn(Encabezados[i]);
                table.Columns.Add(dataColumn);
            }

            foreach (Columna col in Form1.Proyecto_.Lista_Columnas)
            {
                if (col.resultadosETABs[0].prequerida == null) { col.ActualizarRefuerzo(); }

                for (int i = 0; i < col.resultadosETABs.Count; i++)
                {
                    for (int j = 0; j < col.resultadosETABs[i].AsTopMediumButton.Length; j++)
                    {
                        DataRow dataRow = table.NewRow();
                        float FC = 10000;
                        if (j == 0)
                        {
                            dataRow[0] = "Top";

                        }
                        if (j == 1)
                        {
                            dataRow[0] = "Medium";
     
                        }
                        if (j == 2)
                        {
                            dataRow[0] = "Button";
       
                        }

                        dataRow[1] = col.Name;
                        dataRow[2] = col.Label;
                        dataRow[3] = col.Seccions[i].Item2;
                        dataRow[4] = col.Seccions[i].Item1.Material.FC;
                        dataRow[5] = col.Seccions[i].Item1.ToString();

                        dataRow[6] = Math.Round(col.resultadosETABs[i].AsTopMediumButton[j] * FC, 2) ;
                        dataRow[7] = Math.Round(col.resultadosETABs[i].prequerida[j],2) + "%";
                        dataRow[8] = Math.Round(col.resultadosETABs[i].As_asignado[j] * FC, 2);
                        dataRow[9] = Math.Round(col.resultadosETABs[i].pasignada[j], 2) + "%";
                        dataRow[10] = Math.Round(col.resultadosETABs[i].Porct_Refuerzo[j], 2) + "%";

                        table.Rows.Add(dataRow);
                    }


                }


            }

        }

        private void CrearTableVitualEstribos()
        {
            if (Form1.Proyecto_ != null)
            {
                string NameTable = "Cantidad de Estribos";
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

  
        private void BAceptar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DataVirtual.Tables.Clear();
            if (treeViewResultados.Nodes[0].Nodes[1].Checked)
            {
                CrearTableVitualEstribos();

            }
            if (treeViewResultados.Nodes[0].Nodes[0].Checked)
            { 
                CreateTableVirtualInfoColumnas();
            }
            if (DataVirtual.Tables.Count!=0)
            {
                TabladeResultados tabladeResultados = new TabladeResultados();
                TabladeResultados.DataVirtual = DataVirtual;
                tabladeResultados.ShowDialog();
            }

        }

        private void Panel5_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        

        #region TreeView


        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Bounds.IsEmpty)
                return;

            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            Point ckboxPoint = new Point(e.Node.Bounds.X - 16, e.Node.Bounds.Y + ((e.Node.Bounds.Height - 13) / 2));
            Rectangle expandRct = new Rectangle(ckboxPoint.X - 13, e.Bounds.Y + ((e.Bounds.Height - 9) / 2), 9, 9);

            CheckBoxState ckboxState = CheckBoxState.UncheckedNormal;
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Tag != null)
                {
                    ckboxState = CheckBoxState.MixedNormal;
                }
                else
                {
                    int nodosMarcados = e.Node.Nodes.OfType<TreeNode>().Where(node => node.Checked).Count();
                    if (nodosMarcados == e.Node.Nodes.Count)
                        ckboxState = CheckBoxState.CheckedNormal;
                }
            }
            else
            {
                if (e.Node.Checked)
                    ckboxState = CheckBoxState.CheckedNormal;
            }

            if (e.Node.Nodes.Count > 0)
            {
                // dibuja glyph para expandir/colapsar
                if (e.Node.IsExpanded)
                    e.Graphics.DrawImage(Properties.Resources.collapseBtn, expandRct);
                else
                    e.Graphics.DrawImage(Properties.Resources.expandBtn, expandRct);
            }

            // dibuja glyph checkbox
            CheckBoxRenderer.DrawCheckBox(e.Graphics, ckboxPoint, ckboxState);

            if (e.Node.IsSelected)
                e.Graphics.FillRectangle(Brushes.White, e.Node.Bounds);

            // dibuja texto
            Rectangle rctText = new Rectangle
                (
                    e.Node.Bounds.X,
                    e.Node.Bounds.Y,
                    e.Bounds.Width - e.Node.Bounds.X,
                    e.Node.Bounds.Height
                );
            using (SolidBrush brush = new SolidBrush((e.State & TreeNodeStates.Selected) > 0 ? Color.Black : Color.Black))
            {
                e.Graphics.DrawString(e.Node.Text, this.treeViewResultados.Font, brush, rctText, new StringFormat { LineAlignment = StringAlignment.Center });
            }

            if ((e.State & TreeNodeStates.Focused) > 0)
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Node.Bounds);
        }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown)
                return;

            this.treeViewResultados.BeginUpdate();

            if (e.Node.Nodes.Count > 0)
                this.MarcarDesmarcarNodosHijos(e.Node, true);

            if (e.Node.Parent != null)
                this.IndeterminarPadre(e.Node.Parent);

            this.treeViewResultados.Invalidate();
            this.treeViewResultados.EndUpdate();
        }
        private void MarcarDesmarcarNodosHijos(TreeNode nodeParent, bool flag)
        {
            nodeParent.Tag = null;
            foreach (TreeNode child in nodeParent.Nodes)
            {
                child.Checked = nodeParent.Checked;
                if (child.Nodes.Count > 0)
                    this.MarcarDesmarcarNodosHijos(child, false);
            }

            if (flag && nodeParent.Parent != null)
                this.IndeterminarPadre(nodeParent.Parent);
        }
        private void IndeterminarPadre(TreeNode nodeParent)
        {
            int cntIndeterminados = nodeParent.Nodes.OfType<TreeNode>().Where(node => node.Tag != null).Count();
            int cntMarcados = nodeParent.Nodes.OfType<TreeNode>().Where(node => node.Checked).Count();
            nodeParent.Tag = ((cntIndeterminados > 0) || (cntMarcados > 0 && cntMarcados != nodeParent.Nodes.Count)) ? "1" : null;
            nodeParent.Checked = cntMarcados == nodeParent.Nodes.Count;
            if (nodeParent.Parent != null)
                this.IndeterminarPadre(nodeParent.Parent);
        }

        #endregion
    }
}
