using DisenoColumnas.Clases;
using System.Collections.Generic;
using System.IO;

namespace DisenoColumnas.Secciones_Predefinidas
{
    public class Main_Secciones
    {
        public static CLista_Secciones Lista_Secciones = new CLista_Secciones();

        public static void Crear_archivo()
        {



            string Ruta_Carpeta = @"\\servidor\Dllo SW\Secciones Predefinidas - Columnas";
            string Ruta_Archivo = "Secciones.sec";
            string Ruta_Completa = @"\\servidor\Dllo SW\Secciones Predefinidas - Columnas\Secciones.sec";
            bool Encuentra = false;

            DirectoryInfo directory_seccion = new DirectoryInfo(Ruta_Carpeta);

            foreach (FileInfo Archivo in directory_seccion.GetFiles())
            {
                if (Archivo.Name == Ruta_Archivo)
                {
                    Encuentra = true;
                    FunctionsProject.Deserealizar_Secciones(Ruta_Completa, ref Lista_Secciones);
                    break;
                }
            }

            if (Encuentra == false)
            {
                Lista_Secciones = new CLista_Secciones();
                Crear_Secciones();
                FunctionsProject.Serializar_Secciones(Ruta_Completa, Lista_Secciones);
            }

            Form1.secciones_predef = Lista_Secciones;

        }

