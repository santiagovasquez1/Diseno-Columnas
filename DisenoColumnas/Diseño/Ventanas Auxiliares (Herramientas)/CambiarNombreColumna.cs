using DisenoColumnas.Clases;
using System;
using System.Windows.Forms;

namespace DisenoColumnas.Diseño.Ventanas_Auxiliares__Herramientas_
{
    public partial class CambiarNombreColumna : Form
    {
        public CambiarNombreColumna()
        {
            InitializeComponent();
        }

        private void CambiarNombreColumna_Load(object sender, EventArgs e)
        {
            CreateDataGridView();
            if (Form1.m_Informacion != null)
            {
                Form1.m_Informacion.EstiloDatGridView(D_ColLabel);
            }
        }

        private void CreateDataGridView()
        {
            foreach (Columna col in Form1.Proyecto_.Lista_Columnas)
            {
                D_ColLabel.Rows.Add();
                D_ColLabel.Rows[D_ColLabel.Rows.Count - 1].Cells[0].Value = col.Name;
                D_ColLabel.Rows[D_ColLabel.Rows.Count - 1].Cells[1].Value = col.Label;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < D_ColLabel.Rows.Count; i++)
            {
                string Label = "";
                if (D_ColLabel.Rows[i].Cells[1].Value != null)
                {
                    Label = D_ColLabel.Rows[i].Cells[1].Value.ToString();
                }
                Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColLabel.Rows[i].Cells[0].Value.ToString()).Label = Label.ToUpper();
            }
            Close();
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void Label9_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void D_ColLabel_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (D_ColLabel.Rows[e.RowIndex].Cells[1].Value != null)
            {
                D_ColLabel.Rows[e.RowIndex].Cells[1].Value = D_ColLabel.Rows[e.RowIndex].Cells[1].Value.ToString().ToUpper();
            }
            else
            {
                D_ColLabel.Rows[e.RowIndex].Cells[1].Value = "";
            }
            for (int i = 0; i < D_ColLabel.Rows.Count; i++)
            {
                if (i != e.RowIndex)
                {
                    if (D_ColLabel.Rows[i].Cells[1].Value != null)
                    {
                        if (D_ColLabel.Rows[i].Cells[1].Value.ToString() != "")
                        {
                            if (D_ColLabel.Rows[i].Cells[1].Value.ToString() == D_ColLabel.Rows[e.RowIndex].Cells[1].Value.ToString())
                            {
                                MessageBox.Show("El Label asignado ya existe.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                D_ColLabel.Rows[e.RowIndex].Cells[1].Value = "";
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}