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
    /// <summary>제품기타출고관리 디테일</summary>	
    [Table("TN_ORD1401T")]
    public class TN_ORD1401 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1401()
        {
        }
        /// <summary>출고번호</summary>           
        [ForeignKey("TN_ORD1400"), Key, Column("OUT_NO", Order = 0), Required(ErrorMessage = "OutNo")] public string OutNo { get; set; }
        /// <summary>출고순번</summary>           
        [Key, Column("OUT_SEQ", Order = 1), Required(ErrorMessage = "OutSeq")] public int OutSeq { get; set; }
        /// <summary>출고 LOT NO</summary>        
        [Column("OUT_LOT_NO"), Required(ErrorMessage = "OutLotNo")] public string OutLotNo { get; set; }
        /// <summary>출고량</summary>             
        [Column("OUT_QTY"), Required(ErrorMessage = "OutQty")] public decimal OutQty { get; set; }
        /// <summary>창고코드</summary>  
        [Column("WH_CODE")] public string WhCode { get; set; }
        /// <summary>위치코드</summary>  
        [Column("POSITION_CODE")] public string PositionCode { get; set; }
        /// <summary>메모</summary>               
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>
        /// 20211224 오세완 차장
        /// 정렬포장여부 Y/N
        /// </summary>               
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>              
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>              
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_ORD1400 TN_ORD1400 { get; set; }
    }
}