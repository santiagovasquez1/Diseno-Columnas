﻿using B_Operaciones_Matricialesl;
using DisenoColumnas.Clases;
using DisenoColumnas.DefinirColumnas;
using DisenoColumnas.Diseño;
using DisenoColumnas.Diseño.Ventanas_Auxiliares__Herramientas_;
using DisenoColumnas.Interfaz_Inicial;
using DisenoColumnas.Interfaz_Seccion;
using DisenoColumnas.InterfazViewInfo;
using DisenoColumnas.Secciones;
using DisenoColumnas.Secciones_Predefinidas;
using DisenoColumnas.Utilidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas
{
    public partial class Form1 : Form
    {
        public static PlantaColumnas m_PlantaColumnas;
        public static Informacion m_Informacion;
        public static Despiece m_Despiece;
        public static VariablesdeEntrada variablesdeEntrada;
        public static ComboBox mLcolumnas;
        public static CuantiaVolumetrica mCuantiaVolumetrica;
        public static FInterfaz_Seccion mIntefazSeccion;
        public static AgregarAlzado mAgregarAlzado;
        public static FuerzasEnElementos mFuerzasEnElmentos;
        public static Tipo_Edicion pEdicion = Tipo_Edicion.Secciones_modelo;
        public bool CancelDiseño = false;
        public bool CancelGarfica = false;
        private string NameProgram = "Diseño de Columnas";

        private DeserializeDockContent m_deserializeDockContent;

        public static Form1 mFormPrincipal;

        private List<string> ArchivoE2KETABS;
        private List<string> ArchivoResultados2009;
        private List<string> ArchivoFuerzasColumnas2009;

        public static Proyecto Proyecto_;
        public static string TColumna;
        public static CLista_Secciones secciones_predef;

        private string RutaConfig = "";

        public Form1 ()
        {
            InitializeComponent();
            mFormPrincipal = this;
            CargarToolTips();

            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        private void Close_Click ( object sender , EventArgs e )
        {
            Application.Exit();
        }

        private void Minimized_Click ( object sender , EventArgs e )
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Panel5_MouseDown ( object sender , MouseEventArgs e )
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle , 0x112 , 0xf012 , 0);
        }

        private void Panel1_MouseDown ( object sender , MouseEventArgs e )
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle , 0x112 , 0xf012 , 0);
        }

        private void PictureBox1_MouseDown ( object sender , MouseEventArgs e )
        {
            MoveWindow.ReleaseCapture();
            MoveWindow.SendMessage(this.Handle , 0x112 , 0xf012 , 0);
        }

        private void Button_MaxRest_Click ( object sender , EventArgs e )
        {
            if ( WindowState == FormWindowState.Normal )
            {
                WindowState = FormWindowState.Maximized;
                Button_MaxRest.Image = Properties.Resources.Restaurar14x11;
            }
            else if ( WindowState == FormWindowState.Maximized )
            {
                WindowState = FormWindowState.Normal;
                Button_MaxRest.Image = Properties.Resources.Maximizar14X11;
            }
        }

        private void SalirToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            Application.Exit();
        }

        private void NuevoToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            NewProject();

            //Falta resultados
        }

        private void CrearCarpetaOCargar ()
        {
            string NameCarpeta = @"C:\Users\" + Environment.UserName + @"\.colum";
            RutaConfig = NameCarpeta;
            if ( Directory.Exists(NameCarpeta) )
            {
                RutaConfig = NameCarpeta;
            }
            else
            {
                Directory.CreateDirectory(NameCarpeta);
            }
        }

        private void AbrirToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            OpenProject();
        }

        private void OpenProject ( bool ArchivoExterno = false , string Ruta = "" )
        {
            if ( ArchivoExterno == false )
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "DC |*.Colum";
                openFileDialog.Title = "Abrir Proyecto";
                openFileDialog.ShowDialog();
                Ruta = openFileDialog.FileName;
            }

            if ( Ruta != "" )
            {
                if ( Proyecto_ != null )
                {
                    DialogResult box;
                    box = MessageBox.Show("¿Desea guardar cambios en el Proyecto: " + Proyecto_.Name + "?" , Proyecto_.Empresa , MessageBoxButtons.YesNoCancel , MessageBoxIcon.Warning);

                    if ( box == DialogResult.Yes )
                    {
                        Save();
                    }
                }
                FunctionsProject.Deserealizar(Ruta , ref Proyecto_);
                CreateDictonaries();
                CloseWindows();
                Proyecto_.Ruta = Ruta;
                m_Informacion = null; m_Despiece = null; mCuantiaVolumetrica = null; mAgregarAlzado = null;
                mFuerzasEnElmentos = null;

                L_NameProject.Visible = true;
                L_NameProject.Text = Proyecto_.Name;
                Text = NameProgram + " - " + Proyecto_.Name;

                CrearCarpetaOCargar();
                string configFile = Path.Combine(RutaConfig , "DockPanel.temp.config");

                try
                {
                    PanelContenedor.LoadFromXml(configFile , m_deserializeDockContent);
                }
                catch { }
                m_Informacion = new Informacion();
                m_Informacion.Show(PanelContenedor);

                mAgregarAlzado = new AgregarAlzado();
                mAgregarAlzado.DockAreas = DockAreas.DockBottom;
                mAgregarAlzado.Show(PanelContenedor);

                mLcolumnas = LColumna;
                mCuantiaVolumetrica = new CuantiaVolumetrica();
                //   mCuantiaVolumetrica.Show(PanelContenedor);

                m_Despiece = new Despiece();
                m_Despiece.Show(PanelContenedor);

                CambiarSkins();

                mFuerzasEnElmentos = new FuerzasEnElementos();

                LColumna.Items.Clear();
                LColumna.Text = "";
                LColumna.Items.AddRange(Proyecto_.Lista_Columnas.Select(x => x.Name).ToArray());

                if ( Proyecto_.ColumnaSelect != null )
                {
                    LColumna.Text = Proyecto_.ColumnaSelect.Name;
                }
                else
                {
                    LColumna.Text = Proyecto_.Lista_Columnas[0].Name;
                }

                variablesdeEntrada = new VariablesdeEntrada(false);
                LColumna.Enabled = true;
                La_Column.Enabled = true;

                WindowState = FormWindowState.Maximized;
            }
        }

        private void CambiarSkins ()
        {
            m_Despiece.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor = SystemColors.Control;
            m_Despiece.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor = SystemColors.Control;
            m_Despiece.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.StartColor = SystemColors.ControlLight;
            m_Despiece.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.EndColor = SystemColors.ControlLight;
            m_Despiece.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.TextColor = SystemColors.ActiveCaptionText;
        }

        private void CargarToolTips ()
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(Save_B , "Guardar (Ctrl + S)");
            tool.SetToolTip(SaveAs_B , "Guardar (Ctrl + Mayús + S)");
            tool.SetToolTip(Cuantia_Vol_Button , "Calcular Cuantía Volumétrica");
            tool.SetToolTip(Button_Agregar , "Agregar Nuevo Alzado (Ctrl + Mayús + N)");
            tool.SetToolTip(Disenar , "Diseñar Columnas (Ctrl + D)");
            tool.SetToolTip(Button_DLNET , "Exportar Cantidades (Archivo DL NET) (Ctrl + E)");
        }

        private void CloseWindows ()
        {
            if ( m_PlantaColumnas != null )
            {
                m_PlantaColumnas.DockHandler.DockPanel = null;
            }
            m_PlantaColumnas = new PlantaColumnas();

            if ( m_Informacion != null )
            {
                m_Informacion.DockHandler.DockPanel = null;
            }

            if ( m_Despiece != null )
            {
                m_Despiece.DockHandler.DockPanel = null;
            }

            if ( mCuantiaVolumetrica != null )
            {
                mCuantiaVolumetrica.DockHandler.DockPanel = null;
            }

            if ( mAgregarAlzado != null )
            {
                mAgregarAlzado.DockHandler.DockPanel = null;
            }
            if ( mFuerzasEnElmentos != null )
            {
                mFuerzasEnElmentos.DockHandler.DockPanel = null;
            }
            if ( mIntefazSeccion != null )
            {
                mIntefazSeccion.DockHandler.DockPanel = null;
            }
            if ( mCuantiaVolumetrica != null )
            {
                mCuantiaVolumetrica.DockHandler.DockPanel = null;
            }
        }

        private void SaveAs ()
        {
            if ( Proyecto_ != null )
            {
                SaveFileDialog SaveFile = new SaveFileDialog();
                SaveFile.Filter = "DC |*.Colum";
                SaveFile.Title = "Guardar Proyecto";
                SaveFile.ShowDialog();

                if ( SaveFile.FileName != "" )
                {
                    Proyecto_.Ruta = SaveFile.FileName;
                    Proyecto_.Name = Path.GetFileName(SaveFile.FileName).Replace(".Colum" , "");
                    L_NameProject.Text = Proyecto_.Name;
                    Text = NameProgram + " - " + Proyecto_.Name;
                    FunctionsProject.Serializar(Proyecto_.Ruta , Proyecto_);
                    CrearCarpetaOCargar();
                    string configFile = Path.Combine(RutaConfig , "DockPanel.temp.config");
                    PanelContenedor.SaveAsXml(configFile);
                }
            }
        }

        private void Save ()
        {
            #region Guardo proyecto

            if ( Proyecto_ != null )
            {
                if ( Proyecto_.Ruta != "" )
                {
                    FunctionsProject.Serializar(Proyecto_.Ruta , Proyecto_);
                    CrearCarpetaOCargar();
                    string configFile = Path.Combine(RutaConfig , "DockPanel.temp.config");
                    PanelContenedor.SaveAsXml(configFile);
                }
                else
                {
                    SaveAs();
                }
            }

            #endregion Guardo proyecto

            #region Guardado secciones predef

            CUsuario usuario = new CUsuario();
            string Ruta_Carpeta = AppDomain.CurrentDomain.BaseDirectory;
            string Ruta_Archivo = "Secciones.sec";
            string Ruta_Completa = Ruta_Carpeta + Ruta_Archivo;

            usuario.Get_user();

            if ( usuario.Permiso == true & pEdicion == Tipo_Edicion.Secciones_predef )
            {
                FunctionsProject.Serializar_Secciones(Ruta_Completa , secciones_predef);
            }

            #endregion Guardado secciones predef
        }

        private void GuardarToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            Save();
        }

        private void GuardarComoToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            SaveAs();
        }

        private void NewProject ()
        {
            string Ruta1;
            string Ruta2 = "";
            string Ruta3 = "";

            if ( Proyecto_ != null )
            {
                if ( Proyecto_.AlturaEdificio != 0 )
                {
                    if ( Proyecto_.Ruta == "" | Proyecto_.Ruta != "" )
                    {
                        DialogResult box;
                        box = MessageBox.Show("¿Desea guardar cambios en el Proyecto: " + Proyecto_.Name + "?" , Proyecto_.Empresa , MessageBoxButtons.YesNoCancel , MessageBoxIcon.Warning);
                        if ( box == DialogResult.Yes )
                        {
                            Save();
                        }
                    }
                }
            }
            Ruta1 = AbrirE2K2009yCSV2009();

            if ( Ruta1 != "" )
            {
                Ruta2 = AsingarResultadosaColumna();
            }
            if ( Ruta2 != "" )
            {
                Ruta3 = AsingarFuerzasColumnas();
            }

            if ( Ruta1 != "" & Ruta2 != "" & Ruta3 != "" )
            {
                L_NameProject.Visible = true;
                L_NameProject.Text = Proyecto_.Name = "Proyecto1";
                CloseWindows();
                WindowState = FormWindowState.Maximized;
                m_Informacion = null; m_Despiece = null; mCuantiaVolumetrica = null; mAgregarAlzado = null;
                mFuerzasEnElmentos = null; mIntefazSeccion = null; m_PlantaColumnas = null;
                Text = NameProgram + " - " + Proyecto_.Name;
                //Chequear Carga P< 0.4*Ag*F'c
                ChequeoDeCargas chequeoDeCargas = new ChequeoDeCargas();
                chequeoDeCargas.ShowDialog();

                variablesdeEntrada = new VariablesdeEntrada(true);
                variablesdeEntrada.ShowDialog();

                //Cambiar Nombre de Columnas
                m_Informacion = new Informacion();
                DialogResult EditarColumnas = MessageBox.Show("¿Desea editar el nombre de las columnas?" , Proyecto_.Empresa , MessageBoxButtons.YesNo , MessageBoxIcon.Information);
                if ( EditarColumnas == DialogResult.Yes )
                {
                    CambiarNombreColumna nombreColumna = new CambiarNombreColumna();
                    nombreColumna.ShowDialog();
                    MessageBox.Show("NOTA: Los nombres editados solo cambiaran cuando grafique el alzado en AutoCAD." , Proyecto_.Empresa , MessageBoxButtons.OK , MessageBoxIcon.Information);
                }

                m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
                CrearCarpetaOCargar();
                string configFile = Path.Combine(RutaConfig , "DockPanel.temp.config");
                try { PanelContenedor.LoadFromXml(configFile , m_deserializeDockContent); } catch { }

                mLcolumnas = LColumna;
                m_PlantaColumnas = new PlantaColumnas();
                //   m_PlantaColumnas.Show(PanelContenedor);

                m_Informacion.Show(PanelContenedor);

                m_Despiece = new Despiece();
                m_Despiece.Show(PanelContenedor);
                CambiarSkins();
                mCuantiaVolumetrica = new CuantiaVolumetrica();
                //  mCuantiaVolumetrica.Show(PanelContenedor);

                mFuerzasEnElmentos = new FuerzasEnElementos();

                mAgregarAlzado = new AgregarAlzado();
                mAgregarAlzado.DockAreas = DockAreas.DockBottom;
                mAgregarAlzado.Show(PanelContenedor);

                LColumna.Enabled = true;
                La_Column.Enabled = true;
                LColumna.Items.Clear();
                LColumna.Text = "";
                LColumna.Items.AddRange(Proyecto_.Lista_Columnas.Select(x => x.Name).ToArray());
                LColumna.Text = Proyecto_.Lista_Columnas[0].Name;
            }
        }

        private void CreateDictonaries ()
        {
            #region Area refuerzo

            //Unidades en m²
            Proyecto_.AceroBarras = new Dictionary<int , double>();
            Proyecto_.AceroBarras.Add(2 , 0.32 / 10000);
            Proyecto_.AceroBarras.Add(3 , 0.71 / 10000);
            Proyecto_.AceroBarras.Add(4 , 1.29 / 10000);
            Proyecto_.AceroBarras.Add(5 , 1.99 / 10000);
            Proyecto_.AceroBarras.Add(6 , 2.84 / 10000);
            Proyecto_.AceroBarras.Add(7 , 3.87 / 10000);
            Proyecto_.AceroBarras.Add(8 , 5.10 / 10000);
            Proyecto_.AceroBarras.Add(10 , 8.09 / 10000);

            #endregion Area refuerzo

            Proyecto_.Ld_210 = new Dictionary<int , float>();
            Proyecto_.Ld_210.Add(2 , 0.55f);
            Proyecto_.Ld_210.Add(3 , 0.55f);
            Proyecto_.Ld_210.Add(4 , 0.75f);
            Proyecto_.Ld_210.Add(5 , 0.9f);
            Proyecto_.Ld_210.Add(6 , 1.1f);
            Proyecto_.Ld_210.Add(7 , 1.6f);
            Proyecto_.Ld_210.Add(8 , 1.8f);
            Proyecto_.Ld_210.Add(9 , 2.05f);
            Proyecto_.Ld_210.Add(10 , 2.3f);
            Proyecto_.Ld_210.Add(11 , 2.55f);
            Proyecto_.Ld_210.Add(14 , 3.1f);

            Proyecto_.Ld_280 = new Dictionary<int , float>();
            Proyecto_.Ld_280.Add(2 , 0.5f);
            Proyecto_.Ld_280.Add(3 , 0.5f);
            Proyecto_.Ld_280.Add(4 , 0.65f);
            Proyecto_.Ld_280.Add(5 , 0.8f);
            Proyecto_.Ld_280.Add(6 , 0.95f);
            Proyecto_.Ld_280.Add(7 , 1.4f);
            Proyecto_.Ld_280.Add(8 , 1.6f);
            Proyecto_.Ld_280.Add(9 , 1.8f);
            Proyecto_.Ld_280.Add(10 , 2.0f);
            Proyecto_.Ld_280.Add(11 , 2.2f);
            Proyecto_.Ld_280.Add(14 , 2.65f);

            Proyecto_.Ld_350 = new Dictionary<int , float>();
            Proyecto_.Ld_350.Add(2 , 0.45f);
            Proyecto_.Ld_350.Add(3 , 0.45f);
            Proyecto_.Ld_350.Add(4 , 0.55f);
            Proyecto_.Ld_350.Add(5 , 0.7f);
            Proyecto_.Ld_350.Add(6 , 0.85f);
            Proyecto_.Ld_350.Add(7 , 1.25f);
            Proyecto_.Ld_350.Add(8 , 1.4f);
            Proyecto_.Ld_350.Add(9 , 1.6f);
            Proyecto_.Ld_350.Add(10 , 1.8f);
            Proyecto_.Ld_350.Add(11 , 2.0f);
            Proyecto_.Ld_350.Add(14 , 2.4f);

            Proyecto_.Ld_420 = new Dictionary<int , float>();
            Proyecto_.Ld_420.Add(2 , 0.4f);
            Proyecto_.Ld_420.Add(3 , 0.4f);
            Proyecto_.Ld_420.Add(4 , 0.55f);
            Proyecto_.Ld_420.Add(5 , 0.65f);
            Proyecto_.Ld_420.Add(6 , 0.8f);
            Proyecto_.Ld_420.Add(7 , 1.15f);
            Proyecto_.Ld_420.Add(8 , 1.3f);
            Proyecto_.Ld_420.Add(9 , 1.45f);
            Proyecto_.Ld_420.Add(10 , 1.65f);
            Proyecto_.Ld_420.Add(11 , 1.8f);
            Proyecto_.Ld_420.Add(14 , 2.2f);

            Proyecto_.Ld_490 = new Dictionary<int , float>();
            Proyecto_.Ld_490.Add(2 , 0.4f);
            Proyecto_.Ld_490.Add(3 , 0.4f);
            Proyecto_.Ld_490.Add(4 , 0.5f);
            Proyecto_.Ld_490.Add(5 , 0.6f);
            Proyecto_.Ld_490.Add(6 , 0.75f);
            Proyecto_.Ld_490.Add(7 , 1.05f);
            Proyecto_.Ld_490.Add(8 , 1.2f);
            Proyecto_.Ld_490.Add(9 , 1.35f);
            Proyecto_.Ld_490.Add(10 , 1.5f);
            Proyecto_.Ld_490.Add(11 , 1.7f);
            Proyecto_.Ld_490.Add(14 , 2f);

            Proyecto_.Ld_560 = new Dictionary<int , float>();
            Proyecto_.Ld_560.Add(2 , 0.4f);
            Proyecto_.Ld_560.Add(3 , 0.4f);
            Proyecto_.Ld_560.Add(4 , 0.45f);
            Proyecto_.Ld_560.Add(5 , 0.6f);
            Proyecto_.Ld_560.Add(6 , 0.7f);
            Proyecto_.Ld_560.Add(7 , 1.0f);
            Proyecto_.Ld_560.Add(8 , 1.1f);
            Proyecto_.Ld_560.Add(9 , 1.25f);
            Proyecto_.Ld_560.Add(10 , 1.4f);
            Proyecto_.Ld_560.Add(11 , 1.6f);
            Proyecto_.Ld_560.Add(14 , 1.9f);

            #region Diccionario Diametro barras

            //Diametros en centimetros
            Proyecto_.Diametro_ref = new Dictionary<int , float>();
            Proyecto_.Diametro_ref.Add(3 , 0.95f);
            Proyecto_.Diametro_ref.Add(4 , 1.27f);
            Proyecto_.Diametro_ref.Add(5 , 1.59f);
            Proyecto_.Diametro_ref.Add(6 , 1.91f);
            Proyecto_.Diametro_ref.Add(7 , 2.22f);
            Proyecto_.Diametro_ref.Add(8 , 2.54f);
            Proyecto_.Diametro_ref.Add(9 , 2.87f);
            Proyecto_.Diametro_ref.Add(10 , 3.23f);
            Proyecto_.Diametro_ref.Add(11 , 3.58f);
            Proyecto_.Diametro_ref.Add(14 , 4.30f);

            #endregion Diccionario Diametro barras

            #region Diccionario -> Masa Nominal Barras

            Proyecto_.MasaNominalBarras = new Dictionary<int , float>();
            Proyecto_.MasaNominalBarras.Add(2 , 0.25f);
            Proyecto_.MasaNominalBarras.Add(3 , 0.560f);
            Proyecto_.MasaNominalBarras.Add(4 , 1.0f);
            Proyecto_.MasaNominalBarras.Add(5 , 1.56f);
            Proyecto_.MasaNominalBarras.Add(6 , 2.25f);
            Proyecto_.MasaNominalBarras.Add(7 , 3.06f);
            Proyecto_.MasaNominalBarras.Add(8 , 4f);
            Proyecto_.MasaNominalBarras.Add(9 , 5.00f);
            Proyecto_.MasaNominalBarras.Add(10 , 6.40f);
            Proyecto_.MasaNominalBarras.Add(11 , 7.907f);
            Proyecto_.MasaNominalBarras.Add(14 , 8.938f);

            #endregion Diccionario -> Masa Nominal Barras

            #region Diccionario ganchos a 180

            Proyecto_.G180 = new Dictionary<int , float>
            {
                { 2, 0.116f },
                { 3, 0.14f },
                { 4, 0.167f },
                { 5, 0.192f },
                { 6, 0.228f },
                { 7, 0.266f },
                { 8, 0.305f },
                { 10, 0.457f }
            };

            #endregion Diccionario ganchos a 180

            #region Diccionario ganchos a 135

            Proyecto_.G135 = new Dictionary<int , float>
            {
                { 2, 0.063f },
                { 3, 0.094f },
                { 4, 0.125f },
                { 5, 0.157f },
                { 6, 0.214f },
                { 7, 0.249f },
                { 8, 0.286f },
                { 10, 0.363f }
            };

            #endregion Diccionario ganchos a 135

            #region Diccionario Ganchos a 90

            Proyecto_.G90 = new Dictionary<int , float>
            {
                { 2, 0.09f },
                { 3, 0.14f },
                { 4, 0.18f },
                { 5, 0.23f },
                { 6, 0.27f },
                { 7, 0.32f },
                { 8, 0.36f },
                { 10, 0.47f }
            };

            #endregion Diccionario Ganchos a 90
        }

        private string AbrirE2K2009yCSV2009 ()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.DefaultExt = "e2K";
            openFileDialog1.Filter = "Archivo de ETABS |*.$ET;*.e2k";
            openFileDialog1.Title = "Abrir Archivo de ETABS 9.5, 16.0, 17.1, 18.0";
            openFileDialog1.ShowDialog();

            if ( openFileDialog1.FileName != "" )
            {
                Proyecto_ = new Proyecto();
                ArchivoE2KETABS = FunctionsProject.AbrirArchivoE2K2009(openFileDialog1.FileName);

                if ( ArchivoE2KETABS[3].Contains("9.5.0") )
                {
                    CrearObjetosNecesarios2009();
                }
                else
                {
                    CrearObjetosNecesarios2016_2017();
                }
            }
            return openFileDialog1.FileName;
        }

        private string AsingarFuerzasColumnas ()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Fuerzas en Columnas";
            openFileDialog.Filter = "Fuerzas en Columnas |*.csv";
            openFileDialog.ShowDialog();

            if ( openFileDialog.FileName != "" )
            {
                ArchivoFuerzasColumnas2009 = FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item1;

                if ( FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item2 == "OK" )
                {
                    CargarFuerzasColumnas();
                }
                else
                {
                    MessageBox.Show(FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item2 , Proyecto_.Empresa , MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                    openFileDialog.FileName = "";
                }
            }

            return openFileDialog.FileName;
        }

        private string AsingarResultadosaColumna ()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Resultados a Flexo Compresión";
            openFileDialog.Filter = "Resultados Flexo Compresión |*.csv";
            openFileDialog.ShowDialog();

            if ( openFileDialog.FileName != "" )
            {
                ArchivoResultados2009 = FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item1;

                if ( FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item2 == "OK" )
                {
                    AsignarResultados();
                }
                else
                {
                    MessageBox.Show(FunctionsProject.AbriArchivoResultados2009(openFileDialog.FileName).Item2 , Proyecto_.Empresa , MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                    openFileDialog.FileName = "";
                }
            }

            return openFileDialog.FileName;
        }

        private void CargarFuerzasColumnas ()
        {
            List<List<string>> Resultados2 = new List<List<string>>();

            for ( int i = 0 ; i < ArchivoFuerzasColumnas2009.Count ; i++ )
            {
                if ( ArchivoFuerzasColumnas2009[i] != null )
                {
                    Resultados2.Add(ArchivoFuerzasColumnas2009[i].Split(Convert.ToChar(";")).ToList());
                }
            }

            foreach ( Columna columna in Proyecto_.Lista_Columnas )
            {
                for ( int i = 1 ; i < Resultados2.Count ; i++ )
                {
                    for ( int j = 0 ; j < columna.Seccions.Count ; j++ )
                    {
                        if ( columna.Name == Resultados2[i][1] && columna.Seccions[j].Item2 == Resultados2[i][0] )
                        {
                            columna.resultadosETABs[j].Story_Result.Add(Resultados2[i][0]);
                            columna.resultadosETABs[j].Load.Add(Resultados2[i][2]);
                            columna.resultadosETABs[j].Loc.Add(Convert.ToSingle(Resultados2[i][3]));
                            columna.resultadosETABs[j].P.Add(-Convert.ToSingle(Resultados2[i][4]));
                            columna.resultadosETABs[j].V2.Add(Convert.ToSingle(Resultados2[i][5]));
                            columna.resultadosETABs[j].V3.Add(Convert.ToSingle(Resultados2[i][6]));
                            columna.resultadosETABs[j].T.Add(Convert.ToSingle(Resultados2[i][7]));
                            columna.resultadosETABs[j].M2.Add(Convert.ToSingle(Resultados2[i][8]));
                            columna.resultadosETABs[j].M3.Add(Convert.ToSingle(Resultados2[i][9]));
                        }
                    }
                }

                if ( columna.PisosDesechos.Count > 0 )
                {
                    for ( int j = 0 ; j < columna.PisosDesechos.Count ; j++ )
                    {
                        List<string> Story_Result = new List<string>(); List<string> Load = new List<string>();
                        List<float> Loc = new List<float>();
                        List<float> P = new List<float>();
                        List<float> V2 = new List<float>();
                        List<float> V3 = new List<float>();
                        List<float> T = new List<float>();
                        List<float> M2 = new List<float>();
                        List<float> M3 = new List<float>();

                        for ( int i = 0 ; i < Resultados2.Count ; i++ )
                        {
                            if ( columna.Name == Resultados2[i][1] && columna.PisosDesechos[j].Item1 == Resultados2[i][0] )
                            {
                                Story_Result.Add(columna.Seccions[columna.PisosDesechos[j].Item2].Item2);
                                Load.Add(Resultados2[i][2]);
                                Loc.Add(Convert.ToSingle(Resultados2[i][3]));
                                P.Add(-Convert.ToSingle(Resultados2[i][4]));
                                V2.Add(Convert.ToSingle(Resultados2[i][5]));
                                V3.Add(Convert.ToSingle(Resultados2[i][6]));
                                T.Add(Convert.ToSingle(Resultados2[i][7]));
                                M2.Add(Convert.ToSingle(Resultados2[i][8]));
                                M3.Add(Convert.ToSingle(Resultados2[i][9]));
                            }
                        }

                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].Story_Result.AddRange(Story_Result);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].Load.AddRange(Load);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].Loc.AddRange(Loc);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].P.AddRange(P);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].V2.AddRange(V2);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].V3.AddRange(V3);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].T.AddRange(T);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].M2.AddRange(M2);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].M3.AddRange(M3);
                    }
                }
            }

            foreach ( Columna columna in Proyecto_.Lista_Columnas )
            {
                for ( int j = 0 ; j < columna.resultadosETABs.Count ; j++ )
                {
                    List<float[]> PM2M3V2V3 = new List<float[]>();
                    List<float> Vcx = new List<float>(); List<float> Vsx = new List<float>();
                    List<float> Vcy = new List<float>(); List<float> Vsy = new List<float>();
                    int ParaAcero = 0;
                    for ( int k = 0 ; k < columna.resultadosETABs[j].Load.Count ; k++ )
                    {
                        if ( columna.resultadosETABs[j].Load[k].Contains("SU") )
                        {
                            PM2M3V2V3.Add(new float[] { columna.resultadosETABs[j].P[k] , columna.resultadosETABs[j].M2[k] , columna.resultadosETABs[j].M3[k] , columna.resultadosETABs[j].V2[k] , columna.resultadosETABs[j].V3[k] , (float)columna.resultadosETABs[j].As[ParaAcero] * 10000 });
                            Vcx.Add(0); Vcy.Add(0); Vsx.Add(0); Vsy.Add(0);
                        }
                        ParaAcero += 1;
                        if ( ParaAcero > columna.resultadosETABs[j].As.Count - 1 )
                        {
                            ParaAcero = 0;
                        }
                    }

                    ISeccion seccion = FunctionsProject.DeepClone(columna.Seccions[j].Item1);
                    seccion.PM2M3V2V3 = PM2M3V2V3;
                    for ( int k = 0 ; k < PM2M3V2V3.Count ; k++ )
                    {
                        float FC1 = 1000; float FC2 = 100000;
                        float r = 0.04f;
                        float As = PM2M3V2V3[k][5];
                        float Vu3 = PM2M3V2V3[k][4] * FC1;
                        float Vu2 = PM2M3V2V3[k][3] * FC1;
                        float Nu = PM2M3V2V3[k][0] * FC1;
                        float Mu2 = PM2M3V2V3[k][1] * FC2;
                        float Mu3 = PM2M3V2V3[k][2] * FC2;
                        float h = seccion.H * 100;
                        float bw = seccion.B * 100;
                        float Area = (float)seccion.Area * 10000;
                        //VCx
                        float d = ( seccion.B - r ) * 100;
                        Vcx[k] = FunctionsProject.VcDef(seccion.Material.FC , As , Math.Abs(Vu3) , d , h , Math.Abs(Mu2) , bw , Nu , Area);
                        Vsx[k] = Math.Abs(Vu3) / FC1 - Vcx[k];
                        if ( Vsx[k] < 0 )
                        {
                            Vsx[k] = 0;
                        }
                        //VCy
                        d = ( seccion.H - r ) * 100;
                        Vcy[k] = FunctionsProject.VcDef(seccion.Material.FC , As , Math.Abs(Vu2) , d , bw , Math.Abs(Mu3) , h , Nu , Area);
                        Vsy[k] = Math.Abs(Vu2) / FC1 - Vcy[k];
                        if ( Vsy[k] < 0 )
                        {
                            Vsy[k] = 0;
                        }
                    }
                    seccion.Vcx = Vcx; seccion.Vcy = Vcy; seccion.Vsx = Vsx; seccion.Vsy = Vsy;

                    Tuple<ISeccion , string> TupleAux = new Tuple<ISeccion , string>(seccion , columna.Seccions[j].Item2);
                    columna.Seccions[j] = TupleAux;
                }
            }
        }

        private void AsignarResultados ()
        {
            List<List<string>> Resultados2 = new List<List<string>>();

            for ( int i = 0 ; i < ArchivoResultados2009.Count ; i++ )
            {
                if ( ArchivoResultados2009[i] != null )
                {
                    Resultados2.Add(ArchivoResultados2009[i].Split(Convert.ToChar(";")).ToList());
                }
            }

            foreach ( Columna columna in Proyecto_.Lista_Columnas )
            {
                columna.resultadosETABs = new List<ResultadosETABS>();

                for ( int j = 0 ; j < columna.Seccions.Count ; j++ )
                {
                    ResultadosETABS resultados = new ResultadosETABS();
                    for ( int i = 1 ; i < Resultados2.Count ; i++ )
                    {
                        if ( columna.Name == Resultados2[i][1] && columna.Seccions[j].Item2 == Resultados2[i][0] )
                        {
                            resultados.Estacion.Add(Convert.ToSingle(Resultados2[i][3]));
                            resultados.Asmin.Add(Convert.ToSingle(Resultados2[i][7]));
                            resultados.As.Add(Convert.ToSingle(Resultados2[i][8]));
                        }
                    }

                    columna.resultadosETABs.Add(resultados);
                    columna.AsignarAsTopMediumButton_();
                }

                if ( columna.PisosDesechos.Count > 0 )
                {
                    for ( int j = 0 ; j < columna.PisosDesechos.Count ; j++ )
                    {
                        List<float> EstacionesRestantes = new List<float>();
                        List<double> AsminRestantes = new List<double>();
                        List<double> AsRestantes = new List<double>();

                        for ( int i = 0 ; i < Resultados2.Count ; i++ )
                        {
                            if ( columna.Name == Resultados2[i][1] && columna.PisosDesechos[j].Item1 == Resultados2[i][0] )
                            {
                                EstacionesRestantes.Add(Convert.ToSingle(Resultados2[i][3]));
                                AsminRestantes.Add(Convert.ToSingle(Resultados2[i][7]));
                                AsRestantes.Add(Convert.ToSingle(Resultados2[i][8]));
                            }
                        }

                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].Estacion.AddRange(EstacionesRestantes);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].Asmin.AddRange(AsminRestantes);
                        columna.resultadosETABs[columna.PisosDesechos[j].Item2].As.AddRange(AsRestantes);
                    }
                    columna.AsignarAsTopMediumButton_();
                }
            }
        }

        private void CrearObjetosNecesarios2016_2017 ()
        {
            //STORIES
            CreateDictonaries();

            List<List<string>> Stories = new List<List<string>>();

            Proyecto_.Stories = new List<Tuple<string , float>>();

            int Inicio = ArchivoE2KETABS.FindIndex(x => x.Contains("$ STORIES - IN SEQUENCE FROM TOP")) + 1;
            int Fin = ArchivoE2KETABS.FindIndex(x => x.Contains("$ GRIDS")) - 2;

            for ( int i = Inicio ; i < Fin ; i++ )
            {
                Stories.Add(ArchivoE2KETABS[i].Split((char)34).ToList());
            }
            for ( int i = 0 ; i < Stories.Count ; i++ )
            {
                try
                {
                    string Name_Story = Stories[i][1].Replace("\"" , "");
                    float Altura_Story = (float)Convert.ToDouble(Stories[i][2].Replace("  HEIGHT " , "").Split().ToList()[0]);

                    Tuple<string , float> tuple_Aux = new Tuple<string , float>(Name_Story , Altura_Story);
                    Proyecto_.Stories.Add(tuple_Aux);
                }
                catch { }
            }

            //Puntos
            int Inicio_PointCoordinates = ArchivoE2KETABS.FindIndex(x => x.Contains("$ POINT COORDINATES")) + 1;

            int Final_PointCoordinates = ArchivoE2KETABS.FindIndex(x => x.Contains("$ LINE CONNECTIVITIES")) - 1;
            List<List<string>> PointsConnectivities = new List<List<string>>();

            for ( int i = Inicio_PointCoordinates ; i < Final_PointCoordinates ; i++ )
            {
                PointsConnectivities.Add(ArchivoE2KETABS[i].Split().ToList());
            }

            List<Tuple<string , double[]>> Points = new List<Tuple<string , double[]>>();

            for ( int i = 0 ; i < PointsConnectivities.Count ; i++ )
            {
                string PointNumber = PointsConnectivities[i][3].Replace("\"" , "");
                double[] XY = new double[] { Convert.ToDouble(PointsConnectivities[i][5]) , Convert.ToDouble(PointsConnectivities[i][6]) };

                Tuple<string , double[]> Point = new Tuple<string , double[]>(PointNumber , XY);
                Points.Add(Point);
            }

            // Definir Materiales

            int Inicio_MaterialProperties = ArchivoE2KETABS.FindIndex(x => x.Contains("$ MATERIAL PROPERTIES")) + 1;
            int Final_MaterialProperties = ArchivoE2KETABS.FindIndex(x => x.Contains("$ REBAR DEFINITIONS")) - 1;

            List<List<string>> Lista_Materiales_Aux2 = new List<List<string>>();
            List<string> Lista_Materiales_Aux = new List<string>();

            for ( int i = Inicio_MaterialProperties ; i < Final_MaterialProperties ; i++ )
            {
                Lista_Materiales_Aux.Add(ArchivoE2KETABS[i]);
            }

            //Solo Encontrara Material Tipo Concreto

            Proyecto_.Lista_Materiales = new List<MAT_CONCRETE>();

            for ( int i = 0 ; i < Lista_Materiales_Aux.Count ; i++ )
            {
                if ( Lista_Materiales_Aux[i].Contains(" FC ") )
                {
                    Lista_Materiales_Aux2.Add(Lista_Materiales_Aux[i].Split().ToList());
                }
            }

            for ( int i = 0 ; i < Lista_Materiales_Aux2.Count ; i++ )
            {
                MAT_CONCRETE material = new MAT_CONCRETE();
                material.Name = Lista_Materiales_Aux2[i][4].Replace("\"" , "");

                material.FC = (float)Convert.ToDouble(Lista_Materiales_Aux2[i][9]) / 10;
                Proyecto_.Lista_Materiales.Add(material);
            }

            //Secciones

            Proyecto_.Lista_Secciones = new List<ISeccion>();

            int Inicio_FrameSecctions = ArchivoE2KETABS.FindIndex(x => x.Contains("$ FRAME SECTIONS")) + 1;

            int Final_FrameSecctions = ArchivoE2KETABS.FindIndex(x => x.Contains("$ CONCRETE SECTIONS")) - 1;

            List<List<string>> Lista_Secciones_Aux = new List<List<string>>();

            for ( int i = Inicio_FrameSecctions ; i < Final_FrameSecctions ; i++ )
            {
                if ( ArchivoE2KETABS[i].Contains("Rectangular") | ArchivoE2KETABS[i].Contains("Circle") | ArchivoE2KETABS[i].Contains("Te") | ArchivoE2KETABS[i].Contains("Angle") | ArchivoE2KETABS[i].Contains("SD Section") )
                {
                    Lista_Secciones_Aux.Add(ArchivoE2KETABS[i].Split().ToList());
                }
            }

            for ( int i = 0 ; i < Lista_Secciones_Aux.Count ; i++ )
            {
                string Nombre = Lista_Secciones_Aux[i][4].Replace("\"" , "");
                string Material_Aux = Lista_Secciones_Aux[i][7].Replace("\"" , "");
                string SHAPE = Lista_Secciones_Aux[i][11].Replace("\"" , "");
                List<float[]> Coord = null;
                TipodeSeccion tipodeSeccion;
                float TF, TW, B, H;

                if ( SHAPE == "Section" )
                {
                    //Section Designer

                    int Inicio_DesignerSections = ArchivoE2KETABS.FindIndex(x => x.Contains("$ SECTION DESIGNER SECTIONS")) + 1;
                    int Final_DesignerSection = ArchivoE2KETABS.FindIndex(x => x.Contains("$ SLAB PROPERTIES")) - 1;

                    List<List<string>> SectionDesginer = new List<List<string>>();

                    int NoPuntos = 0;
                    for ( int j = Inicio_DesignerSections ; j < Final_DesignerSection ; j++ )
                    {
                        SectionDesginer.Add(ArchivoE2KETABS[j].Split((char)34).ToList());
                    }

                    for ( int k = 0 ; k < SectionDesginer.Count ; k++ )
                    {
                        if ( SectionDesginer[k].Count == 9 )
                        {
                            if ( SectionDesginer[k][5] == "POLYGON" && SectionDesginer[k][1] == Nombre )
                            {
                                NoPuntos = Convert.ToInt32(SectionDesginer[k][6].Replace("  NUMCORNERPTS " , ""));
                                Coord = new List<float[]>();

                                for ( int s = k + 1 ; s < k + NoPuntos + 1 ; s++ )
                                {
                                    string[] SDASD = SectionDesginer[s][2].Split();
                                    float X = Convert.ToSingle(SectionDesginer[s][2].Split()[9]);
                                    float Y = Convert.ToSingle(SectionDesginer[s][2].Split()[12]);

                                    float[] XY = { X , Y };
                                    Coord.Add(XY);
                                }
                                break;
                            }
                        }
                    }

                    tipodeSeccion = TipodeSeccion.None;

                    if ( NoPuntos == 8 )
                    {
                        tipodeSeccion = TipodeSeccion.Tee;
                    }
                    else if ( NoPuntos == 6 )
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
                    if ( SHAPE == "Rectangular" )
                    {
                        tipodeSeccion = TipodeSeccion.Rectangular;
                    }
                    else if ( SHAPE == "Circle" )
                    {
                        tipodeSeccion = TipodeSeccion.Circle;
                    }
                    else if ( SHAPE == "Tee" )
                    {
                        tipodeSeccion = TipodeSeccion.Tee;
                    }
                    else if ( SHAPE == "Angle" )
                    {
                        tipodeSeccion = TipodeSeccion.L;
                    }
                    else { tipodeSeccion = TipodeSeccion.None; }

                    H = (float)Convert.ToDouble(Lista_Secciones_Aux[i][14]);

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

                foreach ( MAT_CONCRETE mAT_ in Proyecto_.Lista_Materiales )
                {
                    if ( mAT_.Name == Material_Aux )
                    {
                        ISeccion seccion = new CRectangulo(Nombre , B , H , mAT_ , tipodeSeccion , Coord);

                        if ( tipodeSeccion == TipodeSeccion.Rectangular )
                        {
                            seccion = new CRectangulo(Nombre , B , H , mAT_ , tipodeSeccion , Coord);
                        }

                        if ( tipodeSeccion == TipodeSeccion.Circle )
                        {
                            seccion = new CCirculo(Nombre , B / 2 , pCentro: new double[] { 0 , 0 } , Material_: mAT_ , Shape_: tipodeSeccion , pCoord: Coord);
                        }

                        if ( tipodeSeccion == TipodeSeccion.Tee | tipodeSeccion == TipodeSeccion.L )
                        {
                            float pTw, pTf;
                            float pH, pB;

                            var Xunicos = Coord.Select(x => x[0]).Distinct().ToList();
                            var Yunicos = Coord.Select(x => x[1]).Distinct().ToList();

                            var p = FunctionsProject.Dimension2(Coord , true); //Dimenciones en X
                            var q = FunctionsProject.Dimension2(Coord , false); //Dimenciones en Y

                            pB = (float)p[0];
                            pTw = (float)p[1];

                            pH = (float)q[0];
                            pTf = (float)q[1];

                            //pTw = FunctionsProject.Dimension(Xunicos, false);
                            //pTf = FunctionsProject.Dimension(Yunicos, false);
                            //pB = FunctionsProject.Dimension(Xunicos, true);
                            //pH = FunctionsProject.Dimension(Yunicos, true);

                            seccion = new CSD(Nombre , pB , pH , pTw , pTf , mAT_ , tipodeSeccion , Coord);
                        }

                        Proyecto_.Lista_Secciones.Add(seccion);
                    }
                }
            }

            //TUPLAS DE COLUMNAS Y VIGAS

            List<List<string>> LineConectives = new List<List<string>>();

            int Inicio_LineConnectives = ArchivoE2KETABS.FindIndex(x => x.Contains("$ LINE CONNECTIVITIES")) + 1;

            int Final_LineConnectives = ArchivoE2KETABS.FindIndex(x => x.Contains("$ AREA CONNECTIVITIES")) - 1;

            for ( int i = Inicio_LineConnectives ; i < Final_LineConnectives ; i++ )
            {
                LineConectives.Add(ArchivoE2KETABS[i].Split().ToList());
            }

            List<Tuple<string , string , double[]>> AuxColums = new List<Tuple<string , string , double[]>>();

            List<Tuple<string , string[] , double[]>> AuxBeams = new List<Tuple<string , string[] , double[]>>();

            for ( int i = 0 ; i < LineConectives.Count ; i++ )
            {
                if ( LineConectives[i][6] == "COLUMN" )
                {
                    string Name = LineConectives[i][4].Replace("\"" , "");
                    string PointNumber = LineConectives[i][8].Replace("\"" , "");
                    Tuple<string , string , double[]> Colum_Tu = new Tuple<string , string , double[]>(Name , PointNumber , new double[2]);
                    AuxColums.Add(Colum_Tu);
                }

                if ( LineConectives[i][6] == "BEAM" )
                {
                    string Name = LineConectives[i][4].Replace("\"" , "");
                    string[] PointNumber = new string[] { LineConectives[i][8].Replace("\"" , "") , LineConectives[i][10].Replace("\"" , "") };

                    Tuple<string , string[] , double[]> Beam_Tu = new Tuple<string , string[] , double[]>(Name , PointNumber , new double[2]);
                    AuxBeams.Add(Beam_Tu);
                }
            }

            // LINE ASSIGNS

            List<List<string>> LineAssingns = new List<List<string>>();

            Inicio = ArchivoE2KETABS.FindIndex(x => x.Contains("$ LINE ASSIGNS")) + 1;

            Fin = ArchivoE2KETABS.FindIndex(x => x.Contains("$ AREA ASSIGNS")) - 1;

            for ( int i = Inicio ; i < Fin ; i++ )
            {
                LineAssingns.Add(ArchivoE2KETABS[i].Split((char)34).ToList());
            }

            // OBJETOS VIGAS

            Proyecto_.Lista_Vigas = new List<Viga>();

            for ( int i = 0 ; i < AuxBeams.Count ; i++ )
            {
                Viga viga = new Viga(AuxBeams[i].Item1);
                viga.Points = AuxBeams[i].Item2;
                for ( int j = 0 ; j < Points.Count ; j++ )
                {
                    if ( AuxBeams[i].Item2[0] == Points[j].Item1 )
                    {
                        viga.CoordXY1 = Points[j].Item2;
                    }
                    if ( AuxBeams[i].Item2[1] == Points[j].Item1 )
                    {
                        viga.CoordXY2 = Points[j].Item2;
                    }
                }
                Proyecto_.Lista_Vigas.Add(viga);
            }

            //Asignar Secciones a Vigas por Piso

            for ( int i = 0 ; i < Proyecto_.Stories.Count ; i++ )
            {
                for ( int j = 0 ; j < LineAssingns.Count ; j++ )
                {
                    if ( LineAssingns[j].Count > 8 )
                    {
                        string Story = LineAssingns[j][3].Replace("\"" , "");
                        string NameBeam = LineAssingns[j][1].Replace("\"" , "");
                        string NameSeccion = LineAssingns[j][5].Replace("\"" , "");
                        foreach ( Viga viga in Proyecto_.Lista_Vigas )
                        {
                            if ( viga.Name == NameBeam && Story == Proyecto_.Stories[i].Item1 )
                            {
                                ISeccion seccion = Proyecto_.Lista_Secciones.Find(x => x.Name == NameSeccion);
                                Tuple<CRectangulo , string> tuple_aux = new Tuple<CRectangulo , string>((CRectangulo)seccion , Story);
                                viga.Seccions.Add(tuple_aux);
                            }
                        }
                    }
                }
            }

            //OBJETOS COLUMNA

            Proyecto_.Lista_Columnas = new List<Columna>();

            for ( int i = 0 ; i < AuxColums.Count ; i++ )
            {
                Columna columna = new Columna(AuxColums[i].Item1);

                for ( int j = 0 ; j < Points.Count ; j++ )
                {
                    if ( AuxColums[i].Item2 == Points[j].Item1 )
                    {
                        columna.CoordXY = Points[j].Item2;
                        columna.Point = AuxColums[i].Item2;
                    }
                }
                Proyecto_.Lista_Columnas.Add(columna);
            }

            //Asignar Secciones a Columnas por Piso

            for ( int i = 0 ; i < Proyecto_.Stories.Count ; i++ )
            {
                for ( int j = 0 ; j < LineAssingns.Count ; j++ )
                {
                    if ( LineAssingns[j].Count > 8 )
                    {
                        string Story = LineAssingns[j][3].Replace("\"" , "");
                        string NameColum = LineAssingns[j][1].Replace("\"" , "");
                        string NameSeccion = LineAssingns[j][5].Replace("\"" , "");
                        foreach ( Columna colum in Proyecto_.Lista_Columnas )
                        {
                            if ( colum.Name == NameColum && Story == Proyecto_.Stories[i].Item1 )
                            {
                                ISeccion seccion = Proyecto_.Lista_Secciones.Find(x => x.Name == NameSeccion);

                                if ( seccion.Shape == TipodeSeccion.None )
                                {
                                    seccion = null;
                                }
                                else
                                {
                                    Tuple<ISeccion , string> tuple_aux = new Tuple<ISeccion , string>(seccion , Story);
                                    colum.Seccions.Add(tuple_aux);
                                }
                            }
                        }
                    }
                }
            }

            //Determinar Viga por Piso

            foreach ( Columna column in Proyecto_.Lista_Columnas )
            {
                List<Viga> vigasPosibles = new List<Viga>();

                foreach ( Viga viga in Proyecto_.Lista_Vigas )
                {
                    if ( viga.Points[0] == column.Point | viga.Points[1] == column.Point )
                    {
                        vigasPosibles.Add(viga);
                    }
                }

                Viga VigaMayor = new Viga("Viga con Mayor H Por Piso");
                ISeccion seccionMayor = new CRectangulo("Inicial" , 0 , -99999 , new MAT_CONCRETE() , TipodeSeccion.None);
                Tuple<CRectangulo , string> tuple_Seccion_Mayor = null;

                for ( int i = 0 ; i < column.Seccions.Count ; i++ )
                {
                    tuple_Seccion_Mayor = new Tuple<CRectangulo , string>((CRectangulo)seccionMayor , column.Seccions[i].Item2);
                    VigaMayor.Seccions.Add(tuple_Seccion_Mayor);
                }

                foreach ( Viga viga1 in vigasPosibles )
                {
                    for ( int i = 0 ; i < viga1.Seccions.Count ; i++ )
                    {
                        for ( int j = 0 ; j < VigaMayor.Seccions.Count ; j++ )
                        {
                            if ( viga1.Seccions[i].Item2 == VigaMayor.Seccions[j].Item2 )
                            {
                                try
                                {
                                    if ( viga1.Seccions[i].Item1.H > VigaMayor.Seccions[j].Item1.H )
                                    {
                                        VigaMayor.Seccions[j] = viga1.Seccions[i];
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }

                column.VigaMayor = VigaMayor;
            }
            //Asignar Altura Libre;

            foreach ( Columna columna1 in Proyecto_.Lista_Columnas )
            {
                columna1.LuzLibre = new List<float>();
                for ( int i = 0 ; i < columna1.Seccions.Count ; i++ )
                {
                    columna1.LuzLibre.Add(0);
                }
                for ( int i = 0 ; i < columna1.Seccions.Count ; i++ )
                {
                    for ( int j = 0 ; j < Proyecto_.Stories.Count ; j++ )
                    {
                        if ( columna1.Seccions[i].Item2 == Proyecto_.Stories[j].Item1 )
                        {
                            columna1.LuzLibre[i] = ( Proyecto_.Stories[j].Item2 - columna1.VigaMayor.Seccions[i].Item1.H );
                        }
                    }
                }
            }

            //Crear Lista de Estribos;

            //Depurar Columnas con Area Menor a 400cm2
            Proyecto_.Lista_Columnas.RemoveAll(x => x.Seccions[x.Seccions.Count - 1].Item1.Area < 0.04f);
        }

        private void CrearObjetosNecesarios2009 ()
        {
            //STORIES
            CreateDictonaries();

            List<List<string>> Stories = new List<List<string>>();

            Proyecto_.Stories = new List<Tuple<string , float>>();

            int Inicio = ArchivoE2KETABS.FindIndex(x => x.Contains("$ STORIES - IN SEQUENCE FROM TOP")) + 1;
            int Fin = ArchivoE2KETABS.FindIndex(x => x.Contains("$ DIAPHRAGM NAMES")) - 2;

            for ( int i = Inicio ; i < Fin ; i++ )
            {
                Stories.Add(ArchivoE2KETABS[i].Split((char)34).ToList());
            }
            for ( int i = 0 ; i < Stories.Count ; i++ )
            {
                try
                {
                    string Name_Story = Stories[i][1].Replace("\"" , "");
                    float Altura_Story = (float)Convert.ToDouble(Stories[i][2].Replace("  HEIGHT " , "").Split().ToList()[0]);

                    Tuple<string , float> tuple_Aux = new Tuple<string , float>(Name_Story , Altura_Story);
                    Proyecto_.Stories.Add(tuple_Aux);
                }
                catch { }
            }

            //Puntos
            int Inicio_PointCoordinates = ArchivoE2KETABS.FindIndex(x => x.Contains("$ POINT COORDINATES")) + 1;

            int Final_PointCoordinates = ArchivoE2KETABS.FindIndex(x => x.Contains("$ LINE CONNECTIVITIES")) - 1;
            List<List<string>> PointsConnectivities = new List<List<string>>();

            for ( int i = Inicio_PointCoordinates ; i < Final_PointCoordinates ; i++ )
            {
                PointsConnectivities.Add(ArchivoE2KETABS[i].Split().ToList());
            }

            List<Tuple<string , double[]>> Points = new List<Tuple<string , double[]>>();

            for ( int i = 0 ; i < PointsConnectivities.Count ; i++ )
            {
                string PointNumber = PointsConnectivities[i][3].Replace("\"" , "");
                double[] XY = new double[] { Convert.ToDouble(PointsConnectivities[i][5]) , Convert.ToDouble(PointsConnectivities[i][6]) };

                Tuple<string , double[]> Point = new Tuple<string , double[]>(PointNumber , XY);
                Points.Add(Point);
            }

            // Definir Materiales

            int Inicio_MaterialProperties = ArchivoE2KETABS.FindIndex(x => x.Contains("$ MATERIAL PROPERTIES")) + 1;
            int Final_MaterialProperties = ArchivoE2KETABS.FindIndex(x => x.Contains("$ FRAME SECTIONS")) - 1;

            List<List<string>> Lista_Materiales_Aux2 = new List<List<string>>();
            List<string> Lista_Materiales_Aux = new List<string>();

            for ( int i = Inicio_MaterialProperties ; i < Final_MaterialProperties ; i++ )
            {
                Lista_Materiales_Aux.Add(ArchivoE2KETABS[i]);
            }

            //Solo Encontrara Material Tipo Concreto

            Proyecto_.Lista_Materiales = new List<MAT_CONCRETE>();

            for ( int i = 0 ; i < Lista_Materiales_Aux.Count ; i++ )
            {
                if ( Lista_Materiales_Aux[i].Contains("FC") )
                {
                    Lista_Materiales_Aux2.Add(Lista_Materiales_Aux[i].Split().ToList());
                }
            }

            for ( int i = 0 ; i < Lista_Materiales_Aux2.Count ; i++ )
            {
                MAT_CONCRETE material = new MAT_CONCRETE();
                material.Name = Lista_Materiales_Aux2[i][4].Replace("\"" , "");
                try
                {
                    material.FC = (float)Convert.ToDouble(Lista_Materiales_Aux2[i][13].Replace("\"" , "")) / 10;
                }
                catch
                {
                    material.FC = (float)Convert.ToDouble(Lista_Materiales_Aux2[i][9]) / 10;
                }
                Proyecto_.Lista_Materiales.Add(material);
            }

            //Secciones

            Proyecto_.Lista_Secciones = new List<ISeccion>();

            int Inicio_FrameSecctions = ArchivoE2KETABS.FindIndex(x => x.Contains("$ FRAME SECTIONS")) + 1;

            int Final_FrameSecctions = ArchivoE2KETABS.FindIndex(x => x.Contains("$ REBAR DEFINITIONS")) - 1;

            if ( Final_FrameSecctions - Inicio_FrameSecctions < 0 )
            {
                Final_FrameSecctions = ArchivoE2KETABS.FindIndex(x => x.Contains("$ CONCRETE SECTIONS")) - 1;
            }

            //Concrete Sections
            int Inicio_ConcreteSections = ArchivoE2KETABS.FindIndex(x => x.Contains("$ CONCRETE SECTIONS")) + 1;
            int Final_ConcreteSections = ArchivoE2KETABS.FindIndex(x => x.Contains("$ WALL/SLAB/DECK PROPERTIES")) + 1;

            List<List<string>> Lista_Secciones_Aux = new List<List<string>>();
            List<List<string>> Lista_Secciones_ConcreteSections = new List<List<string>>();

            for ( int i = Inicio_FrameSecctions ; i < Final_FrameSecctions ; i++ )
            {
                if ( ArchivoE2KETABS[i].Contains("Rectangular") | ArchivoE2KETABS[i].Contains("Circle") | ArchivoE2KETABS[i].Contains("Te") | ArchivoE2KETABS[i].Contains("Angle") | ArchivoE2KETABS[i].Contains("SD Section") )
                {
                    if ( ArchivoE2KETABS[i].Contains("SD Section") )
                    {
                    }
                    Lista_Secciones_Aux.Add(ArchivoE2KETABS[i].Split().ToList());
                }
            }

            for ( int i = Inicio_ConcreteSections ; i < Final_ConcreteSections ; i++ )
            {
                Lista_Secciones_ConcreteSections.Add(ArchivoE2KETABS[i].Split().ToList());
            }

            for ( int i = 0 ; i < Lista_Secciones_Aux.Count ; i++ )
            {
                string Nombre = Lista_Secciones_Aux[i][4].Replace("\"" , "");
                string Material_Aux = Lista_Secciones_Aux[i][7].Replace("\"" , "");
                string SHAPE = Lista_Secciones_Aux[i][10].Replace("\"" , "");
                List<float[]> Coord = null;
                TipodeSeccion tipodeSeccion;
                ConcreteSections Type = ConcreteSections.None;
                for ( int j = 0 ; j < Lista_Secciones_ConcreteSections.Count ; j++ )
                {
                    if ( Lista_Secciones_ConcreteSections[j].Count >= 24 )
                    {
                        string NameComparar = Lista_Secciones_ConcreteSections[j][4].Replace("\"" , "");
                        string Tipo = Lista_Secciones_ConcreteSections[j][7].Replace("\"" , "");
                        if ( NameComparar == Nombre )
                        {
                            if ( Tipo == "BEAM" )
                            {
                                Type = ConcreteSections.Beam;
                            }
                            else if ( Tipo == "COLUMN" )
                            {
                                Type = ConcreteSections.Colum;
                            }
                            break;
                        }
                    }
                }

                float TF, TW, B, H;

                if ( SHAPE == "SD" )
                {
                    //Section Designer

                    int Inicio_DesignerSections = ArchivoE2KETABS.FindIndex(x => x.Contains("$ SECTION DESIGNER SECTIONS")) + 1;
                    int Final_DesignerSection = ArchivoE2KETABS.FindIndex(x => x.Contains("$ WALL/SLAB/DECK PROPERTIES")) - 1;

                    List<List<string>> SectionDesginer = new List<List<string>>();

                    int NoPuntos = 0;
                    for ( int j = Inicio_DesignerSections ; j < Final_DesignerSection ; j++ )
                    {
                        SectionDesginer.Add(ArchivoE2KETABS[j].Split((char)34).ToList());
                    }

                    for ( int k = 0 ; k < SectionDesginer.Count ; k++ )
                    {
                        if ( SectionDesginer[k].Count == 7 )
                        {
                            if ( SectionDesginer[k][5] == "POLYGON" && SectionDesginer[k][1] == Nombre )
                            {
                                NoPuntos = Convert.ToInt32(SectionDesginer[k][6].Replace("  NUMCORNERPTS " , ""));
                                Coord = new List<float[]>();

                                for ( int s = k + 1 ; s < k + NoPuntos + 1 ; s++ )
                                {
                                    string[] SDASD = SectionDesginer[s][2].Split();
                                    float X = Convert.ToSingle(SectionDesginer[s][2].Split()[9]);
                                    float Y = Convert.ToSingle(SectionDesginer[s][2].Split()[12]);

                                    float[] XY = { X , Y };
                                    Coord.Add(XY);
                                }
                                break;
                            }
                        }
                    }

                    tipodeSeccion = TipodeSeccion.None;

                    if ( NoPuntos == 8 )
                    {
                        tipodeSeccion = TipodeSeccion.Tee;
                    }
                    else if ( NoPuntos == 6 )
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
                    if ( SHAPE == "Rectangular" )
                    {
                        tipodeSeccion = TipodeSeccion.Rectangular;
                    }
                    else if ( SHAPE == "Circle" )
                    {
                        tipodeSeccion = TipodeSeccion.Circle;
                    }
                    else if ( SHAPE == "Tee" )
                    {
                        tipodeSeccion = TipodeSeccion.Tee;
                    }
                    else if ( SHAPE == "Angle" )
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

                if ( Type == ConcreteSections.Colum )
                {
                    if ( tipodeSeccion != TipodeSeccion.Circle )
                    {
                        float AuxH = H;
                        H = B;
                        B = AuxH;
                    }
                }

                foreach ( MAT_CONCRETE mAT_ in Proyecto_.Lista_Materiales )
                {
                    if ( mAT_.Name == Material_Aux )
                    {
                        ISeccion seccion = new CRectangulo(Nombre , B , H , mAT_ , tipodeSeccion , Coord);

                        if ( tipodeSeccion == TipodeSeccion.Rectangular )
                        {
                            seccion = new CRectangulo(Nombre , B , H , mAT_ , tipodeSeccion , Coord);
                        }

                        if ( tipodeSeccion == TipodeSeccion.Circle )
                        {
                            seccion = new CCirculo(Nombre , B / 2 , pCentro: new double[] { 0 , 0 } , Material_: mAT_ , Shape_: tipodeSeccion , pCoord: Coord);
                        }

                        if ( tipodeSeccion == TipodeSeccion.Tee | tipodeSeccion == TipodeSeccion.L )
                        {
                            float pTw, pTf;
                            float pH, pB;

                            var Xunicos = Coord.Select(x => x[0]).Distinct().ToList();
                            var Yunicos = Coord.Select(x => x[1]).Distinct().ToList();

                            var p = FunctionsProject.Dimension2(Coord , true); //Dimensiones en X
                            var q = FunctionsProject.Dimension2(Coord , false); //Dimensiones en Y

                            pB = (float)p[0];
                            pTw = (float)p[1];

                            pH = (float)q[0];
                            pTf = (float)q[1];

                            //pTw = FunctionsProject.Dimension(Xunicos, false);
                            //pTf = FunctionsProject.Dimension(Yunicos, false);
                            //pB = FunctionsProject.Dimension(Xunicos, true);
                            //pH = FunctionsProject.Dimension(Yunicos, true);

                            seccion = new CSD(Nombre , pB , pH , pTw , pTf , mAT_ , tipodeSeccion , Coord);
                        }

                        Proyecto_.Lista_Secciones.Add(seccion);
                    }
                }
            }

            //TUPLAS DE COLUMNAS Y VIGAS

            List<List<string>> LineConectives = new List<List<string>>();

            int Inicio_LineConnectives = ArchivoE2KETABS.FindIndex(x => x.Contains("$ LINE CONNECTIVITIES")) + 1;

            int Final_LineConnectives = ArchivoE2KETABS.FindIndex(x => x.Contains("$ AREA CONNECTIVITIES")) - 1;

            for ( int i = Inicio_LineConnectives ; i < Final_LineConnectives ; i++ )
            {
                LineConectives.Add(ArchivoE2KETABS[i].Split().ToList());
            }

            List<Tuple<string , string , double[]>> AuxColums = new List<Tuple<string , string , double[]>>();

            List<Tuple<string , string[] , double[]>> AuxBeams = new List<Tuple<string , string[] , double[]>>();

            for ( int i = 0 ; i < LineConectives.Count ; i++ )
            {
                if ( LineConectives[i][6] == "COLUMN" )
                {
                    string Name = LineConectives[i][4].Replace("\"" , "");
                    string PointNumber = LineConectives[i][8].Replace("\"" , "");
                    Tuple<string , string , double[]> Colum_Tu = new Tuple<string , string , double[]>(Name , PointNumber , new double[2]);
                    AuxColums.Add(Colum_Tu);
                }

                if ( LineConectives[i][6] == "BEAM" )
                {
                    string Name = LineConectives[i][4].Replace("\"" , "");
                    string[] PointNumber = new string[] { LineConectives[i][8].Replace("\"" , "") , LineConectives[i][10].Replace("\"" , "") };

                    Tuple<string , string[] , double[]> Beam_Tu = new Tuple<string , string[] , double[]>(Name , PointNumber , new double[2]);
                    AuxBeams.Add(Beam_Tu);
                }
            }

            // LINE ASSIGNS

            List<List<string>> LineAssingns = new List<List<string>>();

            Inicio = ArchivoE2KETABS.FindIndex(x => x.Contains("$ LINE ASSIGNS")) + 1;

            Fin = ArchivoE2KETABS.FindIndex(x => x.Contains("$ AREA ASSIGNS")) - 1;

            for ( int i = Inicio ; i < Fin ; i++ )
            {
                LineAssingns.Add(ArchivoE2KETABS[i].Split((char)34).ToList());
            }

            // OBJETOS VIGAS

            Proyecto_.Lista_Vigas = new List<Viga>();

            for ( int i = 0 ; i < AuxBeams.Count ; i++ )
            {
                Viga viga = new Viga(AuxBeams[i].Item1);
                viga.Points = AuxBeams[i].Item2;
                for ( int j = 0 ; j < Points.Count ; j++ )
                {
                    if ( AuxBeams[i].Item2[0] == Points[j].Item1 )
                    {
                        viga.CoordXY1 = Points[j].Item2;
                    }
                    if ( AuxBeams[i].Item2[1] == Points[j].Item1 )
                    {
                        viga.CoordXY2 = Points[j].Item2;
                    }
                }
                Proyecto_.Lista_Vigas.Add(viga);
            }

            //Asignar Secciones a Vigas por Piso

            for ( int i = 0 ; i < Proyecto_.Stories.Count ; i++ )
            {
                for ( int j = 0 ; j < LineAssingns.Count ; j++ )
                {
                    if ( LineAssingns[j].Count > 8 )
                    {
                        string Story = LineAssingns[j][3].Replace("\"" , "");
                        string NameBeam = LineAssingns[j][1].Replace("\"" , "");
                        string NameSeccion = LineAssingns[j][5].Replace("\"" , "");
                        foreach ( Viga viga in Proyecto_.Lista_Vigas )
                        {
                            if ( viga.Name == NameBeam && Story == Proyecto_.Stories[i].Item1 )
                            {
                                ISeccion seccion = Proyecto_.Lista_Secciones.Find(x => x.Name == NameSeccion);
                                Tuple<CRectangulo , string> tuple_aux = new Tuple<CRectangulo , string>((CRectangulo)seccion , Story);
                                viga.Seccions.Add(tuple_aux);
                            }
                        }
                    }
                }
            }

            //OBJETOS COLUMNA

            Proyecto_.Lista_Columnas = new List<Columna>();

            for ( int i = 0 ; i < AuxColums.Count ; i++ )
            {
                Columna columna = new Columna(AuxColums[i].Item1);

                for ( int j = 0 ; j < Points.Count ; j++ )
                {
                    if ( AuxColums[i].Item2 == Points[j].Item1 )
                    {
                        columna.CoordXY = Points[j].Item2;
                        columna.Point = AuxColums[i].Item2;
                    }
                }
                Proyecto_.Lista_Columnas.Add(columna);
            }

            for ( int i = 0 ; i < Proyecto_.Stories.Count ; i++ )
            {
                for ( int j = 0 ; j < LineAssingns.Count ; j++ )
                {
                    if ( LineAssingns[j].Count > 8 )
                    {
                        string Story = LineAssingns[j][3].Replace("\"" , "");
                        string NameColum = LineAssingns[j][1].Replace("\"" , "");
                        string NameSeccion = LineAssingns[j][5].Replace("\"" , "");
                        foreach ( Columna colum in Proyecto_.Lista_Columnas )
                        {
                            if ( colum.Name == NameColum && Story == Proyecto_.Stories[i].Item1 )
                            {
                                ISeccion seccion = Proyecto_.Lista_Secciones.Find(x => x.Name == NameSeccion);

                                if ( seccion.Shape == TipodeSeccion.None )
                                {
                                    seccion = null;
                                }
                                else
                                {
                                    Tuple<ISeccion , string> tuple_aux = new Tuple<ISeccion , string>(seccion , Story);
                                    colum.Seccions.Add(tuple_aux);
                                }
                            }
                        }
                    }
                }
            }

            //Determinar Viga por Piso

            foreach ( Columna column in Proyecto_.Lista_Columnas )
            {
                List<Viga> vigasPosibles = new List<Viga>();

                foreach ( Viga viga in Proyecto_.Lista_Vigas )
                {
                    if ( viga.Points[0] == column.Point | viga.Points[1] == column.Point )
                    {
                        vigasPosibles.Add(viga);
                    }
                }

                Viga VigaMayor = new Viga("Viga con Mayor H Por Piso");
                ISeccion seccionMayor = new CRectangulo("Inicial" , 0 , -99999 , new MAT_CONCRETE() , TipodeSeccion.None);
                Tuple<CRectangulo , string> tuple_Seccion_Mayor = null;
                column.VigasEnUnPunto = new List<Viga>();

                for ( int i = 0 ; i < column.Seccions.Count ; i++ )
                {
                    tuple_Seccion_Mayor = new Tuple<CRectangulo , string>((CRectangulo)seccionMayor , column.Seccions[i].Item2);
                    VigaMayor.Seccions.Add(tuple_Seccion_Mayor);
                }

                foreach ( Viga viga1 in vigasPosibles )
                {
                    for ( int i = 0 ; i < viga1.Seccions.Count ; i++ )
                    {
                        for ( int j = 0 ; j < VigaMayor.Seccions.Count ; j++ )
                        {
                            if ( viga1.Seccions[i].Item2 == VigaMayor.Seccions[j].Item2 )
                            {
                                try
                                {
                                    if ( viga1.Seccions[i].Item1.H > VigaMayor.Seccions[j].Item1.H )
                                    {
                                        VigaMayor.Seccions[j] = viga1.Seccions[i];
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }

                for ( int i = VigaMayor.Seccions.Count - 1 ; i >= 0 ; i-- )
                {
                    if ( VigaMayor.Seccions[i].Item1.H < 0 )
                    {
                        ISeccion seccionMayor2 = new CRectangulo("Inicial" , 0 , 0 , new MAT_CONCRETE() , TipodeSeccion.None);
                        tuple_Seccion_Mayor = new Tuple<CRectangulo , string>((CRectangulo)seccionMayor2 , column.Seccions[i].Item2);
                        VigaMayor.Seccions[i] = tuple_Seccion_Mayor;
                    }
                }
                column.VigasEnUnPunto.AddRange(vigasPosibles);
                column.VigaMayor = VigaMayor;
            }
            //Asignar Altura Libre;

            foreach ( Columna columna1 in Proyecto_.Lista_Columnas )
            {
                columna1.LuzLibre = new List<float>();
                for ( int i = 0 ; i < columna1.Seccions.Count ; i++ )
                {
                    columna1.LuzLibre.Add(0);
                }
                for ( int i = 0 ; i < columna1.Seccions.Count ; i++ )
                {
                    for ( int j = 0 ; j < Proyecto_.Stories.Count ; j++ )
                    {
                        if ( columna1.Seccions[i].Item2 == Proyecto_.Stories[j].Item1 )
                        {
                            columna1.LuzLibre[i] = ( Proyecto_.Stories[j].Item2 - columna1.VigaMayor.Seccions[i].Item1.H );
                        }
                    }
                }
            }

            //Depurar Columnas con Area Menor a 400cm2
            Proyecto_.Lista_Columnas.RemoveAll(x => x.Seccions[x.Seccions.Count - 1].Item1.Area < 0.04f);

            //Columnas sin Vigas -> Deshacer Pisos

            foreach ( Columna columna1 in Proyecto_.Lista_Columnas )
            {
                for ( int i = columna1.VigaMayor.Seccions.Count - 1 ; i >= 0 ; i-- )
                {
                    if ( columna1.VigaMayor.Seccions[i].Item1.H == 0 )
                    {
                        float LuzLibre1 = 0; List<Tuple<string , int>> Storys = new List<Tuple<string , int>>(); CRectangulo VigaFinal = null;

                        for ( int j = i - 1 ; j >= 0 ; j-- )
                        {
                            LuzLibre1 += columna1.LuzLibre[j];
                            Storys.Add(new Tuple<string , int>(columna1.VigaMayor.Seccions[j].Item2 , i));
                            if ( columna1.VigaMayor.Seccions[j].Item1.H != 0 )
                            {
                                VigaFinal = FunctionsProject.DeepClone(columna1.VigaMayor.Seccions[j].Item1);
                                break;
                            }
                        }

                        for ( int m = 0 ; m < Storys.Count ; m++ )
                        {
                            int Indice = columna1.Seccions.FindIndex(x => x.Item2 == Storys[m].Item1);
                            columna1.LuzLibre.RemoveAt(Indice);
                            columna1.Seccions.RemoveAll(x => x.Item2 == Storys[m].Item1);
                            columna1.VigaMayor.Seccions.RemoveAt(Indice);
                            columna1.PisosDesechos.Add(new Tuple<string , int>(Storys[m].Item1 , i - Storys.Count));
                        }

                        columna1.LuzLibre[i - Storys.Count] += LuzLibre1;
                        columna1.VigaMayor.Seccions[i - Storys.Count] = new Tuple<CRectangulo , string>(VigaFinal , columna1.VigaMayor.Seccions[i].Item2);
                    }
                }
            }
        }

        private void PlantaDeColumnasToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( m_PlantaColumnas.Created == false )
            {
                m_PlantaColumnas = new PlantaColumnas();
            }

            m_PlantaColumnas.Show(PanelContenedor);
        }

        private void InfromaciónDeColumnasToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( m_Informacion.Created == false )
            {
                m_Informacion = new Informacion();
            }

            m_Informacion.Show(PanelContenedor);
        }

        private void Form1_FormClosing ( object sender , FormClosingEventArgs e )
        {
            if ( Proyecto_ != null )
            {
                if ( Proyecto_.Ruta == "" | Proyecto_.Ruta != "" )
                {
                    DialogResult box;
                    box = MessageBox.Show("¿Desea guardar cambios en el Proyecto: " + Proyecto_.Name + "?" , Proyecto_.Empresa , MessageBoxButtons.YesNoCancel , MessageBoxIcon.Warning);

                    if ( box == DialogResult.Yes )
                    {
                        Save();
                    }
                    else if ( box == DialogResult.No )
                    {
                        Application.Exit();
                    }
                    else if ( box == DialogResult.Cancel )
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void VariablesDeEntradaToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( Proyecto_ != null )
            {
                variablesdeEntrada = new VariablesdeEntrada(false);
                if ( Proyecto_.DMO_DES == GDE.DMO )
                {
                    variablesdeEntrada.Radio_Dmo.Checked = true;
                }
                else if ( Form1.Proyecto_.DMO_DES == GDE.DES )
                {
                    variablesdeEntrada.Radio_Des.Checked = true;
                }
                variablesdeEntrada.T_Vf.Text = Proyecto_.e_Fundacion.ToString();
                variablesdeEntrada.T_arranque.Text = Proyecto_.Nivel_Fundacion.ToString();
                variablesdeEntrada.Fy_Box.Text = Proyecto_.FY.ToString();
                variablesdeEntrada.P_R.Text = Proyecto_.P_R.ToString();
                variablesdeEntrada.e_acabados.Text = Proyecto_.e_acabados.ToString();
                variablesdeEntrada.RedondearDecimales.Checked = Proyecto_.Redondear;
                variablesdeEntrada.SE_F.Text = Proyecto_.SE_F.ToString();
                variablesdeEntrada.PictureBox1.Visible = true;
                variablesdeEntrada.ShowDialog();
            }
        }

        private void LColumna_SelectedIndexChanged ( object sender , EventArgs e )
        {
            if ( LColumna.Text != "" )
            {
                TColumna = LColumna.Text;
                Proyecto_.ColumnaSelect = Proyecto_.Lista_Columnas.Find(x => x.Name == LColumna.Text);
                m_Informacion.Invalidate();
                m_PlantaColumnas.Invalidate();
                m_PlantaColumnas.Grafica.Invalidate();
                m_Despiece.Invalidate();
                mCuantiaVolumetrica.Invalidate();
                mAgregarAlzado.Invalidate();

                if ( mIntefazSeccion != null )
                {
                    mIntefazSeccion.edicion = Tipo_Edicion.Secciones_modelo;
                    mIntefazSeccion.Get_Columna();
                    mIntefazSeccion.Load_Pisos();
                    mIntefazSeccion.Get_section(true);
                    mIntefazSeccion.Invalidate();
                    mFuerzasEnElmentos.Invalidate();
                }
            }
        }

        private void ToolStripButton2_Click ( object sender , EventArgs e )
        {
            OpenProject();
        }

        private void Button8_Click ( object sender , EventArgs e )
        {
            Save();
        }

        private void Button7_Click ( object sender , EventArgs e )
        {
            SaveAs();
        }

        private void ToolStripButton1_Click ( object sender , EventArgs e )
        {
            NewProject();
        }

        private void dibujoSecciónToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( mIntefazSeccion != null )
            {
                if ( mIntefazSeccion.Created == false && Proyecto_.ColumnaSelect != null )
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

        private void Form1_Paint ( object sender , PaintEventArgs e )
        {
        }

        private void Cb_cuantiavol_Click ( object sender , EventArgs e )
        {
            if ( Proyecto_.ColumnaSelect != null )
            {
                float FD1, FD2;

                if ( Proyecto_.DMO_DES == GDE.DMO )
                {
                    FD1 = 0.20f;
                    FD2 = 0.06f;
                }
                else
                {
                    FD1 = 0.30f;
                    FD2 = 0.09f;
                }

                for ( int i = 0 ; i < Proyecto_.ColumnaSelect.Seccions.Count ; i++ )
                {
                    Proyecto_.ColumnaSelect.Seccions[i].Item1.Cuanti_Vol(FD1 , FD2 , Proyecto_.R / 100 , Proyecto_.FY);
                }

                mCuantiaVolumetrica.Invalidate();
            }
        }

        private void Form1_Activated ( object sender , EventArgs e )
        {
        }

        private void PanelContenedor_ActivePaneChanged ( object sender , EventArgs e )
        {
            if ( mCuantiaVolumetrica != null )
            {
                bool ExistFlotante = false;
                foreach ( FloatWindow floatWindow in PanelContenedor.FloatWindows )
                {
                    if ( floatWindow.Text == mCuantiaVolumetrica.Text )
                    {
                        ExistFlotante = true;
                    }
                }

                foreach ( DockContent Panels in PanelContenedor.Contents )
                {
                    if ( Panels.Text == mCuantiaVolumetrica.Text )
                    {
                        ExistFlotante = true;
                    }
                }

                if ( PanelContenedor.ActiveDocument == mCuantiaVolumetrica | ExistFlotante )
                {
                    Cuantia_Vol_Button.Enabled = true;
                }
                else
                {
                    Cuantia_Vol_Button.Enabled = false;
                }
            }

            if ( mAgregarAlzado != null )
            {
                bool ExistFlotante = false;
                foreach ( FloatWindow floatWindow in PanelContenedor.FloatWindows )
                {
                    if ( floatWindow.Text == mAgregarAlzado.Text )
                    {
                        ExistFlotante = true;
                    }
                }

                foreach ( DockContent Panels in PanelContenedor.Contents )
                {
                    if ( Panels.Text == mAgregarAlzado.Text )
                    {
                        ExistFlotante = true;
                    }
                }

                if ( ExistFlotante )
                {
                    Button_Agregar.Enabled = true;
                }
                else
                {
                    Button_Agregar.Enabled = false;
                }
            }
            if ( Proyecto_ != null )
            {
                Disenar.Enabled = true;
                B_AutoCAD.Enabled = true;
                infromaciónDeColumnasToolStripMenuItem.Enabled = true;
                plantaDeColumnasToolStripMenuItem.Enabled = true;
                agregarAlzadoToolStripMenuItem.Enabled = true;
                dibujoDeSecciónToolStripMenuItem.Enabled = true;
                cuantíaVolumétricaToolStripMenuItem.Enabled = true;
                variablesDeEntradaToolStripMenuItem.Enabled = true;
                columnasIgualesToolStripMenuItem.Enabled = true;
                fuerzasToolStripMenuItem.Enabled = true;
                despieceToolStripMenuItem.Enabled = true;
                resultadosToolStripMenuItem.Enabled = true;
                editarNombresDeColumnasToolStripMenuItem.Enabled = true;
                cantidadesDeObraToolStripMenuItem.Enabled = true;
                Button_DLNET.Enabled = true;
            }
            if ( WindowState == FormWindowState.Normal )
            {
                Button_MaxRest.Image = Properties.Resources.Maximizar14X11;
            }
            else if ( WindowState == FormWindowState.Maximized )
            {
                Button_MaxRest.Image = Properties.Resources.Restaurar14x11;
            }
        }

        private void AgregarAlzadoToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( mAgregarAlzado.Created == false )
            {
                mAgregarAlzado = new AgregarAlzado();
            }

            mAgregarAlzado.Show(PanelContenedor);
        }

        private void Button_Agregar_Click ( object sender , EventArgs e )
        {
            AgregarAlzado();
        }

        private void AgregarAlzado ()
        {
            if ( Proyecto_.ColumnaSelect != null )
            {
                int CantidadPisos = 0;
                for ( int i = 0 ; i < Proyecto_.ColumnaSelect.Seccions.Count ; i++ )
                {
                    if ( Proyecto_.ColumnaSelect.Seccions[i].Item1 != null )
                    {
                        CantidadPisos += 1;
                    }
                }

                int MaximoID = -99999;
                int CantidaAnteriorAlzados;
                for ( int i = 0 ; i < Proyecto_.ColumnaSelect.Alzados.Count ; i++ )
                {
                    if ( Proyecto_.ColumnaSelect.Alzados[i].ID > MaximoID )
                    {
                        MaximoID = Proyecto_.ColumnaSelect.Alzados[i].ID;
                    }
                }
                if ( MaximoID == -99999 ) { MaximoID = 1; } else { MaximoID += 1; }
                CantidaAnteriorAlzados = Proyecto_.ColumnaSelect.Alzados.Count;
                Alzado alzadoN = new Alzado(MaximoID , CantidadPisos);
                Proyecto_.ColumnaSelect.Alzados.Add(alzadoN);
                Proyecto_.ColumnaSelect.CrearListaPesosRefuerzos(CantidaAnteriorAlzados);
                mAgregarAlzado.CrearDataGrid(true);
                m_Despiece.Invalidate();
            }
        }

        private void EliminarAlzado ()
        {
            if ( Proyecto_.ColumnaSelect != null )
            {
                if ( Proyecto_.ColumnaSelect.Alzados.Count > 1 )
                {
                    Proyecto_.ColumnaSelect.Alzados.RemoveAt(Proyecto_.ColumnaSelect.Alzados.Count - 1);
                    Proyecto_.ColumnaSelect.KgRefuerzoforColumAlzado.RemoveAt(Proyecto_.ColumnaSelect.Alzados.Count - 1);

                    Proyecto_.ColumnaSelect.ActualizarRefuerzo();
                    Proyecto_.ColumnaSelect.CalcularPesoAcero();
                    try
                    {
                        mAgregarAlzado.CrearDataGrid(true);
                        m_Despiece.Draw_Column.Invalidate();
                        m_Despiece.Draw_Colum_Alzado.Invalidate();
                        m_Informacion.ColumnaSelectAnt = null;
                        m_Informacion.CrearDataGriedView(true);
                        m_Informacion.Invalidate();
                    }
                    catch { }
                }
            }
        }

        private void Form1_Load ( object sender , EventArgs e )
        {
            string FicheroExterno = Environment.CommandLine;
            if ( FicheroExterno.Contains(".Colum") )
            {
                OpenProject(true , FicheroExterno.Split(new char[] { '"' })[3]);
            }
            Text = NameProgram;

            mIntefazSeccion = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
            Main_Secciones.Crear_archivo();
        }

        private void ColumnasIgualesToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            ColSimilares colSimilares = new ColSimilares();
            colSimilares.ShowDialog();
        }

        private void Diseñar ( ref List<Columna> Lista_ColumnasDiseñar )
        {
            //Crear Alzado Base

            foreach ( Columna Col in Lista_ColumnasDiseñar )
            {
                Col.AlzadoBaseSugerido = new List<string[]>();
            }

            foreach ( Columna Col in Lista_ColumnasDiseñar )
            {
                for ( int i = 0 ; i < Col.LuzLibre.Count ; i++ )
                {
                    Col.AlzadoBaseSugerido.Add(new string[] { });
                }
            }

            //Determinar Cantidad de Barras Por Sección Predefinidas

            List<ISeccion> Temp = new List<ISeccion>();
            ISeccion Temp_seccion = null;
            ISeccion Temp_seccion2 = null;
            float FD1 = 0; float FD2 = 0;
            string piso = "";

            if ( Proyecto_.DMO_DES == GDE.DMO )
            {
                FD1 = 0.20f;
                FD2 = 0.06f;
                Temp = secciones_predef.Secciones_DMO;
            }
            else
            {
                FD1 = 0.30f;
                FD2 = 0.09f;
                Temp = secciones_predef.Secciones_DES;
            }

            foreach ( Columna Col in Lista_ColumnasDiseñar )
            {
                //Seleccionar Diferentes secciones

                var Secciones_col = Col.Seccions.Select(x => x.Item1).Distinct().ToList();
                List<ISeccion> Secciones_def = new List<ISeccion>();

                Secciones_tipicas(Secciones_col , Secciones_def);
                int contador = 1;

                for ( int i = Col.Seccions.Count - 1 ; i >= 0 ; i-- )
                {
                    //Asignar seccion predefinida a las columnas
                    piso = Col.Seccions[i].Item2;
                    Temp_seccion = FunctionsProject.DeepClone(Secciones_def.Find(x => x.Area == Col.Seccions[i].Item1.Area));

                    if ( Temp.Exists(x => x.Equals(Temp_seccion)) == true )
                    {
                        Temp_seccion2 = FunctionsProject.DeepClone(Temp.Find(x => x.Equals(Temp_seccion)));
                        Temp_seccion2.B = Temp_seccion.B;
                        Temp_seccion2.H = Temp_seccion.H;
                        Temp_seccion2.Material = Col.Seccions[i].Item1.Material;

                        if ( Temp_seccion2.Refuerzos.Count > 0 & Temp_seccion2.B > Temp_seccion2.H )
                        {
                            double[] Rotacion;
                            Temp_seccion2.Estribo.NoRamasH1 = FunctionsProject.DeepClone(Temp.Find(x => x.Equals(Temp_seccion)).Estribo.NoRamasV1);
                            Temp_seccion2.Estribo.NoRamasV1 = FunctionsProject.DeepClone(Temp.Find(x => x.Equals(Temp_seccion)).Estribo.NoRamasH1);

                            foreach ( CRefuerzo refuerzo in Temp_seccion2.Refuerzos )
                            {
                                Rotacion = Operaciones.Rotacion(refuerzo.Coord[0] , refuerzo.Coord[1] , 3 * Math.PI / 2).ToArray();
                                refuerzo.Coord[0] = Rotacion[0];
                                refuerzo.Coord[1] = Rotacion[1];
                            }
                        }

                        Col.Seccions[i] = new Tuple<ISeccion , string>(Temp_seccion2 , piso);
                        Col.Seccions[i].Item1.Calc_vol_inex(Proyecto_.R / 100 , 4220 , Proyecto_.DMO_DES);
                        Col.Seccions[i].Item1.Cuanti_Vol(FD1 , FD2 , Proyecto_.R / 100 , 4220);
                    }
                    else
                    {
                        Temp_seccion.Calc_vol_inex(Proyecto_.R / 100 , 4220 , Proyecto_.DMO_DES);
                        Temp_seccion.Refuerzo_Base(Proyecto_.R);
                        Temp_seccion.Material = Col.Seccions[i].Item1.Material;
                        Temp_seccion.Calc_vol_inex(Proyecto_.R / 100 , 4220 , Proyecto_.DMO_DES);
                        Temp_seccion.Cuanti_Vol(FD1 , FD2 , Proyecto_.R / 100 , 4220);
                        Col.Seccions[i] = new Tuple<ISeccion , string>(Temp_seccion , piso);
                    }

                    string[] Base = new string[0];
                    Col.Seccions[i].Item1.CalcNoDBarras();
                    if ( Col.Seccions[i].Item1.No_D_Barra.Count == 2 )
                    {
                        Base = new string[4];
                        Base[0] = $"{ Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[0].Item1 / 2)}#{Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[0].Item2)}";
                        Base[1] = $"{ Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[0].Item1 / 2)}#{Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[0].Item2)}";
                        Base[2] = $"{ Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[1].Item1 / 2)}#{Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[1].Item2)}";
                        Base[3] = $"{ Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[1].Item1 / 2)}#{Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[1].Item2)}";
                    }

                    if ( Col.Seccions[i].Item1.No_D_Barra.Count == 1 )
                    {
                        Base = new string[2];
                        Base[0] = $"{ Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[0].Item1 / 2)}#{Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[0].Item2)}";
                        Base[1] = $"{ Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[0].Item1 / 2)}#{Convert.ToString(Col.Seccions[i].Item1.No_D_Barra[0].Item2)}";
                    }

                    //Asignar #Alzado

                    if ( i < Col.Seccions.Count - 1 )
                    {
                        if ( Col.Seccions[i].Item1.Area != Col.Seccions[i + 1].Item1.Area )
                        {
                            if ( Col.AlzadoBaseSugerido[i + 1].Count() == 2 )
                            {
                                contador += 2;
                            }
                            else
                            {
                                contador += 4;
                            }
                            Asignar_Alzado(Col , i , Base , contador);
                        }
                        else
                        {
                            Asignar_Alzado(Col , i , Base , contador);
                        }
                    }
                    else
                    {
                        Asignar_Alzado(Col , i , Base , contador);
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
            if ( Lista_ColumnasDiseñar.Count > 4 )
            {
                Delta = ( Cuadro_diseño.BarraPersonalizada.Width ) / ( Lista_ColumnasDiseñar.Count - 4 );
            }
            else
            {
                Delta = ( Cuadro_diseño.BarraPersonalizada.Width ) / ( Lista_ColumnasDiseñar.Count );
            }

            int CantCol = 1;
            double D_Pro = Math.Ceiling(Delta);
            bool HabilitarReporte = false;

            foreach ( Columna col in Lista_ColumnasDiseñar )
            {
                col.CantEstribos_Sepa = new List<object[]>();
                for ( int i = col.LuzAcum.Count - 1 ; i >= 0 ; i-- )
                {
                    col.CantEstribos_Sepa.Add(new object[] { 0 , 0 , 0 , 0 , 0 , 0 });
                }
            }

            foreach ( Columna col in Lista_ColumnasDiseñar )
            {
                col.AgregarAlzadoSugerido();

                bool Ref_base = true;
                var Temp_base = new List<string>();
                foreach ( var alzado in col.Alzados )
                {
                    foreach ( var alzado_piso in alzado.Colum_Alzado )
                    {
                        if ( alzado_piso == null )
                        {
                            Ref_base = false;
                            break;
                        }
                    }

                    if ( Ref_base == true )
                    {
                        Temp_base.Add(alzado.ID.ToString());
                    }
                }

                //col.Alzados[0].ID;  --- ID QUE SE DEBE AGREGAR
                if ( col.Alzados.Count != 0 )
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

            foreach ( Columna col in Lista_ColumnasDiseñar )
            {
                for ( int i = col.LuzAcum.Count - 1 ; i >= 0 ; i-- )
                {
                    col.CantidadEstribos(i);
                }
                if ( col.Alzados.Count == 0 )
                {
                    Cuadro_diseño.Reporte_RichText.Text += "\n" + " - " + col.Name;
                }
            }
            m_Informacion.MostrarAcero();
            if ( m_Informacion != null )
            {
                m_Informacion.Invalidate();
            }
            if ( mAgregarAlzado != null )
            {
                mAgregarAlzado.CrearDataGrid(true);
            }

            DialogResult dialogResult;
            dialogResult = MessageBox.Show("¿El alzado generado será el definitivo?" , Proyecto_.Empresa , MessageBoxButtons.YesNo , MessageBoxIcon.Information);

            if ( dialogResult == DialogResult.Yes )
            {
                Lista_ColumnasDiseñar.ForEach(x => x.Ready = true);
            }
            if ( m_Despiece != null )
            {
                m_Despiece.Invalidate();
            }
            if ( m_PlantaColumnas != null )
            {
                m_PlantaColumnas.Invalidate();
            }

            //"Mostrar Reporte"
            List<string> Reporte = new List<string>();
            Reporte.Add("-----REPORTE DE DISEÑO -------"); Reporte.Add("");

            Reporte.Add("Columna\tPiso\tObservación");

            foreach ( Columna col in Lista_ColumnasDiseñar )
            {
                for ( int i = 0 ; i < col.resultadosETABs.Count ; i++ )
                {
                    string LineReporte = $"{col.Name}\t{col.Seccions[i].Item2}\t";

                    if ( col.resultadosETABs[i].Porct_Refuerzo[0] > 115f )
                    {
                        LineReporte += "|Top - > 115%|";
                    }
                    else if ( col.resultadosETABs[i].Porct_Refuerzo[0] < 95f )
                    {
                        LineReporte += "|Top - < 95%|";
                    }
                    else
                    {
                        LineReporte += "|Top - ✓OK|";
                    }

                    if ( col.resultadosETABs[i].Porct_Refuerzo[1] > 105f )
                    {
                        LineReporte += " |Medium - > 105%|";
                    }
                    else if ( col.resultadosETABs[i].Porct_Refuerzo[1] < 95f )
                    {
                        LineReporte += " |Medium - < 95%|";
                    }
                    else
                    {
                        LineReporte += " |Medium - ✓OK|";
                    }

                    if ( col.resultadosETABs[i].Porct_Refuerzo[2] > 115f )
                    {
                        LineReporte += "  |Bottom - > 115%|";
                    }
                    else if ( col.resultadosETABs[i].Porct_Refuerzo[2] < 95f )
                    {
                        LineReporte += "  |Bottom - < 95%|";
                    }
                    else
                    {
                        LineReporte += " |Bottom - ✓OK|";
                    }

                    Reporte.Add(LineReporte);
                }
            }

            Reporte.Add(""); Reporte.Add("---------------------------------------"); Reporte.Add("Diseño de Columnas"); Reporte.Add("Versión 1.0");
            Reporte.Add("© 2019 efe- Prima – Ce"); Reporte.Add("Todos los Derechos Reservados.");
            CrearCarpetaOCargar();
            string Ruta_ArchivoTemporal = Path.Combine(RutaConfig , "Reporte.txt");
            StreamWriter writer = new StreamWriter(Ruta_ArchivoTemporal);
            for ( int i = 0 ; i < Reporte.Count ; i++ )
            {
                writer.WriteLine(Reporte[i]);
            }
            writer.Close();
            Process Proc = new Process();
            Proc.StartInfo.FileName = Ruta_ArchivoTemporal;
            Proc.Start();
        }

        /// <summary>
        /// Asigna el numero de alzado a cada uno de los refuerzos bases de la seccion
        /// </summary>
        /// <param name="Col">Columna</param>
        /// <param name="i">Numero de piso</param>
        /// <param name="Base">Refuerzo base</param>
        private static void Asignar_Alzado ( Columna Col , int i , string[] Base , int Contador )
        {
            int m = 0;
            int pos = 0;

            foreach ( CRefuerzo refuerzo in Col.Seccions[i].Item1.Refuerzos )
            {
                if ( Base.Count() == 2 )
                {
                    if ( m % 2 == 0 )
                    {
                        refuerzo.Alzado = Contador;
                    }
                    else
                    {
                        refuerzo.Alzado = Contador + 1;
                    }
                }
                else
                {
                    if ( m % 2 == 0 )
                    {
                        pos = Base[0].IndexOf('#');
                        if ( refuerzo.Diametro == Base[0].Substring(pos) )
                        {
                            refuerzo.Alzado = Contador;
                        }

                        pos = Base[2].IndexOf('#');
                        if ( refuerzo.Diametro == Base[2].Substring(pos) )
                        {
                            refuerzo.Alzado = Contador + 2;
                        }
                    }
                    else
                    {
                        pos = Base[1].IndexOf('#');
                        if ( refuerzo.Diametro == Base[1].Substring(pos) )
                        {
                            refuerzo.Alzado = Contador + 1;
                        }

                        pos = Base[3].IndexOf('#');
                        if ( refuerzo.Diametro == Base[3].Substring(pos) )
                        {
                            refuerzo.Alzado = Contador + 3;
                        }
                    }
                }
                m++;
            }
        }

        /// <summary>
        /// Determina las secciones tipicas del alzado actual
        /// </summary>
        /// <param name="Secciones_col">Listado completo de secciones del alzado</param>
        /// <param name="Secciones_def">Secciones tipicas definitavas</param>
        private static void Secciones_tipicas ( List<ISeccion> Secciones_col , List<ISeccion> Secciones_def )
        {
            var Agrupacion_areas = from p in Secciones_col
                                   group p by p.Area into g
                                   select new { seccionI = g.Select(x1 => x1) };

            foreach ( var aux in Agrupacion_areas )
            {
                var Fc_max = aux.seccionI.ToList().Select(x => x.Material.FC).Max();
                Secciones_def.Add(aux.seccionI.ToList().Find(x => x.Material.FC == Fc_max));
            }
        }

        private void Button1_Click ( object sender , EventArgs e )
        {
            ActionDiseñar();
        }

        private void ActionDiseñar ()
        {
            ColumnasaDiseñar SelecColumnsDisenar = new ColumnasaDiseñar();
            List<Columna> ColumnasADiseñar = new List<Columna>();

            SelecColumnsDisenar.ShowDialog();

            foreach ( Columna col in Proyecto_.Lista_Columnas )
            {
                if ( col.Disenar )
                {
                    ColumnasADiseñar.Add(col);
                }
            }

            if ( ColumnasADiseñar.Count != 0 & CancelDiseño == false )
            {
                Diseñar(ref ColumnasADiseñar);
            }
        }

        private void EditarSeccionesPredeterminadasToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            CUsuario usuario = new CUsuario();
            usuario.Get_user();

            if ( usuario.Permiso == true )
            {
                if ( mIntefazSeccion != null )
                {
                    if ( mIntefazSeccion.Created == false )
                    {
                        pEdicion = Tipo_Edicion.Secciones_predef;
                        mIntefazSeccion = new FInterfaz_Seccion(pEdicion);
                    }

                    if ( mIntefazSeccion.Created == true & pEdicion == Tipo_Edicion.Secciones_modelo )
                    {
                        pEdicion = Tipo_Edicion.Secciones_predef;
                        mIntefazSeccion.Close();
                        mIntefazSeccion = new FInterfaz_Seccion(pEdicion);
                    }
                }
                else
                {
                    pEdicion = Tipo_Edicion.Secciones_predef;
                    mIntefazSeccion = new FInterfaz_Seccion(pEdicion);
                }

                mIntefazSeccion.Show(PanelContenedor);
                mIntefazSeccion.Get_Predef_Secction();
                mIntefazSeccion.Invalidate();
            }
        }

        private IDockContent GetContentFromPersistString ( string persistString )
        {
            if ( persistString == typeof(Informacion).ToString() )
                return m_Informacion;
            else if ( persistString == typeof(AgregarAlzado).ToString() )
                return mAgregarAlzado;
            else if ( persistString == typeof(PlantaColumnas).ToString() )
                return m_PlantaColumnas;
            else if ( persistString == typeof(Despiece).ToString() )
                return m_Despiece;
            else if ( persistString == typeof(CuantiaVolumetrica).ToString() )
                return mCuantiaVolumetrica;
            else
            {
                // DummyDoc overrides GetPersistString to add extra information into persistString.
                // Any DockContent may override this value to add any needed information for deserialization.

                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if ( parsedStrings.Length != 3 )
                    return null;

                if ( parsedStrings[0] != typeof(Informacion).ToString() )
                    return null;

                Informacion dummyDoc = new Informacion();
                if ( parsedStrings[2] != string.Empty )
                    dummyDoc.Text = parsedStrings[2];

                return dummyDoc;
            }
        }

        private void CuantíaVolumétricaToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( mCuantiaVolumetrica.Created == false )
            {
                mCuantiaVolumetrica = new CuantiaVolumetrica();
            }
            mCuantiaVolumetrica.Show(PanelContenedor);
        }

        private void DibujoDeSecciónToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( mIntefazSeccion != null )
            {
                if ( mIntefazSeccion.Created == false && Proyecto_.ColumnaSelect != null )
                {
                    pEdicion = Tipo_Edicion.Secciones_modelo;
                    mIntefazSeccion = new FInterfaz_Seccion(pEdicion);
                }
                else if ( mIntefazSeccion.Created == true && Proyecto_.ColumnaSelect != null && pEdicion == Tipo_Edicion.Secciones_predef )
                {
                    mIntefazSeccion.Close();
                    pEdicion = Tipo_Edicion.Secciones_modelo;
                    mIntefazSeccion = new FInterfaz_Seccion(pEdicion);
                }
            }
            else
            {
                mIntefazSeccion = new FInterfaz_Seccion(pedicion: Tipo_Edicion.Secciones_modelo);
            }

            mIntefazSeccion.Show(PanelContenedor);
            mIntefazSeccion.Invalidate();
        }

        private void FuerzasToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( mFuerzasEnElmentos.Created == false )
            {
                mFuerzasEnElmentos = new FuerzasEnElementos();
            }
            mFuerzasEnElmentos.Show(PanelContenedor);
        }

        private void Button4_Click ( object sender , EventArgs e )
        {
            ColumnasaGraficar columnasaGraficar = new ColumnasaGraficar();
            List<Columna> columnasDrawing = new List<Columna>();

            columnasaGraficar.ShowDialog();

            foreach ( Columna col in Proyecto_.Lista_Columnas )
            {
                col.NamesSimilares.Clear();
                if ( col.aGraficar & col.Ready )
                {
                    columnasDrawing.Add(col);
                }
            }

            foreach ( Columna col in Proyecto_.Lista_Columnas )
            {
                if ( col.ColSimilName != null & col.ColSimilName != "" )
                {
                    if ( columnasDrawing.Exists(x => x.Name == col.Name) )
                    {
                        columnasDrawing.Remove(col);
                    }

                    Proyecto_.Lista_Columnas.Find(x => x.Name == col.ColSimilName).NamesSimilares.Add(col.Label);
                }
            }

            bool Activar_CuantiMax = false;
            List<string> NamesColumsMax = new List<string>();
            if ( CancelGarfica == false && columnasDrawing.Count != 0 )
            {
                foreach ( Columna col in columnasDrawing )
                {
                    for ( int i = col.resultadosETABs.Count - 1 ; i >= 0 ; i-- )
                    {
                        if ( col.resultadosETABs[i].AsTopMediumButton[0] / ( col.Seccions[i].Item1.Area ) > 0.04 )
                        {
                            Activar_CuantiMax = true;
                            NamesColumsMax.Add(col.Name);
                        }
                        else if ( col.resultadosETABs[i].AsTopMediumButton[1] / ( col.Seccions[i].Item1.Area ) > 0.04 )
                        {
                            Activar_CuantiMax = true;
                            NamesColumsMax.Add(col.Name);
                        }
                        else if ( col.resultadosETABs[i].AsTopMediumButton[2] / ( col.Seccions[i].Item1.Area ) > 0.04 )
                        {
                            Activar_CuantiMax = true;
                            NamesColumsMax.Add(col.Name);
                        }
                    }
                }

                List<string> NamesColumMax = NamesColumsMax.Distinct().ToList();
                string ColumnasMaximas = "";
                foreach ( string Name in NamesColumMax )
                {
                    ColumnasMaximas += Name + ",";
                }

                string MensajeColumnasMaxMaxima = "La(s) columna(s): " + ColumnasMaximas + "  en el acero requerido supera la cuantía máxima permisible.";
                DialogResult messageBox;
                messageBox = DialogResult.Yes;
                if ( Activar_CuantiMax )
                {
                    MessageBox.Show(MensajeColumnasMaxMaxima , Proyecto_.Empresa , MessageBoxButtons.OK , MessageBoxIcon.Warning);

                    messageBox = MessageBox.Show("¿Desea continuar? " , Proyecto_.Empresa , MessageBoxButtons.YesNo , MessageBoxIcon.Question);
                }

                if ( messageBox == DialogResult.Yes )
                {
                    GraficarAlzadoColumnas(ref columnasDrawing);
                }
            }
        }

        private void GraficarAlzadoColumnas ( ref List<Columna> ColumnsDrawing )
        {
            double[] XY = new double[] { };
            FunctionsAutoCAD.FunctionsAutoCAD.OpenAutoCAD();
            FunctionsAutoCAD.FunctionsAutoCAD.SetScale("1:75");

            MessageBox.Show("Posicione el puntero en AutoCAD." , Proyecto_.Empresa , MessageBoxButtons.OK , MessageBoxIcon.Information);

            FunctionsAutoCAD.FunctionsAutoCAD.GetPoint(ref XY);
            double DeltaX = 0;
            int NoDes = 1;

            foreach ( Columna col in ColumnsDrawing )
            {
                if ( col.Alzados.Count != 0 )
                {
                    string Names = col.Label;
                    for ( int i = 0 ; i < col.NamesSimilares.Count ; i++ )
                    {
                        Names += "," + col.NamesSimilares[i];
                    }
                    FunctionsAutoCAD.FunctionsAutoCAD.SetScale("1:75");
                    col.DrawColumAutoCAD(XY[0] + DeltaX , XY[1] , Names , NoDes);
                    FunctionsAutoCAD.FunctionsAutoCAD.SetScale("1:15");
                    col.Seccions.Last().Item1.Dibujo_Autocad(XY[0] + DeltaX , XY[1] - 1.60 , NoDes);
                    DeltaX += 10 + col.Alzados[col.Alzados.Count - 1].DistX;
                    NoDes += 1;
                }
            }

            MessageBox.Show("Alzado graficado con éxito." , Proyecto_.Empresa , MessageBoxButtons.OK , MessageBoxIcon.Information);
        }

        private void AcercaDeToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            Interfaz_Inicial.Derechos_de_Autor.Autores autores = new Interfaz_Inicial.Derechos_de_Autor.Autores();

            autores.ShowDialog();
        }

        private void DespieceToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( m_Despiece.Created == false )
            {
                m_Despiece = new Despiece();
            }
            m_Despiece.Show(PanelContenedor);
        }

        private void PanelContenedor_ActiveContentChanged ( object sender , EventArgs e )
        {
        }

        private void ResultadosToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            Resultados.Resultados_ resultados_ = new Resultados.Resultados_();
            resultados_.ShowDialog();
        }

        private void EditarNombresDeColumnasToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            CambiarNombreColumna cambiarNombreColumnas = new CambiarNombreColumna();
            cambiarNombreColumnas.ShowDialog();
        }

        private void CantidadesDeObraToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            GenerarCantidades();
        }

        private void Button1_Click_1 ( object sender , EventArgs e )
        {
            GenerarCantidades();
        }

        private void GenerarCantidades ()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Title = "Exportar Archivo Texto Formato DL NET" , Filter = "Archivo|*.txt" };
            saveFileDialog.ShowDialog();

            string RutaArchivo = saveFileDialog.FileName;

            if ( RutaArchivo != "" )
            {
                List<string> ArchivoaGenerar = new List<string>();

                int CantidadElementos = 0;
                foreach ( Columna col in Proyecto_.Lista_Columnas )
                {
                    if ( col.Label == "" | col.Label == null )
                    {
                        MessageBox.Show($"Asigne el Label de la Columna {col.Name} antes de generar el archivo .txt" , Proyecto_.Empresa , MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                ParaCantidades.Visible = true; BarraProgresoCantidades.Visible = true; int contador = 1;

                BarraProgresoCantidades.Maximum = Proyecto_.Lista_Columnas.Count; BarraProgresoCantidades.Value = 0;
                foreach ( Columna col in Proyecto_.Lista_Columnas )
                {
                    col.CalcularCantidadesDLNET();
                    BarraProgresoCantidades.Value += 1; contador += 1;
                    ParaCantidades.Text = contador + "/" + Proyecto_.Lista_Columnas.Count;
                    Application.DoEvents();
                }

                BarraProgresoCantidades.Visible = false; ParaCantidades.Visible = false;
                foreach ( Columna col in Proyecto_.Lista_Columnas )
                {
                    int CantidadRefuerzo = col.Lista_RefuerzoLongitudinal_DLNET.Count + col.Lista_RefuerzoTransversal_DLNET.Count;
                    if ( CantidadRefuerzo != 0 )
                    {
                        ArchivoaGenerar.Add(col.Label);
                        ArchivoaGenerar.Add(Convert.ToString(1));
                        ArchivoaGenerar.Add(Convert.ToString(CantidadRefuerzo));
                        ArchivoaGenerar.AddRange(col.Lista_RefuerzoLongitudinal_DLNET);
                        ArchivoaGenerar.AddRange(col.Lista_RefuerzoTransversal_DLNET);
                        CantidadElementos += 1;
                    }
                }

                StreamWriter writer;
                try
                {
                    writer = new StreamWriter(RutaArchivo);
                }
                catch ( Exception ex )
                {
                    MessageBox.Show(ex.Message , Proyecto_.Empresa , MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                    return;
                }

                writer.WriteLine(CantidadElementos);
                for ( int i = 0 ; i < ArchivoaGenerar.Count ; i++ )
                {
                    writer.WriteLine(ArchivoaGenerar[i]);
                }
                writer.Close();
                Process Proc = new Process();
                Proc.StartInfo.FileName = RutaArchivo;
                Proc.Start();
            }
        }

        private void AgregrarAlzadoToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( Proyecto_ != null )
            {
                AgregarAlzado();
            }
        }

        private void DiseñarToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            if ( Proyecto_ != null )
            {
                ActionDiseñar();
            }
        }

        private void EliminarToolStripMenuItem_Click ( object sender , EventArgs e )
        {
            EliminarAlzado();
        }

        private void Form1_KeyDown ( object sender , KeyEventArgs e )
        {
        }
    }
}