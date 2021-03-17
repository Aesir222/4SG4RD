using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using PasswordGenerator;
using System.Windows.Navigation;
using System.CodeDom.Compiler;
using System.Threading;

namespace Asgard
{
    public static class Asatru
    {
        private static string Bin { get; set; }
        const string API = "http://api.4sg4rd.club/";
        //const string API = "http://test.4sg4rd.club/";
        //const string API = "http://localhost/test.jwt.api/";
        public static readonly string[] blackList = new[] { "" };
        //public static readonly string[] blackList = new[] {
        //             "Wireshark",
        //             //"Proxifier",
        //             "widecap",
        //             "pcapui",
        //             "FreeCAD",
        //             "KKCap",
        //             "cheatengine-x86_64-SSE4-AVX2",
        //             "OMATIC",
        //             "psiphon-115",
        //             //"devenv",
        //             "jgrasp32",
        //             "jgrasp64",
        //             "Netbeans32",
        //             "Netbeans64",
        //             "idea32",
        //             "idea64",
        //             "eclipse",
        //             "jgrasp",
        //             "Squalr",
        //             "Code",
        //             "dnSpy",
        //             "MagickChecker",
        //             "MagickInstall",
        //             "SpaceChecker",
        //             "chromedriver",
        //             "Advanced_IP_Scanner_2.5.3850",
        //             "GlassWire",
        //             "WeModAuxiliaryService",
        //             "WeMod",
        //             "BlueJ",
        //             //"cmd"
        //         };//Black List Applications
        public static String GetTimestamp(DateTime value) { return value.ToString("HH_mm_ss_ffff"); }
        public static string VarDump(this object obj, int recursion)
        {
            StringBuilder result = new StringBuilder();
            // Protect the method against endless recursion
            if (recursion < 5)
            {
                // Determine object type
                Type t = obj.GetType();
                // Get array with properties for this object
                PropertyInfo[] properties = t.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        // Get the property value
                        object value = property.GetValue(obj, null);
                        // Create indenting string to put in front of properties of a deeper level
                        // We'll need this when we display the property name and value
                        string indent = String.Empty;
                        string spaces = "|   ";
                        string trail = "|...";
                        if (recursion > 0)
                        {
                            indent = new StringBuilder(trail).Insert(0, spaces, recursion - 1).ToString();
                        }
                        if (value != null)
                        {
                            // If the value is a string, add quotation marks
                            string displayValue = value.ToString();
                            if (value is string) displayValue = String.Concat('"', displayValue, '"');
                            // Add property name and value to return string
                            result.AppendFormat("{0}{1} = {2}\n", indent, property.Name, displayValue);
                            try
                            {
                                if (!(value is ICollection))
                                {
                                    // Call var_dump() again to list child properties
                                    // This throws an exception if the current property value
                                    // is of an unsupported type (eg. it has not properties)
                                    result.Append(VarDump(value, recursion + 1));
                                }
                                else
                                {
                                    // 2009-07-29: added support for collections
                                    // The value is a collection (eg. it's an arraylist or generic list)
                                    // so loop through its elements and dump their properties
                                    int elementCount = 0;
                                    foreach (object element in ((ICollection)value))
                                    {
                                        string elementName = String.Format("{0}[{1}]", property.Name, elementCount);
                                        indent = new StringBuilder(trail).Insert(0, spaces, recursion).ToString();
                                        // Display the collection element name and type
                                        result.AppendFormat("{0}{1} = {2}\n", indent, elementName, element.ToString());
                                        // Display the child properties
                                        result.Append(VarDump(element, recursion + 2));
                                        elementCount++;
                                    }
                                    result.Append(VarDump(value, recursion + 1));
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            // Add empty (null) property to return string
                            result.AppendFormat("{0}{1} = {2}\n", indent, property.Name, "null");
                        }
                    }
                    catch
                    {
                        // Some properties will throw an exception on property.GetValue()
                        // I don't know exactly why this happens, so for now i will ignore them...
                    }
                }
            }
            return result.ToString();
        }

