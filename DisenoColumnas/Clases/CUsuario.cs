using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DisenoColumnas.Clases
{
    public class CUsuario
    {
        public string Username { get; set; }
        public bool Permiso { get; set; } = false;

        public void Get_user()
        {
            Username = WindowsIdentity.GetCurrent().Name;
            string Ruta_Completa = @"\\servidor\\Dllo SW\\Secciones Predefinidas - Columnas\\Secciones.sec";
            string User_aux = "";
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