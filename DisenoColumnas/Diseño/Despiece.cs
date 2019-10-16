using DisenoColumnas.DefinirColumnas;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DisenoColumnas.Diseño
{
    public partial class Despiece : DockContent
    {
        public Despiece()
        {
            InitializeComponent();
        }

        private void Draw_Column_Paint(object sender, PaintEventArgs e)
        {
            if (PlantaColumnas.ColumnaSelect != null)
            {
                float MaxB = -999999;
                for (int i = 0; i < PlantaColumnas.ColumnaSelect.Seccions.Count; i++)
                {
                    if (PlantaColumnas.ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        if (PlantaColumnas.ColumnaSelect.Seccions[i].Item1.B > MaxB)
                        {
                            MaxB = PlantaColumnas.ColumnaSelect.Seccions[i].Item1.B;
                        }
                    }
                }

                float SX = (Draw_Column.Width - 15) / MaxB;
                float SY = Draw_Column.Height / (Form1.Proyecto_.AlturaEdificio);
                float X = 7.5f;
                float Y = 5;

                e.Graphics.Clear(Color.White);
                Title_Colum_Model.Text = "Columna: " + PlantaColumnas.ColumnaSelect.Name;
                PlantaColumnas.ColumnaSelect.Paint_Alzado1(e, Draw_Column.Height - 10, Draw_Column.Width, SX, SY, X, Y);
            }
        }

        private void Despiece_Paint(object sender, PaintEventArgs e)
        {
            Draw_Column.Invalidate();
        }

        private int StoryMostrar = 0;

        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (PlantaColumnas.ColumnaSelect != null)
            {
                e.Graphics.Clear(Color.White);

                double MaxAs = -999999;
                for (int i = 0; i < PlantaColumnas.ColumnaSelect.resultadosETABs.Count; i++)
                {
                    if (PlantaColumnas.ColumnaSelect.resultadosETABs[i] != null)
                    {
                        for (int j = 0; i < PlantaColumnas.ColumnaSelect.resultadosETABs[i].As.Count; j++)
                        {
                            if (PlantaColumnas.ColumnaSelect.resultadosETABs[i].As[j] > MaxAs)
                            {
                                MaxAs = PlantaColumnas.ColumnaSelect.resultadosETABs[i].As[j];
                            }
                        }
                    }
                }

                double SX = (Draw_Column.Width - 15) / MaxAs;
                double SY = Draw_Column.Height / (Form1.Proyecto_.AlturaEdificio);

                //e.Graphics.DrawLine(new Pen)
            }
        }
    }
}