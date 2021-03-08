using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    class BinList
    {
        public Number number { get; set; }
        public string scheme { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public bool prepaid { get; set; }
        public Country country { get; set; }
        public Bank bank { get; set; }
    }

    class Number
    {
        public int length { get; set; }
        public bool luhn { get; set; }
    }

    class Country
    {
        public string numeric { get; set; }
        public string alpha2 { get; set; }
        public string name { get; set; }
        public string emoji { get; set; }
        public string currency { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
    }

    class Bank
    {
        public string name { get; set; }
        public string url { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
    }
    class Api
    {
        public string name { set; get; }
        public Parameters parameters { set; get; }

    }

    class Parameters
    {
        public string user { set; get; }
        public string password { set; get; }
        public string id { set; get; }
    }

    class Response
    {
        public Success success { set; get; }
    }

    class Success
    {
        public int status { set; get; }
        public Result result { set; get; }
    }

    class Result
    {
        public string token { set; get; }
    }

    class Throw
    {
        public Error error { set; get; }
    }
    class Error
    {
        public int status { set; get; }
        public string message { set; get; }
    }
}
