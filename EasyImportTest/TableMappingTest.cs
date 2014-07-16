using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EasyImport.DataReader;
using System.Data;

namespace EasyImportTest
{
    [TestClass]
    public class TableMappingTest
    {

        private DataTable _table = null;

        [TestInitialize]
        public void Setup()
        {
            _table = new DataTable();
            _table.Columns.Add("col_111");
            _table.Columns.Add("col_222");
            _table.Columns.Add("col_333");
            _table.Columns.Add("col_444");
            _table.Columns.Add("col_555");

            for (int i = 1; i < 6; i++)
            {
                _table.Rows.Add(new object[] {
                    i * 11111,
                    (char)(65 + i),
                    new DateTime(2000 + i, i, i),
                    i * 11111,
                    i * 11111,
                });
            }
        }

        [TestMethod]
        public void Test_Simple_TransformTable()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111"));
            mapping.Add(new DataImport.TransformationMap("col_222"));
            mapping.Add(new DataImport.TransformationMap("col_333"));

            var map = new DataImportTst().BuilTransformation(_table, mapping);

            Assert.AreEqual(mapping.Count, map.Length);

            Assert.AreEqual(0, map[0].SourceFieldIndex, "First source field should be 0");
            Assert.AreEqual(0, map[0].DestinationFieldIndex, "First destination field should be 0");
            Assert.AreEqual(1, map[1].SourceFieldIndex, "Second source field should be 1");
            Assert.AreEqual(1, map[1].DestinationFieldIndex, "Second destination field should be 1");
            Assert.AreEqual(2, map[2].SourceFieldIndex, "Third source field should be 2");
            Assert.AreEqual(2, map[2].DestinationFieldIndex, "Third destination field should be 2");
        }

        [TestMethod]
        public void Test_Whole_TransformTable()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111"));
            mapping.Add(new DataImport.TransformationMap("col_222"));
            mapping.Add(new DataImport.TransformationMap("col_333"));
            mapping.Add(new DataImport.TransformationMap("col_444"));
            mapping.Add(new DataImport.TransformationMap("col_555"));

            var map = new DataImportTst().BuilTransformation(_table, mapping);

