using DisenoColumnas.Secciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Seccion.Diagrama_de_Interacción
{
    public partial class Solicitaciones : Form
    {
        public static List<float[]> MP_Soli3D = new List<float[]>();
        public static List<Tuple<float[], int>> SolicitacionesConAngulos = new List<Tuple<float[], int>>();
        public static ISeccion Seccion { get; set; }

        public static bool Ultimos { get; set; }

        private List<int> Angulos = new List<int>();

        public Solicitaciones()
        {
            InitializeComponent();

        }

        private void Solicitaciones_Load(object sender, EventArgs e)
        {
            if (MP_Soli3D.Count != 0)
            {
                CalcularAngulos();
            }

            int[] DistinAngulos = Angulos.Distinct().ToArray();
            for (int i = 0; i < DistinAngulos.Length; i++)
            {
                SolicitBox.Items.Add("Angulo: " + DistinAngulos[i] + "°");
            }
        }

        private void CalcularAngulos()
        {
            SolicitacionesConAngulos.Clear();

            for (int i = 0; i < MP_Soli3D.Count; i++)
            {
                int Angulo = (int)(Math.Atan(MP_Soli3D[i][1] / MP_Soli3D[i][2]) * (180 / Math.PI));
                float Msoli = (float)Math.Sqrt(Math.Pow(MP_Soli3D[i][0], 2) + Math.Pow(MP_Soli3D[i][1], 2));

                if (Angulo < 0)
                {
                    Angulo += 360;
                }
                Tuple<float[], int> TupleAux = new Tuple<float[], int>(new float[] { Msoli, MP_Soli3D[i][2] }, Angulo);
                SolicitacionesConAngulos.Add(TupleAux);
                Angulos.Add(Angulo);
            }
        }

        private void CerrarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SolicitBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Close();
            }
        }

        private void SolicitBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IntAngulo = SolicitBox.SelectedIndex;

            int[] DistinAngulos = Angulos.Distinct().ToArray();

            int Angulo = DistinAngulos[IntAngulo];

            List<float[]> MPpuntosSolicitaciones = new List<float[]>();

            for (int i = 0; i < SolicitacionesConAngulos.Count; i++)
            {
                if (Angulo == SolicitacionesConAngulos[i].Item2)
                {
                    MPpuntosSolicitaciones.Add(SolicitacionesConAngulos[i].Item1);
                }
            }

            DiagramaInteraccion.MPpuntosSolicitaciones = MPpuntosSolicitaciones;
            DiagramaInteraccion.MP2D_UnAngulo = Seccion.DiagramaInteraccionParaUnAngulo(Angulo, Ultimos).Item1;
            DiagramaInteraccion.MP3D_UnAngulo = Seccion.DiagramaInteraccionParaUnAngulo(Angulo, Ultimos).Item2;
            DiagramaInteraccion.Diagrama.Title.Text = $"Diagrama de Interacción - {Angulo}°";
            DiagramaInteraccion.Diagrama.GroupBox_Grafica_Diagrama1.Text = "Angulo de " + Angulo + "°";
            DiagramaInteraccion.Diagrama.MostrarValores();
            DiagramaInteraccion.Diagrama.Invalidate();
            DiagramaInteraccion.Diagrama.CharMomentos.Invalidate();


        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Utilidades.MoveWindow.ReleaseCapture();
            Utilidades.MoveWindow.SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void Solicitaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DiagramaInteraccion.MPpuntosSolicitaciones.Clear();
            //DiagramaInteraccion.MP2D_UnAngulo.Clear();
            //DiagramaInteraccion.MP3D_UnAngulo.Clear();
            //DiagramaInteraccion.Diagrama.MostrarValores();
            //DiagramaInteraccion.Diagrama.Invalidate();
            //DiagramaInteraccion.Diagrama.CharMomentos.Invalidate();
        }
    }
}
