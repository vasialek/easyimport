using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.DataReader
{
    public class DataImport
    {

        public class TransformationMap
        {
            public string SourceField { get; private set; }
            public string DestinationField { get; private set; }
            public Func<object, object> Transformation { get; private set; }

            /// <summary>
            /// To help rebuilding
            /// </summary>
            public int SourceFieldIndex { get; set; }
            public int DestinationFieldIndex { get; set; }

            public TransformationMap(string sourceField)
                : this(sourceField, sourceField, null)
            {
            }

            public TransformationMap(string sourceField, string destinationField)
                : this(sourceField, destinationField, null)
            {
            }

            public TransformationMap(string sourceField, Func<object, object> transformation)
                : this(sourceField, sourceField, transformation)
            {
            }

            public TransformationMap(string sourceField, string destinationField, Func<object, object> transformation)
            {
                SourceField = sourceField;
                DestinationField = destinationField;
                Transformation = transformation;
            }
        }

        private ILog _logger = null;
        private ILog Logger
        {
            get
            {
                if(_logger == null)
                {
                    _logger = LogManager.GetLogger(typeof(DataImport));
                    log4net.Config.XmlConfigurator.Configure();
                }
                return _logger;
            }
        }

        public void ImportDiscountRows(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Discount Rows records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("Item_group", "item_group"));
                mapping.Add(new TransformationMap("product_group"));
                mapping.Add(new TransformationMap("ca_id"));
                mapping.Add(new TransformationMap("ca_group_id"));
                mapping.Add(new TransformationMap("price_model"));
                mapping.Add(new TransformationMap("disc_type"));
                mapping.Add(new TransformationMap("disc0"));
                mapping.Add(new TransformationMap("priority"));
                mapping.Add(new TransformationMap("bound_type"));
                mapping.Add(new TransformationMap("bound1"));
                mapping.Add(new TransformationMap("disc1"));
                mapping.Add(new TransformationMap("bound2"));
                mapping.Add(new TransformationMap("disc2"));
                mapping.Add(new TransformationMap("bound3"));
                mapping.Add(new TransformationMap("disc3"));
                mapping.Add(new TransformationMap("bound4"));
                mapping.Add(new TransformationMap("disc4"));
                mapping.Add(new TransformationMap("bound5"));
                mapping.Add(new TransformationMap("disc5"));
                mapping.Add(new TransformationMap("bound6"));
                mapping.Add(new TransformationMap("disc6"));
                mapping.Add(new TransformationMap("bound7"));
                mapping.Add(new TransformationMap("disc7"));
                mapping.Add(new TransformationMap("bound8"));
                mapping.Add(new TransformationMap("disc8"));
                mapping.Add(new TransformationMap("bound9"));
                mapping.Add(new TransformationMap("disc9"));
                mapping.Add(new TransformationMap("bound10"));
                mapping.Add(new TransformationMap("disc10"));

                data = RebuildDataTable(data, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 10));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_disc", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing Discount Rows", ex);
                throw;
            }
        }

        public void ImportContractBalances(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Contract Balance records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("amount"));
                mapping.Add(new TransformationMap("dt"));

                data = RebuildDataTable(data, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 10));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_balance", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing Contract Balances", ex);
                throw;
            }
        }

        public void ImportTransactions(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Transation records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("tran_id"));
                mapping.Add(new TransformationMap("tran_det_id"));
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("inv_id"));
                mapping.Add(new TransformationMap("auth_code"));
                mapping.Add(new TransformationMap("rcpt_no"));
                mapping.Add(new TransformationMap("card_no"));
                mapping.Add(new TransformationMap("acq_inst"));
                mapping.Add(new TransformationMap("ca_id"));
                mapping.Add(new TransformationMap("term_id"));
                mapping.Add(new TransformationMap("loc_dt"));
                mapping.Add(new TransformationMap("sys_dt"));
                mapping.Add(new TransformationMap("curr_loc"));
                mapping.Add(new TransformationMap("curr_rate"));
                mapping.Add(new TransformationMap("amount_loc_r", new Func<object, object>(x => { return ConvertToInt32(x, "amount_loc_r", -1); })));
                mapping.Add(new TransformationMap("amount_loc_vat_r", new Func<object, object>(x => { return ConvertToInt32(x, "amount_loc_vat_r"); })));
                mapping.Add(new TransformationMap("amount_loc_excise_r", new Func<object, object>(x => { return ConvertToInt32(x, "amount_loc_excise_r"); })));
                mapping.Add(new TransformationMap("amount_sys_r", new Func<object, object>(x => { return ConvertDecimalToIntMinor(x, "amount_sys_r"); })));
                mapping.Add(new TransformationMap("odometer"));
                mapping.Add(new TransformationMap("vrn"));
                mapping.Add(new TransformationMap("comment"));
                mapping.Add(new TransformationMap("item_group"));
                mapping.Add(new TransformationMap("product_group"));
                mapping.Add(new TransformationMap("item_code"));
                mapping.Add(new TransformationMap("item_name"));
                mapping.Add(new TransformationMap("item_um"));
                mapping.Add(new TransformationMap("item_quantity"));
                mapping.Add(new TransformationMap("item_price"));
                mapping.Add(new TransformationMap("amount_loc_rd", new Func<object, object>(x => { return ConvertToInt32(x, "amount_loc_rd", -1); })));
                mapping.Add(new TransformationMap("amount_loc_vat_rd", new Func<object, object>(x => { return ConvertToInt32(x, "amount_loc_vat_rd"); })));
                mapping.Add(new TransformationMap("amount_sys_rd", new Func<object, object>(x => { return ConvertToInt32(x, "amount_sys_rd", -1); })));
                mapping.Add(new TransformationMap("vat_rate"));
                mapping.Add(new TransformationMap("amount_loc_excise_rd", new Func<object, object>(x => { return Convert.IsDBNull(x) ? -1 : ConvertToInt32(x, "amount_loc_excise_rd"); })));

                data = RebuildDataTable(data, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 10));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_tran", data, con, t);
                    t.Commit();
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Error importing Transactions", ex);
                throw;
            }
        }

        public void ImportContracts(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Contract records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("contract_type"));
                mapping.Add(new TransformationMap("contract_name"));
                mapping.Add(new TransformationMap("address1_id"));
                mapping.Add(new TransformationMap("address1_street"));
                mapping.Add(new TransformationMap("address1_city"));
                mapping.Add(new TransformationMap("address1_country"));
                mapping.Add(new TransformationMap("address1_post_code"));
                mapping.Add(new TransformationMap("Address1_name"));
                mapping.Add(new TransformationMap("address2_id"));
                mapping.Add(new TransformationMap("address2_street"));
                mapping.Add(new TransformationMap("address2_city"));
                mapping.Add(new TransformationMap("address2_country"));
                mapping.Add(new TransformationMap("address2_post_code"));
                mapping.Add(new TransformationMap("address2_name"));
                mapping.Add(new TransformationMap("phone"));
                mapping.Add(new TransformationMap("fax"));
                mapping.Add(new TransformationMap("email"));
                mapping.Add(new TransformationMap("parent_id"));
                mapping.Add(new TransformationMap("ext_nav_code"));
                mapping.Add(new TransformationMap("contract_no"));
                mapping.Add(new TransformationMap("fin_type"));
                mapping.Add(new TransformationMap("valid_from"));
                mapping.Add(new TransformationMap("valid_to"));
                mapping.Add(new TransformationMap("lang"));
                mapping.Add(new TransformationMap("agent_id"));
                mapping.Add(new TransformationMap("state"));
                mapping.Add(new TransformationMap("Curr", "curr"));
                mapping.Add(new TransformationMap("curr_rate_provider_id"));
                mapping.Add(new TransformationMap("inv_freq"));
                mapping.Add(new TransformationMap("inv_series_id"));
                mapping.Add(new TransformationMap("inv_template_id"));
                mapping.Add(new TransformationMap("send_inv", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("inv_show_dt", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("cred_dt_from"));
                mapping.Add(new TransformationMap("max_debt_block"));
                mapping.Add(new TransformationMap("max_debt_disc"));
                mapping.Add(new TransformationMap("pay_term"));
                mapping.Add(new TransformationMap("cred_term"));
                mapping.Add(new TransformationMap("pay_delay"));
                mapping.Add(new TransformationMap("fine"));
                mapping.Add(new TransformationMap("disc_check_pay", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("disc_curr_period", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("disc_foreign", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("transfer_auto", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("mng_name"));
                mapping.Add(new TransformationMap("mng_position"));
                mapping.Add(new TransformationMap("mng_phone"));
                mapping.Add(new TransformationMap("mng_email"));
                mapping.Add(new TransformationMap("mng_description"));
                mapping.Add(new TransformationMap("description"));
                mapping.Add(new TransformationMap("block_reason"));
                mapping.Add(new TransformationMap("contract_group_id"));
                mapping.Add(new TransformationMap("inv_send_email", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("data_temp_changed", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("inv_comment_id"));
                mapping.Add(new TransformationMap("agg_fuel_quantity"));
                mapping.Add(new TransformationMap("agg_penalty_type"));
                mapping.Add(new TransformationMap("send_advertise", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                mapping.Add(new TransformationMap("dd_agreement_no"));
                mapping.Add(new TransformationMap("credit_limit"));
                mapping.Add(new TransformationMap("campaign_code"));
                mapping.Add(new TransformationMap("einvoice_address"));
                mapping.Add(new TransformationMap("sales_person"));

                data = RebuildDataTable(data, mapping);
                var fields = new Dictionary<string, object>();
                fields["cred_term"] = -1;
                AddFieldToTable(data, fields);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 10));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_contract", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing contracts", ex);
                throw;
            }
        }

        private void AddFieldToTable(DataTable data, Dictionary<string, object> fields)
        {
            // Adds fields to columns
            foreach (var kvp in fields)
            {
                data.Columns.Add(kvp.Key);
            }

            // Fill with data
            for (int i = 0; i < data.Rows.Count; i++)
            {
                foreach (var kvp in fields)
                {
                    data.Rows[i][kvp.Key] = kvp.Value;
                }
            }
        }

        public void ImportItems(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy `items` records to FSCC DB {0}", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("dt_from"));
                mapping.Add(new TransformationMap("dt_to"));
                mapping.Add(new TransformationMap("inv_dt"));
                mapping.Add(new TransformationMap("pay_dt"));
                mapping.Add(new TransformationMap("amount"));
                mapping.Add(new TransformationMap("amount_wo_vat"));
                mapping.Add(new TransformationMap("paid_dt"));
                mapping.Add(new TransformationMap("month_limit3"));
                mapping.Add(new TransformationMap("week_limit1"));
                mapping.Add(new TransformationMap("week_limit2"));
                mapping.Add(new TransformationMap("week_limit3"));
                mapping.Add(new TransformationMap("day_limit1"));
                mapping.Add(new TransformationMap("day_limit2"));
                mapping.Add(new TransformationMap("day_limit3"));
                mapping.Add(new TransformationMap("valid_from"));
                mapping.Add(new TransformationMap("valid_to"));
                mapping.Add(new TransformationMap("is_local"));
                mapping.Add(new TransformationMap("branch_id"));
                mapping.Add(new TransformationMap("block_description"));
                mapping.Add(new TransformationMap("address_street"));
                mapping.Add(new TransformationMap("address_city"));
                mapping.Add(new TransformationMap("address_country"));
                mapping.Add(new TransformationMap("address_post_code"));
                mapping.Add(new TransformationMap("address1_name"));
                //mapping.Add(new TransformationMap("ask_odometer"));
                //mapping.Add(new TransformationMap("ask_vrn"));
                //mapping.Add(new TransformationMap("ca_disc"));
                data = RebuildDataTableOld(data, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("item", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing cards", ex);
                throw;
            }
        }

        public void ImportCards(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Card records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("card_no"));
                mapping.Add(new TransformationMap("state_block"));
                mapping.Add(new TransformationMap("state_stolen"));
                mapping.Add(new TransformationMap("state_lost"));
                mapping.Add(new TransformationMap("check_days"));
                mapping.Add(new TransformationMap("month_limit1"));
                mapping.Add(new TransformationMap("month_limit2"));
                mapping.Add(new TransformationMap("month_limit3"));
                mapping.Add(new TransformationMap("week_limit1"));
                mapping.Add(new TransformationMap("week_limit2"));
                mapping.Add(new TransformationMap("week_limit3"));
                mapping.Add(new TransformationMap("day_limit1"));
                mapping.Add(new TransformationMap("day_limit2"));
                mapping.Add(new TransformationMap("day_limit3"));
                mapping.Add(new TransformationMap("valid_from"));
                mapping.Add(new TransformationMap("valid_to"));
                mapping.Add(new TransformationMap("is_local"));
                mapping.Add(new TransformationMap("branch_id"));
                mapping.Add(new TransformationMap("description"));
                mapping.Add(new TransformationMap("block_description"));
                mapping.Add(new TransformationMap("prod_pin_block"));
                mapping.Add(new TransformationMap("prod_pin_encrypted"));
                mapping.Add(new TransformationMap("prod_track1"));
                mapping.Add(new TransformationMap("prod_track2"));
                mapping.Add(new TransformationMap("prod_embos1"));
                mapping.Add(new TransformationMap("prod_embos2"));
                mapping.Add(new TransformationMap("prod_embos3"));
                mapping.Add(new TransformationMap("prod_embos4"));
                mapping.Add(new TransformationMap("rg_id"));
                mapping.Add(new TransformationMap("address_id"));
                mapping.Add(new TransformationMap("address_street"));
                mapping.Add(new TransformationMap("address_city"));
                mapping.Add(new TransformationMap("address_country"));
                mapping.Add(new TransformationMap("address_post_code"));
                mapping.Add(new TransformationMap("address1_name"));
                mapping.Add(new TransformationMap("ask_odometer", new Func<object, object>(x => { return ConvertToBoolean(x, false); })));
                mapping.Add(new TransformationMap("ask_vrn", new Func<object, object>(x => { return ConvertToBoolean(x, false); })));

                data = RebuildDataTable(data, mapping);

                var fields = new Dictionary<string, object>();
                fields["prod_track2"] = "---";
                fields["rg_id"] = -1;
                AddFieldToTable(data, fields);

                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_card", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing cards", ex);
                throw;
            }
        }

        public void ImportCardAcceptors(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Card Acceptors records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("ca_id"));
                mapping.Add(new TransformationMap("acq_inst"));
                mapping.Add(new TransformationMap("brand_inst", new Func<object, object>(x => { return CheckIfStringFitLength(x, "brand_inst", 11, true); })));
                mapping.Add(new TransformationMap("ca_name", new Func<object, object>(x => { return CheckIfStringFitLength(x, "ca_name", 100, false); })));
                mapping.Add(new TransformationMap("ca_address", new Func<object, object>(x => { return CheckIfStringFitLength(x, "ca_address", 100, false); })));
                mapping.Add(new TransformationMap("ca_city", new Func<object, object>(x => { return CheckIfStringFitLength(x, "ca_city", 50, false); })));
                mapping.Add(new TransformationMap("ca_country"));
                mapping.Add(new TransformationMap("ca_phone"));
                mapping.Add(new TransformationMap("ca_email", new Func<object, object>(x => { return CheckIfStringFitLength(x, "ca_email", 50, false); })));
                mapping.Add(new TransformationMap("state"));
                mapping.Add(new TransformationMap("ca_disc", new Func<object, object>(x => { return ConvertToBoolean(x); })));
                mapping.Add(new TransformationMap("municipality_id"));
                mapping.Add(new TransformationMap("ca_type"));

                data = RebuildDataTable(data, mapping);
                
                //var fields = new Dictionary<string, object>();
                //fields["brand_inst"] = -666;
                //AddFieldToTable(data, fields);

                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 10));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_ca", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing card acceptors", ex);
                throw;
            }
        }

        public void ImportBankTransfers(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Bank Transfer records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("file_name"));
                mapping.Add(new TransformationMap("transfer_no"));
                mapping.Add(new TransformationMap("transfer_line_no"));
                mapping.Add(new TransformationMap("payer_name"));
                mapping.Add(new TransformationMap("cust_iban"));
                mapping.Add(new TransformationMap("payer_no"));
                mapping.Add(new TransformationMap("primary_payer_no"));
                mapping.Add(new TransformationMap("primary_payer_name"));
                mapping.Add(new TransformationMap("recipient_iban"));
                mapping.Add(new TransformationMap("transfer_amount", new Func<object, object>(x => { return ConvertDecimalToBigintMinor(x, "transfer_amount"); })));
                mapping.Add(new TransformationMap("transfer_dt"));
                mapping.Add(new TransformationMap("transfer_comment"));
                mapping.Add(new TransformationMap("netting", new Func<object, object>(x => { return ConvertToBoolean(x, false); })));
                data = RebuildDataTable(data, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                //using (SqlTransaction t = con.BeginTransaction())
                //{
                //    DatabaseHelper.BulkIt("data_nl_transfer", data, con, t);
                //    t.Commit();
                //}
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing Bank Transfers", ex);
                throw;
            }
        }

        //public void ImportCustomers(DataTable data)
        //{
        //    try
        //    {
        //        string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
        //        Logger.InfoFormat("Going to bulk copy `document` records to FSCC DB {0}", data.Rows.Count);
        //        SqlConnection con = new SqlConnection(cs);
        //        con.Open();

        //        var mapping = new List<TransformationMap>();
        //        mapping.Add(new TransformationMap("contract_id"));
        //        mapping.Add(new TransformationMap("cust_id"));
        //        mapping.Add(new TransformationMap("dt_from"));
        //        mapping.Add(new TransformationMap("dt_to"));
        //        mapping.Add(new TransformationMap("inv_dt"));
        //        mapping.Add(new TransformationMap("pay_dt"));
        //        mapping.Add(new TransformationMap("amount"));
        //        mapping.Add(new TransformationMap("amount_wo_vat"));
        //        mapping.Add(new TransformationMap("paid_dt"));
        //        mapping.Add(new TransformationMap("paid_amount"));
        //        //mapping.Add(new TransformationMap("paid"));
        //        //mapping.Add(new TransformationMap("inv_no"));
        //        mapping.Add(new TransformationMap("inv_series"));
        //        mapping.Add(new TransformationMap("files"));
        //        mapping.Add(new TransformationMap("doc_type"));
        //        mapping.Add(new TransformationMap("ext_ref_no"));

        //        data = RebuildDataTable(data, mapping);
        //        Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

        //        using (SqlTransaction t = con.BeginTransaction())
        //        {
        //            DatabaseHelper.BulkIt("document", data, con, t);
        //            t.Commit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("Error importing documents", ex);
        //        throw;
        //    }
        //}

        public void ImportDocuments(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy `document` records to FSCC DB {0}", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("contract_id", new Func<object, object>(x => { return ConvertToInt32(x, "contract_id"); })));
                mapping.Add(new TransformationMap("cust_id", new Func<object, object>(x => { return ConvertToInt32(x, "cust_id"); })));
                mapping.Add(new TransformationMap("dt_from"));
                mapping.Add(new TransformationMap("dt_to"));
                mapping.Add(new TransformationMap("inv_dt"));
                mapping.Add(new TransformationMap("pay_dt"));
                mapping.Add(new TransformationMap("amount"));
                mapping.Add(new TransformationMap("amount_wo_vat", new Func<object, object>(x => { return Convert.IsDBNull(x) ? -1 : x; })));
                mapping.Add(new TransformationMap("paid_dt"));
                mapping.Add(new TransformationMap("paid_amount"));
                mapping.Add(new TransformationMap("paid", new Func<object, object>(x => { return ConvertToBoolean(x.ToString()); })));
                
                // Bad type writing to SQL
                //mapping.Add(new TransformationMap("inv_no", new Func<object, object>(x => { return ConvertToUInt32(x, false); })));
                mapping.Add(new TransformationMap("inv_no", new Func<object, object>(x => { return -1; })));
                mapping.Add(new TransformationMap("inv_no", "inv_no_str"));
                
                mapping.Add(new TransformationMap("inv_series"));
                mapping.Add(new TransformationMap("files"));
                //mapping.Add(new TransformationMap("doc_type"));
                mapping.Add(new TransformationMap("doc_type", new Func<object, object>(x => { return ConvertToInt32(x, "doc_type"); })));
                mapping.Add(new TransformationMap("ext_ref_no"));

                data = RebuildDataTable(data, mapping);

                //var fields = new Dictionary<string, object>();
                //fields["amount_wo_vat"] = -1;
                //AddFieldToTable(data, fields);

                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_doc", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing documents", ex);
                throw;
            }
        }

        public void ImportTerminals(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Terminal records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("ca_id"));
                mapping.Add(new TransformationMap("terminal_id"));
                mapping.Add(new TransformationMap("frame_no"));
                mapping.Add(new TransformationMap("state"));

                data = RebuildDataTable(data, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 10));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_term", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing terminals", ex);
                throw;
            }
        }

        public void ImportIbans(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} IBAN records to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("type"));
                mapping.Add(new TransformationMap("acc_number", new Func<object, object>(x => { return CheckIfStringFitLength(x, "acc_number", 35, true); })));
                mapping.Add(new TransformationMap("bank_code", new Func<object, object>(x => { return CheckIfStringFitLength(x, "bank_code", 35, true); })));
                mapping.Add(new TransformationMap("direct_debit"));

                data = RebuildDataTable(data, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 10));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_iban", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing IBANs", ex);
                throw;
            }
        }

        public void ImportCustomers(DataTable data)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} Customers to FSCC DB", data.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("name"));
                mapping.Add(new TransformationMap("cust_type"));
                mapping.Add(new TransformationMap("company_type"));
                mapping.Add(new TransformationMap("reg_code"));
                mapping.Add(new TransformationMap("vat_code", new Func<object, object>(x => { return CheckIfStringFitLength(x, "vat_code", 20, true); })));
                mapping.Add(new TransformationMap("cust_ref_no"));
                mapping.Add(new TransformationMap("lang"));
                //mapping.Add(new TransformationMap("address_id"));
                mapping.Add(new TransformationMap("address_street"));
                mapping.Add(new TransformationMap("address_city"));
                mapping.Add(new TransformationMap("address_country"));
                mapping.Add(new TransformationMap("address_post_code"));
                mapping.Add(new TransformationMap("address_name"));
                mapping.Add(new TransformationMap("phone"));
                mapping.Add(new TransformationMap("fax"));
                mapping.Add(new TransformationMap("email"));
                mapping.Add(new TransformationMap("branch_id"));
                mapping.Add(new TransformationMap("cust_group_id"));
                mapping.Add(new TransformationMap("description"));
                //mapping.Add(new TransformationMap("web_username"));
                mapping.Add(new TransformationMap("web_psw"));
                mapping.Add(new TransformationMap("insurance_dt"));
                mapping.Add(new TransformationMap("insurance_amount"));
                mapping.Add(new TransformationMap("decision_dt"));
                mapping.Add(new TransformationMap("decision_no"));
                mapping.Add(new TransformationMap("decision_type"));
                mapping.Add(new TransformationMap("guarantee_type"));
                mapping.Add(new TransformationMap("guarantee_bank"));
                mapping.Add(new TransformationMap("guarantee_amount", "guarantee_amount_str", new Func<object, object>(x => { return ConvertToInt32AndMultiply(x, "guarantee_amount", 0.000001); })));
                mapping.Add(new TransformationMap("guarantee_valid_from"));
                mapping.Add(new TransformationMap("guarantee_valid_to"));
                mapping.Add(new TransformationMap("guarantee_description"));
                mapping.Add(new TransformationMap("ins_decision_date"));
                mapping.Add(new TransformationMap("insf_evaluated_dt"));
                mapping.Add(new TransformationMap("insf_limit_amount"));
                mapping.Add(new TransformationMap("insf_risk_class"));
                mapping.Add(new TransformationMap("insf_monitoring"));
                mapping.Add(new TransformationMap("insf_last_mon_dt"));
                mapping.Add(new TransformationMap("insf_next_mon_dt"));
                mapping.Add(new TransformationMap("mng_name"));
                mapping.Add(new TransformationMap("mng_position"));
                mapping.Add(new TransformationMap("mng_phone"));
                mapping.Add(new TransformationMap("mng_email"));
                mapping.Add(new TransformationMap("mng_description"));
                mapping.Add(new TransformationMap("send_advertise", new Func<object, object>(x => { return ConvertToBoolean(x); })));

                data = RebuildDataTable(data, mapping);

                // Add fake field
                data.Columns.Add("web_username");
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    data.Rows[i]["web_username"] = "---";
                }

                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("dbo.data_nl_customer", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing customers", ex);
                throw;
            }
        }

        public void ImportCardProductGroup(DataTable cardProductGroups)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy to `data_nl_card_product_group` records to FSCC DB {0}", cardProductGroups.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                mapping.Add(new TransformationMap("contract_id"));
                mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("card_no"));
                mapping.Add(new TransformationMap("rg_id"));

                var data = RebuildDataTable(cardProductGroups, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_card_product_group", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing card product groups", ex);
                throw;
            }
        }

        public void ImportWebUsers(DataTable webUsers)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy to `data_nl_web_user` records to FSCC DB {0}", webUsers.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                //mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("web_username", "login"));
                mapping.Add(new TransformationMap("web_psw", "pass"));
                //mapping.Add(new TransformationMap("web_user_type"));
                //mapping.Add(new TransformationMap("rights"));
                //mapping.Add(new TransformationMap("username"));

                var data = RebuildDataTable(webUsers, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_web_user", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing web users", ex);
                throw;
            }
        }

        public void ImportFcProductGroups(DataTable fcProductGroups)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy to `data_nl_fcproductgroup` records to FSCC DB {0}", fcProductGroups.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                //mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("ProductGroupCode"));
                mapping.Add(new TransformationMap("ProductGroupName"));
                mapping.Add(new TransformationMap("StringID"));

                var data = RebuildDataTable(fcProductGroups, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_fcproductgroup", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing FC product groups", ex);
                throw;
            }
        }

        public void ImportProductGroupListPrice(DataTable records)
        {
            try
            {
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy to `data_nl_ProductGroupListPrice` records to FSCC DB {0}", records.Rows.Count);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                var mapping = new List<TransformationMap>();
                //mapping.Add(new TransformationMap("cust_id"));
                mapping.Add(new TransformationMap("ID"));
                mapping.Add(new TransformationMap("ServiceProviderID"));
                mapping.Add(new TransformationMap("DeliverySPID"));
                mapping.Add(new TransformationMap("EffectiveDateUTC"));

                var data = RebuildDataTable(records, mapping);
                Logger.DebugFormat("First rows to import:{0}{1}", Environment.NewLine, DatabaseHelper.DumpDataTable(data, 30));

                using (SqlTransaction t = con.BeginTransaction())
                {
                    DatabaseHelper.BulkIt("data_nl_ProductGroupListPrice", data, con, t);
                    t.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing product group list price", ex);
                throw;
            }
        }

        protected void ImportDataTable(string table, DataTable data, Dictionary<string, string> mapping)
        {
            try
            {
                Logger.DebugFormat("Listing 10 records to import to table `{0}`{1}{2}", table, Environment.NewLine, DatabaseHelper.DumpDataTable(data, 10));
                string cs = ConfigurationManager.ConnectionStrings["ExportConnectionString"].ConnectionString;
                Logger.InfoFormat("Going to bulk copy {0} records to FSCC DB {1}", data.Rows.Count, table);
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                Logger.DebugFormat("  Calling bulk copy to import in {0}...", table);
                DatabaseHelper.BulkIt(table, data, con, mapping);
                Logger.Debug("    done.");
            }
            catch (Exception ex)
            {
                Logger.Error("Error importing to table: " + table, ex);
                throw;
            }
        
        }

        public DataImport.TransformationMap[] BuilTransformation(DataTable data, IList<DataImport.TransformationMap> mapping)
        {
            var map = new DataImport.TransformationMap[mapping.Count];

            for (int i = 0; i < data.Columns.Count; i++)
            {
                var matchF = mapping.FirstOrDefault(x => x.SourceField == data.Columns[i].ColumnName);
                if (matchF != null)
                {
                    int indexOfMapped = mapping.IndexOf(matchF);
                    map[indexOfMapped] = matchF;
                    map[indexOfMapped].SourceFieldIndex = i;
                    map[indexOfMapped].DestinationFieldIndex = indexOfMapped;
                }
            }

            return map.ToArray();
        }

        public DataTable RebuildDataTable(DataTable sourceTable, IList<TransformationMap> mapping)
        {
            var map = BuilTransformation(sourceTable, mapping);
            int sourceFieldIndex, destinationFieldIndex;
            DataTable newTable = new DataTable();

            // Format columns for new table
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] != null)
                {
                    sourceFieldIndex = map[i].SourceFieldIndex;
                    destinationFieldIndex = map[i].DestinationFieldIndex;
                    newTable.Columns.Add(map[i].DestinationField);
                }
            }

            // Copy data
            foreach (DataRow r in sourceTable.Rows)
            {
                object[] ar = new object[newTable.Columns.Count];
                int newTableColumnIndex = 0;
                
                for (int i = 0; i < map.Length; i++)
                {
                    // Each column which is mapped
                    if (map[i] != null)
                    {
                        var transformationF = map[i].Transformation;
                        if (transformationF != null)
                        {
                            // Transform data by user-defined callback
                            ar[newTableColumnIndex] = transformationF(r[map[i].SourceFieldIndex]);
                        }
                        else
                        {
                            ar[newTableColumnIndex] = r[map[i].SourceFieldIndex];
                        }
                        newTableColumnIndex++;
                    }
                }

                newTable.Rows.Add(ar);
            }

            return newTable;
        }

        protected DataTable RebuildDataTableOld(DataTable data, IList<TransformationMap> transformMap)
        {
            throw new NotImplementedException("Use RebuildDataTable method instead!");
            try
            {
                var dt = new DataTable();
                // Transformation by column index
                TransformationMap[] transformations = new TransformationMap[transformMap.Count];

                Logger.Info("Mapping table:");
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    Logger.DebugFormat("  {0}", data.Columns[i].ColumnName);
                    var matchF = transformMap.FirstOrDefault(x => x.SourceField == data.Columns[i].ColumnName);
                    if (matchF != null)
                    {
                        Logger.DebugFormat("    is in mapped list");
                        int idx = transformMap.IndexOf(matchF);
                        transformations[idx] = matchF;
                        transformations[idx].SourceFieldIndex = i;
                        dt.Columns.Add(matchF.DestinationField);
                    }
                    else
                    {
                        transformations[i] = null;
                        Logger.WarnFormat("    field `{0}` is not in mapped list, skip it", data.Columns[i].ColumnName);
                    }
                }

                Logger.Debug("Mapped table:");
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (transformations[i] != null)
                    {
                        int indexOfDestination = transformMap[transformations[i].SourceFieldIndex].SourceFieldIndex;
                        Logger.ErrorFormat("  {0} => {1}", data.Columns[i].ColumnName, transformMap[indexOfDestination].DestinationField);
                    }
                }
                return dt;

                int destinationColumnIndex = 0;
                foreach (DataRow row in data.Rows)
                {
                    object[] r = new object[dt.Columns.Count];
                    destinationColumnIndex = 0;
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        // This column is in use
                        if (transformations[i] != null)
                        {
                            // Need to transform this column
                            r[destinationColumnIndex] = row[i];
                            //if (transformations[i].Transformation != null)
                            //{
                            //    r[destinationColumnIndex] = transformations[i].Transformation(row[transformations[i].SourceFieldIndex]);
                            //}
                            //else
                            //{
                            //    r[destinationColumnIndex] = row[transformations[i].SourceFieldIndex];
                            //}
                            destinationColumnIndex++;
                        }
                    }
                    dt.Rows.Add(r);
                }
                return dt;
            }
            catch (Exception ex)
            {
                Logger.Error("Error mapping table for import", ex);
                throw;
            }
        }

        protected string GetPriceAsMinor(decimal p)
        {
            // 0 => 0
            // 12 => 1200
            // 12.34 => 1234
            // 12,34 => 1234
            if (p == 0m)
            {
                return "0";
            }
            string s = p.ToString();
            int pos = 0;
            if ((pos = s.IndexOf('.')) != -1)
            {
                return string.Concat(s.Substring(0, pos), s.Substring(pos + 1));
            }
            if ((pos = s.IndexOf(',')) != -1)
            {
                return string.Concat(s.Substring(0, pos), s.Substring(pos + 1));
            }
            return string.Concat(s, "00");
        }

        private static int _limit = 0;

        /// <summary>
        /// Converts to bigint as minor currency (4.56 => 456)
        /// </summary>
        private Int64 ConvertDecimalToBigintMinor(object x, string fieldName)
        {
            try
            {
                decimal d = Convert.ToDecimal(x);
                //return Convert.ToInt64(d * 100);
                var priceMinor = Convert.ToInt64(d * 100);
                // Without decimal separator and converted to minor
                string original = GetPriceAsMinor(d);
                if (original != priceMinor.ToString())
                {
                    Logger.ErrorFormat("  Field `{0}` convertion is bad. Got: {1}, expected: {2}", fieldName, priceMinor, original);
                    //throw new Exception("XXXXXXXXXX");
                }
                //if (_limit++ > 100)
                //{
                //    throw new Exception("STOP IT!!!");
                //}
                return priceMinor;
            }
            catch (Exception ex)
            {
                Logger.Error("Field `" + fieldName + "` could not be converted to bigint (minor): " + x, ex);
                throw;
            }
        }

        /// <summary>
        /// Converts to int as minor currency (4.56 => 456)
        /// </summary>
        private Int32 ConvertDecimalToIntMinor(object x, string fieldName)
        {
            try
            {
                decimal d = Convert.ToDecimal(x);
                return Convert.ToInt32(d * 100);
            }
            catch (Exception ex)
            {
                Logger.Error("Field `" + fieldName + "` could not be converted to int (minor): " + x, ex);
                throw;
            }
        }

        protected bool ConvertToBoolean(object o, bool defaultValue = true)
        {
            // By default is true
            return Convert.IsDBNull(o) ? defaultValue : ConvertToBoolean(o.ToString());
        }

        protected bool ConvertToBoolean(string x)
        {
            if (x == "1")
            {
                return true;
            }
            if (x == "0")
            {
                return false;
            }
            throw new ArgumentOutOfRangeException("Could not convert to boolean: " + x);
        }

        protected object ConvertToInt64(object o)
        {
            return Convert.IsDBNull(o) ? (object)null : Convert.ToInt64(o);
        }

        protected object ConvertToUInt32(object o, bool hideOverflowException = false)
        {
            try
            {
                //Logger.DebugFormat("Converting to UInt32: {0}", o);
                return Convert.IsDBNull(o) ? (object)null : Convert.ToUInt32(o);
            }
            catch (OverflowException oex)
            {
                Logger.Error("Value to large/small for UInt32: " + o);
                if (hideOverflowException)
                {
                    return null;
                }
                throw;
            }
            catch (Exception)
            {
                Logger.Error("Could not convert to UInt32: " + o);
                throw;
            }
        }

        private int ConvertToInt32AndMultiply(object o, string fieldname, double multiplier)
        {
            try
            {
                Logger.DebugFormat("  converting to double '{0}'", o);
                var v = Convert.ToDouble(o);
                if (v > 0)
                {
                    Logger.DebugFormat("    converted: {0}", v);
                    Logger.DebugFormat("      multiplying by {0} and converting to Int32: {1}", multiplier, Convert.ToInt32(v * multiplier)); 
                }
                return Convert.ToInt32(v * multiplier);
            }
            catch (Exception ex)
            {
                Logger.Error("Field `" + fieldname + "` value could not be converted to Int64: " + o);
                throw;
            }
        }

        protected object ConvertToInt32(object o, string fieldname, object defaultValue = null)
        {
            try
            {
                return Convert.IsDBNull(o) ? (object)null : Convert.ToInt32(o);
            }
            catch (OverflowException oex)
            {
                Logger.Error("Field `" + fieldname + "` value to large/small for Int32: " + o);
                {
                    return defaultValue;
                }
                throw;
            }
            catch (Exception)
            {
                Logger.Error("Field `" + fieldname + "` value could not be converted to Int32: " + o);
                if (defaultValue != null)
                {
                    return defaultValue;
                }
                throw;
            }
        }

        protected string CheckIfStringFitLength(object o, string fieldname, int maxLength, bool hideException = false)
        {
            if (!Convert.IsDBNull(o))
            {
                string s = (string)o;
                if (s.Length > maxLength)
                {
                    if (hideException == false)
                    {
                        throw new ArgumentOutOfRangeException(s + " exceed max length (" + maxLength + ") allowed for field: " + fieldname);
                    }
                    Logger.ErrorFormat("  '{0}' exceed length of {1} allowed for field `{2}`", s, maxLength, fieldname);
                    return s.Substring(0, maxLength);
                }
                return s;
            }
            return null;
        }


    }
}
