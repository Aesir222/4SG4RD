using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Asgard
{
    public abstract class AutoCompleteSource
    {
        private TextBox mTextBox;
        private AutoCompleteMode mAutoCompleteMode;

        public AutoCompleteSource(TextBox textBox) : 
            this(textBox, AutoCompleteMode.Suggest) { }

        public AutoCompleteSource(TextBox textBox, AutoCompleteMode mode)
        {
            if (textBox == null)
            {
                throw new ArgumentNullException("textbox");
            }

            if (textBox.IsDisposed)
            {
                throw new ArgumentException("textbox");
            }

            mTextBox = textBox;
            mAutoCompleteMode = mode;

            mTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            BackgroundWorker autoCompleteLoader = new BackgroundWorker();
            autoCompleteLoader.DoWork += new DoWorkEventHandler(AutoCompleteLoader_DoWork);
            autoCompleteLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AutoCompleteLoader_RunWorkerCompleted);
            autoCompleteLoader.RunWorkerAsync();

        }

        private void AutoCompleteLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AutoCompleteStringCollection collection = e.Result as AutoCompleteStringCollection;
            if (collection == null)
            {
                return;
            }

            if (mTextBox.InvokeRequired)
            {
                mTextBox.Invoke(new SetAutocompleteSource(DoSetAutoCompleteSource),  new object[] { collection });
            }
            else
            {
                DoSetAutoCompleteSource(collection);
            }
        }

        private void DoSetAutoCompleteSource(AutoCompleteStringCollection collection)
        {
            if (mTextBox.IsDisposed)
            {
                return;
            }

            mTextBox.AutoCompleteMode = mAutoCompleteMode;
            mTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            mTextBox.AutoCompleteCustomSource = collection; 
        }

        private void AutoCompleteLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> autoCompleItems = GetAutocompleteItems();
            if (autoCompleItems == null)
            {
                return;
            }

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            collection.AddRange(GetAutocompleteItems().ToArray());
            e.Result = collection;
        }
        protected abstract List<string> GetAutocompleteItems();

        //protected override List<string> GetAutoCompleteReferred()
        //{
            //List<string> result = new List<string>();
            //dynamic referred = await Asatru.Referred(Guid.NewGuid().ToString());
            //if (referred != null)
            //{
            //    foreach (var r in referred)
            //    {
            //        result.Add(r.username.Value);
            //    }
            //    return Task.result;
            //}
        //}
    }
    internal delegate void SetAutocompleteSource(AutoCompleteStringCollection collection);
}
