using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1301T")]
    public class TN_PUR1301 : BaseDomain.BaseDomain
    {
        public TN_PUR1301() {

            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            _Check = "N";
          
          
        }
        [ForeignKey("TN_PUR1300"),Key,Column("INPUT_NO",Order =0)] public string InputNo { get; set; }
        [Key,Column("INPUT_SEQ",Order =1)] public int InputSeq { get; set; }
        [Column("ITEM_CODE"), Required(ErrorMessage = "품목코드는 필수입니다.")] public string ItemCode { get; set; }
        [ForeignKey("TN_PUR1200"), Column("REQ_NO",Order = 10)] public string ReqNo { get; set; }
        [ForeignKey("TN_PUR1200"), Column("REQ_SEQ", Order = 11)] public int? ReqSeq { get; set; }
        [Column("REQ_QTY")] public string ReqQty { get; set; }        
        [Column("COST")] public string Cost { get; set; }
        [Column("INPUT_QTY"), Required(ErrorMessage = "입고수량은 필수입니다.")] public int InputQty { get; set; }
        [Column("INCOST")] public string InCost { get; set; }
        [Column("IN_YN")] public string InYn { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }
        [Column("WH_POSITION")] public string WhPosition { get; set; }
        [Column("LQTY")] public int Lqty { get; set; }
        public virtual TN_PUR1300 TN_PUR1300 { get; set; }
        public virtual TN_PUR1200 TN_PUR1200 { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
      
        [NotMapped] public string _Check { get; set; }                
               
        [NotMapped]
        public decimal ReqAmt {
            get {
                decimal ReqAmt = 0;
                try
                {
                    ReqAmt = Convert.ToDecimal(ReqQty) * Convert.ToDecimal(Cost);
                }
                catch {
                    ReqAmt = 0;
                }
                return ReqAmt;
            }
        }
        [NotMapped]
        public decimal InputAmt
        {
            get
            {
                
                decimal InAmt = 0;
                try
                {
                    InAmt = Convert.ToDecimal(InputQty) * Convert.ToDecimal(InCost);
                    
                }
                catch { InAmt = 0; }
                return InAmt;
            }
        }
    }
}