using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Asgard
{
    public partial class Video1 : Form, IForm
    {
        private int Id { set; get; }
        private string Token { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }
        public Color Color { set; get; }

        private static ChromiumWebBrowser browser;

        //private double[] _previewItems;
       //private double _selectedPreview;
        public Video1()
        {
            InitializeComponent();
            //browser = new ChromiumWebBrowser();
            //browser.FrameLoadEnd += MyBrowserOnFrameLoadEnd;
        }

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 5)
            {
                Id = (int)parameters[0];
                Token = parameters[1].ToString();
                Name = parameters[2].ToString();
                Url = parameters[3].ToString();
                Color = (Color)parameters[4];
            }
        }

        private void Browser_Load(object sender, EventArgs e)
        {
            PictureBoxLoadShop1.Visible = false;
            browser = new ChromiumWebBrowser();
            //await Task.Delay(500);

            PanelTitleShop.BackColor = Color;
            labelTitleToolBar.Text = Name;
            LoadShop();
            PanelBrowser.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
            //browser.Load(Url);
            //LoadShop(browser, Url);
            //{

            //browser.FrameLoadStart += LoadStart;
            //browser.FrameLoadEnd += LoadEnd;
            //}
            //browser.SetZoomLevel((Convert.ToDouble(browser.Tag) - 100) / 25.0);

        }

        private async Task LoadShop()
        {
            PictureBoxLoadShop1.Visible = true;
            bool load = await browser.LoadPage(Url);
            if (load)
            {

            }
            PictureBoxLoadShop1.Visible = false;
        }


        private void IconButtonGoBack_Click(object sender, EventArgs e)
        {
            if (browser.CanGoBack)
            {
                browser.Back();
            }
        }

        private void IconButtonGoForward_Click(object sender, EventArgs e)
        {
            if (browser.CanGoForward)
            {
                browser.Forward();
            }
        }

        private void IconButtonRefresh_Click(object sender, EventArgs e)
        {
            browser.Reload();
        }

        private void IconButtonIncreaseZoom_Click(object sender, EventArgs e)
        {
            ChangeZoom(0.5); //You could also use 0.1 or 1.0, as you like and in the decrease button you use -0.5, etc.
        }

        private void IconButtonDecreaseZoom_Click(object sender, EventArgs e)
        {
            ChangeZoom(-0.5);
        }

        private void ChangeZoom(double change)
        {
            Task<double> task = browser.GetZoomLevelAsync();
            task.ContinueWith(previous =>
            {
                if (previous.IsCompleted)
                {
                    double currentLevel = previous.Result;
                    browser.SetZoomLevel(currentLevel + change);
                }
                else
                {
                    throw new InvalidOperationException("Unexpected failure of calling CEF->GetZoomLevelAsync", previous.Exception);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        private void IconButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IconButtonBack_Click(object sender, EventArgs e)
        {
            //Form form = Parent.Controls.OfType<Bifrost>().FirstOrDefault();
            //if (form == null)
            //{
            //    Parent.Parent.Show();
            //    Parent.Parent.BringToFront();
            //    form.TopLevel = false;
            //    form.Dock = DockStyle.Fill;
            //    Parent.Parent.Controls.Add(form);
            //    Parent.Parent.Tag = form;
            //    form.BringToFront();
            //    form.Show();
            //}
            //else
            //{
            //    form.BringToFront();
            //}
        }
        public async Task<bool> LoadShop(IWebBrowser browser, string address = null)
        {
            try
            {
                TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                void handler(object sender, LoadingStateChangedEventArgs args)
                {
                    if (!args.IsLoading)
                    {
                        browser.LoadingStateChanged -= handler;
                        tcs.TrySetResult(true);
                    }
                }

                browser.LoadingStateChanged += handler;
                if (!string.IsNullOrEmpty(address))
                {
                    browser.Load(address);
                }
                return await tcs.Task;
            }
            catch (Exception) { /*throw;*/ }
            return await new Task<bool>(() => false);
        }
    }
}
