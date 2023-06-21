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
    [Table("SCM_VI_PUR1201_SCM")]
    public class SCM_VI_PUR1201_SCM
    {
        public SCM_VI_PUR1201_SCM()
        {
            _Check = "N";
        }
        [Key,Column("IN_NO",Order =0)] public string InNo { get; set; }
        [Key,Column("IN_SEQ",Order =1)] public Nullable<int> InSeq { get; set; }
        [Column("PO_NO")] public string PoNo { get; set; }
        [Column("PO_SEQ")] public Nullable<int> PoSeq { get; set; }
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        [Column("IN_QTY")] public Nullable<decimal> InQty { get; set; }
        [Column("IN_COST")] public Nullable<decimal> InCost { get; set; }
        [Column("IN_LOT_NO")] public string InLotno { get; set; }
        [Column("IN_CUSTOMER_LOT_NO")] public string InCustomerlot_no { get; set; }
        [Column("PRINT_QTY")] public Nullable<int> PrintQty { get; set; }
        [Column("IN_CONFIRM_FLAG")] public string InConfirmflag { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("MEMO1")] public string Memo1 { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [NotMapped] public string _Check { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}