            Assert.AreEqual(mapping.Count, map.Length);
            foreach (var m in map)
            {
                Assert.AreEqual(m.SourceFieldIndex, m.DestinationFieldIndex, "source and destination indexes should be equal");
                Assert.AreEqual(m.SourceField, m.DestinationField, "Source and destination fields should be same");
            }
        }

        [TestMethod]
        public void Destination_Fields_Differ()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111", "from_111"));
            mapping.Add(new DataImport.TransformationMap("col_222", "from_222"));
            mapping.Add(new DataImport.TransformationMap("col_333", "from_333"));

            var map = new DataImportTst().BuilTransformation(_table, mapping);

            Assert.AreEqual(mapping.Count, map.Length);
            foreach (var m in map)
            {
                Assert.AreEqual(m.SourceFieldIndex, m.DestinationFieldIndex, "source and destination indexes should be equal");
            }
        }

        [TestMethod]
        public void Take_Several_Fields()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111"));
            //mapping.Add(new DataImport.TransformationMap("col_222"));
            mapping.Add(new DataImport.TransformationMap("col_333"));
            //mapping.Add(new DataImport.TransformationMap("col_444"));
            mapping.Add(new DataImport.TransformationMap("col_555"));

            var map = new DataImportTst().BuilTransformation(_table, mapping);

            int sourceFieldIndex, destinationFieldIndex;

            Assert.AreEqual(mapping.Count, map.Length);
            
            // First field is mapped directly
            sourceFieldIndex = map[0].SourceFieldIndex;
            destinationFieldIndex = map[0].DestinationFieldIndex;
            Assert.AreEqual(0, sourceFieldIndex);
            Assert.AreEqual(0, destinationFieldIndex);

            // Second field is skipped, so source index is 3rd
            sourceFieldIndex = map[1].SourceFieldIndex;
            destinationFieldIndex = map[1].DestinationFieldIndex;
            Assert.AreEqual(2, sourceFieldIndex);
            Assert.AreEqual(1, destinationFieldIndex);
            Assert.AreEqual(_table.Columns[sourceFieldIndex].ColumnName, mapping[destinationFieldIndex].DestinationField);

            sourceFieldIndex = map[2].SourceFieldIndex;
            destinationFieldIndex = map[2].DestinationFieldIndex;
            Assert.AreEqual(4, sourceFieldIndex);
            Assert.AreEqual(2, destinationFieldIndex);
            Assert.AreEqual(_table.Columns[sourceFieldIndex].ColumnName, mapping[destinationFieldIndex].DestinationField);
        }

        [TestMethod]
        public void Simple_Rebuild_TableStructure()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111"));
            mapping.Add(new DataImport.TransformationMap("col_222"));
            mapping.Add(new DataImport.TransformationMap("col_333"));

            var newTable = new DataImportTst().TestRebuildDataTable(_table, mapping);

            Assert.AreEqual(mapping.Count, newTable.Columns.Count);

            Assert.AreEqual(mapping[0].DestinationField, _table.Columns[0].ColumnName);
            Assert.AreEqual(mapping[1].DestinationField, _table.Columns[1].ColumnName);
            Assert.AreEqual(mapping[2].DestinationField, _table.Columns[2].ColumnName);
        }

        [TestMethod]
        public void Rebuild_TableStructure()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111"));
            mapping.Add(new DataImport.TransformationMap("col_444"));
            mapping.Add(new DataImport.TransformationMap("col_333"));

            var newTable = new DataImportTst().TestRebuildDataTable(_table, mapping);

            Assert.AreEqual(mapping.Count, newTable.Columns.Count);

            Assert.AreEqual(mapping[0].DestinationField, _table.Columns[0].ColumnName);
            Assert.AreEqual(mapping[1].DestinationField, _table.Columns[3].ColumnName);
            Assert.AreEqual(mapping[2].DestinationField, _table.Columns[2].ColumnName);
        }

        [TestMethod]
        public void Rebuild_TableData_Simple()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111"));
            mapping.Add(new DataImport.TransformationMap("col_222"));
            mapping.Add(new DataImport.TransformationMap("col_333"));

            var newTable = new DataImportTst().TestRebuildDataTable(_table, mapping);

            Assert.AreEqual(mapping.Count, newTable.Columns.Count);

            Assert.AreEqual(_table.Rows[0][0], newTable.Rows[0][0]);
            Assert.AreEqual(_table.Rows[1][1], newTable.Rows[1][1]);
            Assert.AreEqual(_table.Rows[2][2], newTable.Rows[2][2]);
        }

        [TestMethod]
        public void Rebuild_TableData_Custom()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111"));
            //mapping.Add(new DataImport.TransformationMap("col_222"));
            mapping.Add(new DataImport.TransformationMap("col_333"));
            //mapping.Add(new DataImport.TransformationMap("col_444"));
            mapping.Add(new DataImport.TransformationMap("col_555"));

            var newTable = new DataImportTst().TestRebuildDataTable(_table, mapping);

            Assert.AreEqual(mapping.Count, newTable.Columns.Count);

            Assert.AreEqual(_table.Rows[0][0], newTable.Rows[0][0]);
            Assert.AreEqual(_table.Rows[1][2], newTable.Rows[1][1]);
            Assert.AreEqual(_table.Rows[2][4], newTable.Rows[2][2]);
        }

        [TestMethod]
        public void Rebuild_TableData_And_Rename()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111", "first"));
            //mapping.Add(new DataImport.TransformationMap("col_222"));
            mapping.Add(new DataImport.TransformationMap("col_333", "second"));
            //mapping.Add(new DataImport.TransformationMap("col_444"));
            mapping.Add(new DataImport.TransformationMap("col_555", "thrid"));

            var newTable = new DataImportTst().TestRebuildDataTable(_table, mapping);

            Assert.AreEqual(mapping.Count, newTable.Columns.Count);

            Assert.AreEqual(mapping[0].DestinationField, newTable.Columns[0].ColumnName);
            Assert.AreEqual(mapping[1].DestinationField, newTable.Columns[1].ColumnName);
            Assert.AreEqual(mapping[2].DestinationField, newTable.Columns[2].ColumnName);

            Assert.AreEqual(_table.Rows[0][0], newTable.Rows[0][0]);
            Assert.AreEqual(_table.Rows[1][2], newTable.Rows[1][1]);
            Assert.AreEqual(_table.Rows[2][4], newTable.Rows[2][2]);
        }

        [TestMethod]
        public void Transform_Data_One_Column()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("col_111"));
            mapping.Add(new DataImport.TransformationMap("col_222"));
            mapping.Add(new DataImport.TransformationMap("col_333", "year", new Func<object, object>(x => { return ConvertToStringYear(x); })));
            mapping.Add(new DataImport.TransformationMap("col_444"));

            var newTable = new DataImportTst().TestRebuildDataTable(_table, mapping);

            Assert.AreEqual(mapping.Count, newTable.Columns.Count);
            for (int i = 0; i < newTable.Rows.Count; i++)
            {
                string year = Convert.ToDateTime(_table.Rows[i]["col_333"]).Year.ToString();
                Assert.AreEqual(year, newTable.Rows[i]["year"].ToString());
            }
        }

        [TestMethod]
        public void Transform_Customer_Table()
        {
            var mapping = new List<DataImport.TransformationMap>();
            mapping.Add(new DataImport.TransformationMap("cust_id"));
            mapping.Add(new DataImport.TransformationMap("name"));
            mapping.Add(new DataImport.TransformationMap("cust_type"));
            mapping.Add(new DataImport.TransformationMap("company_type"));
            mapping.Add(new DataImport.TransformationMap("reg_code"));
            mapping.Add(new DataImport.TransformationMap("vat_code"));
            mapping.Add(new DataImport.TransformationMap("cust_ref_no"));
            mapping.Add(new DataImport.TransformationMap("lang"));
            mapping.Add(new DataImport.TransformationMap("address_id"));
            mapping.Add(new DataImport.TransformationMap("address_street"));
            mapping.Add(new DataImport.TransformationMap("address_city"));
            mapping.Add(new DataImport.TransformationMap("address_country"));
            mapping.Add(new DataImport.TransformationMap("address_post_code"));
            mapping.Add(new DataImport.TransformationMap("address_name"));
            mapping.Add(new DataImport.TransformationMap("phone"));
            mapping.Add(new DataImport.TransformationMap("fax"));
            mapping.Add(new DataImport.TransformationMap("email"));
            mapping.Add(new DataImport.TransformationMap("branch_id"));
            mapping.Add(new DataImport.TransformationMap("cust_group_id"));
            mapping.Add(new DataImport.TransformationMap("description"));
            mapping.Add(new DataImport.TransformationMap("web_username"));
            mapping.Add(new DataImport.TransformationMap("web_psw"));
            mapping.Add(new DataImport.TransformationMap("insurance_dt"));
            mapping.Add(new DataImport.TransformationMap("insurance_amount"));
            mapping.Add(new DataImport.TransformationMap("decision_dt"));
            mapping.Add(new DataImport.TransformationMap("decision_no"));
            mapping.Add(new DataImport.TransformationMap("decision_type"));
            mapping.Add(new DataImport.TransformationMap("guarantee_type"));
            mapping.Add(new DataImport.TransformationMap("guarantee_bank"));
            mapping.Add(new DataImport.TransformationMap("guarantee_amount"));
            mapping.Add(new DataImport.TransformationMap("guarantee_valid_from"));
            mapping.Add(new DataImport.TransformationMap("guarantee_valid_to"));
            mapping.Add(new DataImport.TransformationMap("guarantee_description"));
            mapping.Add(new DataImport.TransformationMap("ins_decision_date"));
            mapping.Add(new DataImport.TransformationMap("insf_evaluated_dt"));
            mapping.Add(new DataImport.TransformationMap("insf_limit_amount"));
            mapping.Add(new DataImport.TransformationMap("insf_risk_class"));
            mapping.Add(new DataImport.TransformationMap("insf_monitoring"));
            mapping.Add(new DataImport.TransformationMap("insf_last_mon_dt"));
            mapping.Add(new DataImport.TransformationMap("insf_next_mon_dt"));
            mapping.Add(new DataImport.TransformationMap("mng_name"));
            mapping.Add(new DataImport.TransformationMap("mng_position"));
            mapping.Add(new DataImport.TransformationMap("mng_phone"));
            mapping.Add(new DataImport.TransformationMap("mng_email"));
            mapping.Add(new DataImport.TransformationMap("mng_description"));
            mapping.Add(new DataImport.TransformationMap("send_advertise"));

            var data = new DataTable();
            var customers = new DataReaderTst().GetRecords("customers", data);
            var map = new DataImportTst().BuilTransformation(customers, mapping);
            var customersToTransfer = new DataImportTst().TestRebuildDataTable(customers, mapping);

            Assert.AreEqual(mapping.Count, map.Length);
            Assert.AreEqual(map.Length, customersToTransfer.Columns.Count);

        }

        private object ConvertToStringYear(object dt)
        {
            return Convert.ToDateTime(dt).Year.ToString();
        }
    }
}
