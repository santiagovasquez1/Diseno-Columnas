using DisenoColumnas.DefinirColumnas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            RefuerzoRequerido.Invalidate();
        }


        
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (PlantaColumnas.ColumnaSelect != null)
            {

                if(PlantaColumnas.ColumnaSelect.StoryMostrar == -1)
                {
                    PlantaColumnas.ColumnaSelect.StoryMostrar = PlantaColumnas.ColumnaSelect.LuzLibre.Count - 1;

                }
                e.Graphics.Clear(Color.White);

                double MaxAs = -999999;
                for (int i = 0; i < PlantaColumnas.ColumnaSelect.resultadosETABs.Count; i++)
                {

                    if (PlantaColumnas.ColumnaSelect.resultadosETABs[i] != null)
                    {
                        for (int j = 0; j < PlantaColumnas.ColumnaSelect.resultadosETABs[i].As.Count; j++)
                        {
                            if (PlantaColumnas.ColumnaSelect.resultadosETABs[i].As[j] > MaxAs)
                            {
                                MaxAs = PlantaColumnas.ColumnaSelect.resultadosETABs[i].As[j];
                            }
                        }
                    }
                }

                float SX =  (RefuerzoRequerido.Width - 10) / (float)MaxAs;
                int IndiceN = PlantaColumnas.ColumnaSelect.StoryMostrar;
                float AltoEscalar;
                try
                {
                    AltoEscalar = PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN]+ PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN-1];
                }
                catch
                {
                    AltoEscalar = PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN];
                }

                float SY = (RefuerzoRequerido.Height-30) / AltoEscalar;
                float X = 7.5f;
                float Y = 5;

                string MensajeaMostrar;

                try
                {
                    MensajeaMostrar = PlantaColumnas.ColumnaSelect.Seccions[IndiceN].Item2 +" - "+ PlantaColumnas.ColumnaSelect.Seccions[IndiceN-1].Item2;
                }
                catch
                {
                    MensajeaMostrar = PlantaColumnas.ColumnaSelect.Seccions[IndiceN].Item2;
                }
                CambiarPiso.Text = MensajeaMostrar;

                //Lineas Inicales
                float x1, y1, x2, y2;

                x1 = 5;
                x2 = 5;
                float YI = -15;
                y1 = 0;
                //y1 = (float)PlantaColumnas.ColumnaSelect.resultadosETABs[PlantaColumnas.ColumnaSelect.StoryMostrar].Estacion[PlantaColumnas.ColumnaSelect.resultadosETABs[PlantaColumnas.ColumnaSelect.StoryMostrar].Estacion.Count-1];
                

                try
                {
                    y2= PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN] + PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN - 1] + (float)PlantaColumnas.ColumnaSelect.resultadosETABs[IndiceN - 1 ].Estacion[0];

                }
                catch
                {
                   y2 = (float)PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN] + (float)PlantaColumnas.ColumnaSelect.resultadosETABs[IndiceN].Estacion[0];


                }

                e.Graphics.DrawLine(new Pen(Brushes.Black), x1, YI- y1*SY+RefuerzoRequerido.Height, x2, YI-y2*SY+RefuerzoRequerido.Height);


                for (int i = 0; i < PlantaColumnas.ColumnaSelect.resultadosETABs[IndiceN].Estacion.Count; i++)
                {


                    try
                    {
                        x2 = (float)PlantaColumnas.ColumnaSelect.resultadosETABs[IndiceN - 1].As[i];
                        y2 = PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN] + PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN - 1]-(float)PlantaColumnas.ColumnaSelect.resultadosETABs[IndiceN - 1].Estacion[i];

                        e.Graphics.DrawLine(new Pen(Brushes.Red), x1,YI -y2 * SY + RefuerzoRequerido.Height, x2 * SX,YI -y2 * SY + RefuerzoRequerido.Height);

                    }
                    catch {                    }

                    x2 = (float)PlantaColumnas.ColumnaSelect.resultadosETABs[IndiceN].As[i];
                    y2 = PlantaColumnas.ColumnaSelect.LuzLibre[IndiceN]  - PlantaColumnas.ColumnaSelect.resultadosETABs[IndiceN].Estacion[i];

                    e.Graphics.DrawLine(new Pen(Brushes.Red), x1,YI -y2 * SY + RefuerzoRequerido.Height, x2 * SX,YI -y2 * SY + RefuerzoRequerido.Height);


                }


            }
        }

        private void Up_Click(object sender, EventArgs e)
        {
            if(PlantaColumnas.ColumnaSelect != null)
            {
              

                    PlantaColumnas.ColumnaSelect.StoryMostrar = PlantaColumnas.ColumnaSelect.StoryMostrar - 2;
                    if (PlantaColumnas.ColumnaSelect.StoryMostrar < 0)
                    {
                        PlantaColumnas.ColumnaSelect.StoryMostrar = PlantaColumnas.ColumnaSelect.LuzLibre.Count-1;
                    }
                    RefuerzoRequerido.Invalidate();

               


            }
        }

        private void Down_Click(object sender, EventArgs e)
        {
            if (PlantaColumnas.ColumnaSelect != null)
            {

                if (PlantaColumnas.ColumnaSelect.StoryMostrar < PlantaColumnas.ColumnaSelect.LuzLibre.Count - 1)
                {
                    PlantaColumnas.ColumnaSelect.StoryMostrar = PlantaColumnas.ColumnaSelect.StoryMostrar + 2;
                    RefuerzoRequerido.Invalidate();

                }



            }
        }
    }
}