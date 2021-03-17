using CefSharp;
using CefSharp.OffScreen;

using System;
using System.Drawing;
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
                    Hermod hermod = new Hermod("Consejo de Odin.", $"Una senda no puede ser recorrida 2 veces.", Color.FromArgb(30, 38, 70), Color.White);
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
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
            CefSharpSettings.ShutdownOnExit = true;
            CefSettings settings = new CefSettings
            {
               //UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36",
                RootCachePath = "",
                CachePath = "",
                LogSeverity = LogSeverity.Disable,
                LogFile = ""
                //WindowlessRenderingEnabled = true,
            };
            
            //Jokers Stash
            settings.CefCommandLineArgs.Add("ignore-certificate-errors");

            // Set BrowserSubProcessPath based on app bitness at runtime
            settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe");
            
            // Set Google API keys, used for Geolocation requests sans GPS.  See http://www.chromium.org/developers/how-tos/api-keys
            // Environment.SetEnvironmentVariable("GOOGLE_API_KEY", "");
            // Environment.SetEnvironmentVariable("GOOGLE_DEFAULT_CLIENT_ID", "");
            // Environment.SetEnvironmentVariable("GOOGLE_DEFAULT_CLIENT_SECRET", "");

            // Widevine CDM registration - pass in directory where Widevine CDM binaries and manifest.json are located.
            // For more information on support for DRM content with Widevine see: https://github.com/cefsharp/CefSharp/issues/1934
            //Cef.RegisterWidevineCdm(@".\WidevineCdm");

            //Chromium Command Line args
            //http://peter.sh/experiments/chromium-command-line-switches/
            //NOTE: Not all relevant in relation to `CefSharp`, use for reference purposes only.
            //CEF specific command line args
            //https://bitbucket.org/chromiumembedded/cef/src/master/libcef/common/cef_switches.cc?fileviewer=file-view-default
            //IMPORTANT: For enabled/disabled command line arguments like disable-gpu specifying a value of "0" like
            //settings.CefCommandLineArgs.Add("disable-gpu", "0"); will have no effect as the second argument is ignored.

            settings.RemoteDebuggingPort = 8088;
            //The location where cache data will be stored on disk. If empty an in-memory cache will be used for some features and a temporary disk cache for others.
            //HTML5 databases such as localStorage will only persist across sessions if a cache path is specified. 
            //settings.RootCachePath = Path.GetFullPath("cache");
            //If non-null then CachePath must be equal to or a child of RootCachePath
            //We're using a sub folder.
            //
            //settings.CachePath = Path.GetFullPath("cache\\global");
            //settings.UserAgent = "CefSharp Browser" + Cef.CefSharpVersion; // Example User Agent
            //settings.CefCommandLineArgs.Add("renderer-process-limit", "1");
            //settings.CefCommandLineArgs.Add("renderer-startup-dialog");
            //settings.CefCommandLineArgs.Add("enable-media-stream"); //Enable WebRTC
            //settings.CefCommandLineArgs.Add("no-proxy-server"); //Don't use a proxy server, always make direct connections. Overrides any other proxy server flags that are passed.
            //settings.CefCommandLineArgs.Add("debug-plugin-loading"); //Dumps extra logging about plugin loading to the log file.
            settings.CefCommandLineArgs.Add("disable-plugins-discovery"); //Disable discovering third-party plugins. Effectively loading only ones shipped with the browser plus third-party ones as specified by --extra-plugin-dir and --load-plugin switches
            //settings.CefCommandLineArgs.Add("enable-system-flash"); //Automatically discovered and load a system-wide installation of Pepper Flash.
            //settings.CefCommandLineArgs.Add("allow-running-insecure-content"); //By default, an https page cannot run JavaScript, CSS or plugins from http URLs. This provides an override to get the old insecure behavior. Only available in 47 and above.
            //https://peter.sh/experiments/chromium-command-line-switches/#disable-site-isolation-trials
            //settings.CefCommandLineArgs.Add("disable-site-isolation-trials");
            //NOTE: Running the Network Service in Process is not something CEF officially supports
            //It may or may not work for newer versions.
            //settings.CefCommandLineArgs.Add("enable-features", "CastMediaRouteProvider,NetworkServiceInProcess");

            //settings.CefCommandLineArgs.Add("enable-logging"); //Enable Logging for the Renderer process (will open with a cmd prompt and output debug messages - use in conjunction with setting LogSeverity = LogSeverity.Verbose;)
            //settings.LogSeverity = LogSeverity.Verbose; // Needed for enable-logging to output messages

            settings.CefCommandLineArgs.Add("disable-extensions"); //Extension support can be disabled
            settings.CefCommandLineArgs.Add("disable-pdf-extension"); //The PDF extension specifically can be disabled

            //Load the pepper flash player that comes with Google Chrome - may be possible to load these values from the registry and query the dll for it's version info (Step 2 not strictly required it seems)
            //settings.CefCommandLineArgs.Add("ppapi-flash-path", @"C:\Program Files (x86)\Google\Chrome\Application\47.0.2526.106\PepperFlash\pepflashplayer.dll"); //Load a specific pepper flash version (Step 1 of 2)
            //settings.CefCommandLineArgs.Add("ppapi-flash-version", "20.0.0.228"); //Load a specific pepper flash version (Step 2 of 2)

            //Audo play example
            //settings.CefCommandLineArgs["autoplay-policy"] = "no-user-gesture-required";

            //NOTE: For OSR best performance you should run with GPU disabled:
            // `--disable-gpu --disable-gpu-compositing --enable-begin-frame-scheduling`
            // (you'll loose WebGL support but gain increased FPS and reduced CPU usage).
            // http://magpcss.org/ceforum/viewtopic.php?f=6&t=13271#p27075
            //https://bitbucket.org/chromiumembedded/cef/commits/e3c1d8632eb43c1c2793d71639f3f5695696a5e8

            //NOTE: The following function will set all three params
            settings.SetOffScreenRenderingBestPerformanceArgs();
           //settings.CefCommandLineArgs.Add("disable-gpu");
            //settings.CefCommandLineArgs.Add("disable-gpu-compositing");
            //settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling");

            settings.CefCommandLineArgs.Add("disable-gpu-vsync"); //Disable Vsync

            // The following options control accessibility state for all frames.
            // These options only take effect if accessibility state is not set by IBrowserHost.SetAccessibilityState call.
            // --force-renderer-accessibility enables browser accessibility.
            // --disable-renderer-accessibility completely disables browser accessibility.
            //settings.CefCommandLineArgs.Add("force-renderer-accessibility");
            //settings.CefCommandLineArgs.Add("disable-renderer-accessibility");

            //Enables Uncaught exception handler
            settings.UncaughtExceptionStackSize = 10;

            //Disable WebAssembly
            //settings.JavascriptFlags = "--noexpose_wasm";

            // Off Screen rendering (WPF/Offscreen)
            if (settings.WindowlessRenderingEnabled)
            {
                //Disable Direct Composition to test https://github.com/cefsharp/CefSharp/issues/1634
                settings.CefCommandLineArgs.Add("disable-direct-composition");

                // DevTools doesn't seem to be working when this is enabled
                // http://magpcss.org/ceforum/viewtopic.php?f=6&t=14095
                //settings.CefCommandLineArgs.Add("enable-begin-frame-scheduling");
            }


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