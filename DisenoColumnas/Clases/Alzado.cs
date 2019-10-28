using System;
using System.Collections.Generic;
using System.Drawing;
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

        public bool UltPiso { get; set; }

        public float e_Fu { get; set; }
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



        public void Paint(PaintEventArgs e, float SX, float SY, float HeighForm, float YI, float XI)
        {

            if (Coord_Alzado_PB != null)
            {
                List<PointF> Cord_Escala = new List<PointF>();

                for (int i = 0; i < Coord_Alzado_PB.Count; i++)
                {
                    PointF point = new PointF(XI + Coord_Alzado_PB[i][0] * SX, HeighForm - Coord_Alzado_PB[i][1] * SY - YI);
                    Cord_Escala.Add(point);
                }

                if (Cord_Escala.Count != 0)
                {
                    SolidBrush brush = FunctionsProject.ColorBarra(NoBarra);
                    e.Graphics.DrawLines(new Pen(brush, 2), Cord_Escala.ToArray());
                }


            }

        }

        
        public override string ToString()
        {

            if (Tipo == "Botton")
            {
                return $"-{CantBarras}#{NoBarra}";

            }
            else if (Tipo=="A")
            {
    
                if (UnitarioAdicional != null)
                {
                    return $"{CantBarras}#{NoBarra}{Tipo}-{UnitarioAdicional.CantBarras}#{UnitarioAdicional.NoBarra}";
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



    }







}
