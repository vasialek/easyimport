using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using EasyImport.Models;
using EasyImport.Models.Fscc;

namespace EasyImport.DataReader
{
    public interface IDatabaseReader : IDisposable
    {
        void Open(string dataFile);
        IList<DbTable> GetTablesList();
        DbTable GetTableSchema(string tablename);
        DbTable GetTableSchema(DbTable table);
        IList<DbRecord> GetRecords(string table);
        DataTable GetRecords(string table, DataTable dt);
    }

    public class AccessDataReader : IDatabaseReader
    {

        private ILog _logger = null;
        private ILog Logger
        {
            get
            {
                if(_logger == null)
                {
                    _logger = LogManager.GetLogger(typeof(AccessDataReader));
                    log4net.Config.XmlConfigurator.Configure();
                }
                return _logger;
            }
        }

        private OleDbConnection _con = null;

        public void Open(string dataFile)
        {
            try
            {
                Logger.InfoFormat("Going to open Access database file: {0}", dataFile);
                string cs = string.Concat("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=", dataFile, "; User Id=admin; Password=;");
                Logger.DebugFormat("  {0}", cs);
                _con = new OleDbConnection(cs);
                _con.Open();
            }
            catch (Exception ex)
            {
                Logger.Error("Error opening database file", ex);
                throw;
            }
        }

        public IList<DbTable> GetTablesList()
        {
            EnsureConnectionIsOpen("Could not get list of tables.");

            try
            {
                List<DbTable> list = new List<DbTable>();
                Logger.Info("Reading list of tables (not system)...");

                // Get only user tables
                //string[] restrictions = new string[] { };
                DataTable tables = _con.GetSchema("Tables", new string[] { null, null, null, "Table" });
                Logger.InfoFormat("Got {0} tables", tables == null ? "NULL" : tables.Rows.Count.ToString());
                for (int i = 0; tables != null && i < tables.Rows.Count; i++)
                {
                    Logger.DebugFormat("  {0}", tables.Rows[i][2]);
                    list.Add(new DbTable { TableName = tables.Rows[i][2].ToString(), TotalRecords = -1 });
                }

                return list;
            }
            catch (Exception ex)
            {
                Logger.Error("Error reading list of tables", ex);
                throw;
            }
        }

        public DbTable GetTableSchema(string tablename)
        {
            DbTable dt = new DbTable { TableName = tablename };
            return GetTableSchema(dt);
        }

        public DbTable GetTableSchema(DbTable table)
        {
            EnsureConnectionIsOpen("Error getting table schema.");

            try
            {
                //var t = new DbTable();
                DataTable dt = null;
                Logger.InfoFormat("Getting table `{0}` schema", table.TableName);

                // Empty results, I need only schema
                string sql = string.Concat("select * from `", table.TableName, "` where 1 = 0");
                var cmd = new OleDbCommand(sql, _con);
                using(var reader = cmd.ExecuteReader())
	            {
                    dt = reader.GetSchemaTable();
	            }

                Logger.DebugFormat("  got columns for table `{0}`"/*, dt.Columns.Count*/, table.TableName);
                foreach (DataRow dr in dt.Rows)
                {
                    Logger.DebugFormat("  {0,-16}{1}", dr["ColumnName"], dr["DataType"]);
                    table.Fields.Add(new MsDbField(dr["ColumnName"].ToString(), dr["DataType"].ToString()));
                }
                return table;
            }
            catch (Exception ex)
            {
                Logger.Error("Error getting table schema", ex);
                throw;
            }
        }

        public IList<DbRecord> GetRecords(string table)
        {
            EnsureConnectionIsOpen("Could not get records.");

            try
            {
                string sql = string.Format("select * from `{0}`", table);
                Logger.InfoFormat("Reading records from table `{0}`", table);
                Logger.DebugFormat("  {0}", sql);
                var cmd = new OleDbCommand(sql, _con);
                var reader = cmd.ExecuteReader();

                List<DbRecord> list = new List<DbRecord>();

                object[] ar = new object[reader.FieldCount];
                while (reader.Read())
                {
                    reader.GetValues(ar);
                    var r = new DbRecord(ar);
                    list.Add(r);
                }

                Logger.DebugFormat("  Got {0} records", list.Count);
                return list;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public DataTable GetRecords(string table, DataTable dt)
        {
            EnsureConnectionIsOpen("Could not get records.");

            try
            {
                string sql = string.Format("select * from `{0}`", table);
                Logger.InfoFormat("Reading records from table `{0}` (ad DataTable)", table);
                Logger.DebugFormat("  {0}", sql);
                var cmd = new OleDbCommand(sql, _con);
                var reader = cmd.ExecuteReader();
                object[] ar = null;

                //List<DbRecord> list = new List<DbRecord>();
                dt.Clear();
                dt.Columns.Clear();

                if (reader.Read())
                {
                    ar = new object[reader.FieldCount];

                    Logger.DebugFormat("  Structure of `{0}`:", table);
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Logger.DebugFormat("    {0} ({1})", reader.GetName(i), reader.GetDataTypeName(i));
                        dt.Columns.Add(reader.GetName(i));
                    }

                    // Add first row, we read
                    reader.GetValues(ar);
                    dt.Rows.Add(ar);

                    while (reader.Read())
                    {
                        reader.GetValues(ar);
                        dt.Rows.Add(ar);
                        //var r = new DbRecord(ar);
                        //list.Add(r);
                    }
                }

                Logger.DebugFormat("  Got {0} records", dt.Rows.Count);
                return dt;
            }
            catch (Exception ex)
            {
                Logger.Error("Error getting records", ex);
                throw;
            }
        }

        public IList<Iban> GetIbans()
        {
            var ibans = new List<Iban>();
            var list = GetRecords("IBANS");
            var transformer = new DataTransformer();

            foreach (var r in list)
            {
                ibans.Add(transformer.ConvertToIban(r));
            }
            return ibans;
        }

        public void Dispose()
        {
            if (_con != null)
            {
                Logger.Debug("Closing Access database reader connection");
                _con.Close();
            }
        }

        private void EnsureConnectionIsOpen(string title)
        {
            if (_con == null)
            {
                throw new DataException(string.Concat(title, " Connection to database file is not initialized"));
            }
        }
    }

}
