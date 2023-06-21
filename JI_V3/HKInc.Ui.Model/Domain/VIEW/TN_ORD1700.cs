using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 월별납품관리
    /// JI 엑셀파일 '납품202101' 달성율 시트 참조
    /// </summary>	

    [Table("TN_ORD1700T")]
    public class TN_ORD1700 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1700()
        {

        }

        /// <summary>
        /// 업체코드
        /// </summary>
        [Key, Column("CUSTOMER_CODE", Order = 0), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }

        /// <summary>
        /// 품목코드
        /// </summary>
        [Key, Column("ITEM_CODE", Order = 1), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>
        /// 년
        /// </summary>
        [Key, Column("YYYY", Order = 2), Required(ErrorMessage = "YYYY")] public string YYYY { get; set; }

        /// <summary>
        /// 월
        /// </summary>
        [Key, Column("MM", Order = 3), Required(ErrorMessage = "MM")] public string MM { get; set; }


        /// <summary>
        /// 계획수량
        /// </summary>
        [Column("PLAN_QTY")] public decimal PlanQty { get; set; }

        

        /// <summary>
        /// 단가
        /// </summary>               
        [Column("COST")] public decimal Cost { get; set; }

        [Column("D01")] public decimal D01 { get; set; }
        [Column("D02")] public decimal D02 { get; set; }
        [Column("D03")] public decimal D03 { get; set; }
        [Column("D04")] public decimal D04 { get; set; }
        [Column("D05")] public decimal D05 { get; set; }
        [Column("D06")] public decimal D06 { get; set; }
        [Column("D07")] public decimal D07 { get; set; }
        [Column("D08")] public decimal D08 { get; set; }
        [Column("D09")] public decimal D09 { get; set; }
        [Column("D10")] public decimal D10 { get; set; }
        [Column("D11")] public decimal D11 { get; set; }
        [Column("D12")] public decimal D12 { get; set; }
        [Column("D13")] public decimal D13 { get; set; }
        [Column("D14")] public decimal D14 { get; set; }
        [Column("D15")] public decimal D15 { get; set; }
        [Column("D16")] public decimal D16 { get; set; }
        [Column("D17")] public decimal D17 { get; set; }
        [Column("D18")] public decimal D18 { get; set; }
        [Column("D19")] public decimal D19 { get; set; }
        [Column("D20")] public decimal D20 { get; set; }
        [Column("D21")] public decimal D21 { get; set; }
        [Column("D22")] public decimal D22 { get; set; }
        [Column("D23")] public decimal D23 { get; set; }
        [Column("D24")] public decimal D24 { get; set; }
        [Column("D25")] public decimal D25 { get; set; }
        [Column("D26")] public decimal D26 { get; set; }
        [Column("D27")] public decimal D27 { get; set; }
        [Column("D28")] public decimal D28 { get; set; }
        [Column("D29")] public decimal D29 { get; set; }
        [Column("D30")] public decimal D30 { get; set; }
        [Column("D31")] public decimal D31 { get; set; }

        // XRREP5009 그리드에서 차종 표시하기 위해 필수
        // MasterGridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        /// <summary>
        /// 납품수량
        /// </summary>
        [Column("DELIV_QTY")]
        public decimal DelivQty
        {
            get
            {
                decimal Sum = 0;

                decimal[] Qty =
                {
                    D01,D02,D03,D04,D05,D06,D07,D08,D09,D10,
                    D11,D12,D13,D14,D15,D16,D17,D18,D19,D20,
                    D21,D22,D23,D24,D25,D26,D27,D28,D29,D30,
                    D31
                };

                for (int i = 0; i < Qty.Length; i++)
                {
                    Sum += Qty[i];
                }

                return Sum;
            }

            set
            {

            }
        }

        public decimal DueMQty //미납수량
        {
            get
            {
                return PlanQty - DelivQty;
            }
        }

        public string DueRate //달성율
        {
            get
            {
                string rate = string.Empty;

                if (PlanQty != 0)
                {
                    rate = string.Format("{0:##0.##}", DelivQty / PlanQty * 100);
                }

                return rate + " %";
            }
        }

        public decimal DuePlanCost //계획금액
        {
            get
            {
                return PlanQty * Cost;
            }
        }
        
        public decimal DueCost //납품금액
        {
            get
            {
                return DelivQty * Cost;
            }
        }

        public decimal DueMCost //미납금액
        {
            get
            {
                return DuePlanCost - DueCost;
                //return DueMQty * Cost;
            }
        }


    }
}