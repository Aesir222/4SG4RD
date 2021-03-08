using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asgard
{
    class MockAutoCompleteSource :AutoCompleteSource
    {
        public MockAutoCompleteSource(TextBox textBox):base(textBox)
        {

        }

        protected override List<string> GetAutocompleteItems()
        {
            throw new NotImplementedException();
        }
    }
}
