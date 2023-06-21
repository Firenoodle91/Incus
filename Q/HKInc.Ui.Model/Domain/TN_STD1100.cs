using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_STD1100T")]
    public class TN_STD1100 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1100()
        {
            QCT1000List = new List<TN_QCT1000>();
            TN_UPH1000List = new List<TN_UPH1000>();
        }
        
        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Column("ITEM_NM1")] public string ItemNm1 { get; set; }
        [Column("ITEM_GBN")] public string ItemGbn { get; set; }
        [Column("TOP_CATEGORY")] public string TopCategory { get; set; }
        [Column("MIDDLE_CATEGORY")] public string MiddleCategory { get; set; }
        [Column("BOTTOM_CATEGORY")] public string BottomCategory { get; set; }
        [Column("SPEC_1")] public string Spec1 { get; set; }
        [Column("SPEC_2")] public string Spec2 { get; set; }
        [Column("SPEC_3")] public string Spec3 { get; set; }
        [Column("SPEC_4")] public string Spec4 { get; set; }
        [Column("UNIT")] public string Unit { get; set; }
        [Column("WEIGHT")] public decimal Weight { get; set; }
        [Column("SAFE_QTY")] public decimal SafeQty { get; set; }
        [Column("SELF_INSP_YN")] public string SelfInspyn { get; set; }
        [Column("STOCK_INSP_YN")] public string StockInspyn { get; set; }
        [Column("PROC_INSP_YN")] public string ProcInspyn { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("ETCFILE")] public byte[] Etcfile { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("TEMP3")] public string Temp3 { get; set; }
        [Column("TEMP4")] public string Temp4 { get; set; } //수입검사
        [Column("TEMP5")] public string Temp5 { get; set; } //팀코드
        [Column("TEMP6")] public string Temp6 { get; set; }
        [Column("SRC_CODE")] public string SrcCode { get; set; }
        [Column("MAIN_MC")] public string MainMc { get; set; }
        [Column("SRC_QTY")] public decimal SrcQty { get; set; }
        [Column("PROC_CNT")] public Nullable<int> ProcCnt { get; set; }
        [Column("MAIN_CUST")] public string MainCust { get; set; }
        [Column("CUST_ITEMCODE")] public string CustItemCode { get; set; }
        [Column("CUST_ITEMNM")] public string CustItemNm { get; set; }
        [Column("LCTYPE")] public string Lctype { get; set; }

        [NotMapped]
        public string fullName1 { get {
                if (TopCategory == "P03")
                {
                    return ItemNm;
                }
                else
                {
                    return ItemNm + ItemNm1 + Spec1;
                }

            }
        }
        [NotMapped]
        public string fullName
        {
            get
            {
                if (TopCategory == "P03")
                {
                    return ItemNm;
                }
                else
                {
                    return   ItemNm1 ;
                }

            }
        }

        [NotMapped]
        public string BomName
        {
            get
            {
               
                    return ItemNm1+"  ["+ItemCode+"]";
               

            }
        }
        public virtual ICollection<TN_QCT1000> QCT1000List { get; set; }
        public virtual ICollection<TN_UPH1000> TN_UPH1000List { get; set; }
    }
}