        public static void Crear_Secciones()
        {
            string Nombre_Seccion = "";
            int[] Diametros_seccion;
            int CapasX, CapasY, CapasXw, CapasYw;

            MAT_CONCRETE Material = new MAT_CONCRETE
            {
                Name = "H350",
                FC = 350
            };

            #region Seccion35X40

            Nombre_Seccion = "C35x40";
            Diametros_seccion = new int[] { 4, 4, 4, 4, 5, 5, 4, 4, 4, 4 };
            CapasX = 3; CapasY = 4; CapasXw = 0; CapasYw = 0;
            Seccion seccion1 = Crear_Seccion(Nombre_Seccion, 35F, 40F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion1);

            #endregion Seccion35X40

            #region Seccion35X50

            Nombre_Seccion = "C35x50";
            Diametros_seccion = new int[] { 4, 4, 5, 4, 4, 5, 5, 4, 4, 5, 4, 4 };
            CapasX = 3; CapasY = 5; CapasXw = 0; CapasYw = 0;
            Seccion seccion2 = Crear_Seccion(Nombre_Seccion, 35F, 50F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion2);

            #endregion Seccion35X50

            #region Seccion35X60

            Nombre_Seccion = "C35x60";
            Diametros_seccion = new int[] { 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4 };
            CapasX = 3; CapasY = 6; CapasXw = 0; CapasYw = 0;
            Seccion seccion3 = Crear_Seccion(Nombre_Seccion, 35F, 60F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion3);

            #endregion Seccion35X60

            #region Seccion35X80

            Nombre_Seccion = "C35x80";
            Diametros_seccion = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            CapasX = 3; CapasY = 6; CapasXw = 0; CapasYw = 0;
            Seccion seccion4 = Crear_Seccion(Nombre_Seccion, 35F, 80F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion4);

            #endregion Seccion35X80

            #region Seccion30X70

            Nombre_Seccion = "C30x70";
            Diametros_seccion = new int[] { 5, 5, 4, 4, 5, 5, 5, 5, 4, 4, 5, 5 };
            CapasX = 2; CapasY = 6; CapasXw = 0; CapasYw = 0;
            Seccion seccion5 = Crear_Seccion(Nombre_Seccion, 30F, 70F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion5);

            #endregion Seccion30X70

            #region Seccion30X100

            Nombre_Seccion = "C30x100";
            Diametros_seccion = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            CapasX = 2; CapasY = 8; CapasXw = 0; CapasYw = 0;
            Seccion seccion6 = Crear_Seccion(Nombre_Seccion, 30F, 100F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion6);

            #endregion Seccion30X100

            #region Seccion30X120

            Nombre_Seccion = "C30x120";
            Diametros_seccion = new int[] { 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5 };
            CapasX = 2; CapasY = 10; CapasXw = 0; CapasYw = 0;
            Seccion seccion7 = Crear_Seccion(Nombre_Seccion, 30F, 120F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion7);

            #endregion Seccion30X120

            #region Seccion40X70

            Nombre_Seccion = "C40x70";
            Diametros_seccion = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            CapasX = 3; CapasY = 6; CapasXw = 0; CapasYw = 0;
            Seccion seccion8 = Crear_Seccion(Nombre_Seccion, 40F, 70F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion8);

            #endregion Seccion40X70

            #region Seccion40X80

            Nombre_Seccion = "C40x80";
            Diametros_seccion = new int[] { 5, 5, 6, 6, 5, 5, 6, 6, 5, 5, 6, 6, 5, 5 };
            CapasX = 3; CapasY = 6; CapasXw = 0; CapasYw = 0;
            Seccion seccion9 = Crear_Seccion(Nombre_Seccion, 40F, 80F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion9);

            #endregion Seccion40X80

            #region Seccion40X90

            Nombre_Seccion = "C40x90";
            Diametros_seccion = new int[] { 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5 };
            CapasX = 3; CapasY = 7; CapasXw = 0; CapasYw = 0;
            Seccion seccion10 = Crear_Seccion(Nombre_Seccion, 40F, 90F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion10);

            #endregion Seccion40X90

            #region Seccion40X120

            Nombre_Seccion = "C40x120";
            Diametros_seccion = new int[] { 6, 6, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 6, 6 };
            CapasX = 3; CapasY = 9; CapasXw = 0; CapasYw = 0;
            Seccion seccion11 = Crear_Seccion(Nombre_Seccion, 40F, 120F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion11);

            #endregion Seccion40X120

            #region Seccion40X140

            Nombre_Seccion = "C40x140";
            Diametros_seccion = new int[] { 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6 };
            CapasX = 3; CapasY = 10; CapasXw = 0; CapasYw = 0;
            Seccion seccion12 = Crear_Seccion(Nombre_Seccion, 40F, 140F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion12);

            #endregion Seccion40X140

            #region Seccion45X110

            Nombre_Seccion = "C45x110";
            Diametros_seccion = new int[] { 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6 };
            CapasX = 4; CapasY = 8; CapasXw = 0; CapasYw = 0;
            Seccion seccion13 = Crear_Seccion(Nombre_Seccion, 45F, 110F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion13);

            #endregion Seccion45X110

            #region Seccion50X60

            Nombre_Seccion = "C50x60";
            Diametros_seccion = new int[] { 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5 };
            CapasX = 5; CapasY = 6; CapasXw = 0; CapasYw = 0;
            Seccion seccion14 = Crear_Seccion(Nombre_Seccion, 50F, 60F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion14);

            #endregion Seccion50X60

            #region Seccion50X70

            Nombre_Seccion = "C50x70";
            Diametros_seccion = new int[] { 6, 5, 5, 5, 6, 6, 6, 6, 6, 6, 5, 5, 5, 6 };
            CapasX = 4; CapasY = 5; CapasXw = 0; CapasYw = 0;
            Seccion seccion15 = Crear_Seccion(Nombre_Seccion, 50F, 70F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion15);

            #endregion Seccion50X70

            #region Seccion50X80

            Nombre_Seccion = "C50x80";
            Diametros_seccion = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            CapasX = 5; CapasY = 7; CapasXw = 0; CapasYw = 0;
            Seccion seccion16 = Crear_Seccion(Nombre_Seccion, 50F, 80F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion16);

            #endregion Seccion50X80

            #region Seccion50X100

            Nombre_Seccion = "C50x100";
            Diametros_seccion = new int[] { 6, 6, 5, 5, 5, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 5, 5, 5, 6, 6 };
            CapasX = 5; CapasY = 7; CapasXw = 0; CapasYw = 0;
            Seccion seccion17 = Crear_Seccion(Nombre_Seccion, 50F, 100F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion17);

            #endregion Seccion50X100

            #region Seccion50X120

            Nombre_Seccion = "C50x100";
            Diametros_seccion = new int[] { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
            CapasX = 5; CapasY = 8; CapasXw = 0; CapasYw = 0;
            Seccion seccion18 = Crear_Seccion(Nombre_Seccion, 50F, 120F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion18);

            #endregion Seccion50X120

            #region Seccion60X60

            Nombre_Seccion = "C60x60";
            Diametros_seccion = new int[] { 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5 };
            CapasX = 5; CapasY = 5; CapasXw = 0; CapasYw = 0;
            Seccion seccion19 = Crear_Seccion(Nombre_Seccion, 60F, 60F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion19);

            #endregion Seccion60X60

            #region Seccion60X90

            Nombre_Seccion = "C60x90";
            Diametros_seccion = new int[] { 6, 6, 6, 5, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 5, 6, 6, 6 };
            CapasX = 5; CapasY = 7; CapasXw = 0; CapasYw = 0;
            Seccion seccion20 = Crear_Seccion(Nombre_Seccion, 60F, 90F, 0, 0, Material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw);
            Lista_Secciones.Secciones_predefinidas.Add(seccion20);

            #endregion Seccion60X90
        }

        public static Seccion Crear_Seccion(string Nombre_seccion, float b, float h, float tw, float tf, MAT_CONCRETE material, int[] Diametros_Seccion, int CapasX, int CapasY, int CapasXw, int CapasYw)
        {
            Seccion temp = new Seccion(Nombre_seccion, b, h, tf, tw, material, TipodeSeccion.Rectangular, new List<float[]>());
            temp.Refuerzos = Set_Refuerzo_Seccion(Diametros_Seccion, CapasX, CapasXw, CapasYw, CapasY, temp.B, temp.H, temp.TW, temp.TF);
            return temp;
        }

        public static List<CRefuerzo> Set_Refuerzo_Seccion(int[] Diametros_Seccion, int CapasX, int CapasY, int CapasXw, int CapasYw, float b, float h, float Tw, float tf)
        {
            double posx, posy;
            int ContX, ContY;
            double r = 6; //1 es el espesor del estribo #3
            double[] Coord_ref = new double[2];
            double DeltaX1, DeltaY1, DeltaX2, DeltaY2;
            CRefuerzo refuerzoi;
            List<CRefuerzo> Refuerzos_Seccion = new List<CRefuerzo>();

            DeltaX1 = (b - 2 * r) / (CapasX - 1);
            DeltaY1 = (h - 2 * r) / (CapasY - 1);
            DeltaX2 = (Tw - 2 * r) / (CapasXw - 1);
            DeltaY2 = (tf - 2 * r) / (CapasYw - 1);

            posx = -(b / 2) + r; posy = (h / 2) - r;
            ContX = CapasX - 2; ContY = CapasY;

            for (int i = 0; i < Diametros_Seccion.Length - 1; i++)
            {
                Coord_ref[0] = posx;
                Coord_ref[1] = posy;

                refuerzoi = new CRefuerzo(i, "#" + Diametros_Seccion[i], Coord_ref, TipodeRefuerzo.longitudinal);
                Refuerzos_Seccion.Add(refuerzoi);

                posy -= DeltaY1;
                ContY--;

                if (ContY == 0 & ContX > 0)
                {
                    posx += DeltaX1;
                    posy = (h / 2) - r;
                    ContY = 2;
                    DeltaY1 = (h - 2 * r) / (ContY - 1);
                    ContX--;
                }

                if (ContX == 0 & ContY == 0)
                {
                    ContY = CapasY;
                    DeltaY1 = (h - 2 * r) / (ContY - 1);
                    posx = (b / 2) - r;
                    posy = (h / 2) - r;
                }
            }

            return Refuerzos_Seccion;
        }
    }
}