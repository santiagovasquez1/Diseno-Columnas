using DisenoColumnas.Clases;
using DisenoColumnas.Interfaz_Seccion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DisenoColumnas.Secciones
{
    public interface ISeccion
    {
        float B { get; set; }
        MAT_CONCRETE Material { get; set; }
        TipodeSeccion Shape { get; set; }
        double Area { get; set; }
        float H { get; set; }
        Estribo Estribo { get; set; }
        GraphicsPath Seccion_path { get; set; }
        string Name { get; set; }
        bool Editado { get; set; }
        List<GraphicsPath> Shapes_ref { get; set; }
        List<CRefuerzo> Refuerzos { get; set; }
        List<float[]> CoordenadasSeccion { get; set; }

        List<Tuple<int, int>> No_D_Barra { get; set; }
        List<Tuple<List<float[]>, int>> MnPn3D { get; set; }
        List<Tuple<List<float[]>, int>> PnMn2D { get; set; }
        List<Tuple<List<float[]>, int>> MuPu3D { get; set; }
        List<Tuple<List<float[]>, int>> PuMu2D { get; set; }
        List<Tuple<List<float[]>, int>> PnMn2D_v1 { get; set; }

        double Acero_Long { get; set; }

        Tuple<List<float[]>, List<float[]>> DiagramaInteraccionParaUnAngulo(int Angulo, bool MPUiltimos);

        void CalcularArea();

        void Cuanti_Vol(float FactorDisipacion1, float FactorDisipacion2, float r, float FY);

        void Calc_vol_inex(float r, float FY, GDE gDE);

        void Add_Ref_graph(double EscalaX, double EscalaY, double EscalaR, float Dx, float Dy);

        GraphicsPath Add_Estribos(double EscalaX, double EscalaY, float rec, float Dx, float Dy);

        void Dibujo_Seccion(Graphics g, double EscalaX, double EscalaY, bool seleccion, float Dx, float Dy);

        void Dibujo_Autocad(double Xi, double Yi, int Num_Alzado);

        /// <summary>
        /// Calcula y posiciona el refuerzo base, cuando no se cuenta con una seccion predefinida
        /// </summary>
        /// <param name="recub">Recubrimiento de la seccion</param>
        void Refuerzo_Base(double recub);

        void CalcNoDBarras();

        void Actualizar_Ref(Alzado palzado, int indice, FInterfaz_Seccion fInterfaz);

        void Refueroz_Adicional(Alzado palzado, int indice, FInterfaz_Seccion fInterfaz);

        double Peso_Estribo(Estribo pEstribo, float recubrimiento,int Cantidad);

        void DiagramaInteraccion();

        #region Propiedades y Metodos para verificación de Vc

        List<float[]> PM2M3V2V3 { get; set; }
        List<float> Vcx { get; set; }
        List<float> Vcy { get; set; }
        List<float> Vsx { get; set; }
        List<float> Vsy { get; set; }

        #endregion Propiedades y Metodos para verificación de Vc

        #region SobreCargas

        bool Equals(object obj);

        int CompareTo(object obj);

        #endregion SobreCargas
    }
}