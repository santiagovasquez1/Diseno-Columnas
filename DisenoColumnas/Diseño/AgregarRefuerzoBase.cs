using DisenoColumnas.Clases;
using System;
using System.Windows.Forms;

namespace DisenoColumnas.Diseño
{
    public partial class AgregarRefuerzoBase : Form
    {
        public static int IndiceC { get; set; }

        public AgregarRefuerzoBase()
        {
            InitializeComponent();
        }

        private void AgregarRefuerzoBase_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void Cb_Aceptar_Click(object sender, EventArgs e)
        {
            float r;
            bool IsNumeric = Single.TryParse(CantBarras.Text, out r);
            if (NoBarra.Text != "" && r != 0 && IsNumeric)
            {
                string Nomenclatura = r + "#" + NoBarra.Text;
                Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;
                if (TP1.Checked)
                {
                    for (int i = 0; i < Form1.mAgregarAlzado.D_Alzado.Rows.Count; i++)
                    {
                        Form1.mAgregarAlzado.D_Alzado.Rows[i].Cells[IndiceC].Value = "";
                    }

                    Form1.mAgregarAlzado.D_Alzado.Rows[Form1.mAgregarAlzado.D_Alzado.Rows.Count - 1].Cells[IndiceC].Value = Nomenclatura + "T1";
                    if (Form1.mAgregarAlzado.D_Alzado.Rows.Count % 2 != 0)
                    {
                        Form1.mAgregarAlzado.D_Alzado.Rows[0].Cells[IndiceC].Value = Nomenclatura + "T1";
                    }
                    else
                    {
                        Form1.mAgregarAlzado.D_Alzado.Rows[0].Cells[IndiceC].Value = Nomenclatura + "T3";
                    }

                    for (int i = Form1.mAgregarAlzado.D_Alzado.Rows.Count - 2; i > 0; i--)
                    {
                        if ((Form1.mAgregarAlzado.D_Alzado.Rows.Count - i) % 2 != 0)
                        {
                            Form1.mAgregarAlzado.D_Alzado.Rows[i].Cells[IndiceC].Value = Nomenclatura;
                        }
                        else
                        {
                            Form1.mAgregarAlzado.D_Alzado.Rows[i].Cells[IndiceC].Value = Nomenclatura + "T2";
                        }
                    }

                    BarraPersonalizada2.Width = 0;
                    BarraPersonalizada.Visible = true;
                    BarraPersonalizada2.Visible = true;
                    int D_Pro = BarraPersonalizada.Width / Form1.mAgregarAlzado.D_Alzado.Rows.Count - 1;

                    for (int i = Form1.mAgregarAlzado.D_Alzado.Rows.Count - 1; i >= 0; i--)
                    {
                        Form1.mAgregarAlzado.EndCellEdit(IndiceC, i, false, ColumnaSelect);
                        BarraPersonalizada2.Width += D_Pro;
                    }

                    Form1.mAgregarAlzado.CrearDataGrid(true);
                }
                else if (TP2.Checked)
                {
                    for (int i = 0; i < Form1.mAgregarAlzado.D_Alzado.Rows.Count; i++)
                    {
                        Form1.mAgregarAlzado.D_Alzado.Rows[i].Cells[IndiceC].Value = "";
                    }

                    Form1.mAgregarAlzado.D_Alzado.Rows[Form1.mAgregarAlzado.D_Alzado.Rows.Count - 1].Cells[IndiceC].Value = Nomenclatura + "T3";
                    if (Form1.mAgregarAlzado.D_Alzado.Rows.Count % 2 != 0)
                    {
                        Form1.mAgregarAlzado.D_Alzado.Rows[0].Cells[IndiceC].Value = Nomenclatura + "T3";
                    }
                    else
                    {
                        Form1.mAgregarAlzado.D_Alzado.Rows[0].Cells[IndiceC].Value = Nomenclatura + "T1";
                    }

                    for (int i = Form1.mAgregarAlzado.D_Alzado.Rows.Count - 2; i > 0; i--)
                    {
                        if ((Form1.mAgregarAlzado.D_Alzado.Rows.Count - i) % 2 != 0)
                        {
                            Form1.mAgregarAlzado.D_Alzado.Rows[i].Cells[IndiceC].Value = Nomenclatura + "T2";
                        }
                        else
                        {
                            Form1.mAgregarAlzado.D_Alzado.Rows[i].Cells[IndiceC].Value = Nomenclatura;
                        }
                    }

                    BarraPersonalizada2.Width = 0;
                    BarraPersonalizada.Visible = true;
                    BarraPersonalizada2.Visible = true;
                    int D_Pro = BarraPersonalizada.Width / Form1.mAgregarAlzado.D_Alzado.Rows.Count - 1;

                    for (int i = Form1.mAgregarAlzado.D_Alzado.Rows.Count - 1; i >= 0; i--)
                    {
                        Form1.mAgregarAlzado.EndCellEdit(IndiceC, i, false, ColumnaSelect);
                        BarraPersonalizada2.Width += D_Pro;
                    }
                }

                for (int i = 0; i < Form1.mAgregarAlzado.D_Alzado.Rows.Count; i++)
                {
                    Form1.mAgregarAlzado.D_Alzado.Rows[i].Cells[IndiceC].Value = Nomenclatura;
                }
            }
            BarraPersonalizada.Visible = false;
            BarraPersonalizada2.Visible = false;
            Form1.mAgregarAlzado.CrearDataGrid(true);
            Close();
        }

        private void GroupBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}