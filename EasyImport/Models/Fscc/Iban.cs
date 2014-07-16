using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models.Fscc
{
    public class Iban : DbRecord
    {
        public int ContractId { get; set; }
        public int Type { get; set; }
        public string AccNumber { get; set; }
        public string BankCode { get; set; }
        public bool DirectDebit { get; set; }
        public string DirectDebitNo { get; set; }
    }
}