        public static async Task<bool> SocketConnect(string host, int port)
        {
            bool is_success = false;
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 200);
                await Task.Delay(500);
                IPAddress ipAddress = IPAddress.Parse(host);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
                socket.Connect(ipEndPoint);
                if (socket.Connected)
                {
                    is_success = true;
                }
                socket.Close();
            }
            catch (Exception)
            {
                is_success = false;
            }
            return is_success;
        }

        public static void CustomFont()
        {
            //Create your private font collection object.
            PrivateFontCollection pfc = new PrivateFontCollection();

            //Select your font from the resources.
            //My font here is "Digireu.ttf"
            int fontLength = Properties.Resources.CaviarDreams.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.CaviarDreams;

            // create an unsafe memory block for the font data
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);
        }


        public static async Task<object> InfoBin(string bin)
        {
            try
            {
                string urlBinList = "https://lookup.binlist.net/" + bin;
                string result = await RequestGet(urlBinList);
                //return result;
                if (!String.IsNullOrEmpty(result))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    object listBinList = serializer.Deserialize(result, typeof(BinList));
                    return listBinList;
                }
            }
            catch (Exception) { }
            return null; ;
        }

        private static async Task<string> RequestGet(string url)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Method = "GET";
                HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                string result = null;

                using (Stream stream = httpWebResponse.GetResponseStream())
                {
                    StreamReader streamReader = new StreamReader(stream);
                    result = await Task.Run(() => streamReader.ReadToEnd());
                    streamReader.Close();
                }
                return result;
            }
            catch (Exception) { }
            return string.Empty;
        }

        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static string KeyChar(int value)
        {
            if (value > 47 && value < 58)
            {
                int x = 0;
                for (int i = 48; i < 58; i++)
                {
                    if (i != value)
                    {
                        x++;
                    }
                }
                return x.ToString();
            }

            if (value > 95 && value < 106)
            {
                int x = 0;
                for (int i = 96; i < 105; i++)
                {
                    if (i != value)
                    {
                        x++;
                    }
                }
                return x.ToString();
            }
            return string.Empty;
        }

        private static void SetupClickEvents(Control container)
        {
            foreach (Control control in container.Controls)
            {
                control.Click += HandleClicks;
            }
        }
        private static void HandleClicks(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            //MessageBox.Show(string.Format("{0} was clicked!", control.Name));
        }


        public static string CreateMD5(this string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static async Task<string> GenerateToken(string usernameOrEmail, string password)
        {
            try
            {
                string name = "generateToken";
                object parameters = new { usernameOrEmail, password };
                object jsonObject = new { name, parameters };
                string json = JsonConvert.SerializeObject(jsonObject);
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                //MessageBox.Show(restResponse.Content);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);

                if (response != null)
                {
                    if (response.success.status == "200" && response.success.result.token != null)
                    {
                        return response.success.result.token;
                    }
                    else
                    {
                        if (response.success.status == "108")
                        {
                            return response.success.result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception) { }
            return null;
        }


        public static async Task<bool> AddUser(string username, string email, string password, string usernameRefer)
        {
            try
            {
                string name = "addUser";
                object parameters = new { username, email, password, roleId = 5, usernameRefer };

                string json = JsonConvert.SerializeObject(new { name, parameters });
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                //MessageBox.Show(restResponse.Content);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);

                if (response.success.status == "200" && response.success.result.Value == "Inserted successfully.")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception) { }
            return false;
        }

        public static async Task<dynamic> GetWelcome(int id, string token)
        {
            try
            {
                return new
                {
                    userDetails = await GetUserDetails(id, token),
                    lastSession = await GetLastSession(id, token),
                    planDetails = await GetPlanDetails(id, token),
                    valhalla = await GetCountValhalla(id, token),
                    helheim = await GetCountHelheim(id, token)
                };
            }
            catch (Exception) { }
            return null;
        }

        public static async Task<bool> SetValhalla(int userId, string token, int gateId, string cC, bool free = false)
        {
            string name = "setValhalla";
            object parameters = new { gateId, userId, cC, free };
            string json = JsonConvert.SerializeObject(new { name, parameters });
            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result.Value == "Inserted successfully.")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> SetJotunheim(int userId, string token, int gateId, string cC)
        {
            string name = "setJotunheim";
            object parameters = new { gateId, userId, cC };
            string json = JsonConvert.SerializeObject(new { name, parameters });
            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result.Value == "Inserted successfully.")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> DiscountJotun(int userId, string token, int gateId)
        {
            string name = "discountJotun";
            object parameters = new { gateId, userId };
            string json = JsonConvert.SerializeObject(new { name, parameters });
            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result.Value == "Updated successfully.")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> SetHelheim(int userId, string token, int gateId, string cC)
        {
            string name = "setHelheim";
            object parameters = new { gateId, userId, cC };
            string json = JsonConvert.SerializeObject(new { name, parameters });

            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result.Value == "Inserted successfully.")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static async Task<dynamic> GetUserDetails(int id, string token)
        {
            string name = "getUserDetails";
            object parameters = new { id };
            string json = JsonConvert.SerializeObject(new { name, parameters });
            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result != null)
                {
                    return response.success.result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static async Task<dynamic> GetPlanDetails(int userId, string token)
        {
            string name = "getPlanDetails";
            object parameters = new { userId };
            string json = JsonConvert.SerializeObject(new { name, parameters });

            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result.message == null)
                {
                    return response.success.result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static async Task<dynamic> GetCountValhalla(int createBy, string token)
        {
            string name = "getCountValhalla";
            object parameters = new { createBy };
            string json = JsonConvert.SerializeObject(new { name, parameters });

            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result != null)
                {
                    return response.success.result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static async Task<dynamic> GetCountHelheim(int createBy, string token)
        {
            string name = "getCountHelheim";
            object parameters = new { createBy };
            string json = JsonConvert.SerializeObject(new { name, parameters });

            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result != null)
                {
                    return response.success.result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static async Task<dynamic> GetLastValhalla(int createBy, string token)
        {
            string name = "getLastValhalla";
            object parameters = new { createBy };
            string json = JsonConvert.SerializeObject(new { name, parameters });

            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result != null)
                {
                    return response.success.result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static async Task<bool> GetPlanVIP(int userId, string token)
        {
            string name = "getPlanVIP";
            object parameters = new { userId };
            string json = JsonConvert.SerializeObject(new { name, parameters });

            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result.message == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> GenerateNewPassword(string usernameOrEmail)
        {
            dynamic user = await VerifyUsernameOrEmail(usernameOrEmail);
            if (user != null)
            {
                int id = int.Parse(user.id.Value);
                string username = user.username.Value.ToString();
                string email = user.email.Value.ToString();
                string newPassword = new PasswordGenerator.Password(8).Next();
                bool setNewPassword = await SetNewPassword(id, newPassword);
                if (setNewPassword)
                {
                    string htmlEmail = @"<!doctype html>
<html>
  <head>
    <meta name='viewport' content='width=device-width' />
    <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
    <title>4SG4RD CLUB</title>
    <style>
      /* -------------------------------------
          GLOBAL RESETS
      ------------------------------------- */
      
      /*All the styling goes here*/
      
      img {
        border: none;
        -ms-interpolation-mode: bicubic;
        max-width: 100%; 
      }

      body {
        background-color: #f6f6f6;
        font-family: sans-serif;
        -webkit-font-smoothing: antialiased;
        font-size: 14px;
        line-height: 1.4;
        margin: 0;
        padding: 0;
        -ms-text-size-adjust: 100%;
        -webkit-text-size-adjust: 100%; 
      }

      table {
        border-collapse: separate;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        width: 100%; }
        table td {
          font-family: sans-serif;
          font-size: 14px;
          vertical-align: top; 
      }

      /* -------------------------------------
          BODY & CONTAINER
      ------------------------------------- */

      .body {
        background-color: #f6f6f6;
        width: 100%; 
      }

      /* Set a max-width, and make it display as block so it will automatically stretch to that width, but will also shrink down on a phone or something */
      .container {
        display: block;
        margin: 0 auto !important;
        /* makes it centered */
        max-width: 580px;
        padding: 10px;
        width: 580px; 
      }

      /* This should also be a block element, so that it will fill 100% of the .container */
      .content {
        box-sizing: border-box;
        display: block;
        margin: 0 auto;
        max-width: 580px;
        padding: 10px; 
      }

      /* -------------------------------------
          HEADER, FOOTER, MAIN
      ------------------------------------- */
      .main {
        background: #ffffff;
        border-radius: 3px;
        width: 100%; 
      }

      .wrapper {
        box-sizing: border-box;
        padding: 20px; 
      }

      .content-block {
        padding-bottom: 10px;
        padding-top: 10px;
      }

      .footer {
        clear: both;
        margin-top: 10px;
        text-align: center;
        width: 100%; 
      }
        .footer td,
        .footer p,
        .footer span,
        .footer a {
          color: #999999;
          font-size: 12px;
          text-align: center; 
      }

      /* -------------------------------------
          TYPOGRAPHY
      ------------------------------------- */
      h1,
      h2,
      h3,
      h4 {
        color: #000000;
        font-family: sans-serif;
        font-weight: 400;
        line-height: 1.4;
        margin: 0;
        margin-bottom: 30px; 
      }

      h1 {
        font-size: 35px;
        font-weight: 300;
        text-align: center;
        text-transform: capitalize; 
      }

      p,
      ul,
      ol {
        font-family: sans-serif;
        font-size: 14px;
        font-weight: normal;
        margin: 0;
        margin-bottom: 15px; 
      }
        p li,
        ul li,
        ol li {
          list-style-position: inside;
          margin-left: 5px; 
      }

      a {
        color: #3498db;
        text-decoration: underline; 
      }

      /* -------------------------------------
          BUTTONS
      ------------------------------------- */
      .btn {
        box-sizing: border-box;
        width: 100%; }
        .btn > tbody > tr > td {
          padding-bottom: 15px; }
        .btn table {
          width: auto; 
      }
        .btn table td {
          background-color: #ffffff;
          border-radius: 5px;
          text-align: center; 
      }
        .btn a {
          background-color: #ffffff;
          border: solid 1px #3498db;
          border-radius: 5px;
          box-sizing: border-box;
          color: #3498db;
          cursor: pointer;
          display: inline-block;
          font-size: 14px;
          font-weight: bold;
          margin: 0;
          padding: 12px 25px;
          text-decoration: none;
          text-transform: capitalize; 
      }

      .btn-primary table td {
        background-color: #3498db; 
      }

      .btn-primary a {
        background-color: #3498db;
        border-color: #3498db;
        color: #ffffff; 
      }

      /* -------------------------------------
          OTHER STYLES THAT MIGHT BE USEFUL
      ------------------------------------- */
      .last {
        margin-bottom: 0; 
      }

      .first {
        margin-top: 0; 
      }

      .align-center {
        text-align: center; 
      }

      .align-right {
        text-align: right; 
      }

      .align-left {
        text-align: left; 
      }

      .clear {
        clear: both; 
      }

      .mt0 {
        margin-top: 0; 
      }

      .mb0 {
        margin-bottom: 0; 
      }

      .preheader {
        color: transparent;
        display: none;
        height: 0;
        max-height: 0;
        max-width: 0;
        opacity: 0;
        overflow: hidden;
        mso-hide: all;
        visibility: hidden;
        width: 0; 
      }

      .powered-by a {
        text-decoration: none; 
      }

      hr {
        border: 0;
        border-bottom: 1px solid #f6f6f6;
        margin: 20px 0; 
      }

      /* -------------------------------------
          RESPONSIVE AND MOBILE FRIENDLY STYLES
      ------------------------------------- */
      @media only screen and (max-width: 620px) {
        table[class=body] h1 {
          font-size: 28px !important;
          margin-bottom: 10px !important; 
        }
        table[class=body] p,
        table[class=body] ul,
        table[class=body] ol,
        table[class=body] td,
        table[class=body] span,
        table[class=body] a {
          font-size: 16px !important; 
        }
        table[class=body] .wrapper,
        table[class=body] .article {
          padding: 10px !important; 
        }
        table[class=body] .content {
          padding: 0 !important; 
        }
        table[class=body] .container {
          padding: 0 !important;
          width: 100% !important; 
        }
        table[class=body] .main {
          border-left-width: 0 !important;
          border-radius: 0 !important;
          border-right-width: 0 !important; 
        }
        table[class=body] .btn table {
          width: 100% !important; 
        }
        table[class=body] .btn a {
          width: 100% !important; 
        }
        table[class=body] .img-responsive {
          height: auto !important;
          max-width: 100% !important;
          width: auto !important; 
        }
      }

      /* -------------------------------------
          PRESERVE THESE STYLES IN THE HEAD
      ------------------------------------- */
      @media all {
        .ExternalClass {
          width: 100%; 
        }
        .ExternalClass,
        .ExternalClass p,
        .ExternalClass span,
        .ExternalClass font,
        .ExternalClass td,
        .ExternalClass div {
          line-height: 100%; 
        }
        .apple-link a {
          color: inherit !important;
          font-family: inherit !important;
          font-size: inherit !important;
          font-weight: inherit !important;
          line-height: inherit !important;
          text-decoration: none !important; 
        }
        #MessageViewBody a {
          color: inherit;
          text-decoration: none;
          font-size: inherit;
          font-family: inherit;
          font-weight: inherit;
          line-height: inherit;
        }
        .btn-primary table td:hover {
          background-color: #34495e !important; 
        }
        .btn-primary a:hover {
          background-color: #34495e !important;
          border-color: #34495e !important; 
        } 
      }

    </style>
  </head>
  <body class=''>
    <span class='preheader'>This is preheader text. Some clients will show this text as a preview.</span>
    <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='body'>
      <tr>
        <td>&nbsp;</td>
        <td class='container'>
          <div class='content'>

            <!-- START CENTERED WHITE CONTAINER -->
            <table role='presentation' class='main'>

              <!-- START MAIN CONTENT AREA -->
              <tr>
                <td class='wrapper'>
                  <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                    <tr>
                      <td>
                        <p>Hola Asgardiano,</p>
                        <p>Ahora eres bienvenido a 4sg4rd.</p>
                        <table role='presentation' border='0' cellpadding='0' cellspacing='0' class='btn btn-primary'>
                          <tbody>
                            <tr>
                              <td align='left'>
                                <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                                  <tbody>
                                    <tr>

                                    </tr>
                                  </tbody>
                                </table>
                              </td>
                            </tr>
                          </tbody>
                        </table>
                        <p>" + $"Su nueva contraseña:<br/> {newPassword}" + @"</p>
                        <p>Good luck! Hope it works.</p>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </table>
            <!-- END CENTERED WHITE CONTAINER -->

            <!-- START FOOTER -->
            <div class='footer'>
              <table role='presentation' border='0' cellpadding='0' cellspacing='0'>
                <tr>
                  <td class='content-block'>
                    <span class='apple-link'>4SG4RD T34M</span>

                  </td>
                </tr>
                <tr>
                  <td class='content-block powered-by'>
                  </td>
                </tr>
              </table>
            </div>
            <!-- END FOOTER -->

          </div>
        </td>
        <td>&nbsp;</td>
      </tr>
    </table>
  </body>
</html>
";
                    if (await SendEmail(email, username, "4SG4RD CLUB nueva contraseña", htmlEmail))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static async Task<dynamic> VerifyUsernameOrEmail(string usernameOrEmail)
        {
            try
            {
                string name = "verifyUsernameOrEmail";
                object parameters = new { usernameOrEmail };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);

                if (response.success.status == "200" && response.success.result != null)
                {
                    return response.success.result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception) { }
            return null;
        }


        public static async Task<bool> SetNewPassword(int id, string password)
        {
            try
            {
                string name = "setNewPassword";
                object parameters = new { id, password = password.CreateMD5() };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);

                if (response.success.status == "200" && response.success.result == "Set new password successfully.")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception) { }
            return false;
        }

        public static async Task<bool> SetDisUser(int id, string processName)
        {
            try
            {
                string name = "disUser";
                object parameters = new { id, processName };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);

                if (response.success.status == "200" && response.success.result == "Disable successfully.")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception) { }
            return false;
        }

        public static async Task<bool> BlockAesir(int userId, int gateId, string token)
        {
            try
            {
                string name = "blockAesir";
                object parameters = new { userId, gateId };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                dynamic response = await Api(json, token);

                if (response != null)
                {
                    if (response.success.status == "200")
                    {
                        if (response.success.result.message == "Blocked")
                        {
                            return true;
                        }
                        else if (response.success.result.message == "NotBlocked")
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception) { }
            return false;
        }

        public static async Task<bool> BlockJotun(int userId, int gateId, string token)
        {
            try
            {
                string name = "blockAesir";
                object parameters = new { userId, gateId };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                dynamic response = await Api(json, token);

                if (response != null)
                {
                    if (response.success.status == "200")
                    {
                        if (response.success.result.message == "Blocked")
                        {
                            return true;
                        }
                        else if (response.success.result.message == "NotBlocked")
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception) { }
            return false;
        }

        public static async Task<dynamic> GetGates(int userId, string token)
        {
            try
            {
                string name = "getGates";
                object parameters = new { userId };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                dynamic response = await Api(json, token);

                if (response != null)
                {
                    if (response.success.status == "200" && response.success.result != null)
                    {
                        return response.success.result;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception) { }
            return null;
        }

        public static async Task<bool> TakenUsername(string username)
        {
            try
            {
                string name = "takenUsername";
                object parameters = new { username };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);

                if (response != null)
                {
                    if (response.success.status == "200")
                    {
                        if (response.success.result.message == "Taken")
                        {
                            return true;
                        }
                        else if (response.success.result.message == "NotTaken")
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception) { }
            return false;
        }


        public static async Task<bool> TakenEmail(string email)
        {
            try
            {
                string name = "takenEmail";
                object parameters = new { email };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);

                if (response != null)
                {
                    if (response.success.status == "200")
                    {
                        if (response.success.result.message == "Taken")
                        {
                            return true;
                        }
                        else if (response.success.result.message == "NotTaken")
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception) { }
            return false;
        }

        public static async Task<dynamic> RequestPLan(string token, int userId, int planId)
        {
            string name = "requestPLan";
            object parameters = new { userId, planId };
            string json = JsonConvert.SerializeObject(new { name, parameters });
            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result.Value != "Failed to Insert.")
                {
                    return response.success.result.Value;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public static async Task<dynamic> Referred(int roleId)
        {
            try
            {
                string name = "referred";
                object parameters = new { roleId };
                string json = JsonConvert.SerializeObject(new { name, parameters });
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                //MessageBox.Show(restResponse.Content);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);

                if (response.success.status == "200" && response.success.result != null)
                {
                    //if (response.success.result != "Not refrred")
                    //{
                    return response.success.result;
                    //}
                    //else
                    //{
                    //    return null;
                    //}
                }
                else
                {
                    return null;
                }
            }
            catch (Exception) { throw; }
            //return null;
        }

        public static async Task<dynamic> Api(string json, string token)
        {
            try
            {
                RestClient client = new RestClient(API);
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                //if (testc)
                //{
                //MessageBox.Show(json + "\n\r" + "---" + "\n\r" + restResponse.Content);
                //}

                return JsonConvert.DeserializeObject(restResponse.Content);
            }
            catch (Exception) { }
            return null;
        }



        /// <summary>
        /// Gets the MAC address of the current PC.
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            string mac = string.Empty;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //MessageBox.Show(nic.Description);
                if (nic.OperationalStatus == OperationalStatus.Up /*&& (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo") && !nic.Description.Contains("Tunnel") && !nic.Description.Contains("Adapter"))*/)
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        PhysicalAddress address = nic.GetPhysicalAddress();
                        byte[] bytes = address.GetAddressBytes();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            // Display the physical address in hexadecimal. 
                            mac += bytes[i].ToString("X2");
                            // Insert a hyphen after each byte, unless we are at the end of the address. 
                            if (i != bytes.Length - 1)
                            {
                                mac += "-";
                            }
                        }
                    }
                }
            }
            return mac;
        }

        public static string GetIpAddress()
        {
            return new WebClient().DownloadString("http://icanhazip.com");
        }

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        public static async Task<dynamic> GetDataIp()
        {
            try
            {
                RestClient client = new RestClient("http://ip-api.com/json?fields=status,message,country,countryCode,region,regionName,city,zip,lat,lon,timezone,isp,org,asname,query");
                client.Timeout = -1;
                RestRequest request = new RestRequest(Method.POST);
                IRestResponse restResponse = await client.ExecuteAsync(request);
                dynamic response = JsonConvert.DeserializeObject(restResponse.Content);
                //MessageBox.Show(response.status.Value);
                if (response != null)
                {
                    if (response.status.Value == "success")
                    {
                        return response;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception) { }
            return null;
        }

        public static async Task<bool> CreateSession(int userId, string token)
        {
            try
            {
                //MessageBox.Show("OK");
                dynamic dataIp = await GetDataIp();
                //MessageBox.Show(dataIp.query.Value);
                // string mac = GetMacAddress();
                string mac = string.Empty;
                //MessageBox.Show(mac);
                string machine = GetHostName();
                //MessageBox.Show(machine);
                //MessageBox.Show(userId.ToString());


                if (dataIp != null)
                {
                    string name = "createSession";

                    object parameters = new
                    {
                        ip = dataIp.query,
                        mac,
                        machine,
                        country = dataIp.country,
                        countryCode = dataIp.countryCode,
                        region = dataIp.region,
                        regionName = dataIp.regionName,
                        city = dataIp.city,
                        zip = dataIp.zip,
                        lat = dataIp.lat,
                        lon = dataIp.lon,
                        timezone = dataIp.timezone,
                        isp = dataIp.isp,
                        org = dataIp.org,
                        asn = dataIp.asname,
                        userId
                    };

                    string json = JsonConvert.SerializeObject(new { name, parameters });
                    //MessageBox.Show("jSon: " + json);
                    //MessageBox.Show("Token: " + token);
                    dynamic response = await Api(json, token);

                    if (response != null)
                    {
                        if (response.success.status == "200" && response.success.result == "Insert successfully.")
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception) { }
            return false;
        }

        public static async Task<dynamic> GetLastSession(int userId, string token)
        {
            string name = "getLastSession";
            object parameters = new { userId };
            string json = JsonConvert.SerializeObject(new { name, parameters });

            dynamic response = await Api(json, token);

            if (response != null)
            {
                if (response.success.status == "200" && response.success.result != null)
                {
                    return response.success.result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static string[] DynamicToArray(dynamic data)
        {
            string[] x = data.GetType().IsArray ? data as string[] : new string[] { data };
            return x;
        }
        public static bool ValidateEmail(String email)
        {
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expression))
            {
                if (Regex.Replace(email, expression, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static async Task<bool> SendEmail(string email, string username, string subject, string body)
        {
            try
            {
                MailAddress fromAddress = new MailAddress("4sg4rdclub@gmail.com", "4SG4RD CLUB");
                MailAddress toAddress = new MailAddress(email, username);
                const string fromPassword = "dzN#jcmk717PT4YfYE47";

                SmtpClient smtp = new SmtpClient
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };

                using (MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    await smtp.SendMailAsync(message);
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }

        public static string FormatDate(string date)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", null);
            return dateTime.ToString("dd MMM HH:mm");
        }

        public static Task OnClickAsync(this Control source)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            source.Click += OnClick;
            return tcs.Task;

            void OnClick(object sender, EventArgs e)
            {
                tcs.SetResult(true);
            }
        }

        /// <summary>
        /// Compute a value for a long time.
        /// </summary>
        /// <returns>The value computed.</returns>
        /// <param name="loop">Number of iterations to do.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        private static Task<decimal> LongRunningCancellableOperation(int loop, CancellationToken cancellationToken)
        {
            Task<decimal> task = null;

            // Start a task and return it
            task = Task.Run(() =>
            {
                decimal result = 0;

                // Loop for a defined number of iterations
                for (int i = 0; i < loop; i++)
                {
                    // Check if a cancellation is requested, if yes,
                    // throw a TaskCanceledException.

                    if (cancellationToken.IsCancellationRequested)
                        throw new TaskCanceledException(task);

                    // Do something that takes times like a Thread.Sleep in .NET Core 2.
                    Thread.Sleep(10);
                    result += i;
                }

                return result;
            });

            return task;
        }

    }
}
