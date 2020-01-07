using DisenoColumnas.Clases;
using DisenoColumnas.Secciones;
using DisenoColumnas.Utilidades;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Seccion
{
    public partial class FEditarPredef : Form
    {
        private static FInterfaz_Seccion FInterfaz_ { get; set; } = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
        private ISeccion Seccion { get; set; }
        private int index { get; set; } = -1;
        private GDE gde;
        private bool Aplica { get; set; }

        public FEditarPredef(ISeccion pSeccion, FInterfaz_Seccion pInterfaz, GDE pgde)
        {
            FInterfaz_ = pInterfaz;
            Seccion = pSeccion;
            Seccion.CalcNoDBarras();
            gde = pgde;
            InitializeComponent();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FEditarPredef_Load(object sender, EventArgs e)
        {
            string Ref_Base = "Refuerzo Base ";
            if (Seccion.Shape == TipodeSeccion.Rectangular | Seccion.Shape == TipodeSeccion.Circle)
            {
                groupBox2.Text = "Ramas Sección";
                groupBox3.Enabled = false;
                gbPeso.Location = new Point(403, 31);
            }

            if (Seccion.Shape == TipodeSeccion.Tee | Seccion.Shape == TipodeSeccion.L)
            {
                groupBox2.Text = "Ramas Alma";
                groupBox3.Text = "Ramas Aleta";
                groupBox3.Enabled = true;
                groupBox3.Visible = true;
                gbPeso.Location = new Point(599, 31);
            }

            cbEstribo.Text = "#" + Seccion.Estribo.NoEstribo;
            nudSep.Value = (decimal)Seccion.Estribo.Separacion;
            Cargar_Datos(dataGridView1);

            for (int i = 0; i < Seccion.No_D_Barra.Count; i++)
            {
                if (i == Seccion.No_D_Barra.Count - 1)
                {
                    Ref_Base += $"{Seccion.No_D_Barra[i].Item1}#{Seccion.No_D_Barra[i].Item2}";
                }
                else
                {
                    Ref_Base += $"{Seccion.No_D_Barra[i].Item1}#{Seccion.No_D_Barra[i].Item2}+";
                }
            }

            lBase.Text = Ref_Base;
            lBase.Update();
        }

        private void Cuantia_Volumetrica()
        {
            int NumEstribo = 0;
            int pos = 0;

            pos = cbEstribo.Text.IndexOf('#') + 1;
            NumEstribo = Convert.ToInt32(cbEstribo.Text.Substring(pos));

            if (Seccion.Estribo == null)
            {

            }

            Estribo temp = new Estribo(NumEstribo);

            if (nudSep.Value > 0)
            {
                temp.Separacion = (float)nudSep.Value;
            }
            else
            {
                temp.Separacion = Seccion.Estribo.Separacion;
            }

            Seccion.Estribo = temp;


            float FD1, FD2;
            if (gde == GDE.DMO)
            {
                FD1 = 0.20f;
                FD2 = 0.06f;
            }
            else
            {
                FD1 = 0.30f;
                FD2 = 0.09f;
            }

            Seccion.Cuanti_Vol(FD1, FD2, 0.04f, 4220);

            if (Seccion.Shape == TipodeSeccion.Rectangular)
            {
                nuCX.Value = Seccion.Estribo.NoRamasV1;
                nuCY.Value = Seccion.Estribo.NoRamasH1;
            }

            if (Seccion.Shape == TipodeSeccion.Tee | Seccion.Shape == TipodeSeccion.L)
            {
                nuCX.Value = Seccion.Estribo.NoRamasV1;
                nuCY.Value = Seccion.Estribo.NoRamasH1;
                nuCXw.Value = Seccion.Estribo.NoRamasV2;
                nuCYw.Value = Seccion.Estribo.NoRamasH2;
            }
        }

        private void cbEstribo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cuantia_Volumetrica();
            lPesoEstribo.Text = "Peso estribos conf :" + Math.Round(Peso_Estribos(), 2).ToString() + " kg/m";
        }

        private void nudSep_ValueChanged(object sender, EventArgs e)
        {
            Cuantia_Volumetrica();
            lPesoEstribo.Text = "Peso estribos conf :" + Math.Round(Peso_Estribos(), 2).ToString() + " kg/m";
        }

        private double Peso_Estribos()
        {
            return Seccion.Peso_Estribo(Seccion.Estribo, 0.04f);
        }

        private void Cargar_Datos(DataGridView data)
        {
            data.Rows.Clear();
            data.Rows.Add(Seccion.Refuerzos.Count);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i].Cells[0].Value = Seccion.Refuerzos[i].id;
                data.Rows[i].Cells[1].Value = Seccion.Refuerzos[i].Diametro;
                data.Rows[i].Cells[2].Value = Math.Round(Seccion.Refuerzos[i].Coord[0], 2);
                data.Rows[i].Cells[3].Value = Math.Round(Seccion.Refuerzos[i].Coord[1], 2);
            }
        }

        private void Crear_tabla(int CapasX, int CapasY, int CapasXw, int CapasYw, DataGridView data)
        {
            int numero_barras = CapasXw > 0 ? (CapasY + CapasYw) * 2 + (CapasX - 2) * 2 + (CapasXw - 2) * 2 : (CapasY) * 2 + (CapasX - 2) * 2;
            int id = 1;

            double H = Seccion.H * 100;
            double b = Seccion.B * 100;
            double Tw = 0;
            double tf = 0;
            double r = Form1.Proyecto_.R + 1; //1 es el espesor del estribo #3
            double posx, posy;
            double DeltaX1, DeltaY1, DeltaX2, DeltaY2;
            int ContX, ContY;

            if (Seccion.Shape == TipodeSeccion.L | Seccion.Shape == TipodeSeccion.Tee)
            {
                if (Seccion.Shape == TipodeSeccion.L)
                {
                    //Tw = Seccion.TW * 100;
                    //tf = Seccion.TF * 100;
                }
                else
                {
                }
            }

            data.Rows.Clear();
            data.Rows.Add(numero_barras);

            DeltaX1 = (b - 2 * r) / (CapasX - 1);
            DeltaY1 = (H - 2 * r) / (CapasY - 1);
            DeltaX2 = (Tw - 2 * r) / (CapasXw - 1);
            DeltaY2 = (tf - 2 * r) / (CapasYw - 1);

            posx = -(b / 2) + r; posy = (H / 2) - r;
            ContX = CapasX - 2; ContY = CapasY;

            for (int i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i].Cells[0].Value = id;
                data.Rows[i].Cells[1].Value = "#4";

                if (Seccion.Shape == TipodeSeccion.Rectangular)
                {
                    data.Rows[i].Cells[2].Value = Math.Round(posx, 2);
                    data.Rows[i].Cells[3].Value = Math.Round(posy, 2);

                    posy -= DeltaY1;
                    ContY--;

                    if (ContY == 0 & ContX > 0)
                    {
                        posx += DeltaX1;
                        posy = (H / 2) - r;
                        ContY = 2;
                        DeltaY1 = (H - 2 * r) / (ContY - 1);
                        ContX--;
                    }

                    if (ContX == 0 & ContY == 0)
                    {
                        ContY = CapasY;
                        DeltaY1 = (H - 2 * r) / (ContY - 1);
                        posx = (b / 2) - r;
                        posy = (H / 2) - r;
                    }
                }

                id++;
            }
        }

        private void Reload_Seccion()
        {
            CRefuerzo refuerzo;
            DataGridViewComboBoxCell boxCell;
            string diametro;
            int id, indice;
            double x, y;
            double[] coord;
            Seccion.Refuerzos.Clear();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                boxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells[1];
                id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                diametro = boxCell.Value.ToString();
                x = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                y = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                coord = new double[] { x, y };

                refuerzo = new CRefuerzo(id, diametro, coord, TipodeRefuerzo.longitudinal);
                Seccion.Refuerzos.Add(refuerzo);
            }

            Seccion.Acero_Long = Seccion.Refuerzos.Sum(x1 => x1.As_Long);
            Seccion.Editado = true;

            if (gde == GDE.DES)
            {
                indice = Form1.secciones_predef.Secciones_DES.FindIndex(x1 => x1.ToString() == Seccion.ToString());
                Form1.secciones_predef.Secciones_DES[indice] = Seccion;
            }
            else
            {
                indice = Form1.secciones_predef.Secciones_DMO.FindIndex(x1 => x1.ToString() == Seccion.ToString());
                Form1.secciones_predef.Secciones_DMO[indice] = Seccion;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FInterfaz_.Over = false;
            FInterfaz_.Seleccionado = false;
            FInterfaz_.Invalidate();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reload_Seccion();
            FInterfaz_.Over = false;
            FInterfaz_.Seleccionado = false;
            FInterfaz_.Invalidate();
            Close();
        }

        private void Button_Cerrar_Click(object sender, EventArgs e)
        {
            FInterfaz_.Over = false;
            FInterfaz_.Seleccionado = false;
            FInterfaz_.Invalidate();
            Close();
        }

    }
}