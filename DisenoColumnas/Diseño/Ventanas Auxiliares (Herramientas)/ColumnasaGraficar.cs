using DisenoColumnas.Clases;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DisenoColumnas.Diseño
{
    public partial class ColumnasaGraficar : Form
    {
        public ColumnasaGraficar()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool MsgInfo1 = false;

            for (int i = 0; i < D_ColGraficar.Rows.Count; i++)
            {
                Form1.Proyecto_.Lista_Columnas[i].aGraficar = (bool)D_ColGraficar.Rows[i].Cells[2].Value;
                Form1.Proyecto_.Lista_Columnas[i].Ready = (bool)D_ColGraficar.Rows[i].Cells[1].Value;
            }

            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                if (columna.aGraficar)
                {
                    if (columna.ColSimilName != null)
                    {
                        if (columna.ColSimilName != "")
                        {
                            MsgInfo1 = true;
                        }
                    }
                }
            }

            if (MsgInfo1)
            {
                MessageBox.Show("Columnas asignadas una similitud no serán graficadas.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Form1.mFormPrincipal.CancelGarfica = false;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
            Form1.mFormPrincipal.CancelGarfica = true;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Close();
            Form1.mFormPrincipal.CancelGarfica = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            bool TodasSonSelect = false;

            for (int i = 0; i < D_ColGraficar.Rows.Count; i++)
            {
                if ((bool)D_ColGraficar.Rows[i].Cells[2].Value == true)
                {
                    if (i != D_ColGraficar.Rows.Count - 1)
                    {
                        TodasSonSelect = true;
                    }
                }
                else { TodasSonSelect = false; }
            }

            if (D_ColGraficar.Rows.Count != 0)
            {
                for (int i = 0; i < D_ColGraficar.Rows.Count; i++)
                {
                    D_ColGraficar.Rows[i].Cells[2].Value = true;
                    D_ColGraficar.Rows[i].Cells[1].Value = true;
                }
            }

            if (TodasSonSelect)
            {
                if (D_ColGraficar.Rows.Count != 0)
                {
                    for (int i = 0; i < D_ColGraficar.Rows.Count; i++)
                    {
                        D_ColGraficar.Rows[i].Cells[2].Value = false;
                        D_ColGraficar.Rows[i].Cells[1].Value = false;
                    }
                }
            }
        }

        private void ColumnasaGraficar_Load(object sender, EventArgs e)
        {
            LoadDataGridView();

            if (Form1.m_Informacion != null)
            {
                Form1.m_Informacion.EstiloDatGridView(D_ColGraficar);
            }

        }

        private void LoadDataGridView()
        {
            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {
                D_ColGraficar.Rows.Add();

                D_ColGraficar.Rows[D_ColGraficar.Rows.Count - 1].Cells[0].Value = columna.Name;
                D_ColGraficar.Rows[D_ColGraficar.Rows.Count - 1].Cells[1].Value = columna.Ready;
                D_ColGraficar.Rows[D_ColGraficar.Rows.Count - 1].Cells[2].Value = columna.aGraficar;




            }
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            PictureBox2.BackColor = Color.Transparent;
        }

        private void PictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox2.BackColor = Color.White;
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
    }
}