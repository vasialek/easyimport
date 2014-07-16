using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport
{
    public class ValidateFailedException : Exception
    {

        public ValidateFailedException(string msg)
            : base(msg)
        {
        }

    }
}
