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
    public partial class Combos : Form, IForm
    {
        private int Id { set; get; }
        private string Token { set; get; }
        public Combos()
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

        private void Combos_Load(object sender, EventArgs e)
        {
            CircularProgressBarGeneral.Minimum = 0;
            CircularProgressBarGeneral.Maximum = 100;
            CircularProgressBarDetail.Minimum = 0;
            CircularProgressBarDetail.Maximum = 100;
            CircularProgressBarGeneral.Value = 0;
            CircularProgressBarDetail.Value = 0;
        }
    }
}
