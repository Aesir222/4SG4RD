using CefSharp;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asgard
{
    internal class CookieMonster : ICookieVisitor
    {
            readonly List<Tuple<string, string>> cookies = new List<Tuple<string, string>>();
            readonly Action<IEnumerable<Tuple<string, string>>> useAllCookies;

            public CookieMonster(Action<IEnumerable<Tuple<string, string>>> useAllCookies)
            {
                this.useAllCookies = useAllCookies;
            }

            public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
            {
                cookies.Add(new Tuple<string, string>(cookie.Name, cookie.Domain));

                if (count == total - 1)
                    useAllCookies(cookies);

                return true;
            }
        public void Dispose()
        {
        }
    }
}