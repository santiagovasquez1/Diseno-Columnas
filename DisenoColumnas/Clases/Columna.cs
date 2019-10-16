using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DisenoColumnas.Clases
{

   


    [Serializable]
    public class Columna
    {

        public Columna(string Nombre)
        {

            Name = Nombre;

        }


        public void Paint_(PaintEventArgs e, float HeightForm, float WidthForm, float SX, float SY,float WX1, float HY1,float XI, float YI)
        {


            if (CoordXY[0] < 0)
            {
                X_Colum= WX1*SX-Math.Abs( (float)CoordXY[0])* SX;

            }
            else
            {
                X_Colum =WX1*SX+ Math.Abs((float)CoordXY[0]) * SX;
            }

            if (CoordXY[1] < 0)
            {
                Y_Colum = -HY1*SY+ Math.Abs((float)CoordXY[1] )* SY + HeightForm;

            }
            else
            {
                Y_Colum = -HY1*SY- Math.Abs((float)CoordXY[1]) * SY + HeightForm;
            }

            X_Colum += XI;
            Y_Colum += YI;


            w = 0.5f * (SX + SY) * 0.5f;
            h = 0.5f * (SX + SY) * 0.5f;

            if (BrushesColor == null)
            {
                BrushesColor = Brushes.Black;
            }

            Graphics graphics = e.Graphics;

            graphics.FillRectangle(BrushesColor, X_Colum, Y_Colum, w, h);
       

      
        

            float Tamano_Text = (SX + SY) * 0.6f * 0.3f;
            float X_string = X_Colum + w;

            if (X_string + 3 * Tamano_Text >= WidthForm)
            {
                X_string = X_Colum - w-0.6f*SX;
            }

            float Y_string = Y_Colum;


            graphics.DrawString(Name, new Font("Calibri", Tamano_Text, FontStyle.Bold), Brushes.DarkRed, X_string, Y_string);


        }



        public Columna MouseDown(MouseEventArgs mouse)
        {
           
            if(X_Colum<= mouse.X && X_Colum+w >= mouse.X && Y_Colum<= mouse.Y && Y_Colum +h >= mouse.Y )
            {
                BrushesColor = Brushes.Red;
                return this;

            }
            else
            {
                BrushesColor = Brushes.Black;
                return null;
            }

        }







        [NonSerialized]
        public Brush BrushesColor = Brushes.Black;






        private float w;
        private float h;
        private float X_Colum;
        private float Y_Colum;



        public string Name { get; set; }
        public double[] CoordXY { get; set; } = new double[2];   /// Cordenadas en Planta

        public string Point{ get; set; }

        public List<Tuple<Seccion, string>> Seccions { get; set; } = new List<Tuple<Seccion, string>>();
        public Viga VigaMayor { get; set; }


        public List<float> LuzLibre { get; set; }

        public List<ResultadosETABS> resultadosETABs { get; set; }


        private ColumnaAlzadoDrawing CoordForAlzado1 { get; set; }

        private void CoordAlzado()
        {
            CoordForAlzado1 = new ColumnaAlzadoDrawing(this);


        }





        public void Paint_Alzado1(PaintEventArgs e, float HeightForm, float WidthForm, float SX,float SY,float X, float Y)
        {
            CoordAlzado();

            //Fundacion

            float x1 = X+CoordForAlzado1.Lista_CoordendasFundacion[0][0]*SX;
            float y1 =-Y -CoordForAlzado1.Lista_CoordendasFundacion[0][1]*SY+HeightForm;

            float x2 = X+CoordForAlzado1.Lista_CoordendasFundacion[1][0]*SX;
            float y2 = -Y-CoordForAlzado1.Lista_CoordendasFundacion[1][1]*SY + HeightForm;

            float x3 = X+CoordForAlzado1.Lista_CoordendasFundacion[2][0]*SX;
            float y3 = -Y-CoordForAlzado1.Lista_CoordendasFundacion[2][1]*SY+ HeightForm;

            float x4 = X+CoordForAlzado1.Lista_CoordendasFundacion[3][0]*SX;
            float y4 = -Y-CoordForAlzado1.Lista_CoordendasFundacion[3][1]*SY + HeightForm;


            SolidBrush brush = new SolidBrush(Color.FromArgb(78, 59, 49));
            e.Graphics.FillPolygon(brush, new PointF[] { new PointF(x1, y1), new PointF(x2, y2), new PointF(x3, y3) ,new PointF(x4,y4)});
            e.Graphics.DrawLine(new Pen(Brushes.Black), x1, y1, x2, y2);
            e.Graphics.DrawLine(new Pen(Brushes.Black), x2, y2, x3, y3);
            e.Graphics.DrawLine(new Pen(Brushes.Black), x3, y3, x4, y4);
            e.Graphics.DrawLine(new Pen(Brushes.Black), x4, y4, x1, y1);





            for (int i = 0; i < CoordForAlzado1.Lista_Coordenadas_Columna_Piso.Count; i++)
            {

                float x1_ = X + CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][0][0] * SX;
                float y1_ = -Y - CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][0][1] * SY + HeightForm;

                float x2_ = X + CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][1][0] * SX;
                float y2_ = -Y - CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][1][1] * SY + HeightForm;

                float x3_ = X + CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][2][0] * SX;
                float y3_ = -Y - CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][2][1] * SY + HeightForm;

                float x4_ = X + CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][3][0] * SX;
                float y4_ = -Y - CoordForAlzado1.Lista_Coordenadas_Columna_Piso[i][3][1] * SY + HeightForm;


                float x5_ = X + CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][0][0] * SX;
                float y5_ = -Y - CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][0][1] * SY + HeightForm;

                float x6_ = X + CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][1][0] * SX;
                float y6_ = -Y - CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][1][1] * SY + HeightForm;

                float x7_ = X + CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][2][0] * SX;
                float y7_ = -Y - CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][2][1] * SY + HeightForm;

                float x8_ = X + CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][3][0] * SX;
                float y8_ = -Y - CoordForAlzado1.Lista_Coordendas_Losa_Piso[i][3][1] * SY + HeightForm;


            
                SolidBrush brush2 = new SolidBrush(Color.FromArgb(169, 172, 173));
                e.Graphics.FillPolygon(brush2, new PointF[] { new PointF(x1_, y1_), new PointF(x2_, y2_), new PointF(x3_, y3_), new PointF(x4_, y4_) });
                e.Graphics.DrawLine(new Pen(Brushes.Black), x1_, y1_, x2_, y2_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x2_, y2_, x3_, y3_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x3_, y3_, x4_, y4_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x4_, y4_, x1_, y1_);

         
                SolidBrush brush3 = new SolidBrush(Color.FromArgb(198, 198, 198));
                e.Graphics.FillPolygon(brush3, new PointF[] { new PointF(x5_, y5_), new PointF(x6_, y6_), new PointF(x7_, y7_), new PointF(x8_, y8_) });

                e.Graphics.DrawLine(new Pen(Brushes.Black), x5_, y5_, x6_, y6_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x6_, y6_, x7_, y7_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x7_, y7_, x8_, y8_);
                e.Graphics.DrawLine(new Pen(Brushes.Black), x8_, y8_, x5_, y5_);





            }

            //Nivel Fundación

            float x5, y5, x6, y6;


            x5 = CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][0];
            y5 = -Y - CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][1] * SY + HeightForm;

            x6 = CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][0] + WidthForm;
            y6 = -Y - CoordForAlzado1.Lista_CoordenadasNivelFundacion[0][1] * SY + HeightForm;


            Pen Pen_Nivel = new Pen(Brushes.Red);
            Pen_Nivel.DashPattern = new float[] { 5, 2, 15, 4 };
            e.Graphics.DrawLine(Pen_Nivel, x5, y5, x6, y6);




        }





    }
}
