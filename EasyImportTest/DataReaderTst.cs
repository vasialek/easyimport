using EasyImport.DataReader;
using EasyImport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImportTest
{
    public class DataReaderTst : IDatabaseReader
    {
        public void Open(string dataFile)
        {
            throw new NotImplementedException();
        }

        public IList<DbTable> GetTablesList()
        {
            throw new NotImplementedException();
        }

        public DbTable GetTableSchema(string tablename)
        {
            throw new NotImplementedException();
        }

        public DbTable GetTableSchema(DbTable table)
        {
            throw new NotImplementedException();
        }

        public IList<DbRecord> GetRecords(string table)
        {
            throw new NotImplementedException();
        }

        public DataTable GetRecords(string table, DataTable dt)
        {
            switch (table.ToLower())
            {
                case "customers":
                    return GetCustomersTable();
            }
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private DataTable GetCustomersTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("cust_id");
            dt.Columns.Add("name");
            dt.Columns.Add("cust_type");
            dt.Columns.Add("company_type");
            dt.Columns.Add("reg_code");
            dt.Columns.Add("vat_code");
            dt.Columns.Add("cust_ref_no");
            dt.Columns.Add("lang");
            dt.Columns.Add("address_street");
            dt.Columns.Add("address_city");
            dt.Columns.Add("address_country");
            dt.Columns.Add("address_post_code");
            dt.Columns.Add("phone");
            dt.Columns.Add("fax");
            dt.Columns.Add("email");
            dt.Columns.Add("branch_id");
            dt.Columns.Add("cust_group_id");
            dt.Columns.Add("decision_no");
            dt.Columns.Add("guarantee_amount");
            dt.Columns.Add("guarantee_valid_from");
            dt.Columns.Add("mng_name");
            dt.Columns.Add("mng_phone");
            dt.Columns.Add("mng_email");
            dt.Columns.Add("send_advertise");

            dt.Rows.Add(new string[]{
                "39146",
                "Dhr. H. Debets BUURTBEHEER BRUNSSUM B.V.",
                "0",
                "0",
                "56317069",
                "NL852071449B01",
                "55043",
                "7",
                "Postbus 141",
                "Brunssum",
                "NL",
                "6440 AC",
                "045-566 68 67",
                "GSM 0652413964",
                "Info@betereburen.nl",
                "0",
                "1",
                "9",
                "0",
                "2014-06-11 11:42:41",
                "Dhr. H. Debets",
                "045-566 68 67",
                "Info@betereburen.nl",
                "1",
            });

            return dt;
        }
    }
}
