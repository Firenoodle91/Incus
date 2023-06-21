using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 거래처관리
    /// </summary>
    [Table("TN_STD1400T")]
    public class TN_STD1400 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1400()
        {
            TN_STD1401List = new List<TN_STD1401>();
        }

        [Key, Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }  //거래처코드
        [Column("CUSTOMER_NAME"), Required(ErrorMessage = "CustomerName")] public string CustomerName { get; set; }  //거래처명
        [Column("CUSTOMER_NAME_ENG")] public string CustomerNameENG { get; set; }  //거래처명(영문)
        [Column("CUSTOMER_NAME_CHN")] public string CustomerNameCHN { get; set; }  //거래처명(중문)
        [Column("CUSTOMER_TYPE")] public string CustomerType { get; set; }  //거래처구분
        [Column("REGISTRATION_NO")] public string RegistrationNo { get; set; }  //사업자등록번호
        [Column("MY_COMPANY_FLAG")] public string MyCompanyFlag { get; set; }  //본사여부
        [Column("CORPORATION_NO")] public string CorporationNo { get; set; }  //법인등록번호
        [Column("EMAIL")] public string Email { get; set; }  //E-Mail
        [Column("CUSTOMER_CATEGORY_TYPE")] public string CustomerCategoryType { get; set; }  //업태
        [Column("CUSTOMER_CATEGORY_CODE")] public string CustomerCategoryCode { get; set; }  //업종
        [Column("NATIONAL_CODE")] public string NationalCode { get; set; }  //국가
        [Column("REPRESENTATIVE_NAME")] public string RepresentativeName { get; set; }  //대표자명
        [Column("ZIP_CODE")] public string ZipCode { get; set; }  //우편번호
        [Column("ADDRESS")] public string Address { get; set; }  //주소1
        [Column("ADDRESS2")] public string Address2 { get; set; }  //주소2
        [Column("PHONE_NUMBER")] public string PhoneNumber { get; set; }  //전화번호
        [Column("FAX_NUMBER")] public string FaxNumber { get; set; }  //팩스
        [Column("MANAGER_NAME")] public string ManagerName { get; set; }  //담당자
        [Column("MANAGER_PHONE_NUMBER")] public string ManagerPhoneNumber { get; set; }  //담당자연락처
        [Column("MANAGER_IN_PHONE_NUMBER")] public string ManagerInPhoneNumber { get; set; }  //담당자연락처(내선)
        [Column("CUSTOMER_BANK_CODE")] public string CustomerBankCode { get; set; }  //주거래은행
        [Column("ACCOUNT_NUMBER")] public string AccountNumber { get; set; }  //계좌번호
        [Column("BUSINESS_MANAGEMENT_ID")] public string BusinessManagementId { get; set; }  //영업관리자
        [Column("TRADING_START_DATE")] public DateTime? TradingStartDate { get; set; }  //거래시작일
        [Column("TRADING_END_DATE")] public DateTime? TradingEndDate { get; set; }  //거래종료일
        [Column("DEAD_LINE")] public string DeadLine { get; set; }  //마감일
        [Column("MEMO")] public string Memo { get; set; }  //메모
        [Column("HOMEPAGE")] public string Homepage { get; set; }  //홈페이지
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }  //사용여부

        public virtual ICollection<TN_STD1401> TN_STD1401List { get; set; }
    }
}