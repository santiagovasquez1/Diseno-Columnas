using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DisenoColumnas.Diseño
{
    public partial class AgregarRefuerzoBase : Form
    {
        public static int IndiceC { get; set; }
        
        public static bool EditarAlgunosAlzados { get; set; }
        public static List<int> Filas { get; set; }
        public static List<int> Columnas { get; set; }
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
                if (EditarAlgunosAlzados)
                {
              

                    AsignarRefuerzo(Filas, Columnas, Convert.ToInt32(NoBarra.Text), Convert.ToInt32(CantBarras.Text));
                    EditarAlgunosAlzados = false;
                    TP1.Enabled = true;
                    TP2.Enabled = true;

                }
                else
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
                    BarraPersonalizada.Visible = false;
                    BarraPersonalizada2.Visible = false;
                    Form1.mAgregarAlzado.CrearDataGrid(true);
                }
            }

            Close();
        }

        public void AsignarRefuerzo(List<int> Filas_Numeros, List<int> Columnas_Numeros, int NoBarra, int CantBarras)
        {
            Columna col = Form1.Proyecto_.ColumnaSelect;
            List<List<string>> AlzadoTemporal = new List<List<string>>();

            int FilaMayor = Filas_Numeros.Max();

            int Contador = 0; 

            for (int i = Columnas_Numeros[0]; i < Columnas_Numeros[Columnas_Numeros.Count-1]+1; i++)
            {
                
                AlzadoTemporal.Add(new List<string>());
                for (int j = Filas_Numeros[0]; j >= Filas_Numeros[Filas_Numeros.Count-1]; j--)
                {
                    string Nomenclatura = CantBarras + "#" + NoBarra;
                    if (i % 2 == 0)
                    {
                        if (j == col.Seccions.Count - 1)
                        {
                            Nomenclatura += "T1";
                        }
                        else if (j == 0)
                        {
                            if (col.Seccions.Count % 2 != 0)
                            {
                                Nomenclatura += "T1";
                            }
                            else
                            {
                                Nomenclatura += "T3";
                            }
                        }
                        else
                        {
                            if ((col.Seccions.Count - j) % 2 == 0)
                            {
                                Nomenclatura += "T2";
                            }
                            try
                            {


                                if (col.Seccions[j].Item1.B != col.Seccions[j - 1].Item1.B | col.Seccions[j].Item1.H != col.Seccions[j - 1].Item1.H)
                                {
                                    Nomenclatura = CantBarras + "#" + NoBarra;
                                    if ((col.AlzadoBaseSugerido.Count - j) % 2 == 0)
                                    {
                                        Nomenclatura += "T3";
                                    }
                                    else
                                    { Nomenclatura += "T1"; }
                                }
                            }

                            catch { }
                        }
                    }
                    else
                    {
                        if (j == col.Seccions.Count - 1)
                        {
                            Nomenclatura += "T3";
                        }
                        else if (j == 0)
                        {
                            if (col.Seccions.Count % 2 != 0)
                            {
                                Nomenclatura += "T3";
                            }
                            else
                            {
                                Nomenclatura += "T1";
                            }
                        }
                        else
                        {
                            if ((col.Seccions.Count - j) % 2 != 0)
                            {
                                Nomenclatura += "T2";
                            }
                            try
                            {


                                if (col.Seccions[j].Item1.B != col.Seccions[j - 1].Item1.B | col.Seccions[j].Item1.H != col.Seccions[j - 1].Item1.H)
                                {
                                    Nomenclatura = CantBarras + "#" + NoBarra;
                                    if ((col.AlzadoBaseSugerido.Count - j) % 2 == 0)
                                    {
                                        Nomenclatura += "T1";
                                    }
                                    else
                                    { Nomenclatura += "T3"; }
                                }
                            }

                            catch { }
                        }
                    }
                    
                    AlzadoTemporal[Contador].Add(Nomenclatura);
                
                }
                Contador += 1;
            }

            for(int i= 0; i< Columnas_Numeros.Count;i++)
            {
                for (int j= Filas_Numeros.Count-1; j>=0;j--)
                {
                    col.CrearAlzado(Columnas_Numeros[i], Filas_Numeros[j], col, AlzadoTemporal[i][j]);
                }

            }
            for (int i = Columnas_Numeros[0]; i < Columnas_Numeros[Columnas_Numeros.Count - 1] + 1; i++)
            {
                col.ModificarTraslapo(i, col);
                col.DeterminarCoordAlzado(i, col);
            }

            col.ActualizarRefuerzo();
            for (int i = Columnas_Numeros[0]; i < Columnas_Numeros[Columnas_Numeros.Count - 1] + 1; i++)
            {
                col.CalcularPesoAcero(i);
            }
            for (int i = 0; i < Columnas_Numeros.Count; i++)
            {
                for (int j = Filas_Numeros.Count - 1; j >= 0; j--)
                {
                    Form1.mAgregarAlzado.D_Alzado.Rows[Filas_Numeros[j]].Cells[Columnas_Numeros[i] + 1].Value = AlzadoTemporal[i][j].ToString();
                    
                }

            }

            Form1.m_Informacion.MostrarAcero();
            Form1.m_Despiece.Draw_Colum_Alzado.Invalidate();
            Form1.m_Despiece.Draw_Column.Invalidate();
            Form1.mAgregarAlzado.D_Alzado.RefreshEdit();

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

        private void AgregarRefuerzoBase_Load(object sender, EventArgs e)
        {
            if (EditarAlgunosAlzados)
            {
                TP1.Enabled = false;
                TP2.Enabled = false;
            }
        }
    }
}