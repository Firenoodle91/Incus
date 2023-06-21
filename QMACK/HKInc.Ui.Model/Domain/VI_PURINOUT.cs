using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 자재재고관리 입출고상세내역
    /// </summary>
    [Table("VI_PURINOUT")]
    public class VI_PURINOUT
    {
        [Key, Column("NO", Order = 0)] public string NO { get; set; }           // 2022-04-01 김진우 수정 Num => No
        [Key, Column("SEQ", Order = 1)] public Int32 SEQ { get; set; }          // 2022-04-01 김진우 추가
        [Column("INPUT_DATE")] public DateTime InOutDate { get; set; }          // 2022-04-01 김진우 키값 제거
        [Column("ITEM_CODE")] public string ItemCode { get; set; }              // 2022-04-01 김진우 키값 제거
        //[Key, Column("INPUT_DATE", Order = 1)] public DateTime InOutDate { get; set; }
        //[Key, Column("ITEM_CODE", Order = 2)] public string ItemCode { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }                  // 2022-04-01 김진우 추가
        [Column("ITEM_NM1")] public string ItemNm1 { get; set; }                // 2022-04-01 김진우 추가
        [Column("INPUT_QTY")] public Nullable<decimal> InputQty { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
        [Column("WORK")] public string Work { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }                  // 2022-04-01 김진우 추가
        [Column("WH_POSITION")] public string WhPosition { get; set; }          // 2022-04-01 김진우 추가
    }
}