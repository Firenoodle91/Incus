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
    /// 작업지시투입정보
    /// </summary>
    [Table("TN_LOT_MST")]
    public class TN_LOT_MST
    {
        public TN_LOT_MST()
        {
            TN_MPS1405List = new List<TN_MPS1405>();
            TN_MPS1406List = new List<TN_MPS1406>();
            TN_MPS1407List = new List<TN_MPS1407>();
        }
        [Key, Column("WORK_NO", Order = 0)] public string WorkNo { get; set; }
        [Key, Column("LOT_NO", Order = 1)] public string LotNo { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("MC_CODE")] public string McCode { get; set; }
        [Column("SRC_CODE")] public string SrcCode { get; set; }
        [Column("SRC_LOT")] public string SrcLot { get; set; }
        [Column("WORKING_DATE")] public string WorkingDate { get; set; }
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
        [Column("ITEM1")] public string Item1 { get; set; }
        [Column("ITEM2")] public string Item2 { get; set; }
        [Column("ITEM3")] public string Item3 { get; set; }
        [Column("ITEM4")] public string Item4 { get; set; }
        [Column("KNIFE_CODE1")] public string KnifeCode1 { get; set; }
        [Column("KNIFE_CODE2")] public string KnifeCode2 { get; set; }
        [Column("KNIFE_CODE3")] public string KnifeCode3 { get; set; }
        [Column("KNIFE_CODE4")] public string KnifeCode4 { get; set; }
        [Column("KNIFE_CODE5")] public string KnifeCode5 { get; set; }
        [Column("KNIFE_CODE6")] public string KnifeCode6 { get; set; }
        [Column("KNIFE_CODE7")] public string KnifeCode7 { get; set; }

        public virtual ICollection<TN_MPS1405> TN_MPS1405List { get; set; }
        public virtual ICollection<TN_MPS1406> TN_MPS1406List { get; set; }
        public virtual ICollection<TN_MPS1407> TN_MPS1407List { get; set; }
    }
}