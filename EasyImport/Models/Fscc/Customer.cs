using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models.Fscc
{
    public class Customer : DbRecord
    {
        public Int32 CustId { get; set; }
        public String Name { get; set; }
        public Int16 CustType { get; set; }
        public Int16 CompanyType { get; set; }
        public String RegCode { get; set; }
        public String VatCode { get; set; }
        public String CustRefNo { get; set; }
        public Int32 Lang { get; set; }
        public Int32 AddressId { get; set; }
        public String AddressStreet { get; set; }
        public String AddressCity { get; set; }
        public String AddressCountry { get; set; }
        public String AddressPostCode { get; set; }
        public String AddressName { get; set; }
        public String Phone { get; set; }
        public String Fax { get; set; }
        public String Email { get; set; }
        public Int32 BranchId { get; set; }
        public Int32 CustGroupId { get; set; }
        public String Description { get; set; }
        public String WebUsername { get; set; }
        public String WebPsw { get; set; }
        public DateTime InsuranceDt { get; set; }
        public Int32 InsuranceAmount { get; set; }
        public DateTime DecisionDt { get; set; }
        public String DecisionNo { get; set; }
        public Int32 DecisionType { get; set; }
        public Int32 GuaranteeType { get; set; }
        public String GuaranteeBank { get; set; }
        public Int32 GuaranteeAmount { get; set; }
        public DateTime GuaranteeValidFrom { get; set; }
        public DateTime GuaranteeValidTo { get; set; }
        public String GuaranteeDescription { get; set; }
        public DateTime InsDecisionDate { get; set; }
        public DateTime InsfEvaluatedDt { get; set; }
        public Int32 InsfLimitAmount { get; set; }
        public Int32 InsfRiskClass { get; set; }
        public Int32 InsfMonitoring { get; set; }
        public DateTime InsfLastMonDt { get; set; }
        public DateTime InsfNextMonDt { get; set; }
        public String MngName { get; set; }
        public String MngPosition { get; set; }
        public String MngPhone { get; set; }
        public String MngEmail { get; set; }
        public String MngDescription { get; set; }
        public Boolean SendAdvertise { get; set; }
    }
}
