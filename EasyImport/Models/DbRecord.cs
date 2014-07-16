using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models
{
    public class DbRecord
    {
        public object[] Values { get; protected set; }

        public DbRecord()
            : this(null)
        {
        }

        public DbRecord(object[] values)
        {
            Values = values;
        }
    }
}
