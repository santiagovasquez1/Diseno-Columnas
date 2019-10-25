using DisenoColumnas.Clases;
using System;
using System.Collections.Generic;
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
                      Form1.Proyecto_.Lista_Columnas[i].Maestro =(bool) D_ColSim.Rows[i].Cells[1].Value;

                        if (D_ColSim.Rows[i].Cells[2].Value != null  )
                        {
                            if (D_ColSim.Rows[i].Cells[2].Value.ToString() != "")
                            {
                                Form1.Proyecto_.Lista_Columnas[i].ColSimilName = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).Name;
                                Form1.Proyecto_.Lista_Columnas[i].Alzados = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).Alzados;

                                for(int j=0; j< Form1.Proyecto_.Lista_Columnas[i].resultadosETABs.Count; j++)
                                {
                                    Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).resultadosETABs[j].As_asignado;
                                    Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo = Form1.Proyecto_.Lista_Columnas.Find(x => x.Name == D_ColSim.Rows[i].Cells[2].Value.ToString()).resultadosETABs[j].Porct_Refuerzo;
                                }
                         

                            }
                            else
                            {
                                List<Alzado> Aux = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].Alzados);
                                Form1.Proyecto_.Lista_Columnas[i].Alzados = null;
                                Form1.Proyecto_.Lista_Columnas[i].Alzados = Aux;

                                for (int j = 0; j < Form1.Proyecto_.Lista_Columnas[i].resultadosETABs.Count; j++)
                                {
                                    float[] Aux_AsAsing = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado);
                                    float[] Aux_Porcet_Refuerzo = Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo;
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
                            Form1.Proyecto_.Lista_Columnas[i].Alzados = null;
                            Form1.Proyecto_.Lista_Columnas[i].Alzados = Aux;
                            for (int j = 0; j < Form1.Proyecto_.Lista_Columnas[i].resultadosETABs.Count; j++)
                            {
                                float[] Aux_AsAsing = FunctionsProject.DeepClone(Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado);
                                float[] Aux_Porcet_Refuerzo = Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo;
                                Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado = null;
                                Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo = null;
                                Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].As_asignado = Aux_AsAsing;
                                Form1.Proyecto_.Lista_Columnas[i].resultadosETABs[j].Porct_Refuerzo = Aux_Porcet_Refuerzo;
                            }

                        }


                    }
                }
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
            for(int i=0;i< D_ColSim.Rows.Count;i++)
            {
                if ((bool)D_ColSim.Rows[i].Cells[1].Value)
                {
                    NameMaestro.Add(Form1.Proyecto_.Lista_Columnas[i].Name);
                }
            }
            for (int i = 0; i < D_ColSim.Rows.Count; i++)
            {
                DataGridViewComboBoxCell BoxCell =(DataGridViewComboBoxCell)D_ColSim.Rows[i].Cells[2];
                D_ColSim.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                BoxCell.Items.Clear();
                BoxCell.Items.AddRange(NameMaestro.ToArray());
                
            }
        }

        private void D_ColSim_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        
        }
    }
}
