using DisenoColumnas.Clases;
using DisenoColumnas.Diseño.Ventanas_Auxiliares__Herramientas_;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private Columna ColumnaSelectAnt;

        public void CrearDataGrid(bool BotonAgregar = false)
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;

            if (ColumnaSelect != null)
            {
                if (ColumnaSelectAnt != ColumnaSelect | BotonAgregar)
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
                    ColumnaSelectAnt = ColumnaSelect;
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

        private void ModificarTraslapo(int Indice, Columna columna)
        {
            for (int i = columna.Alzados[Indice].Colum_Alzado.Count - 1; i >= 0; i--)
            {
                if (columna.Alzados[Indice].Colum_Alzado[i] != null)
                {
                    float TraslapoFinal;
                    if (columna.Alzados[Indice].Colum_Alzado[i].Tipo.Contains("T"))
                    {
                        float TV1 = 0; float TV2 = 0; float TI = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i].NoBarra, columna.Seccions[i].Item1.Material.FC);

                        try { TV1 = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i + 1].NoBarra, columna.Seccions[i + 1].Item1.Material.FC); } catch { }
                        try { TV2 = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i - 1].NoBarra, columna.Seccions[i - 1].Item1.Material.FC); } catch { }
                        if (TV1 < TV2)
                        {
                            TV1 = TV2;
                        }
                        TraslapoFinal = TI > TV1 ? TI : TV1;
                    }
                    else
                    {
                        TraslapoFinal = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i].NoBarra, columna.Seccions[i].Item1.Material.FC);
                    }

                    columna.Alzados[Indice].Colum_Alzado[i].Traslapo = TraslapoFinal;

                    if (columna.Alzados[Indice].Colum_Alzado[i].UnitarioAdicional != null)
                    {
                        columna.Alzados[Indice].Colum_Alzado[i].UnitarioAdicional.Traslapo = FunctionsProject.FindTraslapo(columna.Alzados[Indice].Colum_Alzado[i].UnitarioAdicional.NoBarra, columna.Seccions[i].Item1.Material.FC);
                    }
                }
            }
        }

        public void EndCellEdit(int IndiceC, int IndiceR, bool isNotPaste, Columna ColumnaSelect)
        {

 
            if (IndiceR < ColumnaSelect.LuzLibre.Count && D_Alzado.Rows[IndiceR].Cells[IndiceC].Value != null)
            {
                string ValorCelda = D_Alzado.Rows[IndiceR].Cells[IndiceC].Value.ToString();
                object[] Clasficiacion = ClasificarCelda(ValorCelda);

                if (Clasficiacion[0].ToString() != "Error")
                {
                    int Cant_Barras = (int)Clasficiacion[1];
                    int NoBarra = (int)Clasficiacion[2];
                    int NoPiso = ColumnaSelect.LuzLibre.Count - IndiceR;
                    float H = ColumnaSelect.LuzLibre[IndiceR];
                    float Hviga = ColumnaSelect.VigaMayor.Seccions[IndiceR].Item1.H;
                    string T = (string)Clasficiacion[3];
                    string Tipo2 = (string)Clasficiacion[7];
                    float HV1, HV2, HVV1, HVV2, Hacum; bool UltimPiso = false;
                    Hacum = 0;
                    try { HV1 = ColumnaSelect.LuzLibre[IndiceR + 1]; HVV1 = ColumnaSelect.Seccions[IndiceR + 1].Item1.H; } catch { HV1 = 0; HVV1 = 0; }
                    try { HV2 = ColumnaSelect.LuzLibre[IndiceR - 1]; HVV2 = ColumnaSelect.Seccions[IndiceR - 1].Item1.H; } catch { HV2 = 0; HVV2 = 0; }

                    try
                    {
                        if (ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR + 1].Tipo == "A" && T == "Botton")
                        {
                            if (ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] != null)
                            {
                                D_Alzado.Rows[IndiceR].Cells[IndiceC].Value = ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR].ToString();
                            }
                            else
                            {
                                D_Alzado.Rows[IndiceR].Cells[IndiceC].Value = "";
                            }

                            MessageBox.Show("Existe superposición de refuerzo en el mismo punto.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch { }
                    try
                    {
                        if (ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR - 1].Tipo == "Botton" && T == "A")
                        {
                            if (ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] != null)
                            {
                                D_Alzado.Rows[IndiceR].Cells[IndiceC].Value = ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR].ToString();
                            }
                            else
                            {
                                D_Alzado.Rows[IndiceR].Cells[IndiceC].Value = "";
                            }

                            MessageBox.Show("Existe superposición de refuerzo en el mismo punto.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch { }

                    for (int i = ColumnaSelect.LuzLibre.Count - 1; i >= IndiceR; i--)
                    {
                        Hacum += ColumnaSelect.LuzLibre[i] + ColumnaSelect.VigaMayor.Seccions[i].Item1.H;
                    }
                    Hacum += Form1.Proyecto_.e_Fundacion;

                    if (NoPiso == ColumnaSelect.LuzLibre.Count)
                    {
                        UltimPiso = true;
                    }

                    AlzadoUnitario unitario = new AlzadoUnitario(Cant_Barras, NoBarra, T, NoPiso, IndiceC, H, Hviga, Form1.Proyecto_.e_Fundacion, UltimPiso, Hacum);
                    unitario.Tipo2 = Tipo2;
                    //AÑADIR REFUERZO ADICIONAL

                    string RefAd_Str = (string)Clasficiacion[4];

                    if (RefAd_Str == "-")
                    {
                        try
                        {
                            if (ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR + 1].Tipo == "A")
                            {
                                if (ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] != null)
                                {
                                    D_Alzado.Rows[IndiceR].Cells[IndiceC].Value = ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR].ToString();
                                }
                                else
                                {
                                    D_Alzado.Rows[IndiceR].Cells[IndiceC].Value = "";
                                }

                                DeterminarCoordAlzado(IndiceC);
                                ColumnaSelect.ActualizarRefuerzo();

                                MessageBox.Show("El Refuerzo adicional asignado no se puede agregar debido a que existe superposición de refuerzo en el mismo punto.", Form1.Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        catch { }

                        int CantBarrasA = (int)Clasficiacion[5];
                        int NoBarraA = (int)Clasficiacion[6];   //Cambio de ABotton a Botton
                        unitario.UnitarioAdicional = new AlzadoUnitario(CantBarrasA, NoBarraA, "Botton", NoPiso, IndiceC, H, Hviga, Form1.Proyecto_.e_Fundacion, UltimPiso, Hacum);
                    }

                    ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] = unitario;
                    ModificarTraslapo(IndiceC - 1, ColumnaSelect);

                    Form1.m_Informacion.MostrarAcero();

                    if (isNotPaste == false)
                    {
                        DeterminarCoordAlzado(IndiceC);
                        ColumnaSelect.ActualizarRefuerzo();
                        Form1.m_Informacion.MostrarAcero();
                        ColumnaSelect.CalcularPesoAcero(IndiceC - 1);
                        Form1.m_Despiece.Invalidate();
                    }
                }
                else
                {
                    ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] = null;
                    D_Alzado.Rows[IndiceR].Cells[IndiceC].Value = "";
                    if (isNotPaste == false)
                    {
                        ColumnaSelect.CalcularPesoAcero(IndiceC - 1);
                    }
                    Form1.m_Despiece.KgRefuerzo_L.Text = ColumnaSelect.KgRefuerzo.ToString();
                    Form1.m_Despiece.Invalidate();
                }
            }
            else if (IndiceR < ColumnaSelect.LuzLibre.Count)
            {
                ColumnaSelect.Alzados[IndiceC - 1].Colum_Alzado[IndiceR] = null;
                DeterminarCoordAlzado(IndiceC);
                ColumnaSelect.ActualizarRefuerzo();
                ColumnaSelect.CalcularPesoAcero(IndiceC - 1);
                Form1.m_Informacion.MostrarAcero();
                Form1.m_Despiece.KgRefuerzo_L.Text = ColumnaSelect.KgRefuerzo.ToString();
                Form1.m_Despiece.Invalidate();
            }
        }

        public void DeterminarCoordAlzado(int Col)
        {
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;
            float PR = Form1.Proyecto_.P_R;
            float eF = Form1.Proyecto_.e_Fundacion;
            float r = eF - PR;

            float r2 = Form1.Proyecto_.R/100;

            float LdAd = 0.4f;

            Alzado a = ColumnaSelect.Alzados[Col - 1];
            float diX = 0;
            //Agregar Distancia X a cada Alzado
            for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
            {
                AlzadoUnitario au = a.Colum_Alzado[i];
                if (au != null)
                {
                    au.x1 = diX;
                }

                diX += 0.1f;
                if (diX > 0.1f)
                {
                    diX = 0f;
                }
            }

            //Agregar Distancias entre Alzados
            for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
            {
                AlzadoUnitario au = a.Colum_Alzado[i];
                if (au != null)
                {
                    for (int j = a.Colum_Alzado.Count - 1; j >= i; j--)
                    {
                        AlzadoUnitario au2 = a.Colum_Alzado[j];

                        if (i != j && au2 != null)
                        {
                            if (au.Tipo == "T2" && au2.Tipo == "T2" || au.Tipo == "T2" && au2.Tipo == "T3" || au.Tipo == "T2" && au2.Tipo == "T1" || au.Tipo == "T2" && au2.Tipo == "T4")
                            {
                                if (au.x1 == au2.x1)
                                {
                                    au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                }
                            }

                            if (au.Tipo == "T4" && au2.Tipo == "T4")
                            {
                                au.x1 = au2.x1;
                            }

                            if (au.Tipo == "T1" && au2.Tipo == "T2" || au.Tipo == "T1" && au2.Tipo == "T4")
                            {
                                if (au.x1 == au2.x1)
                                {
                                    au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                }
                            }
                            if (au.Tipo == "T3" && au2.Tipo == "T3" || au.Tipo == "T3" && au2.Tipo == "T2" || au.Tipo == "T3" && au2.Tipo == "T4")
                            {
                                if (au.x1 == au2.x1)
                                {
                                    au.x1 = au.x1 == 0.1f ? 0 : (float)0.1;
                                }
                            }

                        }
                    }
                }
            }

            for (int i = a.Colum_Alzado.Count - 1; i >= 0; i--)
            {
                AlzadoUnitario au = a.Colum_Alzado[i];

                if (au != null)
                {
                    float Hacum1 = 0; float Hviga1 = 0; float Hacum2 = 0; float Hviga2 = 0; float H1 = 0; float H2 = 0; string Nom3 = "Not"; string Nom4 = "Not"; float x31 = 0; float x41 = 0;
                    string Nom1 = "Not"; string Nom2 = "Not"; float x11 = 0; float x12 = 0;
                    au.Coord_Alzado_PB = new List<float[]>();

                    #region Determinar Variables de Pisos Vecinos

                    //Determinar Variables de Pisos Vecinos

                    try
                    {
                        Nom3 = a.Colum_Alzado[i + 2].Tipo;
                        x31 = a.Colum_Alzado[i + 2].x1;
                    }
                    catch
                    { }

                    try
                    {
                        Nom4 = a.Colum_Alzado[i - 2].Tipo;
                        x41 = a.Colum_Alzado[i - 2].x1;
                    }
                    catch
                    { }

                    try
                    {
                        Nom1 = a.Colum_Alzado[i + 1].Tipo;
                        x11 = a.Colum_Alzado[i + 1].x1;
                    }
                    catch { }

                    try
                    {
                        Nom2 = a.Colum_Alzado[i - 1].Tipo;
                        x12 = a.Colum_Alzado[i - 1].x1;
                    }
                    catch { }

                    try
                    {
                        Hacum1 = ColumnaSelect.LuzAcum[i + 1];
                        Hviga1 = ColumnaSelect.VigaMayor.Seccions[i + 1].Item1.H;
                        H1 = ColumnaSelect.LuzLibre[i + 1];
                    }
                    catch { }
                    try
                    {
                        Hacum2 = ColumnaSelect.LuzAcum[i - 1];
                        Hviga2 = ColumnaSelect.VigaMayor.Seccions[i - 1].Item1.H;
                        H2 = ColumnaSelect.LuzLibre[i - 1];
                    }
                    catch { }

                    #endregion Determinar Variables de Pisos Vecinos

                    float DisG = Form1.Proyecto_.G90[au.NoBarra];
                    if (au.NoStory == 1 && au.Tipo == "T1") //Si es Primer Piso y  Si Tiene Traslapo Tipo1
                    {

                      
                        float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                        float[] XY2 = new float[] { au.x1 + a.DistX, r };
                        float[] XY3 = new float[] { au.x1 + a.DistX, eF + au.H_Stroy / 2 + au.Traslapo / 2 };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);
                    }

                    if (au.NoStory != 1 && au.Tipo == "T1")
                    {

                        float[] XY1 = new float[] { au.x1 + a.DistX, au.Hacum - au.Hviga - au.H_Stroy / 2 - au.Traslapo / 2 };
                        float[] XY2= new float[] { au.x1 + a.DistX, au.Hacum - r2 };
                        float[] XY3 = new float[] { au.x1 + a.DistX + DisG, au.Hacum - r2 };

                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);
                    }
                    else if (au.NoStory == 1 && au.Tipo == "T3") //Si es Primer Piso y  Si Tiene Traslapo Tipo 3
                    {
                        float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                        float[] XY2 = new float[] { au.x1 + a.DistX, r };
                        float[] XY3 = new float[] { au.x1 + a.DistX, Hacum2 - Hviga2 - H2 / 2 + au.Traslapo / 2 };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);
                    }
                    else if (au.NoStory != 1 && au.Tipo == "T3") // Traslapo Tipo 3
                    {
                     
                        float[] XY1 = new float[] { au.x1 + a.DistX, Hacum1 - Hviga1 - H1 / 2 - au.Traslapo / 2 };
                        float[] XY2 = new float[] { au.x1 + a.DistX, au.Hacum - r2 };
                        float[] XY3 = new float[] { au.x1 + a.DistX + DisG, au.Hacum - r2 };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); ; au.Coord_Alzado_PB.Add(XY3);
                    }

                    if (au.UltPiso == false && au.NoStory != 1 && au.Tipo == "T2")
                    {
                        float[] XY1 = new float[] { au.x1 + a.DistX, au.Hacum + H2 / 2 + au.Traslapo / 2 };
                        float[] XY2 = new float[] { au.x1 + a.DistX, Hacum1 - Hviga1 - H1 / 2 - au.Traslapo / 2 };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                    }

                    if (au.NoStory == 1 && au.Tipo == "T4") //Traslapo Tipo 4
                    {
                        if (Nom2 != "T4")
                        {
                            float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                            float[] XY2 = new float[] { au.x1 + a.DistX, r };
                            float[] XY3 = new float[] { au.x1 + a.DistX, au.Hacum - r2 };
                            float[] XY4 = new float[] { au.x1 + a.DistX + DisG, au.Hacum -r2 };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3); au.Coord_Alzado_PB.Add(XY4);
                        }
                        else
                        {
                            float[] XY1 = new float[] { au.x1 + a.DistX + DisG, r };
                            float[] XY2 = new float[] { au.x1 + a.DistX, r };
                            float[] XY3 = new float[] { au.x1 + a.DistX, Hacum2 - r2 };
                            float[] XY4 = new float[] { au.x1 + a.DistX + DisG, Hacum2 - r2 };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3); au.Coord_Alzado_PB.Add(XY4);
                        }
                    }

                    if (au.UltPiso == false && au.Tipo == "A")  //Refuerzo Adicional Parte Superior "Cualquier Piso"
                    {
                        if (Nom1 == "A")
                        {
                            if (a.Colum_Alzado[i + 1].CantBarras + "#" + a.Colum_Alzado[i + 1].NoBarra == au.CantBarras + "#" + au.NoBarra)
                            {
                                if (a.Colum_Alzado[i + 1].Traslapo + LdAd + au.Traslapo + LdAd >= au.H_Stroy)
                                {
                                    if (a.Colum_Alzado[i + 1].Tipo2 == "+")
                                    {
                                        a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();
                                        float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                        float[] XY2 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum - au.Traslapo };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                    else
                                    {
                                        a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();
                                        float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                        float[] XY2 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum - a.Colum_Alzado[i + 1].Hviga - au.Traslapo - LdAd };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                }
                                else

                                {
                                    if (au.Tipo2 == "-")
                                    {
                                        float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum - au.Hviga + au.Traslapo };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                    else if (au.Tipo2 == "+")
                                    {
                                        float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum - au.Traslapo };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                    else
                                    {
                                        float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                    }
                                }
                            }
                            else
                            {
                                if (au.Tipo2 == "-")
                                {
                                    float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - au.Hviga + au.Traslapo };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                }
                                else if (au.Tipo2 == "+")
                                {
                                    float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - au.Traslapo };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                }
                                else
                                {
                                    float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                                }
                            }
                        }
                        else
                        {
                            if (au.Tipo2 == "-")
                            {
                                float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, au.Hacum - au.Hviga + au.Traslapo };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                            else if (au.Tipo2 == "+")
                            {
                                float[] XY1 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                float[] XY2 = new float[] { a.DistX, au.Hacum - au.Traslapo };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                            else
                            {
                                float[] XY1 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                        }
                    }

                    if (au.NoStory != 1 && au.UnitarioAdicional != null)  //Refuerzo Adicional Parte Inferior "Cualquier Piso"
                    {
                        if (au.CantBarras + "#" + au.NoBarra == au.UnitarioAdicional.CantBarras + "#" + au.UnitarioAdicional.NoBarra & au.UnitarioAdicional.UltPiso == false)
                        {
                            if (au.Traslapo + LdAd + au.UnitarioAdicional.Traslapo + LdAd >= au.H_Stroy)
                            {
                                au.Coord_Alzado_PB = new List<float[]>();
                                au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, au.Hacum + au.UnitarioAdicional.Traslapo + LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                            else
                            {
                                au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, Hacum1 + au.UnitarioAdicional.Traslapo + LdAd };
                                au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2);
                            }
                        }
                        else
                        {
                            au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                            float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                            float[] XY2 = new float[] { a.DistX, Hacum1 + au.UnitarioAdicional.Traslapo + LdAd };
                            au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2);
                        }
                    }
                    if (au.NoStory == 1 && au.UnitarioAdicional != null)  //Primer Piso Con Refuerzo Adicional Parte Inferior
                    {
                        if (au.CantBarras + "#" + au.NoBarra == au.UnitarioAdicional.CantBarras + "#" + au.UnitarioAdicional.NoBarra)
                        {
                            if (au.Traslapo + LdAd + au.UnitarioAdicional.Traslapo + LdAd >= au.H_Stroy)
                            {
                                au.Coord_Alzado_PB = new List<float[]>();
                                au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                float[] XY1 = new float[] { a.DistX + DisG, r };
                                float[] XY2 = new float[] { a.DistX, r };
                                float[] XY3 = new float[] { a.DistX, au.Hacum + au.Traslapo + LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                            }
                            else
                            {
                                au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                float[] XY1 = new float[] { a.DistX + DisG, r };
                                float[] XY2 = new float[] { a.DistX, r };
                                float[] XY3 = new float[] { a.DistX, eF + au.UnitarioAdicional.Traslapo + LdAd };
                                au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY3);
                            }
                        }
                        else
                        {
                            au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                            float[] XY1 = new float[] { a.DistX + DisG, r };
                            float[] XY2 = new float[] { a.DistX, r };
                            float[] XY3 = new float[] { a.DistX, eF + au.UnitarioAdicional.Traslapo + LdAd };
                            au.UnitarioAdicional.Coord_Alzado_PB.Add(XY1); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY2); au.UnitarioAdicional.Coord_Alzado_PB.Add(XY3);
                        }
                    }

                    if (au.UltPiso && au.Tipo == "A")   // Ultimo Piso Con Refuerzo Adicional Parte Superior
                    {
                        if (Nom1 == "A")
                        {
                            if (a.Colum_Alzado[i + 1].CantBarras + "#" + a.Colum_Alzado[i + 1].NoBarra == au.CantBarras + "#" + au.NoBarra)
                            {
                                if (a.Colum_Alzado[i + 1].Traslapo + LdAd + au.Traslapo + LdAd >= au.H_Stroy)
                                {
                                    if (a.Colum_Alzado[i + 1].Tipo2 == "+")
                                    {
                                        a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();
                                        float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                        float[] XY3 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum - au.Traslapo };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                    }
                                    else
                                    {
                                        a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();
                                        float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                        float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                        float[] XY3 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum - a.Colum_Alzado[i + 1].Hviga - au.Traslapo - LdAd };
                                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                    }
                                }
                                else
                                {
                                    float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                    float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                }
                            }
                            else
                            {
                                float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                            }
                        }
                        else if (au.UnitarioAdicional != null)
                        {
                            if (au.UnitarioAdicional.CantBarras + "#" + au.UnitarioAdicional.NoBarra == au.CantBarras + "#" + au.NoBarra)
                            {
                                if (au.Traslapo + LdAd + au.Traslapo + LdAd >= au.H_Stroy)
                                {
                                    au.UnitarioAdicional.Coord_Alzado_PB = new List<float[]>();
                                    float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                    float[] XY3 = new float[] { a.DistX, Hacum1 - Hviga1 - au.UnitarioAdicional.Traslapo - LdAd };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                }
                                else
                                {
                                    float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                    float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                    float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                    au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                                }
                            }
                            else
                            {
                                float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                                float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                                float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                            }
                        }
                        else
                        {
                            float[] XY1 = new float[] { a.DistX + DisG, au.Hacum - r2 };
                            float[] XY2 = new float[] { a.DistX, au.Hacum - r2 };
                            float[] XY3 = new float[] { a.DistX, au.Hacum - au.Hviga - au.Traslapo - LdAd };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                        }
                    }

                    if (au.NoStory != 1 && au.Tipo == "Botton")  //Refuerzo Adicional Parte Inferior "Cualquier Piso"
                    {
                        if (Nom1 == "Botton")
                        {
                            if (au.ToString() == a.Colum_Alzado[i + 1].ToString() && a.Colum_Alzado[i + 1].NoStory == 1 && au.Traslapo + a.Colum_Alzado[i + 1].Traslapo + LdAd * 2 >= a.Colum_Alzado[i + 1].H_Stroy)
                            {
                                au.Coord_Alzado_PB = new List<float[]>();
                                a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();

                                float[] XY1 = new float[] { a.DistX + DisG, r };
                                float[] XY2 = new float[] { a.DistX, r };
                                float[] XY3 = new float[] { a.DistX, a.Colum_Alzado[i + 1].Hacum + au.Traslapo + LdAd };
                                a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY1); a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY2); a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY3);
                            }
                            else if (au.ToString() == a.Colum_Alzado[i + 1].ToString() && a.Colum_Alzado[i + 1].NoStory != 1 && au.Traslapo + a.Colum_Alzado[i + 1].Traslapo + LdAd * 2 >= a.Colum_Alzado[i + 1].H_Stroy)
                            {
                                au.Coord_Alzado_PB = new List<float[]>();
                                a.Colum_Alzado[i + 1].Coord_Alzado_PB.Clear();

                                float[] XY1 = new float[] { a.DistX, a.Colum_Alzado[i - 1].Hacum - a.Colum_Alzado[i - 1].Hviga - LdAd - au.Traslapo };
                                float[] XY2 = new float[] { a.DistX, au.Hacum + LdAd + au.Traslapo };
                                a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY1); a.Colum_Alzado[i + 1].Coord_Alzado_PB.Add(XY2);
                            }
                            else
                            {
                                float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                                float[] XY2 = new float[] { a.DistX, Hacum1 + au.Traslapo + LdAd };
                                au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                            }
                        }
                        else
                        {
                            float[] XY1 = new float[] { a.DistX, Hacum1 - Hviga1 - au.Traslapo - LdAd };
                            float[] XY2 = new float[] { a.DistX, Hacum1 + au.Traslapo + LdAd };
                            au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2);
                        }
                    }

                    if (au.NoStory == 1 && au.Tipo == "Botton")  //Primer Piso Con Refuerzo Adicional Parte Inferior
                    {
                        float[] XY1 = new float[] { a.DistX + DisG, r };
                        float[] XY2 = new float[] { a.DistX, r };
                        float[] XY3 = new float[] { a.DistX, eF + au.Traslapo + LdAd };
                        au.Coord_Alzado_PB.Add(XY1); au.Coord_Alzado_PB.Add(XY2); au.Coord_Alzado_PB.Add(XY3);
                    }



                    if (Form1.Proyecto_.Redondear)
                    {
                        ModificarCoordParaEntero(au);
                        if (au.UnitarioAdicional != null)
                        {
                            ModificarCoordParaEntero(au.UnitarioAdicional);
                        }

                    }


                }
            }

      
        }

        private void ModificarCoordParaEntero( AlzadoUnitario au)
        {
            if (au.Coord_Alzado_PB != null)
            {
                if (au.Coord_Alzado_PB.Count != 0)
                {
                    float LongitudInicial = (float)Math.Round(au.CalcularLongitudRefuerzo(au.Coord_Alzado_PB), 2);
                    float LongitudQueDebeSer = FunctionsProject.RedondearDecimales(LongitudInicial, 5);

                    if (LongitudInicial != LongitudQueDebeSer)
                    {
                        float Faltante = (float)Math.Round(Math.Abs(LongitudInicial - LongitudQueDebeSer),2);

                        if (au.Coord_Alzado_PB.Count == 2)
                        {

                            if (au.Coord_Alzado_PB[0][1] > au.Coord_Alzado_PB[1][1])
                            {
                                au.Coord_Alzado_PB[0][1] += Faltante / 2;
                                au.Coord_Alzado_PB[1][1] -= Faltante / 2;
                            }
                            else
                            {
                                au.Coord_Alzado_PB[0][1] -= Faltante / 2;
                                au.Coord_Alzado_PB[1][1] += Faltante / 2;
                            }

                        }
                        if (au.Coord_Alzado_PB.Count == 3)
                        {

                            if (au.NoStory != 1)
                            {
                                if (au.Coord_Alzado_PB[2][1] > au.Coord_Alzado_PB[0][1])
                                {

                                    au.Coord_Alzado_PB[0][1] -= Faltante;
                                }
                                else
                                {
                                    au.Coord_Alzado_PB[2][1] -= Faltante;
                                }

                          
                            }
                            else
                            {
                                au.Coord_Alzado_PB[2][1] += Faltante;
                            }
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
                int CantidadBarrasA = 0;
                int NoBarraA = 0;
                string Raya = "";
                string Tipo2 = "";

                int VecesNumeral = 0;
                for (int i = 0; i < Celda.Length; i++)
                {
                    if (Celda.Substring(i, 1) == "#" && VecesNumeral < 1)
                    {
                        VecesNumeral += 1;
                        try
                        {
                            CantidadBarras = Convert.ToInt32(Celda.Substring(0, i));
                            if (Celda.Substring(0, 1) == "-" | Celda.Substring(0, 1) == "+")
                            {
                                Tipo2 = Celda.Substring(0, 1);
                            }
                        }
                        catch
                        {
                            CantidadBarras = 0;
                        }
                        try
                        {
                            NoBarra = Convert.ToInt32(Celda.Substring(i + 1, 2));
                        }
                        catch
                        {
                            try
                            {
                                NoBarra = Convert.ToInt32(Celda.Substring(i + 1, 1));
                            }
                            catch { NoBarra = 0; }
                        }
                    }
                    if (Celda.Substring(i, 1) == "T")
                    {
                        Traslap = Celda.Substring(i);
                    }

                    if (Celda.Substring(i, 1) == "A")
                    {
                        Traslap = Celda.Substring(i, 1);

                        string AuxAd = Celda.Substring(i);

                        if (AuxAd.Contains("-"))
                        {
                            Raya = "-";
                            for (int j = 0; j < AuxAd.Length; j++)
                            {
                                if (AuxAd.Substring(j, 1) == "#")
                                {
                                    int InicioRaya = AuxAd.IndexOf("-") + 1;

                                    CantidadBarrasA = Convert.ToInt32(AuxAd.Substring(InicioRaya, j - InicioRaya));
                                    try
                                    {
                                        NoBarraA = Convert.ToInt32(AuxAd.Substring(j + 1, 2));
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            NoBarraA = Convert.ToInt32(AuxAd.Substring(j + 1, 1));
                                        }
                                        catch { NoBarraA = 0; }
                                    }
                                }
                            }
                        }
                    }
                }
                try
                {
                    var o = Form1.Proyecto_.AceroBarras[NoBarra];
                    if (Raya == "-") { var m = Form1.Proyecto_.AceroBarras[NoBarraA]; }
                    if (CantidadBarras < 0)
                    {
                        CantidadBarras = -CantidadBarras;
                        if (Traslap != "A")
                        {
                            Traslap = "Botton";
                            Tipo2 = "";
                        }
                        else
                        {
                            Traslap = "A";
                        }
                    }
                    Clasf = new object[] { "Ok", CantidadBarras, NoBarra, Traslap, Raya, CantidadBarrasA, NoBarraA, Tipo2 };
                }
                catch
                {
                }
            }

            return Clasf;
        }

        private void D_Alzado_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Form1.Proyecto_.ColumnaSelect != null)
            {
                EndCellEdit(e.ColumnIndex, e.RowIndex, false, Form1.Proyecto_.ColumnaSelect);
            }
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

                EndCellEdit(D_Alzado.SelectedCells[i].ColumnIndex, D_Alzado.SelectedCells[i].RowIndex, true, Form1.Proyecto_.ColumnaSelect);
            }

            for (int col = 0; col < Form1.Proyecto_.ColumnaSelect.Alzados.Count; col++)
            {
                DeterminarCoordAlzado(col + 1);
                Form1.Proyecto_.ColumnaSelect.ActualizarRefuerzo();
                Form1.Proyecto_.ColumnaSelect.CalcularPesoAcero();
                Form1.m_Despiece.Invalidate();
            }

            Form1.m_Informacion.MostrarAcero();
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

            for (int Col = c; Col < data.Columns.Count; Col++)
            {
                for (int Row = r; Row < rowsInClipboard.Length + r; Row++)
                {
                    if (Col > 0)
                    {
                        if (Form1.Proyecto_.ColumnaSelect != null)
                        {
                            EndCellEdit(Col, Row, true, Form1.Proyecto_.ColumnaSelect);
                        }
                    }
                }
                DeterminarCoordAlzado(Col);
                Form1.Proyecto_.ColumnaSelect.ActualizarRefuerzo();
                Form1.Proyecto_.ColumnaSelect.ActualizarRefuerzo();
                Form1.m_Informacion.MostrarAcero();
                Form1.Proyecto_.ColumnaSelect.CalcularPesoAcero();
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

        private void D_Alzado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.RowIndex == -1)
            {
                Form Form_RefuerzoBase = new AgregarRefuerzoBase();
                AgregarRefuerzoBase.IndiceC = e.ColumnIndex;
                Form_RefuerzoBase.ShowDialog();
            }
            if (e.ColumnIndex != 0 && e.RowIndex != -1 && D_Alzado.SelectedCells.Count==1)
            {

                if (refuerzoAdicional!=null)
                {
                    int RowCorrespondiente = D_Alzado.SelectedCells[0].RowIndex;
                    string Piso = Form1.Proyecto_.ColumnaSelect.Seccions[RowCorrespondiente].Item2;

                    AyudaAgregarRefuerzoAdicional.PisoCorrespondiente = Piso;
                    AyudaAgregarRefuerzoAdicional.AlzadoCorrespondiente = D_Alzado.SelectedCells[0].ColumnIndex - 1;
                    AyudaAgregarRefuerzoAdicional.RowCorrespondiente = RowCorrespondiente;
                    refuerzoAdicional.Invalidate();
                }


            }



         }

        private void AsignarRefuerzoToolStripMenuItem_Click(object sender, EventArgs e)
        {

           var a =D_Alzado.SelectedCells;

            List<int> Filas_numeros = new List<int>();
            List<int> Columnas_numeros = new List<int>();

            foreach(DataGridViewTextBoxCell boxCell in D_Alzado.SelectedCells)
            {
                if (Filas_numeros.Exists(x=>x== boxCell.RowIndex ) == false)
                {
                   
                   Filas_numeros.Add(boxCell.RowIndex); 
                }
                if (Columnas_numeros.Exists(x => x == boxCell.ColumnIndex-1) == false && boxCell.ColumnIndex-0>=0)
                {

                    Columnas_numeros.Add(boxCell.ColumnIndex-1);
                }
            }
            Filas_numeros= Filas_numeros.OrderByDescending(x=>x).ToList();
            Columnas_numeros = Columnas_numeros.OrderBy(x => x).ToList();

            AgregarRefuerzoBase @base = new AgregarRefuerzoBase();
            AgregarRefuerzoBase.Columnas = Columnas_numeros;
            AgregarRefuerzoBase.Filas = Filas_numeros;
            AgregarRefuerzoBase.EditarAlgunosAlzados = true;
            @base.ShowDialog();


        }
       private  AyudaAgregarRefuerzoAdicional refuerzoAdicional;

        private void AsignarRefuerzoAdicionalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (D_Alzado.SelectedCells.Count == 1)
            {
                int RowCorrespondiente = D_Alzado.SelectedCells[0].RowIndex;

                string Piso = Form1.Proyecto_.ColumnaSelect.Seccions[RowCorrespondiente].Item2;
                

                refuerzoAdicional = new AyudaAgregarRefuerzoAdicional();
                AyudaAgregarRefuerzoAdicional.PisoCorrespondiente = Piso;
                AyudaAgregarRefuerzoAdicional.AlzadoCorrespondiente= D_Alzado.SelectedCells[0].ColumnIndex-1;
                AyudaAgregarRefuerzoAdicional.RowCorrespondiente = RowCorrespondiente;
                refuerzoAdicional.Show();
            }
        }

        private void D_Alzado_KeyPress(object sender, KeyPressEventArgs e)
        {
      
        }

        private void D_Alzado_KeyDown(object sender, KeyEventArgs e)
        {

      
        }

        private void D_Alzado_KeyUp(object sender, KeyEventArgs e)
        {
            if (D_Alzado.SelectedCells.Count == 1)
            {
                if (D_Alzado.SelectedCells[0].ColumnIndex>0)
                { 
                if (refuerzoAdicional != null)
                {
                    int RowCorrespondiente = D_Alzado.SelectedCells[0].RowIndex;
                    string Piso = Form1.Proyecto_.ColumnaSelect.Seccions[RowCorrespondiente].Item2;

                    AyudaAgregarRefuerzoAdicional.PisoCorrespondiente = Piso;
                    AyudaAgregarRefuerzoAdicional.AlzadoCorrespondiente = D_Alzado.SelectedCells[0].ColumnIndex - 1;
                    AyudaAgregarRefuerzoAdicional.RowCorrespondiente = RowCorrespondiente;
                    refuerzoAdicional.Invalidate();
                }


            }
            }
        }
    }
}