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
    /// 납품계획관리
    /// </summary>
    [Table("TN_ORD1100T")]
    public class TN_ORD1100 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1100()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;

            TN_PUR2000List = new List<TN_PUR2000>();
            TN_ORD1200List = new List<TN_ORD1200>();
        }
        [ForeignKey("TN_ORD1002"), Key, Column("ORDER_NO", Order = 0)] public string OrderNo { get; set; }
        [ForeignKey("TN_ORD1002"), Key,Column("SEQ",Order =1)] public int Seq { get; set; }
        [Key,Column("DELIV_SEQ",Order =2)] public string DelivSeq { get; set; }
        [Column("DELIV_DATE"), Required(ErrorMessage = "계획일자는 필수입니다.")] public Nullable<DateTime> DelivDate { get; set; }
        [Column("DELIV_QTY"), Required(ErrorMessage = "계획수량은 필수입니다.")] public int DelivQty { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }     
        [Column("DELIV_ID")] public string DelivId { get; set; }
        [Column("PROD_YN")] public string ProdYn { get; set; }
        [Column("OUT_CONFIRM_FLAG")] public string OutConfirmflag { get; set; }
        [Column("TURN_KEY_FLAG")] public string TurnKeyFlag { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }  // 수주처
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("DELIV_CON_DATE")] public Nullable<DateTime> OutDate { get; set; }

        [NotMapped]
        public int TurnKeyRemainQty
        {
            get
            {
                if (TN_PUR2000List.Count == 0) return DelivQty;
                else return DelivQty - TN_PUR2000List.Sum(p => p.PoQty);
            }
        }

        [NotMapped]
        public int ShipmentRemainQty
        {
            get
            {
                if (TN_ORD1200List.Count == 0) return DelivQty;
                else
                {
                    return DelivQty - (int)TN_ORD1200List.Sum(p => p.OutQty);
                }
            }
        }


        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual TN_ORD1002 TN_ORD1002 { get; set; }

        public virtual ICollection<TN_PUR2000> TN_PUR2000List { get; set; }
        public virtual ICollection<TN_ORD1200> TN_ORD1200List { get; set; }
    }

}