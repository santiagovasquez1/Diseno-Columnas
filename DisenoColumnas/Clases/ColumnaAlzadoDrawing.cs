using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace DisenoColumnas.Clases
{
    [Serializable]
    class ColumnaAlzadoDrawing
    {

        public ColumnaAlzadoDrawing(Columna columna)
        {
            PuntosFundacion(columna);
            PuntosPorPiso(columna);

         }
        
        private void PuntosFundacion(Columna columna)
        {

            float[] P1_ = new float[] { 0, 0 }; var P1 = Vector<float>.Build.Dense(P1_);

            var P2 = Vector<float>.Build.Dense(new float[] { columna.Seccions[columna.Seccions.Count - 1].Item1.B, 0 });

            var P3 = Vector<float>.Build.Dense(new float[] { columna.Seccions[columna.Seccions.Count - 1].Item1.B, Form1.Proyecto_.e_Fundacion });

            var P4 = Vector<float>.Build.Dense(new float[] { 0, Form1.Proyecto_.e_Fundacion });

            Lista_CoordendasFundacion.Add(P1);
            Lista_CoordendasFundacion.Add(P2);
            Lista_CoordendasFundacion.Add(P3);
            Lista_CoordendasFundacion.Add(P4);


            var P5 = Vector<float>.Build.Dense(new float[] {0, Form1.Proyecto_.e_Fundacion - Form1.Proyecto_.Nivel_Fundacion });
  
            Lista_CoordenadasNivelFundacion.Add(P5);
        }


        private void PuntosPorPiso(Columna columna)
        {

            float H_S = Form1.Proyecto_.Nivel_Fundacion + Form1.Proyecto_.e_Fundacion;

            float AlturaAcum = 0;

            for (int i = 0; i < columna.Seccions.Count; i++)
            {
                if (columna.Seccions[i].Item1 != null)
                {
                    AlturaAcum += columna.LuzLibre[i] + columna.VigaMayor.Seccions[i].Item1.H;
                }
            }
            AlturaAcum += H_S;

            for (int i = 0; i < columna.LuzLibre.Count; i++)
            {

                if (columna.Seccions[i].Item1 != null )
                {
                    var P1 = Vector<float>.Build.Dense(new float[] { 0, AlturaAcum - columna.VigaMayor.Seccions[i].Item1.H });

                    var P2 = Vector<float>.Build.Dense(new float[] { columna.Seccions[i].Item1.B, AlturaAcum - columna.VigaMayor.Seccions[i].Item1.H });

                    var P3 = Vector<float>.Build.Dense(new float[] { columna.Seccions[i].Item1.B, AlturaAcum - columna.VigaMayor.Seccions[i].Item1.H - columna.LuzLibre[i] });

                    var P4 = Vector<float>.Build.Dense(new float[] { 0, AlturaAcum - columna.VigaMayor.Seccions[i].Item1.H - columna.LuzLibre[i] });
                    
     


                    //Para Vigas

                    var P5 = Vector<float>.Build.Dense(new float[] { 0, AlturaAcum  });
                    var P6 = Vector<float>.Build.Dense(new float[] { columna.Seccions[i].Item1.B, AlturaAcum });
                    var P7 = Vector<float>.Build.Dense(new float[] { columna.Seccions[i].Item1.B, AlturaAcum - columna.VigaMayor.Seccions[i].Item1.H });
                    var P8 = Vector<float>.Build.Dense(new float[] { 0, AlturaAcum - columna.VigaMayor.Seccions[i].Item1.H });
                   



                    AlturaAcum = AlturaAcum - columna.VigaMayor.Seccions[i].Item1.H - columna.LuzLibre[i];
                    List<Vector<float>> ListaAux = new List<Vector<float>>();
                    List<Vector<float>> ListaAux2 = new List<Vector<float>>();



                    ListaAux.Add(P1); ListaAux.Add(P2); ListaAux.Add(P3); ListaAux.Add(P4);
                    ListaAux2.Add(P5); ListaAux2.Add(P6); ListaAux2.Add(P7); ListaAux2.Add(P8);

                    Lista_Coordenadas_Columna_Piso.Add(ListaAux);
                    Lista_Coordendas_Losa_Piso.Add(ListaAux2);


                }

            }

        }



        public List<Vector<float>> Lista_CoordendasFundacion = new List<Vector<float>>();
        public List<Vector<float>> Lista_CoordenadasNivelFundacion = new List<Vector<float>>();
        public List<List<Vector<float>>> Lista_Coordenadas_Columna_Piso = new List<List<Vector<float>>>();
        public List<List<Vector<float>>> Lista_Coordendas_Losa_Piso = new List<List<Vector<float>>>();








    }
}
