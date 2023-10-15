using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_STD1400T")]
    public class TN_STD1400 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1400() { }

        [Key, Column("CUSTOMER_CODE",Order =0), StringLength(20), Required(ErrorMessage = "Customer Code is required.")] public string CustomerCode { get; set; }
        [Column("CUSTOMER_NAME"), StringLength(100)] public string CustomerName { get; set; }
        [Column("DEFAULT_COMPANY_PLAG")] public string DefaultCompanyPlag { get; set; }
        [Column("CUSTOMER_CATEGORY_CODE")] public string CustomerCategoryCode { get; set; }
        [Column("CUSTOMER_CATEGORY_TYPE")] public string CustomerCategoryType { get; set; }
        [Column("NATIONAL_CODE")] public string NationalCode { get; set; }
        [Column("REPRESENTATIVE_NAME")] public string RepresentativeName { get; set; }
        [Column("REGISTRATION_NO")] public string RegistrationNo { get; set; }
        [Column("TELEPHONE")] public string Telephone { get; set; }
        [Column("ZIP_CODE")] public string ZipCode { get; set; }
        [Column("ADDRESS")] public string Address { get; set; }
        [Column("ADDRESS2")] public string Address2 { get; set; }
        [Column("CUSTOMER_BANK_CODE")] public string CustomerBankCode { get; set; }
        [Column("ACCOUNT_NUMBER")] public string AccountNumber { get; set; }
        [Column("FAX")] public string Fax { get; set; }
        [Column("EMP_CODE")] public string EmpCode { get; set; }
        [Column("EMP_IN_TELEPHONE")] public string EmpInTelephone { get; set; }
        [Column("EMP_TELEPHONE")] public string EmpTelephone { get; set; }
        [Column("EMAIL")] public string Email { get; set; }
        [Column("CORPORATION_NO")] public string CorporationNo { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("USE_PLAG"), Required(ErrorMessage = "UseFlag is required.")] public string UseFlag { get; set; }
        [Column("CUST_TYPE")] public string CustType { get; set; }

        [NotMapped] public string ReqCustcode { get { return CustomerCode; } }
    }
}
