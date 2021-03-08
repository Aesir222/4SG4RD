using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    public partial class Settings : Form, IForm
    {
        private int Id { set; get; }
        private string Token { set; get; }
        public Settings()
        {
            InitializeComponent();
        }

        public void InitializeParameters(params object[] parameters)
        {
            if (parameters.Length == 2)
            {
                Id = (int)parameters[0];
                Token = parameters[1].ToString();
            }
        }
    }
}
