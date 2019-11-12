using DisenoColumnas.Clases;
using DisenoColumnas.DefinirColumnas;
using DisenoColumnas.Diseño;
using DisenoColumnas.Interfaz_Inicial;
using DisenoColumnas.Interfaz_Seccion;
using DisenoColumnas.InterfazViewInfo;
using DisenoColumnas.Secciones;
using DisenoColumnas.Secciones_Predefinidas;
using DisenoColumnas.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas
{
    public partial class Form1 : Form
    {
        private PlantaColumnas m_PlantaColumnas;
        public static Informacion m_Informacion;
        public static Despiece m_Despiece;
        public static VariablesdeEntrada variablesdeEntrada;
        public static ComboBox mLcolumnas;
        public static CuantiaVolumetrica mCuantiaVolumetrica;
        public static FInterfaz_Seccion mIntefazSeccion;
        public static AgregarAlzado mAgregarAlzado;
        public static FuerzasEnElementos mFuerzasEnElmentos;
        public bool CancelDiseño = false;
        public bool CancelGarfica = false;

        private DeserializeDockContent m_deserializeDockContent;

        public static Form1 mFormPrincipal;

        private List<string> ArchivoE2K2009ETABS;
        private List<string> ArchivoResultados2009;
        private List<string> ArchivoFuerzasColumnas2009;

        public static Proyecto Proyecto_;
        public static string TColumna;
        public static CLista_Secciones secciones_predef;

        public Form1()
        {
            InitializeComponent();
            mFormPrincipal = this;
            CargarToolTips();

            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Minimized_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Panel5_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Button_MaxRest_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                Button_MaxRest.Image = Properties.Resources.Restaurar14x11;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                Button_MaxRest.Image = Properties.Resources.Maximizar14X11;
            }
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject();

            //Falta resultados
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void OpenProject()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DMC |*.Colum";
            openFileDialog.Title = "Abrir Proyecto";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                if (Proyecto_ != null)
                {
                    DialogResult box;
                    box = MessageBox.Show("¿Desea guardar cambios en el Proyecto: " + Proyecto_.Name + "?", Proyecto_.Empresa, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (box == DialogResult.Yes)
                    {
                        Save();
                    }
                }
                FunctionsProject.Deserealizar(openFileDialog.FileName, ref Proyecto_);

                CloseWindows();
                m_Informacion = null; m_Despiece = null; mCuantiaVolumetrica = null; mAgregarAlzado = null;
                mFuerzasEnElmentos = null;

                L_NameProject.Visible = true;
                L_NameProject.Text = Proyecto_.Name;

                string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.temp.config");
                try
                {
                    PanelContenedor.LoadFromXml(configFile, m_deserializeDockContent);
                }
                catch { }
                m_Informacion = new Informacion();
                try
                {
                    m_Informacion.Show(PanelContenedor);
                }
                catch { }
                mAgregarAlzado = new AgregarAlzado();
                try { mAgregarAlzado.Show(PanelContenedor); } catch { }

                mCuantiaVolumetrica = new CuantiaVolumetrica();
                mCuantiaVolumetrica.Show(PanelContenedor);
                mLcolumnas = LColumna;
                m_Despiece = new Despiece();
                m_Despiece.Show(PanelContenedor);
                mFuerzasEnElmentos = new FuerzasEnElementos();
                mFuerzasEnElmentos.Show(PanelContenedor);

                LColumna.Items.Clear();
                LColumna.Text = "";
                LColumna.Items.AddRange(Proyecto_.Lista_Columnas.Select(x => x.Name).ToArray());

                if (Proyecto_.ColumnaSelect != null)
                {
                    LColumna.Text = Proyecto_.ColumnaSelect.Name;
                }

                variablesdeEntrada = new VariablesdeEntrada(true);
                LColumna.Enabled = true;
                La_Column.Enabled = true;

                CreateDidctonaries();
                WindowState = FormWindowState.Maximized;
            }
        }

        private void CargarToolTips()
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(Save_B, "Guardar (Ctrl + S)");
            tool.SetToolTip(SaveAs_B, "Guardar (Ctrl + Mayús + S)");
            tool.SetToolTip(Cuantia_Vol_Button, "Calcular Cuantía Volumétrica");
            tool.SetToolTip(Button_Agregar, "Agregar Nuevo Alzado (Ctrl + A)");
            tool.SetToolTip(Disenar, "Diseñar Columnas (Ctrl + D)");
        }

        private void CloseWindows()
        {
            if (m_PlantaColumnas != null)
            {
                m_PlantaColumnas.DockHandler.DockPanel = null;
            }
            m_PlantaColumnas = new PlantaColumnas();
            //m_PlantaColumnas.Show(PanelContenedor);

            if (m_Informacion != null)
            {
                m_Informacion.DockHandler.DockPanel = null;
            }

            if (m_Despiece != null)
            {
                m_Despiece.DockHandler.DockPanel = null;
            }

            if (mCuantiaVolumetrica != null)
            {
                mCuantiaVolumetrica.DockHandler.DockPanel = null;
            }

            if (mAgregarAlzado != null)
            {
                mAgregarAlzado.DockHandler.DockPanel = null;
            }
            if (mFuerzasEnElmentos != null)
            {
                mFuerzasEnElmentos.DockHandler.DockPanel = null;
            }
        }

        private void SaveAs()
        {
            if (Proyecto_ != null)
            {
                SaveFileDialog SaveFile = new SaveFileDialog();
                SaveFile.Filter = "DMC |*.Colum";
                SaveFile.Title = "Guardar Proyecto";
                SaveFile.ShowDialog();

                if (SaveFile.FileName != "")
                {
                    Proyecto_.Ruta = SaveFile.FileName;
                    Proyecto_.Name = Path.GetFileName(SaveFile.FileName).Replace(".Colum", "");
                    L_NameProject.Text = Proyecto_.Name;
                    FunctionsProject.Serializar(Proyecto_.Ruta, Proyecto_);
                    string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.temp.config");
                    PanelContenedor.SaveAsXml(configFile);
                }
            }
        }

        private void Save()
        {
            if (Proyecto_ != null)
            {
                if (Proyecto_.Ruta != "")
                {
                    FunctionsProject.Serializar(Proyecto_.Ruta, Proyecto_);
                    string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.temp.config");

                    PanelContenedor.SaveAsXml(configFile);
                }
                else
                {
                    SaveAs();
                }
            }
        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void NewProject()
        {
            string Ruta1, Ruta2, Ruta3;

            if (Proyecto_ != null)
            {
                if (Proyecto_.AlturaEdificio != 0)
                {
                    if (Proyecto_.Ruta == "" | Proyecto_.Ruta != "")
                    {
                        DialogResult box;
                        box = MessageBox.Show("¿Desea guardar cambios en el Proyecto: " + Proyecto_.Name + "?", Proyecto_.Empresa, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (box == DialogResult.Yes)
                        {
                            Save();
                        }
                    }
                }
            }
            Ruta1 = AbrirE2K2009yCSV2009();
            Ruta2 = AsingarResultadosaColumna();
            Ruta3 = AsingarFuerzasColumnas();

            if (Ruta1 != "" && Ruta2 != "" & Ruta3 != "")
            {
                L_NameProject.Visible = true;
                L_NameProject.Text = Proyecto_.Name = "Proyecto1";
                CloseWindows();
                WindowState = FormWindowState.Maximized;
                m_Informacion = null; m_Despiece = null; mCuantiaVolumetrica = null; mAgregarAlzado = null;
                mFuerzasEnElmentos = null;

                variablesdeEntrada = new VariablesdeEntrada(false);
                variablesdeEntrada.ShowDialog();

                m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
                string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.temp.config");
                try { PanelContenedor.LoadFromXml(configFile, m_deserializeDockContent); } catch { }

                mLcolumnas = LColumna;
                m_PlantaColumnas = new PlantaColumnas();
                //   m_PlantaColumnas.Show(PanelContenedor);
                m_Informacion = new Informacion();
                m_Informacion.Show(PanelContenedor);

                m_Despiece = new Despiece();
                m_Despiece.Show(PanelContenedor);

                mCuantiaVolumetrica = new CuantiaVolumetrica();
                mCuantiaVolumetrica.Show(PanelContenedor);

                mFuerzasEnElmentos = new FuerzasEnElementos();
                mFuerzasEnElmentos.Show(PanelContenedor);

                mAgregarAlzado = new AgregarAlzado();
                LColumna.Enabled = true;
                La_Column.Enabled = true;
                LColumna.Items.Clear();
                LColumna.Text = "";
                LColumna.Items.AddRange(Proyecto_.Lista_Columnas.Select(x => x.Name).ToArray());
            }
        }

        private void CreateDidctonaries()
        {
            #region Area refuerzo

            //Unidades en m²
            Proyecto_.AceroBarras = new Dictionary<int, double>();
            Proyecto_.AceroBarras.Add(2, 0.32 / 10000);
            Proyecto_.AceroBarras.Add(3, 0.71 / 10000);
            Proyecto_.AceroBarras.Add(4, 1.29 / 10000);
            Proyecto_.AceroBarras.Add(5, 1.99 / 10000);
            Proyecto_.AceroBarras.Add(6, 2.84 / 10000);
            Proyecto_.AceroBarras.Add(7, 3.87 / 10000);
            Proyecto_.AceroBarras.Add(8, 5.10 / 10000);
            Proyecto_.AceroBarras.Add(10, 8.09 / 10000);

            #endregion Area refuerzo

            Proyecto_.Ld_210 = new Dictionary<int, float>();
            Proyecto_.Ld_210.Add(2, 0.55f);
            Proyecto_.Ld_210.Add(3, 0.55f);
            Proyecto_.Ld_210.Add(4, 0.75f);
            Proyecto_.Ld_210.Add(5, 0.9f);
            Proyecto_.Ld_210.Add(6, 1.1f);
            Proyecto_.Ld_210.Add(7, 1.6f);
            Proyecto_.Ld_210.Add(8, 1.8f);
            Proyecto_.Ld_210.Add(9, 2.05f);
            Proyecto_.Ld_210.Add(10, 2.3f);
            Proyecto_.Ld_210.Add(11, 2.55f);
            Proyecto_.Ld_210.Add(14, 3.1f);

            Proyecto_.Ld_280 = new Dictionary<int, float>();
            Proyecto_.Ld_280.Add(2, 0.5f);
            Proyecto_.Ld_280.Add(3, 0.5f);
            Proyecto_.Ld_280.Add(4, 0.65f);
            Proyecto_.Ld_280.Add(5, 0.8f);
            Proyecto_.Ld_280.Add(6, 0.95f);
            Proyecto_.Ld_280.Add(7, 1.4f);
            Proyecto_.Ld_280.Add(8, 1.6f);
            Proyecto_.Ld_280.Add(9, 1.8f);
            Proyecto_.Ld_280.Add(10, 2.0f);
            Proyecto_.Ld_280.Add(11, 2.2f);
            Proyecto_.Ld_280.Add(14, 2.65f);

            Proyecto_.Ld_350 = new Dictionary<int, float>();
            Proyecto_.Ld_350.Add(2, 0.45f);
            Proyecto_.Ld_350.Add(3, 0.45f);
            Proyecto_.Ld_350.Add(4, 0.55f);
            Proyecto_.Ld_350.Add(5, 0.7f);
            Proyecto_.Ld_350.Add(6, 0.85f);
            Proyecto_.Ld_350.Add(7, 1.25f);
            Proyecto_.Ld_350.Add(8, 1.4f);
            Proyecto_.Ld_350.Add(9, 1.6f);
            Proyecto_.Ld_350.Add(10, 1.8f);
            Proyecto_.Ld_350.Add(11, 2.0f);
            Proyecto_.Ld_350.Add(14, 2.4f);

            Proyecto_.Ld_420 = new Dictionary<int, float>();
            Proyecto_.Ld_420.Add(2, 0.4f);
            Proyecto_.Ld_420.Add(3, 0.4f);
            Proyecto_.Ld_420.Add(4, 0.55f);
            Proyecto_.Ld_420.Add(5, 0.65f);
            Proyecto_.Ld_420.Add(6, 0.8f);
            Proyecto_.Ld_420.Add(7, 1.15f);
            Proyecto_.Ld_420.Add(8, 1.3f);
            Proyecto_.Ld_420.Add(9, 1.45f);
            Proyecto_.Ld_420.Add(10, 1.65f);
            Proyecto_.Ld_420.Add(11, 1.8f);
            Proyecto_.Ld_420.Add(14, 2.2f);

            Proyecto_.Ld_490 = new Dictionary<int, float>();
            Proyecto_.Ld_490.Add(2, 0.4f);
            Proyecto_.Ld_490.Add(3, 0.4f);
            Proyecto_.Ld_490.Add(4, 0.5f);
            Proyecto_.Ld_490.Add(5, 0.6f);
            Proyecto_.Ld_490.Add(6, 0.75f);
            Proyecto_.Ld_490.Add(7, 1.05f);
            Proyecto_.Ld_490.Add(8, 1.2f);
            Proyecto_.Ld_490.Add(9, 1.35f);
            Proyecto_.Ld_490.Add(10, 1.5f);
            Proyecto_.Ld_490.Add(11, 1.7f);
            Proyecto_.Ld_490.Add(14, 2f);

            Proyecto_.Ld_560 = new Dictionary<int, float>();
            Proyecto_.Ld_560.Add(2, 0.4f);
            Proyecto_.Ld_560.Add(3, 0.4f);
            Proyecto_.Ld_560.Add(4, 0.45f);
            Proyecto_.Ld_560.Add(5, 0.6f);
            Proyecto_.Ld_560.Add(6, 0.7f);
            Proyecto_.Ld_560.Add(7, 1.0f);
            Proyecto_.Ld_560.Add(8, 1.1f);
            Proyecto_.Ld_560.Add(9, 1.25f);
            Proyecto_.Ld_560.Add(10, 1.4f);
            Proyecto_.Ld_560.Add(11, 1.6f);
            Proyecto_.Ld_560.Add(14, 1.9f);

            #region Diccionario diametro barras

            //Diametros en centimetros
            Proyecto_.Diametro_ref = new Dictionary<int, float>();
            Proyecto_.Diametro_ref.Add(3, 0.95f);
            Proyecto_.Diametro_ref.Add(4, 1.27f);
            Proyecto_.Diametro_ref.Add(5, 1.59f);
            Proyecto_.Diametro_ref.Add(6, 1.91f);
            Proyecto_.Diametro_ref.Add(7, 2.22f);
            Proyecto_.Diametro_ref.Add(8, 2.54f);
            Proyecto_.Diametro_ref.Add(9, 2.87f);
            Proyecto_.Diametro_ref.Add(10, 3.23f);
            Proyecto_.Diametro_ref.Add(11, 3.58f);
            Proyecto_.Diametro_ref.Add(14, 4.30f);

            #endregion Diccionario diametro barras
        }

        private string AbrirE2K2009yCSV2009()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.DefaultExt = "e2K";
            openFileDialog1.Filter = "Archivo de ETABS 9.5 |*.$ET;*.e2k";
            openFileDialog1.Title = "Abrir Archivo de ETABS 9.5";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                Proyecto_ = new Proyecto();
                ArchivoE2K2009ETABS = FunctionsProject.AbrirArchivoE2K2009(openFileDialog1.FileName);
                CrearObjetosNecesarios();
            }
            return openFileDialog1.FileName;
        }

        private string AsingarFuerzasColumnas()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Fuerzas en Columnas";
            openFileDialog.Filter = "Fuerzas en Columnas |*.csv";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                ArchivoFuerzasColumnas2009 = FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item1;

                if (FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item2 == "OK")
                {
                    CargarFuerzasColumnas();
                }
                else
                {
                    MessageBox.Show(FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item2, Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    openFileDialog.FileName = "";
                }
            }

            return openFileDialog.FileName;
        }

        private string AsingarResultadosaColumna()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Resultados a Flexo Compresión";
            openFileDialog.Filter = "Resultados |*.csv";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                ArchivoResultados2009 = FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item1;

                if (FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item2 == "OK")
                {
                    AsignarResultados();
                }
                else
                {
                    MessageBox.Show(FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item2, Proyecto_.Empresa, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    openFileDialog.FileName = "";
                }
            }

            return openFileDialog.FileName;
        }

        private void CargarFuerzasColumnas()
        {
            List<List<string>> Resultados2 = new List<List<string>>();

            for (int i = 0; i < ArchivoFuerzasColumnas2009.Count; i++)
            {
                if (ArchivoFuerzasColumnas2009[i] != null)
                {
                    Resultados2.Add(ArchivoFuerzasColumnas2009[i].Split(Convert.ToChar(";")).ToList());
                }
            }

            foreach (Columna columna in Proyecto_.Lista_Columnas)
            {
                for (int i = 1; i < Resultados2.Count; i++)
                {
                    for (int j = 0; j < columna.Seccions.Count; j++)
                    {
                        if (columna.Name == Resultados2[i][1] && columna.Seccions[j].Item2 == Resultados2[i][0])
                        {
                            columna.resultadosETABs[j].Story_Result.Add(Resultados2[i][0]);
                            columna.resultadosETABs[j].Load.Add(Resultados2[i][2]);
                            columna.resultadosETABs[j].Loc.Add(Convert.ToSingle(Resultados2[i][3]));
                            columna.resultadosETABs[j].P.Add(Convert.ToSingle(Resultados2[i][4]));
                            columna.resultadosETABs[j].V2.Add(Convert.ToSingle(Resultados2[i][5]));
                            columna.resultadosETABs[j].V3.Add(Convert.ToSingle(Resultados2[i][6]));
                            columna.resultadosETABs[j].T.Add(Convert.ToSingle(Resultados2[i][7]));
                            columna.resultadosETABs[j].M2.Add(Convert.ToSingle(Resultados2[i][8]));
                            columna.resultadosETABs[j].M3.Add(Convert.ToSingle(Resultados2[i][9]));
                        }
                    }
                }
            }
        }

        private void AsignarResultados()
        {
            List<List<string>> Resultados2 = new List<List<string>>();

            for (int i = 0; i < ArchivoResultados2009.Count; i++)
            {
                if (ArchivoResultados2009[i] != null)
                {
                    Resultados2.Add(ArchivoResultados2009[i].Split(Convert.ToChar(";")).ToList());
                }
            }

            foreach (Columna columna in Proyecto_.Lista_Columnas)
            {
                columna.resultadosETABs = new List<ResultadosETABS>();

                for (int j = 0; j < columna.Seccions.Count; j++)
                {
                    ResultadosETABS resultados = new ResultadosETABS();
                    for (int i = 1; i < Resultados2.Count; i++)
                    {
                        if (columna.Name == Resultados2[i][1] && columna.Seccions[j].Item2 == Resultados2[i][0])
                        {
                            resultados.Estacion.Add(Convert.ToSingle(Resultados2[i][3]));
                            resultados.Asmin.Add(Convert.ToSingle(Resultados2[i][7]));
                            resultados.As.Add(Convert.ToSingle(Resultados2[i][8]));
                        }
                    }
                    columna.resultadosETABs.Add(resultados);
                    columna.AsignarAsTopMediumButton_();
                }
            }
        }

        private void CrearObjetosNecesarios()
        {
            //STORIES
            CreateDidctonaries();

            List<List<string>> Stories = new List<List<string>>();

            Proyecto_.Stories = new List<Tuple<string, float>>();

            int Inicio = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ STORIES - IN SEQUENCE FROM TOP")) + 1;
            int Fin = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ DIAPHRAGM NAMES")) - 2;

            for (int i = Inicio; i < Fin; i++)
            {
                Stories.Add(ArchivoE2K2009ETABS[i].Split((char)34).ToList());
            }
            for (int i = 0; i < Stories.Count; i++)
            {
                string Name_Story = Stories[i][1].Replace("\"", "");
                float Altura_Story = (float)Convert.ToDouble(Stories[i][2].Replace("  HEIGHT ", "").Split().ToList()[0]);

                Tuple<string, float> tuple_Aux = new Tuple<string, float>(Name_Story, Altura_Story);
                Proyecto_.Stories.Add(tuple_Aux);
            }

            //Puntos
            int Inicio_PointCoordinates = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ POINT COORDINATES")) + 1;

            int Final_PointCoordinates = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ LINE CONNECTIVITIES")) - 1;
            List<List<string>> PointsConnectivities = new List<List<string>>();

            for (int i = Inicio_PointCoordinates; i < Final_PointCoordinates; i++)
            {
                PointsConnectivities.Add(ArchivoE2K2009ETABS[i].Split().ToList());
            }

            List<Tuple<string, double[]>> Points = new List<Tuple<string, double[]>>();

            for (int i = 0; i < PointsConnectivities.Count; i++)
            {
                string PointNumber = PointsConnectivities[i][3].Replace("\"", "");
                double[] XY = new double[] { Convert.ToDouble(PointsConnectivities[i][5]), Convert.ToDouble(PointsConnectivities[i][6]) };

                Tuple<string, double[]> Point = new Tuple<string, double[]>(PointNumber, XY);
                Points.Add(Point);
            }

            // Definir Materiales

            int Inicio_MaterialProperties = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ MATERIAL PROPERTIES")) + 1;
            int Final_MaterialProperties = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ FRAME SECTIONS")) - 1;

            List<List<string>> Lista_Materiales_Aux2 = new List<List<string>>();
            List<string> Lista_Materiales_Aux = new List<string>();

            for (int i = Inicio_MaterialProperties; i < Final_MaterialProperties; i++)
            {
                Lista_Materiales_Aux.Add(ArchivoE2K2009ETABS[i]);
            }

            //Solo Encontrara Material Tipo Concreto

            Proyecto_.Lista_Materiales = new List<MAT_CONCRETE>();

            for (int i = 0; i < Lista_Materiales_Aux.Count; i++)
            {
                if (Lista_Materiales_Aux[i].Contains("FC"))
                {
                    Lista_Materiales_Aux2.Add(Lista_Materiales_Aux[i].Split().ToList());
                }
            }

            for (int i = 0; i < Lista_Materiales_Aux2.Count; i++)
            {
                MAT_CONCRETE material = new MAT_CONCRETE();
                material.Name = Lista_Materiales_Aux2[i][4].Replace("\"", "");
                material.FC = (float)Convert.ToDouble(Lista_Materiales_Aux2[i][13].Replace("\"", "")) / 10;
                Proyecto_.Lista_Materiales.Add(material);
            }

            //Secciones

            Proyecto_.Lista_Secciones = new List<ISeccion>();

            int Inicio_FrameSecctions = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ FRAME SECTIONS")) + 1;
            int Final_FrameSecctions = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ REBAR DEFINITIONS")) - 1;

            List<List<string>> Lista_Secciones_Aux = new List<List<string>>();

            for (int i = Inicio_FrameSecctions; i < Final_FrameSecctions; i++)
            {
                if (ArchivoE2K2009ETABS[i].Contains("Rectangular") | ArchivoE2K2009ETABS[i].Contains("Circle") | ArchivoE2K2009ETABS[i].Contains("Te") | ArchivoE2K2009ETABS[i].Contains("Angle") | ArchivoE2K2009ETABS[i].Contains("SD Section"))
                {
                    if (ArchivoE2K2009ETABS[i].Contains("SD Section"))
                    {
                    }
                    Lista_Secciones_Aux.Add(ArchivoE2K2009ETABS[i].Split().ToList());
                }
            }

            for (int i = 0; i < Lista_Secciones_Aux.Count; i++)
            {
                string Nombre = Lista_Secciones_Aux[i][4].Replace("\"", "");
                string Material_Aux = Lista_Secciones_Aux[i][7].Replace("\"", "");
                string SHAPE = Lista_Secciones_Aux[i][10].Replace("\"", "");
                List<float[]> Coord = null;
                TipodeSeccion tipodeSeccion;
                float TF, TW, B, H;

                if (SHAPE == "SD")
                {
                    //Section Designer

                    int Inicio_DesignerSections = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ SECTION DESIGNER SECTIONS")) + 1;
                    int Final_DesignerSection = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ WALL/SLAB/DECK PROPERTIES")) - 1;

                    List<List<string>> SectionDesginer = new List<List<string>>();

                    int NoPuntos = 0;
                    for (int j = Inicio_DesignerSections; j < Final_DesignerSection; j++)
                    {
                        SectionDesginer.Add(ArchivoE2K2009ETABS[j].Split((char)34).ToList());
                    }

                    for (int k = 0; k < SectionDesginer.Count; k++)
                    {
                        if (SectionDesginer[k].Count == 7)
                        {
                            if (SectionDesginer[k][5] == "POLYGON")
                            {
                                NoPuntos = Convert.ToInt32(SectionDesginer[k][6].Replace("  NUMCORNERPTS ", ""));
                                Coord = new List<float[]>();

                                for (int s = k + 1; s < k + NoPuntos + 1; s++)
                                {
                                    string[] SDASD = SectionDesginer[s][2].Split();
                                    float X = Convert.ToSingle(SectionDesginer[s][2].Split()[9]);
                                    float Y = Convert.ToSingle(SectionDesginer[s][2].Split()[12]);

                                    float[] XY = { X, Y };
                                    Coord.Add(XY);
                                }
                            }
                        }
                    }

                    tipodeSeccion = TipodeSeccion.None;

                    if (NoPuntos == 8)
                    {
                        tipodeSeccion = TipodeSeccion.Tee;
                    }
                    else if (NoPuntos == 6)
                    {
                        tipodeSeccion = TipodeSeccion.L;
                    }

                    H = 0;
                    B = 0;
                    TW = 0;
                    TF = 0;
                }
                else
                {
                    if (SHAPE == "Rectangular")
                    {
                        tipodeSeccion = TipodeSeccion.Rectangular;
                    }
                    else if (SHAPE == "Circle")
                    {
                        tipodeSeccion = TipodeSeccion.Circle;
                    }
                    else if (SHAPE == "Tee")
                    {
                        tipodeSeccion = TipodeSeccion.Tee;
                    }
                    else if (SHAPE == "Angle")
                    {
                        tipodeSeccion = TipodeSeccion.L;
                    }
                    else { tipodeSeccion = TipodeSeccion.None; }

                    H = (float)Convert.ToDouble(Lista_Secciones_Aux[i][13]);

                    try
                    {
                        B = (float)Convert.ToDouble(Lista_Secciones_Aux[i][16]);
                    }
                    catch
                    { B = H; H = 0; }

                    try
                    {
                        TF = (float)Convert.ToDouble(Lista_Secciones_Aux[i][19]);
                    }
                    catch { TF = 0; }
                    try
                    {
                        TW = (float)Convert.ToDouble(Lista_Secciones_Aux[i][22]);
                    }
                    catch { TW = 0; }
                }

                foreach (MAT_CONCRETE mAT_ in Proyecto_.Lista_Materiales)
                {
                    if (mAT_.Name == Material_Aux)
                    {
                        ISeccion seccion = new CRectangulo(Nombre, B, H, mAT_, tipodeSeccion, Coord);

                        if (tipodeSeccion == TipodeSeccion.Rectangular)
                        {
                            seccion = new CRectangulo(Nombre, B, H, mAT_, tipodeSeccion, Coord);
                        }

                        if (tipodeSeccion == TipodeSeccion.Circle)
                        {
                            seccion = new CCirculo(Nombre, B / 2, pCentro: new double[] { 0, 0 }, Material_: mAT_, Shape_: tipodeSeccion, pCoord: Coord);
                        }

                        if (tipodeSeccion == TipodeSeccion.Tee | tipodeSeccion == TipodeSeccion.L)
                        {
                            float pTw, pTf;
                            float pH, pB;

                            var Xunicos = Coord.Select(x => x[0]).Distinct().ToList();
                            var Yunicos = Coord.Select(x => x[1]).Distinct().ToList();

                            pTw = FunctionsProject.Dimension(Xunicos, false);
                            pTf = FunctionsProject.Dimension(Yunicos, false);
                            pB = FunctionsProject.Dimension(Xunicos, true);
                            pH = FunctionsProject.Dimension(Yunicos, true);

                            seccion = new CSD(Nombre, pB, pH, pTw, pTf, mAT_, tipodeSeccion, Coord);
                        }

                        Proyecto_.Lista_Secciones.Add(seccion);
                    }
                }
            }

            //TUPLAS DE COLUMNAS Y VIGAS

            List<List<string>> LineConectives = new List<List<string>>();

            int Inicio_LineConnectives = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ LINE CONNECTIVITIES")) + 1;

            int Final_LineConnectives = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ AREA CONNECTIVITIES")) - 1;

            for (int i = Inicio_LineConnectives; i < Final_LineConnectives; i++)
            {
                LineConectives.Add(ArchivoE2K2009ETABS[i].Split().ToList());
            }

            List<Tuple<string, string, double[]>> AuxColums = new List<Tuple<string, string, double[]>>();

            List<Tuple<string, string[], double[]>> AuxBeams = new List<Tuple<string, string[], double[]>>();

            for (int i = 0; i < LineConectives.Count; i++)
            {
                if (LineConectives[i][6] == "COLUMN")
                {
                    string Name = LineConectives[i][4].Replace("\"", "");
                    string PointNumber = LineConectives[i][8].Replace("\"", "");
                    Tuple<string, string, double[]> Colum_Tu = new Tuple<string, string, double[]>(Name, PointNumber, new double[2]);
                    AuxColums.Add(Colum_Tu);
                }

                if (LineConectives[i][6] == "BEAM")
                {
                    string Name = LineConectives[i][4].Replace("\"", "");
                    string[] PointNumber = new string[] { LineConectives[i][8].Replace("\"", ""), LineConectives[i][10].Replace("\"", "") };
                    Tuple<string, string[], double[]> Beam_Tu = new Tuple<string, string[], double[]>(Name, PointNumber, new double[2]);
                    AuxBeams.Add(Beam_Tu);
                }
            }

            // LINE ASSIGNS

            List<List<string>> LineAssingns = new List<List<string>>();

            Inicio = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ LINE ASSIGNS")) + 1;

            Fin = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ AREA ASSIGNS")) - 1;

            for (int i = Inicio; i < Fin; i++)
            {
                LineAssingns.Add(ArchivoE2K2009ETABS[i].Split((char)34).ToList());
            }

            // OBJETOS VIGAS

            Proyecto_.Lista_Vigas = new List<Viga>();

            for (int i = 0; i < AuxBeams.Count; i++)
            {
                Viga viga = new Viga(AuxBeams[i].Item1);
                viga.Points = AuxBeams[i].Item2;
                Proyecto_.Lista_Vigas.Add(viga);
            }

            //Asignar Secciones a Vigas por Piso

            for (int i = 0; i < Proyecto_.Stories.Count; i++)
            {
                for (int j = 0; j < LineAssingns.Count; j++)
                {
                    string Story = LineAssingns[j][3].Replace("\"", "");
                    string NameBeam = LineAssingns[j][1].Replace("\"", "");
                    string NameSeccion = LineAssingns[j][5].Replace("\"", "");
                    foreach (Viga viga in Proyecto_.Lista_Vigas)
                    {
                        if (viga.Name == NameBeam && Story == Proyecto_.Stories[i].Item1)
                        {
                            ISeccion seccion = Proyecto_.Lista_Secciones.Find(x => x.Name == NameSeccion);
                            Tuple<CRectangulo, string> tuple_aux = new Tuple<CRectangulo, string>((CRectangulo)seccion, Story);
                            viga.Seccions.Add(tuple_aux);
                        }
                    }
                }
            }

            //OBJETOS COLUMNA

            Proyecto_.Lista_Columnas = new List<Columna>();

            for (int i = 0; i < AuxColums.Count; i++)
            {
                Columna columna = new Columna(AuxColums[i].Item1);

                for (int j = 0; j < Points.Count; j++)
                {
                    if (AuxColums[i].Item2 == Points[j].Item1)
                    {
                        columna.CoordXY = Points[j].Item2;
                        columna.Point = AuxColums[i].Item2;
                    }
                }
                Proyecto_.Lista_Columnas.Add(columna);
            }

            //Asignar Secciones a Columnas por Piso

            for (int i = 0; i < Proyecto_.Stories.Count; i++)
            {
                for (int j = 0; j < LineAssingns.Count; j++)
                {
                    string Story = LineAssingns[j][3].Replace("\"", "");
                    string NameColum = LineAssingns[j][1].Replace("\"", "");
                    string NameSeccion = LineAssingns[j][5].Replace("\"", "");
                    foreach (Columna colum in Proyecto_.Lista_Columnas)
                    {
                        if (colum.Name == NameColum && Story == Proyecto_.Stories[i].Item1)
                        {
                            ISeccion temp = Proyecto_.Lista_Secciones.Find(x => x.Name == NameSeccion);
                            ISeccion seccion = null;

                            if (secciones_predef.Secciones.Exists(x => x == temp) == true)
                            {
                                seccion = secciones_predef.Secciones.Find(x => x == temp);
                                seccion.B = temp.B;
                                seccion.H = temp.H;
                            }
                            else
                            {
                                seccion = temp;
                            }

                            if (seccion.Shape == TipodeSeccion.None)
                            {
                                seccion = null;
                            }
                            else
                            {
                                Tuple<ISeccion, string> tuple_aux = new Tuple<ISeccion, string>(seccion, Story);
                                colum.Seccions.Add(tuple_aux);
                            }
                        }
                    }
                }
            }

            //Determinar Viga por Piso

            foreach (Columna column in Proyecto_.Lista_Columnas)
            {
                List<Viga> vigasPosibles = new List<Viga>();

                foreach (Viga viga in Proyecto_.Lista_Vigas)
                {
                    if (viga.Points[0] == column.Point | viga.Points[1] == column.Point)
                    {
                        vigasPosibles.Add(viga);
                    }
                }

                Viga VigaMayor = new Viga("Viga con Mayor H Por Piso");
                ISeccion seccionMayor = new CRectangulo("Inicial", 0, -99999, new MAT_CONCRETE(), TipodeSeccion.None);
                Tuple<CRectangulo, string> tuple_Seccion_Mayor = null;

                for (int i = 0; i < column.Seccions.Count; i++)
                {
                    tuple_Seccion_Mayor = new Tuple<CRectangulo, string>((CRectangulo)seccionMayor, column.Seccions[i].Item2);
                    VigaMayor.Seccions.Add(tuple_Seccion_Mayor);
                }

                foreach (Viga viga1 in vigasPosibles)
                {
                    for (int i = 0; i < viga1.Seccions.Count; i++)
                    {
                        for (int j = 0; j < VigaMayor.Seccions.Count; j++)
                        {
                            if (viga1.Seccions[i].Item2 == VigaMayor.Seccions[j].Item2)
                            {
                                if (viga1.Seccions[i].Item1.H > VigaMayor.Seccions[j].Item1.H)
                                {
                                    VigaMayor.Seccions[j] = viga1.Seccions[i];
                                }
                            }
                        }
                    }
                }

                column.VigaMayor = VigaMayor;
            }
            //Asignar Altura Libre;

            foreach (Columna columna1 in Proyecto_.Lista_Columnas)
            {
                columna1.LuzLibre = new List<float>();
                for (int i = 0; i < columna1.Seccions.Count; i++)
                {
                    columna1.LuzLibre.Add(0);
                }
                for (int i = 0; i < columna1.Seccions.Count; i++)
                {
                    for (int j = 0; j < Proyecto_.Stories.Count; j++)
                    {
                        if (columna1.Seccions[i].Item2 == Proyecto_.Stories[j].Item1)
                        {
                            columna1.LuzLibre[i] = (Proyecto_.Stories[j].Item2 - columna1.VigaMayor.Seccions[i].Item1.H);
                        }
                    }
                }
            }

            //Crear Lista de Estribos;

            foreach (Columna columna2 in Proyecto_.Lista_Columnas)
            {
                for (int i = 0; i < columna2.Seccions.Count; i++)
                {
                    Estribo estribo = null;
                    if (columna2.Seccions[i].Item1 != null)
                    {
                        estribo = new Estribo(3);
                    }
                    columna2.estribos.Add(estribo);
                }
            }
        }

        private void PlantaDeColumnasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_PlantaColumnas.Created == false)
            {
                m_PlantaColumnas = new PlantaColumnas();
            }

            m_PlantaColumnas.Show(PanelContenedor);
        }

        private void InfromaciónDeColumnasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_Informacion.Created == false)
            {
                m_Informacion = new Informacion();
            }

            m_Informacion.Show(PanelContenedor);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Proyecto_ != null)
            {
                if (Proyecto_.Ruta == "" | Proyecto_.Ruta != "")
                {
                    DialogResult box;
                    box = MessageBox.Show("¿Desea guardar cambios en el Proyecto: " + Proyecto_.Name + "?", Proyecto_.Empresa, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (box == DialogResult.Yes)
                    {
                        Save();
                    }
                    else if (box == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    else if (box == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void VariablesDeEntradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (variablesdeEntrada != null)
            {
                if (Proyecto_ != null)
                {
                    if (Proyecto_.DMO_DES == GDE.DMO)
                    {
                        variablesdeEntrada.Radio_Dmo.Checked = true;
                    }
                    else if (Form1.Proyecto_.DMO_DES == GDE.DES)
                    {
                        variablesdeEntrada.Radio_Des.Checked = true;
                    }

                    variablesdeEntrada.T_Vf.Text = Proyecto_.e_Fundacion.ToString();
                    variablesdeEntrada.T_arranque.Text = Proyecto_.Nivel_Fundacion.ToString();
                    variablesdeEntrada.Fy_Box.Text = Proyecto_.FY.ToString();
                }
                variablesdeEntrada.PictureBox1.Visible = true;
                variablesdeEntrada.ShowDialog();
            }
            else
            {
                variablesdeEntrada = new VariablesdeEntrada(true);
                variablesdeEntrada.ShowDialog();
            }
        }

        private void LColumna_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LColumna.Text != "")
            {
                TColumna = LColumna.Text;
                Proyecto_.ColumnaSelect = Proyecto_.Lista_Columnas.Find(x => x.Name == LColumna.Text);
                m_Informacion.Invalidate();
                m_PlantaColumnas.Invalidate();
                m_Despiece.Invalidate();
                mCuantiaVolumetrica.Invalidate();
                mAgregarAlzado.Invalidate();

                if (mIntefazSeccion != null)
                {
                    mIntefazSeccion.edicion = Tipo_Edicion.Secciones_modelo;
                    mIntefazSeccion.Get_Columna();
                    mIntefazSeccion.Load_Pisos();
                    mIntefazSeccion.Get_section();
                    mIntefazSeccion.Invalidate();
                    mFuerzasEnElmentos.Invalidate();
                }
            }
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void dibujoSecciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mIntefazSeccion != null)
            {
                if (mIntefazSeccion.Created == false && Proyecto_.ColumnaSelect != null)
                {
                    mIntefazSeccion = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
                }
            }
            else
            {
                mIntefazSeccion = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
            }
            mIntefazSeccion.Show(PanelContenedor);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Cb_cuantiavol_Click(object sender, EventArgs e)
        {
            if (Proyecto_.ColumnaSelect != null)
            {
                float FD1, FD2;

                if (Proyecto_.DMO_DES == GDE.DMO)
                {
                    FD1 = 0.20f;
                    FD2 = 0.06f;
                }
                else
                {
                    FD1 = 0.30f;
                    FD2 = 0.09f;
                }

                for (int i = 0; i < Proyecto_.ColumnaSelect.Seccions.Count; i++)
                {
                    Proyecto_.ColumnaSelect.Seccions[i].Item1.Cuanti_Vol(FD1, FD2, Proyecto_.R / 100, Proyecto_.FY);
                }

                mCuantiaVolumetrica.Invalidate();
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
        }

        private void PanelContenedor_ActivePaneChanged(object sender, EventArgs e)
        {
            if (mCuantiaVolumetrica != null)
            {
                bool ExistFlotante = false;
                foreach (FloatWindow floatWindow in PanelContenedor.FloatWindows)
                {
                    if (floatWindow.Text == mCuantiaVolumetrica.Text)
                    {
                        ExistFlotante = true;
                    }
                }

                if (PanelContenedor.ActiveDocument == mCuantiaVolumetrica | ExistFlotante)
                {
                    Cuantia_Vol_Button.Enabled = true;
                }
                else
                {
                    Cuantia_Vol_Button.Enabled = false;
                }
            }

            if (mAgregarAlzado != null)
            {
                bool ExistFlotante = false;
                foreach (FloatWindow floatWindow in PanelContenedor.FloatWindows)
                {
                    if (floatWindow.Text == mAgregarAlzado.Text)
                    {
                        ExistFlotante = true;
                    }
                }

                if (PanelContenedor.ActiveDocument == mAgregarAlzado | ExistFlotante)
                {
                    Button_Agregar.Enabled = true;
                }
                else
                {
                    Button_Agregar.Enabled = false;
                }
            }
            if (Proyecto_ != null)
            {
                Disenar.Enabled = true;
                infromaciónDeColumnasToolStripMenuItem.Enabled = true;
                plantaDeColumnasToolStripMenuItem.Enabled = true;
                agregarAlzadoToolStripMenuItem.Enabled = true;
                dibujoDeSecciónToolStripMenuItem.Enabled = true;
                cuantíaVolumétricaToolStripMenuItem.Enabled = true;
                variablesDeEntradaToolStripMenuItem.Enabled = true;
                columnasIgualesToolStripMenuItem.Enabled = true;
                fuerzasToolStripMenuItem.Enabled = true;
            }
            if (WindowState == FormWindowState.Normal)
            {
                Button_MaxRest.Image = Properties.Resources.Maximizar14X11;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                Button_MaxRest.Image = Properties.Resources.Restaurar14x11;
            }
        }

        private void AgregarAlzadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mAgregarAlzado.Created == false)
            {
                mAgregarAlzado = new AgregarAlzado();
            }

            mAgregarAlzado.Show(PanelContenedor);
        }

        private void Button_Agregar_Click(object sender, EventArgs e)
        {
            if (Proyecto_.ColumnaSelect != null)
            {
                int CantidadPisos = 0;
                for (int i = 0; i < Proyecto_.ColumnaSelect.Seccions.Count; i++)
                {
                    if (Proyecto_.ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        CantidadPisos += 1;
                    }
                }

                int MaximoID = -99999;
                for (int i = 0; i < Proyecto_.ColumnaSelect.Alzados.Count; i++)
                {
                    if (Proyecto_.ColumnaSelect.Alzados[i].ID > MaximoID)
                    {
                        MaximoID = Proyecto_.ColumnaSelect.Alzados[i].ID;
                    }
                }
                if (MaximoID == -99999) { MaximoID = 1; } else { MaximoID += 1; }

                Alzado alzadoN = new Alzado(MaximoID, CantidadPisos);

                Proyecto_.ColumnaSelect.Alzados.Add(alzadoN);
                mAgregarAlzado.CrearDataGrid(true);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Proyecto_ = new Proyecto();
            CreateDidctonaries();
            mIntefazSeccion = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
            Main_Secciones.Crear_archivo();
        }

        private void ColumnasIgualesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColSimilares colSimilares = new ColSimilares();
            colSimilares.ShowDialog();
        }

        private void Diseñar(ref List<Columna> Lista_ColumnasDiseñar)
        {
            //Limpiar Todos Los Alzados En Cada Columna
            foreach (Columna Col in Lista_ColumnasDiseñar)
            {
                Col.Alzados.Clear();
            }

            //Crear Alzado Base

            foreach (Columna Col in Lista_ColumnasDiseñar)
            {
                Col.AlzadoBaseSugerido = new List<string[]>();
            }

            foreach (Columna Col in Lista_ColumnasDiseñar)
            {
                for (int i = 0; i < Col.LuzLibre.Count; i++)
                {
                    Col.AlzadoBaseSugerido.Add(new string[] { });
                }
            }

            //Determinar Cantidad de Barras Por Sección Predefinidas

            foreach (CRectangulo seccion in secciones_predef.Secciones)
            {
                seccion.CalcNoDBarras();
            }

            foreach (Columna Col in Lista_ColumnasDiseñar)
            {
                for (int i = Col.Seccions.Count - 1; i >= 0; i--)
                {
                    string[] Base = new string[0];

                    foreach (CRectangulo seccionP in secciones_predef.Secciones)
                    {
                        if (Col.Seccions[i].Item1.B == seccionP.B && Col.Seccions[i].Item1.H == seccionP.H || Col.Seccions[i].Item1.H == seccionP.B && Col.Seccions[i].Item1.B == seccionP.H)
                        {
                            if (seccionP.No_D_Barra.Count == 2)
                            {
                                Base = new string[4];
                                Base[0] = $"{ Convert.ToString(seccionP.No_D_Barra[0].Item1 / 2)}#{Convert.ToString(seccionP.No_D_Barra[0].Item2)}";
                                Base[1] = $"{ Convert.ToString(seccionP.No_D_Barra[0].Item1 / 2)}#{Convert.ToString(seccionP.No_D_Barra[0].Item2)}";
                                Base[2] = $"{ Convert.ToString(seccionP.No_D_Barra[1].Item1 / 2)}#{Convert.ToString(seccionP.No_D_Barra[1].Item2)}";
                                Base[3] = $"{ Convert.ToString(seccionP.No_D_Barra[1].Item1 / 2)}#{Convert.ToString(seccionP.No_D_Barra[1].Item2)}";
                            }

                            if (seccionP.No_D_Barra.Count == 1)
                            {
                                Base = new string[2];
                                Base[0] = $"{ Convert.ToString(seccionP.No_D_Barra[0].Item1 / 2)}#{Convert.ToString(seccionP.No_D_Barra[0].Item2)}";
                                Base[1] = $"{ Convert.ToString(seccionP.No_D_Barra[0].Item1 / 2)}#{Convert.ToString(seccionP.No_D_Barra[0].Item2)}";
                            }

                            break;
                        }
                    }

                    Col.AlzadoBaseSugerido[i] = Base;
                }
            }

            CuadroDialogoDiseño Cuadro_diseño = new CuadroDialogoDiseño();
            Cuadro_diseño.WindowState = FormWindowState.Maximized;
            Cuadro_diseño.Show();

            Cuadro_diseño.BarraPersonalizada2.Width = 0;
            Cuadro_diseño.BarraPersonalizada.Visible = true;
            Cuadro_diseño.BarraPersonalizada2.Visible = true;
            double Delta;
            if (Lista_ColumnasDiseñar.Count > 4)
            {
                Delta = (Cuadro_diseño.BarraPersonalizada.Width) / (Lista_ColumnasDiseñar.Count - 4);
            }
            else
            {
                Delta = (Cuadro_diseño.BarraPersonalizada.Width) / (Lista_ColumnasDiseñar.Count);
            }

            if (mAgregarAlzado != null)
            {
                mAgregarAlzado.CrearDataGrid(true);
                m_Informacion.CrearDataGriedView(true);
            }
            if (m_Despiece != null)
            {
                m_Despiece.Invalidate();
            }
            int CantCol = 1;
            double D_Pro = Math.Ceiling(Delta);
            bool HabilitarReporte = false;

            foreach (Columna col in Lista_ColumnasDiseñar)
            {
                col.AgregarAlzadoSugerido();
                if (col.Alzados.Count != 0)
                {
                    Cuadro_diseño.Label_Progress.Text = "✓ Columna: " + col.Name;
                }
                else
                {
                    Cuadro_diseño.Label_Progress.Text = "✘ Columna: " + col.Name;
                    HabilitarReporte = true;
                }
                Cuadro_diseño.Canti_Colum.Text = $"{ CantCol}/{Lista_ColumnasDiseñar.Count}";
                CantCol += 1;
                Cuadro_diseño.BarraPersonalizada2.Width += (int)D_Pro;
                Cuadro_diseño.Refresh();
            }
            Cuadro_diseño.OK.Enabled = true;
            Cuadro_diseño.Reporte_RichText.Text = "-------------------------------------------------------------------------------------------";
            Cuadro_diseño.Reporte_RichText.Text += "\n" + "                                 Columnas No Diseñadas:";
            Cuadro_diseño.Reporte_RichText.Text += "\n" + "-------------------------------------------------------------------------------------------";

            Cuadro_diseño.Reporte_RichText.Visible = HabilitarReporte;

            foreach (Columna col in Lista_ColumnasDiseñar)
            {
                if (col.Alzados.Count == 0)
                {
                    Cuadro_diseño.Reporte_RichText.Text += "\n" + " - " + col.Name;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ColumnasaDiseñar SelecColumnsDisenar = new ColumnasaDiseñar();
            List<Columna> ColumnasADiseñar = new List<Columna>();

            SelecColumnsDisenar.ShowDialog();

            foreach (Columna col in Proyecto_.Lista_Columnas)
            {
                if (col.Disenar)
                {
                    ColumnasADiseñar.Add(col);
                }
            }

            if (ColumnasADiseñar.Count != 0 & CancelDiseño == false)
            {
                Diseñar(ref ColumnasADiseñar);
            }
        }

        private void EditarSeccionesPredeterminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CUsuario usuario = new CUsuario();
            usuario.Get_user();

            if (usuario.Permiso == true)
            {
                mIntefazSeccion = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_predef);
                mIntefazSeccion.Show(PanelContenedor);
                mIntefazSeccion.Get_Predef_Secction();
                mIntefazSeccion.Invalidate();
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(Informacion).ToString())
                return m_Informacion;
            else if (persistString == typeof(AgregarAlzado).ToString())
                return mAgregarAlzado;
            else if (persistString == typeof(PlantaColumnas).ToString())
                return m_PlantaColumnas;
            else if (persistString == typeof(Despiece).ToString())
                return m_Despiece;
            else if (persistString == typeof(CuantiaVolumetrica).ToString())
                return mCuantiaVolumetrica;
            else
            {
                // DummyDoc overrides GetPersistString to add extra information into persistString.
                // Any DockContent may override this value to add any needed information for deserialization.

                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if (parsedStrings.Length != 3)
                    return null;

                if (parsedStrings[0] != typeof(Informacion).ToString())
                    return null;

                Informacion dummyDoc = new Informacion();
                if (parsedStrings[2] != string.Empty)
                    dummyDoc.Text = parsedStrings[2];

                return dummyDoc;
            }
        }

        private void CuantíaVolumétricaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mCuantiaVolumetrica.Created == false)
            {
                mCuantiaVolumetrica = new CuantiaVolumetrica();
            }
            mCuantiaVolumetrica.Show(PanelContenedor);
        }

        private void DibujoDeSecciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mIntefazSeccion != null)
            {
                if (mIntefazSeccion.Created == false && Proyecto_.ColumnaSelect != null)
                {
                    mIntefazSeccion = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
                }
            }
            else
            {
                mIntefazSeccion = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
            }
            mIntefazSeccion.Show(PanelContenedor);
        }

        private void FuerzasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mFuerzasEnElmentos.Created == false)
            {
                mFuerzasEnElmentos = new FuerzasEnElementos();
            }
            mFuerzasEnElmentos.Show(PanelContenedor);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ColumnasaGraficar columnasaGraficar = new ColumnasaGraficar();
            List<Columna> columnasDrawing = new List<Columna>();

            columnasaGraficar.ShowDialog();

            foreach (Columna col in Proyecto_.Lista_Columnas)
            {
                col.NamesSimilares.Clear();
                if (col.aGraficar)
                {
                    columnasDrawing.Add(col);
                }
            }

            foreach (Columna col in Proyecto_.Lista_Columnas)
            {
                if (col.ColSimilName != null)
                {
                    if (columnasDrawing.Exists(x => x.Name == col.Name))
                    {
                        columnasDrawing.Remove(col);
                    }

                    Proyecto_.Lista_Columnas.Find(x => x.Name == col.ColSimilName).NamesSimilares.Add(col.ColSimilName);
                }
            }

            //double[] XY = new double[] { };
            //FunctionsAutoCAD.FunctionsAutoCAD.OpenAutoCAD();
            //FunctionsAutoCAD.FunctionsAutoCAD.SetScale("1:75");

            //FunctionsAutoCAD.FunctionsAutoCAD.GetPoint(ref XY);
            //foreach (Columna col in Proyecto_.Lista_Columnas)
            //{
            //    if (col.Name == "C1")
            //    {
            //        col.DrawColumAutoCAD(XY[0], XY[1],col.Name,1);
            //    }

            //}
        }

        private void GraficarAlzadoColumnas(ref List<Columna> ColumnsDrawing)
        {
        }
    }
}