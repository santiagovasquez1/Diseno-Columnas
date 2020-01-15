using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DisenoColumnas.Clases
{
    public class cRefuerzoCuadro
    {

        [Category("Coordenadas")]
        [DisplayName("1. X Inicial (cm)")]
        public float XInicial { get; set; }
        [Category("Coordenadas")]
        [DisplayName("2. Y Inicial (cm)")]
        public float YInicial { get; set; }
        [Category("Coordenadas")]
        [DisplayName("3. X Final (cm)")]

        public float XFinal { get; set; }
        [Category("Coordenadas")]
        [DisplayName("4. Y Final (cm)")]
        public float YFinal { get; set; }

        //Horizontal
        [Category("Dirección Horizontal")]
        [DisplayName("Separación Real (cm)")]
        [ReadOnly(true)]
        [RefreshProperties(RefreshProperties.All)]
        public float Sreal_Horizontal { get; set; }  //ReadOnly

        [Category("Dirección Horizontal")]
        [DisplayName("Separación (cm)")]
        [DefaultValue(10)]
        [RefreshProperties(RefreshProperties.All)]
        public float SMax_Horizontal { get; set; }

        [Category("Dirección Horizontal")]
        [DisplayName("Cantidad")]
        [ReadOnly(true)]
        [RefreshProperties(RefreshProperties.All)]
        public int Cantidad_Horizontal { get; set; }


        //Vertical
        [Category("Dirección Vertical")]
        [DisplayName("Separación Real (cm)")]
        [ReadOnly(true)]
        [RefreshProperties(RefreshProperties.All)]
        public float Sreal_Vertical { get; set; }  //ReadOnly

        [Category("Dirección Vertical")]
        [DisplayName("Separación (cm)")]
        [DefaultValue(10)]
        [RefreshProperties(RefreshProperties.All)]
        public float SMax_Vertical { get; set; }

        [Category("Dirección Vertical")]
        [DisplayName("Cantidad")]
        [ReadOnly(true)]
        [RefreshProperties(RefreshProperties.All)]
        public int Cantidad_Vertical { get; set; }


        [Browsable(false)]
        public int Cantidad_VerticalUsar { get; set; }

        public void HallarCantidadHorizontal()
        {
           
            float DisHo = FunctionsProject.DistanciaEntrePuntos(XInicial, 0, XFinal, 0);


            float EspaciosFloat = (DisHo / SMax_Horizontal) -1;
            int EspaciosX = (int)Math.Ceiling(DisHo / SMax_Horizontal) - 1;
            float Porcentaje1 = EspaciosFloat / EspaciosX;

            if(Porcentaje1>0.98f | Porcentaje1.ToString() =="NaN") { EspaciosX = EspaciosX+1; }




            int CantBarrasX = EspaciosX + 1;
            //Re calcular Separación
            Sreal_Horizontal= (float)Math.Round( DisHo / (CantBarrasX-1),2);
            Cantidad_Horizontal = CantBarrasX;


        }
        public void HallarCantidadVertical()
        {

            float DisVert = FunctionsProject.DistanciaEntrePuntos(0, YInicial, 0, YFinal);

            float EspaciosFloat = (DisVert / SMax_Vertical) - 1; 
            int EspaciosY = (int)Math.Ceiling(DisVert / SMax_Vertical)-1;

            float Porcentaje1 = EspaciosFloat / EspaciosY;

            if (Porcentaje1 > 0.98) { EspaciosY = EspaciosY + 1; }

            int CantBarrasY = EspaciosY ;
            //Re calcular Separación
            Sreal_Vertical = (float)Math.Round(DisVert / EspaciosY,2);
            Cantidad_VerticalUsar = CantBarrasY;
            //Mostrar
            Cantidad_Vertical = CantBarrasY-1;


        }

    


    }




}
