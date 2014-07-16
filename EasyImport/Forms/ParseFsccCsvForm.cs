using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyImport
{
    public partial class ParseFsccCsvForm : Form
    {
        private ILog _logger = null;
        private ILog Logger
        {
            get
            {
                if(_logger == null)
                {
                    _logger = LogManager.GetLogger(typeof(ParseFsccCsvForm));
                    log4net.Config.XmlConfigurator.Configure();
                }
                return _logger;
            }
        }


        public IList<Tuple<string, string>> Fields { get; private set; }

        public ParseFsccCsvForm()
        {
            InitializeComponent();
        }

        private void btnParseTable_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = txtStructure.Lines;
                Logger.InfoFormat("Going to parse {0} lines of FSCC table structure", lines.Length);

                Fields = new List<Tuple<string, string>>();
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] ar = lines[i].Trim().Split(new char[] { '\t' });
                    if (ar == null || ar.Length != 2)
                    {
                        Logger.ErrorFormat("Line #{0} is incorrect: {1}", i+1, lines[i]);
                        throw new ValidateFailedException("Line #" + (i+1) + " is incorrect");
                    }
                    Fields.Add(new Tuple<string, string>(ar[0].Trim(), ar[1].Trim()));
                }
                Logger.Debug("  done");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Error parsing FSCC table structure");
                throw;
            }
        }
    }
}
