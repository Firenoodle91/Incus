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
    /// <summary>자재반품 디테일</summary>	
    [Table("TN_PUR1305T")]
    public class TN_PUR1305 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1305()
        {
            
        }
        /// <summary>반품번호</summary>               
        [ForeignKey("TN_PUR1304"), Key, Column("RETURN_NO", Order = 0), Required(ErrorMessage = "ReturnNo")] public string ReturnNo { get; set; }
        /// <summary>반품순번</summary>               
        [Key, Column("RETURN_SEQ", Order = 1), Required(ErrorMessage = "ReturnSeq")] public int ReturnSeq { get; set; }
        /// <summary>입고번호</summary>               
        [Column("IN_NO")] public string InNo { get; set; }
        /// <summary>입고순번</summary>               
        [Column("IN_SEQ")] public int InSeq { get; set; }
        /// <summary>발주번호</summary>               
        [Column("PO_NO")] public string PoNo { get; set; }
        /// <summary>발주순번</summary>               
        [Column("PO_SEQ")] public int PoSeq { get; set; }
        /// <summary>수입검사번호</summary>           
        [Column("INSP_NO")] public string InspNo { get; set; }
        /// <summary>품번(도번)</summary>             
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>입고량</summary>                 
        [Column("IN_QTY")] public decimal InQty { get; set; }
        /// <summary>반품가능수량</summary>                 
        [Column("RETURN_POSSI_QTY"), Required(ErrorMessage = "ReturnPossiQty")] public decimal ReturnPossiQty { get; set; }
        /// <summary>반품수량</summary>                 
        [Column("RETURN_QTY"), Required(ErrorMessage = "ReturnQty")] public decimal ReturnQty { get; set; }
        /// <summary>입고단가</summary>               
        [Column("IN_COST")] public decimal? InCost { get; set; }
        /// <summary>입고 LOT NO</summary>            
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>납품처 LOT NO</summary>          
        [Column("IN_CUSTOMER_LOT_NO")] public string InCustomerLotNo { get; set; }
        /// <summary>합격수량</summary>                 
        [Column("OK_QTY")] public decimal OkQty { get; set; }
        /// <summary>불량수량</summary>                 
        [Column("NG_QTY")] public decimal NgQty { get; set; }
        /// <summary>입고창고</summary>               
        [Column("RETURN_WH_CODE")] public string ReturnWhCode { get; set; }
        /// <summary>입고위치</summary>               
        [Column("RETURN_WH_POSITION")] public string ReturnWhPosition { get; set; }
        /// <summary>수입검사상태</summary>           
        [Column("IN_INSPECTIONSTATE")] public string InInspectionState { get; set; }
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
        /// <summary>재입고여부</summary>                  
        [Column("RETURN_YN")] public string ReturnYn { get; set; }
        /// <summary>파일명</summary>                    
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>                   
        [Column("FILE_URL")] public string FileUrl { get; set; }




        //public virtual TN_PUR1101 TN_PUR1101 { get; set; }
        public virtual TN_PUR1304 TN_PUR1304 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        //public virtual ICollection<TN_PUR1301> TN_PUR1301List { get; set; }
    }
}