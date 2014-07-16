using EasyImport.Models;
using EasyImport.Models.Fscc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.DataReader
{
    public class DataTransformer
    {
        public Iban ConvertToIban(DbRecord r)
        {
            return new Iban {
                ContractId = Convert.ToInt32(r.Values[0]),
                Type = Convert.ToInt32(r.Values[1]),
                AccNumber = Convert.ToString(r.Values[2]),
                BankCode = Convert.ToString(r.Values[3]),
                DirectDebit = Convert.ToBoolean(r.Values[4]),
            };
        }

        public Customer ConvertToCustomer(DbRecord r)
        {
            return new Customer {
                
            };
        }
    }
}
