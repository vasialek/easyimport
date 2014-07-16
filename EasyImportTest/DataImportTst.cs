using EasyImport.DataReader;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImportTest
{
    public class DataImportTst : DataImport
    {

        private ILog _logger = null;
        private ILog Logger
        {
            get
            {
                if(_logger == null)
                {
                    _logger = LogManager.GetLogger(typeof(DataImportTst));
                    log4net.Config.XmlConfigurator.Configure();
                }
                return _logger;
            }
        }

        public string TestGetPriceAsMinor(decimal p)
        {
            return this.GetPriceAsMinor(p);
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

        public DataTable TestRebuildDataTable(DataTable sourceTable, IList<TransformationMap> mapping)
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
                // Each column which is mapped
                for (int i = 0; i < map.Length; i++)
                {
                    if (map[i] != null)
                    {
                        var transformationF = map[i].Transformation;
                        if (transformationF != null)
                        {
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
    }
}
