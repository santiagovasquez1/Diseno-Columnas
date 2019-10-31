using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Principal;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace DisenoColumnas.Clases
{
    public class CUsuario
    {
        public string Username { get; set; }
        public bool Permiso { get; set; }

        public void Get_user()
        {
            var user = WindowsIdentity.GetCurrent();
            var dominio = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            var token = user.Token;

            Username = user.Name;

        }
    }

    
}
