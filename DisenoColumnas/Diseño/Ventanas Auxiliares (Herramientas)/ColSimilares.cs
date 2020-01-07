using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DisenoColumnas.Diseño
{
    public partial class ColSimilares : Form
    {
        public ColSimilares()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void ColSimilares_Load(object sender, EventArgs e)
        {
            LoadSimilitudCol();
        }

        private void LoadSimilitudCol()
        {
            if (Form1.Proyecto_ != null)
            {
                if (Form1.Proyecto_.Lista_Columnas != null)
                {
                    for (int i = 0; i < Form1.Proyecto_.Lista_Columnas.Count; i++)
                    {
                        D_ColSim.Rows.Add();
                        D_ColSim.Rows[i].Cells[0].Value = Form1.Proyecto_.Lista_Columnas[i].Name;
                        D_ColSim.Rows[i].Cells[1].Value = Form1.Proyecto_.Lista_Columnas[i].Maestro;
                        if (Form1.Proyecto_.Lista_Columnas[i].Maestro)
                        {
                            D_ColSim.Rows[i].Cells[2].ReadOnly = true;
                        }
                        else
                        {
                            if (Form1.Proyecto_.Lista_Columnas[i].ColSimilName != "")
                            {
                                D_ColSim.Rows[i].Cells[2].Value = Form1.Proyecto_.Lista_Columnas[i].ColSimilName;
                            }
                        }
                    }

                    List<string> NameMaestro = new List<string>();
                    for (int i = 0; i < D_ColSim.Rows.Count; i++)
                    {
                        if ((bool)D_ColSim.Rows[i].Cells[1].Value)
                        {
                            NameMaestro.Add(Form1.Proyecto_.Lista_Columnas[i].Name);
                        }
                    }
                    for (int i = 0; i < D_ColSim.Rows.Count; i++)
                    {
                        DataGridViewComboBoxCell BoxCell = (DataGridViewComboBoxCell)D_ColSim.Rows[i].Cells[2];
                        D_ColSim.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        BoxCell.Items.Clear();
                        BoxCell.Items.AddRange(NameMaestro.ToArray());
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Form1.Proyecto_ != null)
            {
                if (Form1.Proyecto_.Lista_Columnas != null)
                {
                    for (int i = 0; i < D_ColSim.Rows.Count; i++)
                    {
                        Form1.Proyecto_.Lista_Columnas[i].Maestro = (bool)D_ColSim.Rows[i].Cells[1].Value;

                        if (D_ColSim.Rows[i].Cells[2].Value != null)
                        {
                            if (D_ColSim.Rows[i].Cells[2].Value.ToString() != "")
                            {
                                if (Form1.Proyecto_.Lista_Columnas[i].resultadosETABs.Count == Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).resultadosETABs.Count)
                                {
                                    for (int j = 0; j < Form1.Proyecto_.Lista_Columnas[i].resultadosETABs.Count; j++)
                                    {
                                        Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).resultadosETABs[j].As_asignado;
                                        // Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).resultadosETABs[j].Porct_Refuerzo;
                                    }

                                    Form1.Proyecto_.Lista_Columnas[i].ColSimilName = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).Name;
                                    Form1.Proyecto_.Lista_Columnas[i].Alzados = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).Alzados;
                                }
                                else
                                {
                                    MessageBox.Show("La columna " + Form1.Proyecto_.Lista_Columnas[i].Name +
                                    " que quiere asignar como similitud de la Columna " + D_ColSim.Rows[i].Cells[2].Value.ToString() +
                                    " no tiene la misma cantidad de pisos.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    return;
                                }
                            }
                            else
                            {
                                List<Alzado> Aux = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].Alzados);
                                Form1.Proyecto_.Lista_Columnas[i].Alzados = null;
                                Form1.Proyecto_.Lista_Columnas[i].Alzados = Aux;
                                Form1.Proyecto_.Lista_Columnas[i].ColSimilName = "";

                                for (int j = 0; j < Form1.Proyecto_.Lista_Columnas[i].resultadosETABs.Count; j++)
                                {
                                    float[] Aux_AsAsing = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado);
                                    float[] Aux_Porcet_Refuerzo = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo);
                                    Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado = null;
                                    Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo = null;
                                    Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado = Aux_AsAsing;
                                    Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo = Aux_Porcet_Refuerzo;
                                }
                            }
                        }
                        else
                        {
                            List<Alzado> Aux = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].Alzados);
                            Form1.Proyecto_.Lista_Columnas[i].ColSimilName = "";
                            Form1.Proyecto_.Lista_Columnas[i].Alzados = null;
                            Form1.Proyecto_.Lista_Columnas[i].Alzados = Aux;
                            for (int j = 0; j < Form1.Proyecto_.Lista_Columnas[i].resultadosETABs.Count; j++)
                            {
                                float[] Aux_AsAsing = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado);
                                float[] Aux_Porcet_Refuerzo = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo);
                                Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado = null;
                                Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo = null;
                                Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado = Aux_AsAsing;
                                Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo = Aux_Porcet_Refuerzo;
                            }
                        }
                    }
                }
                for (int i = 0; i < Form1.Proyecto_.Lista_Columnas.Count; i++)
                {
                    Form1.Proyecto_.Lista_Columnas[i].ActualizarRefuerzo();
                    Form1.Proyecto_.Lista_Columnas[i].CalcularPesoAcero();
                }
                Form1.mFormPrincipal.Invalidate();
                Form1.m_Informacion.Invalidate();
            }

            Close();
        }

        private void D_ColSim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void D_ColSim_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void D_ColSim_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 1)
            {
                D_ColSim.Rows[e.RowIndex].Cells[2].ReadOnly = (bool)D_ColSim.Rows[e.RowIndex].Cells[1].Value;
                DataGridViewComboBoxCell BoxCell = (DataGridViewComboBoxCell)D_ColSim.Rows[e.RowIndex].Cells[2];
                BoxCell.Items.Clear();
                D_ColSim.Rows[e.RowIndex].Cells[2].Value = "";
            }

            List<string> NameMaestro = new List<string>();
            for (int i = 0; i < D_ColSim.Rows.Count; i++)
            {
                if ((bool)D_ColSim.Rows[i].Cells[1].Value)
                {
                    NameMaestro.Add(Form1.Proyecto_.Lista_Columnas[i].Name);
                }
            }
            for (int i = 0; i < D_ColSim.Rows.Count; i++)
            {
                DataGridViewComboBoxCell BoxCell = (DataGridViewComboBoxCell)D_ColSim.Rows[i].Cells[2];
                D_ColSim.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                BoxCell.Items.Clear();
                BoxCell.Items.AddRange(NameMaestro.ToArray());
            }
        }

        #region Sombra Formulario

        //SOMBRA
        private const int WM_NCHITTEST = 0x84;

        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private bool m_aeroEnabled;
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;

                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;
        }

        #endregion Sombra Formulario

        private void Label9_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }
}