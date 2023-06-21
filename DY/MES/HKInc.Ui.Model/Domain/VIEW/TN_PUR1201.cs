using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재입고 디테일</summary>	
    [Table("TN_PUR1201T")]
    public class TN_PUR1201 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1201()
        {
            TN_PUR1301List = new List<TN_PUR1301>();
            InInspectionState = "";
        }
        /// <summary>입고번호</summary>               
        [ForeignKey("TN_PUR1200"), Key, Column("IN_NO", Order = 0), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>입고순번</summary>               
        [Key, Column("IN_SEQ", Order = 1), Required(ErrorMessage = "InSeq")] public int InSeq { get; set; }
        /// <summary>발주번호</summary>               
        [ForeignKey("TN_PUR1101"), Column("PO_NO", Order = 2)] public string PoNo { get; set; }
        /// <summary>발주순번</summary>               
        [ForeignKey("TN_PUR1101"), Column("PO_SEQ", Order = 3)] public int? PoSeq { get; set; }
        /// <summary>품번(도번)</summary>             
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>입고량</summary>                 
        [Column("IN_QTY"), Required(ErrorMessage = "InQty")] public decimal InQty { get; set; }
        /// <summary>입고단가</summary>               
        [Column("IN_COST")] public decimal? InCost { get; set; }
        /// <summary>입고 LOT NO</summary>            
        [Column("IN_LOT_NO"), Required(ErrorMessage = "InLotNo")] public string InLotNo { get; set; }
        /// <summary>납품처 LOT NO</summary>          
        [Column("IN_CUSTOMER_LOT_NO")] public string InCustomerLotNo { get; set; }
        /// <summary>입고창고</summary>               
        [Column("IN_WH_CODE")] public string InWhCode { get; set; }
        /// <summary>입고위치</summary>               
        [Column("IN_WH_POSITION")] public string InWhPosition { get; set; }
        /// <summary>라벨수</summary>                 
        [Column("PRINT_QTY")] public int? PrintQty { get; set; }
        /// <summary>입고확정여부</summary>                   
        [Column("IN_CONFIRM_FLAG")] public string InConfirmFlag { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>협력사메모</summary>                
        [Column("MEMO1")] public string Memo1 { get; set; }
        /// <summary>입고 LOT NO(모)</summary>                   
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                  
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                  
        [Column("TEMP2")] public string Temp2 { get; set; }
        /// <summary>미입고확정여부</summary>                   
        [Column("NOT_IN_CONFIRM_FLAG")] public string NotInConfirmFlag { get; set; }
        /// <summary>파일명</summary>                    
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>                   
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>수입검사여부</summary>                  
        [NotMapped] public string InInspectionState { get; set; }
        /// <summary>재출고 여부(자동차감)</summary>                  
        [Column("REOUT_YN")] public string ReOutYn { get; set; }



        public virtual TN_PUR1101 TN_PUR1101 { get; set; }
        public virtual TN_PUR1200 TN_PUR1200 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual ICollection<TN_PUR1301> TN_PUR1301List { get; set; }
    }
}