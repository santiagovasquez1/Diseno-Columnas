using DisenoColumnas.Clases;
using DisenoColumnas.DefinirColumnas;
using DisenoColumnas.Diseño;
using DisenoColumnas.Interfaz_Inicial;
using DisenoColumnas.Interfaz_Seccion;
using DisenoColumnas.InterfazViewInfo;
using DisenoColumnas.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DisenoColumnas
{
    public partial class Form1 : Form
    {


        private PlantaColumnas m_PlantaColumnas;
        public static Informacion m_Informacion;
        public static Despiece m_Despiece;
        public static VariablesdeEntrada variablesdeEntrada;
        public static ComboBox mLcolumnas;

        private List<string> ArchivoE2K2009ETABS;
        private List<string> ArchivoResultados2009;
        public static Proyecto Proyecto_;

        public Form1()
        {
            InitializeComponent();
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
                FunctionsProject.Deserealizar(openFileDialog.FileName, ref Proyecto_);



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

                m_Informacion = new Informacion();
                m_Informacion.Show(PanelContenedor);

                if (m_Despiece != null)
                {
                    m_Despiece.DockHandler.DockPanel = null;
                }


                mLcolumnas = LColumna;
                m_Despiece = new Despiece();
                m_Despiece.Show(PanelContenedor);
                LColumna.Enabled = true;
                La_Column.Enabled = true;
                LColumna.Items.AddRange(Proyecto_.Lista_Columnas.Select(x => x.Name).ToArray());
                if (Proyecto_.ColumnaSelect != null)
                {
                    LColumna.Text = Proyecto_.ColumnaSelect.Name;
                }
                WindowState = FormWindowState.Maximized;
            }
        }

        private void SaveAs()
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Filter = "DMC |*.Colum";
            SaveFile.Title = "Guardar Proyecto";
            SaveFile.ShowDialog();

            if (SaveFile.FileName != "")
            {
                if (Proyecto_ != null)
                {
                    Proyecto_.Ruta = SaveFile.FileName;

                    FunctionsProject.Serializar(Proyecto_.Ruta, Proyecto_);
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
            string Ruta1 = AbrirE2K2009yCSV2009();
            string Ruta2 = AsingarResultadosaColumna();

            if (Ruta1 != "" && Ruta2 != "")
            {
                WindowState = FormWindowState.Maximized;

                variablesdeEntrada = new VariablesdeEntrada();
                variablesdeEntrada.ShowDialog();
                mLcolumnas = LColumna;
                m_PlantaColumnas = new PlantaColumnas();
             //   m_PlantaColumnas.Show(PanelContenedor);
                m_Informacion = new Informacion();
                m_Informacion.Show(PanelContenedor);

                m_Despiece = new Despiece();
                m_Despiece.Show(PanelContenedor);

                LColumna.Enabled = true;
                La_Column.Enabled = true;

                LColumna.Items.AddRange(Proyecto_.Lista_Columnas.Select(x => x.Name).ToArray());

            }
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

                OpenFileDialog openFileDialog2 = new OpenFileDialog();
                openFileDialog2.Filter = "Abrir Archivo con Resultados |.csv";
            }
            return openFileDialog1.FileName;
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
                material.FC = (float)Convert.ToDouble(Lista_Materiales_Aux2[i][13].Replace("\"", ""));
                Proyecto_.Lista_Materiales.Add(material);
            }

            //Secciones

            Proyecto_.Lista_Secciones = new List<Seccion>();

            int Inicio_FrameSecctions = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ FRAME SECTIONS")) + 1;
            int Final_FrameSecctions = ArchivoE2K2009ETABS.FindIndex(x => x.Contains("$ REBAR DEFINITIONS")) - 1;

            List<List<string>> Lista_Secciones_Aux = new List<List<string>>();

            for (int i = Inicio_FrameSecctions; i < Final_FrameSecctions; i++)
            {
                if (ArchivoE2K2009ETABS[i].Contains("Rectangular") | ArchivoE2K2009ETABS[i].Contains("Circle") | ArchivoE2K2009ETABS[i].Contains("Te") | ArchivoE2K2009ETABS[i].Contains("Angle") | ArchivoE2K2009ETABS[i].Contains("SD Section"))
                {
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
                    if(mAT_.Name== Material_Aux) {

                        Seccion seccion = new Seccion(Nombre, B, H,TF,TW, mAT_, tipodeSeccion,Coord);
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
                            Seccion seccion = Proyecto_.Lista_Secciones.Find(x => x.Name == NameSeccion);
                            Tuple<Seccion, string> tuple_aux = new Tuple<Seccion, string>(seccion, Story);
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
                            Seccion seccion = Proyecto_.Lista_Secciones.Find(x => x.Name == NameSeccion);
                            if (seccion.Shape == TipodeSeccion.None)
                            {
                                seccion = null;
                            }
                            Tuple<Seccion, string> tuple_aux = new Tuple<Seccion, string>(seccion, Story);
                            colum.Seccions.Add(tuple_aux);
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
                Seccion seccionMayor = new Seccion("Inicial", 0, -99999, 0, 0, new MAT_CONCRETE(), TipodeSeccion.None);
                Tuple<Seccion, string> tuple_Seccion_Mayor = null;

                for (int i = 0; i < Proyecto_.Stories.Count; i++)
                {
                    tuple_Seccion_Mayor = new Tuple<Seccion, string>(seccionMayor, Proyecto_.Stories[i].Item1);
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
                    columna1.LuzLibre.Add(Proyecto_.Stories[i].Item2 - columna1.VigaMayor.Seccions[i].Item1.H);
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
                    box = MessageBox.Show("¿Desea guardar los cambios en el Proyecto?", Proyecto_.Empresa, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

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
                variablesdeEntrada.ShowDialog();
            }
            else
            {
                variablesdeEntrada = new VariablesdeEntrada();
                variablesdeEntrada.ShowDialog();
            }
        }



        private void LColumna_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LColumna.Text != "")
            {
                Proyecto_.ColumnaSelect = Proyecto_.Lista_Columnas.Find(x => x.Name == LColumna.Text);
                m_Informacion.Invalidate();
                m_PlantaColumnas.Invalidate();
                m_Despiece.Invalidate();
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
            FInterfaz_Seccion interfaz_Seccion = new FInterfaz_Seccion();
            interfaz_Seccion.Show(PanelContenedor);
        }
    }
}
