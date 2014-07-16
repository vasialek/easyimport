using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models
{
    public class DbTable
    {
        public string TableName { get; set; }
        public int TotalRecords { get; set; }
        public IList<IDbField> Fields { get; set; }

        public DbTable()
        {
            Fields = new List<IDbField>();
        }
    }
}
