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
    /// <summary>제품기타입고관리 디테일</summary>	
    [Table("TN_ORD1301T")]
    public class TN_ORD1301 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1301()
        {
        }
        /// <summary>입고번호</summary>                 
        [ForeignKey("TN_ORD1300"), Key, Column("IN_NO", Order = 0), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>입고순번</summary>                 
        [Key, Column("IN_SEQ", Order = 1), Required(ErrorMessage = "InSeq")] public int InSeq { get; set; }
        /// <summary>입고 LOT NO</summary>              
        [Column("IN_LOT_NO"), Required(ErrorMessage = "InLotNo")] public string InLotNo { get; set; }
        /// <summary>입고량</summary>                   
        [Column("IN_QTY"), Required(ErrorMessage = "InQty")] public decimal InQty { get; set; }
        /// <summary>입고창고</summary>               
        [Column("IN_WH_CODE")] public string InWhCode { get; set; }
        /// <summary>입고위치</summary>               
        [Column("IN_WH_POSITION")] public string InWhPosition { get; set; }
        /// <summary>메모</summary>                     
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>
        /// 20211224 오세완 차장
        /// 정렬포장 여부 Y/N
        /// </summary>                     
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                    
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                    
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_ORD1300 TN_ORD1300 { get; set; }
    }
}