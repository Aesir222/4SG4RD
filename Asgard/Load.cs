using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Load : Form
    {
        bool State = true;
        bool Stop = false;
        int Count = 0;
        // private static readonly string[] blackList = new[] { "cmd" };//Black List Applications

        public Load()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void TimerFadeOut_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.01;
            if (Count == 0)
            {
                while (State)
                {
                    TimerFadeOut.Stop();
                    Login login = new Login();
                    login.Show();
                    login.BringToFront();
                    this.Hide();
                    Stop = true;
                    break;
                }
            }
        }

        private void TimerFadeIn_Tick(object sender, EventArgs e)
        {
            if (!State)
            {
                TimerFadeIn.Stop();
            }

            if (this.Opacity < 1)
            {
                this.Opacity += 0.01;
            }

            Count += 1;
            CircularProgressBar.Value += 1;

            if (Count == 100)
            {
                TimerFadeIn.Stop();
                TimerFadeOut.Start();

            }
            //Thread.Sleep(1000);
        }

        private void Load_Load(object sender, EventArgs e)
        {

            TimerMonitorAplications.Start();
            CircularProgressBar.Value = 0;
            CircularProgressBar.Minimum = 0;
            CircularProgressBar.Maximum = 100;
            TimerFadeIn.Start();
        }


        //private void MonitorApplications()
        //{
        //    var restart = false;
        //    int n = 0;
        //    while (n < 1)
        //    {
        //        Process[] procs = Process.GetProcesses();
        //        foreach (Process p in procs)
        //        {
        //            foreach (string bl in blackList)
        //            {
        //                if (p.ProcessName == bl)
        //                {
        //                    Hermod hermod = new Hermod("Advertencia.", "Se detecto una actividad sospechosa, que puede afectar el funcionamiento de 4SG4RD.");
        //                    State = false;
        //                    hermod.ShowDialog();
        //                    hermod.BringToFront();
        //                    this.Close();
        //                }
        //            }

        //            if (Stop)
        //            {
        //                Stop = false;
        //                break;
        //            }
        //        }
        //        restart = true;
        //        n++;
        //    }
        //    Thread.Sleep(1000);
        //    if (restart)
        //    {
        //        restart = false;
        //        Task.Run(() => MonitorApplications());

        //    }
        //}

        private void TimerMonitorAplications_Tick(object sender, EventArgs e)
        {
            Process[] procs = Process.GetProcesses();
            foreach (Process p in procs)
            {
                foreach (string bl in Asatru.blackList)
                {
                    if (p.ProcessName == bl)
                    {
                        Yggdrasil.KillCEF();
                        TimerMonitorAplications.Stop();
                        State = false;
                        Hermod hermod = new Hermod("Advertencia.", $"{p.ProcessName} es un aplicativo que consideramos sospechoso y puede afectar el funcionamiento de 4SG4RD.");
                        hermod.ShowDialog();
                        hermod.BringToFront();
                        Application.Exit();
                        this.Close();
                    }
                }

                if (Stop)
                {
                    TimerMonitorAplications.Stop();
                    Stop = false;
                }
            }
        }
    }
}

