using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyImport.Forms
{
    public partial class InputForm : Form
    {

        public string[] Lines { get; set; }

        public InputForm()
            : this("")
        {
        }

        public InputForm(string text)
        {
            InitializeComponent();
            DialogResult = DialogResult.No;
            txtInput.Text = text;
        }

        public void SetReadonly(bool isReadonly)
        {
            txtInput.ReadOnly = isReadonly;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Lines = txtInput.Lines;
            DialogResult = DialogResult.OK;
        }
    }
}
