using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loader
{
    public partial class Load : Form
    {
        bool State = true;
        bool Stop = false;
        int Count = 0;
        int ValueCircularBar = 0;
        private readonly string[] blackList;
        //private readonly string[] blackList = new[] {
        //"Wireshark",
        //"Proxifier",
        //"widecap",
        //"pcapui",
        //"FreeCAD",
        //"KKCap",
        //"cheatengine-x86_64-SSE4-AVX2",
        //"OMATIC",
        //"psiphon-115",
        //"devenv",
        //"jgrasp32",
        //"jgrasp64",
        //"Netbeans32",
        //"Netbeans64",
        //"idea32",
        //"idea64",
        //"eclipse",
        //"jgrasp",
        //"Squalr",
        //"Code",
        //"dnSpy",
        //"MagickChecker",
        //"MagickInstall",
        //"SpaceChecker",
        //"chromedriver",
        //"Advanced_IP_Scanner_2.5.3850",
        //"GlassWire",
        //"WeModAuxiliaryService",
        //"WeMod",
        //"BlueJ",
        //"cmd"
        //};//Black List Applications

        public Load()
        {
            InitializeComponent();
        }

        private void TimerFadeOut_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.01;
            if (Count == 0)
            {
                while (State)
                {
                    TimerFadeOut.Stop();
                    Stop = true;
                    Process.Start(@".\Asgard.exe");
                    this.Close();
                    break;
                }
            }
            Count--;
        }

        private void TimerFadeIn_Tick(object sender, EventArgs e)
        {
            if (!State)
            {
                TimerFadeIn.Stop();
            }

            if (ValueCircularBar <= 100)
            {
                if (this.Opacity < 1)
                {
                    this.Opacity += 0.01;
                }


                CircularProgressBar.Value = ValueCircularBar;
                ValueCircularBar++;
                Count++;
            }

            if (Count == 100)
            {
                TimerFadeIn.Stop();
            }
        }

        private void TimerMonitorAplications_Tick(object sender, EventArgs e)
        {
            //Process[] procs = Process.GetProcesses();
            //foreach (Process p in procs)
            //{
            //    foreach (string bl in blackList)
            //    {
            //        if (p.ProcessName == bl)
            //        {
            //            //Yggdrasil.KillCEF();
            //            TimerMonitorAplications.Stop();
            //            State = false;
            //            Hermod hermod = new Hermod("Advertencia.", $"{p.ProcessName} es un aplicativo que consideramos sospechoso y puede afectar el funcionamiento de 4SG4RD.");
            //            hermod.ShowDialog();
            //            hermod.BringToFront();
            //            Application.Exit();
            //            this.Close();
            //        }
            //    }

            //    if (Stop)
            //    {
            //        TimerMonitorAplications.Stop();
            //        Stop = false;
            //    }
            //}
        }

        private void Load_Load(object sender, EventArgs e)
        {
            CircularProgressBar.Minimum = 0;
            CircularProgressBar.Maximum = 100;
            CircularProgressBar.Value = 0;
            //TimerMonitorAplications.Start();
            TimerFadeIn.Start();
            Updater();
        }

        private async Task Updater()
        {
            WebClient webClient = new WebClient();
            try
            {
                FileVersionInfo.GetVersionInfo("Asgard.exe");
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo("Asgard.exe");
                // Print the file name and version number.
                //MessageBox.Show("File: " + myFileVersionInfo.FileDescription + '\n' +
                //   "Version number: " + myFileVersionInfo.FileVersion);
                if (myFileVersionInfo.FileVersion != "1.0.0.0")
                {
                    LabelDownload.Text = "Descargando archivos necesarios, por favor espere.";
                    await webClient.DownloadFileTaskAsync("https://4sg4rd.club/update/update.zip", @"update.zip");
                    File.Delete(@".\Asgard.exe");
                    string zipPath = @".\update.zip";
                    string extractPath = @".\";
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    File.Delete(@".\update.zip");
                    LabelDownload.Text = "Descarga Correcta.";
                }
                else
                {
                    LabelDownload.Text = "Un agradable visitante de tierras lejanas...";
                    await Task.Delay(5000);
                }

                TimerFadeOut.Start();
                // Process.Start(@".\Asgard.exe");
                // this.Close();
            }
            catch (Exception)
            {
                //TimerFadeIn.Start();
                //throw;
                LabelDownload.Text = "";
                File.Delete(@".\update.zip");
                await Task.Delay(5000);
                TimerFadeOut.Start();
                //while (Count == 99 )
                //{
                //}
                //State = false;
                //TimerFadeOut.Start();
                //Process.Start(@".\Asgard.exe");
                //this.Close();

            }

        }

    }
}
