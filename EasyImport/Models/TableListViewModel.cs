using EasyImport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models
{
    public class TableListViewModel
    {

        public string DatasourcFilename { get; set; }

        public IList<DbTable> Tables { get; set; }

    }
}
