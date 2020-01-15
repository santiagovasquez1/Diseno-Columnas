using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_de_Extension
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistrarExtension();
            MessageBox.Show("holi");
            Close();

        }

        private void RegistrarExtension()
        {
  
            RegistryKey clave2 = Registry.CurrentUser.OpenSubKey("Software", true);
            clave2.CreateSubKey("Classes");
            clave2 = clave2.OpenSubKey("Classes", true);
            try
            {
                clave2.DeleteSubKey("archivo.Colum");
            }
            catch { }
            try
            {
                clave2.DeleteSubKey(".colum");
            }
            catch { }
        }
    }
}
