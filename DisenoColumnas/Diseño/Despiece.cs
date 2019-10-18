using DisenoColumnas.Clases;
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
            Columna ColumnaSelect = Form1.Proyecto_.ColumnaSelect;
            if (ColumnaSelect != null)
            {
                float MaxB = -999999;
                for (int i = 0; i < ColumnaSelect.Seccions.Count; i++)
                {
                    if (ColumnaSelect.Seccions[i].Item1 != null)
                    {
                        if (ColumnaSelect.Seccions[i].Item1.B > MaxB)
                        {
                            MaxB = ColumnaSelect.Seccions[i].Item1.B;
                        }
                    }
                }

                float SX = (Draw_Column.Width - 15) / MaxB;
                float SY = Draw_Column.Height / (Form1.Proyecto_.AlturaEdificio);
                float X = 7.5f;
                float Y = 5;

                e.Graphics.Clear(Color.White);
                Title_Colum_Model.Text = "Columna: " + ColumnaSelect.Name;
                ColumnaSelect.Paint_Alzado1(e, Draw_Column.Height - 10, Draw_Column.Width, SX, SY, X, Y);
            }
        }

        private void Despiece_Paint(object sender, PaintEventArgs e)
        {
            Draw_Column.Invalidate();
            //RefuerzoRequerido.Invalidate();
        }

        //

        //private void PictureBox2_Paint(object sender, PaintEventArgs e)
        //{
        //    if (ColumnaSelect != null)
        //    {
        //        if(ColumnaSelect.StoryMostrar == -1)
        //        {
        //            ColumnaSelect.StoryMostrar = ColumnaSelect.LuzLibre.Count - 1;

        //        }
        //        e.Graphics.Clear(Color.White);

        //        double MaxAs = -999999;
        //        for (int i = 0; i < ColumnaSelect.resultadosETABs.Count; i++)
        //        {
        //            if (ColumnaSelect.resultadosETABs[i] != null)
        //            {
        //                for (int j = 0; j < ColumnaSelect.resultadosETABs[i].As.Count; j++)
        //                {
        //                    if (ColumnaSelect.resultadosETABs[i].As[j] > MaxAs)
        //                    {
        //                        MaxAs = ColumnaSelect.resultadosETABs[i].As[j];
        //                    }
        //                }
        //            }
        //        }

        //        float SX =  (RefuerzoRequerido.Width - 10) / (float)MaxAs;
        //        int IndiceN = ColumnaSelect.StoryMostrar;
        //        float AltoEscalar;
        //        try
        //        {
        //            AltoEscalar = ColumnaSelect.LuzLibre[IndiceN]+ ColumnaSelect.LuzLibre[IndiceN-1];
        //        }
        //        catch
        //        {
        //            AltoEscalar = ColumnaSelect.LuzLibre[IndiceN];
        //        }

        //        float SY = (RefuerzoRequerido.Height-30) / AltoEscalar;
        //        float X = 7.5f;
        //        float Y = 5;

        //        string MensajeaMostrar;

        //        try
        //        {
        //            MensajeaMostrar = ColumnaSelect.Seccions[IndiceN].Item2 +" - "+ ColumnaSelect.Seccions[IndiceN-1].Item2;
        //        }
        //        catch
        //        {
        //            MensajeaMostrar = ColumnaSelect.Seccions[IndiceN].Item2;
        //        }
        //        CambiarPiso.Text = MensajeaMostrar;

        //        //Lineas Inicales
        //        float x1, y1, x2, y2;

        //        x1 = 5;
        //        x2 = 5;
        //        float YI = -15;
        //        y1 = 0;
        //        //y1 = (float)ColumnaSelect.resultadosETABs[ColumnaSelect.StoryMostrar].Estacion[ColumnaSelect.resultadosETABs[ColumnaSelect.StoryMostrar].Estacion.Count-1];

        //        try
        //        {
        //            y2= ColumnaSelect.LuzLibre[IndiceN] + ColumnaSelect.LuzLibre[IndiceN - 1] + (float)ColumnaSelect.resultadosETABs[IndiceN - 1 ].Estacion[0];

        //        }
        //        catch
        //        {
        //           y2 = (float)ColumnaSelect.LuzLibre[IndiceN] + (float)ColumnaSelect.resultadosETABs[IndiceN].Estacion[0];

        //        }

        //        e.Graphics.DrawLine(new Pen(Brushes.Black), x1, YI- y1*SY+RefuerzoRequerido.Height, x2, YI-y2*SY+RefuerzoRequerido.Height);

        //        for (int i = 0; i < ColumnaSelect.resultadosETABs[IndiceN].Estacion.Count; i++)
        //        {
        //            try
        //            {
        //                x2 = (float)ColumnaSelect.resultadosETABs[IndiceN - 1].As[i];
        //                y2 = ColumnaSelect.LuzLibre[IndiceN] + ColumnaSelect.LuzLibre[IndiceN - 1]-(float)ColumnaSelect.resultadosETABs[IndiceN - 1].Estacion[i];

        //                e.Graphics.DrawLine(new Pen(Brushes.Red), x1,YI -y2 * SY + RefuerzoRequerido.Height, x2 * SX,YI -y2 * SY + RefuerzoRequerido.Height);

        //            }
        //            catch {                    }

        //            x2 = (float)ColumnaSelect.resultadosETABs[IndiceN].As[i];
        //            y2 = ColumnaSelect.LuzLibre[IndiceN]  - ColumnaSelect.resultadosETABs[IndiceN].Estacion[i];

        //            e.Graphics.DrawLine(new Pen(Brushes.Red), x1,YI -y2 * SY + RefuerzoRequerido.Height, x2 * SX,YI -y2 * SY + RefuerzoRequerido.Height);

        //        }

        //    }
        //}

        //private void Up_Click(object sender, EventArgs e)
        //{
        //    if(ColumnaSelect != null)
        //    {
        //            ColumnaSelect.StoryMostrar = ColumnaSelect.StoryMostrar - 2;
        //            if (ColumnaSelect.StoryMostrar < 0)
        //            {
        //                ColumnaSelect.StoryMostrar = ColumnaSelect.LuzLibre.Count-1;
        //            }
        //            RefuerzoRequerido.Invalidate();

        //    }
        //}

        //private void Down_Click(object sender, EventArgs e)
        //{
        //    if (ColumnaSelect != null)
        //    {
        //        if (ColumnaSelect.StoryMostrar < ColumnaSelect.LuzLibre.Count - 1)
        //        {
        //            ColumnaSelect.StoryMostrar = ColumnaSelect.StoryMostrar + 2;
        //            RefuerzoRequerido.Invalidate();

        //        }

        //    }
        //}
    }
}