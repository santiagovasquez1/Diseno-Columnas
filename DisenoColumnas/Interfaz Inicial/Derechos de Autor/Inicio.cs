using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace DisenoColumnas.Interfaz_Inicial.Derechos_de_Autor
{
    public partial class Inicio : Form
    {
        public string ComprobarEntrada = "FAIL";

        public Inicio()
        {
            InitializeComponent();
           // RegistrarExtension();
        }
        //private void RegistrarExtension()
        //{
        //    RegistryKey clave1 = Registry.CurrentUser.OpenSubKey("Software", true);
        //    clave1.CreateSubKey("Classes");
        //    clave1 = clave1.OpenSubKey("Classes", true);

        //    clave1.CreateSubKey(".Colum");
        //    clave1 = clave1.OpenSubKey(".Colum", true);
        //    clave1.SetValue("", "archivo.Colum");

        //    clave1.Close();
        //    ////////////////////////////////////////
        //    RegistryKey clave2 = Registry.CurrentUser.OpenSubKey("Software", true);
        //    clave2.CreateSubKey("Classes");
        //    clave2 = clave2.OpenSubKey("Classes", true);

        //    clave2.CreateSubKey("archivo.Colum");
        //    clave2 = clave2.OpenSubKey("archivo.Colum", true);
        //    clave2.SetValue("", "File COLUM");

        //    clave2.CreateSubKey("DefaultIcon");
        //    clave2 = clave2.OpenSubKey("DefaultIcon", true);
        //    clave2.SetValue("", Application.StartupPath + "\\Iconos\\DocumentoColumnas1.ico");

        //    clave2.Close();
        //    ////////////////////////////////////////
        //    RegistryKey clave3 = Registry.CurrentUser.OpenSubKey("Software", true);
        //    clave3.CreateSubKey("Classes");
        //    clave3 = clave3.OpenSubKey("Classes", true);

        //    clave3.CreateSubKey("archivo.Colum");
        //    clave3 = clave3.OpenSubKey("archivo.Colum", true);
        //    clave3.SetValue("", "File COLUM");

        //    clave3.CreateSubKey("shell");
        //    clave3 = clave3.OpenSubKey("shell", true);

        //    clave3.CreateSubKey("open");
        //    clave3 = clave3.OpenSubKey("open", true);

        //    clave3.CreateSubKey("command");
        //    clave3 = clave3.OpenSubKey("command", true);

        //    clave3.SetValue("", "\"" + Application.StartupPath + "\\DisenoColumnas.exe\" \"%1\"");

        //    clave3.Close();
        //}
        private void Inicio_Load(object sender, EventArgs e)

        {
            this.Opacity = 0.0;
            timer1.Start();
        }

        private int cont = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            cont += 1;
            if (cont == 40)
            {
                Comprobar(this);
                timer1.Stop();
                //timer2.Start();
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
            {
                timer2.Stop();
            }
        }



        public static void Comprobar(Inicio Formulario)
        {
            string ComprobarEntrada = "CORRECT";

            String IP_Servidor = "";
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                string hostname = "FCSAS.COM";

                IPAddress[] addresses = Dns.GetHostAddresses(hostname);
                foreach (IPAddress address in addresses)
                {
                    IP_Servidor = address.ToString();
                }

                try
                {
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                    if (ctx.ConnectedServer.ToLower() == "servidor.fcsas.com".ToLower())
                        ComprobarEntrada = "CORRECT";
                }
                catch
                {
                }

                List<IPAddressCollection> ListaIPS = new List<IPAddressCollection>();

                foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        IPAddressCollection IP = properties.DnsAddresses;
                        ListaIPS.Add(IP);
                    }
                }

                for (int i = 0; i < ListaIPS.Count; i++)
                {
                    foreach (IPAddress iPAddress in ListaIPS[i])
                    {
                        string IP_CLIENTE = iPAddress.ToString();
                        if (IP_CLIENTE == IP_Servidor)
                        {
                            ComprobarEntrada = "CORRECT";
                            break;
                        }
                    }
                }

                if (ComprobarEntrada != "CORRECT")
                {
                    ComprobarEntrada = ComprobarAccesoPorMac();
                }

                if (ComprobarEntrada == "CORRECT")
                {
                    MessageBox.Show("BIENVENIDO", "efe Prima Ce", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Formulario.Visible = false;
                    Form1 Fromulario = new Form1();
                    Fromulario.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Acceso Denegado ", "efe Prima Ce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                }
            }
            else
            {
                ComprobarEntrada = ComprobarAccesoPorMac();
                if (ComprobarEntrada == "CORRECT")
                {
                    //MessageBox.Show("BIENVENIDO", "efe Prima Ce", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Formulario.Visible = false;
                    Form1 Fromulario = new Form1();
                    Fromulario.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Acceso Denegado ", "efe Prima Ce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                }
            }
        }

        private static string ComprobarAccesoPorMac()
        {
            // Por Dirección Mac
            string ComprobarEntrada = "FAIL";
            NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();
            List<string> MacAdress = new List<string>();
            foreach (NetworkInterface adapter in Interfaces)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                MacAdress.Add(adapter.GetPhysicalAddress().ToString());
            }
            List<string> ListaMacs = ListMacAdress();

            for (int i = 0; i < MacAdress.Count; i++)
            {
                for (int j = 0; j < ListaMacs.Count; j++)
                {
                    if (MacAdress[i] == ListaMacs[j])
                    {
                        ComprobarEntrada = "CORRECT";
                        break;
                    }
                }
            }
            return ComprobarEntrada;
        }

        private static List<string> ListMacAdress()
        {
            List<string> AuxMacs = new List<string>();
            AuxMacs.Add("00D8610657A5");
            AuxMacs.Add("448A5BF11455");
            AuxMacs.Add("64006AFC2C84");
            AuxMacs.Add("309C2328F381");
            AuxMacs.Add("E0D55E66DDBF");
            AuxMacs.Add("309C2328F34A");
            return AuxMacs;
        }
    }
}