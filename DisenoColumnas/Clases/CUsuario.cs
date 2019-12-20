using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace DisenoColumnas.Clases
{
    public class CUsuario
    {
        public string Username { get; set; }
        public bool Permiso { get; set; } = false;

        public void Get_user()
        {
            string Ruta_Carpeta;
            string Ruta_Archivo;
            string Ruta_Completa;

            Username = WindowsIdentity.GetCurrent().Name;
            string User_aux = "";
            //Ruta_Completa = @"\\servidor\\Dllo SW\\Secciones Predefinidas - Columnas\\Secciones.sec";

            //try
            //{
            Ruta_Carpeta = Application.StartupPath;
            Ruta_Archivo = @"\\Secciones.sec";
            Ruta_Completa = Ruta_Carpeta + Ruta_Archivo;
            //}
            //catch (System.Exception)
            //{
            //    Ruta_Completa = @"\\servidor\\Dllo SW\\Secciones Predefinidas - Columnas\\Secciones.sec";
            //}

            FileInfo finfo = new FileInfo(Ruta_Completa);
            var FSec = finfo.GetAccessControl();

            foreach (FileSystemAccessRule rule in FSec.GetAccessRules(true, true, typeof(NTAccount)))
            {
                User_aux = rule.IdentityReference.ToString();

                if (Username == User_aux)
                {
                    if (rule.FileSystemRights == FileSystemRights.FullControl)
                    {
                        Permiso = true;
                    }
                    break;
                }
            }
        }
    }
}