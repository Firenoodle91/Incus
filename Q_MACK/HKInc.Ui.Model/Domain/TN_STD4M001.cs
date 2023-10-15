using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_STD4M001T")]
    public class TN_STD4M001 : BaseDomain.MES_BaseDomain
    {
        public TN_STD4M001() { }

        [Key, Column("L4MNO", Order = 0)] public string L4mno { get; set; }
        [Key,Column("ITEM_CODE",Order =1)] public string ItemCode { get; set; }
        [Key,Column("SEQ",Order =2)] public Nullable<int> Seq { get; set; }
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Column("CAR_TYPE")] public string CarType { get; set; }
        [Column("CHG_DATE")] public Nullable<DateTime> ChgDate { get; set; }
        [Column("CHG_CUST")] public string ChgCust { get; set; }
        [Column("CHG_NOTE")] public string ChgNote { get; set; }
        [Column("CHG_MEMO")] public string ChgMemo { get; set; }
        [Column("REQ_CUST")] public string ReqCust { get; set; }
        [Column("REQ_USER")] public string ReqUser { get; set; }
        [Column("CHK_CUST_1CHA")] public string ChkCust1cha { get; set; }
        [Column("CHK_DATE_1CHA")] public Nullable<DateTime> ChkDate1cha { get; set; }
        [Column("CHK_QC_1CHA")] public string ChkQc1cha { get; set; }
        [Column("CHK_QC_USER_1CHA")] public string ChkQcuser1cha { get; set; }
        [Column("CHK_QC_FILE_1CHA")] public string ChkQcfile1cha { get; set; }
        [Column("CHK_CUST_2CHA")] public string ChkCust2cha { get; set; }
        [Column("CHK_DATE_2CHA")] public Nullable<DateTime> ChkDate2cha { get; set; }
        [Column("CHK_QC_2CHA")] public string ChkQc2cha { get; set; }
        [Column("CHK_QC_USER_2CHA")] public string ChkQcuser2cha { get; set; }
        [Column("CHK_QC_FILE_2CHA")] public string ChkQcfile2cha { get; set; }
        [Column("CHK_CUST_3CHA")] public string ChkCust3cha { get; set; }
        [Column("CHK_DATE_3CHA")] public Nullable<DateTime> ChkDate3cha { get; set; }
        [Column("CHK_QC_3CHA")] public string ChkQc3cha { get; set; }
        [Column("CHK_QC_USER_3CHA")] public string ChkQcuser3cha { get; set; }
        [Column("CHK_QC_FILE_3CHA")] public string ChkQcfile3cha { get; set; }
        [Column("FINAL_USER")] public string FinalUser { get; set; }
        [Column("PROD_WORK_DATE")] public Nullable<DateTime> ProdWorkdate { get; set; }
        [Column("REQ_DOC")] public string ReqDoc { get; set; }
        [Column("ETC_FILE1")] public string EtcFile1 { get; set; }
        [Column("ETC_FILE2")] public string EtcFile2 { get; set; }
        [Column("ETC_FILE3")] public string EtcFile3 { get; set; }
        [Column("ETC_FILE4")] public string EtcFile4 { get; set; }
        [Column("ETC_FILE5")] public string EtcFile5 { get; set; }
        [Column("MEMO1")] public string Memo1 { get; set; }
        [Column("MEMO2")] public string Memo2 { get; set; }
        [Column("MEMO3")] public string Memo3 { get; set; }
        [Column("MEMO4")] public string Memo4 { get; set; }

        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}