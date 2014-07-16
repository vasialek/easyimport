using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport
{
    public class Settings
    {
        #region " Name / Version "
        public static string Name = "EasyImport";
        public static string Version = "0.6.9";
        public static string NameVersion
        {
            get { return string.Format("{0} (v{1})", Name, Version); }
        }
        #endregion

    }
}
