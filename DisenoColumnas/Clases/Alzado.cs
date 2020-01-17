using DisenoColumnas.Diseño;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public class Alzado
    {
        public Alzado(int ID_, int NoPisos)
        {
            ID = ID_;
            Colum_Alzado = new List<AlzadoUnitario>();
            for (int i = 0; i < NoPisos; i++)
            {
                Colum_Alzado.Add(null);
            }
            DistX = ID * 0.6f;
        }

        public int ID { get; set; }
        public float DistX { get; set; }
        public List<AlzadoUnitario> Colum_Alzado { get; set; }
    }

    [Serializable]
    public class AlzadoUnitario
    {
        public AlzadoUnitario UnitarioAdicional { get; set; }

        public string Tipo2 { get; set; }

        public int NoStory { get; set; }

        public int CantBarras { get; set; }
        public int NoBarra { get; set; }
        public string Tipo { get; set; }
        public List<float[]> Coord_Alzado_PB { get; set; }

        public float Traslapo { get; set; }

        public float Hviga { get; set; }
        public float H_Stroy { get; set; }

        public float x1 { get; set; }

        public float Hacum { get; set; }

        public bool CotaAutoCAD { get; set; } = false;

        public bool UltPiso { get; set; }

        public float e_Fu { get; set; }
        public float Longitud { get; set; }

        #region Propiedades para Cantidades

        public Tuple<int, string> Cant_Nomenclatura_DLNET { get; set; }

        #endregion Propiedades para Cantidades

        #region Propiedades Auxiliares

        private bool MoveBarra = false;
        private float MouseX = 0;
        private float MouseY = 0;

        #endregion Propiedades Auxiliares

        public AlzadoUnitario(int CantBarras_, int NoBarra_, string Traslapo, int NoPiso_, int NoAlzado_, float H, float Hviga_, float e_fund_, bool UltPiso_, float Hacum_)
        {
            CantBarras = CantBarras_;
            NoBarra = NoBarra_;
            Tipo = Traslapo;
            NoStory = NoPiso_;
            H_Stroy = H;
            Hviga = Hviga_;
            e_Fu = e_fund_;
            UltPiso = UltPiso_;
            Hacum = Hacum_;
        }

        [NonSerialized]
        private Form_Barra FormBarra = new Form_Barra();

        private List<float[]> Coord_Alzado_PB_Escal { get; set; }

        public void Paint(PaintEventArgs e, float SX, float SY, float HeighForm, float YI, float XI)
        {
            if (Coord_Alzado_PB != null)
            {
                List<PointF> Cord_Escala = new List<PointF>();
                Coord_Alzado_PB_Escal = new List<float[]>();

                for (int i = 0; i < Coord_Alzado_PB.Count; i++)
                {
                    PointF point = new PointF(XI + Coord_Alzado_PB[i][0] * SX, HeighForm - Coord_Alzado_PB[i][1] * SY - YI);
                    float[] XY = new float[] { XI + Coord_Alzado_PB[i][0] * SX, HeighForm - Coord_Alzado_PB[i][1] * SY - YI };
                    Coord_Alzado_PB_Escal.Add(XY);
                    Cord_Escala.Add(point);
                }

                if (Cord_Escala.Count != 0)
                {
                    SolidBrush brush = FunctionsProject.ColorBarra(NoBarra);
                    e.Graphics.DrawLines(new Pen(brush, 2), Cord_Escala.ToArray());
                }

                if (FormBarra == null)
                {
                    FormBarra = new Form_Barra();
                }

                if (FormBarra.Disposing)
                {
                    FormBarra = new Form_Barra();
                }

                FormBarra.Location = new Point((int)MouseX, (int)MouseY);
                FormBarra.CantBarras.Text = Convert.ToString(CantBarras);
                FormBarra.D_Barra.Text = Convert.ToString(NoBarra);
                FormBarra.Ld_Barra.Text = Convert.ToString(Traslapo);

                if (Coord_Alzado_PB.Count != 0)
                {
                    Longitud = CalcularLongitudRefuerzo(Coord_Alzado_PB);

                    FormBarra.L_Barra.Text = String.Format("{0:0.00}", Math.Round(Longitud, 2));
                }

                if (MoveBarra)
                {
                    try
                    {
                        FormBarra.Visible = true;
                    }
                    catch { FormBarra = new Form_Barra(); FormBarra.Visible = true; }
                    Form1.m_Despiece.Invalidate();
                }
                else
                {
                    FormBarra.Visible = false;
                }
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            float EsBarra = 4f;
            if (Coord_Alzado_PB_Escal != null)
            {
                if (Coord_Alzado_PB_Escal.Count == 2)
                {
                    if (Coord_Alzado_PB_Escal[0][1] < Coord_Alzado_PB_Escal[1][1])
                    {
                        if (e.X >= Coord_Alzado_PB_Escal[0][0] && e.X <= Coord_Alzado_PB_Escal[1][0] + EsBarra &&
                            e.Y >= Coord_Alzado_PB_Escal[0][1] && e.Y <= Coord_Alzado_PB_Escal[1][1])
                        {
                            MouseX = Cursor.Position.X;
                            MouseY = Cursor.Position.Y;
                            MoveBarra = true;
                        }
                        else
                        {
                            MoveBarra = false;
                        }
                    }
                    else
                    {
                        if (e.X >= Coord_Alzado_PB_Escal[0][0] && e.X <= Coord_Alzado_PB_Escal[1][0] + EsBarra &&
                       e.Y >= Coord_Alzado_PB_Escal[1][1] && e.Y <= Coord_Alzado_PB_Escal[0][1])
                        {
                            MouseX = Cursor.Position.X;
                            MouseY = Cursor.Position.Y;
                            MoveBarra = true;
                        }
                        else
                        {
                            MoveBarra = false;
                        }
                    }
                }

                if (Coord_Alzado_PB_Escal.Count == 3 | Coord_Alzado_PB_Escal.Count == 4)
                {
                    GraphicsPath path = new GraphicsPath();
                    List<PointF> pointFs = Coord_Alzado_PB_Escal.ConvertAll(new Converter<float[], PointF>(FunctionsProject.CoversionaPuntos));
                    path.AddLines(pointFs.ToArray());

                    if (path.IsVisible(e.Location))
                    {
                        MouseX = Cursor.Position.X;
                        MouseY = Cursor.Position.Y;
                        MoveBarra = true;
                    }
                    else
                    {
                        MoveBarra = false;
                    }
                }
            }
        }

        public override string ToString()
        {
            if (Tipo == "Botton")
            {
                return $"-{CantBarras}#{NoBarra}";
            }
            else if (Tipo == "A")
            {
                if (UnitarioAdicional != null)
                {
                    if (Tipo2 != "")
                    {
                        return $"{Tipo2}{CantBarras}#{NoBarra}{Tipo}-{UnitarioAdicional.CantBarras}#{UnitarioAdicional.NoBarra}";
                    }
                    else
                    {
                        return $"{CantBarras}#{NoBarra}{Tipo}-{UnitarioAdicional.CantBarras}#{UnitarioAdicional.NoBarra}";
                    }
                }
                else if (Tipo2 != "")
                {
                    return $"{Tipo2}{CantBarras}#{NoBarra}{Tipo}";
                }
                else
                {
                    return $"{CantBarras}#{NoBarra}{Tipo}";
                }
            }
            else
            {
                return $"{CantBarras}#{NoBarra}{Tipo}";
            }
        }

        public float CalcularLongitudRefuerzo(List<float[]> Coordenadas)
        {
            float Longitud = 0;
            for (int i = 0; i < Coordenadas.Count; i++)
            {
                try
                {
                    float X1 = (float)Math.Round(Coordenadas[i][0], 2);
                    float X2 = (float)Math.Round(Coordenadas[i - 1][0], 2);
                    float Y1 = (float)Math.Round(Coordenadas[i][1], 2);
                    float Y2 = (float)Math.Round(Coordenadas[i - 1][1], 2);

                    Longitud += (float)Math.Sqrt(Math.Pow(X1 - X2, 2) + Math.Pow(Y1 - Y2, 2));
                }
                catch { }
            }

            return Longitud;
        }

        #region Metodos: Cantidades de Obra DlNet

        public void CreararTuplesParaCantidades()
        {
            if (Coord_Alzado_PB != null)
            {
                if (Coord_Alzado_PB.Count != 0)
                {
                    string Nomenclatura = "";
                    float Gancho90 = Form1.Proyecto_.G90[NoBarra];
                    float Longitud = (float)Math.Round(CalcularLongitudRefuerzo(Coord_Alzado_PB), 2);

                    if (Coord_Alzado_PB.Count == 2)  //Recta
                    {
                        Nomenclatura = $" #{NoBarra}  {String.Format("{0:0.00}", Longitud)}";
                    }
                    if (Coord_Alzado_PB.Count == 3)  //L
                    {
                        Nomenclatura = $" #{NoBarra}  {String.Format("{0:0.00}", Longitud)}   L{String.Format("{0:0.00}", Gancho90)}";
                    }
                    if (Coord_Alzado_PB.Count == 4)  //C
                    {
                        Nomenclatura = $" #{NoBarra}  {String.Format("{0:0.00}", Longitud)}   L{String.Format("{0:0.00}", Gancho90)}  L{String.Format("{0:0.00}", Gancho90)}";
                    }
                    Cant_Nomenclatura_DLNET = new Tuple<int, string>(CantBarras, Nomenclatura);
                }
            }
        }

        #endregion Metodos: Cantidades de Obra DlNet
    }
}