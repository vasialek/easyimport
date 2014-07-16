using EasyImport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyImport.DataReader
{
    public class DatabaseHelper
    {

        private static Regex _rxVaryingType = new Regex(@"(varchar|nvarchar|varbinary)(\s*)(\()(\s*)(\d+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        private static Regex _rxTypeWithPrecision = new Regex(@"(decimal)(\s*)(\()(\d+)(\s*)(,)(\d+)(\s*)(\))", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        public enum Databases { MsSql, MsAccess }

        /// <summary>
        /// Input should be in MS SQL format, without braces. Varchar/Varbinary types will be modified to have MAX size
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ParseFsccDataType(string t)
        {
            // int, varchar(19), varbinary(20)
            Match m = _rxVaryingType.Match(t);
            if (m.Success)
	        {
		        return string.Format("[{0}](MAX)", m.Groups[1].Value, m.Groups[5].Value);
	        }
            m = _rxTypeWithPrecision.Match(t);
            if (m.Success)
            {
                return string.Format("[{0}]({1}, {2})", m.Groups[1].Value, m.Groups[4].Value, m.Groups[7].Value);
            }
            return string.Concat("[", t, "]");
        }

        public static DbTypeIso ConvertToType(string rawType/*, Databases inputType, Databases outputType*/)
        {
            if (rawType.StartsWith("system.", StringComparison.InvariantCultureIgnoreCase))
            {
                rawType = rawType.Substring("system.".Length);
            }

            rawType = rawType.ToLower();
            switch (rawType)
            {
                case "string":
                    return DbTypeIso.NATIONAL_CHARACTER_VARYING;
                case "int32":
                case "int":
                    return DbTypeIso.INTEGER;
                case "datetime":
                    return DbTypeIso.DATE;
                case "byte":
                    return DbTypeIso.SMALLINT;
                case "double":
                    return DbTypeIso.FLOAT;
                case "boolean":
                    return DbTypeIso.BOOLEAN;
                case "byte[]":
                    return DbTypeIso.BINARY_VARYING;
                case "decimal":
                    return DbTypeIso.DECIMAL;
            }

            throw new ArgumentOutOfRangeException("Could not convert to Database type: " + rawType);
        }

        public static string GetCreateTableSql(DbTable table, bool useCodeFormatting)
        {
            StringBuilder sb = new StringBuilder();
            string separator = useCodeFormatting ? Environment.NewLine : "";

            sb.AppendFormat("create table [dbo].[{0}]{1}(", table.TableName, separator);
            for (int i = 0; i < table.Fields.Count; i++)
            {
                sb.Append(table.Fields[i].GetDbCreate());
                if (i != table.Fields.Count - 1)
                {
                    sb.Append(",");
                }
                if (useCodeFormatting)
                {
                    separator = string.Concat(Environment.NewLine, "    ");
                    sb.Append(separator);
                }
            }
            sb.Append(");");


            return sb.ToString();
        }

        public static void BulkIt(string tableName, DataTable sourceData, SqlConnection con, SqlTransaction tran)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran))
            {
                for (int a = 0; a < sourceData.Columns.Count; a++)
                {
                    bulkCopy.ColumnMappings.Add(sourceData.Columns[a].ColumnName, sourceData.Columns[a].ColumnName);
                }
                //bulkCopy.DestinationTableName = "dbo." + tableName;
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.BulkCopyTimeout = 30000;
                bulkCopy.WriteToServer(sourceData);
            }
        }

        public static void BulkIt(string tableName, DataTable sourceData, SqlConnection con, Dictionary<string, string> mapping)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            using (SqlTransaction t = con.BeginTransaction())
            {
                if (mapping == null)
                {
                    BulkIt(tableName, sourceData, con, t);
                }else
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, t))
                    {
                        foreach (var kvp in mapping)
                        {
                            // source => destination
                            bulkCopy.ColumnMappings.Add(kvp.Key, kvp.Value);
                        }
                        //bulkCopy.DestinationTableName = "dbo." + tableName;
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.BulkCopyTimeout = 30000;
                        bulkCopy.WriteToServer(sourceData);
                    } 
                }
                t.Commit();
            }
        }

        public static string DumpDataTable(DataTable dt, int limitRows)
        {
            StringBuilder sb = new StringBuilder();

            // Header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                // +5 additional chars to end of column length
                sb.Append(dt.Columns[i].ColumnName).Append("   | ");
            }
            sb.AppendLine();

            int total = Math.Min(limitRows, dt.Rows.Count);
            for (int i = 0; i < total; i++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    string s = dt.Rows[i][c].ToString().Trim();
                    int len = dt.Columns[c].ColumnName.Length;
                    if (s.Length < len)
                    {
                        sb.Append(s.PadRight(len)).Append("   | ");
                    }
                    else
                    {
                        sb.Append(s.Substring(0, len)).Append("...| ");
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string DumpTableStructure(IList<IDbField> fields)
        {
            var sb = new StringBuilder();

            foreach (var f in fields)
            {
                sb.AppendFormat("  {0} ({1})", f.FieldName, f.FieldType).AppendLine();
            }

            return sb.ToString();
        }
    }
}
