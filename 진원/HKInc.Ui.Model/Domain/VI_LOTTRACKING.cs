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
    /// 20220111 오세완 차장 VI_LOT_TRACKING -> VI_LOT_TRACKING_V2로 변경
    /// </summary>
    [Table("VI_LOT_TRACKING_V2")]
    public class VI_LOTTRACKING
    {
        [Key,Column("WORK_NO",Order =0)] public string WorkNo { get; set; }
        [Key,Column("SEQ", Order =1)] public int Seq { get; set; }
        [Key,Column("ITEM_CODE", Order =2)] public string ItemCode { get; set; }
        [Key,Column("PROCESS_CODE", Order =3)] public string ProcessCode { get; set; }
        [Key,Column("LOT_NO", Order =4)] public string LotNo { get; set; }
        [Column("START_DATE")] public Nullable<DateTime> StartDate { get; set; }
        [Column("END_DATE")] public Nullable<DateTime> EndDate { get; set; }
        [Key,Column("RESULT_DATE",Order =5)] public Nullable<DateTime> ResultDate { get; set; }
        [Column("RESULT_QTY")] public Nullable<int> ResultQty { get; set; }
        [Column("FAIL_QTY")] public Nullable<int> FailQty { get; set; }
        [Column("OK_QTY")] public Nullable<int> OkQty { get; set; }
        [Column("WORK_ID")] public string WorkId { get; set; }
        [Column("PROCESS_TURN")] public Nullable<int> ProcessTurn { get; set; }
        [Column("MC_CODE")] public string McCode { get; set; }
        [Column("SRC_CODE")] public string SrcCode { get; set; }
        [Column("SRC_LOT")] public string SrcLot { get; set; }
        [Column("SRC_CODE1")] public string SrcCode1 { get; set; }
        [Column("SRC_LOT1")] public string SrcLot1 { get; set; }
        [Column("SRC_CODE2")] public string SrcCode2 { get; set; }
        [Column("SRC_LOT2")] public string SrcLot2 { get; set; }
        [Column("SRC_CODE3")] public string SrcCode3 { get; set; }
        [Column("SRC_LOT3")] public string SrcLot3 { get; set; }
        [Column("SRC_CODE4")] public string SrcCode4 { get; set; }
        [Column("SRC_LOT4")] public string SrcLot4 { get; set; }
        [Column("SRC_CODE5")] public string SrcCode5 { get; set; }
        [Column("SRC_LOT5")] public string SrcLot5 { get; set; }
        [Column("SRC_CODE6")] public string SrcCode6 { get; set; }
        [Column("SRC_LOT6")] public string SrcLot6 { get; set; }
        [Column("SRC_CODE7")] public string SrcCode7 { get; set; }
        [Column("SRC_LOT7")] public string SrcLot7 { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}