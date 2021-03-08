using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asgard
{
    interface IForm
    {
        void InitializeParameters(params object[] parameters);
    }
}
