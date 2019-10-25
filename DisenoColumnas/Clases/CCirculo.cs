using System;
using System.Collections.Generic;
using System.Drawing;

namespace DisenoColumnas.Clases
{
    public class CCirculo
    {
        public double radio { get; set; }
        public double[] Centro { get; set; } = { };
        public List<PointF> Puntos { get; set; } = new List<PointF>();

        public CCirculo(double pradio, double[] pCentro)
        {
            radio = pradio;
            Centro = pCentro;
        }

        public void Set_puntos(int numero_puntos)
        {
            double delta_angulo = 2 * Math.PI / numero_puntos;
            double angulo = 0;

            PointF pi=new PointF();
            double xc = Centro[0];
            double yc = Centro[1];

            for (int i = 0; i < numero_puntos; i++)
            {
                pi.X = Convert.ToSingle(xc + Math.Cos(angulo) * radio);
                pi.Y = Convert.ToSingle(yc + Math.Sin(angulo) * radio);
                Puntos.Add(pi);
                angulo += delta_angulo;
            }
        }
    }
}