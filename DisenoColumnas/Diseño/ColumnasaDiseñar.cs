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

namespace DisenoColumnas.Diseño
{
    public partial class ColumnasaDiseñar : Form
    {
        public ColumnasaDiseñar()
        {
            InitializeComponent();
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            PictureBox2.BackColor = Color.Transparent;
        }

        private void PictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox2.BackColor = Color.White;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Close();
            Form1.mFormPrincipal.CancelDiseño = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
            Form1.mFormPrincipal.CancelDiseño = true;
        }

        private void ColumnasaDiseñar_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }





        private void LoadDataGridView()
        {

            foreach (Columna columna in Form1.Proyecto_.Lista_Columnas)
            {

                D_ColDiseno.Rows.Add();

                D_ColDiseno.Rows[D_ColDiseno.Rows.Count - 1].Cells[0].Value = columna.Name;
                D_ColDiseno.Rows[D_ColDiseno.Rows.Count - 1].Cells[1].Value = columna.Disenar;

            }

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

        private void Button1_Click(object sender, EventArgs e)
        {

            for(int i=0; i< D_ColDiseno.Rows.Count; i++)
            {

             Form1.Proyecto_.Lista_Columnas[i].Disenar = (bool)D_ColDiseno.Rows[i].Cells[1].Value;
              
            }

            Form1.mFormPrincipal.CancelDiseño = false;
            Close();




        }

        private void Button3_Click(object sender, EventArgs e)
        {
            bool TodasSonSelect=false;

            for (int i = 0; i < D_ColDiseno.Rows.Count; i++)
            {
                if ((bool)D_ColDiseno.Rows[i].Cells[1].Value == true)
                {
                    if (i != D_ColDiseno.Rows.Count - 1)
                    {
                        TodasSonSelect = true;
                    }
                }
                else { TodasSonSelect = false; }
            }

            if (D_ColDiseno.Rows.Count != 0)
            {
                for (int i = 0; i < D_ColDiseno.Rows.Count; i++)
                {
                    D_ColDiseno.Rows[i].Cells[1].Value = true;
                }
            }


            if (TodasSonSelect)
            {
                if (D_ColDiseno.Rows.Count != 0)
                {
                    for (int i = 0; i < D_ColDiseno.Rows.Count; i++)
                    {
                        D_ColDiseno.Rows[i].Cells[1].Value = false;
                    }
                }

            }


        }
    }
}
