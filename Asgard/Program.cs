using CefSharp;
using CefSharp.OffScreen;

using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;


namespace Asgard
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
                    AppDomain.CurrentDomain.AssemblyResolve += Resolver;
                    LoadCef();

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
        //[MethodImpl(MethodImplOptions.NoInlining)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadCef()
        {
            //CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
            CefSharpSettings.ShutdownOnExit = true;


            CefSettings settings = new CefSettings
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36",
                RootCachePath = "",
                CachePath = "",
                LogSeverity = LogSeverity.Disable,
                LogFile = "",
                //IgnoreCertificateErrors = true

                //WindowlessRenderingEnabled = true,
            };
            //Jokers Stash
            settings.CefCommandLineArgs.Add("ignore-certificate-errors");
            // Set BrowserSubProcessPath based on app bitness at runtime

            // Set BrowserSubProcessPath based on app bitness at runtime
            settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe");

            //NOTE: The following function will set all three params
            settings.SetOffScreenRenderingBestPerformanceArgs();
            //settings.CefCommandLineArgs.Add("disable-gpu");
            //settings.CefCommandLineArgs.Add("disable-gpu-compositing");
            //settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling");

            //settings.CefCommandLineArgs.Add("renderer-process-limit", "1");
            //settings.CefCommandLineArgs.Add("log-file");
            //settings.CefCommandLineArgs.Add("renderer-startup-dialog", "renderer-startup-dialog");
            //settings.CefCommandLineArgs.Add("incognito");
            //settings.CefCommandLineArgs.Add("test-type");
            settings.CefCommandLineArgs.Add("disable-application-cache");
            //settings.CefCommandLineArgs.Add("disable-image-loading");
            settings.CefCommandLineArgs.Add("disable-web-security");
            //Disable WebRTC
            //settings.CefCommandLineArgs.Add("webrtc.multiple_routes_enabled", "0");
            //settings.CefCommandLineArgs.Add("webrtc.nonproxied_udp_enabled", "0");
            //settings.CefCommandLineArgs.Add("webrtc.ip_handling_policy", "disable_non_proxied_udp");
            //settings.CefCommandLineArgs.Add("enable-media-stream", "0");
            //settings.CefCommandLineArgs.Add("disable-plugins-discovery"); //Disable discovering third-party plugins. Effectively loading only ones shipped with the browser plus third-party ones as specified by --extra-plugin-dir and --load-plugin switches
            //settings.CefCommandLineArgs.Add("disable-direct-write", "1");

            //settings.CefCommandLineArgs.Add("enable-system-flash"); //Automatically discovered and load a system-wide installation of Pepper Flash.
            //settings.CefCommandLineArgs.Add("allow-running-insecure-content"); //By default, an https page cannot run JavaScript, CSS or plugins from http URLs. This provides an override to get the old insecure behavior. Only available in 47 and above.
            //https://peter.sh/experiments/chromium-command-line-switches/#disable-site-isolation-trials

            //settings.CefCommandLineArgs.Add("disable-gpu");
            //settings.CefCommandLineArgs.Add("disable-gpu-vsync");

            // Disable Flash
            //settings.CefCommandLineArgs.Add("disable-system-flash", "1");

            // Proxy Stuff
            // More Refinement is needed for reaching local IP's
            //settings.CefCommandLineArgs.Add("proxy-auto-detect", "1");
            //settings.CefCommandLineArgs.Add("winhttp-proxy-resolver", "1");

            //settings.CefCommandLineArgs.Add("force-renderer-accessibility");



            Cef.EnableWaitForBrowsersToClose();
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        // Will attempt to load missing assembly from either x86 or x64 subdir
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp", StringComparison.Ordinal))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(
                    AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                    Environment.Is64BitProcess ? "x64" : "x86",
                    assemblyName);

                var outputAssembly = File.Exists(archSpecificPath) ? Assembly.LoadFile(archSpecificPath) : null;

                return outputAssembly;
            }

            return null;
        }
    }
}