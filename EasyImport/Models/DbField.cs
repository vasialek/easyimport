using EasyImport.DataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models
{

    public interface IDbField
    {
        string FieldName { get; set; }
        string FieldTypeRaw { get; set; }
        DbTypeIso FieldType { get; }
        int FieldSize { get; set; }
        bool AllowNull { get; set; }
        string GetDbCreate();
    }

    public class MsDbField : IDbField
    {
        public string FieldName { get; set; }
        public string FieldTypeRaw { get; set; }
        public DbTypeIso FieldType { get { return string.IsNullOrEmpty(FieldTypeRaw) ? DbTypeIso.__NOT_SET__ : DatabaseHelper.ConvertToType(FieldTypeRaw); } }
        public int FieldSize { get; set; }
        public bool AllowNull { get; set; }

        public MsDbField()
            : this(null, null)
        {
        }

        public MsDbField(string fieldName, string fieldType)
        {
            FieldName = fieldName;
            FieldTypeRaw = fieldType;
        }

        public string GetDbCreate()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("  [{0}] [{1}]", FieldName, FieldType);
            if (FieldType.Equals("nvarchar") || FieldType.Equals("varchar"))
            {
                sb.Append(" (MAX)");
            }

            return sb.ToString();
        }
    }
}
