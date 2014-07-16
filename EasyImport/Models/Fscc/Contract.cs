using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyImport.Models.Fscc
{
    public class Contract : DbRecord
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
        public Int32 ContractId { get; set; }
        public Int16 ContractType { get; set; }
        public String ContractName { get; set; }
        public Int32 Address1Id { get; set; }
        public String Address1Street { get; set; }
        public String Address1City { get; set; }
        public String Address1Country { get; set; }
        public String Address1PostCode { get; set; }
        public String Address1Name { get; set; }
        public Int32 Address2Id { get; set; }
        public String Address2Street { get; set; }
        public String Address2City { get; set; }
        public String Address2Country { get; set; }
        public String Address2PostCode { get; set; }
        public String Address2Name { get; set; }
        public Int32 ParentId { get; set; }
        public String ExtNavCode { get; set; }
        public String ContractNo { get; set; }
        public Int32 FinType { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public Int32 AgentId { get; set; }
        public Int16 State { get; set; }
        public String Curr { get; set; }
        public Int32 CurrRateProviderId { get; set; }
        public Int16 InvFreq { get; set; }
        public Int32 InvSeriesId { get; set; }
        public Int32 InvTemplateId { get; set; }
        public Boolean SendInv { get; set; }
        public Boolean InvShowDt { get; set; }
        public DateTime CredDtFrom { get; set; }
        public Int64 MaxDebtBlock { get; set; }
        public Int64 MaxDebtDisc { get; set; }
        public Int16 PayTerm { get; set; }
        public Int16 CredTerm { get; set; }
        public Int16 PayDelay { get; set; }
        public Decimal Fine { get; set; }
        public Boolean DiscCheckPay { get; set; }
        public Boolean DiscCurrPeriod { get; set; }
        public Boolean DiscForeign { get; set; }
        public Boolean TransferAuto { get; set; }
        public String MngName { get; set; }
        public String MngPosition { get; set; }
        public String MngPhone { get; set; }
        public String MngEmail { get; set; }
        public String MngDescription { get; set; }
        public String BlockReason { get; set; }
        public Int32 ContractGroupId { get; set; }
        public Boolean InvSendEmail { get; set; }
        public Boolean DataTempChanged { get; set; }
        public Int32 InvCommentId { get; set; }
        public Int64 AggFuelQuantity { get; set; }
        public Int16 AggPenaltyType { get; set; }
        public Boolean SendAdvertise { get; set; }
        public String DdAgreementNo { get; set; }
        public Int64 CreditLimit { get; set; }
        public String CampaignCode { get; set; }
        public String EinvoiceAddress { get; set; }
        public String SalesPerson { get; set; }
    }
}
