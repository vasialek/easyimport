using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models.Fscc
{
    public class BankTransfer : DbRecord
    {
        public Int32 ContractId { get; set; }
        public Int32 CustId { get; set; }
        public String FileName { get; set; }
        public String TransferNo { get; set; }
        public String TransferLineNo { get; set; }
        public String PayerName { get; set; }
        public String CustIban { get; set; }
        public String PayerNo { get; set; }
        public String PrimaryPayerNo { get; set; }
        public String PrimaryPayerName { get; set; }
        public String RecipientIban { get; set; }
        public Int64 TransferAmount { get; set; }
        public DateTime TransferDt { get; set; }
        public String TransferComment { get; set; }
        public Boolean Netting { get; set; }
    }
}
