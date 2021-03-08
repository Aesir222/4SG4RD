using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loader
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool app;

            using (Mutex mutex = new Mutex(true, "instance", out app))
            {
                if (app)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Load());

                    mutex.ReleaseMutex();
                }
                else
                {
                    Hermod hermod = new Hermod("Consejo de Odin.", $"Una senda no puede ser recorrida 2 veces.");
                    hermod.ShowDialog();
                    hermod.BringToFront();
                    //Application.Exit();
                    //this.Close();
                }

            }
        }
    }
}
