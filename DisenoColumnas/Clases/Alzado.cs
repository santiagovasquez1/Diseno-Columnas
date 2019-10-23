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
            DistX = ID * 0.4f;
        }

        public int ID { get; set; }
        public float DistX { get; set; }
        public List<AlzadoUnitario> Colum_Alzado { get; set; }

        


    }

    [Serializable]
    public class AlzadoUnitario
    {



        public int NoStory { get; set; }

        public int CantBarras { get; set; }
        public int NoBarra { get; set; }
        public string Traslapo_Nomenc { get; set; }
        public List<float[]> Coord_Alzado_PB { get; set; }

        public float Traslapo { get; set; }
        private int NoAlzado { get; set; }
        public float Hviga { get; set; }
        public float H_Stroy { get; set; }

        public float DistX { get; set; }

        private float HViga_V1 { get; set; }
        private float H_Stroy_V1 { get; set; }
        private float HViga_V2 { get; set; }
        private float H_Stroy_V2 { get; set; }

        public float Hacum { get; set; }

        public bool UltPiso { get; set; }

        public float e_Fu { get; set; }
        public AlzadoUnitario(int CantBarras_, int NoBarra_, string Traslapo, int NoPiso_, int NoAlzado_, float H, float HV1, float HV2, float Hviga_, float Hviga_V1, float Hviga_V2, float e_fund_, bool UltPiso_, float Hacum_)
        {
            CantBarras = CantBarras_;
            NoBarra = NoBarra_;
            Traslapo_Nomenc = Traslapo;
            NoStory = NoPiso_;
            NoAlzado = NoAlzado_;
            H_Stroy = H;
            Hviga = Hviga_;
            e_Fu = e_fund_;
            HViga_V1 = Hviga_V1;
            HViga_V2 = Hviga_V2;
            H_Stroy_V1 = HV1;
            H_Stroy_V2 = HV2;
            UltPiso = UltPiso_;
            Hacum = Hacum_;
        }



        public void Paint(PaintEventArgs e, float SX, float SY, float HeighForm, float YI,float XI)
        {

            if (Coord_Alzado_PB != null)
            {
                List<PointF> Points = new List<PointF>();

                for (int i = 0; i < Coord_Alzado_PB.Count; i++)
                {
                    PointF point = new PointF(XI+Coord_Alzado_PB[i][0] * SX, HeighForm - Coord_Alzado_PB[i][1] * SY - YI);
                    Points.Add(point);
                }
                if (Points.Count != 0)
                {
                    e.Graphics.DrawLines(new Pen(Brushes.Black), Points.ToArray());
                }


            }

        }

















        public override string ToString()
        {
            return $"{CantBarras}#{NoBarra}{Traslapo_Nomenc}";
        }




    }





}
