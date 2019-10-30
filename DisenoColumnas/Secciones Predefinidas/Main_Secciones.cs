using DisenoColumnas.Clases;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DisenoColumnas.Secciones_Predefinidas
{
    public class Main_Secciones
    {
        public static CLista_Secciones Lista_Secciones = new CLista_Secciones();

        public static void Crear_archivo()
        {
            string Ruta_Carpeta = @"\\servidor\\Dllo SW\\Secciones Predefinidas - Columnas";
            string Ruta_Archivo = "Secciones.sec";
            string Ruta_Completa = @"\\servidor\\Dllo SW\\Secciones Predefinidas - Columnas\\Secciones.sec";
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
            Seccion seccioni;

            List<MAT_CONCRETE> Lista_materiales = new List<MAT_CONCRETE>();

            MAT_CONCRETE Material1 = new MAT_CONCRETE
            {
                Name = "H350",
                FC = 350
            };

            MAT_CONCRETE Material2 = new MAT_CONCRETE
            {
                Name = "H280",
                FC = 280
            };

            MAT_CONCRETE Material3 = new MAT_CONCRETE
            {
                Name = "H210",
                FC = 210
            };

            Lista_materiales.AddRange(new MAT_CONCRETE[] { Material1, Material2, Material3 });

            foreach (MAT_CONCRETE material in Lista_materiales)
            {
                #region Seccion35X40

                Nombre_Seccion = "C35x40" + material.Name;
                Diametros_seccion = new int[] { 4, 4, 4, 4, 5, 5, 4, 4, 4, 4 };
                CapasX = 3; CapasY = 4; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 35F, 40F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion35X40

                #region Seccion35X50

                Nombre_Seccion = "C35x50" + material.Name;
                Diametros_seccion = new int[] { 4, 4, 5, 4, 4, 5, 5, 4, 4, 5, 4, 4 };
                CapasX = 3; CapasY = 5; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 35F, 50F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion35X50

                #region Seccion35X60

                Nombre_Seccion = "C35x60" + material.Name;
                Diametros_seccion = new int[] { 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4 };
                CapasX = 3; CapasY = 6; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 35F, 60F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion35X60

                #region Seccion35X80

                Nombre_Seccion = "C35x80" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
                CapasX = 3; CapasY = 6; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 35F, 80F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion35X80

                #region Seccion30X70

                Nombre_Seccion = "C30x70" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 4, 4, 5, 5, 5, 5, 4, 4, 5, 5 };
                CapasX = 2; CapasY = 6; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 30F, 70F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion30X70

                #region Seccion30X100

                Nombre_Seccion = "C30x100" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
                CapasX = 2; CapasY = 8; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 30F, 100F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion30X100

                #region Seccion30X120

                Nombre_Seccion = "C30x120" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5, 5, 4, 5, 4, 5, 4, 5, 5, 5 };
                CapasX = 2; CapasY = 10; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 30F, 120F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion30X120

                #region Seccion40X70

                Nombre_Seccion = "C40x70" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
                CapasX = 3; CapasY = 6; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 40F, 70F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion40X70

                #region Seccion40X80

                Nombre_Seccion = "C40x80" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 6, 6, 5, 5, 6, 6, 5, 5, 6, 6, 5, 5 };
                CapasX = 3; CapasY = 6; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 40F, 80F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion40X80

                #region Seccion40X90

                Nombre_Seccion = "C40x90" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5, 5 };
                CapasX = 3; CapasY = 7; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 40F, 90F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion40X90

                #region Seccion40X120

                Nombre_Seccion = "C40x120" + material.Name;
                Diametros_seccion = new int[] { 6, 6, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 6, 6 };
                CapasX = 3; CapasY = 9; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 40F, 120F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion40X120

                #region Seccion40X140

                Nombre_Seccion = "C40x140" + material.Name;
                Diametros_seccion = new int[] { 6, 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6, 6 };
                CapasX = 3; CapasY = 10; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 40F, 140F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion40X140

                #region Seccion45X110

                Nombre_Seccion = "C45x110" + material.Name;
                Diametros_seccion = new int[] { 6, 6, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 6, 6 };
                CapasX = 4; CapasY = 8; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 45F, 110F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion45X110

                #region Seccion50X60

                Nombre_Seccion = "C50x60" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5 };
                CapasX = 5; CapasY = 6; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 50F, 60F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion50X60

                #region Seccion50X70

                Nombre_Seccion = "C50x70" + material.Name;
                Diametros_seccion = new int[] { 6, 5, 5, 5, 6, 6, 6, 6, 6, 6, 5, 5, 5, 6 };
                CapasX = 4; CapasY = 5; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 50F, 70F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion50X70

                #region Seccion50X80

                Nombre_Seccion = "C50x80" + material.Name;
                Diametros_seccion = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
                CapasX = 5; CapasY = 7; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 50F, 80F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion50X80

                #region Seccion50X100

                Nombre_Seccion = "C50x100" + material.Name;
                Diametros_seccion = new int[] { 6, 6, 5, 5, 5, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 5, 5, 5, 6, 6 };
                CapasX = 5; CapasY = 7; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 50F, 100F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion50X100

                #region Seccion50X120

                Nombre_Seccion = "C50x120" + material.Name;
                Diametros_seccion = new int[] { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6 };
                CapasX = 5; CapasY = 8; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 50F, 120F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion50X120

                #region Seccion60X60

                Nombre_Seccion = "C60x60" + material.Name;
                Diametros_seccion = new int[] { 5, 6, 6, 6, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 5 };
                CapasX = 5; CapasY = 5; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 60F, 60F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion60X60

                #region Seccion60X90

                Nombre_Seccion = "C60x90" + material.Name;
                Diametros_seccion = new int[] { 6, 6, 6, 5, 6, 6, 6, 6, 6, 5, 5, 6, 6, 6, 6, 6, 5, 6, 6, 6 };
                CapasX = 5; CapasY = 7; CapasXw = 0; CapasYw = 0;
                seccioni = FunctionsProject.DeepClone(Crear_Seccion(Nombre_Seccion, 60F, 90F, 0, 0, material, Diametros_seccion, CapasX, CapasY, CapasXw, CapasYw));
                Lista_Secciones.Secciones.Add(seccioni);

                #endregion Seccion60X90
            }
        }

        public static Seccion Crear_Seccion(string Nombre_seccion, float b, float h, float tw, float tf, MAT_CONCRETE material, int[] Diametros_Seccion, int CapasX, int CapasY, int CapasXw, int CapasYw)
        {
            Seccion temp = new Seccion(Nombre_seccion, b / 100, h / 100, tf / 100, tw / 100, material, TipodeSeccion.Rectangular, new List<float[]>());
            temp.Refuerzos = Set_Refuerzo_Seccion(Diametros_Seccion, CapasX, CapasY, CapasXw, CapasYw, b, h, tw, tf);
            temp.Acero_Long = temp.Refuerzos.Sum(X => X.As_Long);
            temp.Estribo = Set_estribo(temp, temp.Material);
            return temp;
        }

        public static List<CRefuerzo> Set_Refuerzo_Seccion(int[] Diametros_Seccion, int CapasX, int CapasY, int CapasXw, int CapasYw, float b, float h, float Tw, float tf)
        {
            double posx, posy;
            int ContX, ContY, id;
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
            id = 1;

            for (int i = 0; i < Diametros_Seccion.Length; i++)
            {
                Coord_ref[0] = posx;
                Coord_ref[1] = posy;

                refuerzoi = new CRefuerzo(id, "#" + Diametros_Seccion[i], pcoord: new double[] { posx, posy }, ptipo: TipodeRefuerzo.longitudinal);
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
                id++;
            }

            return Refuerzos_Seccion;
        }

        public static Estribo Set_estribo(Seccion Seccioni, MAT_CONCRETE material)
        {
            Estribo Temp = new Estribo(3);
            int pos = 0;
            string Nombre_Seccion="";
         
            pos = Seccioni.Name.IndexOf(material.Name);
            Nombre_Seccion = Seccioni.Name.Substring(0, pos);

            switch (Nombre_Seccion)
            {
                case "C30x70":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 7.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 9.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 8.5f };
                    }
                    return Temp;

                case "C30x100":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 8.0f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 10.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 10.0f };
                    }
                    return Temp;

                case "C30x120":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 8.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 10.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 13f };
                    }
                    return Temp;

                case "C35x40":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 8.0f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 10.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 7.5f };
                    }
                    return Temp;

                case "C35x50":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 9.0f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 11.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 9.5f };
                    }
                    return Temp;

                case "C35x60":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 10.0f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 8.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 11.0f };
                    }
                    return Temp;

                case "C35x80":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 7.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 9.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 12.5f };
                    }
                    return Temp;

                case "C40x70":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 10f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 8.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 11.0f };
                    }
                    return Temp;

                case "C40x80":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 9.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 7.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 10.5f };
                    }
                    return Temp;

                case "C40x90":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 10.0f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 8.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 12.0f };
                    }
                    return Temp;

                case "C40x120":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 8.0f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 10.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 13.0f };
                    }
                    return Temp;

                case "C40x140":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 8.0f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 10.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 13.0f };
                    }
                    return Temp;

                case "C45x110":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 7.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 9.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 12.5f };
                    }
                    return Temp;

                case "C50x60":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 8.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 10.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 13f };
                    }
                    return Temp;

                case "C50x70":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 10f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 7.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 10f };
                    }
                    return Temp;

                case "C50x80":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 9.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 11.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 13f };
                    }
                    return Temp;

                case "C50x100":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 10.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 10.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 13f };
                    }
                    return Temp;

                case "C50x120":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 10.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 13.0f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 13f };
                    }
                    return Temp;

                case "C60x60":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 7.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 9.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 12.5f };
                    }
                    return Temp;

                case "C60x90":
                    if (material.FC == 350)
                    {
                        Temp = new Estribo(3) { Separacion = 8.5f };
                    }
                    if (material.FC == 280)
                    {
                        Temp = new Estribo(3) { Separacion = 10.5f };
                    }
                    if (material.FC == 210)
                    {
                        Temp = new Estribo(3) { Separacion = 13.0f };
                    }
                    return Temp;
            }

            return Temp;
        }
    }
}