using CefSharp;
using CefSharp.OffScreen;
using CefSharp.ResponseFilter;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Query.Dynamic;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Web.Hosting;

namespace Asgard
{
    public static class Yggdrasil
    {
        public static ChromiumWebBrowser browser;
        public static string Url { set; get; }

        public static object evaluateJavaScriptResult;
        public static string God { set; get; }

        public static async Task<bool> LoadPage(this IWebBrowser browser, string address = null, string proxy = null)
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
                    if (proxy != null)
                    {
                        bool setProxy = await browser.SetProxy(proxy);
                        if (setProxy)
                        {
                            browser.Load(address);
                        }
                        else
                        {
                            browser.Load(address);
                        }
                    }
                    else
                    {
                        browser.Load(address);
                    }
                }

                return await tcs.Task;
            }
            catch (Exception) { /*throw;*/ }
            return await new Task<bool>(() => false);
        }

        public static Task<bool> FrameLoad(this IWebBrowser browser)
        {
            try
            {
                TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

                void handler(object sender, FrameLoadEndEventArgs args)
                {
                    if (!args.Frame.IsMain)
                        return;


                    tcs.TrySetResult(true);

                }

                browser.FrameLoadEnd += handler;

                return tcs.Task;
            }
            catch (Exception) { }
            return new Task<bool>(() => false);
        }


        public static void KillCEF()
        {
            //Wait until the browser has finished closing(which by default happens on a different thread).
            //Cef.EnableWaitForBrowsersToClose(); must be called before Cef.Initialize to enable this feature
            //See https://github.com/cefsharp/CefSharp/issues/3047 for details
            //await Task.Delay(1000);
            Cef.WaitForBrowsersToClose();

            //Clean up Chromium objects.  You need to call this in your application otherwise
            //you will get a crash when closing.
            //await Task.Delay(1000);
            Cef.PreShutdown();
            //await Task.Delay(1000);
            Cef.Shutdown();
        }

        public static async Task<IFrame> GetFrameByName(this ChromiumWebBrowser browser, string name, string nameException = "ExceptionGetFrameByNameDefault", int limit = 5)
        {
            List<long> identifier = browser.GetBrowser().GetFrameIdentifiers();
            foreach (long i in identifier)
            {
                IFrame frame = browser.GetBrowser().GetFrame(i);
                if (frame != null)
                {
                    for (int j = 0; j < limit; j++)
                    {
                        if (frame.Name == name)
                        {
                            return frame;
                        }
                        await Task.Delay(1000);
                    }
                }
            }
            //browser.Screenshot(nameException);
            return null;
        }


        public static async Task<IFrame> GetFrameByUrl(this ChromiumWebBrowser browser, string url, string nameException = "ExceptionGetFrameByUrlDefault", int limit = 20)
        {
            List<long> identifier = browser.GetBrowser().GetFrameIdentifiers();
            foreach (long i in identifier)
            {
                IFrame frame = browser.GetBrowser().GetFrame(i);
                if (frame != null)
                {
                    //for (int j = 0; j < limit; j++)
                    //{
                    if (frame.Url == url)
                    {
                        return frame;
                    }
                    //}
                    await Task.Delay(100);
                }
            }
            //browser.Screenshot(nameException);
            return null;
        }

        public static async Task<bool> ElementExists(this ChromiumWebBrowser browser, string selector, string nameException = "ExceptionExistsDefault", int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = document.querySelector('{selector}');" +
                @"return (node === document.body) ? false : document.body.contains(node);
                })();";

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await browser.ExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            // await browser.ScreenshotnameException);
            return false;
        }

        public static async Task<bool> ElementInnerTextContent(this ChromiumWebBrowser browser, string selector, string text, string nameException = "ExceptionExistsDefault", int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = document.querySelector('{selector}');" +
                @"let textFind = node.innerText.trim();" +
                $"if(textFind.includes('{text}'))" +
                @"{
                    return true;
                }else{
                    return false;
                } 
                })();";

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await browser.ExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            // await browser.ScreenshotnameException);
            return false;
        }
        public static async Task<bool> FElementInnerTextContent(this IFrame frame, string selector, string text, int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = document.querySelector('{selector}');" +
                @"let textFind = node.innerText.trim();" +
                $"if(textFind.includes('{text}'))" +
                @"{
                    return true;
                }else{
                    return false;
                } 
                })();";

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await frame.FExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            return false;
        }


        public static async Task<bool> ElementValue(this ChromiumWebBrowser browser, string selector, string value, string nameException = "ElementValue", int limit = 5)
        {
            string JsScript = @"(function(){" +
                $"let value = document.querySelector('{selector}').value.trim();" +
                $"if(value == '{value}')" +
                @"{
                    return true;
                }else{
                    return false;
                } 
                })();";

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await browser.ExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            // await browser.ScreenshotnameException);
            return false;
        }



        public static async Task<bool> ElementInnerTextEquals(this ChromiumWebBrowser browser, string selector, string text, string nameException = "ExceptionExistsDefault", int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = document.querySelector('{selector}');" +
                @"let textFind = node.innerText.trim();" +
                $"if(textFind == '{text}')" +
                @"{
                    return true;
                }else{
                    return false;
                } 
                })();";

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await browser.ExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            // await browser.ScreenshotnameException);
            return false;
        }

        public static async Task<bool> FElementInnerTextEquals(this IFrame frame, string selector, string text, string nameException = "ExceptionExistsDefault", int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = document.querySelector('{selector}');" +
                @"let textFind = node.innerText.trim();" +
                $"if(textFind == '{text}')" +
                @"{
                    return true;
                }else{
                    return false;
                } 
                })();";

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await frame.FExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            //browser.Screenshot(nameException);
            return false;
        }

        public static async Task<bool> FElementExists(this IFrame frame, string selector, int limit = 20)
        {
            if (frame != null)
            {
                string JsScript = @"(function(){" +
    $"let node = document.querySelector('{selector}');" +
    @"return (node === document.body) ? false : document.body.contains(node);
                })();";

                for (int i = 0; i < limit; i++)
                {
                    if ((bool)await frame.FExecuteScript(JsScript))
                    {
                        return true;
                    }
                    await Task.Delay(1000);
                }

            }
            //       frame.Screenshot(nameException);
            return false;
        }

        public static async Task<bool> ElementVisible(this ChromiumWebBrowser browser, string selector, string nameException = "ExceptionVisibleDefault", int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = document.querySelector('{selector}');" +
                @"return !!( node.offsetWidth || node.offsetHeight || node.getClientRects().length );
                })();";

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await browser.ExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            // await browser.ScreenshotnameException);
            return false;
        }


        public static async Task<bool> NodeVisibility(this ChromiumWebBrowser browser, string node, string nameException = "ExceptionVisibilityDefault", int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = {node};" +
                @"return (node == 'hidden') ? false : true;
                })();";
            //let node = document.querySelector("iframe[title='recaptcha challenge']").parentNode.parentNode.style.visibility

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await browser.ExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            // await browser.ScreenshotnameException);
            return false;
        }

        public static async Task<bool> ElementInvisible(this ChromiumWebBrowser browser, string selector, string nameException = "ExceptionInvisibleDefault", int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = document.querySelector('{selector}');" +
                @"return !!( node.offsetWidth || node.offsetHeight || node.getClientRects().length );
                })();";

            for (int i = 0; i < limit; i++)
            {
                if (!(bool)await browser.ExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            // await browser.ScreenshotnameException);
            return false;
        }

        public static async Task<bool> FrameElementVisible(this IFrame frame, string selector, int limit = 20)
        {
            string JsScript = @"(function(){" +
                $"let node = document.querySelector('{selector}');" +
                @"return !!( node.offsetWidth || node.offsetHeight || node.getClientRects().length );
                })();";

            for (int i = 0; i < limit; i++)
            {
                if ((bool)await frame.FExecuteScript(JsScript))
                {
                    return true;
                }
                await Task.Delay(1000);
            }
            //frame.Screenshot(nameException);
            return false;
        }

        public static async Task<bool> SendKeys(this ChromiumWebBrowser browser, string selector, string keys,int delay=50)
        {
            try
            {
                if (selector != string.Empty)
                {
                    bool click = await browser.Click(selector);
                    if (!click)
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(keys))
                {
                    for (int i = 0; i < keys.Length; i++)
                    {
                        KeyEvent k = new KeyEvent
                        {
                            WindowsKeyCode = ToKeyValue(char.Parse(keys.Substring(i, 1))),
                            FocusOnEditableField = true,
                            IsSystemKey = false,
                            Type = KeyEventType.Char
                        };
                        browser.GetBrowser().GetHost().SendKeyEvent(k);
                        await Task.Delay(delay);
                    }
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }

        public static async Task<bool> SendKeyCode(this ChromiumWebBrowser browser, int keyCode)
        {
            try
            {
                KeyEvent k = new KeyEvent
                {
                    WindowsKeyCode = keyCode,
                    FocusOnEditableField = true,
                    IsSystemKey = false,
                    Type = KeyEventType.KeyDown
                };
                browser.GetBrowser().GetHost().SendKeyEvent(k);
                await Task.Delay(50);
                k = new KeyEvent
                {
                    WindowsKeyCode = keyCode,
                    FocusOnEditableField = true,
                    IsSystemKey = false,
                    Type = KeyEventType.KeyUp
                };
                browser.GetBrowser().GetHost().SendKeyEvent(k);
                await Task.Delay(50);
                return true;
            }
            catch { }
            return false;
        }

        public static async Task<bool> JsSendKeys(this ChromiumWebBrowser browser, string selector, string keys)
        {
            try
            {
                string elementSetValue = $"document.querySelector('{selector}').value ='{keys}';";
                bool setValue = (bool)await browser.ExecuteScript(elementSetValue);
                if (setValue)
                {
                    await Task.Delay(1000);
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static async Task<bool> FJsSendKeys(this IFrame frame, string selector, string value)
        {
            try
            {
                string elementSetValue = $"document.querySelector('{selector}').value ='{value}';";
                bool setValue = (bool)await frame.FExecuteScript(elementSetValue);
                if (setValue)
                {
                    await Task.Delay(1000);
                    return true;
                }
            }
            catch { /*throw;*/ }
            return false;
        }


        public static async Task<bool> Click(this ChromiumWebBrowser browser, string selector)
        {
            try
            {
                string elementPosition = @"(function(){" +
                    $"let gbc, X = 0, Y = 0; element = document.querySelector('{selector}');" +
                   @"if (element)
                    {
                        gbc = element.getBoundingClientRect();
                        let centerX = gbc.width / 2;
                        let centerY = gbc.height / 2;
                        X = gbc.x + centerX;
                        Y = gbc.y + centerY;
                    }
                    return { x: X, y: Y}
                    })();";

                dynamic findElementPosition = await browser.ExecuteScript(elementPosition);
                if (findElementPosition.ToString() == "System.Dynamic.ExpandoObject")
                {
                    await browser.ClickXY((int)findElementPosition.x, (int)findElementPosition.y);
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }
        public static async Task ClickXY(this ChromiumWebBrowser browser, int x, int y)
        {
            try
            {
                browser.GetBrowser().GetHost().SendMouseClickEvent(x, y, MouseButtonType.Left, false, 1, CefEventFlags.None);
                await Task.Delay(100);
                browser.GetBrowser().GetHost().SendMouseClickEvent(x, y, MouseButtonType.Left, true, 1, CefEventFlags.None);
                await Task.Delay(500);
            }
            catch (Exception) { /*throw;*/ }
        }

        public static Task<object> ExecuteScript(this ChromiumWebBrowser browser, string script, bool message = false)
        {
            try
            {
                return browser.EvaluateScriptAsync(script).ContinueWith(taskJS =>
                {
                    if (taskJS.IsCompleted)
                    {
                        JavascriptResponse response = taskJS.Result;
                        taskJS.Dispose();
                        if (message)
                        {
                            //Clipboard.SetText(script);
                            MessageBox.Show(response.VarDump(0)
                                + "\n\r"
                                + "Script Copy To Clipboard: \n\r __________________________\n\r "
                                + script, response.Success.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }

                        if (response.Success)
                        {
                            //if (response.Result == null)
                            if (response.Result == null || response.Result.ToString() == "")
                            //if (string.IsNullOrEmpty(response.Result.ToString()))
                            {
                                return true;
                            }
                            else
                            {
                                if (response.Result.GetType() == typeof(ExpandoObject))
                                {
                                    dynamic res = response.Result as ExpandoObject;
                                    return res;
                                }
                                else
                                {
                                    return response.Result;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                });
            }
            catch (Exception) { }
            return new Task<object>(() => false);
        }

        public static Task<object> FExecuteScript(this IFrame frame, string script, bool message = false)
        {
            try
            {
                return frame.EvaluateScriptAsync(script).ContinueWith(taskJS =>
                {
                    if (taskJS.IsCompleted)
                    {
                        JavascriptResponse response = taskJS.Result;
                        taskJS.Dispose();
                        if (message)
                        {
                            MessageBox.Show(response.VarDump(0) + "\n\r" + "Script: \n\r __________________________\n\r " + script, response.Success.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        if (response.Success)
                        {
                            if (response.Result == null)
                            {
                                return true;
                            }
                            else
                            {
                                if (response.Result.GetType() == typeof(ExpandoObject))
                                {
                                    dynamic res = response.Result as ExpandoObject;
                                    return res;
                                }
                                else
                                {
                                    return response.Result;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                });
            }
            catch (Exception) { }
            return new Task<object>(() => false);
        }

        public static async Task Scroll(this ChromiumWebBrowser browser, int scroll, int initDelay = 1000, int endDelay = 1000)
        {
            await Task.Delay(initDelay);
            await browser.ExecuteScript($"window.scrollTo(0, {scroll});");
            await Task.Delay(endDelay);
        }

        //private static bool IsPropertyExist(dynamic dynamicObj, string property)
        //{
        //    try
        //    {
        //        dynamic value = dynamicObj[property].Value;
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public static async Task<bool> Screenshot(this ChromiumWebBrowser browser, string nameScreenshot)
        {
            try
            {
                string directory = Path.Combine(@"Ginnungagap\", God);
                Directory.CreateDirectory(directory); // no need to check if it exists
                Bitmap screenshot = await browser.ScreenshotAsync();
                if (screenshot != null)
                {
                    screenshot.Save($"{directory}\\{nameScreenshot}.jpg");
                    screenshot.Dispose();
                }
                return true;
            }
            catch (Exception) { return false; }
        }


        //public static ChromiumWebBrowser Load()
        //{
        //    //Configure();
        //    browser = new ChromiumWebBrowser(Url);

        //    return browser;
        //}
        public static async Task SoftKill(this IWebBrowser browser)
        {
            try
            {
                //await Task.Delay(500);
                if (!browser.IsDisposed)
                {
                    //await Task.Run(() => Cef.GetGlobalCookieManager().DeleteCookies(url, string.Empty));
                    //  await Task.Run(() => browser.GetBrowser().Reload(true));
                    await Task.Run(() => browser.GetBrowser().CloseBrowser(false));
                    await Task.Run(() => browser.Dispose());
                }
            }
            catch (Exception) { }
        }

        public static async Task Kill(this IWebBrowser browser, string url)
        {
            try
            {
                //await Task.Delay(500);
                if (!browser.IsDisposed)
                {
                    await Task.Run(() => Cef.GetGlobalCookieManager().DeleteCookies(url, string.Empty));
                    await Task.Run(() => browser.GetBrowser().Reload(false));
                    await Task.Run(() => browser.GetBrowser().CloseBrowser(false));
                    await Task.Run(() => browser.Dispose());
                }
            }
            catch (Exception) { }
        }

        public static async Task Kill(this CancellationTokenSource tokenCancel)
        {
            try
            {
                if (tokenCancel != null)
                {
                    await Task.Run(() => tokenCancel.Cancel());
                    await Task.Run(() => tokenCancel.Dispose());
                    await Task.Run(() => tokenCancel = null);
                }
            }
            catch (Exception) { }
        }

        private static int ToKeyValue(char ch)
        {
            return (int)(Keys)ch;
        }

        private static async Task<bool> SetProxy(this IWebBrowser browser, string proxy)
        {
            try
            {
                return await Cef.UIThreadTaskFactory.StartNew(delegate
                {
                    IRequestContext requestContext = browser.GetBrowser().GetHost().RequestContext;
                    Dictionary<string, object> v = new Dictionary<string, object>
                    {
                        ["mode"] = "fixed_servers",
                        ["server"] = proxy
                    };
                    return requestContext.SetPreference("proxy", v, out string errorMessage);
                });
            }
            catch (Exception) { }
            return false;
        }

        private static async Task<bool> DisableImagesLoading(this IWebBrowser browser)
        {
            return await Cef.UIThreadTaskFactory.StartNew(delegate
            {
                IRequestContext requestContext = browser.GetBrowser().GetHost().RequestContext;
                string errorMessage;
                return requestContext.SetPreference("disable-image-loading", true, out errorMessage);
            });
        }

        public static async Task DisableAlerts(this ChromiumWebBrowser browser)
        {
            try
            {
                string disableAlerts = @"addJS_Node (null, null, overrideSelectNativeJS_Functions);
            function overrideSelectNativeJS_Functions()
            {
                window.alert = function alert(message) {
                    console.log(message);
                }
            }

            function addJS_Node(text, s_URL, funcToRun)
            {
                var D = document;
                var scriptNode = D.createElement('script');
                scriptNode.type = 'text/javascript';
                if (text) scriptNode.textContent = text;
                if (s_URL) scriptNode.src = s_URL;
                if (funcToRun) scriptNode.textContent = '(' + funcToRun.toString() + ')()';

                var targ = D.getElementsByTagName('head')[0] || D.body || D.documentElement;
                targ.appendChild(scriptNode);
            }";

                await browser.ExecuteScript(disableAlerts);
            }
            catch (Exception) { }

        }


        public static async Task GetSource(this ChromiumWebBrowser browser, string name = "source")
        {
            try
            {
                string directory = Path.Combine(@"Ginnungagap\", "source");
                Directory.CreateDirectory(directory); // no need to check if it exists
                string source = await browser.GetBrowser().MainFrame.GetSourceAsync();
                string file = $"{directory}\\{name}.html";
                StreamWriter streamWriter = new StreamWriter(file, false, Encoding.Default);
                streamWriter.Write(source);
                streamWriter.Close();
                System.Diagnostics.Process.Start(file);
            }
            catch (Exception) { }
        }

        //public static bool FindElement(this ChromiumWebBrowser browser, string selector)
        //{
        //    string jsScript = "(function(){" +
        //        $"let gbc, X = 0, Y = 0; obj = document.querySelector('{selector}');" +
        //        "if (obj)" +
        //        "{" +
        //            "gbc = obj.getBoundingClientRect();" +
        //            "let centerX = gbc.width / 2;" +
        //            "let centerY = gbc.height / 2;" +
        //            "let X = gbc.x + centerX;" +
        //            "let Y = gbc.y + centerY;" +
        //        "}" +
        //        "return { x: X, y: Y}" +
        //        "})();";

        //    Task<JavascriptResponse> task = browser.EvaluateScriptAsync(jsScript);
        //    task.ContinueWith(t =>
        //    {
        //        if (!t.IsFaulted)
        //        {
        //            if (t.Result.Success && t.Result.Result != null)
        //            {
        //                dynamic response = t.Result.Result as ExpandoObject;

        //                browser.GetBrowser().GetHost().SendMouseClickEvent((int)response.x, (int)response.y, MouseButtonType.Left, false, 1, CefEventFlags.None);
        //                Thread.Sleep(100);
        //                browser.GetBrowser().GetHost().SendMouseClickEvent((int)response.x, (int)response.y, MouseButtonType.Left, true, 1, CefEventFlags.None);
        //            }
        //        }
        //    });
        //    return true;
        //}

        //public static void Click(this bool t)
        //{
        //    browser.GetBrowser().GetHost().SendMouseClickEvent(x, y, MouseButtonType.Left, false, 1, CefEventFlags.None);
        //    Thread.Sleep(100);
        //    browser.GetBrowser().GetHost().SendMouseClickEvent(x, y, MouseButtonType.Left, true, 1, CefEventFlags.None);
        //}

        //public static void SelectElement(this ChromiumWebBrowser browser, string selector, string value, string by = "value")
        //{
        //    string jsScriptSelect = "(function(){" +
        //    $"let el = document.querySelector('{selector}');" +
        //    "var rect = el.getBoundingClientRect();" +
        //    "scrollLeft = window.pageXOffset || document.documentElement.scrollLeft;" +
        //    "scrollTop = window.pageYOffset || document.documentElement.scrollTop;" +
        //    "return { y: rect.top + scrollTop, x: rect.left + scrollLeft }" +
        //    "})();";

        //    var main = browser.GetMainFrame();
        //    Task<JavascriptResponse> taskSelect = main.EvaluateScriptAsync(jsScriptSelect);
        //    taskSelect.ContinueWith(ts =>
        //    {
        //        if (!ts.IsFaulted)
        //        {
        //            if (ts.Result.Success && ts.Result.Result != null)
        //            {
        //                dynamic response = ts.Result.Result as ExpandoObject;

        //                browser.GetBrowser().GetHost().SendMouseClickEvent((int)response.x, (int)response.y, MouseButtonType.Left, false, 1, CefEventFlags.None);
        //                Thread.Sleep(100);
        //                browser.GetBrowser().GetHost().SendMouseClickEvent((int)response.x, (int)response.y, MouseButtonType.Left, true, 1, CefEventFlags.None);
        //            }
        //        }

        //        string jsScriptOption = $"let options = document.querySelectorAll('{selector} > option');" +
        //        "for (let option of options)" +
        //        "{" +
        //            $"if (option.{by} == '{value}')" +
        //            "{" +
        //                "option.setAttribute('selected','true');" +
        //            "}" +
        //        "}";

        //        Task<JavascriptResponse> taskOption = browser.EvaluateScriptAsync(jsScriptOption);
        //        taskOption.ContinueWith(c =>
        //       {
        //           //if (!ts.IsFaulted)
        //           //{ 
        //           //    if (ts.Result.Success)
        //           //    {

        //           //    }
        //           //}
        //       });
        //    });
        //}


        //public static Task LoadPageAsync(this IWebBrowser browser, string address = null)
        //{
        //    var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        //    EventHandler<LoadingStateChangedEventArgs> handler = null;
        //    handler = (sender, args) =>
        //    {
        //        //Wait for while page to finish loading not just the first frame
        //        if (!args.IsLoading)
        //        {
        //            browser.LoadingStateChanged -= handler;
        //            //Important that the continuation runs async using TaskCreationOptions.RunContinuationsAsynchronously
        //            tcs.TrySetResult(true);
        //        }
        //    };

        //    browser.LoadingStateChanged += handler;

        //    if (!string.IsNullOrEmpty(address))
        //    {
        //        browser.Load(address);
        //    }

        //    return tcs.Task;
        //}

        //public static async Task<bool> ConsoleProgressDetail()
        //{
        //    return false;
        //}

    }
}
