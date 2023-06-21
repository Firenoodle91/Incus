using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
   /// <summary>수주관리 디테일</summary>	
   [Table("TN_ORD1001T")]
    public class TN_ORD1001 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1001()
        {
            TN_ORD1100List = new List<TN_ORD1100>();
        }
        /// <summary>수주번호</summary>             
        [ForeignKey("TN_ORD1000"), Key, Column("ORDER_NO", Order = 0), Required(ErrorMessage = "OrderNo")] public string OrderNo { get; set; }
        /// <summary>순번</summary>                 
        [Key, Column("ORDER_SEQ", Order = 1), Required(ErrorMessage = "OrderSeq")] public int OrderSeq { get; set; }
        /// <summary>품번(도번)</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>수(중)량</summary>             
        [Column("ORDER_QTY"), Required(ErrorMessage = "OrderQty")] public decimal OrderQty { get; set; }
        /// <summary>수주단가</summary>             
        [Column("ORDER_COST")] public decimal? OrderCost { get; set; }
        /// <summary>마감월기준</summary>                 
        [Column("END_MONTH_DATE")] public DateTime? EndMonthDate { get; set; }
        /// <summary>생산마감월기준</summary>                 
        [Column("WORK_END_MONTH_DATE")] public DateTime? WorkEndMonthDate { get; set; }
        /// <summary>작업지시발행일</summary>                 
        [Column("WORK_NO_DATE")] public DateTime? WorkNoDate { get; set; }
        /// <summary>생산계획시작일</summary>                 
        [Column("PLAN_START_DATE")] public DateTime? PlanStartDate { get; set; }
        /// <summary>생산계획종료일</summary>                 
        [Column("PLAN_END_DATE")] public DateTime? PlanEndDate { get; set; }
        /// <summary>작업의뢰수량</summary>                 
        [Column("PLAN_WORK_QTY")] public decimal? PlanWorkQty { get; set; }
        /// <summary>생산의뢰</summary>                 
        [Column("PRODUCTION_FLAG")] public string ProductionFlag { get; set; }
        /// <summary>출고의뢰</summary>                 
        [Column("OUT_CONFIRM_FLAG")] public string OutConfirmFlag { get; set; }
        /// <summary>턴키의뢰</summary>                 
        [Column("TURN_KEY_FLAG")] public string TurnKeyFlag { get; set; }
        /// <summary>작업지시번호</summary>                 
        [Column("WORK_NO")] public string WorkNo { get; set; }
        /// <summary>작업지시서출력여부</summary>                 
        [Column("WORK_PRINT_FLAG")] public string WorkPrintFlag { get; set; }
        /// <summary>메모</summary>                 
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                 
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                
        [Column("TEMP2")]	public string Temp2 { get; set; }

        /// <summary>
        /// 납품계획여부
        /// </summary>
        [NotMapped] public string DelivFlag
        {
            get
            {
                var delivSumQty = TN_ORD1100List.Count == 0 ? 0 : TN_ORD1100List.Sum(p => p.DelivQty);
                return (OrderQty <= delivSumQty) ? "Y" : (delivSumQty <= 0 ? "N" : "R");
            }
        }

        [NotMapped]
        public string BanWorkNo
        {
            get
            {
                var obj = TN_ORD1100List.FirstOrDefault();
                if (obj == null) return null;
                else
                {
                    var obj2 = obj.TN_MPS1100List.FirstOrDefault();
                    if (obj2 == null) return null;
                    else
                    {
                        var obj3 = obj2.TN_MPS1200List.Where(p => p.Temp != null && p.Temp != "").FirstOrDefault();
                        if (obj3 == null) return null;
                        else return obj3.Temp;
                    }
                }
            }
        }

        [NotMapped]
        public string OutRepNo
        {
            get
            {
                var obj = TN_ORD1100List.FirstOrDefault();
                if (obj == null) return null;
                else
                {
                    var obj2 = obj.TN_ORD1101List.FirstOrDefault();
                    if (obj2 == null) return null;
                    else return obj2.TN_ORD1103.OutRepNo;
                }
            }
        }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_ORD1000 TN_ORD1000 { get; set; }
        public virtual ICollection<TN_ORD1100> TN_ORD1100List { get; set; }
    }
}