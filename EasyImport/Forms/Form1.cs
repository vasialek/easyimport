using EasyImport.DataReader;
using EasyImport.Forms;
using EasyImport.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyImport
{
    public partial class Form1 : Form
    {

        private TableListViewModel _tablesList = new TableListViewModel();

        private ILog _logger = null;
        private ILog Logger
        {
            get
            {
                if(_logger == null)
                {
                    _logger = LogManager.GetLogger("Form");
                    log4net.Config.XmlConfigurator.Configure();
                }
                return _logger;
            }
        }

        #region " Get/set text methods (invoked) "

        delegate void SetTextDelegate(string name, string value);
        delegate string GetTextDelegate(string name);

        /// <summary>
        /// Returns text of control.
        /// </summary>
        /// <param name="name">Name of control</param>
        /// <returns>Text of control. Returns "checked"/"" or "" for CheckBox</returns>
        private string GetText(string name)
        {
            string s = "";

            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new GetTextDelegate(GetText), name);
                }
                else
                {
                    Control[] ar = this.Controls.Find(name, false);
                    if ((ar != null) && (ar.Length > 0))
                    {
                        if (ar[0].GetType() == typeof(CheckBox))
                        {
                            s = ((CheckBox)ar[0]).Checked ? "checked" : "";
                        }
                        else
                        {
                            s = ar[0].Text;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return s;
        }

        /// <summary>
        /// Thread safe AddText
        /// </summary>
        /// <param name="name">Name of control to add text</param>
        /// <param name="value">Text to add</param>
        private void AddText(string name, string value)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new SetTextDelegate(AddText), name, value);
                }
                else
                {
                    Control[] ar = this.Controls.Find(name, false);
                    if ((ar != null) && (ar.Length > 0))
                    {
                        ar[0].Text += value;
                        if (ar[0].GetType() == typeof(TextBox))
                        {
                            ((TextBox)ar[0]).SelectionStart = Int32.MaxValue;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Thread safe SetText
        /// </summary>
        /// <param name="name">Name of control to set text</param>
        /// <param name="value">Text to set</param>
        private void SetText(string name, string value)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new SetTextDelegate(SetText), name, value);
                }
                else
                {
                    Control[] ar = this.Controls.Find(name, false);
                    if ((ar != null) && (ar.Length > 0))
                    {
                        ar[0].Text = value;
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region " Debug to UI console "

        protected enum LogImportance { No, Debug, Info, Warn, Error }

        protected delegate void DebugDelegate(LogImportance level, string msg, params object[] args);

        /// <summary>
        /// Outputs message to UI output console
        /// </summary>
        /// <param name="msg">Message to input, could be used like string.Format()</param>
        /// <param name="args"></param>
        protected void Debug(string msg, params object[] args)
        {
            
            Debug(LogImportance.Info, msg, args);
        }

        /// <summary>
        /// Outputs message to UI output console
        /// </summary>
        /// <param name="level">Level of imortance - Error, Info, etc...</param>
        /// <param name="msg">Message to input, could be used like string.Format()</param>
        /// <param name="args"></param>
        protected void Debug(LogImportance level, string msg, params object[] args)
        {
            if (InvokeRequired)
            {
                Invoke(new DebugDelegate(Debug), level, msg, args);
            }else
            {
                StringBuilder sb = new StringBuilder();
                if (level != LogImportance.No)
                {
                    sb.AppendFormat("[{0}] ", level.ToString().ToUpper());
                }

                switch (level)
                {
                    case LogImportance.No:
                        break;
                    case LogImportance.Debug:
                        if (Logger.IsDebugEnabled)
                        {
                            Logger.DebugFormat(msg, args);
                        }
                        break;
                    case LogImportance.Info:
                        if (Logger.IsInfoEnabled)
                        {
                            Logger.InfoFormat(msg, args);
                        }
                        break;
                    case LogImportance.Warn:
                        if (Logger.IsWarnEnabled)
                        {
                            Logger.WarnFormat(msg, args);
                        }
                        break;
                    case LogImportance.Error:
                        if (Logger.IsErrorEnabled)
                        {
                            Logger.ErrorFormat(msg, args);
                        }
                        break;
                }

                sb.AppendFormat(msg, args);
                sb.AppendLine();
                this.AddText("txtOutput", sb.ToString());
                txtOutput.SelectionStart = int.MaxValue;
                txtOutput.ScrollToCaret(); 
            }
        }

        #endregion

        delegate void _SetControlEnabledDelegate(Control parent, string name, bool isEnabled);

        protected void SetControlEnabled(Control parent, string name, bool isEnabled)
        {
            if (InvokeRequired)
            {
                Invoke(new _SetControlEnabledDelegate(SetControlEnabled), parent, name, isEnabled);
            }
            else
            {
                Control[] ar = parent.Controls.Find(name, false);
                if (ar != null && ar.Length > 0)
                {
                    ar[0].Enabled = isEnabled;
                } 
            }
        }

        /// <summary>
        /// Ensures that application is the only. Throws ApplicationException if there is such application
        /// </summary>
        private void EnsureSingleApplication()
        {
            bool createdNew = false;
            Mutex mx = new Mutex(false, Settings.Name, out createdNew);
            Logger.DebugFormat("Is mutex created: {0}", createdNew);

            // If application is already running
            if (createdNew == false)
            {
                throw new ApplicationException(Settings.Name + " application is already running!");
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        System.Windows.Forms.Timer _timer = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            Debug("Starting " + Settings.NameVersion);
            EnsureSingleApplication();

            this.Text = Settings.NameVersion;
            toolStripClock.Text = DateTime.Now.ToString("HH:mm:ss");
        
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000;
            _timer.Start();
            _timer.Tick += _timer_Tick;
        }

        protected void _timer_Tick(object sender, EventArgs e)
        {
            string t = DateTime.Now.ToString("HH:mm:ss");
            if (InvokeRequired)
            {
                Invoke(new GetTextDelegate(UpdateClock), t);
            }else
            {
                UpdateClock(t);
            }
        }

        protected string UpdateClock(string time)
        {
            toolStripClock.Text = time;
            return "";
        }

        private void txtDatasource_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "Access Database|*.mdb|All files|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtDatasource.Text = dlg.FileName;
                _tablesList.DatasourcFilename = dlg.FileName;
            }
        }

        private IDatabaseReader _dr = null;
        private void btnReadData_Click(object sender, EventArgs e)
        {
            //string filename = GetText("txtDatasource");
            if (string.IsNullOrWhiteSpace(_tablesList.DatasourcFilename))
            {
                throw new ValidateFailedException("Please choose Access database file to import");
            }

            new Task(() =>
            {
                SetControlEnabled(this, "btnReadData", false);
                _dr = new AccessDataReader();
                _dr.Open(txtDatasource.Text);
                _tablesList.Tables = _dr.GetTablesList();
                lstTables.Items.Clear();
                lstTables.Items.AddRange(_tablesList.Tables.Select(x => new ListViewItem(new string[] { "?", x.TableName, x.TotalRecords.ToString() })).ToArray());
                SetControlEnabled(this, "btnReadData", true);
            }).Start();
        }

        private void btnAnalizeTables_Click(object sender, EventArgs e)
        {
            try
            {
                _dr = new AccessDataReader();
                _dr.Open(_tablesList.DatasourcFilename);
                var tables = _dr.GetTablesList();
                StringBuilder sb = new StringBuilder();
                foreach (var t in tables)
                {
                    sb.AppendLine(t.TableName);
                    _dr.GetTableSchema(t);
                    sb.AppendLine(DatabaseHelper.DumpTableStructure(t.Fields));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error analizing tables", ex);
                Debug(LogImportance.Error, string.Concat("Error analizing tables", Environment.NewLine, ex.Message));
                throw;
            }
        }

        private void btnCreateTableSql_Click(object sender, EventArgs e)
        {
            EnsureDatabaseReader();

            try
            {
                for (int i = 0; i < _tablesList.Tables.Count; i++)
                {
                    _dr.GetTableSchema(_tablesList.Tables[i]);
                    Debug(DatabaseHelper.GetCreateTableSql(_tablesList.Tables[i], true));
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error generating SQL for create tables");
                Debug(LogImportance.Error, string.Concat("Error generating SQL for create tables", Environment.NewLine, ex.Message));
                throw;
            }
        }

        private void EnsureDatabaseReader()
        {
            if (_dr == null)
            {
                throw new ValidateFailedException("Please choose Access database file and read it");
            }
        }

        private void btnSqlTableFromCsv_Click(object sender, EventArgs e)
        {
            var f = new ParseFsccCsvForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Debug("Got {0} parsed fields for FSCC table", f.Fields.Count);
                var sb = new StringBuilder();
                sb.AppendLine("CREATE TABLE [dbo].[XXXX]").AppendLine("(");
                for (int i = 0; i < f.Fields.Count; i++)
                {
                    //MsDbField dbField = new MsDbField(f.Fields[i].Item1, f.Fields[i].Item2);
                    //sb.Append(dbField.GetDbCreate());
                    sb.AppendFormat("\t[{0}] {1} NULL,", f.Fields[i].Item1, DatabaseHelper.ParseFsccDataType(f.Fields[i].Item2));
                    sb.AppendLine();
                }
                sb.AppendLine(")");

                sb.AppendLine("-------------------------------------------------------");
                sb.AppendLine("var mapping = new List<TransformationMap>();");
                for (int i = 0; i < f.Fields.Count; i++)
                {
                    sb.AppendFormat("mapping.Add(new TransformationMap(\"{0}\"));", f.Fields[i].Item1).AppendLine();
                }

                if (MessageBox.Show("Copy table create SQL to Clipboard?", Settings.NameVersion, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Clipboard.SetText(sb.ToString());
                }
            }
        }

        private void btnWriteData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_tablesList.DatasourcFilename))
            {
                throw new ValidateFailedException("Choose database source filename");
            }
            try
            {
                new Task(() =>
                {
                    try
                    {
                        using (_dr = new AccessDataReader())
                        {
                            _dr.Open(_tablesList.DatasourcFilename);
                            _tablesList.Tables = _dr.GetTablesList();
                            var import = new DataImport();
                            var dt = new DataTable();

                            //var customers = _dr.GetRecords("CUSTOMERS", dt);
                            //Debug("Read {0} prepared as DataTable", customers.Rows.Count);
                            //import.ImportCustomers(dt);

                            //var contracts = _dr.GetRecords("CONTRACTS", dt);
                            //Debug("Read {0} prepared as DataTable", contracts.Rows.Count);
                            //import.ImportContracts(dt);

                            //var ibans = _dr.GetRecords("IBANS", dt);
                            //Debug("Read {0} prepared as DataTable", ibans.Rows.Count);
                            //import.ImportIbans(dt);

                            //var transfers = _dr.GetRecords("BANK TRANSFERS", dt);
                            //Debug("Read {0} prepared as DataTable", transfers.Rows.Count);
                            //import.ImportBankTransfers(dt);

                            //var cards = _dr.GetRecords("CARDS", dt);
                            //Debug("Read {0} prepared as DataTable", cards.Rows.Count);
                            //import.ImportCards(dt);

                            //var transactions = _dr.GetRecords("CONTRACT_BALANCES", dt);
                            //Debug("Read {0} prepared as DataTable", transactions.Rows.Count);
                            //import.ImportContractBalances(dt);

                            //var transactions = _dr.GetRecords("DISCOUNT_ROWS", dt);
                            //Debug("Read {0} prepared as DataTable", transactions.Rows.Count);
                            //import.ImportDiscountRows(dt);

                            // Brand_ins is bad!!!
                            //var acceptors = _dr.GetRecords("CARD ACCEPTORS", dt);
                            //Debug("Read {0} prepared as DataTable", acceptors.Rows.Count);
                            //import.ImportCardAcceptors(dt);

                            //var terminals = _dr.GetRecords("TERMINALS", dt);
                            //Debug("Read {0} prepared as DataTable", terminals.Rows.Count);
                            //import.ImportTerminals(dt);

                            //var documents = _dr.GetRecords("DOCUMENTS", dt);
                            //Debug("Read {0} prepared as DataTable", documents.Rows.Count);
                            //import.ImportDocuments(dt);

                            //var transactions = _dr.GetRecords("TRANSACTIONS", dt);
                            //Debug("Read {0} prepared as DataTable", transactions.Rows.Count);
                            //import.ImportTransactions(dt);

                            //var cardProductGroups = _dr.GetRecords("CARDS_ProdGr", dt);
                            //Debug("Read {0} prepared as DataTable", cardProductGroups.Rows.Count);
                            //import.ImportCardProductGroup(dt);

                            //var webUsers = _dr.GetRecords("Users", dt);
                            //Debug("Read {0} prepared as DataTable", webUsers.Rows.Count);
                            //import.ImportWebUsers(dt);

                            //var fcProductGroups = _dr.GetRecords("FCProductGroup", dt);
                            //Debug("Read {0} prepared as DataTable", fcProductGroups.Rows.Count);
                            //import.ImportFcProductGroups(dt);

                            //var pgListPrice = _dr.GetRecords("ProductGroupListPrice", dt);
                            //Debug("Read {0} prepared as DataTable", pgListPrice.Rows.Count);
                            //import.ImportProductGroupListPrice(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Error making import", ex);
                        throw;
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                Logger.Error("Error writing data to MS SQL (FSCC)", ex);
                Debug("Error writing data to MS SQL (FSCC)");
                throw;
            }
        }

        private string[] RequestLinesFromUser()
        {
            var dlg = new InputForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.Lines;
            }
            return null;
        }

        private void btnCreateModel_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = RequestLinesFromUser();
                if (lines != null)
                {
                    var p = new AvUtils.Parsers.DbSchemaParser();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string s = lines[i].Trim();
                        if (string.IsNullOrEmpty(s) == false)
                        {
                            var dnv = p.ParseLine(s);
                            if (string.IsNullOrEmpty(dnv.Remark) == false)
                            {
                                sb.AppendLine("///").AppendFormat("/// {0}", dnv.Remark).AppendLine().AppendLine("///"); 
                            }
                            sb.AppendFormat("public {0} {1} {{ get; set; }}", dnv.VarType, AvUtils.Common.UnderscoresToCamelCase(dnv.Name)).AppendLine();
                        }
                    }
                    if (MessageBox.Show("Copy C# Model to Clipboard?", Settings.NameVersion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Clipboard.SetText(sb.ToString());
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.Error("Error creating C# model from SQL", ex);
                Debug(LogImportance.Error, "Error creating C# model from SQL");
            }
        }

        private void btnCreateMapping_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = RequestLinesFromUser();
                if (lines != null)
                {
                    var p = new AvUtils.Parsers.DbSchemaParser();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string s = lines[i].Trim();
                        if (string.IsNullOrEmpty(s) == false)
                        {
                            var dnv = p.ParseLine(s);
                            sb.AppendFormat("mapping[\"{0}\"] = \"{0}\";", dnv.Name).AppendLine();
                        }
                    }
                    if (MessageBox.Show("Copy default mapping to Clipboard?", Settings.NameVersion, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Clipboard.SetText(sb.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error creating default mapping from SQL", ex);
                throw;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
        }

        private void lstTables_DoubleClick(object sender, EventArgs e)
        {
            EnsureDatabaseReader();
            try
            {
                string tableName = ((ListView)sender).SelectedItems[0].SubItems[1].Text;
                var dt = _dr.GetTableSchema(tableName);
                var dlg = new InputForm(string.Concat(tableName, Environment.NewLine, DatabaseHelper.DumpTableStructure(dt.Fields)));
                dlg.Height = 700;
                dlg.SetReadonly(true);
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error("Error on tables list double click", ex);
                throw;
            }
        }
    }
}
