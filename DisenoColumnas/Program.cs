using System;
using System.Windows.Forms;

namespace DisenoColumnas
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Interfaz_Inicial.Derechos_de_Autor.Inicio());
        }
    }
}