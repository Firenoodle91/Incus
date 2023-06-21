﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220328 오세완 차장
    /// 자재입고 디테일
    /// </summary>
    [Table("TN_PUR1301T")]
    public class TN_PUR1301 : BaseDomain.BaseDomain
    {
        public TN_PUR1301() { }

        [ForeignKey("TN_PUR1300"),Key,Column("INPUT_NO",Order =0)] public string InputNo { get; set; }
        [Key,Column("INPUT_SEQ",Order =1)] public int InputSeq { get; set; }
        [Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        [Column("REQ_NO")] public string ReqNo { get; set; }
        [Column("REQ_SEQ")] public Nullable<int> ReqSeq { get; set; }
        [Column("REQ_QTY")] public Nullable<decimal> ReqQty { get; set; }        
        [Column("COST")] public string Cost { get; set; }
        [Column("INPUT_QTY"), Required(ErrorMessage = "InputQty")] public Nullable<decimal> InputQty { get; set; }
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
        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }
                
               
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