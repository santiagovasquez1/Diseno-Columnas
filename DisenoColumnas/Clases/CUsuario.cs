using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DisenoColumnas.Clases
{
    public class CUsuario
    {
        public string Username { get; set; }
        public bool Permiso { get; set; } = false;

        public void Get_user ()
        {
            string Ruta_Completa;

            Username = WindowsIdentity.GetCurrent().Name;
            string Ruta_Carpeta = AppDomain.CurrentDomain.BaseDirectory;
            string Ruta_Archivo = "Secciones.sec";
            string User_aux = "";
            Ruta_Completa = Ruta_Carpeta + Ruta_Archivo;

            FileInfo finfo = new FileInfo(Ruta_Completa);
            var FSec = finfo.GetAccessControl();

            foreach ( FileSystemAccessRule rule in FSec.GetAccessRules(true , true , typeof(NTAccount)) )
            {
                User_aux = rule.IdentityReference.ToString();

                if ( Username == User_aux )
                {
                    if ( rule.FileSystemRights == FileSystemRights.FullControl )
                    {
                        Permiso = true;
                    }
                    break;
                }
            }
        }
    }
}